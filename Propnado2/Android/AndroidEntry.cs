using System.Threading.Tasks;
using System;
using System.Numerics;
using System.Text;
using System.Collections.Generic;
using Veldrid;
using Engine.Assets.Rendering;
using Engine.Assets.Audio;
using Engine.Assets;
using Engine.Assets.Textures;
using Engine.Game;
using System.Diagnostics;

public static class AndroidEntry
{
    public static GameApp game;
    public static double lastTime;
    public static Renderer renderer;
    public static bool isBusy = false;
    private static Vector2? size;
    private static Dictionary<string, IntPtr> proc = new Dictionary<string, IntPtr>();
    private static TimeSpan startTime;
    private static TimeSpan currentTime => new TimeSpan(DateTime.UtcNow.Ticks);

    public static void Init()
    {
        Console.WriteLine("Starting...");
        renderer = new Renderer("MainRenderer");
        Renderer.Current = renderer;
        RenderTexture2D mainRT = new RenderTexture2D("MainRenderTexture2D", RenderingGlobals.MainSwapchain);
        renderer.SetRenderTarget(mainRT);
        renderer.InternalRenderTexture.ReCreate();
        // AudioGlobals.InitGameAudio();
        MiscGlobals.InitGameMisc();
        game = new Propnado.Propnado2();
        game.Setup();
        ResourceManager.Update();
        startTime = currentTime;
    }

    public static void DeInit()
    {
        // ResourceManager.Update();
        // ResourceManager.UnloadAll();
        // AudioGlobals.DisposeGameAudio();
        game = null;
        Console.WriteLine("Exit.");
    }

    public static void MainLoopTick()
    {
        if (MiscGlobals.ReCreateAllNextFrame)
        {
            ResourceManager.UnloadAll();
            ResourceManager.ReCreateAll();
            ResourceManager.Update();
        }
        MiscGlobals.ReCreateAllNextFrame = false;
        RenderingGlobals.NextFrameBackend = null;
        FrameLoop();
    }

    public static void FrameLoop()
    {
        // if (!isBusy)
        {
            Frame((double)(currentTime - startTime).TotalSeconds);
        }
    }

    public static void Resize(int w, int h)
    {
        size = new Vector2(w, h);
    }

    public static int Frame(double time)
    {
        isBusy = true;
        Debug.Assert(renderer != null, "h1");
        Debug.Assert(RenderingGlobals.SwapchainFramebuffer != null, "h2");
        Debug.Assert(RenderingGlobals.GameImGui != null, "h4");
        if (size.HasValue)
        {
            RenderingGlobals.Resize((uint)size?.X, (uint)size?.Y);
            renderer.InternalRenderTexture?.Dispose();
            renderer.SetRenderTarget(new RenderTexture2D("MainRenderTexture2D", RenderingGlobals.MainSwapchain));
            renderer.InternalRenderTexture.ReCreate();
        }
        size = null;
        double delta = time - lastTime;
        MiscGlobals.FPS = 1d / Math.Max(delta, 0.001d);
        delta = Math.Min(delta, 0.5d);
        ResourceManager.Update();
        RenderingGlobals.ImGuiSetTarget(RenderingGlobals.SwapchainFramebuffer.OutputDescription, (int)RenderingGlobals.SwapchainFramebuffer.Width, (int)RenderingGlobals.SwapchainFramebuffer.Height);
        RenderingGlobals.GameImGui.Update((float)delta, MiscGlobals.GameInputHandler);
        renderer.ProjectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(70f * (MathF.PI / 180f), (float)renderer.InternalRenderTexture.Width / renderer.InternalRenderTexture.Height, 0.1f, 1000f);
        Renderer.Current = renderer;
        game.PreDraw(renderer, delta);
        renderer.Begin();
        renderer.Clear();
        Renderer.Current = renderer;
        game.Draw(renderer, delta);
        DebugGlobals.DrawDebugWindow();
        ImGuiNET.ImGui.EndFrame();
        RenderingGlobals.GameImGui.Render(RenderingGlobals.GameGraphics, renderer.CommandList);
        renderer.End();
        renderer.Submit();
        RenderingGlobals.SwapMainBuffer();
        MiscGlobals.GameInputHandler.Update();
        game.Tick(delta);
        lastTime = time;
        isBusy = false;
        return 0;
    }
}