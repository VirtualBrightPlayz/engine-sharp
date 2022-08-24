#if SHADER_COMPILER
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Assimp;
using Engine.Assets.Rendering;
using Newtonsoft.Json;
using SharpGLTF.Geometry;
using SharpGLTF.Geometry.VertexTypes;
using SharpGLTF.Materials;
using SharpGLTF.Memory;
using SharpGLTF.Schema2;
using Veldrid.SPIRV;

public static class ShaderCompiler
{
    [StructLayout(LayoutKind.Sequential)]
    public struct GLTF_VERTEX
    {
        public Vector3 POSITION;
        public Vector3 NORMAL;
        public Vector4 TANGENT;
        public Vector4 COLOR_0;
        public Vector2 TEXCOORD_0;
        public Vector2 TEXCOORD_1;
        public UInt4 JOINTS_0;
        public Vector4 WEIGHTS_0;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct UInt4
    {
        public uint X, Y, Z, W;
        public Vector4 ToVec4() => new Vector4(X, Y, Z, W);
    }

    public static async Task Main(string[] args)
    {
        using AssimpContext ctx = new AssimpContext();
        foreach (var shader in args)
        {
            if (shader.Contains("glsl"))
            {
                var processor = new ShaderProcessor();
                processor.CreateShaders(shader, File.ReadAllText(shader), CreateShaders).Wait();
                Console.WriteLine($"Compiling {shader}.json");
                File.WriteAllText($"{shader}.json", JsonConvert.SerializeObject(processor.Passes));
            }
            else
            {
                Console.WriteLine($"Compiling {shader}.glb");
                var scene = ctx.ImportFile(shader, PostProcessSteps.Triangulate | PostProcessSteps.FlipUVs);
                var root = ModelRoot.CreateModel();
                var glScene = root.UseScene("default");
                // var anim = root.CreateAnimation();
                SharpGLTF.Schema2.Animation anim = null;
                var lookup = new Dictionary<string, SharpGLTF.Schema2.Node>();
                ProcessNode(scene.RootNode, ref anim, scene, ref root, null, ref lookup, Path.GetDirectoryName(shader));
                root.SaveGLB($"{shader}.glb", new WriteSettings()
                {
                    Validation = SharpGLTF.Validation.ValidationMode.Skip,
                });
            }
        }
    }

