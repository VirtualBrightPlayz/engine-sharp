using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Engine.Assets.Rendering;
using Veldrid;
using Veldrid.SPIRV;

namespace Engine.Assets.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public struct UInt4
    {
        public uint X, Y, Z, W;
        /*public UInt4(Vector4 value)
        {
            X = (uint)value.X;
            Y = (uint)value.Y;
            Z = (uint)value.Z;
            W = (uint)value.W;
        }*/
        // public Vector4 ToVec4() => new Vector4(X, Y, Z, W);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct VertexPositionColorUV : IVertex
    {
        public Vector3 Position;
        public Vector3 Normal;
        public Vector2 UV0;
        public Vector2 UV1;
        public RgbaFloat Color;
        public Vector4 BoneWeights;
        public UInt4 BoneIndices;

        public VertexPositionColorUV(Vector3 position, Vector3 normal, Vector2 uv0, Vector2 uv1, RgbaFloat color)
        {
            Position = position;
            Normal = normal;
            UV0 = uv0;
            UV1 = uv1;
            Color = color;
            BoneWeights = Vector4.Zero;
            BoneIndices = new UInt4();
        }

        public VertexPositionColorUV(Vector3 position, Vector3 normal, Vector2 uv0, Vector2 uv1, RgbaFloat color, Vector4 boneWeights, UInt4 boneInds)
        {
            Position = position;
            Normal = normal;
            UV0 = uv0;
            UV1 = uv1;
            Color = color;
            BoneWeights = boneWeights;
            BoneIndices = boneInds;
        }

        public const uint SizeInBytes = 88;
    }

    public interface IVertex
    {
    }

    public class Mesh : GraphicsResource
    {
        public override bool IsValid => _vertexBuffer != null && _indexBuffer != null && !_vertexBuffer.IsDisposed && !_indexBuffer.IsDisposed;

        private DeviceBuffer _vertexBuffer;
        private DeviceBuffer _indexBuffer;
        private uint _indexCount;
        private List<Renderer> _hasDrawn = new List<Renderer>();

        public List<uint> Indices { get; set; } = new List<uint>();
        public IReadOnlyList<IVertex> VertexList => _vertexList;
        private List<IVertex> _vertexList = new List<IVertex>();
        public bool IsBigMesh { get; private set; }

        public Mesh(string name, bool isBigMesh) : base(name)
        {
            IsBigMesh = isBigMesh;
            ReCreate();
        }

        protected override void ReCreateInternal()
        {
            if (_vertexBuffer != null && !_vertexBuffer.IsDisposed)
                _vertexBuffer.Dispose();
            if (_indexBuffer != null && !_indexBuffer.IsDisposed)
                _indexBuffer.Dispose();
            _hasDrawn.Clear();
        }

        protected override Resource CloneInternal(string cloneName)
        {
            throw new NotImplementedException();
        }

        public static uint GetFormatSize(VertexElementFormat format)
        {
            // TODO: fill the rest of these out
            switch (format)
            {
                case VertexElementFormat.Float1:
                case VertexElementFormat.Int1:
                case VertexElementFormat.UInt1:
                    return 4 * 1;
                case VertexElementFormat.Float2:
                case VertexElementFormat.Int2:
                case VertexElementFormat.UInt2:
                    return 4 * 2;
                case VertexElementFormat.Float3:
                case VertexElementFormat.Int3:
                case VertexElementFormat.UInt3:
                    return 4 * 3;
                case VertexElementFormat.Float4:
                case VertexElementFormat.Int4:
                case VertexElementFormat.UInt4:
                    return 4 * 4;
                default:
                    return 0;
            }
        }

        public void UploadData<T>(T[] vertices) where T : unmanaged, IVertex
        {
            SetVertexList<T>(vertices);
        }

        public void ClearVertexList()
        {
            _vertexList.Clear();
        }

        public void SetVertexList<T>(T[] vertices) where T : struct, IVertex
        {
            ClearVertexList();
            AddVertexList<T>(vertices);
        }

        public void AddVertexList<T>(T[] vertices) where T : struct, IVertex
        {
            var v = vertices.Cast<IVertex>();
            if (_vertexList.Count == 0 || _vertexList[^1] is T)
                _vertexList.AddRange(vertices.Cast<IVertex>());
            else
                throw new InvalidOperationException($"{typeof(T).FullName} is not valid in the current VertexList");
        }

        public void AddVertex<T>(T vertex) where T : struct, IVertex
        {
            if (_vertexList.Count == 0 || _vertexList[^1] is T)
                _vertexList.Add(vertex);
            else
                throw new InvalidOperationException($"{typeof(T).FullName} is not valid in the current VertexList");
        }

        protected override void DisposeInternal()
        {
            if (_vertexBuffer != null && !_vertexBuffer.IsDisposed)
                _vertexBuffer.Dispose();
            _vertexBuffer = null;
            if (_indexBuffer != null && !_indexBuffer.IsDisposed)
                _indexBuffer.Dispose();
            _indexBuffer = null;
        }

        protected override void BindInternal(Renderer renderer)
        {
            renderer.CommandList.SetVertexBuffer(0, _vertexBuffer);
            renderer.CommandList.SetIndexBuffer(_indexBuffer, IsBigMesh ? IndexFormat.UInt32 : IndexFormat.UInt16);
            renderer.CommandList.DrawIndexed(_indexCount, 1, 0, 0, 0);
        }

        protected override void UploadInternal(Renderer renderer)
        {
            if (!_hasDrawn.Contains(renderer))
                _hasDrawn.Add(renderer);
            UploadDataSkipChecks(renderer);
        }

        protected override bool IsValidForInternal(Renderer renderer)
        {
            return _hasDrawn.Contains(renderer);
        }

        public void UploadData<T>(Renderer renderer) where T : unmanaged, IVertex
        {
            if (_vertexList.Count == 0)
                throw new InvalidOperationException("VertexList contains no values");
            if (_vertexList[^1] is not T)
                throw new InvalidOperationException($"{typeof(T).FullName} is not valid in the current VertexList");
            for (int i = 0; i < renderer.BoundMaterial.Shader._compileResult.VertexElements.Length; i++)
            {
                VertexElementDescription elem = renderer.BoundMaterial.Shader._compileResult.VertexElements[i];
                string elemName = elem.Name;
                FieldInfo info = typeof(T).GetField(elemName, BindingFlags.Instance | BindingFlags.Public);
                if (info == null)
                    throw new InvalidOperationException($"{typeof(T).FullName} does not have a public field named {elemName}");
                uint fsize = (uint)Marshal.SizeOf(info.FieldType);
                uint elemSize = GetFormatSize(elem.Format);
                if (fsize != elemSize || elemSize == 0)
                    throw new InvalidOperationException($"{typeof(T).FullName}.{info.Name} does not follow the vertex format of {elemSize} bytes");
                if (Array.IndexOf(typeof(T).GetFields(), info) != i)
                    throw new InvalidOperationException($"{typeof(T).FullName}.{info.Name} is not in the {i} place in the struct");
            }

            /*if (_vertexBuffer != null && !_vertexBuffer.IsDisposed)
                _vertexBuffer.Dispose();
            if (_indexBuffer != null && !_indexBuffer.IsDisposed)
                _indexBuffer.Dispose();*/

            T[] data = _vertexList.Cast<T>().ToArray();
            uint size = (uint)Unsafe.SizeOf<T>();

            if (_vertexBuffer == null || _vertexBuffer.IsDisposed)
                _vertexBuffer = ResourceManager.GraphicsFactory.CreateBuffer(new BufferDescription((uint)data.Length * size, BufferUsage.VertexBuffer));
            if (_indexBuffer == null || _indexBuffer.IsDisposed)
                _indexBuffer = ResourceManager.GraphicsFactory.CreateBuffer(new BufferDescription((uint)Indices.Count * (uint)(IsBigMesh ? sizeof(uint) : sizeof(ushort)), BufferUsage.IndexBuffer));
            _vertexBuffer.Name = $"{Name}_VertexBuffer";
            _indexBuffer.Name = $"{Name}_IndexBuffer";
            if (renderer != null)
            {
                renderer.CommandList.UpdateBuffer(_vertexBuffer, 0, data);
                if (IsBigMesh)
                    renderer.CommandList.UpdateBuffer(_indexBuffer, 0, Indices.ToArray());
                else
                    renderer.CommandList.UpdateBuffer(_indexBuffer, 0, Indices.Select(x => (ushort)x).ToArray());
            }
            else
            {
                ModelGlobals.GameGraphics.UpdateBuffer(_vertexBuffer, 0, data);
                if (IsBigMesh)
                    ModelGlobals.GameGraphics.UpdateBuffer(_indexBuffer, 0, Indices.ToArray());
                else
                    ModelGlobals.GameGraphics.UpdateBuffer(_indexBuffer, 0, Indices.Select(x => (ushort)x).ToArray());
            }
            _indexCount = (uint)Indices.Count;
        }

        private unsafe void UploadDataSkipChecks(Renderer renderer)
        {
            if (_vertexList.Count == 0)
                return;
            if (_vertexBuffer != null && !_vertexBuffer.IsDisposed)
                _vertexBuffer.Dispose();
            if (_indexBuffer != null && !_indexBuffer.IsDisposed)
                _indexBuffer.Dispose();

            object[] data = _vertexList.Cast<object>().ToArray();

            uint size = (uint)Marshal.SizeOf(data[0]);
            IntPtr ptr = Marshal.AllocHGlobal((int)size * data.Length);
            long lPtr = ptr.ToInt64();
            for (int i = 0; i < data.Length; i++)
            {
                IntPtr dPtr = new IntPtr(lPtr);
                Marshal.StructureToPtr(data[i], dPtr, true);
                lPtr += size;
            }

            _vertexBuffer = ResourceManager.GraphicsFactory.CreateBuffer(new BufferDescription((uint)data.Length * size, BufferUsage.VertexBuffer));
            _indexBuffer = ResourceManager.GraphicsFactory.CreateBuffer(new BufferDescription((uint)Indices.Count * (uint)(IsBigMesh ? sizeof(uint) : sizeof(ushort)), BufferUsage.IndexBuffer));
            _vertexBuffer.Name = $"{Name}_VertexBuffer";
            _indexBuffer.Name = $"{Name}_IndexBuffer";
            if (renderer != null)
            {
                renderer.CommandList.UpdateBuffer(_vertexBuffer, 0, ptr, (uint)data.Length * size);
                if (IsBigMesh)
                    renderer.CommandList.UpdateBuffer(_indexBuffer, 0, Indices.ToArray());
                else
                    renderer.CommandList.UpdateBuffer(_indexBuffer, 0, Indices.Select(x => (ushort)x).ToArray());
            }
            else
            {
                ModelGlobals.GameGraphics.UpdateBuffer(_vertexBuffer, 0, ptr, (uint)data.Length * size);
                if (IsBigMesh)
                    ModelGlobals.GameGraphics.UpdateBuffer(_indexBuffer, 0, Indices.ToArray());
                else
                    ModelGlobals.GameGraphics.UpdateBuffer(_indexBuffer, 0, Indices.Select(x => (ushort)x).ToArray());
            }
            Marshal.FreeHGlobal(ptr);
            _indexCount = (uint)Indices.Count;
        }
    }
}