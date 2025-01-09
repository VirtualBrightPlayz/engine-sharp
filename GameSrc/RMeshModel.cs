using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using Engine.Assets;
using Engine.Assets.Audio;
using Engine.Assets.Models;
using Engine.Assets.Rendering;
using Engine.Assets.Textures;
using Engine.Game;

namespace GameSrc
{
    public class RMeshModel : Resource
    {
        public const string ShaderPath = "Shaders/RMesh";
        public const uint ShaderWorldInfoSetId = 3;
        public const uint ShaderForwardSetId = 4;
        public static GraphicsShader shader { get; set; }
        public static string[] RoomAmbiencePaths => new string[]
        {
            "SFX/Ambient/Room ambience/rumble.ogg",
            "SFX/Ambient/Room ambience/lowdrone.ogg",
            "SFX/Ambient/Room ambience/pulsing.ogg",
            "SFX/Ambient/Room ambience/ventilation.ogg",
            "SFX/Ambient/Room ambience/rumble.ogg",
            "SFX/Ambient/Room ambience/drip.ogg",
            "SFX/Alarm/Alarm.ogg",
            "SFX/Ambient/Room ambience/895.ogg",
            "SFX/Ambient/Room ambience/fuelpump.ogg",
            "SFX/Ambient/Room ambience/Fan.ogg",
            "SFX/Ambient/Room ambience/servers1.ogg",
        };
        public override bool IsValid => true;
        private Mesh[] meshes;
        private Material[] materials;
        private CompoundBuffer[] uniforms;
        private string _path;
        public Vector3[] CollisionPositions { get; private set; }
        public uint[] CollisionTriangles { get; private set; }
        public static Vector3 RoomScale => new Vector3(1f, 1f, -1f) * 1f / 102.4f;
        private List<RMeshPointModel> pointModels = new List<RMeshPointModel>();
        public IReadOnlyList<RMeshPointModel> Models => pointModels;
        private List<RMeshAudioSource> soundEmitters = new List<RMeshAudioSource>();
        public IReadOnlyList<RMeshAudioSource> Sounds => soundEmitters;
        private List<ForwardConsts.ForwardLight> _lights = new List<ForwardConsts.ForwardLight>();
        public IReadOnlyList<ForwardConsts.ForwardLight> Lights => _lights;
        public UniformBuffer LightUniform { get; private set; }
        public CompoundBuffer LightBuffer { get; private set; }

        public struct RMeshAudioSource
        {
            public Vector3 position;
            public float range;
            public AudioClip clip;
        }

        public struct RMeshPointModel
        {
            public Vector3 position;
            public Vector3 euler;
            public Vector3 scale;
            public string path;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct RMeshVertexLayout : IVertex
        {
            public Vector3 Position;
            public Vector3 Normal;
            public Vector2 UV0;
            public Vector2 UV1;
            public Vector4 Color;
            public Vector3 Tangent;
            public Vector3 BiTangent;
        }

        public RMeshModel(string path) : this(path, path)
        {
        }
        
