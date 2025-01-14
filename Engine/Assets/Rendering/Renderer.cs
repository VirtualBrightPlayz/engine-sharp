using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Engine.Assets.Models;
using Engine.Assets.Textures;
using Veldrid;

namespace Engine.Assets.Rendering
{
    public class Renderer : Resource, IDisposable
    {
        public struct RenderBatch
        {
            public List<Mesh> meshes;
            public List<Action<Renderer, RenderBatch, int>> callbacks;
            public List<object> instances;
            public Material material;
            public string pass;
        }

        public static Renderer Current { get; set; }
        public override bool IsValid => _commandList != null && !_commandList.IsDisposed;
        private CommandList _commandList;
        public CommandList CommandList => _commandList;
        public Matrix4x4 ViewMatrix { get; set; }
        public Matrix4x4 ProjectionMatrix { get; set; }
        // public Matrix4x4 WorldMatrix { get; set; }
        public Vector3 ViewPosition { get; set; }
        public UniformBuffer ViewMatrixResource { get; private set; }
        public UniformBuffer ProjMatrixResource { get; private set; }
        // public UniformBuffer WorldMatrixResource { get; private set; }
        public UniformBuffer WorldInfoResource { get; private set; }
        public Dictionary<GraphicsShader, CompoundBuffer> MatrixBuffers { get; private set; } = new Dictionary<GraphicsShader, CompoundBuffer>();
        public Dictionary<GraphicsShader, CompoundBuffer> WorldInfoBuffers { get; private set; } = new Dictionary<GraphicsShader, CompoundBuffer>();
        public Material BoundMaterial { get; private set; }
        public RenderTexture2D InternalRenderTexture { get; private set; }
        private List<RenderBatch> batches = new List<RenderBatch>();
        private Mesh BlitMesh;
        private Material BlitMaterial;
        private GraphicsShader BlitShader = new GraphicsShader("Shaders/Blit");
        public bool IsDrawing { get; private set; } = false;

        [StructLayout(LayoutKind.Sequential)]
        public struct BlitVertex : IVertex
        {
            public Vector3 Position;
            public Vector2 UV0;
            public Vector4 Color;

            public BlitVertex(Vector3 pos, Vector2 uv, Vector4 col)
            {
                Position = pos;
                UV0 = uv;
                Color = col;
            }
        }

        public Renderer(string name) : base(name)
        {
            ViewMatrix = Matrix4x4.Identity;
            ReCreate();
        }

        protected override void ReCreateInternal()
        {
            if (_commandList != null && !_commandList.IsDisposed)
                _commandList.Dispose();
            ViewMatrixResource = new UniformBuffer(UniformConsts.ViewMatrixName, (uint)16 * 4);
            ProjMatrixResource = new UniformBuffer(UniformConsts.ProjectionMatrixName, (uint)16 * 4);
            // WorldMatrixResource = new UniformBuffer(UniformConsts.WorldMatrixName, (uint)16 * 4);
            WorldInfoResource = new UniformBuffer("WorldInfo", (uint)4 * 4);
            foreach (var item in MatrixBuffers)
            {
                item.Value.ReCreate();
            }
            foreach (var item in WorldInfoBuffers)
            {
                item.Value.ReCreate();
            }
            _commandList = ResourceManager.GraphicsFactory.CreateCommandList();
            _commandList.Name = Name;
            BlitMesh = new Mesh("BlitMesh", false);
            BlitMaterial = new Material("BlitMaterial", BlitShader);
            BlitVertex[] blitVertices = new BlitVertex[]
            {
                new BlitVertex(new Vector3(-1f, -1f, 1f), new Vector2(0f, 1f), Vector4.One),
                new BlitVertex(new Vector3(-1f, 1f, 1f), new Vector2(0f, 0f), Vector4.One),
                new BlitVertex(new Vector3(1f, -1f, 1f), new Vector2(1f, 1f), Vector4.One),
                new BlitVertex(new Vector3(1f, 1f, 1f), new Vector2(1f, 0f), Vector4.One),
            };
            uint[] inds = new uint[]
            {
                0, 1, 2,
                3, 2, 1,
            };
            BlitMesh.Indices.Clear();
            BlitMesh.Indices.AddRange(inds);
            BlitMesh.UploadData(blitVertices);
        }

        protected override Resource CloneInternal(string cloneName)
        {
            throw new NotImplementedException();
        }

        public void Begin()
        {
            IsDrawing = true;
            _commandList.Begin();
            _commandList.SetFramebuffer(InternalRenderTexture.InternalFramebuffer ?? InternalRenderTexture.InternalSwapchain.Framebuffer);
            ViewMatrixResource.UploadData(this, ViewMatrix);
            ProjMatrixResource.UploadData(this, ProjectionMatrix);
            WorldInfoResource.UploadData(this, ViewPosition);
        }

