using System;
using System.IO;
using System.Linq;
using System.Numerics;
using BepuPhysics;
using BepuPhysics.Collidables;
using BepuUtilities;
using DotRecast.Recast;
using DotRecast.Recast.Geom;
using Engine.Assets.Audio;
using Engine.Assets.Models;
using Engine.Assets.Rendering;
using Engine.Game;
using Engine.Game.Entities;
using ImGuiNET;

namespace GameSrc.NPCs
{
    public class SCP173 : PhysicsEntity
    {
        private Vector3[] path = Array.Empty<Vector3>();
        private int pathIndex = 0;
        public bool isMoving = false;
        public float speed = 38f;// / 8f;
        public Model Model { get; private set; }
        private readonly string _path;
        private readonly Material _material;
        public UniformBuffer WorldMatrixUniform { get; private set; }
        public Capsule shape;
        public AudioSource stoneSource;
        public AudioClip stoneClip;
        public Matrix4x4 RenderWorldMatrix => Matrix4x4.CreateFromQuaternion(Rotation) * Matrix4x4.CreateScale(Scale) * Matrix4x4.CreateTranslation(Position - QuaternionEx.Transform(Vector3.UnitY * shape.HalfLength, Rotation));

        public SCP173(string name, string path, Material material) : base(name)
        {
            _path = path;
            _material = material;
            Create();
        }

        public void Create()
        {
            // rendering
            if (SCPCB.Models.TryGetValue(_path, out var mdl))
            {
                Model = mdl;
            }
            else
            {
                Model = new Model(Name, _path, _material, true, false);
                SCPCB.Models.TryAdd(_path, Model);
            }
            WorldMatrixUniform = new UniformBuffer(UniformConsts.WorldMatrixName, (uint)16 * 4);
            // physics
            shape = new Capsule(0.1f, 0.2f);
            shapeIndex = Game.Simulation.Shapes.Add(shape);
            var inertia = shape.ComputeInertia(1f);
            bodyHandle = Game.Simulation.Bodies.Add(BodyDescription.CreateDynamic(Position, inertia, new CollidableDescription(shapeIndex.Value, 0.1f, float.PositiveInfinity, ContinuousDetection.Continuous()), shape.Radius * 0.02f));
            // audio
            stoneClip = new AudioClip(Path.Combine(SCPCB.Instance.Data.SFXDir, "SCP", "173", "StoneDrag.ogg"));
            stoneSource = new AudioSource(Name);
            stoneSource.SetBuffer(stoneClip);
            stoneSource.Looping = true;
            stoneSource.MaxDistance = 8f;
        }

        public void UpdatePath()
        {
            path = SCPCB.Instance.NavMap.SimpleGetPath(Position, SCPCB.Instance.player.Position);
            pathIndex = path.Length - 1;
        }

        public override void PreDraw(Renderer renderer, double dt)
        {
            base.PreDraw(renderer, dt);
            WorldMatrixUniform.UploadData(RenderWorldMatrix);
        }

        public override void Draw(Renderer renderer, double dt)
        {
            base.Draw(renderer, dt);
            Model.SetWorldMatrixDraw(renderer, WorldMatrixUniform, ForwardConsts.ForwardBasePassName);
            for (int i = 1; i < ForwardConsts.LightUniforms.Count; i++)
                Model.SetWorldMatrixDraw(renderer, WorldMatrixUniform, ForwardConsts.ForwardAddPassName + '#' + i);
            if (ImGui.Begin(Name))
            {
                ImGui.Text($"pathIndex = {pathIndex}");
                ImGui.Text($"path.Length = {path.Length}");
                if (ImGui.Button("Update Path"))
                    UpdatePath();
                if (ImGui.Button("Update Map"))
                    SCPCB.Instance.RebuildNavMap();
                ImGui.Checkbox("IsMoving", ref isMoving);
            }
            ImGui.End();
        }

        public override void Tick(double dt)
        {
            base.Tick(dt);
            stoneSource.Position = Position;
            Vector3 dir = Vector3.Zero;
            if (!bodyHandle.HasValue || SCPCB.Instance.player == null)
            {
                stoneSource.Gain = 0f;
                return;
            }
            Game.Simulation.Bodies[bodyHandle.Value].Velocity.Angular = Vector3.Zero;
            if (Vector3.Dot(SCPCB.Instance.player.viewDirection, Vector3.Normalize(Position - SCPCB.Instance.player.Position)) > 0.5f || !isMoving)
            {
                stoneSource.Gain = 0f;
                Game.Simulation.Bodies[bodyHandle.Value].Velocity.Linear = -Vector3.UnitY;
                return;
            }
            if (pathIndex < 0 || pathIndex >= path.Length || Vector3.DistanceSquared(SCPCB.Instance.player.Position, Position) < 2f * 2f)
            {
                UpdatePath();
                if (SCPCB.Instance.player.Position != Position)
                {
                    dir = Vector3.Normalize(SCPCB.Instance.player.Position - Position);
                }
            }
            else if (path[pathIndex] != Position)
            {
                dir = Vector3.Normalize(path[pathIndex] - Position);
            }
            dir.Y = 0f;
            Game.Simulation.Awakener.AwakenBody(bodyHandle.Value);
            Game.Simulation.Bodies[bodyHandle.Value].Velocity.Linear = (dir * speed) - Vector3.UnitY;
            if (!stoneSource.IsPlaying)
                stoneSource.Play();
            if (dir.LengthSquared() > 0.1f)
            {
                stoneSource.Gain = 1f;
                Rotation = Quaternion.CreateFromAxisAngle(LocalUp, MathF.Atan2(dir.X, dir.Z));
                MarkTransformDirty(TransformDirtyFlags.Rotation);
            }
            else
            {
                stoneSource.Gain = 0f;
            }
            Vector3 dir2 = Vector3.Zero;
            if (pathIndex - 1 >= 0)
                Vector3.Normalize(path[pathIndex - 1] - Position);
            dir2.Y = 0f;
            Vector3 t0 = path[pathIndex];
            t0.Y = 0f;
            Vector3 t1 = Position;
            t1.Y = 0f;
            if (/*Vector3.Dot(dir, dir2) < 0f ||*/ Vector3.Distance(t1, t0) < 1f /*|| dir.LengthSquared() < 0.1f*/)
            {
                pathIndex--;
            }
            // MarkTransformDirty(TransformDirtyFlags.Position);
        }
    }
}