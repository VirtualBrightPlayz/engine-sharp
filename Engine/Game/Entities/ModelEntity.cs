using System;
using System.Collections.Concurrent;
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
        public static ConcurrentDictionary<string, WeakReference<Model>> Models = new ConcurrentDictionary<string, WeakReference<Model>>();
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
            if (Models.TryGetValue(_path, out var mdl) && mdl.TryGetTarget(out var mdl2))
            {
                Model = mdl2;
            }
            else
            {
                Model = new Model(Name, _path, _material, true, false);
                if (Models.ContainsKey(_path))
                    Models.TryRemove(_path, out _);
                Models.TryAdd(_path, new WeakReference<Model>(Model));
            }
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
            Model.SetWorldMatrixDraw(renderer, WorldMatrixUniform, ForwardConsts.ForwardBasePassName);
            for (int i = 1; i < ForwardConsts.LightUniforms.Count; i++)
                Model.SetWorldMatrixDraw(renderer, WorldMatrixUniform, ForwardConsts.ForwardAddPassName + '#' + i);
        }
    }
}