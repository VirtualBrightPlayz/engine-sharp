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

        public CompoundBuffer(string name, GraphicsShader shader, uint layoutIndex, params IMaterialBindable[] bindables)
        {
            Name = name;
            InternalShader = shader;
            LayoutIndex = layoutIndex;
            Bindables = bindables;
            // ReCreate();
        }

        public bool Contains(GraphicsShader shader, uint layoutIndex, params IMaterialBindable[] bindables)
        {
            return InternalShader == shader && LayoutIndex == layoutIndex /*&& Bindables.All(x => Array.IndexOf(bindables, x) != -1)*/;
        }

        public override async Task ReCreate()
        {
            // if (HasBeenInitialized)
            //     return;
            await base.ReCreate();
            if (InternalResourceSet != null && !InternalResourceSet.IsDisposed)
                InternalResourceSet.Dispose();
            await InternalShader.ReCreate();
            foreach (var item in Bindables)
            {
                await item.ReCreate();
            }
            ResourceSetDescription desc = new ResourceSetDescription(InternalShader._reflResourceLayouts[(int)LayoutIndex], Bindables.SelectMany(x => x.Bindables).ToArray());
            InternalResourceSet = ResourceManager.GraphicsFactory.CreateResourceSet(desc);
            InternalResourceSet.Name = Name;
        }

        public override Task<Resource> Clone(string cloneName)
        {
            throw new NotSupportedException("Can't clone CompoundBuffers");
        }

        public override void Dispose()
        {
            InternalResourceSet.Dispose();
        }
    }
}