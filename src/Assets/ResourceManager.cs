using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using Engine.Assets.Audio;
using Engine.Assets.Models;
using Engine.Assets.Rendering;
using Engine.Assets.Textures;
using ImGuiNET;
using Veldrid;

namespace Engine.Assets
{
    public static class ResourceManager
    {
        public static ResourceFactory GraphicsFactory => Program.GameGraphics.ResourceFactory;
        public static List<Resource> AllResources { get; private set; } = new List<Resource>();
        private static List<Resource> StagedResources = new List<Resource>();
        public static Dictionary<string, ImFontPtr> Fonts { get; private set; } = new Dictionary<string, ImFontPtr>();
        private readonly static object _poolLock = new object();

        public static void Update()
        {
            lock (_poolLock)
            {
                AllResources.AddRange(StagedResources);
                StagedResources.Clear();
            }
        }

        public static void ReCreateAll()
        {
            foreach (var item in AllResources)
            {
                item.HasBeenInitialized = false;
            }
            ImGuiIOPtr io = ImGui.GetIO();
            // io.Fonts.ClearFonts();
            foreach (var item in Fonts)
            {
                item.Value.Destroy();
            }
            Fonts.Clear();
            foreach (var item in AllResources)
            {
                item.ReCreate();
            }
        }

        public static bool HasResource(string name)
        {
            return AllResources.Any(x => x.Name == name);
        }

        public static Resource Clone(string name, Resource asset)
        {
            return Clone<Resource>(name, asset);
        }

        public static T Clone<T>(string name, T asset) where T : Resource
        {
            T cl = CheckAndReturn<T>(name);
            if (cl != null)
                return cl;
            cl = asset.Clone(name) as T;
            Add(cl);
            return cl;
        }

        public static T CheckAndReturn<T>(string name) where T : Resource
        {
            lock (_poolLock)
            {
                if (AllResources.Any(x => x.Name == name && x is T))
                {
                    return AllResources.First(x => x.Name == name && x is T) as T;
                }
                if (StagedResources.Any(x => x.Name == name && x is T))
                {
                    return StagedResources.First(x => x.Name == name && x is T) as T;
                }
                return null;
            }
        }

        public static ImFontPtr LoadImGuiFont(string path)
        {
            // return ImGui.GetFont();
            if (Fonts.Any(x => x.Key == path))
            {
                return Fonts.First(x => x.Key == path).Value;
            }
            ImGuiIOPtr io = ImGui.GetIO();
            ImFontPtr font = io.Fonts.AddFontFromFileTTF(path, 24f);
            Fonts.Add(path, font);
            io.Fonts.Build();
            Program.GameImGui.RecreateFontDeviceTexture();
            return font;
        }

        public static void Add(Resource resource)
        {
            lock (_poolLock)
            {
                StagedResources.Add(resource);
            }
        }

        public static Texture2D LoadTexture(string path)
        {
            return LoadTexture(path, path);
        }

        public static Texture2D LoadTexture(string name, string path)
        {
            Texture2D tex = CheckAndReturn<Texture2D>(name);
            if (tex != null)
                return tex;
            tex = new Texture2D(name, path);
            Add(tex);
            return tex;
        }

        public static AudioClip LoadAudioClip(string path)
        {
            AudioClip buf = CheckAndReturn<AudioClip>(path);
            if (buf != null)
                return buf;
            buf = new AudioClip(path);
            Add(buf);
            return buf;
        }

        public static GraphicsShader LoadShader(string path)
        {
            GraphicsShader buf = CheckAndReturn<GraphicsShader>(path);
            if (buf != null)
                return buf;
            buf = new GraphicsShader(path);
            Add(buf);
            return buf;
        }

        public static Model LoadModel(Material material, string path)
        {
            Model buf = CheckAndReturn<Model>(path);
            if (buf != null)
                return buf;
            buf = new Model(path, material);
            Add(buf);
            return buf;
        }

        public static UniformBuffer CreateUniformBuffer(string name, uint size)
        {
            UniformBuffer buf = CheckAndReturn<UniformBuffer>(name);
            if (buf != null && buf.Size == size)
                return buf;
            buf = new UniformBuffer(name, size);
            Add(buf);
            return buf;
        }

        public static CompoundBuffer CreateCompoundBuffer(string name, GraphicsShader shader, uint index, params IMaterialBindable[] bindables)
        {
            CompoundBuffer buf = CheckAndReturn<CompoundBuffer>(name);
            if (buf != null && buf.Contains(shader, index, bindables))
                return buf;
            buf = new CompoundBuffer(name, shader, index, bindables);
            Add(buf);
            return buf;
        }

        public static Material CreateMaterial(string name, GraphicsShader shader)
        {
            Material buf = CheckAndReturn<Material>(name);
            if (buf != null)
                return buf;
            buf = new Material(name, shader);
            Add(buf);
            return buf;
        }

        public static Renderer CreateRenderer(string name)
        {
            Renderer buf = CheckAndReturn<Renderer>(name);
            if (buf != null)
                return buf;
            buf = new Renderer(name);
            Add(buf);
            return buf;
        }

        public static Mesh CreateMesh(string name, bool isBig, Material material)
        {
            Mesh buf = CheckAndReturn<Mesh>(name);
            if (buf != null)
                return buf;
            buf = new Mesh(name, isBig, material);
            Add(buf);
            return buf;
        }

        public static void Unload(Resource resource)
        {
            lock (_poolLock)
            {
                resource.Dispose();
                AllResources.Remove(resource);
            }
        }

        public static void UnloadAll()
        {
            lock (_poolLock)
            {
                foreach (var item in AllResources)
                {
                    item.Dispose();
                }
                AllResources.Clear();
            }
        }
    }
}
