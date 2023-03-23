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
using Myra;
using Myra.Graphics2D.UI;

namespace Propnado
{
    public partial class Propnado2 : GameApp
    {
        public override string Name => "Propnado2";

        // public Entity Player;
        private Texture2D tex;
        private Desktop desktop;

        public override void Setup()
        {
            base.Setup();
            MyraEnvironment.Platform = new VeldridSurface();
            desktop = new Desktop();
            desktop.Root = new Test.AllWidgets();
            // Entities.Add(new StaticModelEntity("H", "Shaders/cube.gltf", null));
            /*
            Model mdl = new Model("Cube", "Shaders/cube.gltf", new Material("Base", new GraphicsShader("Shaders/MainMesh")), true, true);
            int meshId = DrawMeshSys.GetMeshId(mdl.Meshes[0]);
            Entity ent = Scene.CreateEntity();
            ent.Set(new MeshData()
            {
                MeshId = DrawMeshSys.GetMeshId(mdl.Meshes[0]),
                MaterialId = DrawMeshSys.GetMaterialId(mdl.InternalMaterials[0]),
            });
            */
            // tex = new Texture2D("Bitmap", ms.GetBuffer());
        }

        public override void Draw(Renderer renderer, double dt)
        {
            base.Draw(renderer, dt);
            desktop.Render();
            // Renderer.Current.Blit(tex);
        }

        public override void Tick(double dt)
        {
            // if (Player == null)
            {
                // Player = new NoClipPlayer();
                // Entities.Add(Player);
            }
            base.Tick(dt);
        }
    }
}
