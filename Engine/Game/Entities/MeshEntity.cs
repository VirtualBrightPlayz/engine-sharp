using System;
using System.Threading.Tasks;
using Engine.Assets;
using Engine.Assets.Models;
using Engine.Assets.Rendering;

namespace Engine.Game.Entities
{
    public class MeshEntity : PhysicsEntity
    {
        public Mesh Mesh { get; private set; }
        public BepuPhysics.Collidables.Mesh shape;

        public MeshEntity(string name, string path, Material material) : base(name)
        {
            Create(path, material);
            /*List<Triangle> tris = new List<Triangle>();
            for (int i = 0; i < Model.CollisionTriangles.Length - 2; i+=3)
            {
                var v0 = Model.CollisionPositions[Model.CollisionTriangles[i+0]];
                var v1 = Model.CollisionPositions[Model.CollisionTriangles[i+1]];
                var v2 = Model.CollisionPositions[Model.CollisionTriangles[i+2]];
                tris.Add(new Triangle(v0, v1, v2));
                tris.Add(new Triangle(v2, v1, v0));
            }
            Game.BufferPool.Take<Triangle>(tris.Count, out var triBuf);
            shape = new BepuPhysics.Collidables.Mesh(triBuf, Vector3.One, Game.BufferPool);
            shapeIndex = Game.Simulation.Shapes.Add(shape);
            var inertia = shape.ComputeOpenInertia(1f);
            // bodyHandle = Game.Simulation.Bodies.Add(BodyDescription.CreateDynamic(Position, inertia, shapeIndex.Value, 0.01f));
            // bodyHandle = Game.Simulation.Bodies.Add(BodyDescription.CreateKinematic(Position, new CollidableDescription(shapeIndex.Value, 0.3f), -10f));
            staticHandle = Game.Simulation.Statics.Add(new StaticDescription(Position, Rotation, shapeIndex.Value));*/
        }

        private void Create(string path, Material material)
        {
            // Mesh = new Mesh(path, false, material);
        }

        public override void PreDraw(Renderer renderer, double dt)
        {
            base.PreDraw(renderer, dt);
            // Mesh.PreDraw(renderer);
        }

        public override void Draw(Renderer renderer, double dt)
        {
            base.Draw(renderer, dt);
            // Mesh.SetWorldMatrix(renderer, WorldMatrix);
            // Mesh.Draw(renderer);
        }
    }
}