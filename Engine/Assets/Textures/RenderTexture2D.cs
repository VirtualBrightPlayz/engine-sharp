using System;
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
        public override bool IsValid => ColorTex != null;
        public Texture ColorTex { get; private set; }
        public IntPtr ImGuiTex { get; private set; }
        public bool IsRaw { get; private set; }
        public Framebuffer InternalFramebuffer { get; private set; }
        public Swapchain InternalSwapchain { get; private set; }
        public Sampler InternalSampler { get; private set; }
        public SamplerInfo Info { get; private set; } = SamplerInfo.Linear;
        public uint Width { get; private set; }
        public uint Height { get; private set; }

        public BindableResource[] Bindables => new BindableResource[]
        {
            ColorTex,
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

        protected override void ReCreateInternal()
        {
            if (IsRaw)
                return;
            TextureDescription colorDesc = TextureDescription.Texture2D(Width, Height, 1, 1, PixelFormat.R8_G8_B8_A8_UNorm, TextureUsage.Sampled | TextureUsage.RenderTarget);
            ColorTex = ResourceManager.GraphicsFactory.CreateTexture(colorDesc);
            ColorTex.Name = Name;
            FramebufferDescription fbDesc = new FramebufferDescription(null, ColorTex);
            InternalFramebuffer = ResourceManager.GraphicsFactory.CreateFramebuffer(fbDesc);
            UpdateSamplerInfo(Info);
        }

        protected override Resource CloneInternal(string cloneName)
        {
            return this;
        }

        public bool HasDepth()
        {
            return IsRaw && (InternalFramebuffer != null ? InternalFramebuffer.DepthTarget.HasValue : InternalSwapchain.Framebuffer.DepthTarget.HasValue);
        }

        public void UpdateSamplerInfo(SamplerInfo info)
        {
            if (IsRaw)
                return;
            Info = info;
            if (InternalSampler != null && !InternalSampler.IsDisposed)
                InternalSampler.Dispose();
            InternalSampler = ResourceManager.GraphicsFactory.CreateSampler(Info.GetSamplerDescription());
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
            if (ColorTex != null && !ColorTex.IsDisposed)
                ColorTex.Dispose();
            ColorTex = null;
        }
    }
}