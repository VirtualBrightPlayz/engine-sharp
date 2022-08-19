using System;
using System.Numerics;
using BepuUtilities;
using Engine.Assets;
using Engine.Assets.Models;
using Engine.Assets.Rendering;
using Engine.Game.Entities;

namespace GameBSrc
{
    public class EnemyEntity : Entity
    {
        public Model Model { get; private set; }
        public float speed;
        public double startAnimTime;
        public double endAnimTime;
        public double animSpeed = 1d;
        public CompoundBuffer buffer;
        public bool fog = true;
        public Vector3 Center => Position + Vector3.UnitY;

        public EnemyEntity(string name, string path, Material material) : base(name)
        {
            Model = ResourceManager.Clone<Model>($"{name}_{Random.Shared.Next()}", ResourceManager.LoadModel(name, material, path, false, true));
        }

        public override void Draw(Renderer renderer, double dt)
        {
            if (!fog)
            {
                BGame.Instance.fogUniform.UploadData(renderer, new Vector4(0f, 0f, 0f, 100f));
            }
            if (Model.AnimationTime < startAnimTime)
            {
                Model.AnimationTime = startAnimTime;
            }
            Model.AnimationTime += dt * 60f * animSpeed;
            while (Model.AnimationTime >= endAnimTime)
            {
                Model.AnimationTime -= endAnimTime - startAnimTime;
            }
            Model.SetWorldMatrixDraw(renderer, WorldMatrix);
            base.Draw(renderer, dt);
            if (!fog)
            {
                BGame.Instance.fogUniform.UploadData(renderer, BGame.Instance.fogData);
            }
        }

        public override void Tick(double dt)
        {
            var pos = BGame.Instance.player.Position;
            pos = Position - pos;
            pos = Vector3.Normalize(pos);
            pos.Y = 0f;
            pos.Z *= -1f;
            Rotation = Quaternion.CreateFromRotationMatrix(Matrix4x4.CreateLookAt(Position, Position + pos, Vector3.UnitY));
            QuaternionEx.Transform(-Vector3.UnitZ, Rotation, out var forward);
            Position += forward * speed * 60f * (float)dt;
            MarkTransformDirty(TransformDirtyFlags.Position | TransformDirtyFlags.Rotation);
            base.Tick(dt);
        }
    }
}