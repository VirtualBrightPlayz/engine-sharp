using System;
using System.Collections.Generic;
using System.Numerics;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
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

        public Renderer(string name)
        {
            Name = name;
            ReCreate();
            ViewMatrix = Matrix4x4.Identity;
            ViewMatrixResource = new UniformBuffer(UniformConsts.ViewMatrixName, (uint)16 * 4);
            ViewMatrixResource.UploadData(ViewMatrix);
            ProjMatrixResource = new UniformBuffer(UniformConsts.ProjectionMatrixName, (uint)16 * 4);
            ProjMatrixResource.UploadData(ProjectionMatrix);
            WorldInfoResource = new UniformBuffer("WorldInfo", (uint)4 * 4);
            WorldInfoResource.UploadData(ViewPosition);
        }

        public override void ReCreate()
        {
            if (HasBeenInitialized)
                return;
            base.ReCreate();
            if (_commandList != null && !_commandList.IsDisposed)
                _commandList.Dispose();
            if (ViewMatrixResource != null)
                ViewMatrixResource.ReCreate();
            if (ProjMatrixResource != null)
                ProjMatrixResource.ReCreate();
            if (WorldInfoResource != null)
                WorldInfoResource.ReCreate();
            foreach (var item in MatrixBuffers)
            {
                item.Value.ReCreate();
            }
            _commandList = ResourceManager.GraphicsFactory.CreateCommandList();
            _commandList.Name = Name;
        }

        public override Resource Clone(string cloneName)
        {
            throw new NotImplementedException();
        }

        public void Begin()
        {
            ViewMatrixResource.UploadData(ViewMatrix);
            ProjMatrixResource.UploadData(ProjectionMatrix);
            WorldInfoResource.UploadData(ViewPosition);
            _commandList.Begin();
            // _commandList.SetFramebuffer(InternalRenderTexture.InternalFramebuffer ?? InternalRenderTexture.InternalSwapchain.Framebuffer);
            _commandList.SetFramebuffer(Program.GameGraphics.SwapchainFramebuffer);
        }

        public void SetupStandardMatrixUniforms(Material material)
        {
            if (!MatrixBuffers.ContainsKey(material.Shader))
                MatrixBuffers[material.Shader] = new CompoundBuffer("ProjViewMatrixBuffer", material.Shader, UniformConsts.ViewProjectionMatrixBufferSet, ViewMatrixResource, ProjMatrixResource);
            material.SetUniforms(UniformConsts.ViewProjectionMatrixBufferSet, MatrixBuffers[material.Shader]);
        }

        public void SetupStandardWorldInfoUniforms(Material material, uint setId)
        {
            if (!WorldInfoBuffers.ContainsKey(material.Shader))
                WorldInfoBuffers[material.Shader] = new CompoundBuffer("WorldInfoBuffer", material.Shader, setId, WorldInfoResource);
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
            _commandList.ClearDepthStencil(1f, 0);
        }

        public void End()
        {
            _commandList.End();
        }

        public void Submit()
        {
            Program.GameGraphics.SubmitCommands(_commandList);
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