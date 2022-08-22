using Engine.Assets.Rendering;
using Engine;
using Veldrid;
using System.Numerics;

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

        public static void InitGameMisc()
        {
            GameInputSnapshot = new InputHandler();
        }
    }
}