using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Engine.Assets
{
    public static class FileManager
    {
        private static ConcurrentDictionary<string, byte[]> streams = new ConcurrentDictionary<string, byte[]>();

        public static Stream LoadStream(string path)
        {
            if (streams.TryGetValue(path, out byte[] data))
            {
                if (data == null)
                    return null;
                return new MemoryStream(data, false);
            }
            var stream = typeof(FileManager).Assembly.GetManifestResourceStream(typeof(FileManager).Assembly.FullName + "." + path.Replace("/", "."));
            if (stream == null)
                stream = typeof(FileManager).Assembly.GetManifestResourceStream(path.Replace("/", "."));
            string filepath = Path.Combine(typeof(FileManager).Assembly.Location, "..", path);
            if (stream == null && File.Exists(filepath))
                stream = File.OpenRead(filepath);
            filepath = Path.Combine(Directory.GetCurrentDirectory(), path);
            if (stream == null && File.Exists(filepath))
                stream = File.OpenRead(filepath);
            if (stream == null)
            {
                streams.TryAdd(path, null);
                return null;
            }
            byte[] arr = new byte[stream.Length];
            stream.Read(arr);
            stream.Dispose();
            streams.TryAdd(path, arr);
            MemoryStream ms = new MemoryStream(arr, false);
            return ms;
        }

        public static byte[] LoadBytes(string path)
        {
            using var stream = LoadStream(path);
            if (stream == null)
                return new byte[0];
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

        public static void UnCache(string path)
        {
            streams.TryRemove(path, out _);
        }

        public static bool IsCached(string path)
        {
            return streams.ContainsKey(path);
        }

        public static bool Exists(string path)
        {
            using var stream = LoadStream(path);
            return stream != null;
        }
    }
}