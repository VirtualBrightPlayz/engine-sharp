using System;
using System.Linq;
using System.Threading.Tasks;
using Veldrid;

namespace Engine.Assets.Rendering
{
    public class CompoundBuffer : Resource
    {
        public override bool IsValid => InternalResourceSet != null && !InternalResourceSet.IsDisposed;
        public ResourceSet InternalResourceSet { get; private set; }
        public GraphicsShader InternalShader { get; private set; }
        public IMaterialBindable[] Bindables { get; private set; }
        public uint LayoutIndex { get; private set; }

        public CompoundBuffer(string name, GraphicsShader shader, string layoutName, params IMaterialBindable[] bindables) : this(name, shader, (uint)shader.GetSetIndex(layoutName), bindables)
        {
        }

        public CompoundBuffer(string name, GraphicsShader shader, uint layoutIndex, params IMaterialBindable[] bindables) : base(name)
        {
            InternalShader = shader;
            LayoutIndex = layoutIndex;
            Bindables = bindables;
            ReCreate();
        }

        public bool Contains(GraphicsShader shader, string layoutName, params IMaterialBindable[] bindables)
        {
            return InternalShader == shader && LayoutIndex == (uint)shader.GetSetIndex(layoutName) /*&& Bindables.All(x => Array.IndexOf(bindables, x) != -1)*/;
        }

        public bool Contains(GraphicsShader shader, uint layoutIndex, params IMaterialBindable[] bindables)
        {
            return InternalShader == shader && LayoutIndex == layoutIndex /*&& Bindables.All(x => Array.IndexOf(bindables, x) != -1)*/;
        }

        protected override void ReCreateInternal()
        {
            if (InternalResourceSet != null && !InternalResourceSet.IsDisposed)
                InternalResourceSet.Dispose();
            InternalShader.ReCreate();
            foreach (var item in Bindables)
            {
                item.ReCreate();
            }
            if (LayoutIndex < InternalShader._reflResourceLayouts.Count && LayoutIndex >= 0)
            {
                ResourceSetDescription desc = new ResourceSetDescription(InternalShader._reflResourceLayouts[(int)LayoutIndex], Bindables.SelectMany(x => x.Bindables).ToArray());
                InternalResourceSet = ResourceManager.GraphicsFactory.CreateResourceSet(desc);
                InternalResourceSet.Name = Name;
            }
        }

        protected override Resource CloneInternal(string cloneName)
        {
            throw new NotSupportedException("Can't clone CompoundBuffers");
        }

        protected override void DisposeInternal()
        {
            if (InternalResourceSet != null && !InternalResourceSet.IsDisposed)
                InternalResourceSet.Dispose();
            InternalResourceSet = null;
            // InternalShader.Dispose();
            foreach (var item in Bindables)
            {
                // item.Dispose();
            }
        }
    }
}