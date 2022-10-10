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

    public static void Main(string[] args)
    {
        Init().GetAwaiter().GetResult();
    }

    public static async Task<int> Init()
    {
        Console.WriteLine("Starting...");
        RenderingGlobals.InitGameGraphics(GraphicsBackend.Vulkan);
        RenderingGlobals.Window.Resized += OnWindowResize;
        renderer = await ResourceManager.CreateRenderer("MainRenderer");
        Renderer.Current = renderer;
        renderer.SetRenderTarget(new RenderTexture2D("MainRenderTexture2D", RenderingGlobals.GameGraphics.MainSwapchain));
        await renderer.InternalRenderTexture.ReCreate();
        AudioGlobals.InitGameAudio();
        MiscGlobals.InitGameMisc();
        game = new GameBSrc.BGame();
        await game.Setup();
        ResourceManager.Update();
        startTime = currentTime;
        while (!MiscGlobals.IsClosing)
        {
            MiscGlobals.Snapshot = RenderingGlobals.Window.PumpEvents();
            FrameLoop();
            if (!RenderingGlobals.Window.Exists)
                break;
        }
        AudioGlobals.DisposeGameAudio();
        RenderingGlobals.DisposeGameGraphics();
        Console.WriteLine("Exit.");
        return 0;
    }

    public static async void FrameLoop()
    {
        try
        {
            if (!isBusy)
            {
                await Frame((double)(currentTime - startTime).TotalSeconds);
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

    public static async Task<int> Frame(double time)
    {
        isBusy = true;
        if (size.HasValue)
        {
            Console.WriteLine(size?.X);
            Console.WriteLine(size?.Y);
            RenderingGlobals.Resize((uint)size?.X, (uint)size?.Y);
            renderer.InternalRenderTexture.Dispose();
            renderer.SetRenderTarget(new RenderTexture2D("MainRenderTexture2D", RenderingGlobals.GameGraphics.MainSwapchain));
            await renderer.InternalRenderTexture.ReCreate();
        }
        size = null;
        double delta = time - lastTime;
        MiscGlobals.FPS = 1d / Math.Max(delta, 0.001d);
        delta = Math.Min(delta, 0.5d);
        ResourceManager.Update();
        RenderingGlobals.ImGuiSetTarget(RenderingGlobals.GameGraphics.SwapchainFramebuffer.OutputDescription);
        RenderingGlobals.GameImGui.Update((float)delta, MiscGlobals.GameInputHandler);
        renderer.ProjectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(70f * (MathF.PI / 180f), (float)renderer.InternalRenderTexture.Width / renderer.InternalRenderTexture.Height, 0.1f, 1000f);
        Renderer.Current = renderer;
        await game.PreDraw(renderer, delta);
        renderer.Begin();
        renderer.Clear();
        Renderer.Current = renderer;
        await game.Draw(renderer, delta);
        // DebugGlobals.DrawDebugWindow();
        ImGuiNET.ImGui.EndFrame();
        RenderingGlobals.GameImGui.Render(RenderingGlobals.GameGraphics, renderer.CommandList);
        renderer.End();
        renderer.Submit();
        RenderingGlobals.GameGraphics.SwapBuffers();
        MiscGlobals.GameInputHandler.Update();
        await game.Tick(delta);
        lastTime = time;
        isBusy = false;
        return 0;
    }
}