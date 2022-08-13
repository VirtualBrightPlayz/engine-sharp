using System;
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

        public UniformBuffer(string name, uint size)
        {
            Name = name;
            Size = size;
            ReCreate();
        }

        public override void ReCreate()
        {
            // if (HasBeenInitialized)
                // return;
            // base.ReCreate();
            if (InternalBuffer != null && !InternalBuffer.IsDisposed)
                InternalBuffer.Dispose();
            InternalBuffer = ResourceManager.GraphicsFactory.CreateBuffer(new BufferDescription(Size, BufferUsage.UniformBuffer | BufferUsage.Dynamic));
            InternalBuffer.Name = Name;
        }

        public override Resource Clone(string cloneName)
        {
            throw new NotImplementedException();
        }

        public void UploadData<T>(T[] data) where T : struct
        {
            Program.GameGraphics.UpdateBuffer(InternalBuffer, 0, data);
        }

        public void UploadData<T>(T data) where T : struct
        {
            Program.GameGraphics.UpdateBuffer(InternalBuffer, 0, data);
        }

        public void UploadData<T>(uint offsetInBytes, T[] data) where T : struct
        {
            Program.GameGraphics.UpdateBuffer(InternalBuffer, offsetInBytes, data);
        }

        public void UploadData(uint offsetInBytes, uint sizeInBytes, IntPtr data)
        {
            Program.GameGraphics.UpdateBuffer(InternalBuffer, offsetInBytes, data, sizeInBytes);
        }

        public override void Dispose()
        {
            if (InternalBuffer != null)
                InternalBuffer.Dispose();
        }
    }
}
