#if SHADER_COMPILER
using System;
using System.IO;
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
            if (Shaders.Length == 0)
            {
                Log.LogError("No shaders to reflect!");
                return false;
            }
            foreach (var shader in Shaders)
            {
                var processor = new ShaderProcessor();
                processor.CreateShaders(shader.ItemSpec, File.ReadAllText(shader.ItemSpec), CreateShaders).Wait();
            }
            return true;
        }

        private void CreateShaders(string vertCode, string fragCode, string pass, string path)
        {
            var compileResult = SpirvCompilation.CompileVertexFragment(Encoding.ASCII.GetBytes(vertCode), Encoding.ASCII.GetBytes(fragCode), CrossCompileTarget.GLSL).Reflection;
            Log.LogMessage($"{path}.{pass}.json");
            File.WriteAllText($"{path}.{pass}.json", JsonConvert.SerializeObject(compileResult));
        }
    }
}
#endif