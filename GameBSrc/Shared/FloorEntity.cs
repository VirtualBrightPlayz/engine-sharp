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
        public static string DiffusePath => Path.Combine(BGame.Instance.Data.GFXDir, "sign.jpg");
        public const string MarkerName = "FloorMarker";
        public static Texture2D DiffuseTexture = new Texture2D(DiffusePath);

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

        public override void Create(bool createStatic)
        {
            floorTextureRenderer = new Renderer($"Floor_{FloorNumber}");

            floorTexture = new RenderTexture2D($"Floor_{FloorNumber}", TexSize, TexSize);
            floorTexture.ReCreate();
            cubeMat = new Material($"{MarkerName}_{FloorNumber}", BGame.Instance.Shader);
            cubeBuffer = new CompoundBuffer($"{DiffusePath}_{FloorNumber}", cubeMat.Shader, UniformConsts.DiffuseTextureSet, floorTexture, Texture2D.DefaultWhite, Texture2D.DefaultNormal);
            cubeMat.SetUniforms(UniformConsts.DiffuseTextureSet, cubeBuffer);

            // /*
            Cube = new ModelEntity($"{MarkerName}_{FloorNumber}", CubePath, cubeMat);
            Cube.Create();
            Cube.Model.CompoundBuffers.Clear();
            Cube.Model.CompoundBuffers.Add(cubeBuffer);
            Cube.Scale = Vector3.One * 0.25f;
            if (FloorNumber % 2 == 0)
                Cube.Position = new Vector3(-0.24f, -FloorNumber*2f-0.6f, 1.5f);
            else
                Cube.Position = new Vector3(7.4f+0.6f+0.24f, -FloorNumber*2f-0.6f, 7f-1.5f);
            Cube.MarkTransformDirty(TransformDirtyFlags.Position | TransformDirtyFlags.Rotation | TransformDirtyFlags.Scale);
            BGame.Instance.Entities.Add(Cube);
            // */

            base.Create(createStatic);
        }

        public void RenderFloorTexture()
        {
            // floorTextureUIRenderer = new ImGuiRenderer(RenderingGlobals.GameGraphics, floorTexture.InternalFramebuffer.OutputDescription, (int)floorTexture.InternalFramebuffer.Width, (int)floorTexture.InternalFramebuffer.Height);
            floorTextureUIRenderer = RenderingGlobals.GameImGui;
            RenderingGlobals.ImGuiSetTarget(floorTexture);
            var font = UIExt.Pretext(floorTextureUIRenderer, (int)(TexSize / 4f));
            // if (!hasRendered)
                // floorTextureUIRenderer.RecreateFontDeviceTexture();
            floorTextureRenderer.SetRenderTarget(floorTexture);
            floorTextureRenderer.Begin();
            floorTextureRenderer.Clear();
            floorTextureRenderer.Blit(DiffuseTexture);
            floorTextureUIRenderer.Update(0f, MiscGlobals.GameInputHandler);
            UIExt.BeginDraw();
            UIExt.TextLeft(font, TexSize / 4f, (Vector2.One * TexSize / 2f) - (Vector2.One * (TexSize / 8f)), Vector2.Zero, $"{FloorNumber}", UIExt.Color(new Vector4(0f, 0f, 0f, 1f)));
            ImGui.EndFrame();
            floorTextureUIRenderer.Render(RenderingGlobals.GameGraphics, floorTextureRenderer.CommandList);
            floorTextureRenderer.End();
            floorTextureRenderer.Submit();
        }

        public override void MarkTransformDirty(TransformDirtyFlags flags)
        {
            base.MarkTransformDirty(flags);
        }

        public override void Tick(double dt)
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
            base.Tick(dt);
            if (!hasRendered)
                RenderFloorTexture();
            hasRendered = true;
        }

        public override void ReCreate()
        {
            base.ReCreate();
            floorTexture.HasBeenInitialized = false;
            floorTexture.ReCreate();
            hasRendered = false;
        }

        public override void Unload()
        {
            base.Unload();
            floorTexture.Dispose();
        }

        public override void Draw(Renderer renderer, double dt)
        {
            if ((renderer.ViewPosition - Position).LengthSquared() > 225f)
                return;
            base.Draw(renderer, dt);
        }

        public override void Dispose()
        {
            floorTexture.Dispose();
            floorTextureUIRenderer.Dispose();
            base.Dispose();
        }
    }
}