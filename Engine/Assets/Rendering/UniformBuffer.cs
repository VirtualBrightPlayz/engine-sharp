using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Veldrid;

namespace Engine.Assets.Rendering
{
    public static class UniformConsts
    {
        public const uint ViewProjectionMatrixBufferSet = 0;
        public const string ViewMatrixName = "ViewMatrix";
        public const string ProjectionMatrixName = "ProjectionMatrix";
        public const uint WorldMatrixBufferSet = 1;
        public const string WorldMatrixName = "WorldMatrix";
        public const uint WorldInfoBufferSet = 3;
        public const string WorldInfoName = "WorldInfo";
        public const uint DiffuseTextureSet = 2;
        public const string DiffuseTextureName = "DiffuseTexture";
        public const uint BonesMatrixBufferSet = 3;
        public const string BonesMatrixName = "BonesMatrix";
    }

    public class UniformBuffer : Resource, IMaterialBindable
    {
        public override bool IsValid => InternalBuffer != null && !InternalBuffer.IsDisposed;
        public DeviceBuffer InternalBuffer { get; private set; }
        public uint Size { get; private set; }
        public BindableResource[] Bindables => new BindableResource[]
        {
            InternalBuffer
        };
        private Dictionary<GraphicsShader, Dictionary<uint, ResourceSet>> resSets = new Dictionary<GraphicsShader, Dictionary<uint, ResourceSet>>();

        public UniformBuffer(string name, uint size) : base(name)
        {
            Size = size;
            ReCreate();
        }

        public void Bind(Renderer renderer, Material material, uint setId)
        {
            if (!resSets.ContainsKey(material.Shader))
            {
                resSets[material.Shader] = new Dictionary<uint, ResourceSet>();
            }
            if (!resSets[material.Shader].ContainsKey(setId))
            {
                ResourceSetDescription desc = new ResourceSetDescription(material.Shader._reflResourceLayouts[(int)setId], Bindables);
                resSets[material.Shader][setId] = ResourceManager.GraphicsFactory.CreateResourceSet(desc);
                resSets[material.Shader][setId].Name = Name;
            }
            renderer.CommandList.SetGraphicsResourceSet(setId, resSets[material.Shader][setId]);
        }

        protected override void ReCreateInternal()
        {
            if (InternalBuffer != null && !InternalBuffer.IsDisposed)
                InternalBuffer.Dispose();
            foreach (var kvp in resSets)
            {
                foreach (var set in kvp.Value)
                {
                    set.Value.Dispose();
                }
            }
            resSets.Clear();
            InternalBuffer = ResourceManager.GraphicsFactory.CreateBuffer(new BufferDescription(Size, BufferUsage.UniformBuffer | BufferUsage.Dynamic));
            InternalBuffer.Name = Name;
        }

        protected override Resource CloneInternal(string cloneName)
        {
            throw new NotImplementedException();
        }

        public void UploadData<T>(T[] data) where T : unmanaged
        {
            RenderingGlobals.GameGraphics.UpdateBuffer(InternalBuffer, 0, data);
        }

        public void UploadData<T>(T data) where T : unmanaged
        {
            RenderingGlobals.GameGraphics.UpdateBuffer(InternalBuffer, 0, data);
        }

        public void UploadData<T>(Renderer renderer, T[] data) where T : unmanaged
        {
            // RenderingGlobals.GameGraphics.UpdateBuffer(InternalBuffer, 0, data);
            renderer.CommandList.UpdateBuffer(InternalBuffer, 0, data);
        }

        public void UploadData<T>(Renderer renderer, T data) where T : unmanaged
        {
            // RenderingGlobals.GameGraphics.UpdateBuffer(InternalBuffer, 0, data);
            renderer.CommandList.UpdateBuffer(InternalBuffer, 0, data);
        }

        public void UploadData<T>(uint offsetInBytes, T[] data) where T : unmanaged
        {
            RenderingGlobals.GameGraphics.UpdateBuffer(InternalBuffer, offsetInBytes, data);
        }

        public void UploadData(uint offsetInBytes, uint sizeInBytes, IntPtr data)
        {
            RenderingGlobals.GameGraphics.UpdateBuffer(InternalBuffer, offsetInBytes, data, sizeInBytes);
        }

        protected override void DisposeInternal()
        {
            InternalBuffer?.Dispose();
            // InternalBuffer = null;
        }
    }
}
