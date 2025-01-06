#if SHADER_COMPILER
using System;
using System.IO;
using System.Reflection;
using System.Text;
using Microsoft.Build.Framework;
using Microsoft.Build.Utilities;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Veldrid.SPIRV;

namespace Engine.Assets.Rendering
{
    public class ReflectShaders : Task
    {
        [Required]
        public ITaskItem[] Shaders { get; set; }

        public override bool Execute()
        {
            string curDir = Directory.GetCurrentDirectory();
            /*AppDomain.CurrentDomain.AssemblyResolve += (s, a) =>
            {
                Console.WriteLine(a.Name.Split(',')[0]);
                var spath = Path.Combine(Path.GetDirectoryName(typeof(ReflectShaders).Assembly.Location), a.Name.Split(',')[0] + ".dll");
                Console.WriteLine(spath);
                if (File.Exists(spath)) return Assembly.LoadFile(spath);
                spath = Path.Combine(Directory.GetCurrentDirectory(), a.Name.Split(',')[0] + ".dll");
                Console.WriteLine(spath);
                if (File.Exists(spath)) return Assembly.LoadFile(spath);
                foreach (var item in Directory.GetFiles(Path.GetDirectoryName(typeof(ReflectShaders).Assembly.Location), $"*{a.Name.Split(',')[0]}*", SearchOption.AllDirectories))
                {
                    // if (Path.GetExtension(item) == ".dll")
                    {
                        return Assembly.LoadFile(item);
                    }
                }
                return null;
            };*/
            if (Shaders.Length == 0)
            {
                Log.LogError("No shaders to reflect!");
                return false;
            }
            Directory.SetCurrentDirectory(Path.GetDirectoryName(typeof(ReflectShaders).Assembly.Location));
            Environment.CurrentDirectory = Path.GetDirectoryName(typeof(ReflectShaders).Assembly.Location);
            foreach (var shader in Shaders)
            {
                var processor = new ShaderProcessor();
                processor.CreateShaders(Path.Combine(curDir, shader.ItemSpec), File.ReadAllText(Path.Combine(curDir, shader.ItemSpec)), CreateShaders).Wait();
            }
            Directory.SetCurrentDirectory(curDir);
            return true;
        }

        private System.Threading.Tasks.Task CreateShaders(string vertCode, string fragCode, string pass, string path)
        {
            var compileResult = SpirvCompilation.CompileVertexFragment(Encoding.ASCII.GetBytes(vertCode), Encoding.ASCII.GetBytes(fragCode), CrossCompileTarget.GLSL).Reflection;
            Log.LogMessage($"{path}.{pass}.json");
            File.WriteAllText($"{path}.{pass}.json", JsonConvert.SerializeObject(compileResult));
            return System.Threading.Tasks.Task.CompletedTask;
        }
    }
}
#endif