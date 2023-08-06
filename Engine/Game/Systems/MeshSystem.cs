using System;
using System.Collections.Generic;
using System.Linq;
using DefaultEcs;
using DefaultEcs.System;
using Engine.Assets.Models;
using Engine.Assets.Rendering;
using Engine.Game.Components;

namespace Engine.Game.Systems
{
    public sealed class MeshSystem : AComponentSystem<double, MeshData>
    {
        public int NextId { get; private set; }
        public Dictionary<int, WeakReference<Mesh>> MeshLookup { get; private set; }
        public Dictionary<int, WeakReference<Material>> MaterialLookup { get; private set; }

        public MeshSystem(World world) : base(world)
        {
            NextId = 0;
            MeshLookup = new Dictionary<int, WeakReference<Mesh>>();
            MaterialLookup = new Dictionary<int, WeakReference<Material>>();
        }

        public int GetMeshId(Mesh mesh)
        {
            var item = MeshLookup.FirstOrDefault(x => x.Value.TryGetTarget(out var m) && m == mesh);
            if (item.Value == null)
                MeshLookup.Add(++NextId, new WeakReference<Mesh>(mesh));
            return MeshLookup.FirstOrDefault(x => x.Value.TryGetTarget(out var m) && m == mesh).Key;
        }

        public int GetMaterialId(Material mat)
        {
            var item = MaterialLookup.FirstOrDefault(x => x.Value.TryGetTarget(out var m) && m == mat);
            if (item.Value == null)
                MaterialLookup.Add(++NextId, new WeakReference<Material>(mat));
            return MaterialLookup.FirstOrDefault(x => x.Value.TryGetTarget(out var m) && m == mat).Key;
        }

        protected override void Update(double state, ref MeshData component)
        {
            if (MaterialLookup[component.MaterialId].TryGetTarget(out Material mat) && MeshLookup[component.MeshId].TryGetTarget(out Mesh mesh))
            {
                mat.PreDraw(Renderer.Current);
                mat.Bind(Renderer.Current);
                mesh.SetWorldMatrix(Renderer.Current, System.Numerics.Matrix4x4.Identity);
                mesh.DrawNow(Renderer.Current);
            }
        }
    }
}