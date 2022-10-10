using Engine.Assets.Rendering;
using Engine;
using Veldrid;
using System.Numerics;
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
        public static double FPS { get; set; }
        public static double TPS { get; set; }
        public static bool ReCreateAllNextFrame { get; set; } = false;
    #if WEBGL
        public static WebAssemblyJSRuntime WebRuntime { get; set; }
    #endif

        public static void InitGameMisc()
        {
            GameInputHandler = new InputHandler();
        #if WEBGL
            IsFocused = true;
        #endif
        }
    }
}