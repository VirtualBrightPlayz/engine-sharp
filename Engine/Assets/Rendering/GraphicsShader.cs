using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Engine.Assets.Models;
using Engine.Assets.Textures;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Veldrid;
using Veldrid.SPIRV;

namespace Engine.Assets.Rendering
{
    public class GraphicsShader : Resource
    {
        public override bool IsValid => _shaders != null;
        private string _path;
        public Dictionary<string, Shader[]> _shaders { get; private set; } = new Dictionary<string, Shader[]>();
        public SpirvReflection _compileResult { get; private set; }
        public List<ResourceLayout> _reflResourceLayouts { get; private set; } = new List<ResourceLayout>();
        public List<ShaderPass> Passes { get; private set; } = new List<ShaderPass>();
        private Dictionary<string, string> defines;
        private int fragInputs;
        private List<string> passes;
        private string[] vertexCode;
        private string[] fragmentCode;

        public GraphicsShader(string path) : this(path, path)
        {
        }

        public GraphicsShader(string name, string path)
        {
            Name = name;
            _path = path;
        }

        public override async Task ReCreate()
        {
            if (HasBeenInitialized)
                return;
            await base.ReCreate();
            foreach (var res in _reflResourceLayouts)
            {
                if (res != null && !res.IsDisposed)
                    res.Dispose();
            }
            foreach (var shaderSet in _shaders)
            {
                if (shaderSet.Value != null)
                {
                    for (int i = 0; i < shaderSet.Value.Length; i++)
                    {
                        if (shaderSet.Value[i] != null && !shaderSet.Value[i].IsDisposed)
                            shaderSet.Value[i].Dispose();
                    }
                }
            }
            _shaders.Clear();
        #if WEBGL
            string manifest = await FileManager.LoadString($"{_path}.glsl.json");
            Passes = JsonConvert.DeserializeObject<List<ShaderPass>>(manifest);
            foreach (var pass in Passes)
            {
                string passManifest = await FileManager.LoadString($"{_path}.glsl.{pass.PassName}.json");
                object result = JsonConvert.DeserializeObject(passManifest);
                JObject lin = JObject.FromObject(result);
                ShaderDescription vertShader = new ShaderDescription(ShaderStages.Vertex, Encoding.ASCII.GetBytes(lin["VertexShader"].ToObject<string>()), "main");
                ShaderDescription fragShader = new ShaderDescription(ShaderStages.Fragment, Encoding.ASCII.GetBytes(lin["FragmentShader"].ToObject<string>()), "main");
                Shader[] shaders = new Shader[2];
                shaders[0] = ResourceManager.GraphicsFactory.CreateShader(vertShader);
                shaders[1] = ResourceManager.GraphicsFactory.CreateShader(fragShader);
                _shaders.Add(pass.PassName, shaders);
                _compileResult = lin["Reflection"].ToObject<SpirvReflection>();
                string json = lin["Reflection"].ToString(Formatting.None);
                using var ms = new MemoryStream(Encoding.UTF8.GetBytes(json));
                _compileResult = SpirvReflection.LoadFromJson(ms);
            }
            // _compileResult = SpirvReflection.LoadFromJson(await FileManager.LoadStream(_path + ".glsl.main.json"));
        #else
            if (await FileManager.Exists($"{_path}.glsl"))
            {
                string shaderCode = await FileManager.LoadStringASCII($"{_path}.glsl");
                var processor = new ShaderProcessor();
                await processor.CreateShaders(_path, shaderCode, CreateShaders);
            }
            else
            {
                string vertCode = await FileManager.LoadStringASCII($"{_path}.vert");
                string fragCode = await FileManager.LoadStringASCII($"{_path}.frag");
                CreateShaders(vertCode, fragCode, "main", _path);
            }
        #endif
            _reflResourceLayouts.Clear();
            for (int i = 0; i < _compileResult.ResourceLayouts.Length; i++)
            {
                ResourceLayoutDescription desc = _compileResult.ResourceLayouts[i];
                _reflResourceLayouts.Add(ResourceManager.GraphicsFactory.CreateResourceLayout(desc));
            }
        }

        public override Task<Resource> Clone(string cloneName)
        {
            GraphicsShader res = new GraphicsShader(cloneName, _path);
            return Task.FromResult<Resource>(res);
        }

        public bool HasSet(uint index)
        {
            return _reflResourceLayouts.Count > index;
        }

        private async void CreateShaders(string vertCode, string fragCode, string pass, string pa)
        {
        #if WEBGL
            Console.WriteLine(vertCode);
            Console.WriteLine(fragCode);
            Console.WriteLine(pass);
            Console.WriteLine(pa);
            ShaderDescription vertShader = new ShaderDescription(ShaderStages.Vertex, Encoding.ASCII.GetBytes(vertCode), "main");
            ShaderDescription fragShader = new ShaderDescription(ShaderStages.Fragment, Encoding.ASCII.GetBytes(fragCode), "main");
            Shader[] shaders = new Shader[2];
            shaders[0] = ResourceManager.GraphicsFactory.CreateShader(vertShader);
            shaders[1] = ResourceManager.GraphicsFactory.CreateShader(fragShader);
            _shaders.Add(pass, shaders);
            _compileResult = SpirvReflection.LoadFromJson(await FileManager.LoadStream(pa + ".glsl.main.json"));
        #else
            ShaderDescription vertShader = new ShaderDescription(ShaderStages.Vertex, Encoding.ASCII.GetBytes(vertCode), "main");
            ShaderDescription fragShader = new ShaderDescription(ShaderStages.Fragment, Encoding.ASCII.GetBytes(fragCode), "main");
            Shader[] shaders = ResourceManager.GraphicsFactory.CreateFromSpirv(vertShader, fragShader, new CrossCompileOptions(RenderingGlobals.APIBackend == GraphicsBackend.OpenGL, RenderingGlobals.APIBackend == GraphicsBackend.Vulkan));
            for (int i = 0; i < shaders.Length; i++)
            {
                shaders[i].Name = Name + shaders[i].Stage.ToString();
            }
            _compileResult = SpirvCompilation.CompileVertexFragment(vertShader.ShaderBytes, fragShader.ShaderBytes, GetCompilationTarget(RenderingGlobals.APIBackend)).Reflection;
            _shaders.Add(pass, shaders);
        #endif
        }

        private static CrossCompileTarget GetCompilationTarget(GraphicsBackend backend)
        {
            switch (backend)
            {
                case GraphicsBackend.Direct3D11:
                    return CrossCompileTarget.HLSL;
                case GraphicsBackend.Vulkan:
                case GraphicsBackend.OpenGL:
                    return CrossCompileTarget.GLSL;
                case GraphicsBackend.Metal:
                    return CrossCompileTarget.MSL;
                case GraphicsBackend.OpenGLES:
                    return CrossCompileTarget.ESSL;
                default:
                    throw new SpirvCompilationException($"Invalid GraphicsBackend: {backend}");
            }
        }

        public override void Dispose()
        {
            foreach (var shaderSet in _shaders)
            {
                if (shaderSet.Value != null)
                {
                    for (int i = 0; i < shaderSet.Value.Length; i++)
                    {
                        if (shaderSet.Value[i] != null && !shaderSet.Value[i].IsDisposed)
                            shaderSet.Value[i].Dispose();
                    }
                }
            }
            _shaders.Clear();
        }
    }
}
