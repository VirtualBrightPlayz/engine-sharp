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
        public Dictionary<int, Mesh> MeshLookup { get; private set; }
        public Dictionary<int, Material> MaterialLookup { get; private set; }

        public MeshSystem(World world) : base(world)
        {
            NextId = 0;
            MeshLookup = new Dictionary<int, Mesh>();
            MaterialLookup = new Dictionary<int, Material>();
        }

        public int GetMeshId(Mesh mesh)
        {
            if (!MeshLookup.ContainsValue(mesh))
                MeshLookup.Add(++NextId, mesh);
            return MeshLookup.FirstOrDefault(x => x.Value == mesh).Key;
        }

        public int GetMaterialId(Material mat)
        {
            if (!MaterialLookup.ContainsValue(mat))
                MaterialLookup.Add(++NextId, mat);
            return MaterialLookup.FirstOrDefault(x => x.Value == mat).Key;
        }

        protected override void Update(double state, ref MeshData component)
        {
            MaterialLookup[component.MaterialId].PreDraw(Renderer.Current);
            MaterialLookup[component.MaterialId].Bind(Renderer.Current);
            MeshLookup[component.MeshId].SetWorldMatrix(Renderer.Current, System.Numerics.Matrix4x4.Identity);
            MeshLookup[component.MeshId].DrawNow(Renderer.Current);
        }
    }
}