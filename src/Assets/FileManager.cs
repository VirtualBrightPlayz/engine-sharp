using System;
using System.IO;
using System.Text;

namespace Engine.Assets
{
    public static class FileManager
    {
        public static Stream LoadStream(string path)
        {
            var stream = typeof(Program).Assembly.GetManifestResourceStream(typeof(Program).Assembly.FullName + "." + path.Replace("/", "."));
            string filepath = Path.Combine(Directory.GetCurrentDirectory(), path);
            if (stream == null && File.Exists(filepath))
                stream = File.OpenRead(filepath);
            return stream;
        }

        public static byte[] LoadBytes(string path)
        {
            using var stream = LoadStream(path);
            if (stream == null)
                return null;
            byte[] data = new byte[stream.Length];
            stream.Read(data, 0, data.Length);
            return data;
        }

        public static string LoadString(string path)
        {
            return Encoding.UTF8.GetString(LoadBytes(path));
        }

        public static string LoadStringASCII(string path)
        {
            return Encoding.ASCII.GetString(LoadBytes(path));
        }
    }
}