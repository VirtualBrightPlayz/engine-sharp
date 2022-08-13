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
            if (FileManager.Exists($"{_path}.glsl"))
            {
                string shaderCode = FileManager.LoadStringASCII($"{_path}.glsl");
                CreateShaders(shaderCode);
            }
            else
            {
                string vertCode = FileManager.LoadStringASCII($"{_path}.vert");
                string fragCode = FileManager.LoadStringASCII($"{_path}.frag");
                CreateShaders(vertCode, fragCode, "main", "main");
            }
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

        private void CreateShaders(string shaderCode)
        {
            const string pragmaVert = "#pragma vertex ";
            const string pragmaFrag = "#pragma fragment ";
            const string pragmaVertIn = "#pragma in vertex ";
            const string pragmaFragIn = "#pragma in fragment ";
            const string pragmaIfVert = "#if vertex";
            const string pragmaIfFrag = "#if fragment";
            const string pragmaEndIf = "#endif";
            const string version = "#version 450";
            string filename = $"\"{Name}\"";
            string[] lines = shaderCode.ReplaceLineEndings().Split(Environment.NewLine);
            List<string> vertexShaderOutput = new List<string>();
            List<string> fragmentShaderOutput = new List<string>();
            vertexShaderOutput.Add(version);
            fragmentShaderOutput.Add(version);
            fragmentShaderOutput.Add("layout(location = 0) out vec4 fragColor;");
            string vertexMain = string.Empty;
            string fragmentMain = string.Empty;
            int curLine = 0;
            int vertInputs = 0;
            int fragInputs = 0;
            int ifState = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                if (line.ToLower().StartsWith("#include "))
                {
                    // TODO
                }
                else if (line.ToLower().StartsWith(pragmaVert))
                {
                    vertexMain = line.Substring(pragmaVert.Length - 1).Trim();
                }
                else if (line.ToLower().StartsWith(pragmaFrag))
                {
                    fragmentMain = line.Substring(pragmaFrag.Length - 1).Trim();
                }
                else if (line.ToLower().StartsWith(pragmaEndIf))
                {
                    ifState = 0;
                }
                else if (line.ToLower().StartsWith(pragmaIfVert))
                {
                    ifState = 1;
                }
                else if (line.ToLower().StartsWith(pragmaIfFrag))
                {
                    ifState = 2;
                }
                else if (line.ToLower().StartsWith(pragmaVertIn))
                {
                    string vertexInput = line.Substring(pragmaVertIn.Length - 1).Trim();
                    vertexShaderOutput.Add($"#line {curLine} {filename}");
                    vertexShaderOutput.Add($"layout(location = {vertInputs}) in {vertexInput};");
                    vertInputs++;
                }
                else if (line.ToLower().StartsWith(pragmaFragIn))
                {
                    string fragmentInput = line.Substring(pragmaFragIn.Length - 1).Trim();
                    vertexShaderOutput.Add($"#line {curLine} {filename}");
                    vertexShaderOutput.Add($"layout(location = {fragInputs}) out {fragmentInput};");
                    fragmentShaderOutput.Add($"#line {curLine} {filename}");
                    fragmentShaderOutput.Add($"layout(location = {fragInputs}) in {fragmentInput};");
                    fragInputs++;
                }
                else if (!line.StartsWith("#"))
                {
                    switch (ifState)
                    {
                        case 0:
                            vertexShaderOutput.Add($"#line {curLine} {filename}");
                            vertexShaderOutput.Add(line);
                            fragmentShaderOutput.Add($"#line {curLine} {filename}");
                            fragmentShaderOutput.Add(line);
                            break;
                        case 1:
                            vertexShaderOutput.Add($"#line {curLine} {filename}");
                            vertexShaderOutput.Add(line);
                            break;
                        case 2:
                            fragmentShaderOutput.Add($"#line {curLine} {filename}");
                            fragmentShaderOutput.Add(line);
                            break;
                    }
                }
                curLine++;
            }
            /*File.WriteAllLines("vert.dbg", vertexShaderOutput);
            File.WriteAllLines("frag.dbg", fragmentShaderOutput);*/
            CreateShaders(string.Join(Environment.NewLine, vertexShaderOutput), string.Join(Environment.NewLine, fragmentShaderOutput), vertexMain, fragmentMain);
        }

        private void CreateShaders(string vertCode, string fragCode, string vertexMain, string fragmentMain)
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
