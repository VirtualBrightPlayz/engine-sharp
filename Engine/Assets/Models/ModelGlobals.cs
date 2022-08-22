using Engine.Assets.Rendering;
using Veldrid;

namespace Engine.Assets.Models
{
    public static class ModelGlobals
    {
        public static GraphicsDevice GameGraphics => RenderingGlobals.GameGraphics;
    }
}