using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
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

        public ModelEntity(string name, string path, Material material) : base(name)
        {
            Create(path, material);
        }

        private async void Create(string path, Material material)
        {
            Model = await ResourceManager.Clone<Model>($"{Name}_{Random.Shared.Next()}", await ResourceManager.LoadModel(Name, material, path));
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