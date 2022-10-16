using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
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
        private static List<Resource> UnStagedResources = new List<Resource>();
        public static Dictionary<string, ImFontPtr> Fonts { get; private set; } = new Dictionary<string, ImFontPtr>();
        // private readonly static object _poolLock = new object();
        private static bool isBusy = false;
        public static bool IsBusy => FileManager.IsBusy;

        public static void Update()
        {
            // lock (_poolLock)
            {
                AllResources.AddRange(StagedResources);
                AllResources.RemoveAll(x => UnStagedResources.Contains(x));
                StagedResources.Clear();
                UnStagedResources.Clear();
            }
        }

        public static async Task ReCreateAll()
        {
            var res = AllResources;//.ToArray();
            foreach (var item in res)
            {
                item.HasBeenInitialized = false;
            }
            ImGuiIOPtr io = ImGui.GetIO();
            foreach (var item in Fonts)
            {
                item.Value.Destroy();
            }
            Fonts.Clear();
            foreach (var item in res)
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
            /*while (isBusy)
            {
                await Task.Yield();
            }*/
            // lock (_poolLock)
            {
                if (AllResources.Any(x => x.Name == name && x is T))
                {
                    var y = AllResources.First(x => x.Name == name && x is T) as T;
                    if (!y.HasBeenInitialized)
                        await y.ReCreate();
                    return y;
                }
                if (StagedResources.Any(x => x.Name == name && x is T))
                {
                    var y = StagedResources.First(x => x.Name == name && x is T) as T;
                    if (!y.HasBeenInitialized)
                        await y.ReCreate();
                    return y;
                }
                return null;
            }
        }

        public static async Task<ImFontPtr> LoadImGuiFont(ImGuiRenderer renderer, string path, float size = 24f)
        {
            /*if (Fonts.Any(x => x.Key == path))
            {
                return Fonts.First(x => x.Key == path).Value;
            }*/
            ImGuiIOPtr io = ImGui.GetIO();
            using Stream str = await FileManager.LoadStream(path);
            byte[] data = null;//new byte[str.Length];
            using StreamReader reader = new StreamReader(str, Encoding.UTF8);
            data = Encoding.UTF8.GetBytes(reader.ReadToEnd());
            // Console.WriteLine(str.Read(data, 0, data.Length));
            ImFontPtr font = null;
            unsafe
            {
                IntPtr dataptr = IntPtr.Zero;
                try
                {
                    dataptr = Marshal.AllocHGlobal(data.Length);
                    Marshal.Copy(data, 0, dataptr, data.Length);
                    ImFontConfig cfg = new ImFontConfig();

                    cfg.FontDataOwnedByAtlas = 0;

                    cfg.GlyphMaxAdvanceX = float.MaxValue;
                    cfg.OversampleH = 3;
                    cfg.OversampleV = 1;
                    cfg.RasterizerMultiply = 1f;
                    cfg.EllipsisChar = ushort.MaxValue;

                    cfg.FontData = dataptr.ToPointer();
                    cfg.FontDataSize = data.Length;
                    cfg.SizePixels = size;

                    byte[] name = Encoding.UTF8.GetBytes(Path.GetFileName(path));
                    Marshal.Copy(name, 0, new IntPtr(cfg.Name), Math.Min(name.Length, 39));
                    cfg.Name[Math.Min(name.Length, 39)] = (byte)'\0';

                    ImFontConfigPtr ptr = new ImFontConfigPtr(&cfg);
                    font = io.Fonts.AddFont(ptr);
                    // font = io.Fonts.AddFontFromMemoryTTF(dataptr, data.Length, size, ptr);
                    ptr.Destroy();
                    Fonts.Add(path, font);
                    io.Fonts.Build();
                    renderer.RecreateFontDeviceTexture();
                    // RenderingGlobals.GameImGui.RecreateFontDeviceTexture();
                }
                finally
                {
                    Marshal.FreeHGlobal(dataptr);
                }
            }
            return font;
        }

        public static async Task Add(Resource resource)
        {
            await resource.ReCreate();
            // lock (_poolLock)
            {
                StagedResources.Add(resource);
                #if WEBGL
                // Update();
                #endif
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

        public static async Task<Texture2D> LoadTexture(string name, byte[] data)
        {
            Texture2D tex = await CheckAndReturn<Texture2D>(name);
            if (tex != null)
                return tex;
            tex = new Texture2D(name, data);
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
            // lock (_poolLock)
            {
                resource.Dispose();
                UnStagedResources.Add(resource);
                // AllResources.Remove(resource);
            }
        }

        public static void UnloadAll()
        {
            // lock (_poolLock)
            {
                foreach (var item in AllResources)
                {
                    item.Dispose();
                }
            }
        }

        public static void Clear()
        {
            // lock (_poolLock)
            {
                AllResources.Clear();
            }
        }
    }
}
