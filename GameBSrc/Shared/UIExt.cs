using System;
using System.IO;
using System.Numerics;
using System.Threading.Tasks;
using Engine;
using Engine.Assets;
using Engine.Assets.Audio;
using Engine.Assets.Textures;
using ImGuiNET;
using Veldrid;

namespace GameBSrc
{
    public static class UIExt
    {
        public static ImGuiIOPtr _io;
        public static ImDrawListPtr _drawList;
        public static async Task<ImFontPtr> Pretext(ImGuiRenderer renderer, float size)
        {
            var font = await ResourceManager.LoadImGuiFont(renderer, $"{BGame.Instance.Data.GFXDir}/Pretext.TTF", size);
            return font;
        }
        public static Vector2 MenuScale = Vector2.One;

        public static void BeginDraw()
        {
            _io = ImGui.GetIO();
            _drawList = ImGui.GetBackgroundDrawList();
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
            // _drawList.AddText(pos, color, text);
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
    }
}