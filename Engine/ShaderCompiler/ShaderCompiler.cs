#if SHADER_COMPILER
using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Assimp;
using Engine.Assets.Rendering;
using Newtonsoft.Json;
using Veldrid.SPIRV;

public class ShaderCompiler
{
    public static async Task Main(string[] args)
    {
        using AssimpContext ctx = new AssimpContext();
        foreach (var shader in args)
        {
            if (shader.Contains("glsl"))
            {
                var processor = new ShaderProcessor();
                processor.CreateShaders(shader, File.ReadAllText(shader), CreateShaders).Wait();
                Console.WriteLine($"Compiling {shader}.json");
                File.WriteAllText($"{shader}.json", JsonConvert.SerializeObject(processor.Passes));
            }
            else
            {
                // var scene = ctx.ImportFile(shader);
                ctx.ConvertFromFileToFile(shader, $"{shader}.gltf", "gltf2");
                // File.WriteAllText($"{shader}.json", JsonConvert.SerializeObject(scene, Formatting.Indented));
            }
        }
    }

    private static void CreateShaders(string vertCode, string fragCode, string pass, string path)
    {
        var compileResult = SpirvCompilation.CompileVertexFragment(Encoding.ASCII.GetBytes(vertCode), Encoding.ASCII.GetBytes(fragCode), CrossCompileTarget.ESSL);
        Console.WriteLine($"Compiling {path}.{pass}.json");
        File.WriteAllText($"{path}.{pass}.json", JsonConvert.SerializeObject(compileResult));
    }
}
#endif
