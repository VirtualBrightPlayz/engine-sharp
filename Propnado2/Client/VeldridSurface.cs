using System;
using System.Collections.Generic;
using System.IO;
using Avalonia;
using Avalonia.Media;
using Avalonia.Platform;
using Avalonia.Skia;
using Avalonia.Visuals.Media.Imaging;
using Engine.Assets;
using Engine.Assets.Rendering;
using Myra.Graphics2D.UI;
using Myra.Platform;
using SkiaSharp;
using Veldrid;

public class VeldridSurface : IMyraPlatform
{
    public System.Drawing.Point ViewSize => new System.Drawing.Point((int)RenderingGlobals.ViewSize.X, (int)RenderingGlobals.ViewSize.Y);
    public IMyraRenderer Renderer { get; set; } = new VeldridDrawCtx();
    private Dictionary<Keys, Key> keyLookup = new Dictionary<Keys, Key>();

    public VeldridSurface()
    {
        var keys = Enum.GetValues<Keys>();
        foreach (var k in keys)
        {
            var name = Enum.GetName(k);

            Key key;
            if (Enum.TryParse(name, out key))
            {
                keyLookup[k] = key;
            }
        }
    }

    public MouseInfo GetMouseInfo()
    {
        var input = MiscGlobals.GameInputHandler;
        return new MouseInfo()
        {
            Wheel = input.WheelDelta,
            Position = new System.Drawing.Point((int)input.MousePosition.X, (int)input.MousePosition.Y),
            IsLeftButtonDown = input.IsMouseDown(Veldrid.MouseButton.Left),
            IsRightButtonDown = input.IsMouseDown(Veldrid.MouseButton.Right),
            IsMiddleButtonDown = input.IsMouseDown(Veldrid.MouseButton.Middle),
        };
    }

    public TouchCollection GetTouchState()
    {
        return TouchCollection.Empty;
    }

    public void SetKeysDown(bool[] keys)
    {
        var input = MiscGlobals.GameInputHandler;
        for (int i = 0; i < keys.Length; i++)
        {
            var ks = (Keys)i;
            keys[i] = keyLookup.TryGetValue(ks, out Key k) ? input.IsKeyPressed(k) : false;
        }
    }
}