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

        public ModelEntity(string name, string path, Material material) : base(name)
        {
            _path = path;
            _material = material;
            // Create(path, material);
        }

        public virtual async Task Create()
        {
            Model = await ResourceManager.Clone<Model>($"{Name}_{Random.Shared.Next()}", await ResourceManager.LoadModel(Name, _material, _path));
        }

        public override async Task Draw(Renderer renderer, double dt)
        {
            await base.Draw(renderer, dt);
            await Model.SetWorldMatrixDraw(renderer, WorldMatrix);
        }
    }
}