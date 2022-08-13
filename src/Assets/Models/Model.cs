using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using Assimp;
using Engine.Assets.Rendering;
using Engine.Assets.Textures;
using Veldrid;

namespace Engine.Assets.Models
{
    public class Model : Resource
    {
        public override bool IsValid => false;
        private Mesh[] _meshes { get; set; }
        private Dictionary<int, List<UniformLayout>> _uniforms = new Dictionary<int, List<UniformLayout>>();
        private Dictionary<int, UniformBuffer> bonesBuffers = new Dictionary<int, UniformBuffer>();
        private Scene _assimpScene;
        private Animation _animation;
        private Dictionary<string, uint> _boneIdByName = new Dictionary<string, uint>();
        public Rendering.Material InternalMaterial { get; private set; }
        private string _path;
        private Assimp.Matrix4x4 _rootNodeInverseTransform
        {
            get
            {
                Assimp.Matrix4x4 m = _assimpScene.RootNode.Transform;
                m.Inverse();
                return m;
            }
        }

        public Model(string path, Rendering.Material material)
        {
            Name = path;
            _path = path;
            InternalMaterial = material;
            AssimpLoadMeshes(path, material);
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

        private void AssimpLoadMeshes(string path, Rendering.Material material)
        {
            using AssimpContext ctx = new AssimpContext();
            Scene scene = ctx.ImportFile(path, PostProcessSteps.Triangulate | PostProcessSteps.FlipUVs);
            _meshes = new Mesh[scene.MeshCount];
            for (int i = 0; i < _meshes.Length; i++)
            {
                Assimp.Mesh amesh = scene.Meshes[i];
                _meshes[i] = new Mesh(amesh.Name, false, material);
                Mesh mesh = _meshes[i];
                uint[] inds = amesh.GetUnsignedIndices();
                uint[] inds2 = new uint[inds.Length];
                for (uint j = 0; j < inds.Length - 3; j+=3)
                {
                    inds2[j+0] = inds[j+2];
                    inds2[j+1] = inds[j+1];
                    inds2[j+2] = inds[j+0];
                }
                mesh.Indices = inds2.ToList();
                // mesh.Indices = amesh.GetIndices().Select(x => (uint)x).ToList();
                mesh.Vertices = amesh.Vertices.Select(x => new Vector3(x.X, x.Y, x.Z)).ToList();
                mesh.Normals = amesh.Normals.Select(x => new Vector3(x.X, x.Y, x.Z)).ToList();
                if (amesh.TextureCoordinateChannelCount > 0)
                    mesh.UV0s = amesh.TextureCoordinateChannels[0].Select(x => new Vector2(x.X, x.Y)).ToList();
                else
                    mesh.UV0s = new Vector2[amesh.VertexCount].ToList();
                if (amesh.TextureCoordinateChannelCount > 1)
                    mesh.UV1s = amesh.TextureCoordinateChannels[1].Select(x => new Vector2(x.X, x.Y)).ToList();
                else
                    mesh.UV1s = new Vector2[amesh.VertexCount].ToList();
                if (amesh.VertexColorChannelCount > 0)
                    mesh.Colors = amesh.VertexColorChannels[0].Select(x => new RgbaFloat(x.R, x.G, x.B, x.A)).ToList();
                else
                {
                    RgbaFloat[] cols = new RgbaFloat[amesh.VertexCount];
                    for (uint j = 0; j < amesh.VertexCount; j++)
                        cols[j] = RgbaFloat.White;
                    mesh.Colors = cols.ToList();
                }
                if (amesh.BoneCount > 0)
                {
                    Vector4[] bWeights = new Vector4[amesh.VertexCount];
                    UInt4[] bInds = new UInt4[amesh.VertexCount];
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
                    mesh.BoneWeights = bWeights.ToList();
                    mesh.BoneIndices = bInds.ToList();
                }
                _uniforms[i] = new List<UniformLayout>();
                if (scene.HasMaterials)
                {
                    Assimp.Material mat = scene.Materials[amesh.MaterialIndex];
                    if (mat.GetMaterialTexture(Assimp.TextureType.Diffuse, 0, out TextureSlot slot))
                    {
                        string diffusePath = Path.Combine(Path.GetDirectoryName(path), Path.GetFileName(slot.FilePath));
                        _uniforms[i].Add(new UniformLayout(UniformConsts.DiffuseTextureName, ResourceManager.LoadTexture(diffusePath), false, true));
                    }
                }
                bonesBuffers[i] = new UniformBuffer($"{Name}_BonesBuffer_{i}", (uint)16 * 4 * 64);
                mesh.UploadDataOld();
                mesh.InternalMaterial.SetUniforms(UniformConsts.DiffuseTextureSet, _uniforms[i].ToArray());
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
        }

        public override void ReCreate()
        {
            if (HasBeenInitialized)
                return;
            base.ReCreate();
            for (int i = 0; i < _meshes.Length; i++)
            {
                _meshes[i].ReCreate();
            }
            foreach (var buf in bonesBuffers)
            {
                buf.Value.ReCreate();
            }
        }

        public override Resource Clone(string cloneName)
        {
            Model m = new Model(_path, ResourceManager.Clone<Rendering.Material>(cloneName + InternalMaterial.Name, InternalMaterial));
            return m;
        }

        public void SetWorldMatrix(System.Numerics.Matrix4x4 WorldMatrix)
        {
            for (int i = 0; i < _meshes.Length; i++)
            {
                _meshes[i].SetWorldMatrix(WorldMatrix);
                _meshes[i].PreDraw(Renderer.Current);
            }
        }

        public void PreDraw(Renderer renderer)
        {
            for (int i = 0; i < _meshes.Length; i++)
            {
                System.Numerics.Matrix4x4[] buf = AssimpAnimate(Program.Time, i);
                bonesBuffers[i].UploadData(buf);
                // _meshes[i].InternalMaterial.SetUniforms(UniformConsts.BonesMatrixBufferSet, new UniformLayout(UniformConsts.BonesMatrixName, bonesBuffers[i], true, false));
                _meshes[i].InternalMaterial.SetUniforms(UniformConsts.DiffuseTextureSet, _uniforms[i].ToArray());
                _meshes[i].PreDraw(renderer);
            }
        }

        public void Draw(Renderer renderer)
        {
            for (int i = 0; i < _meshes.Length; i++)
            {
                _meshes[i].Draw(renderer);
            }
        }

        public override void Dispose()
        {
            for (int i = 0; i < _meshes.Length; i++)
            {
                _meshes[i].Dispose();
            }
            foreach (var buf in bonesBuffers)
            {
                buf.Value.Dispose();
            }
            _meshes = null;
        }
    }
}