    private static (SharpGLTF.Schema2.Mesh, Skin) ProcessMesh(Assimp.Mesh aiMesh, Assimp.Scene aiScene, ref ModelRoot root, string dir)
    {
        var aiMaterial = aiScene.Materials[aiMesh.MaterialIndex];
        var material = root.CreateMaterial(aiMaterial.Name);

        if (aiMaterial.GetMaterialTexture(TextureType.BaseColor, 0, out var slot))
        {
            string path = Path.Combine(dir, slot.FilePath);
            if (File.Exists(path))
                material = material.WithPBRMetallicRoughness().WithChannelTexture("BaseColor", 0, path);
        }
        if (aiMaterial.GetMaterialTexture(TextureType.Diffuse, 0, out var slot2))
        {
            string path = Path.Combine(dir, slot2.FilePath);
            if (File.Exists(path))
                material = material.WithPBRMetallicRoughness().WithChannelTexture("BaseColor", 0, path);
        }
            // material = material.WithChannelTexture("Diffuse", 0, slot.FilePath);

        Vector4[] bWeights = new Vector4[aiMesh.VertexCount];
        UInt4[] bInds = new UInt4[aiMesh.VertexCount];
        if (aiMesh.BoneCount > 0)
        {
            for (int j = 0; j < aiMesh.BoneCount; j++)
            {
                for (int k = 0; k < aiMesh.Bones[j].VertexWeightCount; k++)
                {
                    float weight = aiMesh.Bones[j].VertexWeights[k].Weight;
                    int vertId = aiMesh.Bones[j].VertexWeights[k].VertexID;
                    if (bWeights[vertId].X == 0f)
                    {
                        bWeights[vertId].X = weight;
                        bInds[vertId].X = (uint)j;
                    }
                    else if (bWeights[vertId].Y == 0f)
                    {
                        bWeights[vertId].Y = weight;
                        bInds[vertId].Y = (uint)j;
                    }
                    else if (bWeights[vertId].Z == 0f)
                    {
                        bWeights[vertId].Z = weight;
                        bInds[vertId].Z = (uint)j;
                    }
                    else if (bWeights[vertId].W == 0f)
                    {
                        bWeights[vertId].W = weight;
                        bInds[vertId].W = (uint)j;
                    }
                }
            }
        }

        var verts = new GLTF_VERTEX[aiMesh.VertexCount];
        for (int i = 0; i < aiMesh.VertexCount; i++)
        {
            verts[i] = new GLTF_VERTEX()
            {
                POSITION = ToNum3(aiMesh.Vertices[i]),
                NORMAL = ToNum3(aiMesh.HasNormals ? aiMesh.Normals[i] : new Vector3D()),
                TANGENT = ToNum4(aiMesh.HasTangentBasis ? aiMesh.Tangents[i] : new Vector3D()),
                COLOR_0 = ToNumCol(aiMesh.HasVertexColors(0) ? aiMesh.VertexColorChannels[0][i] : new Color4D(1f, 1f, 1f, 1f)),
                TEXCOORD_0 = ToNum2(aiMesh.HasTextureCoords(0) ? aiMesh.TextureCoordinateChannels[0][i] : new Vector3D()),
                TEXCOORD_1 = ToNum2(aiMesh.HasTextureCoords(1) ? aiMesh.TextureCoordinateChannels[1][i] : new Vector3D()),
                JOINTS_0 = bInds[i],
                WEIGHTS_0 = bWeights[i],
            };
        }

        // var matBuilder = new MaterialBuilder(aiMaterial.Name);
        // var builder = new MeshBuilder<VertexPositionNormalTangent, VertexColor1Texture2, VertexJoints4>(aiMesh.Name);
        // builder.UsePrimitive(matBuilder).AddTriangle();
        var mesh = root.CreateMesh(aiMesh.Name);
        var prim = mesh.CreatePrimitive();
        prim.DrawPrimitiveType = SharpGLTF.Schema2.PrimitiveType.TRIANGLES;

        /*
        var size = Marshal.SizeOf<GLTF_VERTEX>();
        byte[] data = GetBytes(verts);
        var buffer = root.CreateBuffer(size * verts.Length);
        Array.Copy(data, buffer.Content, data.Length);
        var bufferView = root.CreateBufferView(size, 0);
        */

        prim = prim.WithVertexAccessor(nameof(GLTF_VERTEX.POSITION), verts.Select(x => x.POSITION).ToArray());
        prim = prim.WithVertexAccessor(nameof(GLTF_VERTEX.NORMAL), verts.Select(x => x.NORMAL).ToArray());
        prim = prim.WithVertexAccessor(nameof(GLTF_VERTEX.TANGENT), verts.Select(x => x.TANGENT).ToArray());
        prim = prim.WithVertexAccessor(nameof(GLTF_VERTEX.COLOR_0), verts.Select(x => x.COLOR_0).ToArray());
        prim = prim.WithVertexAccessor(nameof(GLTF_VERTEX.TEXCOORD_0), verts.Select(x => x.TEXCOORD_0).ToArray());
        prim = prim.WithVertexAccessor(nameof(GLTF_VERTEX.TEXCOORD_1), verts.Select(x => x.TEXCOORD_1).ToArray());
        prim = prim.WithVertexAccessor(nameof(GLTF_VERTEX.JOINTS_0), verts.Select(x => x.JOINTS_0).ToArray());
        prim = prim.WithVertexAccessor(nameof(GLTF_VERTEX.WEIGHTS_0), verts.Select(x => x.WEIGHTS_0).ToArray());
        prim = prim.WithIndicesAccessor(SharpGLTF.Schema2.PrimitiveType.TRIANGLES, aiMesh.GetIndices());
        prim = prim.WithMaterial(material);

        return (mesh, null);
        // return (mesh, root.CreateSkin(aiMesh.Name));

        /*
        AddVertexInfo(root, prim, bufferView, verts.Length, nameof(GLTF_VERTEX.POSITION), DimensionType.VEC3);
        AddVertexInfo(root, prim, bufferView, verts.Length, nameof(GLTF_VERTEX.NORMAL), DimensionType.VEC3);
        AddVertexInfo(root, prim, bufferView, verts.Length, nameof(GLTF_VERTEX.TANGENT), DimensionType.VEC4);
        AddVertexInfo(root, prim, bufferView, verts.Length, nameof(GLTF_VERTEX.COLOR_0), DimensionType.VEC4);
        AddVertexInfo(root, prim, bufferView, verts.Length, nameof(GLTF_VERTEX.TEXCOORD_0), DimensionType.VEC2);
        AddVertexInfo(root, prim, bufferView, verts.Length, nameof(GLTF_VERTEX.TEXCOORD_1), DimensionType.VEC2);
        AddVertexInfo(root, prim, bufferView, verts.Length, nameof(GLTF_VERTEX.JOINTS_0), DimensionType.VEC4);
        AddVertexInfo(root, prim, bufferView, verts.Length, nameof(GLTF_VERTEX.WEIGHTS_0), DimensionType.VEC4);
        */

        /*
        var positionAccessor = root.CreateAccessor(mesh.Name);
        positionAccessor.SetVertexData(buffer, (int)Marshal.OffsetOf<GLTF_VERTEX>(nameof(GLTF_VERTEX.POSITION)), verts.Length, DimensionType.VEC3);
        prim.SetVertexAccessor(nameof(GLTF_VERTEX.POSITION), positionAccessor);

        var normalAccessor = root.CreateAccessor(mesh.Name);
        normalAccessor.SetVertexData(buffer, (int)Marshal.OffsetOf<GLTF_VERTEX>(nameof(GLTF_VERTEX.NORMAL)), verts.Length, DimensionType.VEC3);
        prim.SetVertexAccessor(nameof(GLTF_VERTEX.NORMAL), normalAccessor);

        var tangentAccessor = root.CreateAccessor(mesh.Name);
        tangentAccessor.SetVertexData(buffer, (int)Marshal.OffsetOf<GLTF_VERTEX>(nameof(GLTF_VERTEX.TANGENT)), verts.Length, DimensionType.VEC4);
        prim.SetVertexAccessor(nameof(GLTF_VERTEX.TANGENT), tangentAccessor);
        */
    }

