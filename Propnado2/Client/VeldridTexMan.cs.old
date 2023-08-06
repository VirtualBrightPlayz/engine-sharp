using System.Collections.Generic;
using System.Drawing;
using Engine.Assets.Rendering;
using Engine.Assets.Textures;
using FontStashSharp.Interfaces;
using SixLabors.ImageSharp.PixelFormats;

public class VeldridTexMan : ITexture2DManager
{
    public List<Texture2D> textures = new List<Texture2D>();
    public List<Material> mats = new List<Material>();
    private GraphicsShader shader = new GraphicsShader("Shaders/Blit");

    public object CreateTexture(int width, int height)
    {
        var tex = new Texture2D("UI_Texture", (uint)width, (uint)height);
        textures.Add(tex);
        var mat = new Material("UI_Blit", shader);
        mats.Add(mat);
        return mats.Count - 1;
    }

    public Point GetTextureSize(object texture)
    {
        var tex = textures[(int)texture];
        // var mat = mats[(int)texture];
        return new Point((int)tex.Width, (int)tex.Height);
    }

    public void SetTextureData(object texture, Rectangle bounds, byte[] data)
    {
        var tex = textures[(int)texture];
        // var mat = mats[(int)texture];
        for (int y = bounds.Y; y < bounds.Height; y++)
        {
            for (int x = bounds.X; x < bounds.Width; x++)
            {
                // var col = new Rgba32(data[x + y * bounds.Width + 0], data[x + y * bounds.Width + 1], data[x + y * bounds.Width + 2], data[x + y * bounds.Width + 3]);
                var col = new Rgba32(data[x * 4 + y * bounds.Width * 4 + 0], data[x * 4 + y * bounds.Width * 4 + 1], data[x * 4 + y * bounds.Width * 4 + 2], data[x * 4 + y * bounds.Width * 4 + 3]);
                tex.SetPixel(col, x, y);
            }
        }
        tex.Apply();
    }
}