        public RMeshModel(string name, string path) : base(name)
        {
            Name = name;
            _path = path;
            using var stream = FileManager.LoadStream(path);
            bool hasTriggerBox = false;
            string header = stream.ReadString();
            if (header == "RoomMesh")
            {
            }
            else if (header == "RoomMesh.HasTriggerBox")
            {
                hasTriggerBox = true;
            }
            else
            {
                throw new Exception("RMesh Header invalid.");
            }
            int count = stream.ReadInt();
            meshes = new Mesh[count];
            materials = new Material[count];
            uniforms = new CompoundBuffer[count];
            int vertexCount = 0;
            List<uint> colTris = new List<uint>();
            List<Vector3> colPos = new List<Vector3>();
            LightUniform = new UniformBuffer(ForwardConsts.LightBufferName, ForwardConsts.LightInfo.Size);
            // LightUniform.UploadData(ForwardConsts.GetLightInfo());

            for (int i = 0; i < count; i++)
            {
                // textures
                string[] tex = new string[2];
                for (int j = 0; j < 2; j++)
                {
                    byte temp1b = (byte)stream.ReadByte();
                    if (temp1b != 0)
                    {
                        tex[j] = stream.ReadString();
                        if (!string.IsNullOrWhiteSpace(tex[j]))
                            tex[j] = Path.Combine(Path.GetDirectoryName(path), tex[j]);
                    }
                }

                // verts
                int count2 = stream.ReadInt();
                Vector3[] vertices = new Vector3[count2];
                Vector2[] uv0s = new Vector2[count2];
                Vector2[] uv1s = new Vector2[count2];
                Vector3[] colors = new Vector3[count2];
                Vector3[] normals = new Vector3[count2];
                Vector3[] tangents = new Vector3[count2];
                Vector3[] bitangents = new Vector3[count2];

                for (int j = 0; j < count2; j++)
                {
                    // world coords
                    float x = stream.ReadFloat();
                    float y = stream.ReadFloat();
                    float z = stream.ReadFloat();

                    vertices[j] = new Vector3(x, y, z) * RoomScale;
                    
                    // uv0
                    float u = stream.ReadFloat();
                    float v = stream.ReadFloat();
                    uv0s[j] = new Vector2(u, v);

                    // uv1
                    float u1 = stream.ReadFloat();
                    float v1 = stream.ReadFloat();
                    uv1s[j] = new Vector2(u1, v1);

                    float r = stream.ReadByte() / 255f;
                    float g = stream.ReadByte() / 255f;
                    float b = stream.ReadByte() / 255f;
                    colors[j] = new Vector3(r, g, b);
                }

                // tris
                count2 = stream.ReadInt();
                int[] tris = new int[count2 * 3];

                for (int j = 0; j < count2; j++)
                {
                    tris[j * 3 + 0] = stream.ReadInt();
                    tris[j * 3 + 1] = stream.ReadInt();
                    tris[j * 3 + 2] = stream.ReadInt();

                    int t0 = tris[j * 3 + 0];
                    int t1 = tris[j * 3 + 1];
                    int t2 = tris[j * 3 + 2];
                    Vector3 pos0 = vertices[t0];
                    Vector3 pos1 = vertices[t1];
                    Vector3 pos2 = vertices[t2];
                    Vector2 uv0 = uv0s[t0];
                    Vector2 uv1 = uv0s[t1];
                    Vector2 uv2 = uv0s[t2];
                    Vector3 edge0 = pos1 - pos0;
                    Vector3 edge1 = pos2 - pos0;
                    Vector2 deltaUV0 = uv1 - uv0;
                    Vector2 deltaUV1 = uv2 - uv0;

                    Vector3 normal1 = default;
                    Vector3 tangent1 = default;
                    Vector3 bitangent1 = default;

                    normal1 = Vector3.Normalize(Vector3.Cross(pos1 - pos0, pos2 - pos0));

                    float f = 1f / (deltaUV0.X * deltaUV1.Y - deltaUV1.X * deltaUV0.Y);

                    tangent1.X = f * (deltaUV1.Y * edge0.X - deltaUV0.Y * edge1.X);
                    tangent1.Y = f * (deltaUV1.Y * edge0.Y - deltaUV0.Y * edge1.Y);
                    tangent1.Z = f * (deltaUV1.Y * edge0.Z - deltaUV0.Y * edge1.Z);

                    bitangent1.X = f * (-deltaUV1.X * edge0.X + deltaUV0.X * edge1.X);
                    bitangent1.Y = f * (-deltaUV1.X * edge0.Y + deltaUV0.X * edge1.Y);
                    bitangent1.Z = f * (-deltaUV1.X * edge0.Z + deltaUV0.X * edge1.Z);

                    normals[t0] = normal1;
                    normals[t1] = normal1;
                    normals[t2] = normal1;

                    tangents[t0] = tangent1;
                    tangents[t1] = tangent1;
                    tangents[t2] = tangent1;

                    bitangents[t0] = bitangent1;
                    bitangents[t1] = bitangent1;
                    bitangents[t2] = bitangent1;
                }

                shader ??= new GraphicsShader("Shaders/RMesh");
                Material material = new Material($"{tex[1]}_{i}_{Random.Shared.Next()}", shader);
                // Material material = ResourceManager.CreateMaterial($"RMeshMat_{tex[1]}", shader);
                Texture2D diffuse = SCPCB.GetTexture(tex[1]);
                string bumppath = Path.Combine(SCPCB.Instance.Data.GameDir, SCPCB.Instance.Data.GetBumpPath(Path.GetFileName(tex[1]).ToLower()) ?? string.Empty);
                // string bumppath = Path.Combine(Path.GetDirectoryName(tex[1]), Path.GetFileNameWithoutExtension(tex[1]) + "bump.jpg");
                Texture2D bump = (!string.IsNullOrWhiteSpace(bumppath) && File.Exists(bumppath)) ? SCPCB.GetTexture(bumppath) : Texture2D.DefaultNormal;
                if (string.IsNullOrWhiteSpace(tex[0]))
                    uniforms[i] = new CompoundBuffer($"RMeshBuf_{tex[1]}", shader, UniformConsts.DiffuseTextureSet, diffuse, Texture2D.DefaultWhite, bump);
                else
                    uniforms[i] = new CompoundBuffer($"RMeshBuf_{tex[1]}_{tex[0]}", shader, UniformConsts.DiffuseTextureSet, diffuse, SCPCB.GetTexture(tex[0]), bump);
                meshes[i] = new Mesh($"{name}_{i}", false);
                materials[i] = material;
                RMeshVertexLayout[] data = new RMeshVertexLayout[vertices.Length];
                for (int j = 0; j < data.Length; j++)
                {
                    data[j] = new RMeshVertexLayout()
                    {
                        Position = vertices[j],
                        Normal = normals[j],
                        UV0 = uv0s[j],
                        UV1 = uv1s[j],
                        Color = new Vector4(colors[j], 1f),
                        Tangent = tangents[j],
                        BiTangent = bitangents[j],
                    };
                }
                /*meshes[i].Vertices = vertices.ToList();
                meshes[i].Normals = new Vector3[vertices.Length].ToList();
                meshes[i].UV0s = uv0s.ToList();
                meshes[i].UV1s = uv1s.ToList();
                meshes[i].Colors = colors.Select(x => new RgbaFloat(x.X, x.Y, x.Z, 1f)).ToList();*/
                meshes[i].Indices = tris.Select(x => (uint)x).ToList();
                meshes[i].UploadData<RMeshVertexLayout>(data);

                colPos.AddRange(vertices);
                colTris.AddRange(meshes[i].Indices.Select(x => (uint)(x + vertexCount)).ToArray());
                vertexCount += vertices.Length;

                LightBuffer = new CompoundBuffer($"RMeshBuffer_{ForwardConsts.LightBufferName}", shader, ShaderForwardSetId, LightUniform);
            }

            // collision mesh
            count = stream.ReadInt();
            for (int i = 0; i < count; i++)
            {
                int count2 = stream.ReadInt();
                Vector3[] vertices = new Vector3[count2];
                for (int j = 0; j < count2; j++)
                {
                    // world coords
                    float x = stream.ReadFloat();
                    float y = stream.ReadFloat();
                    float z = stream.ReadFloat();

                    vertices[j] = new Vector3(x, y, z) * RoomScale;
                }

                // tris
                count2 = stream.ReadInt();
                int[] tris = new int[count2 * 3];

                for (int j = 0; j < count2; j++)
                {
                    tris[j * 3 + 0] = stream.ReadInt();
                    tris[j * 3 + 1] = stream.ReadInt();
                    tris[j * 3 + 2] = stream.ReadInt();
                }

                /*
                colPos.AddRange(vertices);
                colTris.AddRange(tris.Select(x => (uint)(x + vertexCount)).ToArray());
                vertexCount += vertices.Length;
                */
            }

            if (hasTriggerBox)
            {
                // trigger box count
                count = stream.ReadInt();
                for (int i = 0; i < count; i++)
                {
                    // trigger box mesh count
                    int count2 = stream.ReadInt();
                    for (int j = 0; j < count2; j++)
                    {
                        int count3 = stream.ReadInt();
                        for (int k = 0; k < count3; k++)
                        {
                            // world coords
                            float x = stream.ReadFloat();
                            float y = stream.ReadFloat();
                            float z = stream.ReadFloat();

                            // new Vector3(x, y, z) * RoomScale;
                        }
                        count3 = stream.ReadInt();
                        for (int k = 0; k < count3; k++)
                        {
                            stream.ReadInt();
                            stream.ReadInt();
                            stream.ReadInt();
                            /*
                            tris[j * 3 + 0] = stream.ReadInt();
                            tris[j * 3 + 1] = stream.ReadInt();
                            tris[j * 3 + 2] = stream.ReadInt();
                            */
                        }

                        string triggerName = stream.ReadString();
                    }
                }
            }

            // point entities
            count = stream.ReadInt();
            for (int i = 0; i < count; i++)
            {
                string classname = stream.ReadString();
                switch (classname)
                {
                    case "screen":
                    {
                        float x = stream.ReadFloat();
                        float y = stream.ReadFloat();
                        float z = stream.ReadFloat();

                        Vector3 position = new Vector3(x, y, z) * RoomScale;
                        string screenPath = stream.ReadString();
                    }
                    break;
                    case "waypoint":
                    {
                        float x = stream.ReadFloat();
                        float y = stream.ReadFloat();
                        float z = stream.ReadFloat();
                        Vector3 position = new Vector3(x, y, z) * RoomScale;
                    }
                    break;
                    case "light":
                    {
                        float x = stream.ReadFloat();
                        float y = stream.ReadFloat();
                        float z = stream.ReadFloat();
                        Vector3 position = new Vector3(x, y, z) * RoomScale;
                        float range = stream.ReadFloat() * (1f / 102.4f);// / 2000f;
                        string strColor = stream.ReadString();
                        float intensity = MathF.Min(stream.ReadFloat() * 0.8f, 1.0f);
                        string[] splColor = strColor.Split(' ');
                        float.TryParse(splColor[0], out float r);
                        float.TryParse(splColor[1], out float g);
                        float.TryParse(splColor[2], out float b);
                        _lights.Add(new ForwardConsts.ForwardLight()
                        {
                            Position = position,
                            Color = new Vector4(r / 255f, g / 255f, b / 255f, intensity),
                            Range = range,
                        });
                    }
                    break;
                    case "spotlight":
                    {
                        float x = stream.ReadFloat();
                        float y = stream.ReadFloat();
                        float z = stream.ReadFloat();
                        Vector3 position = new Vector3(x, y, z) * RoomScale;
                        float range = stream.ReadFloat() / 2000f;
                        string strColor = stream.ReadString();
                        float intensity = MathF.Min(stream.ReadFloat() * 0.8f, 1.0f);
                        string strAngles = stream.ReadString();
                        int innerConeAngle = stream.ReadInt();
                        int outerConeAngle = stream.ReadInt();
                    }
                    break;
                    case "soundemitter":
                    {
                        float x = stream.ReadFloat();
                        float y = stream.ReadFloat();
                        float z = stream.ReadFloat();
                        Vector3 position = new Vector3(x, y, z) * RoomScale;
                        int soundIndex = stream.ReadInt();
                        float range = stream.ReadFloat();
                        AudioClip clip = new AudioClip(Path.Combine(Path.GetDirectoryName(path), "..", "..", RoomAmbiencePaths[soundIndex]));
                        soundEmitters.Add(new RMeshAudioSource()
                        {
                            position = position,
                            range = range,
                            clip = clip,
                        });
                    }
                    break;
                    case "playerstart":
                    {
                        float x = stream.ReadFloat();
                        float y = stream.ReadFloat();
                        float z = stream.ReadFloat();
                        Vector3 position = new Vector3(x, y, z) * RoomScale;
                        string strAngles = stream.ReadString();
                    }
                    break;
                    case "model":
                    {
                        // TODO
                        string mdlFile = stream.ReadString();
                        float x = stream.ReadFloat();
                        float y = stream.ReadFloat();
                        float z = stream.ReadFloat();
                        Vector3 position = new Vector3(x, y, z) * RoomScale;
                        float xEuler = stream.ReadFloat();
                        float yEuler = stream.ReadFloat();
                        float zEuler = stream.ReadFloat();
                        Vector3 euler = new Vector3(xEuler, yEuler, zEuler);
                        float xScale = stream.ReadFloat();
                        float yScale = stream.ReadFloat();
                        float zScale = stream.ReadFloat();
                        Vector3 scale = new Vector3(xScale, yScale, zScale) * (1f / 102.4f);
                        pointModels.Add(new RMeshPointModel()
                        {
                            path = mdlFile,
                            position = position,
                            euler = euler,
                            scale = scale,
                        });
                    }
                    break;
                }
            }

            CollisionPositions = colPos.ToArray();
            CollisionTriangles = colTris.ToArray();
        }

