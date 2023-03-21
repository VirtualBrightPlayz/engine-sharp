using System.Threading.Tasks;
using DefaultEcs;
using Engine.Assets.Models;
using Engine.Assets.Rendering;
using Engine.Game;
using Engine.Game.Components;

namespace Propnado
{
    public partial class Propnado2 : GameApp
    {
        public override string Name => "Propnado2";

        // public Entity Player;

        public override void Setup()
        {
            base.Setup();
            Model mdl = new Model("Cube", "Shaders/cube.gltf", new Material("Base", new GraphicsShader("Shaders/MainMesh")), true, true);
            int meshId = DrawMeshSys.GetMeshId(mdl.Meshes[0]);
            // Entities.Add(new StaticModelEntity("H", "Shaders/cube.gltf", null));
            Entity ent = Scene.CreateEntity();
            ent.Set(new MeshData()
            {
                MeshId = DrawMeshSys.GetMeshId(mdl.Meshes[0]),
                MaterialId = DrawMeshSys.GetMaterialId(mdl.InternalMaterials[0]),
            });
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
