using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using BepuPhysics;
using BepuPhysics.Collidables;
using Engine.Assets;
using Engine.Assets.Models;
using Engine.Assets.Rendering;
using Engine.Assets.Textures;
using Veldrid;

namespace Engine.Game.Entities
{
    public class ModelEntity : PhysicsEntity
    {
        public Model Model { get; private set; }
        public BepuPhysics.Collidables.Mesh shape;

        public ModelEntity(string name, string path, Material material) : base(name)
        {
            Model = ResourceManager.Clone<Model>($"{name}_{Random.Shared.Next()}", ResourceManager.LoadModel(material, path));
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

        public override void PreDraw(double dt)
        {
            base.PreDraw(dt);
            Model.SetWorldMatrix(WorldMatrix);
            Model.PreDraw(Renderer.Current);
        }

        public override void Draw(double dt)
        {
            base.Draw(dt);
            Model.Draw(Renderer.Current);
        }
    }
}