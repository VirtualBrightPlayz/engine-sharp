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
        public static InputHandler GameInputSnapshot { get; private set; }
        public static bool IsClosing { get; set; } = false;
        public static bool IsFocused { get; private set; } = false;
        public static double FPS { get; private set; }
        public static double TPS { get; private set; }
        public static bool ReCreateAllNextFrame { get; set; } = false;
    #if WEBGL
        public static WebAssemblyJSRuntime WebRuntime { get; set; }
    #endif

        public static void InitGameMisc()
        {
            GameInputSnapshot = new InputHandler();
        #if WEBGL
            IsFocused = true;
        #endif
        }
    }
}