        public void SetupStandardMatrixUniforms(Material material)
        {
            if (!MatrixBuffers.ContainsKey(material.Shader))
            {
                MatrixBuffers[material.Shader] = new CompoundBuffer("ProjViewMatrixBuffer", material.Shader, UniformConsts.ViewMatrixName, ViewMatrixResource, ProjMatrixResource);
                MatrixBuffers[material.Shader].ReCreate();
            }
            material.SetUniforms(UniformConsts.ViewProjectionMatrixBufferSet, MatrixBuffers[material.Shader]);
        }

        public void SetupStandardWorldInfoUniforms(Material material, uint setId)
        {
            if (!WorldInfoBuffers.ContainsKey(material.Shader))
            {
                // WorldInfoBuffers[material.Shader] = new CompoundBuffer("WorldInfoBuffer", material.Shader, setId, WorldInfoResource);
                // WorldInfoBuffers[material.Shader].ReCreate();
            }
            material.SetUniforms(setId, new UniformLayout("WorldInfoBuffer", WorldInfoResource, true, true));
        }

        public void SetRenderTarget(RenderTexture2D target)
        {
            InternalRenderTexture = target;
        }

        public void SetFramebuffer(Framebuffer fb)
        {
            _commandList.SetFramebuffer(fb);
        }

        public void Clear()
        {
            _commandList.ClearColorTarget(0, RgbaFloat.Black);
            if (InternalRenderTexture.HasDepth())
                _commandList.ClearDepthStencil(1f, 0);
        }

        public void End()
        {
            for (int i = 0; i < batches.Count; i++)
            {
                BindMaterial(batches[i].material, batches[i].pass);
                for (int j = 0; j < batches[i].meshes.Count; j++)
                {
                    batches[i].callbacks[j].Invoke(this, batches[i], j);
                    DrawMeshNow(batches[i].meshes[j]);
                }
            }
            _commandList.End();
            IsDrawing = false;
            batches.Clear();
        }

        public void Submit()
        {
            RenderingGlobals.GameGraphics.SubmitCommands(_commandList);
            // RenderingGlobals.GameGraphics.WaitForIdle();
        }

        public void Blit(Texture2D tex)
        {
            BlitMaterial.ClearUniforms(0);
            BlitMaterial.SetUniforms(0, new UniformLayout("Diffuse", tex, false, true));
            BlitMaterial.PreDraw(this);
            BlitMaterial.Bind(this, "main");
            DrawMeshNow(BlitMesh);
        }

        public void Blit(Material mat)
        {
            BindMaterial(mat);
            DrawMeshNow(BlitMesh);
        }

        public void NewPass()
        {
            End();
            Submit();
            Begin();
        }

        public void BindMaterial(Material material, string pass = "main", bool bindPipeline = true)
        {
            BoundMaterial = material;
            SetupStandardMatrixUniforms(material);
            material.PreDraw(this);
            material.Bind(this, pass.Split('#')[0], bindPipeline);
        }

        public void DrawMesh(Mesh mesh, Action<Renderer, RenderBatch, int> callback, object instanceData, Material material, string pass = "main")
        {
            int idx = batches.FindIndex(x => x.material == material && x.pass == pass);
            if (idx == -1)
            {
                batches.Add(new RenderBatch()
                {
                    meshes = new List<Mesh>()
                    {
                        mesh,
                    },
                    callbacks = new List<Action<Renderer, RenderBatch, int>>()
                    {
                        callback,
                    },
                    instances = new List<object>()
                    {
                        instanceData,
                    },
                    material = material,
                    pass = pass,
                });
            }
            else
            {
                batches[idx].meshes.Add(mesh);
                batches[idx].callbacks.Add(callback);
                batches[idx].instances.Add(instanceData);
            }
        }

        public void DrawMeshNow(Mesh mesh)
        {
            // WorldMatrixResource.UploadData(this, WorldMatrix);
            if (!mesh.IsValidForRenderer(this))
                mesh.UploadToRenderer(this);
            mesh.Bind(this);
        }

        protected override void DisposeInternal()
        {
            foreach (var cbuf in MatrixBuffers)
            {
                cbuf.Value.Dispose();
            }
            MatrixBuffers.Clear();
            foreach (var item in WorldInfoBuffers)
            {
                item.Value.Dispose();
            }
            WorldInfoBuffers.Clear();
            ViewMatrixResource?.Dispose();
            // ViewMatrixResource = null;
            ProjMatrixResource?.Dispose();
            // ProjMatrixResource = null;
            // WorldMatrixResource?.Dispose();
            WorldInfoResource?.Dispose();
            // WorldInfoResource = null;
            _commandList.Dispose();
            _commandList = null;
        }
    }
}