#if SHADER_COMPILER
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using Engine.Assets.Rendering;
using Newtonsoft.Json;
using SharpGLTF.Geometry;
using SharpGLTF.Geometry.VertexTypes;
using SharpGLTF.Materials;
using SharpGLTF.Memory;
using SharpGLTF.Schema2;
using Veldrid.SPIRV;
using File = System.IO.File;
using Assimp = Silk.NET.Assimp;

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

    public struct TextureSlot
    {
        public string FilePath;
        public Assimp.TextureType TextureType;
        public int TextureIndex;
        public Assimp.TextureMapping Mapping;
        public int UVIndex;
        public float BlendFactor;
        public Assimp.TextureOp Operation;
        public Assimp.TextureWrapMode WrapModeU;
        public Assimp.TextureWrapMode WrapModeV;
        public int Flags;
    }

    public static unsafe async Task Main(string[] args)
    {
        using Assimp.Assimp ctx = Assimp.Assimp.GetApi();
        foreach (var shader in args)
        {
            if (shader.Contains("glsl"))
            {
                var processor = new ShaderProcessor();
                processor.CreateShaders(shader, File.ReadAllText(shader), CreateShaders).Wait();
                Log.Info(nameof(ShaderCompiler), $"Compiling {shader}.json");
                File.WriteAllText($"{shader}.json", JsonConvert.SerializeObject(processor.Passes));
            }
            else
            {
                Log.Info(nameof(ShaderCompiler), $"Compiling {shader}.glb");
                var scene = ctx.ImportFile(shader, PostProcessSteps.Triangulate | PostProcessSteps.FlipUVs);
                var root = ModelRoot.CreateModel();
                var glScene = root.UseScene("default");
                // var anim = root.CreateAnimation();
                SharpGLTF.Schema2.Animation anim = null;
                var lookup = new Dictionary<string, SharpGLTF.Schema2.Node>();
                ProcessNode(scene->MRootNode, ref anim, scene, ref root, null, ref lookup, Path.GetDirectoryName(shader));
                foreach (var node in root.LogicalNodes)
                {
                    // if (node.Skin == null)
                    //     node.LocalMatrix = Matrix4x4.Identity;
                }
                root.SaveGLB($"{shader}.glb", new WriteSettings()
                {
                    Validation = SharpGLTF.Validation.ValidationMode.Skip,
                });
            }
        }
    }

    // https://github.com/assimp/assimp-net/blob/master/AssimpNet/Material.cs
    private static string FullyQualifyTextureName(string baseName, Assimp.TextureType type, int texIndex)
    {
        if (string.IsNullOrEmpty(baseName))
            return string.Empty;
        return string.Format("{0},{1},{2}", baseName, (int)type, texIndex);
    }

    private static unsafe Assimp.MaterialProperty* GetProperty(Assimp.Material* material, string fqn)
    {
        for (int i = 0; i < material->MNumProperties; i++)
        {
            if (material->MProperties[i]->MKey == fqn)
            {
                return material->MProperties[i];
            }
        }
        return null;
    }

    private static unsafe string PropGetStringValue(Assimp.MaterialProperty* prop)
    {
        if (prop->MType != Assimp.PropertyTypeInfo.String)
            return null;
        return Encoding.UTF8.GetString(prop->MData, (int)prop->MDataLength); // TODO: does this work?
    }

    private static unsafe int PropGetInt32Value(Assimp.MaterialProperty* prop)
    {
        if (prop->MType != Assimp.PropertyTypeInfo.Integer && prop->MType != Assimp.PropertyTypeInfo.Float)
            return default;
        return BitConverter.ToInt32(new ReadOnlySpan<byte>(prop->MData, (int)prop->MDataLength));
    }

    private static unsafe float PropGetSingleValue(Assimp.MaterialProperty* prop)
    {
        if (prop->MType != Assimp.PropertyTypeInfo.Integer && prop->MType != Assimp.PropertyTypeInfo.Float)
            return default;
        return BitConverter.ToSingle(new ReadOnlySpan<byte>(prop->MData, (int)prop->MDataLength));
    }

    private static unsafe bool GetMaterialTexture(Assimp.Material* material, Assimp.TextureType type, int index, out TextureSlot texture)
    {
        string texName = FullyQualifyTextureName(Assimp.Assimp.MatkeyTextureBase, type, index);
        var texNameProp = GetProperty(material, texName);
        if (texNameProp == null)
        {
            texture = new TextureSlot();
            return false;
        }

        string mappingName = FullyQualifyTextureName(Assimp.Assimp.MatkeyMappingBase, type, index);
        string uvIndexName = FullyQualifyTextureName(Assimp.Assimp.MatkeyUvwsrcBase, type, index);
        string blendFactorName = FullyQualifyTextureName(Assimp.Assimp.MatkeyTexblendBase, type, index);
        string texOpName = FullyQualifyTextureName(Assimp.Assimp.MatkeyTexopBase, type, index);
        string uMapModeName = FullyQualifyTextureName(Assimp.Assimp.MatkeyMappingmodeUBase, type, index);
        string vMapModeName = FullyQualifyTextureName(Assimp.Assimp.MatkeyMappingmodeVBase, type, index);
        string texFlagsName = FullyQualifyTextureName(Assimp.Assimp.MatkeyTexflagsBase, type, index);

        var mapping = GetProperty(material, mappingName);
        var uvIndex = GetProperty(material, uvIndexName);
        var blendFactor = GetProperty(material, blendFactorName);
        var texOp = GetProperty(material, texOpName);
        var uMapMode = GetProperty(material, uMapModeName);
        var vMapMode = GetProperty(material, vMapModeName);
        var texFlags = GetProperty(material, texFlagsName);

        texture = new TextureSlot();

        texture.FilePath = PropGetStringValue(texNameProp);
        texture.TextureType = type;
        texture.TextureIndex = index;
        texture.Mapping = (mapping != null) ? (Assimp.TextureMapping)PropGetInt32Value(mapping) : Assimp.TextureMapping.UV;
        texture.UVIndex = (uvIndex != null) ? PropGetInt32Value(uvIndex) : 0;
        texture.BlendFactor = (blendFactor != null) ? PropGetSingleValue(blendFactor) : 0f;
        texture.Operation = (texOp != null) ? (Assimp.TextureOp)PropGetSingleValue(texOp) : Assimp.TextureOp.Multiply;
        texture.WrapModeU = (uMapMode != null) ? (Assimp.TextureWrapMode)PropGetInt32Value(uMapMode) : Assimp.TextureWrapMode.Wrap;
        texture.WrapModeV = (vMapMode != null) ? (Assimp.TextureWrapMode)PropGetInt32Value(vMapMode) : Assimp.TextureWrapMode.Wrap;
        texture.Flags = (texFlags != null) ? PropGetInt32Value(texFlags) : 0;

        return true;
    }

    private static unsafe (SharpGLTF.Schema2.Mesh, Skin) ProcessMesh(Assimp.Mesh* aiMesh, Assimp.Scene* aiScene, Node node, ref ModelRoot root, string dir)
    {
        var aiMaterial = aiScene->MMaterials[aiMesh->MMaterialIndex];
        var material = root.CreateMaterial(/*aiMaterial.Name*/);//TODO: re-add material names

        if (GetMaterialTexture(aiMaterial, Assimp.TextureType.BaseColor, 0, out var slot))
        {
            string path = Path.Combine(dir, slot.FilePath);
            if (File.Exists(path))
                material = material.WithPBRMetallicRoughness().WithChannelTexture("BaseColor", 0, path);
        }
        if (GetMaterialTexture(aiMaterial, Assimp.TextureType.Diffuse, 0, out var slot2))
        {
            string path = Path.Combine(dir, slot2.FilePath);
            if (File.Exists(path))
                material = material.WithPBRMetallicRoughness().WithChannelTexture("BaseColor", 0, path);
        }
            // material = material.WithChannelTexture("Diffuse", 0, slot.FilePath);

        Matrix4x4[] bOffsets = new Matrix4x4[aiMesh->MNumVertices];
        // Array.Fill(bOffsets, new Matrix4x4());
        Array.Fill(bOffsets, Matrix4x4.Identity);
        Vector4[] bWeights = new Vector4[aiMesh->MNumVertices];
        UInt4[] bInds = new UInt4[aiMesh->MNumVertices];
        if (aiMesh->MNumBones > 0)
        {
            for (int j = 0; j < aiMesh->MNumBones; j++)
            {
                Matrix4x4 mat = ToNum(aiMesh->MBones[j]->MOffsetMatrix);
                // mat = node.WorldMatrix * mat;
                // Matrix4x4.Invert(mat, out mat);
                for (int k = 0; k < aiMesh->MBones[j]->MNumWeights; k++)
                {
                    float weight = aiMesh->MBones[j]->MWeights[k].MWeight;
                    uint vertId = aiMesh->MBones[j]->MWeights[k].MVertexId;
                    // bOffsets[vertId] = Matrix4x4.Add(bOffsets[vertId], mat * weight);
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

        var verts = new GLTF_VERTEX[aiMesh->MNumVertices];
        for (int i = 0; i < aiMesh->MNumVertices; i++)
        {
            verts[i] = new GLTF_VERTEX()
            {
                POSITION = Vector3.Transform(ToNum3(aiMesh->MVertices[i]), bOffsets[i]),
                NORMAL = Vector3.TransformNormal(ToNum3(aiMesh->MNormals != null ? aiMesh->MNormals[i] : new Vector3()), bOffsets[i]),
                TANGENT = ToNum4(Vector3.TransformNormal(aiMesh->MTangents != null ? aiMesh->MTangents[i] : new Vector3(), bOffsets[i])),
                COLOR_0 = ToNumCol(aiMesh->MColors.Element0 != null ? aiMesh->MColors.Element0[i] : new Vector4(1f, 1f, 1f, 1f)),
                TEXCOORD_0 = ToNum2(aiMesh->MTextureCoords.Element0 != null ? aiMesh->MTextureCoords.Element0[i] : new Vector3()),
                TEXCOORD_1 = ToNum2(aiMesh->MTextureCoords.Element1 != null ? aiMesh->MTextureCoords.Element1[i] : new Vector3()),
                JOINTS_0 = bInds[i],
                WEIGHTS_0 = bWeights[i],
            };
        }

        // var matBuilder = new MaterialBuilder(aiMaterial.Name);
        // var builder = new MeshBuilder<VertexPositionNormalTangent, VertexColor1Texture2, VertexJoints4>(aiMesh.Name);
        // builder.UsePrimitive(matBuilder).AddTriangle();
        var mesh = root.CreateMesh(aiMesh->MName);
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

        var inds = new List<int>();
        for (int i = 0; i < aiMesh->MNumFaces; i++)
            for (int j = 0; j < aiMesh->MFaces[i].MNumIndices; j++)
                inds.Add((int)aiMesh->MFaces[i].MIndices[j]);

        prim = prim.WithIndicesAccessor(SharpGLTF.Schema2.PrimitiveType.TRIANGLES, inds);
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
        var array = new Vector4Array(view.Content, encoding: EncodingType.UNSIGNED_INT);
        array.Fill(values.Select(x => x.ToVec4()));

        var accessor = root.CreateAccessor();

        accessor.SetVertexData(view, 0, values.Count, DimensionType.VEC4, EncodingType.UNSIGNED_INT, false);

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

    private static Vector2 ToNum2(Vector2 vector3D)
    {
        return new Vector2(vector3D.X, vector3D.Y);
    }

    private static Vector2 ToNum2(Vector3 vector3D)
    {
        return new Vector2(vector3D.X, vector3D.Y);
    }

    private static Vector4 ToNumCol(Vector4 color4D)
    {
        return new Vector4(color4D.X, color4D.Y, color4D.Z, color4D.W);
    }

    private static Vector3 ToNum3(Vector3 vector3D)
    {
        return new Vector3(vector3D.X, vector3D.Y, vector3D.Z);
    }

    private static Vector4 ToNum4(Vector3 vector3D)
    {
        return new Vector4(vector3D.X, vector3D.Y, vector3D.Z, 1f);
    }

    private static unsafe SharpGLTF.Schema2.Node ProcessNode(Assimp.Node* aiNode, ref SharpGLTF.Schema2.Animation anim, Assimp.Scene* aiScene, ref ModelRoot root, SharpGLTF.Schema2.Node parent, ref Dictionary<string, SharpGLTF.Schema2.Node> lookup, string dir)
    {
        SharpGLTF.Schema2.Node n = null;
        if (parent == null)
            n = root.DefaultScene.CreateNode(aiNode->MName);
            // n = root.CreateLogicalNode();
        else
            n = parent.CreateNode(aiNode->MName);
        lookup.TryAdd(aiNode->MName, n);
        for (int i = 0; i < aiNode->MNumChildren; i++)
        {
            var n2 = ProcessNode(aiNode->MChildren[i], ref anim, aiScene, ref root, n, ref lookup, dir);
        }
        n.LocalMatrix = ToNum(aiNode->MTransformation);
        if (aiScene->MNumAnimations > 0)
        {
            for (int i = 0; i < aiScene->MNumAnimations; i++)
            {
                if (GetChannel(aiNode, aiScene->MAnimations[i], out var channel))
                {
                    // var n2 = n.FindNode(x => x.Name == channel.NodeName);
                    var n2 = n;
                    // n2.WorldMatrix = ToNum(aiNode.Transform);
                    n2.LocalMatrix = ToNum(aiNode->MTransformation);
                    // Console.WriteLine(n.LocalMatrix);
                    // n2.WithLocalTransform(new SharpGLTF.Transforms.AffineTransform(ToNum3(channel.PositionKeys.First().Value), ToNum(channel.RotationKeys.First().Value), ToNum3(channel.ScalingKeys.First().Value)));
                    if (channel->MNumPositionKeys > 0)
                    {
                        var dict = ToPositionDict(aiScene, n2, aiScene->MAnimations[i], channel);
                        if (dict.Count > 0)
                            n2.WithTranslationAnimation("Track" + aiScene->MAnimations[i]->MName, dict);
                    }
                    if (channel->MNumRotationKeys > 0)
                    {
                        var dict = ToRotationDict(aiScene, n2, aiScene->MAnimations[i], channel);
                        if (dict.Count > 0)
                            n2.WithRotationAnimation("Track" + aiScene->MAnimations[i]->MName, dict);
                        // anim.CreateRotationChannel(n, channel.RotationKeys.ToDictionary(x => (float)(x.Time * aiScene.Animations[0].TicksPerSecond), x => ToNum(x.Value)));
                    }
                    if (channel->MNumScalingKeys > 0)
                    {
                        var dict = ToScaleDict(aiScene, n2, aiScene->MAnimations[i], channel);
                        if (dict.Count > 0)
                            n2.WithScaleAnimation("Track" + aiScene->MAnimations[i]->MName, dict);
                        // anim.CreateScaleChannel(n, channel.ScalingKeys.ToDictionary(x => (float)(x.Time * aiScene.Animations[0].TicksPerSecond), x => ToNum3(x.Value)));
                    }
                }
            }
        }
        SharpGLTF.Schema2.Mesh mesh = null;
        Skin skin = null;
        if (aiNode->MNumMeshes > 0)
        {
            if (false && aiScene->MMeshes[aiNode->MMeshes[0]]->MNumBones > 0)
            {
                (mesh, skin) = ProcessMesh(aiScene->MMeshes[aiNode->MMeshes[0]], aiScene, n, ref root, dir);
                var ns = SharpGLTF.Schema2.Node.Flatten(n).ToList();
                var lst2 = new List<(SharpGLTF.Schema2.Node, System.Numerics.Matrix4x4)>();
                var lst = new List<SharpGLTF.Schema2.Node>();
                for (int i = 0; i < aiScene->MMeshes[aiNode->MMeshes[0]]->MNumBones; i++)
                {
                    var bone = aiScene->MMeshes[aiNode->MMeshes[0]]->MBones[i];
                    var bNode = lookup[bone->MName];
                    var matrix = ToNum(bone->MOffsetMatrix);
                    System.Numerics.Matrix4x4.Invert(matrix, out var inv);
                    lst.Add(bNode);
                    lst2.Add((bNode, inv));
                }
                // Log.Debug(nameof(ShaderCompiler), "Skinned Mesh");
                // lst2.Add((n, n.WorldMatrix));
                foreach (var node in ns)
                {
                    // System.Numerics.Matrix4x4.Invert(Matrix4x4.Identity, out var inv);
                    System.Numerics.Matrix4x4.Invert(node.WorldMatrix, out var inv);
                    // var inv = node.LocalMatrix;
                    // var inv = System.Numerics.Matrix4x4.Identity;
                    // lst2.Add((node, inv));
                }
                // root.DefaultScene.CreateNode("Skinned Mesh").WithSkinnedMesh(mesh, n.WorldMatrix, lst.ToArray());
                // root.CreateLogicalNode().WithSkinnedMesh(mesh, n.WorldMatrix, ns.ToArray());
                // root.CreateLogicalNode().WithSkinnedMesh(mesh, lst2.ToArray());
                // root.DefaultScene.CreateNode().WithSkinnedMesh(mesh, lst2.ToArray());
                // n.LocalMatrix = Matrix4x4.Identity;

                // n.WithSkinnedMesh(mesh, lst2.ToArray());
                n.WithSkinnedMesh(mesh, Matrix4x4.Identity, lst.ToArray());
            }
            else
            {
                var ns = Node.Flatten(n).ToList();
                for (int i = 0; i < aiNode->MNumMeshes; i++)
                {
                    (mesh, skin) = ProcessMesh(aiScene->MMeshes[aiNode->MMeshes[i]], aiScene, n, ref root, dir);
                    if (aiScene->MMeshes[aiNode->MMeshes[i]]->MNumBones > 0)
                    {
                        ns.Clear();
                        for (int j = 0; j < aiScene->MMeshes[aiNode->MMeshes[i]]->MNumBones; j++)
                        {
                            var bone = aiScene->MMeshes[aiNode->MMeshes[i]]->MBones[j];
                            var bNode = lookup[bone->MName];
                            ns.Add(bNode);
                        }
                        // if (skin == null)
                        {
                            skin = root.CreateSkin();
                            skin.Skeleton = n;
                            skin.BindJoints(Matrix4x4.Identity, ns.ToArray());
                        }
                        n.CreateNode().WithMesh(mesh).WithSkin(skin);
                        // n.CreateNode(i.ToString()).WithSkinnedMesh(mesh, Matrix4x4.Identity, ns);
                    }
                    else
                        n.CreateNode(i.ToString()).WithMesh(mesh);
                }
            }
        }
        return n;
    }

    private static unsafe Dictionary<float, Vector3> ToPositionDict(Assimp.Scene* scene, Node node, Assimp.Animation* scnAnim, Assimp.NodeAnim* anim)
    {
        var dict = new Dictionary<float, Vector3>();
        for (int i = 0; i < anim->MNumPositionKeys; i++)
        {
            var key = anim->MPositionKeys[i];
            var time = key.MTime / scnAnim->MTicksPerSecond;
            if (double.IsFinite(time))
                dict.TryAdd((float)time, ToNum3(key.MValue));
        }
        return dict;
        // channel.PositionKeys.ToDictionary(x => (float)(x.Time * aiScene.Animations[0].TicksPerSecond), x => ToNum3(x.Value))
    }

    private static unsafe Dictionary<float, Quaternion> ToRotationDict(Assimp.Scene* scene, Node node, Assimp.Animation* scnAnim, Assimp.NodeAnim* anim)
    {
        var dict = new Dictionary<float, Quaternion>();
        for (int i = 0; i < anim->MNumRotationKeys; i++)
        {
            var key = anim->MRotationKeys[i];
            var time = key.MTime / scnAnim->MTicksPerSecond;
            if (double.IsFinite(time))
                dict.TryAdd((float)time, ToNum(key.MValue));
        }
        return dict;
    }

    private static unsafe Dictionary<float, Vector3> ToScaleDict(Assimp.Scene* scene, Node node, Assimp.Animation* scnAnim, Assimp.NodeAnim* anim)
    {
        var dict = new Dictionary<float, Vector3>();
        for (int i = 0; i < anim->MNumScalingKeys; i++)
        {
            var key = anim->MScalingKeys[i];
            var time = key.MTime / scnAnim->MTicksPerSecond;
            if (double.IsFinite(time))
                dict.TryAdd((float)time, ToNum3(key.MValue));
        }
        return dict;
    }

    private static System.Numerics.Matrix4x4 ToNum(System.Numerics.Matrix4x4 value)
    {
        // Console.WriteLine(value);
        return System.Numerics.Matrix4x4.Transpose(new System.Numerics.Matrix4x4(
            value.M11, value.M12, value.M13, value.M14,
            value.M21, value.M22, value.M23, value.M24,
            value.M31, value.M32, value.M33, value.M34,
            value.M41, value.M42, value.M42, value.M44
        ));
        return System.Numerics.Matrix4x4.Transpose(new System.Numerics.Matrix4x4(value.M11, value.M12, value.M13, value.M14,
            value.M21, value.M22, value.M23, value.M24,
            value.M31, value.M32, value.M33, value.M34,
            value.M41, value.M42, value.M43, value.M44));
    }

    private static System.Numerics.Quaternion ToNum(Assimp.AssimpQuaternion value)
    {
        return value;
        return System.Numerics.Quaternion.Inverse(new System.Numerics.Quaternion(value.X, value.Y, value.Z, value.W));
    }

    private static unsafe bool GetChannel(Assimp.Node* node, Assimp.Animation* anim, out Assimp.NodeAnim* channel)
    {
        for (int i = 0; i < anim->MNumChannels; i++)
        {
            if (anim->MChannels[i]->MNodeName == node->MName)
            {
                channel = anim->MChannels[i];
                return true;
            }
        }
        channel = null;
        return false;
    }

    private static Task CreateShaders(string vertCode, string fragCode, string pass, string path)
    {
        var compileResult = SpirvCompilation.CompileVertexFragment(Encoding.ASCII.GetBytes(vertCode), Encoding.ASCII.GetBytes(fragCode), CrossCompileTarget.ESSL, new CrossCompileOptions(true, false, false));
        Log.Info(nameof(ShaderCompiler), $"Compiling {path}.{pass}.json");
        File.WriteAllText($"{path}.{pass}.json", JsonConvert.SerializeObject(compileResult));
        return Task.CompletedTask;
    }
}
#endif
