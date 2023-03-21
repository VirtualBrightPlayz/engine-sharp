using System.Threading.Tasks;
using System;
using System.Numerics;
using System.Text;
using Veldrid;
using Engine.Assets.Rendering;
using Engine.Assets.Audio;
using Engine.Assets;
using Engine.Assets.Textures;
using System.Collections.Generic;
using System.Reflection;
using System.Linq.Expressions;
using System.Linq;

public static class DesktopEntry
{
    public static GameBSrc.BGame game;
    public static double lastTime;
    public static Renderer renderer;
    public static bool isBusy = false;
    private static Vector2? size;
    private static Dictionary<string, IntPtr> proc = new Dictionary<string, IntPtr>();
    private static TimeSpan startTime;
    private static TimeSpan currentTime => new TimeSpan(DateTime.UtcNow.Ticks);

    public static int Main(string[] args)
    {
        return Init();
    }

    public static int Init()
    {
        Console.WriteLine("Starting...");
        RenderingGlobals.InitGameGraphics(GraphicsBackend.Vulkan);
        RenderingGlobals.Window.Resized += OnWindowResize;
        renderer = new Renderer("MainRenderer");
        Renderer.Current = renderer;
        RenderTexture2D mainRT = new RenderTexture2D("MainRenderTexture2D", RenderingGlobals.GameGraphics.MainSwapchain);
        renderer.SetRenderTarget(mainRT);
        renderer.InternalRenderTexture.ReCreate();
        AudioGlobals.InitGameAudio();
        MiscGlobals.InitGameMisc();
        game = new GameBSrc.BGame();
        game.Setup();
        ResourceManager.Update();
        startTime = currentTime;
        while (!MiscGlobals.IsClosing)
        {
            MiscGlobals.Snapshot = RenderingGlobals.Window.PumpEvents();
            if (MiscGlobals.ReCreateAllNextFrame)
            {
                ResourceManager.UnloadAll();
                ResourceManager.ReCreateAll();
                ResourceManager.Update();
            }
            if (RenderingGlobals.NextFrameBackend.HasValue)
            {
                ResourceManager.UnloadAll();
                game.Unload();
                mainRT.Dispose();
                // AudioGlobals.DisposeGameAudio();
                RenderingGlobals.DisposeGameGraphics();
                RenderingGlobals.InitGameGraphics(RenderingGlobals.NextFrameBackend.Value);
                // AudioGlobals.InitGameAudio();
                Renderer.Current = renderer;
                mainRT = new RenderTexture2D("MainRenderTexture2D", RenderingGlobals.GameGraphics.MainSwapchain);
                renderer.SetRenderTarget(mainRT);
                game.ReCreate();
                ResourceManager.ReCreateAll();
                ResourceManager.Update();
            }
            MiscGlobals.ReCreateAllNextFrame = false;
            RenderingGlobals.NextFrameBackend = null;
            FrameLoop();
            if (!RenderingGlobals.Window.Exists)
                break;
        }
        AudioGlobals.DisposeGameAudio();
        RenderingGlobals.DisposeGameGraphics();
        Console.WriteLine("Exit.");
        return 0;
    }

    public static void FrameLoop()
    {
        try
        {
            if (!isBusy)
            {
                Frame((double)(currentTime - startTime).TotalSeconds);
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e.ToString());
        }
    }

    public static void OnWindowResize()
    {
        if (RenderingGlobals.GameGraphics == null)
            return;
        size = new Vector2(RenderingGlobals.Window.Width, RenderingGlobals.Window.Height);
    }

    public static int Frame(double time)
    {
        isBusy = true;
        if (size.HasValue)
        {
            RenderingGlobals.Resize((uint)size?.X, (uint)size?.Y);
            renderer.InternalRenderTexture.Dispose();
            renderer.SetRenderTarget(new RenderTexture2D("MainRenderTexture2D", RenderingGlobals.GameGraphics.MainSwapchain));
            renderer.InternalRenderTexture.ReCreate();
        }
        size = null;
        double delta = time - lastTime;
        MiscGlobals.FPS = 1d / Math.Max(delta, 0.001d);
        delta = Math.Min(delta, 0.5d);
        ResourceManager.Update();
        RenderingGlobals.ImGuiSetTarget(RenderingGlobals.GameGraphics.SwapchainFramebuffer.OutputDescription, (int)RenderingGlobals.GameGraphics.SwapchainFramebuffer.Width, (int)RenderingGlobals.GameGraphics.SwapchainFramebuffer.Height);
        RenderingGlobals.GameImGui.Update((float)delta, MiscGlobals.GameInputHandler);
        renderer.ProjectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(70f * (MathF.PI / 180f), (float)renderer.InternalRenderTexture.Width / renderer.InternalRenderTexture.Height, 0.1f, 1000f);
        Renderer.Current = renderer;
        game.PreDraw(renderer, delta);
        renderer.Begin();
        renderer.Clear();
        Renderer.Current = renderer;
        game.Draw(renderer, delta);
        // DebugGlobals.DrawDebugWindow();
        ImGuiNET.ImGui.EndFrame();
        RenderingGlobals.GameImGui.Render(RenderingGlobals.GameGraphics, renderer.CommandList);
        renderer.End();
        renderer.Submit();
        RenderingGlobals.GameGraphics.SwapBuffers();
        MiscGlobals.GameInputHandler.Update();
        game.Tick(delta);
        lastTime = time;
        isBusy = false;
        return 0;
    }
}