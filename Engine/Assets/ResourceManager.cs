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
        public static Dictionary<ImGuiRenderer, Dictionary<string, ImFontPtr>> Fonts { get; private set; } = new Dictionary<ImGuiRenderer, Dictionary<string, ImFontPtr>>();

        public static void Update()
        {
            AllResources.AddRange(StagedResources);
            AllResources.RemoveAll(x => UnStagedResources.Contains(x));
            StagedResources.Clear();
            UnStagedResources.Clear();
        }

        public static void ReCreateAll()
        {
            var res = AllResources;//.ToArray();
            foreach (var item in res)
            {
                item.HasBeenInitialized = false;
            }
            ImGuiIOPtr io = ImGui.GetIO();
            foreach (var item in Fonts)
            {
                foreach (var item2 in item.Value)
                {
                    item2.Value.Destroy();
                }
            }
            Fonts.Clear();
            foreach (var item in res)
            {
                item.ReCreate();
            }
        }

        public static T FindByName<T>(string name) where T : Resource
        {
            if (AllResources.Any(x => x.Name == name && x is T))
            {
                var y = AllResources.First(x => x.Name == name && x is T) as T;
                // if (!y.HasBeenInitialized)
                    // y.ReCreate();
                return y;
            }
            if (StagedResources.Any(x => x.Name == name && x is T))
            {
                var y = StagedResources.First(x => x.Name == name && x is T) as T;
                // if (!y.HasBeenInitialized)
                    // y.ReCreate();
                return y;
            }
            return null;
        }

        public static ImFontPtr LoadImGuiFont(ImGuiRenderer renderer, string path, float size = 24f)
        {
            string path2 = path + "_size" + size;
            if (RenderingGlobals.GameImGui != renderer)
                return null;
            if (Fonts.Any(x => x.Value.Any(y => y.Key == path2)))
            {
                renderer.RecreateFontDeviceTexture();
                return Fonts.First(x => x.Value.Any(y => y.Key == path2)).Value[path2];
            }
            ImGuiIOPtr io = ImGui.GetIO();
            using Stream str = FileManager.LoadStream(path);
            byte[] data = null;//new byte[str.Length];
            using StreamReader reader = new StreamReader(str, Encoding.ASCII);
            data = Encoding.ASCII.GetBytes(reader.ReadToEnd());
            ImFontPtr font = null;

            font = io.Fonts.AddFontFromFileTTF(path, size);
            if (!Fonts.ContainsKey(renderer))
                Fonts.Add(renderer, new Dictionary<string, ImFontPtr>());
            Fonts[renderer].Add(path2, font);
            io.Fonts.Build();
            renderer.RecreateFontDeviceTexture();
            return font;
            /*
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

                    byte[] name = Encoding.ASCII.GetBytes(Path.GetFileName(path));
                    Marshal.Copy(name, 0, new IntPtr(cfg.Name), Math.Min(name.Length, 39));
                    cfg.Name[Math.Min(name.Length, 39)] = (byte)'\0';

                    ImFontConfigPtr ptr = new ImFontConfigPtr(&cfg);
                    // font = io.Fonts.AddFont(ptr);
                    font = io.Fonts.AddFontFromMemoryTTF(dataptr, data.Length, size, ptr);
                    // ptr.Destroy();
                    if (!Fonts.ContainsKey(renderer))
                        Fonts.Add(renderer, new Dictionary<string, ImFontPtr>());
                    Fonts[renderer].Add(path, font);
                    bool built = io.Fonts.Build();
                    renderer.RecreateFontDeviceTexture();
                    // RenderingGlobals.GameImGui.RecreateFontDeviceTexture();
                }
                finally
                {
                    Marshal.FreeHGlobal(dataptr);
                }
            }
            return font;
            */
        }

        internal static void AddInternal(Resource resource)
        {
            StagedResources.Add(resource);
        }

        internal static void UnloadInternal(Resource resource)
        {
            UnStagedResources.Add(resource);
        }

        public static void UnloadAll()
        {
            foreach (var item in AllResources)
            {
                item.Dispose();
            }
        }

        internal static void Clear()
        {
            AllResources.Clear();
        }
    }
}
