using System;
using Engine.Assets.Rendering;
using Silk.NET.Core.Native;
using Silk.NET.SDL;

namespace Engine.Assets.Logging
{
    public class ConsoleLog : ILog, IErrorHandler
    {
        public bool ExitOnFatal { get; set; } = false;

        public void Debug(string msg)
        {
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine($"[DEBUG] {msg}");
            Console.ResetColor();
        }

        public void Error(string msg)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"[ERROR] {msg}");
            Console.ResetColor();
        }

        public unsafe void Fatal(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[FATAL] {msg}");
            Console.ResetColor();
            if (ExitOnFatal)
            {
                Sdl sdl = Sdl.GetApi();
                RenderingGlobals.Window.WindowState = Veldrid.WindowState.Normal;
                RenderingGlobals.SwapMainBuffer();
                sdl.ShowSimpleMessageBox((uint)MessageBoxFlags.Error, "Fatal Error", msg, (Window*)RenderingGlobals.Window.SdlWindowHandle);
                Environment.Exit(1);
            }
        }

        public void Info(string msg)
        {
            Console.ForegroundColor = ConsoleColor.DarkGreen;
            Console.WriteLine($"[INFO] {msg}");
            Console.ResetColor();
        }

        public void Warn(string msg)
        {
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine($"[WARN] {msg}");
            Console.ResetColor();
        }
    }
}
