using System.Threading.Tasks;
using System;
using System.Numerics;
using System.Text;
using Microsoft.JSInterop;
using Microsoft.JSInterop.WebAssembly;
using Veldrid;
using Engine.Assets.Rendering;
using Engine.Assets.Audio;
using Engine.Assets;
using Engine.Assets.Textures;

public class WebRuntime : WebAssemblyJSRuntime
{
}

public static class WebEntry
{
    struct VertexPositionColor
    {
        public Vector2 Position; // This is the position, in normalized device coordinates.
        public RgbaFloat Color; // This is the color of the vertex.
        public VertexPositionColor(Vector2 position, RgbaFloat color)
        {
            Position = position;
            Color = color;
        }
        public const uint SizeInBytes = 24;
    }

    private const string VertexCode = @"
#version 300 es

precision highp float;
precision highp int;

layout(location = 0) in vec2 Position;
layout(location = 1) in vec4 Color;

out vec4 fsin_Color;

void main()
{
    gl_Position = vec4(Position, 0, 1);
    fsin_Color = Color;
}";

    private const string FragmentCode = @"
#version 300 es

precision highp float;
precision highp int;

in vec4 fsin_Color;
layout(location = 0) out vec4 fsout_Color;

void main()
{
    fsout_Color = fsin_Color;
}";

    // [JSInvokable]
    public static void Frame2(double time)
    {
        _commandList.Begin();
        _commandList.SetFramebuffer(_graphicsDevice.SwapchainFramebuffer);
        _commandList.ClearColorTarget(0, RgbaFloat.Black);
        _commandList.SetVertexBuffer(0, _vertexBuffer);
        _commandList.SetIndexBuffer(_indexBuffer, IndexFormat.UInt16);
        _commandList.SetPipeline(_pipeline);
        _commandList.DrawIndexed(
            indexCount: 6,
            instanceCount: 1,
            indexStart: 0,
            vertexOffset: 0,
            instanceStart: 0);
        _commandList.End();
        _graphicsDevice.SubmitCommands(_commandList);
        _graphicsDevice.SwapBuffers();
    }

    public static GraphicsDevice _graphicsDevice;
    public static DeviceBuffer _vertexBuffer;
    public static DeviceBuffer _indexBuffer;
    public static Shader[] _shaders;
    public static Pipeline _pipeline;
    public static CommandList _commandList;


    public static GameBSrc.BGame game;
    public static double lastTime;
    public static Renderer renderer;
    public static WebRuntime runtime;

    public static async Task<int> Main(string[] args)
    {
        Console.WriteLine("Starting...");
        runtime = new WebRuntime();
        // FileManager.HttpCallback = HttpLoad;
        FileManager.HttpPrefix = runtime.Invoke<string>("getHttpPrefix");
        RenderingGlobals.InitGameGraphics(GraphicsBackend.OpenGLES);
        renderer = await ResourceManager.CreateRenderer("MainRenderer");
        Renderer.Current = renderer;
        renderer.SetRenderTarget(new RenderTexture2D("MainRenderTexture2D", RenderingGlobals.GameGraphics.MainSwapchain));
        AudioGlobals.InitGameAudio();
        MiscGlobals.InitGameMisc();
        game = new GameBSrc.BGame();
        game.Setup();
        ResourceManager.Update();
        runtime.InvokeVoid("init");
        return 0;
    }

    private static string HttpLoad(string arg)
    {
        return runtime.Invoke<string>("getHttpContent", arg);
    }

    [JSInvokable]
    public static void Frame(double time)
    {
        double delta = time - lastTime;
        ResourceManager.Update();
        RenderingGlobals.GameImGui.Update((float)delta, MiscGlobals.GameInputSnapshot);
        renderer.ProjectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(70f * (MathF.PI / 180f), (float)renderer.InternalRenderTexture.Width / renderer.InternalRenderTexture.Height, 0.1f, 1000f);
        game.PreDraw(renderer, delta);
        renderer.Begin();
        renderer.Clear();
        game.Draw(renderer, delta);
        RenderingGlobals.GameImGui.Render(RenderingGlobals.GameGraphics, renderer.CommandList);
        renderer.End();
        renderer.Submit();
        RenderingGlobals.GameGraphics.SwapBuffers();
        MiscGlobals.GameInputSnapshot.Update();
        game.Tick(delta);
        lastTime = time;
    }

