using System.IO;
using System.Numerics;
using System.Threading.Tasks;
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
            Create();
        }

        private async void Create()
        {
            floorTextureRenderer = await ResourceManager.CreateRenderer($"Floor_{FloorNumber}");
            floorTexture = new RenderTexture2D($"Floor_{FloorNumber}", TexSize, TexSize);
            floorTextureUIRenderer = new ImGuiRenderer(RenderingGlobals.GameGraphics, floorTexture.InternalFramebuffer.OutputDescription, (int)floorTexture.InternalFramebuffer.Width, (int)floorTexture.InternalFramebuffer.Height);
            // RenderFloorTexture();
            cubeMat = await ResourceManager.CreateMaterial($"{MarkerName}_{FloorNumber}", await BGame.Instance.Shader);
            cubeBuffer = await ResourceManager.CreateCompoundBuffer($"{DiffusePath}_{FloorNumber}", cubeMat.Shader, UniformConsts.DiffuseTextureSet, floorTexture, await Texture2D.DefaultWhite, await Texture2D.DefaultNormal);
            cubeMat.SetUniforms(UniformConsts.DiffuseTextureSet, cubeBuffer);
            Cube = new ModelEntity($"{MarkerName}_{FloorNumber}", CubePath, cubeMat);
            Cube.Model.CompoundBuffers.Add(cubeBuffer);
            Cube.Scale = Vector3.One * 0.25f;
            if (FloorNumber % 2 == 0)
                Cube.Position = new Vector3(-0.24f, -FloorNumber*2f-0.6f, 1.5f);
            else
                Cube.Position = new Vector3(7.4f+0.6f+0.24f, -FloorNumber*2f-0.6f, 7f-1.5f);
            Cube.MarkTransformDirty(TransformDirtyFlags.Position | TransformDirtyFlags.Rotation | TransformDirtyFlags.Scale);
            BGame.Instance.Entities.Add(Cube);
        }

        public async void RenderFloorTexture()
        {
            floorTextureRenderer.SetRenderTarget(floorTexture);
            floorTextureRenderer.Begin();
            floorTextureRenderer.Clear();
            floorTextureRenderer.Blit(await DiffuseTexture);
            floorTextureUIRenderer.Update(0f, MiscGlobals.GameInputSnapshot);
            UIExt.BeginDraw();
            UIExt.Pretext(floorTextureUIRenderer);
            if (!hasRendered)
                floorTextureUIRenderer.RecreateFontDeviceTexture();
            UIExt.TextLeft(UIExt.Pretext(floorTextureUIRenderer), TexSize / 4f, (Vector2.One * TexSize / 2f) - (Vector2.One * (TexSize / 8f)), Vector2.Zero, $"{FloorNumber}", UIExt.Color(new Vector4(0f, 0f, 0f, 1f)));
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
            base.Tick(dt);
            if (!hasRendered)
                RenderFloorTexture();
            hasRendered = true;
        }

        public override void Draw(Renderer renderer, double dt)
        {
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