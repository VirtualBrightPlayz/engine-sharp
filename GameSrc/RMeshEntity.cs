using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using BepuPhysics;
using BepuPhysics.Collidables;
using Engine.Assets;
using Engine.Assets.Audio;
using Engine.Assets.Models;
using Engine.Assets.Rendering;
using Engine.Assets.Textures;
using Engine.Game.Entities;
using Veldrid;

namespace GameSrc
{
    public class RMeshEntity : PhysicsEntity
    {
        public RMeshModel Room { get; private set; }
        public AudioSource[] Sources { get; private set; }
        public BepuPhysics.Collidables.Mesh shape;
        public ForwardConsts.ForwardLight[] Lights { get; private set; }

        public RMeshEntity(string name, string path) : base(name)
        {
            Room = ResourceManager.Clone<RMeshModel>($"{name}_{Random.Shared.Next()}", ResourceManagerExtensions.LoadRoomModel(path));
            List<Triangle> tris = new List<Triangle>();
            for (int i = 0; i < Room.CollisionTriangles.Length - 2; i+=3)
            {
                var v0 = Room.CollisionPositions[Room.CollisionTriangles[i+0]];
                var v1 = Room.CollisionPositions[Room.CollisionTriangles[i+1]];
                var v2 = Room.CollisionPositions[Room.CollisionTriangles[i+2]];
                tris.Add(new Triangle(v0, v1, v2));
                // tris.Add(new Triangle(v2, v1, v0));
            }
            Game.BufferPool.Take<Triangle>(tris.Count, out var triBuf);
            for (int i = 0; i < tris.Count; i++)
            {
                triBuf[i] = tris[i];
            }
            shape = new BepuPhysics.Collidables.Mesh(triBuf, Vector3.One, Game.BufferPool);
            shapeIndex = Game.Simulation.Shapes.Add(shape);
            staticHandle = Game.Simulation.Statics.Add(new StaticDescription(Position, Rotation, shapeIndex.Value));
            Sources = new AudioSource[Room.Sounds.Count];
            for (int i = 0; i < Sources.Length; i++)
            {
                Sources[i] = new AudioSource($"{name}_{i}");
                Sources[i].SetBuffer(Room.Sounds[i].clip);
                Sources[i].Position = Vector3.Transform(Room.Sounds[i].position, WorldMatrix);
                Sources[i].MaxDistance = Room.Sounds[i].range * 3f;
                Sources[i].ReferenceDistance = Room.Sounds[i].range * 0.75f;
                Sources[i].Looping = true;
            }
            Lights = new ForwardConsts.ForwardLight[Room.Lights.Count];
            for (int i = 0; i < Lights.Length; i++)
            {
                Lights[i] = new ForwardConsts.ForwardLight();
                Lights[i].Position = Vector3.Transform(Room.Lights[i].Position, WorldMatrix);
                Lights[i].Color = Room.Lights[i].Color;
                Lights[i].Range = Room.Lights[i].Range;
            }
            ForwardConsts.Lights.AddRange(Lights);
            StopAudio();
        }

        public void PlayAudio()
        {
            for (int i = 0; i < Sources.Length; i++)
            {
                Sources[i].Play();
            }
        }

        public void StopAudio()
        {
            for (int i = 0; i < Sources.Length; i++)
            {
                Sources[i].Stop();
            }
        }

        public override void MarkTransformDirty(TransformDirtyFlags flags)
        {
            base.MarkTransformDirty(flags);
            if (flags.HasFlag(TransformDirtyFlags.Position) || flags.HasFlag(TransformDirtyFlags.Rotation))
            {
                for (int i = 0; i < Sources.Length; i++)
                {
                    Sources[i].Position = Vector3.Transform(Room.Sounds[i].position, WorldMatrix);
                    Sources[i].Play();
                }
                for (int i = 0; i < Lights.Length; i++)
                {
                    Lights[i].Position = Vector3.Transform(Room.Lights[i].Position, WorldMatrix);
                }
            }
        }

        public override void PreDraw(double dt)
        {
            base.PreDraw(dt);
        }

        public override void Draw(double dt)
        {
            base.Draw(dt);
            if ((Position - Renderer.Current.ViewPosition).LengthSquared() > 50f * 50f)
            {
                return;
            }
            Room.SetWorldMatrix(WorldMatrix);
            Room.Draw(dt);
        }

        public override void Tick(double dt)
        {
            base.Tick(dt);
        }

        public override void Dispose()
        {
            for (int i = 0; i < Sources.Length; i++)
            {
                Sources[i].Dispose();
            }
            base.Dispose();
        }
    }
}