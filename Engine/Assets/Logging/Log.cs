using System;

public static class Log
{
    public static ILog LogInterface { get; set; } = null;
    public static IErrorHandler ErrorHandler { get; set; } = null;

    public static void Debug(string pfx, string msg) => LogInterface?.Debug($"[{pfx}] {msg}");
    public static void Info(string pfx, string msg) => LogInterface?.Info($"[{pfx}] {msg}");
    public static void Warn(string pfx, string msg) => LogInterface?.Warn($"[{pfx}] {msg}");
    public static void Error(string pfx, string msg) => LogInterface?.Error($"[{pfx}] {msg}");
    public static void Fatal(string pfx, string msg)
    {
        if (ErrorHandler == null)
            FallbackFatal($"[{pfx}] {msg}");
        else
            ErrorHandler.Fatal($"[{pfx}] {msg}");
    }
    private static void FallbackFatal(string msg) => throw new Exception(msg);
}
