using System;
using System.IO;
using Engine.Assets.Rendering;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using Veldrid;

namespace Engine.Assets.Textures
{
    public enum WrapMode : byte
    {
        Wrap,
        Mirror,
        Clamp,
    }

    public enum FilterMode : byte
    {
        Point,
        Linear,
        Anisotropic,
    }

    public struct SamplerInfo
    {
        public WrapMode wrap;
        public FilterMode filter;
        public uint maxAnsio;
        public static SamplerInfo Point => new SamplerInfo(WrapMode.Wrap, FilterMode.Point, 0);
        public static SamplerInfo Linear => new SamplerInfo(WrapMode.Wrap, FilterMode.Linear, 0);
        public static SamplerInfo Ansio4x => new SamplerInfo(WrapMode.Wrap, FilterMode.Anisotropic, 4);

        public SamplerInfo(WrapMode wrap, FilterMode filter, uint maxAnsio)
        {
            this.wrap = wrap;
            this.filter = filter;
            this.maxAnsio = maxAnsio;
        }

        public SamplerDescription GetSamplerDescription()
        {
            SamplerAddressMode addressMode = SamplerAddressMode.Wrap;
            switch (wrap)
            {
                case WrapMode.Mirror:
                    addressMode = SamplerAddressMode.Mirror;
                    break;
                case WrapMode.Clamp:
                    addressMode = SamplerAddressMode.Clamp;
                    break;
            }
            SamplerFilter filterMode = SamplerFilter.MinLinear_MagLinear_MipLinear;
            switch (filter)
            {
                case FilterMode.Point:
                    filterMode = SamplerFilter.MinPoint_MagPoint_MipPoint;
                    break;
                case FilterMode.Anisotropic:
                    filterMode = SamplerFilter.Anisotropic;
                    break;
            }
            return new SamplerDescription(addressMode, addressMode, addressMode, filterMode, null, maxAnsio, 0, 0, 0, SamplerBorderColor.TransparentBlack);
        }
    }

    public class Texture2D : Resource, IMaterialBindable
    {
        public override bool IsValid => Tex != null;
        public Texture Tex { get; private set; }
        public IntPtr ImGuiTex { get; private set; }
        public Sampler InternalSampler { get; private set; }
        public SamplerInfo Info { get; private set; } = SamplerInfo.Linear;
        public static Texture2D DefaultWhite => ResourceManager.LoadTexture("Shaders/white.bmp");
        public static Texture2D DefaultNormal => ResourceManager.LoadTexture("Shaders/bump.bmp");
        private string _path;
        private Rgba32[] _texData;
        public uint Width { get; private set; }
        public uint Height { get; private set; }

        public BindableResource[] Bindables => new BindableResource[]
        {
            Tex,
            InternalSampler,
        };

        public Texture2D(string path) : this(path, path)
        {
        }

        public Texture2D(string name, string path)
        {
            Name = name;
            _path = path;
            ReCreate();
        }

        public override void ReCreate()
        {
            if (HasBeenInitialized)
                return;
            base.ReCreate();
            if (Tex != null && !Tex.IsDisposed)
                Tex.Dispose();
            if (_texData == null)
            {
                using Image<Rgba32> img = Image.Load<Rgba32>(_path);
                Width = (uint)img.Width;
                Height = (uint)img.Height;
                _texData = new Rgba32[img.Width * img.Height];
                img.CopyPixelDataTo(_texData);
            }
            TextureDescription desc = TextureDescription.Texture2D(Width, Height, 1, 1, PixelFormat.R8_G8_B8_A8_UNorm, TextureUsage.Sampled);
            Tex = ResourceManager.GraphicsFactory.CreateTexture(desc);
            Tex.Name = Name;
            Program.GameGraphics.UpdateTexture(Tex, _texData, 0, 0, 0, Width, Height, 1, 0, 0);
            InternalSampler = ResourceManager.GraphicsFactory.CreateSampler(Info.GetSamplerDescription());
            InternalSampler.Name = Name;
            ImGuiTex = Program.GameImGui.GetOrCreateImGuiBinding(ResourceManager.GraphicsFactory, Tex);
        }

        public override Resource Clone(string cloneName)
        {
            base.ReCreate();
            Texture2D tex = new Texture2D(cloneName, _path);
            tex.ReCreate();
            return tex;
        }

        public void UpdateSamplerInfo(SamplerInfo info)
        {
            Info = info;
            if (InternalSampler != null && !InternalSampler.IsDisposed)
                InternalSampler.Dispose();
            InternalSampler = ResourceManager.GraphicsFactory.CreateSampler(Info.GetSamplerDescription());
            InternalSampler.Name = Name;
        }

        public override void Dispose()
        {
            Program.GameImGui.RemoveImGuiBinding(Tex);
            InternalSampler.Dispose();
            Tex.Dispose();
        }
    }
}