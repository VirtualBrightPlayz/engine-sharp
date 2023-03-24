using System.Threading.Tasks;
using Android;
using Android.App;
using Android.OS;
using Veldrid;

[Activity(Label = "@string/app_name", MainLauncher = true, Theme = "@android:style/Theme.DeviceDefault.NoActionBar.Fullscreen", ConfigurationChanges = Android.Content.PM.ConfigChanges.KeyboardHidden | Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize)]
public class MainActivity : Activity
{
    public GameSurface surface;

    protected override void OnCreate(Bundle savedInstanceState)
    {
        base.OnCreate(savedInstanceState);

        GraphicsBackend backend = GraphicsDevice.IsBackendSupported(GraphicsBackend.Vulkan) ? GraphicsBackend.Vulkan : GraphicsBackend.Vulkan;
        surface = new GameSurface(this, backend);
        surface.DeviceCreated += () => Task.Factory.StartNew(() => AndroidEntry.Init(), TaskCreationOptions.LongRunning);
        SetContentView(surface);
    }
}