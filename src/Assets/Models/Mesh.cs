using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Text;
using Engine.Assets.Rendering;
using Veldrid;
using Veldrid.SPIRV;

namespace Engine.Assets.Models
{
    [StructLayout(LayoutKind.Sequential)]
    public struct UInt4
    {
        public uint X, Y, Z, W;
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

    public class Mesh : Resource
    {
        public override bool IsValid => _vertexBuffer != null && _indexBuffer != null && !_vertexBuffer.IsDisposed && !_indexBuffer.IsDisposed;

        private DeviceBuffer _vertexBuffer;
        private DeviceBuffer _indexBuffer;
        private uint _indexCount;
        public Material InternalMaterial { get; private set; }
        public CompoundBuffer InternalWorldBuffer { get; private set; }
        public UniformBuffer InternalWorldUniform { get; private set; }
        public List<uint> Indices { get; set; } = new List<uint>();
        public IReadOnlyList<IVertex> VertexList => _vertexList;
        private List<IVertex> _vertexList = new List<IVertex>();
        [Obsolete]
        public List<Vector3> Vertices { get; set; } = new List<Vector3>();
        [Obsolete]
        public List<Vector3> Normals { get; set; } = new List<Vector3>();
        [Obsolete]
        public List<Vector2> UV0s { get; set; } = new List<Vector2>();
        [Obsolete]
        public List<Vector2> UV1s { get; set; } = new List<Vector2>();
        [Obsolete]
        public List<Vector4> BoneWeights { get; set; } = new List<Vector4>();
        [Obsolete]
        public List<UInt4> BoneIndices { get; set; } = new List<UInt4>();
        [Obsolete]
        public List<RgbaFloat> Colors { get; set; } = new List<RgbaFloat>();
        public bool IsBigMesh { get; private set; }

        public Matrix4x4 WorldMatrix { get; set; } = Matrix4x4.Identity;

        public Mesh(string name, bool isBigMesh, Material material)
        {
            Name = name;
            InternalMaterial = material;
            InternalWorldUniform = new UniformBuffer("WorldUniform", (uint)16 * 4);
            InternalWorldBuffer = new CompoundBuffer("WorldBuffer", material.Shader, UniformConsts.WorldMatrixBufferSet, InternalWorldUniform);
            // InternalWorldBuffer = ResourceManager.CreateCompoundBuffer("WorldBuffer", material.Shader, UniformConsts.WorldMatrixBufferSet, InternalWorldUniform);
            IsBigMesh = isBigMesh;
        }

        public override void ReCreate()
        {
            if (HasBeenInitialized)
                return;
            base.ReCreate();
            if (_vertexBuffer != null && !_vertexBuffer.IsDisposed)
                _vertexBuffer.Dispose();
            if (_indexBuffer != null && !_indexBuffer.IsDisposed)
                _indexBuffer.Dispose();
            if (InternalMaterial != null)
                InternalMaterial.ReCreate();
            if (InternalWorldUniform != null)
                InternalWorldUniform.ReCreate();
            if (InternalWorldBuffer != null)
                InternalWorldBuffer.ReCreate();
            UploadDataSkipChecks();
        }

