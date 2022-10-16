using System.Numerics;
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

        #if WEBGL
        public static void InitGameGraphics(GraphicsBackend api, SwapchainSource swapchainSource)
        #else
        public static void InitGameGraphics(GraphicsBackend api)
        #endif
        {
            APIBackend = api;
        #if WEBGL
            ViewSize = new Vector2(600, 400);
            var desc = new SwapchainDescription(swapchainSource, (uint)ViewSize.X, (uint)ViewSize.Y, Veldrid.PixelFormat.R32_Float, true);
            GameGraphics = GraphicsDevice.CreateOpenGLES(new GraphicsDeviceOptions()
            {
                SingleThreaded = true,
                PreferDepthRangeZeroToOne = true,
                PreferStandardClipSpaceYDirection = true,
            }, desc);
        #else
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
                SingleThreaded = false,
                PreferDepthRangeZeroToOne = true,
                PreferStandardClipSpaceYDirection = true,
                SyncToVerticalBlank = true,
                SwapchainDepthFormat = PixelFormat.R32_Float,
            }, api, out win, out gfx);
            Window = win;
            GameGraphics = gfx;
            APIBackend = gfx.BackendType;
        #endif
            GameImGui = new ImGuiRenderer(GameGraphics, GameGraphics.SwapchainFramebuffer.OutputDescription, (int)ViewSize.X, (int)ViewSize.Y);
        }

        public static void ImGuiSetTarget(OutputDescription desc)
        {
            return;
            if (desc.Equals(ImGuiOutput))
                return;
            GameImGui.CreateDeviceResources(GameGraphics, desc);
            ImGuiOutput = desc;
        }

        public static void Resize(uint w, uint h)
        {
            ViewSize = new Vector2(w, h);
            GameGraphics.ResizeMainWindow((uint)w, (uint)h);
            GameImGui.WindowResized((int)w, (int)h);
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