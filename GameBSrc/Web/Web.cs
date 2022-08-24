using System.Threading.Tasks;
using System;
using System.Numerics;
using System.Text;
using Microsoft.JSInterop;
using Microsoft.JSInterop.WebAssembly;
using Veldrid;
using Engine.Assets.Rendering;
using Engine.Assets.Audio;
using Engine.Assets;
using Engine.Assets.Textures;
using System.Runtime.InteropServices;

public class WebRuntime : WebAssemblyJSRuntime
{
}

public static class WebEntry
{
    public static GameBSrc.BGame game;
    public static double lastTime;
    public static Renderer renderer;
    public static WebRuntime runtime;
    public static bool isBusy = false;
    private static Vector2? size;

    [DllImport("emscripten", CallingConvention = CallingConvention.Cdecl)]
    public static extern bool emscripten_webgl_enable_extension(IntPtr context, IntPtr extension);
    [DllImport("emscripten", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr emscripten_webgl_get_current_context();
    [DllImport("emscripten", CallingConvention = CallingConvention.Cdecl)]
    public static unsafe extern void emscripten_set_main_loop(IntPtr callback, int fps, int simulate_infinite_loop);
    [DllImport("emscripten", CallingConvention = CallingConvention.Cdecl)]
    public static extern IntPtr emscripten_get_now();
    [DllImport("emscripten", CallingConvention = CallingConvention.Cdecl)]
    public static extern void emscripten_pause_main_loop();
    [DllImport("emscripten", CallingConvention = CallingConvention.Cdecl)]
    public static extern void emscripten_resume_main_loop();

    public static async Task<int> Main(string[] args)
    {
        await Task.Yield();
        return 0;
    }

    [JSInvokable]
    public static async Task<int> Init()
    {
        Console.WriteLine("Starting...");
        runtime = new WebRuntime();
        FileManager.HttpPrefix = runtime.Invoke<string>("getHttpPrefix");
        RenderingGlobals.InitGameGraphics(GraphicsBackend.OpenGLES);
        renderer = await ResourceManager.CreateRenderer("MainRenderer");
        Renderer.Current = renderer;
        renderer.SetRenderTarget(new RenderTexture2D("MainRenderTexture2D", RenderingGlobals.GameGraphics.MainSwapchain));
        await renderer.InternalRenderTexture.ReCreate();
        AudioGlobals.InitGameAudio();
        MiscGlobals.InitGameMisc();
        MiscGlobals.WebRuntime = runtime;
        game = new GameBSrc.BGame();
        await game.Setup();
        ResourceManager.Update();
        unsafe
        {
            delegate* unmanaged<void> em_callback_func = &FrameLoop;
            emscripten_set_main_loop((IntPtr)em_callback_func, 0, 0);
        }
        return 0;
    }

    private static string HttpLoad(string arg)
    {
        return runtime.Invoke<string>("getHttpContent", arg);
    }

    [UnmanagedCallersOnly(EntryPoint = "FrameLoop")]
    public static async void FrameLoop()
    {
        if (!isBusy)
        {
            await Frame((double)emscripten_get_now() / 1000d);
        }
    }

    [JSInvokable]
    public static void OnWindowResize(float w, float h)
    {
        if (RenderingGlobals.GameGraphics == null)
            return;
        size = new Vector2(w, h);
    }

    [JSInvokable]
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
        delta = Math.Min(delta, 0.5d);
        ResourceManager.Update();
        RenderingGlobals.GameImGui.Update((float)delta, MiscGlobals.GameInputSnapshot);
        renderer.ProjectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(70f * (MathF.PI / 180f), (float)renderer.InternalRenderTexture.Width / renderer.InternalRenderTexture.Height, 0.1f, 1000f);
        Renderer.Current = renderer;
        await game.PreDraw(renderer, delta);
        renderer.Begin();
        renderer.Clear();
        await game.Draw(renderer, delta);
        // DebugGlobals.DrawDebugWindow();
        ImGuiNET.ImGui.EndFrame();
        RenderingGlobals.GameImGui.Render(RenderingGlobals.GameGraphics, renderer.CommandList);
        renderer.End();
        renderer.Submit();
        RenderingGlobals.GameGraphics.SwapBuffers();
        MiscGlobals.GameInputSnapshot.Update();
        await game.Tick(delta);
        lastTime = time;
        isBusy = false;
        return 0;
    }
}