        public override Resource Clone(string cloneName)
        {
            Mesh m = new Mesh(cloneName, IsBigMesh, InternalMaterial);
            m.Vertices = Vertices.ToList();
            m.Normals = Normals.ToList();
            m.UV0s = UV0s.ToList();
            m.UV1s = UV1s.ToList();
            m.BoneWeights = BoneWeights.ToList();
            m.BoneIndices = BoneIndices.ToList();
            m.Colors = Colors.ToList();
            m.ReCreate();
            return m;
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

        public void UploadData<T>(T[] vertices) where T : struct, IVertex
        {
            SetVertexList<T>(vertices);
            UploadData<T>();
        }

        public void UploadData<T>() where T : struct, IVertex
        {
            if (_vertexList.Count == 0)
                throw new InvalidOperationException("VertexList contains no values");
            if (_vertexList[^1] is not T)
                throw new InvalidOperationException($"{typeof(T).FullName} is not valid in the current VertexList");
            for (int i = 0; i < InternalMaterial.Shader._compileResult.Reflection.VertexElements.Length; i++)
            {
                VertexElementDescription elem = InternalMaterial.Shader._compileResult.Reflection.VertexElements[i];
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

            if (_vertexBuffer != null && !_vertexBuffer.IsDisposed)
                _vertexBuffer.Dispose();
            if (_indexBuffer != null && !_indexBuffer.IsDisposed)
                _indexBuffer.Dispose();

            T[] data = _vertexList.Cast<T>().ToArray();
            uint size = (uint)Unsafe.SizeOf<T>();

            _vertexBuffer = ResourceManager.GraphicsFactory.CreateBuffer(new BufferDescription((uint)data.Length * size, BufferUsage.VertexBuffer));
            _indexBuffer = ResourceManager.GraphicsFactory.CreateBuffer(new BufferDescription((uint)Indices.Count * (uint)(IsBigMesh ? sizeof(uint) : sizeof(ushort)), BufferUsage.IndexBuffer));
            _vertexBuffer.Name = $"{Name}_VertexBuffer";
            _indexBuffer.Name = $"{Name}_IndexBuffer";
            Program.GameGraphics.UpdateBuffer(_vertexBuffer, 0, data);
            if (IsBigMesh)
                Program.GameGraphics.UpdateBuffer(_indexBuffer, 0, Indices.ToArray());
            else
                Program.GameGraphics.UpdateBuffer(_indexBuffer, 0, Indices.Select(x => (ushort)x).ToArray());
            _indexCount = (uint)Indices.Count;
        }

        private unsafe void UploadDataSkipChecks()
        {
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
            Program.GameGraphics.UpdateBuffer(_vertexBuffer, 0, ptr, (uint)data.Length * size);
            Marshal.FreeHGlobal(ptr);
            if (IsBigMesh)
                Program.GameGraphics.UpdateBuffer(_indexBuffer, 0, Indices.ToArray());
            else
                Program.GameGraphics.UpdateBuffer(_indexBuffer, 0, Indices.Select(x => (ushort)x).ToArray());
            _indexCount = (uint)Indices.Count;
        }

        [Obsolete]
        public void UploadDataOld()
        {
            VertexPositionColorUV[] data = new VertexPositionColorUV[Vertices.Count];
            for (int i = 0; i < data.Length; i++)
            {
                if (BoneWeights.Any() && BoneIndices.Any())
                {
                    data[i] = new VertexPositionColorUV(Vertices[i], Normals[i], UV0s[i], UV1s[i], Colors[i], BoneWeights[i], BoneIndices[i]);
                }
                else
                {
                    data[i] = new VertexPositionColorUV(Vertices[i], Normals[i], UV0s[i], UV1s[i], Colors[i]);
                }
            }
            UploadData<VertexPositionColorUV>(data);
            /*
            return;
            if (_vertexBuffer != null && !_vertexBuffer.IsDisposed)
                _vertexBuffer.Dispose();
            if (_indexBuffer != null && !_indexBuffer.IsDisposed)
                _indexBuffer.Dispose();
            VertexPositionColorUV[] data = new VertexPositionColorUV[Vertices.Count];
            for (int i = 0; i < data.Length; i++)
            {
                if (BoneWeights.Any() && BoneIndices.Any())
                {
                    data[i] = new VertexPositionColorUV(Vertices[i], Normals[i], UV0s[i], UV1s[i], Colors[i], BoneWeights[i], BoneIndices[i]);
                }
                else
                {
                    data[i] = new VertexPositionColorUV(Vertices[i], Normals[i], UV0s[i], UV1s[i], Colors[i]);
                }
            }
            _vertexBuffer = ResourceManager.GraphicsFactory.CreateBuffer(new BufferDescription((uint)data.Length * VertexPositionColorUV.SizeInBytes, BufferUsage.VertexBuffer));
            _indexBuffer = ResourceManager.GraphicsFactory.CreateBuffer(new BufferDescription((uint)Indices.Count * (uint)(IsBigMesh ? sizeof(uint) : sizeof(ushort)), BufferUsage.IndexBuffer));
            _vertexBuffer.Name = $"{Name}_VertexBuffer";
            _indexBuffer.Name = $"{Name}_IndexBuffer";
            Program.GameGraphics.UpdateBuffer(_vertexBuffer, 0, data);
            if (IsBigMesh)
                Program.GameGraphics.UpdateBuffer(_indexBuffer, 0, Indices.ToArray());
            else
                Program.GameGraphics.UpdateBuffer(_indexBuffer, 0, Indices.Select(x => (ushort)x).ToArray());
            _indexCount = (uint)Indices.Count;
            */
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

        public void AddVertex(Vector3 position, Vector3 normal, Vector2 uv0, Vector2 uv1, RgbaFloat color)
        {
            Vertices.Add(position);
            Normals.Add(normal);
            UV0s.Add(uv0);
            UV1s.Add(uv1);
            Colors.Add(color);
            BoneWeights.Clear();
            BoneIndices.Clear();
        }

        public void AddVertex(Vector3 position, Vector3 normal, Vector2 uv0, Vector2 uv1, RgbaFloat color, Vector4 weight, UInt4 index)
        {
            Vertices.Add(position);
            Normals.Add(normal);
            UV0s.Add(uv0);
            UV1s.Add(uv1);
            Colors.Add(color);
            BoneWeights.Add(weight);
            BoneIndices.Add(index);
        }

        public void SetWorldMatrix(Matrix4x4 worldBuffer)
        {
            InternalWorldUniform.UploadData(Renderer.Current, worldBuffer);
            InternalMaterial.SetUniforms(UniformConsts.WorldMatrixBufferSet, InternalWorldBuffer);
        }

        public void PreDraw(Renderer renderer)
        {
            renderer.SetupStandardMatrixUniforms(InternalMaterial);
            InternalMaterial.PreDraw();
        }

        public void Draw(Renderer renderer, string pass = "")
        {
            InternalMaterial.Bind(renderer, pass);
            renderer.CommandList.SetVertexBuffer(0, _vertexBuffer);
            renderer.CommandList.SetIndexBuffer(_indexBuffer, IsBigMesh ? IndexFormat.UInt32 : IndexFormat.UInt16);
            renderer.CommandList.DrawIndexed(_indexCount, 1, 0, 0, 0);
        }

        public override void Dispose()
        {
            InternalWorldBuffer.Dispose();
            InternalWorldUniform.Dispose();
            _vertexBuffer.Dispose();
            _indexBuffer.Dispose();
        }
    }
}