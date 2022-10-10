#if WEBGL
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
            _mouseDelta = Vector2.Zero;
            _mouseDeltaOffset = Vector2.Zero;
            _mousePosition = _mouseMove;
        }

        [JSInvokable]
        public static void OnMouseUnlocked()
        {
            if (MiscGlobals.GameInputHandler == null)
                return;
            MiscGlobals.GameInputHandler._mouseLocked = false;
        }

        [JSInvokable]
        public static void OnKeyDown(string key)
        {
            if (MiscGlobals.GameInputHandler == null)
                return;
            if (Enum.TryParse<Key>(key, true, out Key res))
            {
                MiscGlobals.GameInputHandler._currentlyPressedKeys.Add(res);
                MiscGlobals.GameInputHandler._newlyPressedKeysThisFrame.Add(res);
                MiscGlobals.GameInputHandler._keyEvents.Add(new KeyEvent(res, true, MiscGlobals.GameInputHandler.GetModifierKeys()));
            }
        }

        [JSInvokable]
        public static void OnKeyUp(string key)
        {
            if (MiscGlobals.GameInputHandler == null)
                return;
            if (Enum.TryParse<Key>(key, true, out Key res))
            {
                MiscGlobals.GameInputHandler._currentlyPressedKeys.Remove(res);
                MiscGlobals.GameInputHandler._newlyPressedKeysThisFrame.Remove(res);
                MiscGlobals.GameInputHandler._keyEvents.Add(new KeyEvent(res, false, MiscGlobals.GameInputHandler.GetModifierKeys()));
            }
        }

        [JSInvokable]
        public static void OnMousePosition(float x, float y)
        {
            if (MiscGlobals.GameInputHandler == null)
                return;
            MiscGlobals.GameInputHandler._mouseMove = new Vector2(x, y);
        }

        [JSInvokable]
        public static void OnMouseMove(float x, float y)
        {
            if (MiscGlobals.GameInputHandler == null)
                return;
            MiscGlobals.GameInputHandler._mouseDelta += new Vector2(x, y);
        }

        [JSInvokable]
        public static void OnMouseDown(int button)
        {
            if (MiscGlobals.GameInputHandler == null)
                return;
            if (MiscGlobals.GameInputHandler._mouseDownButtons != button)
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
                    // Console.WriteLine($"down {btn.Value}");
                    MiscGlobals.GameInputHandler._currentlyPressedMouseButtons.Add(btn.Value);
                    MiscGlobals.GameInputHandler._newlyPressedMouseButtonsThisFrame.Add(btn.Value);
                    MiscGlobals.GameInputHandler._mouseEvents.Add(new MouseEvent(btn.Value, true));
                }
                MiscGlobals.GameInputHandler._mouseDownButtons = button;
            }
        }

        [JSInvokable]
        public static void OnMouseUp(int button)
        {
            if (MiscGlobals.GameInputHandler == null)
                return;
            if (MiscGlobals.GameInputHandler._mouseDownButtons != button)
            {
                int bitButton = (MiscGlobals.GameInputHandler._mouseDownButtons ^ button);
                MouseButton? btn = null;
                if ((bitButton & 1) == 1)
                {
                    btn = MouseButton.Left;
                }
                if ((bitButton & 2) == 2)
                {
                    btn = MouseButton.Right;
                }
                if ((bitButton & 4) == 4)
                {
                    btn = MouseButton.Middle;
                }
                if (btn.HasValue)
                {
                    // Console.WriteLine($"up {btn.Value}");
                    MiscGlobals.GameInputHandler._currentlyPressedMouseButtons.Remove(btn.Value);
                    MiscGlobals.GameInputHandler._newlyPressedMouseButtonsThisFrame.Remove(btn.Value);
                    MiscGlobals.GameInputHandler._mouseEvents.Add(new MouseEvent(btn.Value, false));
                }
                MiscGlobals.GameInputHandler._mouseDownButtons = button;
            }
        }
    }
}
#endif