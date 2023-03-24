using Android.Content;
using Android.Graphics;
using Android.Runtime;
using Android.Views;
using System;
using System.Diagnostics;
using System.Threading.Tasks;
using Veldrid;

namespace SampleBase.Android
{
    public class VeldridSurfaceView : SurfaceView, ISurfaceHolderCallback
    {
        private readonly GraphicsBackend _backend;
        protected GraphicsDeviceOptions DeviceOptions { get; }
        private bool _surfaceDestroyed;
        private bool _paused;
        private bool _enabled;
        private bool _needsResize;
        private bool _surfaceCreated;

        public GraphicsDevice GraphicsDevice { get; protected set; }
        public Swapchain MainSwapchain { get; protected set; }

        public event Action Rendering;
        public event Action DeviceCreated;
        public event Action DeviceDisposed;
        public event Action Resized;
        public event Action OnSurfaceCreated;
        public event Action OnSurfaceDestroyed;

        public VeldridSurfaceView(Context context, GraphicsBackend backend)
            : this(context, backend, new GraphicsDeviceOptions())
        {
        }

        public VeldridSurfaceView(Context context, GraphicsBackend backend, GraphicsDeviceOptions deviceOptions) : base(context)
        {
            if (!(backend == GraphicsBackend.Vulkan || backend == GraphicsBackend.OpenGLES))
            {
                throw new NotSupportedException($"{backend} is not supported on Android.");
            }

            _backend = backend;
            DeviceOptions = deviceOptions;
            Holder.AddCallback(this);
        }

        public void Disable()
        {
            _enabled = false;
        }

        public void SurfaceCreated(ISurfaceHolder holder)
        {
            bool deviceCreated = false;
            if (_backend == GraphicsBackend.Vulkan)
            {
                if (GraphicsDevice == null)
                {
                    GraphicsDevice = GraphicsDevice.CreateVulkan(DeviceOptions);
                    deviceCreated = true;
                }

                SwapchainSource ss = SwapchainSource.CreateAndroidSurface(holder.Surface.Handle, JNIEnv.Handle);
                SwapchainDescription sd = new SwapchainDescription(
                    ss,
                    (uint)Width,
                    (uint)Height,
                    DeviceOptions.SwapchainDepthFormat,
                    DeviceOptions.SyncToVerticalBlank);
                MainSwapchain = GraphicsDevice.ResourceFactory.CreateSwapchain(sd);
            }
            else
            {
                SwapchainSource ss = SwapchainSource.CreateAndroidSurface(holder.Surface.Handle, JNIEnv.Handle);
                SwapchainDescription sd = new SwapchainDescription(
                    ss,
                    (uint)Width,
                    (uint)Height,
                    DeviceOptions.SwapchainDepthFormat,
                    DeviceOptions.SyncToVerticalBlank);
                GraphicsDevice = GraphicsDevice.CreateOpenGLES(DeviceOptions, sd);
                MainSwapchain = GraphicsDevice.MainSwapchain;
                deviceCreated = true;
            }

            _surfaceDestroyed = false;

            if (deviceCreated)
            {
                DeviceCreated?.Invoke();
            }

            _surfaceCreated = true;
            OnSurfaceCreated?.Invoke();
            _paused = false;
        }

        public void RunContinuousRenderLoop()
        {
            Task.Factory.StartNew(() => RenderLoop(), TaskCreationOptions.LongRunning);
        }

        public void SurfaceDestroyed(ISurfaceHolder holder)
        {
            _surfaceCreated = false;
            _surfaceDestroyed = true;
            HandleSurfaceDestroyed();
        }

        public void SurfaceChanged(ISurfaceHolder holder, [GeneratedEnum] Format format, int width, int height)
        {
            _needsResize = true;
        }

        private void RenderLoop()
        {
            _enabled = true;
            while (_enabled)
            {
                try
                {
                    if (_paused)
                    {
                        continue;
                    }

                    if (!_surfaceCreated)
                    {
                        continue;
                    }

                    if (_needsResize)
                    {
                        _needsResize = false;
                        MainSwapchain.Resize((uint)Width, (uint)Height);
                        Resized?.Invoke();
                    }

                    if (GraphicsDevice != null)
                    {
                        Rendering?.Invoke();
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("Encountered an error while rendering: " + e);
                    throw;
                }
            }
        }

        private void HandleSurfaceDestroyed()
        {
            if (_backend == GraphicsBackend.Vulkan)
            {
                MainSwapchain?.Dispose();
                MainSwapchain = null;
            }
            else
            {
                GraphicsDevice?.Dispose();
                GraphicsDevice = null;
                MainSwapchain = null;
                DeviceDisposed?.Invoke();
            }
            OnSurfaceDestroyed?.Invoke();
        }

        public void OnPause()
        {
            _paused = true;
        }

        public void OnResume()
        {
            // _paused = false;
        }
    }
}