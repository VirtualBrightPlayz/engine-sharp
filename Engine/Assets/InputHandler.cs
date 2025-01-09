using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using Engine.Assets.Rendering;
using Veldrid;
using Veldrid.Sdl2;
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
                #if !WEBGL
                    var oldpos = _mousePosition;
                    RenderingGlobals.Window.SetMousePosition(value);
                    _mouseDeltaOffset = value - oldpos;
                    _mousePosition = value;
                #endif
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
                    _mouseLocked = value;
                #if WEBGL
                    MiscGlobals.WebRuntime.InvokeVoid("lockMouse", value);
                #else
                    RenderingGlobals.Window.CursorVisible = !value;
                #endif
                }
            }
        }

    #if WEBGL
        public Vector2 MouseDelta => _mouseDelta;
    #else
        public Vector2 MouseDelta => -RenderingGlobals.Window.MouseDelta;// + _mouseDelta;
    #endif

        public InputHandler()
        {
        }

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

        public void Update()
        {
            _newlyPressedKeysThisFrame.Clear();
            _newlyPressedMouseButtonsThisFrame.Clear();
            _keyEvents.Clear();
            _keyCharEvents.Clear();
            _mouseEvents.Clear();
        #if WEBGL
            UpdateMouse();
        #else
            MiscGlobals.IsFocused = RenderingGlobals.Window == null ? true : RenderingGlobals.Window.Focused;
            _mouseDelta = new Vector2((int)_mouseDeltaOffset.X, (int)_mouseDeltaOffset.Y);
            _mouseDeltaOffset = Vector2.Zero;
            _mousePosition = MousePosition;

            foreach (var key in MiscGlobals.Snapshot.KeyEvents.Where(x => x.Down && !x.Repeat).Select(x => x.Key))
            {
                _currentlyPressedKeys.Add(key);
            }

            foreach (var key in MiscGlobals.Snapshot.KeyEvents.Where(x => !x.Down && !x.Repeat).Select(x => x.Key))
            {
                _currentlyPressedKeys.Remove(key);
            }
        #endif
        }

    #if WEBGL
        public IReadOnlyList<KeyEvent> KeyEvents => _keyEvents;
    #else
        public IReadOnlyList<KeyEvent> KeyEvents => MiscGlobals.Snapshot.KeyEvents;
    #endif

    #if WEBGL
        public IReadOnlyList<MouseEvent> MouseEvents => _mouseEvents;
    #else
        public IReadOnlyList<MouseEvent> MouseEvents => MiscGlobals.Snapshot.MouseEvents;
    #endif

    #if WEBGL
        public IReadOnlyList<char> KeyCharPresses => _keyCharEvents;
    #else
        public IReadOnlyList<char> KeyCharPresses => MiscGlobals.Snapshot.KeyCharPresses;
    #endif

    #if WEBGL
        public Vector2 MousePosition => _mousePosition;
    #else
        public Vector2 MousePosition => MiscGlobals.Snapshot.MousePosition;
    #endif

    #if WEBGL
        public float WheelDelta => 0f;
    #else
        public float WheelDelta => MiscGlobals.Snapshot.WheelDelta;
    #endif

        public bool IsMouseDown(Veldrid.MouseButton button)
        {
        #if WEBGL
            return (_currentlyPressedMouseButtons.Contains(button));
        #else
            return MiscGlobals.Snapshot.IsMouseDown(button);
        #endif
        }

        public bool IsKeyPressed(Veldrid.Key key)
        {
            return _currentlyPressedKeys.Contains(key);
        }

        public void Dispose()
        {
        }
    }
}
