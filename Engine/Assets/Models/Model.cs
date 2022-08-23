using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using Assimp;
using Engine.Assets.Rendering;
using Engine.Assets.Textures;
using Veldrid;

namespace Engine.Assets.Models
{
    public class Model : Resource
    {
        public const string ShaderPath = "Shaders/MainMesh";
        public const uint ShaderWorldInfoSetId = 3;
        public const uint ShaderForwardSetId = 4;
        public const uint ShaderBonesSetId = 5;
        public override bool IsValid => false;
        private Mesh[] _meshes { get; set; }
        public Mesh[] Meshes => _meshes;
        public Vector3[] CollisionPositions { get; private set; }
        public uint[] CollisionTriangles { get; private set; }
        private Rendering.Material _ogMaterial;
        public List<CompoundBuffer> CompoundBuffers { get; private set; } = new List<CompoundBuffer>();
        private Dictionary<int, UniformBuffer> bonesUniforms = new Dictionary<int, UniformBuffer>();
        private Dictionary<int, CompoundBuffer> bonesBuffers = new Dictionary<int, CompoundBuffer>();
        private Scene _assimpScene;
        private Animation _animation;
        private Dictionary<string, uint> _boneIdByName = new Dictionary<string, uint>();
        public List<Rendering.Material> InternalMaterials { get; private set; }
        public UniformBuffer LightUniform { get; private set; }
        public CompoundBuffer LightBuffer { get; private set; }
        private string _path;
        private bool _shouldLoadMats;
        private bool _shouldAnimate;
        public double AnimationTime { get; set; }
        private Assimp.Matrix4x4 _rootNodeInverseTransform
        {
            get
            {
                Assimp.Matrix4x4 m = _assimpScene.RootNode.Transform;
                m.Inverse();
                return m;
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct ModelVertexLayout : IVertex
        {
            public Vector3 Position;
            public Vector3 Normal;
            public Vector2 UV0;
            public Vector2 UV1;
            public Vector4 Color;
            public Vector3 Tangent;
            public Vector3 BiTangent;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct AnimModelVertexLayout : IVertex
        {
            public Vector3 Position;
            public Vector3 Normal;
            public Vector2 UV0;
            public Vector2 UV1;
            public Vector4 Color;
            public Vector3 Tangent;
            public Vector3 BiTangent;
            public Vector4 BoneWeights;
            public UInt4 BoneIndices;
        }

        public Model(string name, string path, Rendering.Material material, bool loadMaterials, bool animate)
        {
            Name = name;
            _path = path;
            _ogMaterial = material;
            _shouldLoadMats = loadMaterials;
            _shouldAnimate = animate;
            InternalMaterials = new List<Rendering.Material>();
            Load(path, material, loadMaterials, animate);
        }

        private async void Load(string path, Rendering.Material material, bool loadMaterials, bool animate)
        {
            LightUniform = await ResourceManager.CreateUniformBuffer(ForwardConsts.LightBufferName, ForwardConsts.LightInfo.Size);
            AssimpLoadMeshes(path, material, animate);
            LightBuffer = await ResourceManager.CreateCompoundBuffer($"Model_{ForwardConsts.LightBufferName}", _ogMaterial?.Shader ?? await ResourceManager.LoadShader(ShaderPath), ShaderForwardSetId, LightUniform);
        }

        private System.Numerics.Matrix4x4[] AssimpAnimate(double time, int i)
        {
            var buf = new System.Numerics.Matrix4x4[64];
            for (int j = 0; j < buf.Length; j++)
            {
                buf[j] = System.Numerics.Matrix4x4.Identity;
            }
            if (_animation != null)
            {
                double totalSeconds = _animation.DurationInTicks * _animation.TicksPerSecond;
                double ticks = (time % totalSeconds) * _animation.TicksPerSecond;
                AssimpUpdateChannel(ticks, i, _assimpScene.RootNode, Assimp.Matrix4x4.Identity, ref buf);
            }
            return buf;
        }

        private void AssimpUpdateChannel(double time, int i, Node node, Assimp.Matrix4x4 parentTransform, ref System.Numerics.Matrix4x4[] _boneTransformations)
        {
            Assimp.Matrix4x4 nodeTransform = node.Transform;
            if (GetChannel(node, out NodeAnimationChannel channel))
            {
                Assimp.Matrix4x4 scale = InterpolateScale(time, channel);
                Assimp.Matrix4x4 rotation = InterpolateRotation(time, channel);
                Assimp.Matrix4x4 position = InterpolatePosition(time, channel);
                nodeTransform = scale * rotation * position;
            }

            if (_boneIdByName.TryGetValue(node.Name, out uint id))
            {
                Assimp.Matrix4x4 m = _assimpScene.Meshes[i].Bones[(int)id].OffsetMatrix * nodeTransform * parentTransform * _rootNodeInverseTransform;
                _boneTransformations[id] = m.ToSystemMatrixTransposed();
            }

            foreach (var childNode in node.Children)
            {
                AssimpUpdateChannel(time, i, childNode, nodeTransform * parentTransform, ref _boneTransformations);
            }
        }

        private Assimp.Matrix4x4 InterpolatePosition(double time, NodeAnimationChannel channel)
        {
            Vector3D position;

            if (channel.PositionKeyCount == 1)
            {
                position = channel.PositionKeys[0].Value;
            }
            else
            {
                uint frameIndex = 0;
                for (uint i = 0; i < channel.PositionKeyCount - 1; i++)
                {
                    if (time < (float)channel.PositionKeys[(int)(i + 1)].Time)
                    {
                        frameIndex = i;
                        break;
                    }
                }

                VectorKey currentFrame = channel.PositionKeys[(int)frameIndex];
                VectorKey nextFrame = channel.PositionKeys[(int)((frameIndex + 1) % channel.PositionKeyCount)];

                double delta = (time - (float)currentFrame.Time) / (float)(nextFrame.Time - currentFrame.Time);

                Vector3D start = currentFrame.Value;
                Vector3D end = nextFrame.Value;
                position = (start + (float)delta * (end - start));
            }

            return Assimp.Matrix4x4.FromTranslation(position);
        }

        private Assimp.Matrix4x4 InterpolateRotation(double time, NodeAnimationChannel channel)
        {
            Assimp.Quaternion rotation;

            if (channel.RotationKeyCount == 1)
            {
                rotation = channel.RotationKeys[0].Value;
            }
            else
            {
                uint frameIndex = 0;
                for (uint i = 0; i < channel.RotationKeyCount - 1; i++)
                {
                    if (time < (float)channel.RotationKeys[(int)(i + 1)].Time)
                    {
                        frameIndex = i;
                        break;
                    }
                }

                QuaternionKey currentFrame = channel.RotationKeys[(int)frameIndex];
                QuaternionKey nextFrame = channel.RotationKeys[(int)((frameIndex + 1) % channel.RotationKeyCount)];

                double delta = (time - (float)currentFrame.Time) / (float)(nextFrame.Time - currentFrame.Time);

                Assimp.Quaternion start = currentFrame.Value;
                Assimp.Quaternion end = nextFrame.Value;
                rotation = Assimp.Quaternion.Slerp(start, end, (float)delta);
                rotation.Normalize();
            }

            return rotation.GetMatrix();
        }

        private Assimp.Matrix4x4 InterpolateScale(double time, NodeAnimationChannel channel)
        {
            Vector3D scale;

            if (channel.ScalingKeyCount == 1)
            {
                scale = channel.ScalingKeys[0].Value;
            }
            else
            {
                uint frameIndex = 0;
                for (uint i = 0; i < channel.ScalingKeyCount - 1; i++)
                {
                    if (time < (float)channel.ScalingKeys[(int)(i + 1)].Time)
                    {
                        frameIndex = i;
                        break;
                    }
                }

                VectorKey currentFrame = channel.ScalingKeys[(int)frameIndex];
                VectorKey nextFrame = channel.ScalingKeys[(int)((frameIndex + 1) % channel.ScalingKeyCount)];

                double delta = (time - (float)currentFrame.Time) / (float)(nextFrame.Time - currentFrame.Time);

                Vector3D start = currentFrame.Value;
                Vector3D end = nextFrame.Value;

                scale = (start + (float)delta * (end - start));
            }

            return Assimp.Matrix4x4.FromScaling(scale);
        }

        private bool GetChannel(Node node, out NodeAnimationChannel channel)
        {
            foreach (NodeAnimationChannel c in _animation.NodeAnimationChannels)
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

        private async void AssimpLoadMeshes(string path, Rendering.Material defMaterial, bool animate)
        {
            int vertexCount = 0;
            List<Vector3> colPos = new List<Vector3>();
            List<uint> colTris = new List<uint>();
            using AssimpContext ctx = new AssimpContext();
            Scene scene = ctx.ImportFileFromStream(await FileManager.LoadStream(path), PostProcessSteps.Triangulate | PostProcessSteps.FlipUVs | PostProcessSteps.CalculateTangentSpace | PostProcessSteps.GenerateNormals | PostProcessSteps.FixInFacingNormals | PostProcessSteps.FindInvalidData);
            _meshes = new Mesh[scene.MeshCount];
            for (int i = 0; i < _meshes.Length; i++)
            {
                InternalMaterials.Add(defMaterial ?? await ResourceManager.CreateMaterial($"{Name}_{i}", _ogMaterial?.Shader ?? await ResourceManager.LoadShader(ShaderPath)));
                Rendering.Material material = InternalMaterials[i];
                List<ModelVertexLayout> vertices = new List<ModelVertexLayout>();
                List<AnimModelVertexLayout> animVertices = new List<AnimModelVertexLayout>();
                Assimp.Mesh amesh = scene.Meshes[i];
                _meshes[i] = await ResourceManager.CreateMesh($"{Name}_{i}", false, material);
                Mesh mesh = _meshes[i];
                uint[] inds = amesh.GetUnsignedIndices();
                uint[] inds2 = new uint[inds.Length];
                for (uint j = 0; j < inds.Length; j+=3)
                {
                    inds2[j+0] = inds[j+2];
                    inds2[j+1] = inds[j+1];
                    inds2[j+2] = inds[j+0];
                }
                mesh.Indices = inds2.ToList();
                // mesh.Indices = amesh.GetIndices().Select(x => (uint)x).ToList();
                var positions = amesh.Vertices.Select(x => new Vector3(x.X, x.Y, x.Z)).ToList();
                var normals = amesh.Normals.Select(x => new Vector3(x.X, x.Y, x.Z)).ToList();
                var tangents = amesh.Tangents.Select(x => new Vector3(x.X, x.Y, x.Z)).ToList();
                var bitangents = amesh.BiTangents.Select(x => new Vector3(x.X, x.Y, x.Z)).ToList();
                List<Vector2> uv0s;
                if (amesh.TextureCoordinateChannelCount > 0)
                    uv0s = amesh.TextureCoordinateChannels[0].Select(x => new Vector2(x.X, x.Y)).ToList();
                else
                    uv0s = new Vector2[amesh.VertexCount].ToList();
                List<Vector2> uv1s;
                if (amesh.TextureCoordinateChannelCount > 1)
                    uv1s = amesh.TextureCoordinateChannels[1].Select(x => new Vector2(x.X, x.Y)).ToList();
                else
                    uv1s = new Vector2[amesh.VertexCount].ToList();
                List<Vector4> colors;
                if (amesh.VertexColorChannelCount > 0)
                    colors = amesh.VertexColorChannels[0].Select(x => new Vector4(x.R, x.G, x.B, x.A)).ToList();
                else
                {
                    Vector4[] cols = new Vector4[amesh.VertexCount];
                    for (uint j = 0; j < amesh.VertexCount; j++)
                        cols[j] = Vector4.One;
                    colors = cols.ToList();
                }
                Vector4[] bWeights = new Vector4[amesh.VertexCount];
                UInt4[] bInds = new UInt4[amesh.VertexCount];
                if (amesh.BoneCount > 0)
                {
                    for (int j = 0; j < amesh.BoneCount; j++)
                    {
                        _boneIdByName.Add(amesh.Bones[j].Name, (uint)j);
                        for (int k = 0; k < amesh.Bones[j].VertexWeightCount; k++)
                        {
                            float weight = amesh.Bones[j].VertexWeights[k].Weight;
                            int vertId = amesh.Bones[j].VertexWeights[k].VertexID;
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
                    // mesh.BoneWeights = bWeights.ToList();
                    // mesh.BoneIndices = bInds.ToList();
                }
                if (scene.HasMaterials && _shouldLoadMats)
                {
                    Assimp.Material mat = scene.Materials[amesh.MaterialIndex];
                    if (mat.GetMaterialTexture(Assimp.TextureType.Diffuse, 0, out TextureSlot slot))
                    {
                        string diffusePath = Path.Combine(Path.GetDirectoryName(path), Path.GetFileName(slot.FilePath));
                        CompoundBuffer buffer = await ResourceManager.CreateCompoundBuffer(diffusePath, mesh.InternalMaterial.Shader, UniformConsts.DiffuseTextureSet, await ResourceManager.LoadTexture(diffusePath), await Texture2D.DefaultWhite, await Texture2D.DefaultNormal);
                        CompoundBuffers.Add(buffer);
                        mesh.InternalMaterial.SetUniforms(UniformConsts.DiffuseTextureSet, CompoundBuffers[i]);
                    }
                    else
                    {
                        // _compoundBuffers.Add(null);
                    }
                }
                else
                {
                    // _compoundBuffers.Add(null);
                }
                if (_shouldAnimate)
                {
                    bonesUniforms[i] = await ResourceManager.CreateUniformBuffer($"{Name}_BonesUniform_{i}", (uint)16 * 4 * 64);
                    bonesBuffers[i] = await ResourceManager.CreateCompoundBuffer($"{Name}_BonesBuffer_{i}", mesh.InternalMaterial.Shader, ShaderBonesSetId, bonesUniforms[i]);
                }
                for (int j = 0; j < positions.Count; j++)
                {
                    vertices.Add(new ModelVertexLayout()
                    {
                        Position = positions[j],
                        Normal = normals[j],
                        UV0 = uv0s[j],
                        UV1 = uv1s[j],
                        Color = colors[j],
                        Tangent = tangents[j],
                        BiTangent = bitangents[j],
                    });
                    animVertices.Add(new AnimModelVertexLayout()
                    {
                        Position = positions[j],
                        Normal = normals[j],
                        UV0 = uv0s[j],
                        Color = colors[j],
                        Tangent = tangents[j],
                        BiTangent = bitangents[j],
                        BoneWeights = bWeights[j],
                        BoneIndices = bInds[j],
                    });
                }
                if (animate)
                    mesh.UploadData(animVertices.ToArray());
                else
                    mesh.UploadData(vertices.ToArray());
                colPos.AddRange(positions);
                colTris.AddRange(inds.Select(x => (uint)(x + vertexCount)).ToArray());
                vertexCount += positions.Count;
            }
            _assimpScene = scene;
            if (_assimpScene.HasAnimations)
            {
                _animation = _assimpScene.Animations[0];
            }
            else
            {
                _animation = null;
            }

            CollisionPositions = colPos.ToArray();
            CollisionTriangles = colTris.ToArray();
        }

        public override async Task ReCreate()
        {
            if (HasBeenInitialized)
                return;
            await base.ReCreate();
            for (int i = 0; i < _meshes.Length; i++)
            {
                await _meshes[i].ReCreate();
            }
            foreach (var buf in bonesUniforms)
            {
                await buf.Value.ReCreate();
            }
        }

        public override async Task<Resource> Clone(string cloneName)
        {
            Model m = new Model(cloneName, _path, _ogMaterial != null ? await ResourceManager.Clone<Rendering.Material>(cloneName + _ogMaterial.Name, _ogMaterial) : null, _shouldLoadMats, _shouldAnimate);
            return m;
        }

        public void SetWorldMatrixDraw(Renderer renderer, System.Numerics.Matrix4x4 WorldMatrix)
        {
            for (int i = 0; i < _meshes.Length; i++)
            {
                if (_shouldAnimate)
                {
                    System.Numerics.Matrix4x4[] buf = AssimpAnimate(AnimationTime, i);
                    bonesUniforms[i].UploadData(renderer, buf);
                    _meshes[i].InternalMaterial.SetUniforms(ShaderBonesSetId, bonesBuffers[i]);
                }
                _meshes[i].SetWorldMatrix(renderer, WorldMatrix);
                if (CompoundBuffers.Count > 0 && CompoundBuffers.Count == _meshes.Length)
                    _meshes[i].InternalMaterial.SetUniforms(UniformConsts.DiffuseTextureSet, CompoundBuffers[i]);
                _meshes[i].InternalMaterial.SetUniforms(ShaderForwardSetId, LightBuffer);
                renderer.SetupStandardWorldInfoUniforms(_meshes[i].InternalMaterial, ShaderWorldInfoSetId);
                renderer.SetupStandardMatrixUniforms(_meshes[i].InternalMaterial);
                _meshes[i].PreDraw(renderer);

                ForwardConsts.ForwardLight[] sortedLights = ForwardConsts.Lights.OrderBy(x => (x.Position - renderer.ViewPosition).LengthSquared()).Take(ForwardConsts.MaxRealtimeLights).ToArray();
                if (sortedLights.Length == 0)
                {
                    int j = 0;
                    LightUniform.UploadData(renderer, ForwardConsts.GetLightInfo(j, j == 0, sortedLights));
                    _meshes[i].Draw(renderer, j == 0 ? ForwardConsts.ForwardBasePassName : ForwardConsts.ForwardAddPassName);
                }
                else
                {
                    for (int j = 0; j < (float)sortedLights.Length / ForwardConsts.MaxLightsPerPass; j++)
                    {
                        LightUniform.UploadData(renderer, ForwardConsts.GetLightInfo(j, j == 0, sortedLights));
                        _meshes[i].Draw(renderer, j == 0 ? ForwardConsts.ForwardBasePassName : ForwardConsts.ForwardAddPassName);
                    }
                }
            }
        }

        public override void Dispose()
        {
            foreach (var buf in bonesUniforms)
            {
                buf.Value.Dispose();
            }
            _meshes = null;
        }
    }
}