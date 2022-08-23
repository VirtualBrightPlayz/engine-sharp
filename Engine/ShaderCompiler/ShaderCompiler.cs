#if SHADER_COMPILER
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Engine.Assets.Rendering;
using Newtonsoft.Json;
using Veldrid.SPIRV;

public class ShaderCompiler
{
    public static async Task Main(string[] args)
    {
        // string curDir = Directory.GetCurrentDirectory();
        foreach (var shader in args)
        {
            if (!shader.Contains("glsl"))
                continue;
            var processor = new ShaderProcessor();
            processor.CreateShaders(shader, File.ReadAllText(shader), CreateShaders).Wait();
        }
    }

    private static void CreateShaders(string vertCode, string fragCode, string pass, string path)
    {
        var compileResult = SpirvCompilation.CompileVertexFragment(Encoding.ASCII.GetBytes(vertCode), Encoding.ASCII.GetBytes(fragCode), CrossCompileTarget.GLSL).Reflection;
        Console.WriteLine($"{path}.{pass}.json");
        File.WriteAllText($"{path}.{pass}.json", JsonConvert.SerializeObject(compileResult));
    }
}
#endif
