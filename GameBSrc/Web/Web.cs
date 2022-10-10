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
using System.Collections.Generic;
using System.Reflection;
using System.Linq.Expressions;
using System.Linq;

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
    private static Dictionary<string, IntPtr> proc = new Dictionary<string, IntPtr>();

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

    public static IntPtr getProcAddress(string name)
    {
        if (proc.ContainsKey(name))
            return proc[name];
        if (proc.ContainsKey(name + "EXT"))
            return proc[name + "EXT"];
        throw new Exception(name);
    }

    public static Delegate CreateDelegate(object instance, MethodInfo method)
    {
        var param = method.GetParameters().Select(x => x.ParameterType).ToList();
        param.Add(method.ReturnType);
        var type = Expression.GetDelegateType(param.ToArray());

        return Delegate.CreateDelegate(type, instance, method);
    }

    [JSInvokable]
    public static async Task<int> Init()
    {
        Console.WriteLine("Starting...");
        runtime = new WebRuntime();
        FileManager.HttpPrefix = runtime.Invoke<string>("getHttpPrefix");
        WebSwapchainSource swapchainSource = new WebSwapchainSource();
        unsafe
        {
            Emscripten.EmscriptenWebGLContextAttributes attributes = new Emscripten.EmscriptenWebGLContextAttributes();
            Emscripten.emscripten_webgl_init_context_attributes(&attributes);
            attributes.majorVersion = 2;
            attributes.minorVersion = 0;
            attributes.powerPreference = Emscripten.EM_WEBGL_POWER_PREFERENCE_DEFAULT;
            int context = Emscripten.emscripten_webgl_create_context("#canvas", &attributes);
            if (context == 0)
            {
                throw new Exception($"{nameof(Emscripten.emscripten_webgl_create_context)} returned 0");
            }
            int res = Emscripten.emscripten_webgl_get_context_attributes(context, &attributes);
            if (res != Emscripten.EMSCRIPTEN_RESULT_SUCCESS)
            {
                throw new Exception($"{nameof(Emscripten.emscripten_webgl_get_context_attributes)} returned {res}");
            }
            if (attributes.majorVersion < 2)
            {
                throw new Exception($"{nameof(attributes.majorVersion)} is {attributes.majorVersion}, WebGL2 is required");
            }
            res = Emscripten.emscripten_webgl_make_context_current(context);
            if (res != Emscripten.EMSCRIPTEN_RESULT_SUCCESS)
            {
                throw new Exception($"{nameof(Emscripten.emscripten_webgl_make_context_current)} returned {res}");
            }
            res = Emscripten.emscripten_webgl_get_current_context();
            if (res != context)
            {
                throw new Exception($"{nameof(Emscripten.emscripten_webgl_get_current_context)} returned {res}, which is a different context from {context}");
            }

            /*var pinvokes = typeof(GL).GetMethods(BindingFlags.Public | BindingFlags.Static);
            foreach (var item in pinvokes)
            {
                var fn = CreateDelegate(null, item);
                IntPtr fnPtr = Marshal.GetFunctionPointerForDelegate(fn);
                proc.Add(item.Name, fnPtr);
            }*/

            swapchainSource.platformInfo = new Veldrid.OpenGL.OpenGLPlatformInfo(
                (IntPtr)context,
                // Emscripten.emscripten_GetProcAddress,
                getProcAddress,
                (i) => Emscripten.emscripten_webgl_make_context_current((int)i),
                () => (IntPtr)Emscripten.emscripten_webgl_get_current_context(),
                () => { },
                (i) => Emscripten.emscripten_webgl_destroy_context((int)i),
                () => { },
                (b) => { }
            );
        }

        RenderingGlobals.InitGameGraphics(GraphicsBackend.OpenGLES, swapchainSource);
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
        try
        {
            if (!isBusy)
            {
                await Frame((double)emscripten_get_now() / 1000d);
            }
        }
        catch (Exception e)
        {
            runtime.InvokeVoid("engineError", e.ToString());
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