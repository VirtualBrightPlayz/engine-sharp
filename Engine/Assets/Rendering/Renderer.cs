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
    public class Renderer : Resource
    {
        public static Renderer Current { get; set; }
        public override bool IsValid => _commandList != null && !_commandList.IsDisposed;
        private CommandList _commandList;
        public CommandList CommandList => _commandList;
        public Matrix4x4 ViewMatrix { get; set; }
        public Matrix4x4 ProjectionMatrix { get; set; }
        public Vector3 ViewPosition { get; set; }
        public UniformBuffer ViewMatrixResource { get; private set; }
        public UniformBuffer ProjMatrixResource { get; private set; }
        public UniformBuffer WorldInfoResource { get; private set; }
        public Dictionary<GraphicsShader, CompoundBuffer> MatrixBuffers { get; private set; } = new Dictionary<GraphicsShader, CompoundBuffer>();
        public Dictionary<GraphicsShader, CompoundBuffer> WorldInfoBuffers { get; private set; } = new Dictionary<GraphicsShader, CompoundBuffer>();
        public RenderTexture2D InternalRenderTexture { get; private set; }
        private Mesh BlitMesh;
        private Task<GraphicsShader> BlitShader => ResourceManager.LoadShader("Shaders/Blit");

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

        public Renderer(string name)
        {
            Name = name;
            // ReCreate();
            ViewMatrix = Matrix4x4.Identity;
            ViewMatrixResource = new UniformBuffer(UniformConsts.ViewMatrixName, (uint)16 * 4);
            // ViewMatrixResource.UploadData(ViewMatrix);
            ProjMatrixResource = new UniformBuffer(UniformConsts.ProjectionMatrixName, (uint)16 * 4);
            // ProjMatrixResource.UploadData(ProjectionMatrix);
            WorldInfoResource = new UniformBuffer("WorldInfo", (uint)4 * 4);
            // WorldInfoResource.UploadData(ViewPosition);
        }

        public override async Task ReCreate()
        {
            if (HasBeenInitialized)
                return;
            await base.ReCreate();
            if (_commandList != null && !_commandList.IsDisposed)
                _commandList.Dispose();
            if (ViewMatrixResource != null)
                await ViewMatrixResource.ReCreate();
            if (ProjMatrixResource != null)
                await ProjMatrixResource.ReCreate();
            if (WorldInfoResource != null)
                await WorldInfoResource.ReCreate();
            foreach (var item in MatrixBuffers)
            {
                await item.Value.ReCreate();
            }
            _commandList = ResourceManager.GraphicsFactory.CreateCommandList();
            _commandList.Name = Name;
            BlitMesh = await ResourceManager.CreateMesh("BlitMesh", false, await ResourceManager.CreateMaterial("BlitMaterial", await BlitShader));
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
            BlitMesh.Indices.AddRange(inds);
            BlitMesh.UploadData(blitVertices);
        }

        public override Task<Resource> Clone(string cloneName)
        {
            throw new NotImplementedException();
        }

        public void Begin()
        {
            _commandList.Begin();
            _commandList.SetFramebuffer(InternalRenderTexture.InternalFramebuffer ?? InternalRenderTexture.InternalSwapchain.Framebuffer);
            // _commandList.SetFramebuffer(Program.GameGraphics.SwapchainFramebuffer);
            ViewMatrixResource.UploadData(this, ViewMatrix);
            ProjMatrixResource.UploadData(this, ProjectionMatrix);
            WorldInfoResource.UploadData(this, ViewPosition);
        }

        public async Task SetupStandardMatrixUniforms(Material material)
        {
            if (!MatrixBuffers.ContainsKey(material.Shader))
            {
                MatrixBuffers[material.Shader] = new CompoundBuffer("ProjViewMatrixBuffer", material.Shader, UniformConsts.ViewProjectionMatrixBufferSet, ViewMatrixResource, ProjMatrixResource);
                await MatrixBuffers[material.Shader].ReCreate();
            }
            material.SetUniforms(UniformConsts.ViewProjectionMatrixBufferSet, MatrixBuffers[material.Shader]);
        }

        public async Task SetupStandardWorldInfoUniforms(Material material, uint setId)
        {
            if (!WorldInfoBuffers.ContainsKey(material.Shader))
            {
                WorldInfoBuffers[material.Shader] = new CompoundBuffer("WorldInfoBuffer", material.Shader, setId, WorldInfoResource);
                await WorldInfoBuffers[material.Shader].ReCreate();
            }
            material.SetUniforms(setId, WorldInfoBuffers[material.Shader]);
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
            _commandList.End();
        }

        public void Submit()
        {
            RenderingGlobals.GameGraphics.SubmitCommands(_commandList);
        }

        public void Blit(Texture2D tex)
        {
            BlitMesh.InternalMaterial.ClearUniforms(0);
            BlitMesh.InternalMaterial.SetUniforms(0, new UniformLayout("Diffuse", tex, false, true));
            BlitMesh.Draw(this, "main");
        }

        public override void Dispose()
        {
            foreach (var cbuf in MatrixBuffers)
            {
                cbuf.Value.Dispose();
            }
            MatrixBuffers.Clear();
            ViewMatrixResource.Dispose();
            ProjMatrixResource.Dispose();
            WorldInfoResource.Dispose();
            _commandList.Dispose();
        }
    }
}