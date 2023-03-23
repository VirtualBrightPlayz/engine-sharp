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

        public GraphicsShader(string path) : this(path, path)
        {
        }

        public GraphicsShader(string name, string path) : base(name)
        {
            _path = path;
            ReCreate();
        }

        protected override void ReCreateInternal()
        {
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

            if (FileManager.Exists($"{_path}.glsl"))
            {
                string shaderCode = FileManager.LoadStringASCII($"{_path}.glsl");
                var processor = new ShaderProcessor();
                processor.CreateShaders(_path, shaderCode, CreateShaders);
                Passes = processor.Passes.ToList();
            }
            else
            {
                string vertCode = FileManager.LoadStringASCII($"{_path}.vert");
                string fragCode = FileManager.LoadStringASCII($"{_path}.frag");
                CreateShaders(vertCode, fragCode, "main", _path);
            }

            _reflResourceLayouts.Clear();
            for (int i = 0; i < _compileResult.ResourceLayouts.Length; i++)
            {
                ResourceLayoutDescription desc = _compileResult.ResourceLayouts[i];
                _reflResourceLayouts.Add(ResourceManager.GraphicsFactory.CreateResourceLayout(desc));
            }
        }

        protected override Resource CloneInternal(string cloneName)
        {
            throw new Exception("Can't clone shaders");
        }

        public bool HasSet(string name)
        {
            for (int i = 0; i < _compileResult.ResourceLayouts.Length; i++)
            {
                if (_compileResult.ResourceLayouts[i].Elements.Any(x => x.Name == name))
                    return true;
            }
            return false;
        }

        public bool HasSet(uint index)
        {
            return _reflResourceLayouts.Count > index;
        }

        public int GetSetIndex(string name)
        {
            for (int i = 0; i < _compileResult.ResourceLayouts.Length; i++)
            {
                if (_compileResult.ResourceLayouts[i].Elements.Any(x => x.Name == name))
                    return i;
            }
            return -1;
        }

        public string[] GetSetNames(uint index)
        {
            if (index < _compileResult.ResourceLayouts.Length)
                return _compileResult.ResourceLayouts[(int)index].Elements.Select(x => x.Name).ToArray();
            // if (index < _reflResourceLayouts.Count)
                // return _reflResourceLayouts[(int)index].Name;
            return new string[0];
        }

        private void CreateShaders(string vertCode, string fragCode, string pass, string pa)
        {
            ShaderDescription vertShader = new ShaderDescription(ShaderStages.Vertex, Encoding.ASCII.GetBytes(vertCode), "main");
            ShaderDescription fragShader = new ShaderDescription(ShaderStages.Fragment, Encoding.ASCII.GetBytes(fragCode), "main");
            Shader[] shaders = ResourceManager.GraphicsFactory.CreateFromSpirv(vertShader, fragShader, new CrossCompileOptions(RenderingGlobals.APIBackend == GraphicsBackend.OpenGL, RenderingGlobals.APIBackend == GraphicsBackend.Vulkan));
            for (int i = 0; i < shaders.Length; i++)
            {
                shaders[i].Name = Name + shaders[i].Stage.ToString();
            }
            if (pass == "main")
                _compileResult = SpirvCompilation.CompileVertexFragment(vertShader.ShaderBytes, fragShader.ShaderBytes, GetCompilationTarget(RenderingGlobals.APIBackend)).Reflection;
            else
                SpirvCompilation.CompileVertexFragment(vertShader.ShaderBytes, fragShader.ShaderBytes, GetCompilationTarget(RenderingGlobals.APIBackend));
            _shaders.Add(pass, shaders);
        }

        private static CrossCompileTarget GetCompilationTarget(GraphicsBackend backend)
        {
            switch (backend)
            {
                case GraphicsBackend.Direct3D11:
                    // return CrossCompileTarget.HLSL;
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

        protected override void DisposeInternal()
        {
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
        }
    }
}