    public static MeshPrimitive WithVertexAccessor(this MeshPrimitive primitive, string attribute, IReadOnlyList<UInt4> values)
    {
        // Guard.NotNull(primitive, nameof(primitive));
        // Guard.NotNull(values, nameof(values));

        var root = primitive.LogicalParent.LogicalParent;

        // create a vertex buffer and fill it
        var view = root.CreateBufferView(16 * values.Count, 0, BufferMode.ARRAY_BUFFER);
        var array = new Vector4Array(view.Content, encoding: EncodingType.UNSIGNED_SHORT);
        array.Fill(values.Select(x => x.ToVec4()));

        var accessor = root.CreateAccessor();

        accessor.SetVertexData(view, 0, values.Count, DimensionType.VEC4, EncodingType.UNSIGNED_SHORT, false);

        primitive.SetVertexAccessor(attribute, accessor);

        return primitive;
    }

    private static MeshPrimitive AddVertexInfo(ModelRoot root, MeshPrimitive prim, SharpGLTF.Schema2.BufferView bufferView, int length, string name, DimensionType type)
    {
        var accessor = root.CreateAccessor(name);
        accessor.SetVertexData(bufferView, (int)Marshal.OffsetOf<GLTF_VERTEX>(name), length, type);
        prim.SetVertexAccessor(name, accessor);
        return prim;
    }

