using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Engine.Assets.Models;
using Engine.Assets.Textures;
using Veldrid;
using Veldrid.SPIRV;

namespace Engine.Assets.Rendering
{
    public class GraphicsShader : Resource
    {
        public override bool IsValid => _shaders != null;
        private string _path;
        public Shader[] _shaders { get; private set; }
        public VertexFragmentCompilationResult _compileResult { get; private set; }
        public List<ResourceLayout> _reflResourceLayouts { get; private set; } = new List<ResourceLayout>();

        public GraphicsShader(string path) : this(path, path)
        {
        }

        public GraphicsShader(string name, string path)
        {
            Name = name;
            _path = path;
            ReCreate();
        }

        public override void ReCreate()
        {
            if (HasBeenInitialized)
                return;
            base.ReCreate();
            foreach (var res in _reflResourceLayouts)
            {
                if (res != null && !res.IsDisposed)
                    res.Dispose();
            }
            if (_shaders != null)
                for (int i = 0; i < _shaders.Length; i++)
                {
                    if (_shaders[i] != null && !_shaders[i].IsDisposed)
                        _shaders[i].Dispose();
                }
            string vertCode = FileManager.LoadStringASCII($"{_path}.vert");
            string fragCode = FileManager.LoadStringASCII($"{_path}.frag");
            CreateShaders(vertCode, fragCode);
            _reflResourceLayouts.Clear();
            for (int i = 0; i < _compileResult.Reflection.ResourceLayouts.Length; i++)
            {
                ResourceLayoutDescription desc = _compileResult.Reflection.ResourceLayouts[i];
                _reflResourceLayouts.Add(ResourceManager.GraphicsFactory.CreateResourceLayout(desc));
            }
        }

        public override Resource Clone(string cloneName)
        {
            GraphicsShader res = new GraphicsShader(cloneName, _path);
            return res;
        }

        private void CreateShaders(string vertCode, string fragCode)
        {
            ShaderDescription vertShader = new ShaderDescription(ShaderStages.Vertex, Encoding.ASCII.GetBytes(vertCode), "main");
            ShaderDescription fragShader = new ShaderDescription(ShaderStages.Fragment, Encoding.ASCII.GetBytes(fragCode), "main");
            _shaders = ResourceManager.GraphicsFactory.CreateFromSpirv(vertShader, fragShader, new CrossCompileOptions(Program.APIBackend == GraphicsBackend.OpenGL, Program.APIBackend == GraphicsBackend.Vulkan));
            for (int i = 0; i < _shaders.Length; i++)
            {
                _shaders[i].Name = Name + _shaders[i].Stage.ToString();
            }
            _compileResult = SpirvCompilation.CompileVertexFragment(vertShader.ShaderBytes, fragShader.ShaderBytes, GetCompilationTarget(Program.APIBackend));
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
            for (int i = 0; i < _shaders.Length; i++)
            {
                _shaders[i].Dispose();
            }
            _shaders = null;
        }
    }
}
