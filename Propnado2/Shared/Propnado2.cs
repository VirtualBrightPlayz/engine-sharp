using System.IO;
using System.Threading.Tasks;
using Avalonia;
using Avalonia.Markup.Xaml;
using Avalonia.Media.Imaging;
using Avalonia.Skia;
using DefaultEcs;
using Engine.Assets.Models;
using Engine.Assets.Rendering;
using Engine.Assets.Textures;
using Engine.Game;
using Engine.Game.Components;
using FontStashSharp;
using Myra;
using Myra.Graphics2D.UI;
using Myra.Graphics2D.UI.Styles;
using TheArtOfDev.HtmlRenderer.Avalonia;

namespace Propnado
{
    public partial class Propnado2 : GameApp
    {
        public override string Name => "Propnado2";

        public override void Setup()
        {
            base.Setup();
            var shader = new GraphicsShader("Shaders/Main");
            var mat = new Material("Test", shader);
            var mesh = new Model("cube", "Shaders/cube.gltf", mat, false, true);
            var tex = new Texture2D("Shaders/white.bmp");
            mat.SetUniforms(1, new UniformLayout("Diffuse", tex, false, true));
            Entity ent = Scene.CreateEntity();
            ent.Set(new MeshData()
            {
                MaterialId = DrawMeshSys.GetMaterialId(mat),
                MeshId = DrawMeshSys.GetMeshId(mesh.Meshes[0]),
                MaterialPass = "main",
            });
        }

        public override void Draw(Renderer renderer, double dt)
        {
            base.Draw(renderer, dt);
        }

        public override void Tick(double dt)
        {
            base.Tick(dt);
        }
    }
}
