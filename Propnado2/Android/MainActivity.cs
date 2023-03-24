using System;
using System.Collections.Generic;
using System.Numerics;
using System.Threading.Tasks;
using Android;
using Android.App;
using Android.OS;
using Android.Views;
using Engine.Assets;
using Engine.Assets.Rendering;
using SampleBase.Android;
using Veldrid;

[Activity(Label = "@string/app_name", MainLauncher = true, Theme = "@android:style/Theme.DeviceDefault.NoActionBar.Fullscreen", ConfigurationChanges = Android.Content.PM.ConfigChanges.KeyboardHidden | Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
public class MainActivity : Activity, InputSnapshot
{
    public VeldridSurfaceView surface;
    public List<Veldrid.KeyEvent> keys = new List<Veldrid.KeyEvent>();
    public List<MouseEvent> mouse = new List<MouseEvent>();
    public List<char> chars = new List<char>();
    public Vector2 pos;
    private GraphicsBackend backend;
    private bool loopRunning = false;

    public IReadOnlyList<Veldrid.KeyEvent> KeyEvents => keys;
    public IReadOnlyList<MouseEvent> MouseEvents => mouse;
    public IReadOnlyList<char> KeyCharPresses => chars;
    public Vector2 MousePosition => pos;
    public float WheelDelta => 0f;

    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);
        backend = GraphicsDevice.IsBackendSupported(GraphicsBackend.Vulkan) ? GraphicsBackend.Vulkan : GraphicsBackend.OpenGLES;
        surface = new VeldridSurfaceView(this, backend, new GraphicsDeviceOptions()
        {
            PreferDepthRangeZeroToOne = true,
            PreferStandardClipSpaceYDirection = true,
            SwapchainDepthFormat = PixelFormat.R16_UNorm,
            SyncToVerticalBlank = true,
        });
        bool hasRun = false;
        surface.Rendering += () =>
        {
            if (surface != null && surface.GraphicsDevice != null && surface.MainSwapchain != null && (RenderingGlobals.GameGraphics == null || RenderingGlobals.MainSwapchain == null))
            {
                RenderingGlobals.InitGameGraphicsFrom(surface.GraphicsDevice.BackendType, surface.GraphicsDevice, surface.MainSwapchain, surface.Width, surface.Height);
                AndroidEntry.Resize(surface.Width, surface.Height);
                MiscGlobals.Snapshot = this;
            }
            mouse.Clear();
            mouse.Add(new MouseEvent(MouseButton.Left, false));
            AndroidEntry.MainLoopTick();
        };
        surface.Resized += () => AndroidEntry.Resize(surface.Width, surface.Height);
        surface.DeviceCreated += () =>
        {
            Console.WriteLine("Device Created");
        };
        surface.DeviceDisposed += () =>
        {
            Console.WriteLine("Device Disposed");
            RenderingGlobals.NullGameGraphics();
        };
        surface.OnSurfaceCreated += () =>
        {
            Console.WriteLine("Surface Created");
            RenderingGlobals.InitGameGraphicsFrom(surface.GraphicsDevice.BackendType, surface.GraphicsDevice, surface.MainSwapchain, surface.Width, surface.Height);
            AndroidEntry.Resize(surface.Width, surface.Height);
            MiscGlobals.Snapshot = this;
            if (!hasRun)
            {
                hasRun = true;
                AndroidEntry.Init();
            }
            if (!loopRunning)
            {
                surface.RunContinuousRenderLoop();
                loopRunning = true;
            }
        };
        surface.OnSurfaceDestroyed += () =>
        {
            Console.WriteLine("Surface Destroyed");
        };
        SetContentView(surface);
    }

    protected override void OnPause()
    {
        base.OnPause();
        Console.WriteLine("Pause");
        surface.OnPause();
        RenderingGlobals.Pause();
    }

    protected override void OnResume()
    {
        base.OnResume();
        Console.WriteLine("Resume");
        surface.OnResume();
    }

    public override bool DispatchTouchEvent(MotionEvent ev)
    {
        pos.X = ev.GetAxisValue(Axis.X);
        pos.Y = ev.GetAxisValue(Axis.Y);
        // mouse.Add(new MouseEvent(MouseButton.Left, ev.IsButtonPressed(MotionEventButtonState.Primary)));
        mouse.Add(new MouseEvent(MouseButton.Left, true));
        // mouse.Add(new MouseEvent(MouseButton.Right, ev.IsButtonPressed(MotionEventButtonState.Secondary)));
        return true;
    }

    public bool IsMouseDown(MouseButton button)
    {
        return /*mouse.FindLastIndex(x => x.MouseButton == button) !=*/ mouse.FindLastIndex(x => x.Down && x.MouseButton == button) != -1;
    }
}