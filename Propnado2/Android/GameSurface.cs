using System;
using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Views;
using Engine.Assets;
using Engine.Assets.Audio;
using Engine.Assets.Rendering;
using Veldrid;

public class GameSurface : SurfaceView, ISurfaceHolderCallback
{
    private GraphicsBackend _backend;
    public event Action DeviceCreated;

    public GameSurface(Context ctx, GraphicsBackend backend) : base(ctx)
    {
        _backend = backend;
        Holder.AddCallback(this);
    }

    public void SurfaceChanged(ISurfaceHolder holder, [GeneratedEnum] Format format, int width, int height)
    {
        // throw new System.NotImplementedException();
    }

    public void SurfaceCreated(ISurfaceHolder holder)
    {
        if (_backend == GraphicsBackend.Vulkan)
        {
            if (RenderingGlobals.GameGraphics == null)
            {
                var ss = SwapchainSource.CreateAndroidSurface(holder.Surface.Handle, JNIEnv.Handle);
                RenderingGlobals.InitGameGraphicsFrom(_backend, ss, Width, Height);
            }
        }
        else
        {
            throw new System.NotImplementedException();
        }

        // DeviceCreated?.Invoke();
    }

    public void SurfaceDestroyed(ISurfaceHolder holder)
    {
        // throw new System.NotImplementedException();
    }
}