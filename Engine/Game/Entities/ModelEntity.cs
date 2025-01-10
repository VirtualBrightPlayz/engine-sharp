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
    public class ModelEntity : Entity
    {
        public Model Model { get; private set; }
        private readonly string _path;
        private readonly Material _material;
        public UniformBuffer WorldMatrixUniform { get; private set; }

        public ModelEntity(string name, string path, Material material) : base(name)
        {
            _path = path;
            _material = material;
        }

        public virtual void Create()
        {
            Model = new Model(Name, _path, _material, false, false);
            // Model = await ResourceManager.Clone<Model>($"{Name}_{Random.Shared.Next()}", await ResourceManager.LoadModel(Name, _material, _path));
            WorldMatrixUniform = new UniformBuffer(UniformConsts.WorldMatrixName, (uint)16 * 4);
        }

        public override void PreDraw(Renderer renderer, double dt)
        {
            base.PreDraw(renderer, dt);
            WorldMatrixUniform.UploadData(WorldMatrix);
        }

        public override void Draw(Renderer renderer, double dt)
        {
            base.Draw(renderer, dt);
            Model.SetWorldMatrixDraw(renderer, WorldMatrixUniform);
        }
    }
}