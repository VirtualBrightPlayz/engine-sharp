using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Veldrid;

namespace Engine.Assets.Rendering
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

    public enum ShaderCullMode
    {
        Back = 0,
        Front = 1,
        None = 2,
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
        public ShaderCullMode CullMode = ShaderCullMode.Back;

        public BlendStateDescription GetBlendStateDescription()
        {
            return new BlendStateDescription(RgbaFloat.White, new BlendAttachmentDescription(BlendEnabled, (BlendFactor)SrcColor, (BlendFactor)DestColor, (BlendFunction)ColorFunc, (BlendFactor)SrcAlpha, (BlendFactor)DestAlpha, (BlendFunction)AlphaFunc));
        }

        public DepthStencilStateDescription GetDepthStencilStateDescription()
        {
            return new DepthStencilStateDescription(DepthTest, DepthWrite, (ComparisonKind)DepthCompare);
        }
    }

    public class ShaderProcessor
    {
        public List<ShaderPass> Passes { get; private set; } = new List<ShaderPass>();
        private Dictionary<string, string> defines;
        private int fragInputs;
        private int uniformInputs;
        private List<string> passes;
        private string[] vertexCode;
        private string[] fragmentCode;

        public async Task CreateShaders(string _path, string shaderCode, Func<string, string, string, string, Task> callback)
        {
        #if WEBGL
            const string version = "#version 300 es";
        #else
            const string version = "#version 450";
        #endif
            passes = new List<string>();
            {
                List<string> vertexShaderOutput = new List<string>();
                List<string> fragmentShaderOutput = new List<string>();
                vertexShaderOutput.Add(version);
                fragmentShaderOutput.Add(version);
            #if WEBGL
                vertexShaderOutput.Add("precision highp float;");
                fragmentShaderOutput.Add("precision highp float;");
                vertexShaderOutput.Add("precision highp int;");
                fragmentShaderOutput.Add("precision highp int;");
                // vertexShaderOutput.Add("precision highp texture2D;");
                // fragmentShaderOutput.Add("precision highp texture2D;");
                // vertexShaderOutput.Add("precision highp sampler2D;");
                // fragmentShaderOutput.Add("precision highp sampler2D;");
                fragmentShaderOutput.Add("out vec4 FragColor;");
            #else
                fragmentShaderOutput.Add("layout(location = 0) out vec4 FragColor;");
            #endif
                defines = new Dictionary<string, string>();
                fragInputs = 0;
                uniformInputs = 0;
                (string[] vert, string[] frag) = await ProccessShaderCode(_path, shaderCode, "main");
                vertexShaderOutput.AddRange(vert);
                fragmentShaderOutput.AddRange(frag);
                // CreateShaders(string.Join(Environment.NewLine, vertexShaderOutput), string.Join(Environment.NewLine, fragmentShaderOutput), "main");
                /*
                File.WriteAllLines("vert.dbg", vertexShaderOutput);
                File.WriteAllLines("frag.dbg", fragmentShaderOutput);
                */
            }
            foreach (var pass in passes.ToArray())
            {
                List<string> vertexShaderOutput = new List<string>();
                List<string> fragmentShaderOutput = new List<string>();
                vertexShaderOutput.Add(version);
                fragmentShaderOutput.Add(version);
            #if WEBGL
                vertexShaderOutput.Add("precision highp float;");
                fragmentShaderOutput.Add("precision highp float;");
                vertexShaderOutput.Add("precision highp int;");
                fragmentShaderOutput.Add("precision highp int;");
                // vertexShaderOutput.Add("precision highp texture2D;");
                // fragmentShaderOutput.Add("precision highp texture2D;");
                // vertexShaderOutput.Add("precision highp sampler2D;");
                // fragmentShaderOutput.Add("precision highp sampler2D;");
                fragmentShaderOutput.Add("out vec4 FragColor;");
            #else
                fragmentShaderOutput.Add("layout(location = 0) out vec4 FragColor;");
            #endif
                defines = new Dictionary<string, string>();
                foreach (var pass2 in passes.ToArray())
                {
                    defines.Add(pass2, pass2 == pass ? "1" : "0");
                }
                fragInputs = 0;
                uniformInputs = 0;
                List<string> pas = new List<string>();
                (string[] vert, string[] frag) = await ProccessShaderCode(_path, shaderCode, pass);
                vertexShaderOutput.AddRange(vert);
                fragmentShaderOutput.AddRange(frag);
                await callback?.Invoke(string.Join(Environment.NewLine, vertexShaderOutput), string.Join(Environment.NewLine, fragmentShaderOutput), pass, _path);
            }
        }

        private async Task<(string[], string[])> ProccessShaderCode(string file, string shaderCode, string curpass)
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
            const string pragmaDraw = "#draw ";
            const string pragmaPass = "#pass ";
            const string pragmaUniform = "#uniform ";
            const string pragmaTextureUniform = "#texture2d ";
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
                    if (!passes.Contains(passName))
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
                        if (await FileManager.Exists(Path.Combine(dir, incFileName)))
                        {
                            string incCode = await FileManager.LoadStringASCII(Path.Combine(dir, incFileName));
                            (string[] incVertCode, string[] incFragCode) = await ProccessShaderCode(Path.Combine(dir, incFileName), incCode, curpass);
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
                else if (line.ToLower().StartsWith(pragmaDraw))
                {
                    string depth = line.Substring(pragmaDraw.Length - 1).Trim();
                    string[] settings = depth.Split(' ');
                    const string depthErrorString = "#draw PassName Cull";
                    if (settings.Length != 2)
                        throw new InvalidOperationException(depthErrorString);
                    string passName = settings[0];
                    if (!Enum.TryParse<ShaderCullMode>(settings[1], true, out var compare))
                        throw new InvalidOperationException(depthErrorString);
                    ShaderPass pass = new ShaderPass();
                    pass.CullMode = compare;
                    int passIndex = Passes.FindIndex(x => x.PassName == passName);
                    if (passIndex != -1)
                    {
                        Passes[passIndex].CullMode = compare;
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
                #if WEBGL
                    vertexShaderOutput.Add($"layout(location = {vertInputs}) in {vertexInput};");
                #else
                    vertexShaderOutput.Add($"#line {curLine} {filename}");
                    vertexShaderOutput.Add($"layout(location = {vertInputs}) in {vertexInput};");
                #endif
                    vertInputs++;
                }
                else if (line.ToLower().StartsWith(pragmaFragIn))
                {
                    string fragmentInput = line.Substring(pragmaFragIn.Length - 1).Trim();
                #if WEBGL
                    // vertexShaderOutput.Add($"#line {curLine} {filename}");
                    vertexShaderOutput.Add($"out {fragmentInput};");
                    // fragmentShaderOutput.Add($"#line {curLine} {filename}");
                    fragmentShaderOutput.Add($"in {fragmentInput};");
                #else
                    vertexShaderOutput.Add($"#line {curLine} {filename}");
                    vertexShaderOutput.Add($"layout(location = {fragInputs}) out {fragmentInput};");
                    fragmentShaderOutput.Add($"#line {curLine} {filename}");
                    fragmentShaderOutput.Add($"layout(location = {fragInputs}) in {fragmentInput};");
                #endif
                    fragInputs++;
                }
                else if (line.ToLower().StartsWith(pragmaUniform))
                {
                    string fragmentInput = line.Substring(pragmaUniform.Length - 1).Trim();
                    string[] settings = fragmentInput.Split(' ');
                #if WEBGL
                    // vertexShaderOutput.Add($"#line {curLine} {filename}");
                    vertexShaderOutput.Add($"uniform {string.Join(' ', settings[0..^2])}");
                    // fragmentShaderOutput.Add($"#line {curLine} {filename}");
                    fragmentShaderOutput.Add($"uniform {string.Join(' ', settings[0..^2])}");
                    // vertexShaderOutput.Add($"layout(set = {settings[^2]}, binding = {settings[^1]}) uniform {string.Join(' ', settings[0..^2])}");
                    // fragmentShaderOutput.Add($"layout(set = {settings[^2]}, binding = {settings[^1]}) uniform {string.Join(' ', settings[0..^2])}");
                #else
                    vertexShaderOutput.Add($"#line {curLine} {filename}");
                    vertexShaderOutput.Add($"layout(set = {settings[^2]}, binding = {settings[^1]}) uniform {string.Join(' ', settings[0..^2])}");
                    fragmentShaderOutput.Add($"#line {curLine} {filename}");
                    fragmentShaderOutput.Add($"layout(set = {settings[^2]}, binding = {settings[^1]}) uniform {string.Join(' ', settings[0..^2])}");
                #endif
                    uniformInputs++;
                }
                /*else if (line.ToLower().StartsWith(pragmaTextureUniform))
                {
                    string fragmentInput = line.Substring(pragmaTextureUniform.Length - 1).Trim();
                    string[] settings = fragmentInput.Split(' ');
                #if WEBGL
                    // vertexShaderOutput.Add($"#line {curLine} {filename}");
                    vertexShaderOutput.Add($"uniform {string.Join(' ', settings[0..^2])};");
                    // fragmentShaderOutput.Add($"#line {curLine} {filename}");
                    fragmentShaderOutput.Add($"uniform {string.Join(' ', settings[0..^2])};");
                #else
                    vertexShaderOutput.Add($"#line {curLine} {filename}");
                    vertexShaderOutput.Add($"layout(set = {settings[^2]}, binding = {settings[^1]}) uniform {string.Join(' ', settings[0..^2])}");
                    fragmentShaderOutput.Add($"#line {curLine} {filename}");
                    fragmentShaderOutput.Add($"layout(set = {settings[^2]}, binding = {settings[^1]}) uniform {string.Join(' ', settings[0..^2])}");
                #endif
                    uniformInputs++;
                }*/
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
                        #if !WEBGL
                            vertexShaderOutput.Add($"#line {curLine} {filename}");
                            fragmentShaderOutput.Add($"#line {curLine} {filename}");
                        #endif
                            vertexShaderOutput.Add(line);
                            fragmentShaderOutput.Add(line);
                            break;
                        case 1:
                        #if !WEBGL
                            vertexShaderOutput.Add($"#line {curLine} {filename}");
                        #endif
                            vertexShaderOutput.Add(line);
                            break;
                        case 2:
                        #if !WEBGL
                            fragmentShaderOutput.Add($"#line {curLine} {filename}");
                        #endif
                            fragmentShaderOutput.Add(line);
                            break;
                    }
                }
                curLine++;
            }
            return (vertexShaderOutput.ToArray(), fragmentShaderOutput.ToArray());
            // vertexCode = vertexShaderOutput.ToArray();
            // fragmentCode = fragmentShaderOutput.ToArray();
        }
    }
}