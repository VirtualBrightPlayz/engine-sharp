using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
// using Silk.NET.Input;
using Veldrid;
#if WEBGL
using Microsoft.JSInterop;
#endif

namespace Engine.Assets
{
    public partial class InputHandler : InputSnapshot, IDisposable
    {
        private HashSet<Veldrid.Key> _currentlyPressedKeys = new HashSet<Veldrid.Key>();
        private HashSet<Veldrid.Key> _newlyPressedKeysThisFrame = new HashSet<Veldrid.Key>();
        private HashSet<Veldrid.MouseButton> _currentlyPressedMouseButtons = new HashSet<Veldrid.MouseButton>();
        private HashSet<Veldrid.MouseButton> _newlyPressedMouseButtonsThisFrame = new HashSet<Veldrid.MouseButton>();
        private List<KeyEvent> _keyEvents = new List<KeyEvent>();
        private List<MouseEvent> _mouseEvents = new List<MouseEvent>();
        private List<char> _keyCharEvents = new List<char>();
        private Vector2 _mousePosition;
        private Vector2 _mouseDelta;
        private Vector2 _mouseDeltaOffset;
        public Vector2 Position
        {
            get => _mousePosition;
            set
            {
                if (MiscGlobals.IsFocused)
                {
                    // TODO:
                    /*
                    var oldpos = Program.GameInputContext.Mice[0].Position;
                    Program.GameInputContext.Mice[0].Position = value;
                    _mouseDeltaOffset = value - oldpos;
                    */
                }
            }
        }
        public bool _mouseLocked = false;
        public bool IsMouseLocked
        {
            get => _mouseLocked;
            set
            {
                if (MiscGlobals.IsFocused || !value)
                {
                    // TODO:
                    _mouseLocked = value;
                    // Program.GameInputContext.Mice[0].Cursor.CursorMode = value ? CursorMode.Hidden : CursorMode.Normal;
                #if WEBGL
                    MiscGlobals.WebRuntime.InvokeVoid("lockMouse", value);
                #endif
                }
            }
        }

        public Vector2 MouseDelta => _mouseDelta;

        /*
        public static Veldrid.Key SilkToVeldridKey(Silk.NET.Input.Key key)
        {
            try
            {
                switch (key)
                {
                    default:
                        return Enum.Parse<Veldrid.Key>(Enum.GetName(key), true);
                }
            }
            catch (ArgumentException)
            {
                return Veldrid.Key.Unknown;
            }
        }

        public static Veldrid.MouseButton SilkToVeldridMouseButton(Silk.NET.Input.MouseButton key)
        {
            try
            {
                switch (key)
                {
                    default:
                        return Enum.Parse<Veldrid.MouseButton>(Enum.GetName(key), true);
                }
            }
            catch (ArgumentException)
            {
                return Veldrid.MouseButton.Left;
            }
        }
        */

        public InputHandler()
        {
            /*Program.GameInputContext.Keyboards[0].KeyUp += KeyUp;
            Program.GameInputContext.Keyboards[0].KeyDown += KeyDown;
            Program.GameInputContext.Keyboards[0].KeyChar += KeyChar;
            Program.GameInputContext.Mice[0].MouseUp += MouseUp;
            Program.GameInputContext.Mice[0].MouseDown += MouseDown;*/
        }

        /*
        private void KeyChar(Silk.NET.Input.IKeyboard arg1, char arg2)
        {
            _keyCharEvents.Add(arg2);
        }
        */

        private ModifierKeys GetModifierKeys()
        {
            ModifierKeys mod = ModifierKeys.None;
            if (_currentlyPressedKeys.Contains(Veldrid.Key.ShiftLeft) || _currentlyPressedKeys.Contains(Veldrid.Key.ShiftRight))
                mod |= ModifierKeys.Shift;
            if (_currentlyPressedKeys.Contains(Veldrid.Key.ControlLeft) || _currentlyPressedKeys.Contains(Veldrid.Key.ControlRight))
                mod |= ModifierKeys.Control;
            if (_currentlyPressedKeys.Contains(Veldrid.Key.AltLeft) || _currentlyPressedKeys.Contains(Veldrid.Key.AltRight))
                mod |= ModifierKeys.Alt;
            return mod;
        }

        /*
        private void MouseUp(Silk.NET.Input.IMouse m, Silk.NET.Input.MouseButton btn)
        {
            Veldrid.MouseButton k = SilkToVeldridMouseButton(btn);
            _mouseEvents.Add(new MouseEvent(k, false));
            _currentlyPressedMouseButtons.Remove(k);
            _newlyPressedMouseButtonsThisFrame.Remove(k);
        }

        private void MouseDown(Silk.NET.Input.IMouse m, Silk.NET.Input.MouseButton btn)
        {
            Veldrid.MouseButton k = SilkToVeldridMouseButton(btn);
            _mouseEvents.Add(new MouseEvent(k, true));
            if (_currentlyPressedMouseButtons.Add(k))
            {
                _newlyPressedMouseButtonsThisFrame.Add(k);
            }
        }

        private void KeyDown(Silk.NET.Input.IKeyboard kbd, Silk.NET.Input.Key key, int arg3)
        {
            Veldrid.Key k = SilkToVeldridKey(key);
            _keyEvents.Add(new KeyEvent(k, true, GetModifierKeys()));
            if (_currentlyPressedKeys.Add(k))
            {
                _newlyPressedKeysThisFrame.Add(k);
            }
        }

        private void KeyUp(Silk.NET.Input.IKeyboard kbd, Silk.NET.Input.Key key, int arg3)
        {
            Veldrid.Key k = SilkToVeldridKey(key);
            _keyEvents.Add(new KeyEvent(k, false, GetModifierKeys()));
            _currentlyPressedKeys.Remove(k);
            _newlyPressedKeysThisFrame.Remove(k);
        }
        */

        public void Update()
        {
            _newlyPressedKeysThisFrame.Clear();
            _newlyPressedMouseButtonsThisFrame.Clear();
            _keyEvents.Clear();
            _keyCharEvents.Clear();
            _mouseEvents.Clear();
            UpdateMouse();
            /*_mouseDelta = _mousePosition - Program.GameInputContext.Mice[0].Position + _mouseDeltaOffset;
            _mouseDeltaOffset = Vector2.Zero;
            _mousePosition = Program.GameInputContext.Mice[0].Position;*/
        }

        public IReadOnlyList<KeyEvent> KeyEvents => _keyEvents;

        public IReadOnlyList<MouseEvent> MouseEvents => _mouseEvents;

        public IReadOnlyList<char> KeyCharPresses => _keyCharEvents;

        public Vector2 MousePosition => _mousePosition;

        public float WheelDelta => 0f;

        public bool IsMouseDown(Veldrid.MouseButton button)
        {
            return (_currentlyPressedMouseButtons.Contains(button));
        }

        public bool IsKeyPressed(Veldrid.Key key)
        {
            return _currentlyPressedKeys.Contains(key);
        }

        public void Dispose()
        {
            /*Program.GameInputContext.Keyboards[0].KeyUp -= KeyUp;
            Program.GameInputContext.Keyboards[0].KeyDown -= KeyDown;
            Program.GameInputContext.Keyboards[0].KeyChar -= KeyChar;
            Program.GameInputContext.Mice[0].MouseUp -= MouseUp;
            Program.GameInputContext.Mice[0].MouseDown -= MouseDown;*/
        }
    }
}
