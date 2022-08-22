using System;
using System.Numerics;
using Microsoft.JSInterop;
using Veldrid;

namespace Engine.Assets
{
    public partial class InputHandler
    {
        public Vector2 _mouseMove;
        public int _mouseDownButtons;

        private void UpdateMouse()
        {
            _mouseDelta = _mousePosition - _mouseMove + _mouseDeltaOffset;
            _mouseDeltaOffset = Vector2.Zero;
            _mousePosition = _mouseMove;
        }

        [JSInvokable]
        public static void OnKeyDown(string key)
        {
            if (Enum.TryParse<Key>(key, true, out Key res))
            {
                MiscGlobals.GameInputSnapshot._currentlyPressedKeys.Add(res);
                MiscGlobals.GameInputSnapshot._newlyPressedKeysThisFrame.Add(res);
                MiscGlobals.GameInputSnapshot._keyEvents.Add(new KeyEvent(res, true, MiscGlobals.GameInputSnapshot.GetModifierKeys()));
            }
        }

        [JSInvokable]
        public static void OnKeyUp(string key)
        {
            if (Enum.TryParse<Key>(key, true, out Key res))
            {
                MiscGlobals.GameInputSnapshot._currentlyPressedKeys.Remove(res);
                MiscGlobals.GameInputSnapshot._newlyPressedKeysThisFrame.Remove(res);
                MiscGlobals.GameInputSnapshot._keyEvents.Add(new KeyEvent(res, false, MiscGlobals.GameInputSnapshot.GetModifierKeys()));
            }
        }

        [JSInvokable]
        public static void OnMouseMove(float x, float y)
        {
            MiscGlobals.GameInputSnapshot._mouseMove = new Vector2(x, y);
        }

        [JSInvokable]
        public static void OnMouseDown(int button)
        {
            if (MiscGlobals.GameInputSnapshot._mouseDownButtons == button)
            {
                MouseButton? btn = null;
                if ((button & 1) == 1)
                {
                    btn = MouseButton.Left;
                }
                if ((button & 2) == 2)
                {
                    btn = MouseButton.Right;
                }
                if ((button & 4) == 4)
                {
                    btn = MouseButton.Middle;
                }
                if (btn.HasValue)
                {
                    MiscGlobals.GameInputSnapshot._currentlyPressedMouseButtons.Add(btn.Value);
                    MiscGlobals.GameInputSnapshot._newlyPressedMouseButtonsThisFrame.Add(btn.Value);
                    MiscGlobals.GameInputSnapshot._mouseEvents.Add(new MouseEvent(btn.Value, true));
                }
                MiscGlobals.GameInputSnapshot._mouseDownButtons = button;
            }
        }

        [JSInvokable]
        public static void OnMouseUp(int button)
        {
            if (MiscGlobals.GameInputSnapshot._mouseDownButtons == button)
            {
                MouseButton? btn = null;
                if ((button & 1) == 1)
                {
                    btn = MouseButton.Left;
                }
                if ((button & 2) == 2)
                {
                    btn = MouseButton.Right;
                }
                if ((button & 4) == 4)
                {
                    btn = MouseButton.Middle;
                }
                if (btn.HasValue)
                {
                    MiscGlobals.GameInputSnapshot._currentlyPressedMouseButtons.Remove(btn.Value);
                    MiscGlobals.GameInputSnapshot._newlyPressedMouseButtonsThisFrame.Remove(btn.Value);
                    MiscGlobals.GameInputSnapshot._mouseEvents.Add(new MouseEvent(btn.Value, false));
                }
                MiscGlobals.GameInputSnapshot._mouseDownButtons = button;
            }
        }
    }
}