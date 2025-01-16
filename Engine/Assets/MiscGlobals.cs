using Engine.Assets.Rendering;
using Engine;
using Veldrid;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Reflection;
using System;
using OpenAL;
using Engine.Assets.Logging;
#if WEBGL
using Microsoft.JSInterop.WebAssembly;
#endif

namespace Engine.Assets
{
    public static class MiscGlobals
    {
        public static InputSnapshot Snapshot { get; set; }
        public static InputHandler GameInputHandler { get; private set; }
        public static bool IsClosing { get; set; } = false;
        public static bool IsFocused { get; internal set; } = false;
        public static double FPS { get; internal set; }
        public static bool ReCreateAllNextFrame { get; set; } = false;
    #if WEBGL
        public static WebAssemblyJSRuntime WebRuntime { get; set; }
    #endif
        private static double fpsTimer = 0d;
        private static int fpsCounter = 0;

        public static void InitGameMisc()
        {
            GameInputHandler = new InputHandler();
        #if WEBGL
            IsFocused = true;
        #endif
        }

        public static void Init()
        {
            ConsoleLog log = new ConsoleLog()
            {
                ExitOnFatal = true,
            };
            Log.LogInterface = log;
            Log.ErrorHandler = log;
            NativeLibrary.SetDllImportResolver(typeof(Vulkan.VulkanNative).Assembly, DLResolver);
            NativeLibrary.SetDllImportResolver(typeof(Assimp.AssimpContext).Assembly, DLResolver);
            NativeLibrary.SetDllImportResolver(typeof(OpenAL.ALC10).Assembly, OALResolver);
        }

        public static void Frame(double delta)
        {
            fpsCounter++;
            fpsTimer -= delta;
            if (fpsTimer <= 0d)
            {
                fpsTimer = 1d;
                FPS = fpsCounter;
                fpsCounter = 0;
            }
        }

        private static nint DLResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
        {
            if (libraryName.StartsWith("libdl") && OperatingSystem.IsLinux())
            {
                libraryName = "libdl.so.2";
            }
            return NativeLibrary.Load(libraryName, assembly, searchPath);
        }

        private static IntPtr OALResolver(string libraryName, Assembly assembly, DllImportSearchPath? searchPath)
        {
            if (libraryName.Equals("soft_oal.dll") && OperatingSystem.IsLinux())
            {
                libraryName = "libopenal.so.1";
            }
            return NativeLibrary.Load(libraryName, assembly, searchPath);
        }
    }
}