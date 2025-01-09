using System;
using System.IO;
using System.Numerics;
using Engine;
using Engine.Assets;
using Engine.Assets.Audio;
using Engine.Assets.Rendering;
using Engine.Assets.Textures;
using ImGuiNET;

namespace GameSrc
{
    public static class UIExt
    {
        public static ImGuiIOPtr _io;
        public static ImDrawListPtr _drawList;
        public static AudioSource _clickSource;
        public static string FontFolder => Path.Combine(SCPCB.Instance.Data.GFXDir, "font");
        public static string MenuFolder => Path.Combine(SCPCB.Instance.Data.GFXDir, "menu");
        public static Vector4 MenuSelected = new Vector4(0.1f, 0.1f, 0.1f, 1f);
        public static Texture2D MenuWhite = new Texture2D($"{MenuFolder}/menuwhite.jpg");
        public static Texture2D MenuBlack = new Texture2D($"{MenuFolder}/menublack.jpg");
        public static AudioClip ButtonSFX = new AudioClip(Path.Combine(SCPCB.Instance.Data.SFXDir, "Interact", "Button.ogg"));
        public static ImFontPtr CourierNew = LoadCourierNew(RenderingGlobals.GameImGui);
        public static ImFontPtr CourierNewBold = LoadCourierNewBold(RenderingGlobals.GameImGui);
        public static ImFontPtr DS_Digital = LoadDS_Digital(RenderingGlobals.GameImGui);
        public static ImFontPtr LoadCourierNew(Veldrid.ImGuiRenderer renderer) => ResourceManager.LoadImGuiFont(renderer, $"{FontFolder}/cour/Courier New.ttf");
        public static ImFontPtr LoadCourierNewBold(Veldrid.ImGuiRenderer renderer) => ResourceManager.LoadImGuiFont(renderer, $"{FontFolder}/courbd/Courier New.ttf");
        public static ImFontPtr LoadDS_Digital(Veldrid.ImGuiRenderer renderer) => ResourceManager.LoadImGuiFont(renderer, $"{FontFolder}/DS-DIGI/DS-Digital.ttf");
        public static Vector2 MenuScale = Vector2.One;

        public static void BeginDraw()
        {
            if (_clickSource == null)
            {
                _clickSource = new AudioSource("ClickSource");
                _clickSource.SetBuffer(ButtonSFX);
                _clickSource.MaxDistance = float.PositiveInfinity;
                _clickSource.RolloffFactor = 0f;
                _clickSource.ReferenceDistance = 0f;
            }
            _io = ImGui.GetIO();
            _drawList = ImGui.GetBackgroundDrawList();
            _clickSource.Position = AudioGlobals.Position;
        }

        public static bool IsInside(Vector2 val, Vector2 pos, Vector2 size)
        {
            Vector2 pos2 = pos + size;
            return val.X > pos.X && val.Y > pos.Y && val.X < pos2.X && val.Y < pos2.Y;
        }

        public static uint Color(Vector4 color)
        {
            byte[] b = new byte[]
            {
                (byte)(color.X * 255f),
                (byte)(color.Y * 255f),
                (byte)(color.Z * 255f),
                (byte)(color.W * 255f),
            };
            uint u = BitConverter.ToUInt32(b);
            return u;
        }

        public static void Image(Texture2D tex, Vector2 pos, Vector2 size)
        {
            pos *= MenuScale;
            size *= MenuScale;
            _drawList.AddImage(tex.ImGuiTex, pos, pos + size);
        }

        public static void TextLeft(ImFontPtr font, float fSize, Vector2 pos, Vector2 size, string text, uint color)
        {
            pos *= MenuScale;
            size *= MenuScale;
            _drawList.AddText(font, fSize, pos, color, text);
        }

        public static void TextRight(ImFontPtr font, float fSize, Vector2 pos, Vector2 size, string text, uint color)
        {
            pos *= MenuScale;
            size *= MenuScale;
            _drawList.AddText(font, fSize, pos + ImGui.CalcTextSize(text) / 2f, color, text);
        }

        public static void TextCentered(ImFontPtr font, float fSize, Vector2 pos, Vector2 size, string text, uint color)
        {
            pos *= MenuScale;
            size *= MenuScale;
            if (size == Vector2.Zero)
                size = ImGui.CalcTextSize(text);
            _drawList.AddText(font, fSize, pos + size / 2f - ImGui.CalcTextSize(text), color, text);
        }

        public static void Rect(Vector2 pos, Vector2 size, uint col)
        {
            pos *= MenuScale;
            size *= MenuScale;
            _drawList.AddRect(pos, pos + size, col);
        }

        public static bool Button(string text, float textSize, Vector2 pos, Vector2 size)
        {
            pos *= MenuScale;
            size *= MenuScale;
            float border = 3f;
            _drawList.AddImage(MenuWhite.ImGuiTex, pos, pos + size);
            bool over = IsInside(_io.MousePos, pos, size);
            if (over)
                _drawList.AddRectFilled(pos + Vector2.One * border, pos + size - Vector2.One * border, Color(MenuSelected));
            else
                _drawList.AddImage(MenuBlack.ImGuiTex, pos + Vector2.One * border, pos + size - Vector2.One * border);
            TextCentered(CourierNew, textSize, pos / MenuScale, size / MenuScale, text, Color(Vector4.One));
            bool clicked = over && _io.MouseClicked[0];
            if (clicked)
                _clickSource.Play();
            return clicked;
        }
    }
}