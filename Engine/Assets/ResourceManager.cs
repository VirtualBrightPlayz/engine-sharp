using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
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
        public static ResourceFactory GraphicsFactory => RenderingGlobals.GameGraphics.ResourceFactory;
        public static List<Resource> AllResources { get; private set; } = new List<Resource>();
        private static List<Resource> StagedResources = new List<Resource>();
        public static Dictionary<string, ImFontPtr> Fonts { get; private set; } = new Dictionary<string, ImFontPtr>();
        private readonly static object _poolLock = new object();
        private static bool isBusy = false;
        public static bool IsBusy => FileManager.IsBusy;

        public static void Update()
        {
            lock (_poolLock)
            {
                AllResources.AddRange(StagedResources);
                StagedResources.Clear();
            }
        }

        public static async Task ReCreateAll()
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
                await item.ReCreate();
            }
        }

        public static bool HasResource(string name)
        {
            return AllResources.Any(x => x.Name == name);
        }

        public static Task<Resource> Clone(string name, Resource asset)
        {
            return Clone<Resource>(name, asset);
        }

        public static async Task<T> Clone<T>(string name, T asset) where T : Resource
        {
            T cl = await CheckAndReturn<T>(name);
            if (cl != null)
                return cl;
            cl = await asset.Clone(name) as T;
            await Add(cl);
            return cl;
        }

        public static async Task<T> CheckAndReturn<T>(string name) where T : Resource
        {
            while (isBusy)
            {
                await Task.Yield();
            }
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

        public static ImFontPtr LoadImGuiFont(string path, float size = 24f)
        {
            if (Fonts.Any(x => x.Key == path))
            {
                return Fonts.First(x => x.Key == path).Value;
            }
            ImGuiIOPtr io = ImGui.GetIO();
            ImFontPtr font = io.Fonts.AddFontFromFileTTF(path, size);
            Fonts.Add(path, font);
            io.Fonts.Build();
            RenderingGlobals.GameImGui.RecreateFontDeviceTexture();
            return font;
        }

        public static async Task Add(Resource resource)
        {
            await resource.ReCreate();
            lock (_poolLock)
            {
                StagedResources.Add(resource);
            }
        }

        public static Task<Texture2D> LoadTexture(string path)
        {
            return LoadTexture(path, path);
        }

        public static async Task<Texture2D> LoadTexture(string name, string path)
        {
            Texture2D tex = await CheckAndReturn<Texture2D>(name);
            if (tex != null)
                return tex;
            tex = new Texture2D(name, path);
            await Add(tex);
            return tex;
        }

        public static async Task<AudioClip> LoadAudioClip(string path)
        {
            AudioClip buf = await CheckAndReturn<AudioClip>(path);
            if (buf != null)
                return buf;
            buf = new AudioClip(path);
            await Add(buf);
            return buf;
        }

        public static async Task<GraphicsShader> LoadShader(string path)
        {
            GraphicsShader buf = await CheckAndReturn<GraphicsShader>(path);
            if (buf != null)
                return buf;
            buf = new GraphicsShader(path);
            await Add(buf);
            return buf;
        }

        public static async Task<Model> LoadModel(Material material, string path)
        {
            Model buf = await CheckAndReturn<Model>(path);
            if (buf != null)
                return buf;
            buf = new Model(path, path, material, true, false);
            await Add(buf);
            return buf;
        }

        public static async Task<Model> LoadModel(string name, Material material, string path)
        {
            Model buf = await CheckAndReturn<Model>(path);
            if (buf != null)
                return buf;
            buf = new Model(path, path, material, true, false);
            await Add(buf);
            return buf;
        }

        public static async Task<Model> LoadModel(string name, Material material, string path, bool shouldLoadMats, bool animated)
        {
            Model buf = await CheckAndReturn<Model>(path);
            if (buf != null)
                return buf;
            buf = new Model(path, path, material, shouldLoadMats, animated);
            await Add(buf);
            return buf;
        }

        public static async Task<UniformBuffer> CreateUniformBuffer(string name, uint size)
        {
            UniformBuffer buf = await CheckAndReturn<UniformBuffer>(name);
            if (buf != null && buf.Size == size)
                return buf;
            buf = new UniformBuffer(name, size);
            await Add(buf);
            return buf;
        }

        public static async Task<CompoundBuffer> CreateCompoundBuffer(string name, GraphicsShader shader, uint index, params IMaterialBindable[] bindables)
        {
            CompoundBuffer buf = await CheckAndReturn<CompoundBuffer>(name);
            if (buf != null && buf.Contains(shader, index, bindables))
                return buf;
            buf = new CompoundBuffer(name, shader, index, bindables);
            await Add(buf);
            return buf;
        }

        public static async Task<Material> CreateMaterial(string name, GraphicsShader shader)
        {
            Material buf = await CheckAndReturn<Material>(name);
            if (buf != null)
                return buf;
            buf = new Material(name, shader);
            await Add(buf);
            return buf;
        }

        public static async Task<Renderer> CreateRenderer(string name)
        {
            Renderer buf = await CheckAndReturn<Renderer>(name);
            if (buf != null)
                return buf;
            buf = new Renderer(name);
            await Add(buf);
            return buf;
        }

        public static async Task<Mesh> CreateMesh(string name, bool isBig, Material material)
        {
            Mesh buf = await CheckAndReturn<Mesh>(name);
            if (buf != null)
                return buf;
            buf = new Mesh(name, isBig, material);
            await Add(buf);
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
