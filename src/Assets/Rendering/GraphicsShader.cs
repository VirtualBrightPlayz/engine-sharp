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
        public enum ShaderBlendFunction
        {
            Add = 0,
            Subtract = 1,
            ReverseSubtract = 2,
            Minimum = 3,
            Maximum = 4
        }

        public enum ShaderBlendFactor
        {
            Zero = 0,
            One = 1,
            SourceAlpha = 2,
            InverseSourceAlpha = 3,
            DestinationAlpha = 4,
            InverseDestinationAlpha = 5,
            SourceColor = 6,
            InverseSourceColor = 7,
            DestinationColor = 8,
            InverseDestinationColor = 9,
            BlendFactor = 10,
            InverseBlendFactor = 11
        }

        public enum ShaderComparisonKind
        {
            Never = 0,
            Less = 1,
            Equal = 2,
            LessEqual = 3,
            Greater = 4,
            NotEqual = 5,
            GreaterEqual = 6,
            Always = 7
        }

        public class ShaderPass
        {
            public string PassName;
            public bool DepthTest = true;
            public bool DepthWrite = true;
            public ShaderComparisonKind DepthCompare = ShaderComparisonKind.LessEqual;
            public bool BlendEnabled = true;
            public ShaderBlendFactor SrcColor = ShaderBlendFactor.One;
            public ShaderBlendFactor DestColor = ShaderBlendFactor.Zero;
            public ShaderBlendFunction ColorFunc = ShaderBlendFunction.Add;
            public ShaderBlendFactor SrcAlpha = ShaderBlendFactor.One;
            public ShaderBlendFactor DestAlpha = ShaderBlendFactor.Zero;
            public ShaderBlendFunction AlphaFunc = ShaderBlendFunction.Add;

            public BlendStateDescription GetBlendStateDescription()
            {
                return new BlendStateDescription(RgbaFloat.White, new BlendAttachmentDescription(BlendEnabled, (BlendFactor)SrcColor, (BlendFactor)DestColor, (BlendFunction)ColorFunc, (BlendFactor)SrcAlpha, (BlendFactor)DestAlpha, (BlendFunction)AlphaFunc));
            }

            public DepthStencilStateDescription GetDepthStencilStateDescription()
            {
                return new DepthStencilStateDescription(DepthTest, DepthWrite, (ComparisonKind)DepthCompare);
            }
        }

        public override bool IsValid => _shaders != null;
        private string _path;
        public Dictionary<string, Shader[]> _shaders { get; private set; } = new Dictionary<string, Shader[]>();
        public VertexFragmentCompilationResult _compileResult { get; private set; }
        public List<ResourceLayout> _reflResourceLayouts { get; private set; } = new List<ResourceLayout>();
        public List<ShaderPass> Passes { get; private set; } = new List<ShaderPass>();

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
                CreateShaders(shaderCode);
            }
            else
            {
                string vertCode = FileManager.LoadStringASCII($"{_path}.vert");
                string fragCode = FileManager.LoadStringASCII($"{_path}.frag");
                CreateShaders(vertCode, fragCode, "main");
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

        private void ProccessShaderCode(string file, string shaderCode, string curpass, ref Dictionary<string, string> defines, ref int fragInputs, ref List<string> passes, out string[] vertexCode, out string[] fragmentCode)
        {
            const string pragmaVert = "#pragma vertex ";
            const string pragmaFrag = "#pragma fragment ";
            const string pragmaVertIn = "#pragma in vertex ";
            const string pragmaFragIn = "#pragma in fragment ";
            const string pragmaIfVert = "#if vertex";
            const string pragmaIfFrag = "#if fragment";
            const string pragmaIf = "#if ";
            const string pragmaEndIf = "#endif";
            const string pragmaInclude = "#include ";
            const string pragmaDefine = "#define ";
            const string pragmaDepth = "#depth ";
            const string pragmaBlend = "#blend ";
            const string pragmaPass = "#pass ";
            // const string version = "#version 450";
            string filename = $"\"{file}\"";
            string[] lines = shaderCode.ReplaceLineEndings().Split(Environment.NewLine);
            List<string> vertexShaderOutput = new List<string>();
            List<string> fragmentShaderOutput = new List<string>();
            // vertexShaderOutput.Add(version);
            // fragmentShaderOutput.Add(version);
            // fragmentShaderOutput.Add("layout(location = 0) out vec4 fragColor;");
            string vertexMain = string.Empty;
            string fragmentMain = string.Empty;
            int curLine = 0;
            int vertInputs = 0;
            // int fragInputs = 0;
            int ifState = 0;
            for (int i = 0; i < lines.Length; i++)
            {
                string line = lines[i].Trim();
                foreach (var define in defines)
                {
                    line = line.Replace($"${define.Key}$", define.Value);
                }
                if (line.ToLower().StartsWith(pragmaPass))
                {
                    string passName = line.Substring(pragmaPass.Length - 1).Trim();
                    passes.Add(passName);
                }
                else if (line.ToLower().StartsWith(pragmaInclude))
                {
                    string inc = line.Substring(pragmaInclude.Length - 1).Trim();
                    int st = inc.IndexOf("\"");
                    int en = inc.LastIndexOf("\"");
                    if (st != -1 && en != -1)
                    {
                        string incFileName = inc.Substring(st + 1, (en + 1) - (st + 2));
                        string dir = Path.GetDirectoryName(file);
                        if (FileManager.Exists(Path.Combine(dir, incFileName)))
                        {
                            string incCode = FileManager.LoadStringASCII(Path.Combine(dir, incFileName));
                            ProccessShaderCode(Path.Combine(dir, incFileName), incCode, curpass, ref defines, ref fragInputs, ref passes, out string[] incVertCode, out string[] incFragCode);
                            vertexShaderOutput.AddRange(incVertCode);
                            fragmentShaderOutput.AddRange(incFragCode);
                        }
                        else
                            throw new FileNotFoundException($"[{file}:{curLine}] {Path.Combine(dir, incFileName)} does not exist");
                    }
                    else
                        throw new InvalidOperationException($"[{file}:{curLine}] #include missing '\"' at start and/or end");
                }
                else if (line.ToLower().StartsWith(pragmaDefine))
                {
                    string defineLine = line.Substring(pragmaDefine.Length - 1).Trim();
                    int spc = defineLine.IndexOf(' ');
                    if (spc != -1)
                    {
                        string defName = defineLine.Substring(0, spc);
                        string defValue = defineLine.Substring(spc + 1);
                        defines.Add(defName, defValue);
                    }
                }
                else if (line.ToLower().StartsWith(pragmaDepth))
                {
                    string depth = line.Substring(pragmaDepth.Length - 1).Trim();
                    string[] settings = depth.Split(' ');
                    const string depthErrorString = "#depth PassName TestEnabled WriteEnabled CompareKind";
                    if (settings.Length != 4)
                        throw new InvalidOperationException(depthErrorString);
                    string passName = settings[0];
                    if (!bool.TryParse(settings[1], out bool testEnabled))
                        throw new InvalidOperationException(depthErrorString);
                    if (!bool.TryParse(settings[2], out bool writeEnabled))
                        throw new InvalidOperationException(depthErrorString);
                    if (!Enum.TryParse<ShaderComparisonKind>(settings[3], true, out var compare))
                        throw new InvalidOperationException(depthErrorString);
                    ShaderPass pass = new ShaderPass();
                    pass.DepthTest = testEnabled;
                    pass.DepthWrite = writeEnabled;
                    pass.DepthCompare = compare;
                    int passIndex = Passes.FindIndex(x => x.PassName == passName);
                    if (passIndex != -1)
                    {
                        Passes[passIndex].DepthTest = testEnabled;
                        Passes[passIndex].DepthWrite = writeEnabled;
                        Passes[passIndex].DepthCompare = compare;
                    }
                    else
                        Passes.Add(pass);
                }
                else if (line.ToLower().StartsWith(pragmaBlend))
                {
                    string blend = line.Substring(pragmaBlend.Length - 1).Trim();
                    string[] settings = blend.Split(' ');
                    const string blendErrorString = "#blend PassName Enabled SrcColorFactor DestColorFactor ColorFunc SrcAlphaFactor DestAlphaFactor AlphaFunc";
                    if (settings.Length != 8)
                        throw new InvalidOperationException(blendErrorString);
                    string passName = settings[0];
                    if (!bool.TryParse(settings[1], out bool blendEnabled))
                        throw new InvalidOperationException(blendErrorString);
                    if (!Enum.TryParse<ShaderBlendFactor>(settings[2], true, out var srcColor))
                        throw new InvalidOperationException(blendErrorString);
                    if (!Enum.TryParse<ShaderBlendFactor>(settings[3], true, out var dstColor))
                        throw new InvalidOperationException(blendErrorString);
                    if (!Enum.TryParse<ShaderBlendFunction>(settings[4], true, out var colorFunc))
                        throw new InvalidOperationException(blendErrorString);
                    if (!Enum.TryParse<ShaderBlendFactor>(settings[5], true, out var srcAlpha))
                        throw new InvalidOperationException(blendErrorString);
                    if (!Enum.TryParse<ShaderBlendFactor>(settings[6], true, out var dstAlpha))
                        throw new InvalidOperationException(blendErrorString);
                    if (!Enum.TryParse<ShaderBlendFunction>(settings[7], true, out var alphaFunc))
                        throw new InvalidOperationException(blendErrorString);

                    ShaderPass pass = new ShaderPass();
                    pass.PassName = passName;
                    pass.BlendEnabled = blendEnabled;
                    pass.SrcColor = srcColor;
                    pass.DestColor = dstColor;
                    pass.ColorFunc = colorFunc;
                    pass.SrcAlpha = srcAlpha;
                    pass.DestAlpha = dstAlpha;
                    pass.AlphaFunc = alphaFunc;
                    int passIndex = Passes.FindIndex(x => x.PassName == passName);
                    if (passIndex != -1)
                    {
                        Passes[passIndex].BlendEnabled = blendEnabled;
                        Passes[passIndex].SrcColor = srcColor;
                        Passes[passIndex].DestColor = dstColor;
                        Passes[passIndex].ColorFunc = colorFunc;
                        Passes[passIndex].SrcAlpha = srcAlpha;
                        Passes[passIndex].DestAlpha = dstAlpha;
                        Passes[passIndex].AlphaFunc = alphaFunc;
                    }
                    else
                        Passes.Add(pass);
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
                    line = lines[i];
                    foreach (var define in defines)
                    {
                        line = line.Replace($"${define.Key}$", define.Value);
                    }
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
            vertexCode = vertexShaderOutput.ToArray();
            fragmentCode = fragmentShaderOutput.ToArray();
        }

        private void CreateShaders(string shaderCode)
        {
            const string version = "#version 450";
            List<string> passes = new List<string>();
            {
                List<string> vertexShaderOutput = new List<string>();
                List<string> fragmentShaderOutput = new List<string>();
                vertexShaderOutput.Add(version);
                fragmentShaderOutput.Add(version);
                fragmentShaderOutput.Add("layout(location = 0) out vec4 FragColor;");
                Dictionary<string, string> defines = new Dictionary<string, string>();
                int fragInputs = 0;
                ProccessShaderCode(_path, shaderCode, "main", ref defines, ref fragInputs, ref passes, out string[] vert, out string[] frag);
                vertexShaderOutput.AddRange(vert);
                fragmentShaderOutput.AddRange(frag);
                // CreateShaders(string.Join(Environment.NewLine, vertexShaderOutput), string.Join(Environment.NewLine, fragmentShaderOutput), "main");
                /*
                File.WriteAllLines("vert.dbg", vertexShaderOutput);
                File.WriteAllLines("frag.dbg", fragmentShaderOutput);
                */
            }
            foreach (var pass in passes)
            {
                List<string> vertexShaderOutput = new List<string>();
                List<string> fragmentShaderOutput = new List<string>();
                vertexShaderOutput.Add(version);
                fragmentShaderOutput.Add(version);
                fragmentShaderOutput.Add("layout(location = 0) out vec4 FragColor;");
                Dictionary<string, string> defines = new Dictionary<string, string>();
                foreach (var pass2 in passes)
                {
                    defines.Add(pass2, pass2 == pass ? "1" : "0");
                }
                int fragInputs = 0;
                List<string> pas = new List<string>();
                ProccessShaderCode(_path, shaderCode, pass, ref defines, ref fragInputs, ref pas, out string[] vert, out string[] frag);
                vertexShaderOutput.AddRange(vert);
                fragmentShaderOutput.AddRange(frag);
                CreateShaders(string.Join(Environment.NewLine, vertexShaderOutput), string.Join(Environment.NewLine, fragmentShaderOutput), pass);
            }
        }

        private void CreateShaders(string vertCode, string fragCode, string pass)
        {
            ShaderDescription vertShader = new ShaderDescription(ShaderStages.Vertex, Encoding.ASCII.GetBytes(vertCode), "main");
            ShaderDescription fragShader = new ShaderDescription(ShaderStages.Fragment, Encoding.ASCII.GetBytes(fragCode), "main");
            Shader[] shaders = ResourceManager.GraphicsFactory.CreateFromSpirv(vertShader, fragShader, new CrossCompileOptions(Program.APIBackend == GraphicsBackend.OpenGL, Program.APIBackend == GraphicsBackend.Vulkan));
            for (int i = 0; i < shaders.Length; i++)
            {
                shaders[i].Name = Name + shaders[i].Stage.ToString();
            }
            _compileResult = SpirvCompilation.CompileVertexFragment(vertShader.ShaderBytes, fragShader.ShaderBytes, GetCompilationTarget(Program.APIBackend));
            _shaders.Add(pass, shaders);
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