    private static byte[] GetBytes(GLTF_VERTEX[] verts)
    {
        int size = Marshal.SizeOf<GLTF_VERTEX>();
        byte[] data = new byte[size * verts.Length];
        for (int i = 0; i < verts.Length; i++)
        {
            IntPtr ptr = IntPtr.Zero;
            try
            {
                ptr = Marshal.AllocHGlobal(size);
                Marshal.StructureToPtr(verts[i], ptr, true);
                Marshal.Copy(ptr, data, i * size, size);
            }
            finally
            {
                Marshal.FreeHGlobal(ptr);
            }
        }
        return data;
    }

    private static Vector2 ToNum2(Vector2D vector3D)
    {
        return new Vector2(vector3D.X, vector3D.Y);
    }

    private static Vector2 ToNum2(Vector3D vector3D)
    {
        return new Vector2(vector3D.X, vector3D.Y);
    }

    private static Vector4 ToNumCol(Color4D color4D)
    {
        return new Vector4(color4D.R, color4D.G, color4D.B, color4D.A);
    }

    private static Vector3 ToNum3(Vector3D vector3D)
    {
        return new Vector3(vector3D.X, vector3D.Y, vector3D.Z);
    }

    private static Vector4 ToNum4(Vector3D vector3D)
    {
        return new Vector4(vector3D.X, vector3D.Y, vector3D.Z, 1f);
    }

    private static SharpGLTF.Schema2.Node ProcessNode(Assimp.Node aiNode, ref SharpGLTF.Schema2.Animation anim, Assimp.Scene aiScene, ref ModelRoot root, SharpGLTF.Schema2.Node parent, ref Dictionary<string, SharpGLTF.Schema2.Node> lookup, string dir)
    {
        SharpGLTF.Schema2.Node n = null;
        if (parent == null)
            n = root.DefaultScene.CreateNode(aiNode.Name);
            // n = root.CreateLogicalNode();
        else
            n = parent.CreateNode(aiNode.Name);
        lookup.TryAdd(aiNode.Name, n);
        for (int i = 0; i < aiNode.ChildCount; i++)
        {
            var n2 = ProcessNode(aiNode.Children[i], ref anim, aiScene, ref root, n, ref lookup, dir);
        }
        if (aiScene.HasAnimations)
        {
            if (GetChannel(aiNode, aiScene.Animations[0], out var channel))
            {
                // var n2 = n.FindNode(x => x.Name == channel.NodeName);
                var n2 = n;
                // n2.WorldMatrix = ToNum(aiNode.Transform);
                n2.LocalMatrix = ToNum(aiNode.Transform);
                // n2.WithLocalTransform(new SharpGLTF.Transforms.AffineTransform(ToNum3(channel.PositionKeys.First().Value), ToNum(channel.RotationKeys.First().Value), ToNum3(channel.ScalingKeys.First().Value)));
                if (channel.HasPositionKeys)
                {
                    n2.WithTranslationAnimation("Track" + aiScene.Animations[0].Name, channel.PositionKeys.ToDictionary(x => (float)(x.Time * aiScene.Animations[0].TicksPerSecond), x => ToNum3(x.Value)));
                }
                if (channel.HasRotationKeys)
                {
                    n2.WithRotationAnimation("Track" + aiScene.Animations[0].Name, channel.RotationKeys.ToDictionary(x => (float)(x.Time * aiScene.Animations[0].TicksPerSecond), x => ToNum(x.Value)));
                    // anim.CreateRotationChannel(n, channel.RotationKeys.ToDictionary(x => (float)(x.Time * aiScene.Animations[0].TicksPerSecond), x => ToNum(x.Value)));
                }
                if (channel.HasScalingKeys)
                {
                    n2.WithScaleAnimation("Track" + aiScene.Animations[0].Name, channel.ScalingKeys.ToDictionary(x => (float)(x.Time * aiScene.Animations[0].TicksPerSecond), x => ToNum3(x.Value)));
                    // anim.CreateScaleChannel(n, channel.ScalingKeys.ToDictionary(x => (float)(x.Time * aiScene.Animations[0].TicksPerSecond), x => ToNum3(x.Value)));
                }
            }
        }
        SharpGLTF.Schema2.Mesh mesh = null;
        Skin skin = null;
        if (aiNode.HasMeshes && aiNode.MeshCount > 0)
        {
            if (aiScene.Meshes[aiNode.MeshIndices[0]].HasBones)
            {
                (mesh, skin) = ProcessMesh(aiScene.Meshes[aiNode.MeshIndices[0]], aiScene, ref root, dir);
                var ns = SharpGLTF.Schema2.Node.Flatten(n).ToList();
                var lst2 = new List<(SharpGLTF.Schema2.Node, System.Numerics.Matrix4x4)>();
                var lst = new List<SharpGLTF.Schema2.Node>();
                for (int i = 0; i < aiScene.Meshes[aiNode.MeshIndices[0]].BoneCount; i++)
                {
                    lst.Add(lookup[aiScene.Meshes[aiNode.MeshIndices[0]].Bones[i].Name]);
                }
                // Console.WriteLine("Skinned Mesh");
                // lst2.Add((n, n.WorldMatrix));
                foreach (var node in ns)
                {
                    // System.Numerics.Matrix4x4.Invert(node.WorldMatrix, out var inv);
                    var inv = node.LocalMatrix;
                    inv = System.Numerics.Matrix4x4.Identity;
                    lst2.Add((node, inv));
                }
                // root.DefaultScene.CreateNode("Skinned Mesh").WithSkinnedMesh(mesh, n.WorldMatrix, lst.ToArray());
                // root.CreateLogicalNode().WithSkinnedMesh(mesh, n.WorldMatrix, ns.ToArray());
                // root.CreateLogicalNode().WithSkinnedMesh(mesh, lst2.ToArray());
                root.DefaultScene.CreateNode().WithSkinnedMesh(mesh, lst2.ToArray());
            }
            else
            {
                for (int i = 0; i < aiNode.MeshCount; i++)
                {
                    (mesh, skin) = ProcessMesh(aiScene.Meshes[aiNode.MeshIndices[i]], aiScene, ref root, dir);
                    n.CreateNode().WithMesh(mesh);
                }
            }
        }
        return n;
    }

