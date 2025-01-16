using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
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
        public static ConcurrentDictionary<StaticHandle, string> FloorLookup { get; private set; } = new ConcurrentDictionary<StaticHandle, string>();
        public RMeshModel Room { get; private set; }
        public AudioSource[] Sources { get; private set; }
        public StaticModelEntity[] Models { get; private set; }
        public BepuPhysics.Collidables.Mesh shape;
        public Compound compoundShape;
        public BepuPhysics.Collidables.Mesh[] shapes;
        public TypedIndex[] shapeIndexes;
        public StaticHandle?[] staticHandles;
        public ForwardConsts.ForwardLight[] Lights { get; private set; }
        public UniformBuffer WorldMatrixUniform { get; private set; }
        public Vector3[] NavPoints { get; private set; }
        public Vector3 PlayerStart => Vector3.Transform(Room.PlayerStart, WorldMatrix);

        public RMeshEntity(string name, string path) : base(name)
        {
            shader ??= new GraphicsShader("Shaders/MainMesh");
            if (SCPCB.RMeshModels.TryGetValue(path, out var r))
                Room = r;
            else
            {
                Room = new RMeshModel($"{name}_{Random.Shared.Next()}", path);
                SCPCB.RMeshModels.TryAdd(path, Room);
            }
        #if false
            shapes = new BepuPhysics.Collidables.Mesh[Room.Meshes.Length];
            shapeIndexes = new TypedIndex[Room.Meshes.Length];
            staticHandles = new StaticHandle?[Room.Meshes.Length];
            Game.BufferPool.Take<CompoundChild>(Room.Meshes.Length, out var children);
            for (int i = 0; i < Room.Meshes.Length; i++)
            {
                Debug.Assert(Room.Meshes[i].Indices.Count % 3 == 0);
                Game.BufferPool.Take<Triangle>(Room.Meshes[i].Indices.Count / 3, out var triBuf);
                for (int j = 0; j < Room.Meshes[i].Indices.Count / 3; j++)
                {
                    RMeshModel.RMeshVertexLayout vertex0 = (RMeshModel.RMeshVertexLayout)Room.Meshes[i].VertexList[(int)Room.Meshes[i].Indices[j*3+0]];
                    RMeshModel.RMeshVertexLayout vertex1 = (RMeshModel.RMeshVertexLayout)Room.Meshes[i].VertexList[(int)Room.Meshes[i].Indices[j*3+1]];
                    RMeshModel.RMeshVertexLayout vertex2 = (RMeshModel.RMeshVertexLayout)Room.Meshes[i].VertexList[(int)Room.Meshes[i].Indices[j*3+2]];
                    Debug.Assert(float.IsFinite(vertex0.Position.X) && float.IsFinite(vertex0.Position.Y) && float.IsFinite(vertex0.Position.Z));
                    Debug.Assert(float.IsFinite(vertex1.Position.X) && float.IsFinite(vertex1.Position.Y) && float.IsFinite(vertex1.Position.Z));
                    Debug.Assert(float.IsFinite(vertex2.Position.X) && float.IsFinite(vertex2.Position.Y) && float.IsFinite(vertex2.Position.Z));
                    triBuf[j] = new Triangle(vertex0.Position, vertex1.Position, vertex2.Position);
                    // triBuf[j] = new Triangle(vertex2.Position, vertex1.Position, vertex0.Position);
                }
                shapes[i] = new BepuPhysics.Collidables.Mesh(triBuf, Vector3.One, Game.BufferPool);
                shapeIndexes[i] = Game.Simulation.Shapes.Add(shape);
                children[i] = new CompoundChild(RigidPose.Identity, shapeIndexes[i]);
                // staticHandles[i] = Game.Simulation.Statics.Add(new StaticDescription(Position, Rotation, shapeIndexes[i]));
                // FloorLookup.TryAdd(staticHandles[i], Room.Textures[i]);
            }
            compoundShape = new Compound(children);
            // compoundShape = new BigCompound(children, Game.Simulation.Shapes, Game.BufferPool, Game.Dispatcher);
            shapeIndex = Game.Simulation.Shapes.Add(compoundShape);
            staticHandle = Game.Simulation.Statics.Add(new StaticDescription(Position, Rotation, shapeIndex.Value));
        #elif true
            Dictionary<string, List<Triangle>> trisLookup = new Dictionary<string, List<Triangle>>();
            int offset = 0;
            for (int i = 0; i < Room.Meshes.Length; i++)
            {
                string type = SCPCB.Instance.Data.GetFloorType(Path.GetFileName(Room.Textures[i]).ToLower());
                if (!trisLookup.ContainsKey(type))
                    trisLookup[type] = new List<Triangle>();
                for (int j = 0; j < Room.Meshes[i].Indices.Count / 3; j++)
                {
                    int index = j * 3 + offset;
                    var v0 = Room.CollisionPositions[Room.CollisionTriangles[index+0]];
                    var v1 = Room.CollisionPositions[Room.CollisionTriangles[index+1]];
                    var v2 = Room.CollisionPositions[Room.CollisionTriangles[index+2]];
                    trisLookup[type].Add(new Triangle(v0, v1, v2));
                }
                offset += Room.Meshes[i].Indices.Count;
            }
            shapes = new BepuPhysics.Collidables.Mesh[trisLookup.Count];
            shapeIndexes = new TypedIndex[trisLookup.Count];
            staticHandles = new StaticHandle?[trisLookup.Count];
            int id = 0;
            foreach (var kvp in trisLookup)
            {
                Game.BufferPool.Take<Triangle>(kvp.Value.Count, out var triBuf);
                for (int i = 0; i < kvp.Value.Count; i++)
                {
                    triBuf[i] = kvp.Value[i];
                }
                shapes[id] = new BepuPhysics.Collidables.Mesh(triBuf, Vector3.One, Game.BufferPool);
                shapeIndexes[id] = Game.Simulation.Shapes.Add(shapes[id]);
                staticHandles[id] = Game.Simulation.Statics.Add(new StaticDescription(Position, Rotation, shapeIndexes[id]));
                FloorLookup.TryAdd(staticHandles[id].Value, kvp.Key);
                id++;
            }
        #else
            shapes = new BepuPhysics.Collidables.Mesh[0];
            shapeIndexes = new TypedIndex[0];
            staticHandles = new StaticHandle?[0];

            List<Triangle> tris = new List<Triangle>();
            for (int i = 0; i < Room.CollisionTriangles.Length / 3; i++)
            {
                var v0 = Room.CollisionPositions[Room.CollisionTriangles[i*3+0]];
                var v1 = Room.CollisionPositions[Room.CollisionTriangles[i*3+1]];
                var v2 = Room.CollisionPositions[Room.CollisionTriangles[i*3+2]];
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
        #endif

            CreateAudio();
            NavPoints = new Vector3[Room.Waypoints.Count];
            for (int i = 0; i < NavPoints.Length; i++)
            {
                NavPoints[i] = Vector3.Transform(Room.Waypoints[i], WorldMatrix);
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
            WorldMatrixUniform = new UniformBuffer($"{path}_{UniformConsts.WorldMatrixName}", (uint)16 * 4);
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
            for (int i = 0; i < staticHandles.Length; i++)
            {
                if (staticHandles[i].HasValue)
                {
                    if (flags.HasFlag(TransformDirtyFlags.Position))
                        Game.Simulation.Statics[staticHandles[i].Value].Pose.Position = Position;
                    if (flags.HasFlag(TransformDirtyFlags.Rotation))
                        Game.Simulation.Statics[staticHandles[i].Value].Pose.Orientation = Rotation;
                    Game.Simulation.Statics.UpdateBounds(staticHandles[i].Value);
                }
            }
            if (flags.HasFlag(TransformDirtyFlags.Position) || flags.HasFlag(TransformDirtyFlags.Rotation) || flags.HasFlag(TransformDirtyFlags.Scale))
            {
                for (int i = 0; i < Sources.Length; i++)
                {
                    Sources[i].Position = Vector3.Transform(Room.Sounds[i].position, WorldMatrix);
                    Sources[i].Play();
                }
                for (int i = 0; i < NavPoints.Length; i++)
                {
                    NavPoints[i] = Vector3.Transform(Room.Waypoints[i], WorldMatrix);
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

        public void CreateAudio()
        {
            if (Sources == null || Sources.Length == 0)
            {
                Sources = new AudioSource[Room.Sounds.Count];
                for (int i = 0; i < Sources.Length; i++)
                {
                    Sources[i] = new AudioSource($"{Name}_{i}");
                    Sources[i].SetBuffer(Room.Sounds[i].clip);
                    Sources[i].Position = Vector3.Transform(Room.Sounds[i].position, WorldMatrix);
                    Sources[i].MaxDistance = Room.Sounds[i].range * 1f;
                    Sources[i].ReferenceDistance = Room.Sounds[i].range * 0.75f;
                    Sources[i].Looping = true;
                }
            }
        }

        public void DestroyAudio()
        {
            for (int i = 0; i < Sources.Length; i++)
            {
                Sources[i].Dispose();
            }
            Sources = Array.Empty<AudioSource>();
        }

        public override void PreDraw(Renderer renderer, double dt)
        {
            base.PreDraw(renderer, dt);
            if ((Position - renderer.ViewPosition).LengthSquared() > SCPCBPlayerEntity.MaxRoomRenderDistance * SCPCBPlayerEntity.MaxRoomRenderDistance)
            {
                DestroyAudio();
                return;
            }
            CreateAudio();
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
            WorldMatrixUniform.UploadData(renderer, WorldMatrix);
            Room.Draw(renderer, WorldMatrixUniform, ForwardConsts.ForwardBasePassName);
            for (int i = 1; i < ForwardConsts.LightUniforms.Count; i++)
                Room.Draw(renderer, WorldMatrixUniform, ForwardConsts.ForwardAddPassName + '#' + i);
        }

        public override void Tick(double dt)
        {
            base.Tick(dt);
            /*
            if (SCPCB.Instance.player == null || (Position - SCPCB.Instance.player.Position).LengthSquared() > SCPCBPlayerEntity.MaxRoomRenderDistance * SCPCBPlayerEntity.MaxRoomRenderDistance)
            {
                for (int i = 0; i < staticHandles.Length; i++)
                {
                    if (staticHandles[i].HasValue)
                    {
                        Game.Simulation.Statics.Remove(staticHandles[i].Value);
                        staticHandles[i] = null;
                    }
                }
                return;
            }
            for (int i = 0; i < staticHandles.Length; i++)
            {
                if (!staticHandles[i].HasValue)
                {
                    staticHandles[i] = Game.Simulation.Statics.Add(new StaticDescription(Position, Rotation, shapeIndexes[i]));
                    // Game.Simulation.Statics.UpdateBounds(staticHandles[i].Value);
                }
            }
            */
            for (int i = 0; i < Models.Length; i++)
            {
                Models[i].Tick(dt);
            }
        }

        public override void Dispose()
        {
            for (int i = 0; i < staticHandles.Length; i++)
            {
                if (staticHandles[i].HasValue)
                {
                    FloorLookup.TryRemove(staticHandles[i].Value, out _);
                    Game.Simulation.Statics.Remove(staticHandles[i].Value);
                    staticHandles[i] = null;
                }
                Game.Simulation.Shapes.Remove(shapeIndexes[i]);
            }
            DestroyAudio();
            for (int i = 0; i < Models.Length; i++)
            {
                Models[i].Dispose();
            }
            base.Dispose();
        }
    }
}