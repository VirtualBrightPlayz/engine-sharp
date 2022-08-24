using System;
using System.Numerics;
using System.Threading.Tasks;
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
        private readonly string path;
        private Material material;

        public EnemyEntity(string name, string path, Material material) : base(name)
        {
            this.path = path;
            this.material = material;
            // Create(path, material);
        }

        public async Task Create()
        {
            var mat = await ResourceManager.Clone<Material>($"{Name}_Material", material);
            // Model = await ResourceManager.Clone<Model>($"{Name}_{Random.Shared.Next()}", await ResourceManager.LoadModel(Name, material, path, false, true));
            Model = await ResourceManager.LoadModel(Name, mat, path, false, true);
            Model.CompoundBuffers.Clear();
            Model.CompoundBuffers.Add(buffer);
        }

        public override async Task Draw(Renderer renderer, double dt)
        {
            /*if (!fog)
            {
                BGame.Instance.fogUniform.UploadData(renderer, new Vector4(0f, 0f, 0f, 100f));
            }*/
            if (Model.AnimationTime < startAnimTime)
            {
                Model.AnimationTime = startAnimTime;
            }
            Model.AnimationTime += dt * 60f * animSpeed;
            while (Model.AnimationTime >= endAnimTime)
            {
                Model.AnimationTime -= endAnimTime - startAnimTime;
            }
            await base.Draw(renderer, dt);
            await Model.SetWorldMatrixDraw(renderer, WorldMatrix);
            /*if (!fog)
            {
                BGame.Instance.fogUniform.UploadData(renderer, BGame.Instance.fogData);
            }*/
        }

        public override async Task Tick(double dt)
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
            await base.Tick(dt);
        }
    }
}