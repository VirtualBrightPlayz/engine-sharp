using System;
using System.Collections.Concurrent;
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
        public static ConcurrentDictionary<string, WeakReference<Model>> Models = new ConcurrentDictionary<string, WeakReference<Model>>();
        public Model Model { get; private set; }
        public BepuPhysics.Collidables.Mesh shape;
        private readonly string _path;
        private readonly Material _material;
        public UniformBuffer WorldMatrixUniform { get; private set; }

        public StaticModelEntity(string name, string path, Material material) : base(name)
        {
            _path = path;
            _material = material;
        }

        public virtual void Create(bool createStatic)
        {
            if (shapeIndex.HasValue)
                Game.Simulation.Shapes.Remove(shapeIndex.Value);
            if (staticHandle.HasValue)
                Game.Simulation.Statics.Remove(staticHandle.Value);
            if (Models.TryGetValue(_path, out var mdl) && mdl.TryGetTarget(out var mdl2))
            {
                Model = mdl2;
            }
            else
            {
                Model = new Model(Name, _path, _material, true, false);
                if (Models.ContainsKey(_path))
                    Models.TryRemove(_path, out _);
                Models.TryAdd(_path, new WeakReference<Model>(Model));
            }
            WorldMatrixUniform = new UniformBuffer(UniformConsts.WorldMatrixName, (uint)16 * 4);
            List<Triangle> tris = new List<Triangle>();
            for (int i = 0; i < Model.CollisionTriangles.Length / 3; i++)
            {
                var v0 = Model.CollisionPositions[Model.CollisionTriangles[i*3+0]];
                var v1 = Model.CollisionPositions[Model.CollisionTriangles[i*3+1]];
                var v2 = Model.CollisionPositions[Model.CollisionTriangles[i*3+2]];
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
            if (createStatic)
                staticHandle = Game.Simulation.Statics.Add(new StaticDescription(Position, Rotation, shapeIndex.Value));
            else
                staticHandle = null;
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
            WorldMatrixUniform.UploadData(WorldMatrix);
        }

        public override void Draw(Renderer renderer, double dt)
        {
            base.Draw(renderer, dt);
            Model.SetWorldMatrixDraw(renderer, WorldMatrixUniform, ForwardConsts.ForwardBasePassName);
            for (int i = 1; i < ForwardConsts.LightUniforms.Count; i++)
                Model.SetWorldMatrixDraw(renderer, WorldMatrixUniform, ForwardConsts.ForwardAddPassName + '#' + i);
        }
    }
}