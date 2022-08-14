using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Numerics;
using Engine.Assets;
using Engine.Assets.Models;
using Engine.Assets.Rendering;
using Engine.Assets.Textures;
using Engine.Game;
using Engine.VeldridSilk;
using ImGuiNET;
using Silk.NET.Input;
using Silk.NET.Maths;
using Silk.NET.OpenAL;
using Silk.NET.Windowing;
using Silk.NET.Windowing.Extensions.Veldrid;
using Veldrid;

namespace Engine
{
    public static class Program
    {
        public static GameApp Game { get; private set; }
        public static GraphicsBackend APIBackend { get; private set; }
        public static IWindow GameWindow { get; private set; }
        public static GraphicsDevice GameGraphics { get; private set; }
        public static AL GameAudio { get; private set; }
        public static ALContext GameAudioContext { get; private set; }
        private static unsafe Device* GameAudioDevice;
        private static unsafe Context* GameAudioCtx;
        public static IInputContext GameInputContext { get; private set; }
        public static InputHandler GameInputSnapshotHandler { get; private set; }
        public static ImGuiRenderer GameImGui { get; private set; }
        public static Renderer MainRenderer { get; private set; }
        public static GraphicsShader MainMeshShader { get; private set; }
        public static double Time { get; private set; }
        public static RenderDoc RenderDocInstance { get; set; }
        public static GraphicsBackend? NextFrameBackend { get; set; }
        public static bool ReCreateAllNextFrame { get; set; } = false;
        public static bool IsClosing { get; set; } = false;
        public static double FPS { get; private set; }
        public static bool IsFocused { get; private set; }

        public static int Main(string[] args)
        {
            Game = new GameSrc.SCPCB();

            ChangeBackend(GraphicsBackend.Vulkan, false, true);
            GameWindow.Initialize();
            MainRenderer = ResourceManager.CreateRenderer(nameof(MainRenderer));
            Renderer.Current = MainRenderer;
            MainRenderer.SetRenderTarget(new RenderTexture2D("MainRenderTexture2D", GameGraphics.MainSwapchain));
            Game.Setup();
            while (!IsClosing)
            {
                if (!GameWindow.IsInitialized)
                    GameWindow.Initialize();
                while (!GameWindow.IsClosing && !IsClosing)
                {
                    GameWindow.DoEvents();
                    if (!GameWindow.IsClosing)
                    {
                        GameWindow.DoUpdate();
                    }
                    if (!GameWindow.IsClosing)
                    {
                        GameWindow.DoRender();
                    }
                    if (NextFrameBackend.HasValue)
                        ChangeBackend(NextFrameBackend.Value, true, true);
                    else if (ReCreateAllNextFrame)
                        ResourceManager.ReCreateAll();
                    ReCreateAllNextFrame = false;
                }
                GameWindow.DoEvents();
                GameWindow.Reset();
                if (NextFrameBackend.HasValue)
                {
                    GameWindow.Dispose();
                    ChangeBackend(NextFrameBackend.Value, false, true);
                }
                else
                    break;
            }
            return 0;
        }

        private static unsafe void ChangeBackend(GraphicsBackend backend, bool close, bool destoryWindow)
        {
            APIBackend = backend;
            if (close)
            {
                if (GameInputSnapshotHandler != null)
                    GameInputSnapshotHandler.Dispose();
                if (GameImGui != null)
                    GameImGui.Dispose();
                if (GameGraphics != null)
                {
                    GameGraphics.DisposeWhenIdle(GameWindow);
                    GameGraphics = null;
                }
                if (GameWindow != null && destoryWindow)
                {
                    GameWindow.Load -= Load;
                    GameWindow.Closing -= Unload;
                    GameWindow.Resize -= Resize;
                    GameWindow.Render -= Frame;
                    GameWindow.Update -= Update;
                    GameWindow.FocusChanged -= FocusChange;
                    GameWindow.Close();
                }
            }
            else
            {
                NextFrameBackend = null;
                if (destoryWindow)
                {
                    WindowOptions options = new WindowOptions();
                    options.Title = $"{Game.Name} <{backend}>";
                    options.API = backend.ToGraphicsAPI();
                    options.WindowState = Silk.NET.Windowing.WindowState.Maximized;
                    options.IsVisible = true;
                    options.Size = Monitor.GetMainMonitor(null).Bounds.Size;
                    options.UpdatesPerSecond = 60;
                    options.PreferredDepthBufferBits = 32;
                    options.FramesPerSecond = Monitor.GetMainMonitor(null).VideoMode.RefreshRate ?? 60;
                    if (GameAudioContext == null)
                    {
                        GameAudioContext = ALContext.GetApi(false);
                        GameAudio = AL.GetApi(false);
                        GameAudioDevice = GameAudioContext.OpenDevice("");
                        if (GameAudioDevice == null)
                            throw new Exception("GameAudioDevice == null");
                        GameAudioCtx = GameAudioContext.CreateContext(GameAudioDevice, null);
                        GameAudioContext.MakeContextCurrent(GameAudioCtx);
                        var err = GameAudio.GetError();
                        if (err != AudioError.NoError)
                            throw new Exception($"AudioError {err.ToString()}");
                        GameAudio.DistanceModel(Silk.NET.OpenAL.DistanceModel.LinearDistanceClamped);
                    }
                    Window.PrioritizeSdl();
                    GameWindow = Window.Create(options);
                    GameWindow.Load += Load;
                    GameWindow.Closing += Unload;
                    GameWindow.Resize += Resize;
                    GameWindow.Render += Frame;
                    GameWindow.Update += Update;
                    GameWindow.FocusChanged += FocusChange;
                }
                else
                {
                    GameWindow.Title = $"{Game.Name} <{backend}>";
                }
            }
        }

