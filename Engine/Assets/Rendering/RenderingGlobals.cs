using System.Numerics;
using Engine.Assets.Textures;
using Veldrid;
using Veldrid.StartupUtilities;

namespace Engine.Assets.Rendering
{
    public static class RenderingGlobals
    {
        public static GraphicsDevice GameGraphics { get; private set; }
        public static GraphicsBackend APIBackend { get; private set; }
        public static ImGuiRenderer GameImGui { get; private set; }
        private static OutputDescription ImGuiOutput { get; set; }
        public static GraphicsBackend? NextFrameBackend { get; set; }
        public static RenderDoc RenderDocInstance { get; set; }
        public static Vector2 ViewSize { get; private set; }
#if !WEBGL
        public static Veldrid.Sdl2.Sdl2Window Window { get; private set; }
#endif

        public static void InitGameGraphicsFrom(GraphicsBackend api, SwapchainSource swapchainSource, int w, int h)
        {
            APIBackend = api;
            ViewSize = new Vector2(w, h);
            var desc = new SwapchainDescription(swapchainSource, (uint)ViewSize.X, (uint)ViewSize.Y, Veldrid.PixelFormat.D32_Float_S8_UInt, true);
            switch (api)
            {
                case GraphicsBackend.Vulkan:
                    GameGraphics = GraphicsDevice.CreateVulkan(new GraphicsDeviceOptions()
                    {
                        // SingleThreaded = false,
                        PreferDepthRangeZeroToOne = true,
                        PreferStandardClipSpaceYDirection = true,
                    }, desc);
                    break;
            }
            Window = null;
            GameImGui = new ImGuiRenderer(GameGraphics, GameGraphics.SwapchainFramebuffer.OutputDescription, (int)ViewSize.X, (int)ViewSize.Y);
        }

        public static void InitGameGraphics(GraphicsBackend api)
        {
            APIBackend = api;
            Veldrid.Sdl2.Sdl2Window win = null;
            GraphicsDevice gfx = null;
            ViewSize = new Vector2(600, 400);
            VeldridStartup.CreateWindowAndGraphicsDevice(new WindowCreateInfo()
            {
                X = 100,
                Y = 100,
                WindowWidth = (int)ViewSize.X,
                WindowHeight = (int)ViewSize.Y,
            }, new GraphicsDeviceOptions()
            {
                // SingleThreaded = false,
                PreferDepthRangeZeroToOne = true,
                PreferStandardClipSpaceYDirection = true,
                SyncToVerticalBlank = true,
                SwapchainDepthFormat = PixelFormat.D32_Float_S8_UInt,
            }, api, out win, out gfx);
            Window = win;
            GameGraphics = gfx;
            APIBackend = gfx.BackendType;
            GameImGui = new ImGuiRenderer(GameGraphics, GameGraphics.SwapchainFramebuffer.OutputDescription, (int)ViewSize.X, (int)ViewSize.Y);
        }

        public static void ImGuiSetTarget(OutputDescription desc, int w, int h)
        {
            if (desc.Equals(ImGuiOutput))
                return;
            GameImGui.CreateDeviceResources(GameGraphics, desc);
            GameImGui.WindowResized(w, h);
            ImGuiOutput = desc;
        }

        public static void ImGuiSetTarget(RenderTexture2D rt)
        {
            if (rt.InternalFramebuffer.OutputDescription.Equals(ImGuiOutput))
                return;
            GameImGui.CreateDeviceResources(GameGraphics, rt.InternalFramebuffer.OutputDescription);
            GameImGui.WindowResized((int)rt.Width, (int)rt.Height);
            ImGuiOutput = rt.InternalFramebuffer.OutputDescription;
        }

        public static void Resize(uint w, uint h)
        {
            ViewSize = new Vector2(w, h);
            GameGraphics.ResizeMainWindow((uint)w, (uint)h);
            GameImGui.WindowResized((int)w, (int)h);
        }

        public static void DisposeGameGraphicsSkipWindow()
        {
            GameImGui.Dispose();
            GameImGui = null;
            GameGraphics.Dispose();
            GameGraphics = null;
        }

        public static void DisposeGameGraphics()
        {
            GameImGui.Dispose();
            GameImGui = null;
#if !WEBGL
            Window.Close();
            Window = null;
#endif
            GameGraphics.Dispose();
            GameGraphics = null;
        }
    }
}