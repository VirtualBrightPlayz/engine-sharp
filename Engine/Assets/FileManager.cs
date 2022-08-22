using System;
using System.IO;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
#if WEBGL && !SHADER_COMPILER
using Microsoft.AspNetCore.Components.WebAssembly.Http;
#endif

namespace Engine.Assets
{
    public static class FileManager
    {
        public static string HttpPrefix { get; set; } = string.Empty;
        public static Func<string, string> HttpCallback { get; set; }
        private static bool isBusy = false;
        public static bool IsBusy => isBusy;

        public static async Task<Stream> LoadStream(string path)
        {
            while (isBusy)
            {
                await Task.Yield();
            }
        #if WEBGL && !SHADER_COMPILER
            // isBusy = true;
            Console.WriteLine(HttpPrefix + "/" + path);
            using var client = new HttpClient();
            var stream = await client.GetStreamAsync(HttpPrefix + "/" + path);
            isBusy = false;
            return stream;
        #else
            var stream = typeof(FileManager).Assembly.GetManifestResourceStream(typeof(FileManager).Assembly.FullName + "." + path.Replace("/", "."));
            string filepath = Path.Combine(Directory.GetCurrentDirectory(), path);
            if (stream == null && File.Exists(filepath))
                stream = File.OpenRead(filepath);
            return stream;
        #endif
        }

        public static async Task<byte[]> LoadBytes(string path)
        {
            using var stream = await LoadStream(path);
            if (stream == null)
                return null;
            byte[] data = new byte[stream.Length];
            stream.Read(data, 0, data.Length);
            return data;
        }

        public static async Task<string> LoadString(string path)
        {
            return Encoding.UTF8.GetString(await LoadBytes(path));
        }

        public static async Task<string> LoadStringASCII(string path)
        {
            return Encoding.ASCII.GetString(await LoadBytes(path));
        }

        public static async Task<bool> Exists(string path)
        {
            using var stream = await LoadStream(path);
            return stream != null;
        }
    }
}