        private static void Load()
        {
            GameGraphics = GameWindow.CreateGraphicsDevice(new GraphicsDeviceOptions()
            {
                // Debug = true,
                HasMainSwapchain = true,
                SwapchainSrgbFormat = false,
                ResourceBindingModel = ResourceBindingModel.Improved,
                PreferStandardClipSpaceYDirection = true,
                PreferDepthRangeZeroToOne = true,
                SwapchainDepthFormat = PixelFormat.D32_Float_S8_UInt,
                // SyncToVerticalBlank = true,
            }, APIBackend);
            GameInputContext = GameWindow.CreateInput();
            GameInputSnapshotHandler = new InputHandler();
            GameImGui = new ImGuiRenderer(GameGraphics, GameGraphics.SwapchainFramebuffer.OutputDescription, GameWindow.Size.X, GameWindow.Size.Y);

            MainMeshShader = ResourceManager.LoadShader("Shaders/MainMesh");
            ResourceManager.ReCreateAll();
        }

        private static unsafe void Unload()
        {
            Game.Dispose();
            ResourceManager.UnloadAll();
            GameInputSnapshotHandler?.Dispose();
            GameImGui?.Dispose();
            GameGraphics?.Dispose();
            if (GameAudioContext != null)
            {
                GameAudioContext.DestroyContext(GameAudioCtx);
                GameAudioContext.CloseDevice(GameAudioDevice);
                GameAudio.Dispose();
                GameAudioContext.Dispose();
            }
            IsClosing = true;
        }

        private static void Resize(Vector2D<int> size)
        {
            GameGraphics.ResizeMainWindow((uint)size.X, (uint)size.Y);
            GameImGui.WindowResized(size.X, size.Y);
        }

        private static void FocusChange(bool state)
        {
            IsFocused = state;
        }

        private static void Frame(double delta)
        {
            Time += delta;
            if ((long)(Time - delta) != (long)(Time))
            {
                FPS = (1f / delta);
            }
            ResourceManager.Update();
            GameImGui.Update((float)delta, GameInputSnapshotHandler);
            MainRenderer.ProjectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(70f * (MathF.PI / 180f), (float)MainRenderer.InternalRenderTexture.Width / MainRenderer.InternalRenderTexture.Height, 0.1f, 1000f);
            Game.PreDraw(delta);
            MainRenderer.Begin();
            MainRenderer.Clear();
            Game.Draw(delta);
            if (ImGui.Begin("Debug"))
            {
                ImGui.Text($"FPS: {FPS}");
                ImGui.Text($"Time Delta: {delta}");
                ImGui.Text($"Current Rendering API: {APIBackend.ToString()}");
                if (ImGui.Button("Exit"))
                {
                    IsClosing = true;
                }
                ImGui.Text($"Mesh Count: {ResourceManager.AllResources.Count(x => x is Mesh)}");
                ImGui.Text($"Material Count: {ResourceManager.AllResources.Count(x => x is Material)}");
                ImGui.Text($"Texture2D Count: {ResourceManager.AllResources.Count(x => x is Texture2D)}");
                if (ImGui.Button("ReCreate Resources"))
                {
                    ReCreateAllNextFrame = true;
                }
                if (ImGui.Button("Rendering API"))
                {
                    ImGui.OpenPopup("DebugRenderAPI");
                }
                if (ImGui.BeginPopup("DebugRenderAPI"))
                {
                    var vals = Enum.GetValues<GraphicsBackend>();
                    foreach (var val in vals)
                    {
                        if (GraphicsDevice.IsBackendSupported(val) && ImGui.Button(val.ToString()))
                        {
                            NextFrameBackend = val;
                        }
                    }
                    ImGui.EndPopup();
                }
                if (RenderDocInstance == null)
                {
                    if (ImGui.Button("RenderDoc"))
                    {
                        RenderDoc rd = null;
                        if (RenderDoc.Load(out rd))
                        {
                            RenderDocInstance = rd;
                            NextFrameBackend = APIBackend;
                        }
                    }
                }
                else
                {
                    if (ImGui.Button("Capture"))
                    {
                        RenderDocInstance.TriggerCapture();
                    }
                    if (ImGui.Button("Open UI"))
                    {
                        RenderDocInstance.LaunchReplayUI();
                    }
                }
                ImGui.End();
            }
            GameImGui.Render(GameGraphics, MainRenderer.CommandList);
            MainRenderer.End();
            MainRenderer.Submit();
            GameGraphics.SwapBuffers();
            GameInputSnapshotHandler.Update();
        }

        private static void Update(double dt)
        {
            Game.Tick(dt);
        }
    }
}
