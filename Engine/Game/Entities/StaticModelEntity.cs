using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using BepuPhysics;
using BepuPhysics.Collidables;
using Engine.Assets;
using Engine.Assets.Models;
using Engine.Assets.Rendering;
using Engine.Assets.Textures;
using Veldrid;

namespace Engine.Game.Entities
{
    public class StaticModelEntity : PhysicsEntity
    {
        public Model Model { get; private set; }
        public BepuPhysics.Collidables.Mesh shape;
        private readonly string _path;
        private readonly Material _material;

        public StaticModelEntity(string name, string path, Material material) : base(name)
        {
            _path = path;
            _material = material;
            // Create(path, material);
        }

        public virtual async Task Create()
        {
            Model = await ResourceManager.Clone<Model>($"{Name}_{Random.Shared.Next()}", await ResourceManager.LoadModel(Name, _material, _path));
            List<Triangle> tris = new List<Triangle>();
            for (int i = 0; i < Model.CollisionTriangles.Length - 2; i+=3)
            {
                var v0 = Model.CollisionPositions[Model.CollisionTriangles[i+0]];
                var v1 = Model.CollisionPositions[Model.CollisionTriangles[i+1]];
                var v2 = Model.CollisionPositions[Model.CollisionTriangles[i+2]];
                tris.Add(new Triangle(v0, v1, v2));
                tris.Add(new Triangle(v2, v1, v0));
            }
            Game.BufferPool.Take<Triangle>(tris.Count, out var triBuf);
            for (int i = 0; i < tris.Count; i++)
            {
                triBuf[i] = tris[i];
            }
            shape = new BepuPhysics.Collidables.Mesh(triBuf, Scale, Game.BufferPool);
            shapeIndex = Game.Simulation.Shapes.Add(shape);
            staticHandle = Game.Simulation.Statics.Add(new StaticDescription(Position, Rotation, shapeIndex.Value));
        }

        public override void MarkTransformDirty(TransformDirtyFlags flags)
        {
            if (flags.HasFlag(TransformDirtyFlags.Scale) && shapeIndex.HasValue && staticHandle.HasValue)
            {
                shape.Scale = Scale;
                Game.Simulation.Shapes.Remove(shapeIndex.Value);
                shapeIndex = Game.Simulation.Shapes.Add(shape);
                Game.Simulation.Statics[staticHandle.Value].SetShape(shapeIndex.Value);
            }
            base.MarkTransformDirty(flags);
        }

        public override void PreDraw(Renderer renderer, double dt)
        {
            base.PreDraw(renderer, dt);
        }

        public override void Draw(Renderer renderer, double dt)
        {
            base.Draw(renderer, dt);
            Model.SetWorldMatrixDraw(renderer, WorldMatrix);
        }
    }
}