        protected override Resource CloneInternal(string cloneName)
        {
            return new RMeshModel(cloneName, _path);
        }

        protected override void ReCreateInternal()
        {
            if (HasBeenInitialized)
                return;
            LightUniform.ReCreate();
            foreach (var mesh in meshes)
            {
                mesh.ReCreate();
            }
            foreach (var mat in materials)
            {
                mat.ReCreate();
            }
            foreach (var arr in uniforms)
            {
                arr.ReCreate();
            }
            foreach (var sound in soundEmitters)
            {
                sound.clip.ReCreate();
            }
        }

        public void SetWorldMatrix(Renderer renderer, Matrix4x4 WorldMatrix)
        {
            // LightUniform.UploadData(ForwardConsts.GetLightInfo());
            for (int i = 0; i < meshes.Length; i++)
            {
                renderer.WorldMatrix = WorldMatrix;
                materials[i].SetUniforms(UniformConsts.DiffuseTextureSet, uniforms[i]);
                materials[i].SetUniforms(ShaderForwardSetId, LightBuffer);
                renderer.SetupStandardWorldInfoUniforms(materials[i], ShaderWorldInfoSetId);
            }
        }

        public void Draw(Renderer renderer, double dt)
        {
            for (int i = 0; i < meshes.Length; i++)
            {
                ForwardConsts.ForwardLight[] sortedLights = ForwardConsts.Lights.OrderBy(x => (x.Position - renderer.ViewPosition).LengthSquared()).Take(ForwardConsts.MaxRealtimeLights).ToArray();
                for (int j = 0; j < (float)sortedLights.Length / ForwardConsts.MaxLightsPerPass; j++)
                {
                    LightUniform.UploadData(renderer, ForwardConsts.GetLightInfo(j, j == 0, sortedLights));
                    renderer.BindMaterial(materials[i], j == 0 ? ForwardConsts.ForwardBasePassName : ForwardConsts.ForwardAddPassName);
                    renderer.DrawMeshNow(meshes[i]);
                }
            }
        }

        protected override void DisposeInternal()
        {
            LightUniform.Dispose();
            for (int i = 0; i < meshes.Length; i++)
            {
                meshes[i].Dispose();
            }
            for (int i = 0; i < materials.Length; i++)
            {
                materials[i].Dispose();
            }
        }
    }
}