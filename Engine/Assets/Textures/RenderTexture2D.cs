using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Engine.Assets.Rendering;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Veldrid;

namespace Engine.Assets.Textures
{
    public class RenderTexture2D : Resource, IMaterialBindable
    {
        public override bool IsValid => ColorTex != null && DepthTex != null;
        public Texture ColorTex { get; private set; }
        public Texture DepthTex { get; private set; }
        public IntPtr ImGuiTex { get; private set; }
        public bool IsRaw { get; private set; }
        public Framebuffer InternalFramebuffer { get; private set; }
        public Swapchain InternalSwapchain { get; private set; }
        public Sampler InternalSampler { get; private set; }
        public SamplerInfo Info { get; private set; } = SamplerInfo.Linear;
        public uint Width { get; private set; }
        public uint Height { get; private set; }
        private Dictionary<GraphicsShader, Dictionary<uint, ResourceSet>> resSets = new Dictionary<GraphicsShader, Dictionary<uint, ResourceSet>>();

        public BindableResource[] Bindables => new BindableResource[]
        {
            ColorTex,
            DepthTex,
            InternalSampler,
        };

        public RenderTexture2D(string name, Framebuffer framebuffer) : base(name)
        {
            InternalFramebuffer = framebuffer;
            Width = InternalFramebuffer.Width;
            Height = InternalFramebuffer.Height;
            IsRaw = true;
        }

        public RenderTexture2D(string name, Swapchain swapchain) : base(name)
        {
            InternalSwapchain = swapchain;
            Width = InternalSwapchain.Framebuffer.Width;
            Height = InternalSwapchain.Framebuffer.Height;
            IsRaw = true;
        }

        public RenderTexture2D(string name, uint width, uint height) : base(name)
        {
            Width = width;
            Height = height;
            IsRaw = false;
            ReCreate();
        }

        public void Bind(Renderer renderer, Material material, uint setId)
        {
            if (!resSets.ContainsKey(material.Shader))
            {
                resSets[material.Shader] = new Dictionary<uint, ResourceSet>();
            }
            if (!resSets[material.Shader].ContainsKey(setId))
            {
                ResourceSetDescription desc = new ResourceSetDescription(material.Shader._reflResourceLayouts[(int)setId], Bindables);
                resSets[material.Shader][setId] = ResourceManager.GraphicsFactory.CreateResourceSet(desc);
                resSets[material.Shader][setId].Name = Name;
            }
            renderer.CommandList.SetGraphicsResourceSet(setId, resSets[material.Shader][setId]);
        }

        protected override void ReCreateInternal()
        {
            if (IsRaw)
                return;
            foreach (var kvp in resSets)
            {
                foreach (var set in kvp.Value)
                {
                    set.Value.Dispose();
                }
            }
            resSets.Clear();
            TextureDescription colorDesc = TextureDescription.Texture2D(Width, Height, 1, 1, PixelFormat.R8_G8_B8_A8_UNorm, TextureUsage.Sampled | TextureUsage.RenderTarget);
            ColorTex = ResourceManager.GraphicsFactory.CreateTexture(colorDesc);
            ColorTex.Name = Name;
            TextureDescription depthDesc = TextureDescription.Texture2D(Width, Height, 1, 1, PixelFormat.D32_Float_S8_UInt, TextureUsage.Sampled | TextureUsage.DepthStencil);
            DepthTex = ResourceManager.GraphicsFactory.CreateTexture(depthDesc);
            DepthTex.Name = Name;
            FramebufferDescription fbDesc = new FramebufferDescription(DepthTex, ColorTex);
            InternalFramebuffer = ResourceManager.GraphicsFactory.CreateFramebuffer(fbDesc);
            UpdateSamplerInfo(Info);
        }

        protected override Resource CloneInternal(string cloneName)
        {
            return this;
        }

        public bool HasDepth()
        {
            return (DepthTex != null && !DepthTex.IsDisposed) || (IsRaw && (InternalFramebuffer != null ? InternalFramebuffer.DepthTarget.HasValue : InternalSwapchain.Framebuffer.DepthTarget.HasValue));
        }

        public void UpdateSamplerInfo(SamplerInfo info)
        {
            if (IsRaw)
                return;
            Info = info;
            if (InternalSampler != null && !InternalSampler.IsDisposed)
                InternalSampler.Dispose();
            InternalSampler = ResourceManager.GraphicsFactory.CreateSampler(Info.GetSamplerDescription(1));
            InternalSampler.Name = Name;
        }

        protected override void DisposeInternal()
        {
            if (IsRaw)
                return;
            if (InternalSampler != null && !InternalSampler.IsDisposed)
                InternalSampler.Dispose();
            InternalSampler = null;
            if (RenderingGlobals.GameImGui != null && ColorTex != null && ColorTex.IsDisposed)
                RenderingGlobals.GameImGui.RemoveImGuiBinding(ColorTex);
            if (InternalFramebuffer != null && !InternalFramebuffer.IsDisposed)
                InternalFramebuffer.Dispose();
            InternalFramebuffer = null;
            if (DepthTex != null && !DepthTex.IsDisposed)
                DepthTex.Dispose();
            DepthTex = null;
            if (ColorTex != null && !ColorTex.IsDisposed)
                ColorTex.Dispose();
            ColorTex = null;
        }
    }
}