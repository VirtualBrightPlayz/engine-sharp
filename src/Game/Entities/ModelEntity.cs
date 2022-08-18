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
            Model = ResourceManager.Clone<Model>($"{name}_{Random.Shared.Next()}", ResourceManager.LoadModel(name, material, path));
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