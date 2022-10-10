using System.IO;
using System.Numerics;
using System.Threading.Tasks;
using BepuPhysics;
using Engine;
using Engine.Assets;
using Engine.Assets.Rendering;
using Engine.Assets.Textures;
using Engine.Game.Entities;
using ImGuiNET;
using Veldrid;

namespace GameBSrc
{
    public class FloorEntity : StaticModelEntity
    {
        public enum FloorAction
        {
            Unknown = 0,
            Steps = 1,
            Lights = 2,
            Flash = 3,
            Walk = 4,
            Run = 5,
            Kalle = 6,
            Breath = 7,
            Proceed = 8,
            Trap = 9,
            Act_173 = 11,
            Cell = 12,
            Lock = 13,
            Radio2 = 15,
            Radio3 = 16,
            Radio4 = 17,
            Trick1 = 18,
            Trick2 = 19,
            Roar = 20,
            Darkness = 21,
        }

        public const string CubePath = "Shaders/cube.gltf";
        public string DiffusePath => Path.Combine(BGame.Instance.Data.GFXDir, "sign.jpg");
        public const string MarkerName = "FloorMarker";
        public Task<Texture2D> DiffuseTexture => ResourceManager.LoadTexture(DiffusePath);

        public RenderTexture2D floorTexture;
        public Renderer floorTextureRenderer;
        public ImGuiRenderer floorTextureUIRenderer;
        public ModelEntity Cube;
        public Material cubeMat;
        public CompoundBuffer cubeBuffer;
        public readonly int FloorNumber;
        public uint TexSize = 512;
        public bool hasRendered = false;
        public FloorAction Action;

        public FloorEntity(int number, string path, Material material) : base($"Floor_{number}", path, material)
        {
            FloorNumber = number;
            // Create();
        }

        public override async Task Create(bool createStatic)
        {
            floorTextureRenderer = await ResourceManager.CreateRenderer($"Floor_{FloorNumber}");

            floorTexture = new RenderTexture2D($"Floor_{FloorNumber}", TexSize, TexSize);
            await floorTexture.ReCreate();
            // floorTextureUIRenderer = new ImGuiRenderer(RenderingGlobals.GameGraphics, floorTexture.InternalFramebuffer.OutputDescription, (int)floorTexture.InternalFramebuffer.Width, (int)floorTexture.InternalFramebuffer.Height);
            // floorTextureUIRenderer = RenderingGlobals.GameImGui;
            // await RenderFloorTexture();
            cubeMat = await ResourceManager.CreateMaterial($"{MarkerName}_{FloorNumber}", await BGame.Instance.Shader);
            cubeBuffer = await ResourceManager.CreateCompoundBuffer($"{DiffusePath}_{FloorNumber}", cubeMat.Shader, UniformConsts.DiffuseTextureSet, floorTexture, await Texture2D.DefaultWhite, await Texture2D.DefaultNormal);
            cubeMat.SetUniforms(UniformConsts.DiffuseTextureSet, cubeBuffer);

            /*
            Cube = new ModelEntity($"{MarkerName}_{FloorNumber}", CubePath, cubeMat);
            await Cube.Create();
            Cube.Model.CompoundBuffers.Add(cubeBuffer);
            Cube.Scale = Vector3.One * 0.25f;
            if (FloorNumber % 2 == 0)
                Cube.Position = new Vector3(-0.24f, -FloorNumber*2f-0.6f, 1.5f);
            else
                Cube.Position = new Vector3(7.4f+0.6f+0.24f, -FloorNumber*2f-0.6f, 7f-1.5f);
            Cube.MarkTransformDirty(TransformDirtyFlags.Position | TransformDirtyFlags.Rotation | TransformDirtyFlags.Scale);
            BGame.Instance.Entities.Add(Cube);
            */

            await base.Create(false);
        }

        public async Task RenderFloorTexture()
        {
            return;
            floorTextureUIRenderer = new ImGuiRenderer(RenderingGlobals.GameGraphics, floorTexture.InternalFramebuffer.OutputDescription, (int)floorTexture.InternalFramebuffer.Width, (int)floorTexture.InternalFramebuffer.Height);
            ImGui.EndFrame();
            var font = await UIExt.Pretext(floorTextureUIRenderer, (int)(TexSize / 4f));
            // if (!hasRendered)
                // floorTextureUIRenderer.RecreateFontDeviceTexture();
            // RenderingGlobals.ImGuiSetTarget(floorTexture.InternalFramebuffer.OutputDescription);
            // floorTextureUIRenderer.CreateDeviceResources(RenderingGlobals.GameGraphics, floorTexture.InternalFramebuffer.OutputDescription);
            floorTextureRenderer.SetRenderTarget(floorTexture);
            floorTextureRenderer.Begin();
            floorTextureRenderer.Clear();
            floorTextureRenderer.Blit(await DiffuseTexture);
            System.Console.WriteLine("J");
            floorTextureUIRenderer.Update(0.01f, MiscGlobals.GameInputHandler);
            System.Console.WriteLine("I");
            UIExt.BeginDraw();
            System.Console.WriteLine($"K {font.IsLoaded()} {font.FontSize}");
            unsafe
            {
                System.Console.WriteLine($"fgkldj {new System.IntPtr(UIExt._drawList.NativePtr)}");
            }
            UIExt.TextLeft(font, font.FontSize, (Vector2.One * TexSize / 2f) - (Vector2.One * (TexSize / 8f)), Vector2.Zero, $"{FloorNumber}", UIExt.Color(new Vector4(0f, 0f, 0f, 1f)));
            System.Console.WriteLine("H");
            ImGui.EndFrame();
            floorTextureUIRenderer.Render(RenderingGlobals.GameGraphics, floorTextureRenderer.CommandList);
            floorTextureRenderer.End();
            floorTextureRenderer.Submit();
            // ImGui.EndFrame();
            // floorTextureUIRenderer.Update(1f, MiscGlobals.GameInputSnapshot);
        }

        public override void MarkTransformDirty(TransformDirtyFlags flags)
        {
            base.MarkTransformDirty(flags);
        }

        public override async Task Tick(double dt)
        {
            bool isVisible = (Renderer.Current.ViewPosition - Position).LengthSquared() > 225f;
            if (staticHandle.HasValue && isVisible)
            {
                Game.Simulation.Statics.Remove(staticHandle.Value);
                staticHandle = null;
            }
            else if (!isVisible && !staticHandle.HasValue)
            {
                staticHandle = Game.Simulation.Statics.Add(new StaticDescription(Position, Rotation, shapeIndex.Value));
            }
            await base.Tick(dt);
            if (!hasRendered)
                await RenderFloorTexture();
            hasRendered = true;
        }

        public override async Task Draw(Renderer renderer, double dt)
        {
            if ((renderer.ViewPosition - Position).LengthSquared() > 225f)
                return;
            await base.Draw(renderer, dt);
        }

        public override void Dispose()
        {
            floorTexture.Dispose();
            floorTextureUIRenderer.Dispose();
            base.Dispose();
        }
    }
}