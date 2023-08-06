using System;

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

        public void Fatal(string msg)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[FATAL] {msg}");
            Console.ResetColor();
            if (ExitOnFatal)
                Environment.Exit(1);
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
