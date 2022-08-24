using System.Numerics;
using Veldrid;

namespace Engine.Assets.Rendering
{
    public static class RenderingGlobals
    {
        public static GraphicsDevice GameGraphics { get; private set; }
        public static GraphicsBackend APIBackend { get; private set; }
        public static ImGuiRenderer GameImGui { get; private set; }
        public static GraphicsBackend? NextFrameBackend { get; set; }
        public static RenderDoc RenderDocInstance { get; set; }
        public static Vector2 ViewSize { get; private set; }

        public static void InitGameGraphics(GraphicsBackend api)
        {
            APIBackend = api;
        #if WEBGL
            ViewSize = new Vector2(600, 400);
            var desc = new SwapchainDescription(SwapchainSource.CreateWeb(), (uint)ViewSize.X, (uint)ViewSize.Y, Veldrid.PixelFormat.R32_Float, true);
            GameGraphics = GraphicsDevice.CreateOpenGLES(new GraphicsDeviceOptions()
            {
                SingleThreaded = true,
                PreferDepthRangeZeroToOne = true,
                PreferStandardClipSpaceYDirection = true,
            }, desc);
        #endif
            GameImGui = new ImGuiRenderer(GameGraphics, GameGraphics.SwapchainFramebuffer.OutputDescription, (int)ViewSize.X, (int)ViewSize.Y);
        }

        public static void Resize(uint w, uint h)
        {
            ViewSize = new Vector2(w, h);
            GameGraphics.ResizeMainWindow((uint)w, (uint)h);
            GameImGui.WindowResized((int)w, (int)h);
        }

        public static void DisposeGameGraphics()
        {
            
        }
    }
}