    private static System.Numerics.Matrix4x4 ToNum(Assimp.Matrix4x4 value)
    {
        return System.Numerics.Matrix4x4.Transpose(new System.Numerics.Matrix4x4(value.A1, value.A2, value.A3, value.A4,
            value.B1, value.B2, value.B3, value.B4,
            value.C1, value.C2, value.C3, value.C4,
            value.D1, value.D2, value.D3, value.D4));
    }

    private static System.Numerics.Quaternion ToNum(Assimp.Quaternion value)
    {
        
        return System.Numerics.Quaternion.Inverse(new System.Numerics.Quaternion(value.X, value.Y, value.Z, value.W));
    }

    private static bool GetChannel(Assimp.Node node, Assimp.Animation anim, out NodeAnimationChannel channel)
    {
        foreach (NodeAnimationChannel c in anim.NodeAnimationChannels)
        {
            if (c.NodeName == node.Name)
            {
                channel = c;
                return true;
            }
        }
        channel = null;
        return false;
    }

    private static void CreateShaders(string vertCode, string fragCode, string pass, string path)
    {
        var compileResult = SpirvCompilation.CompileVertexFragment(Encoding.ASCII.GetBytes(vertCode), Encoding.ASCII.GetBytes(fragCode), CrossCompileTarget.ESSL);
        Console.WriteLine($"Compiling {path}.{pass}.json");
        File.WriteAllText($"{path}.{pass}.json", JsonConvert.SerializeObject(compileResult));
    }
}
#endif
