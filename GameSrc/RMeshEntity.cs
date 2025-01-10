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
        public static GraphicsShader shader { get; set; }
        public RMeshModel Room { get; private set; }
        public AudioSource[] Sources { get; private set; }
        public StaticModelEntity[] Models { get; private set; }
        public BepuPhysics.Collidables.Mesh shape;
        public ForwardConsts.ForwardLight[] Lights { get; private set; }
        public UniformBuffer WorldMatrixUniform { get; private set; }

        public RMeshEntity(string name, string path) : base(name)
        {
            shader ??= new GraphicsShader("Shaders/MainMesh");
            if (SCPCB.RMeshModels.TryGetValue(path, out var r))
                Room = r;
            else
            {
                Room = new RMeshModel($"{name}_{Random.Shared.Next()}", path);
                SCPCB.RMeshModels.Add(path, Room);
            }
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
            for (int i = 0; i < Lights.Length; i++)
            {
                ForwardConsts.Lights.Add(Lights[i]);
            }
            StopAudio();
            Models = new StaticModelEntity[Room.Models.Count];
            for (int i = 0; i < Models.Length; i++)
            {
                Models[i] = new StaticModelEntity(Room.Models[i].path, Path.Combine(SCPCB.Instance.Data.PropsDir, Room.Models[i].path), new Material(Room.Models[i].path, shader));
                Models[i].Create(true);
                Models[i].Position = Vector3.Transform(Room.Models[i].position, WorldMatrix);
                var rot = Quaternion.CreateFromYawPitchRoll(Room.Models[i].euler.X, Room.Models[i].euler.Y, Room.Models[i].euler.Z);
                Models[i].Rotation = rot * Rotation;
                Models[i].Scale = Room.Models[i].scale;
                Models[i].MarkTransformDirty(TransformDirtyFlags.Position | TransformDirtyFlags.Rotation | TransformDirtyFlags.Scale);
            }
            WorldMatrixUniform = new UniformBuffer(UniformConsts.WorldMatrixName, (uint)16 * 4);
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
            if (flags.HasFlag(TransformDirtyFlags.Position) || flags.HasFlag(TransformDirtyFlags.Rotation) || flags.HasFlag(TransformDirtyFlags.Scale))
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
                for (int i = 0; i < Models.Length; i++)
                {
                    Models[i].Position = Vector3.Transform(Room.Models[i].position, WorldMatrix);
                    var rot = Quaternion.CreateFromYawPitchRoll(Room.Models[i].euler.Y * (MathF.PI / 180f), -Room.Models[i].euler.X * (MathF.PI / 180f), Room.Models[i].euler.Z * (MathF.PI / 180f));
                    Models[i].Rotation = rot * Rotation;
                    Models[i].Scale = Room.Models[i].scale;
                    Models[i].MarkTransformDirty(TransformDirtyFlags.Position | TransformDirtyFlags.Rotation | TransformDirtyFlags.Scale);
                }
            }
        }

        public override void PreDraw(Renderer renderer, double dt)
        {
            base.PreDraw(renderer, dt);
            if ((Position - renderer.ViewPosition).LengthSquared() > SCPCBPlayerEntity.MaxRoomRenderDistance * SCPCBPlayerEntity.MaxRoomRenderDistance)
            {
                return;
            }
            for (int i = 0; i < Models.Length; i++)
            {
                Models[i].PreDraw(renderer, dt);
            }
            WorldMatrixUniform.UploadData(WorldMatrix);
        }

        public override void Draw(Renderer renderer, double dt)
        {
            base.Draw(renderer, dt);
            if ((Position - renderer.ViewPosition).LengthSquared() > SCPCBPlayerEntity.MaxRoomRenderDistance * SCPCBPlayerEntity.MaxRoomRenderDistance)
            {
                return;
            }
            for (int i = 0; i < Models.Length; i++)
            {
                Models[i].Draw(renderer, dt);
            }
            Room.Draw(renderer, WorldMatrixUniform, dt);
        }

        public override void Tick(double dt)
        {
            base.Tick(dt);
            for (int i = 0; i < Models.Length; i++)
            {
                Models[i].Tick(dt);
            }
        }

        public override void Dispose()
        {
            for (int i = 0; i < Sources.Length; i++)
            {
                Sources[i].Dispose();
            }
            for (int i = 0; i < Models.Length; i++)
            {
                Models[i].Dispose();
            }
            base.Dispose();
        }
    }
}