    public static unsafe int Main2(string[] args)
    {
        Console.WriteLine("Starting...");

        using var runtime = new WebRuntime();

        var desc = new SwapchainDescription(SwapchainSource.CreateWeb(), 600, 400, Veldrid.PixelFormat.R32_Float, true);
        _graphicsDevice = GraphicsDevice.CreateOpenGLES(new GraphicsDeviceOptions()
        {
            SingleThreaded = true,
        }, desc);

        ResourceFactory factory = _graphicsDevice.ResourceFactory;
        VertexPositionColor[] quadVertices =
        {
            new VertexPositionColor(new Vector2(-0.75f, 0.75f), RgbaFloat.Red),
            new VertexPositionColor(new Vector2(0.75f, 0.75f), RgbaFloat.Green),
            new VertexPositionColor(new Vector2(-0.75f, -0.75f), RgbaFloat.Blue),
            new VertexPositionColor(new Vector2(0.75f, -0.75f), RgbaFloat.Yellow)
        };
        ushort[] quadIndices = { 0, 1, 2, 3, 2, 1 };
        _vertexBuffer = factory.CreateBuffer(new BufferDescription(4 * VertexPositionColor.SizeInBytes, BufferUsage.VertexBuffer));
        _indexBuffer = factory.CreateBuffer(new BufferDescription(6 * sizeof(ushort), BufferUsage.IndexBuffer));
        _graphicsDevice.UpdateBuffer(_vertexBuffer, 0, quadVertices);
        _graphicsDevice.UpdateBuffer(_indexBuffer, 0, quadIndices);
        VertexLayoutDescription vertexLayout = new VertexLayoutDescription(
            new VertexElementDescription("Position", VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float2),
            new VertexElementDescription("Color", VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float4));
        ShaderDescription vertexShaderDesc = new ShaderDescription(
            ShaderStages.Vertex,
            Encoding.UTF8.GetBytes(VertexCode.Trim()),
            "main");
        ShaderDescription fragmentShaderDesc = new ShaderDescription(
            ShaderStages.Fragment,
            Encoding.UTF8.GetBytes(FragmentCode.Trim()),
            "main");

        _shaders = new Shader[2];
        _shaders[0] = factory.CreateShader(vertexShaderDesc);
        _shaders[1] = factory.CreateShader(fragmentShaderDesc);

        GraphicsPipelineDescription pipelineDescription = new GraphicsPipelineDescription();
        pipelineDescription.BlendState = BlendStateDescription.SingleOverrideBlend;
        pipelineDescription.DepthStencilState = new DepthStencilStateDescription(
            depthTestEnabled: true,
            depthWriteEnabled: true,
            comparisonKind: ComparisonKind.LessEqual);
        pipelineDescription.RasterizerState = new RasterizerStateDescription(
            cullMode: FaceCullMode.Back,
            fillMode: PolygonFillMode.Solid,
            frontFace: FrontFace.Clockwise,
            depthClipEnabled: true,
            scissorTestEnabled: false);
        pipelineDescription.PrimitiveTopology = PrimitiveTopology.TriangleList;
        pipelineDescription.ResourceLayouts = System.Array.Empty<ResourceLayout>();
        pipelineDescription.ShaderSet = new ShaderSetDescription(
            vertexLayouts: new VertexLayoutDescription[] { vertexLayout },
            shaders: _shaders);
        pipelineDescription.Outputs = _graphicsDevice.SwapchainFramebuffer.OutputDescription;
        _pipeline = factory.CreateGraphicsPipeline(pipelineDescription);

        _commandList = factory.CreateCommandList();

        runtime.InvokeVoid("init");
        return 0;
    }
}