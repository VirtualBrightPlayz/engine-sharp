using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using BepuPhysics;
using BepuUtilities;
using BepuUtilities.Memory;
using Engine;
using Engine.Assets;
using Engine.Assets.Audio;
using Engine.Assets.Rendering;
using Engine.Assets.Textures;
using Engine.Game;
using Engine.Game.Entities;
using ImGuiNET;

namespace GameBSrc
{
    public class BGame : GameApp
    {
        public const uint FogSetId = 6;
        public static BGame Instance => Program.Game as BGame;
        public override string Name => "SCP-087-B CSharp";
        public GameData Data { get; private set; }
        public BPlayerEntity player;
        public AudioSource Music;
        public AudioClip musicClip;
        public AudioSource Radio;
        public AudioSource miscSource;
        public AudioSource horrorSource;
        public AudioClip[] radioClips => new AudioClip[]
        {
            ResourceManager.LoadAudioClip(Path.Combine(BGame.Instance.Data.SFXDir, "radio1.ogg")),
            ResourceManager.LoadAudioClip(Path.Combine(BGame.Instance.Data.SFXDir, "radio2.ogg")),
            ResourceManager.LoadAudioClip(Path.Combine(BGame.Instance.Data.SFXDir, "radio3.ogg")),
            ResourceManager.LoadAudioClip(Path.Combine(BGame.Instance.Data.SFXDir, "radio4.ogg")),
            ResourceManager.LoadAudioClip(Path.Combine(BGame.Instance.Data.SFXDir, "death.ogg")),
        };
        public AudioClip[] horrorClips => new AudioClip[]
        {
            ResourceManager.LoadAudioClip(Path.Combine(BGame.Instance.Data.SFXDir, "horror1.ogg")),
            ResourceManager.LoadAudioClip(Path.Combine(BGame.Instance.Data.SFXDir, "horror2.ogg")),
            ResourceManager.LoadAudioClip(Path.Combine(BGame.Instance.Data.SFXDir, "horror3.ogg")),
        };
        public AudioClip fireOnClip => ResourceManager.LoadAudioClip(Path.Combine(BGame.Instance.Data.SFXDir, "match.ogg"));
        public AudioClip fireOffClip => ResourceManager.LoadAudioClip(Path.Combine(BGame.Instance.Data.SFXDir, "fireout.ogg"));
        public string MusicClipPath => Path.Combine(Data.SFXDir, "music.ogg");
        public List<StaticModelEntity> floors = new List<StaticModelEntity>();
        public GraphicsShader Shader => ResourceManager.LoadShader("Shaders/MainMeshFog");
        public GraphicsShader AnimShader => ResourceManager.LoadShader("Shaders/MainMeshAnimFog");
        public int radioState = 0;
        public int currentFloor = 0;
        public int killTimer = 0;
        public int prevFrameFloor = -1;
        public float[] floorTimers;
        public FloorEntity.FloorAction[] floorActions;
        public UniformBuffer fogUniform;
        public CompoundBuffer fogBuffer;
        public Vector4 fogData;
        public const float MinFloorTime = 0.01f;
        public EnemyEntity enemy;
        public int Brightness = 40;
        public bool DebugMode = false;

        public BGame() : base()
        {
            Data = new GameData("GameB");
        }

        public override void Setup()
        {
            base.Setup();
            fogUniform = ResourceManager.CreateUniformBuffer("WorldFogInfo", (uint)4 * 4);
            fogBuffer = ResourceManager.CreateCompoundBuffer("WorldFogInfo", Shader, FogSetId, fogUniform);
            SetAmbient(Brightness);
            fogData = new Vector4(0f, 0f, 0f, 2.5f);
            musicClip = ResourceManager.LoadAudioClip(MusicClipPath);
            Music = new AudioSource("Music");
            Music.SetBuffer(musicClip);
            Music.Looping = true;
            Music.MaxDistance = float.PositiveInfinity;
            Music.RolloffFactor = 0f;
            Music.ReferenceDistance = 0f;
            Music.MinGain = 1f;
            Music.MaxGain = 1f;
            Music.Play();

            Radio = new AudioSource("Radio");
            Radio.SetBuffer(radioClips[0]);
            Radio.Looping = false;
            Radio.MaxDistance = float.PositiveInfinity;
            Radio.RolloffFactor = 0f;
            Radio.ReferenceDistance = 0f;
            Radio.MinGain = 1f;
            Radio.MaxGain = 1f;
            Radio.Stop();

            miscSource = new AudioSource("Misc");
            miscSource.SetBuffer(fireOnClip);
            miscSource.Looping = false;
            miscSource.MaxDistance = float.PositiveInfinity;
            miscSource.RolloffFactor = 0f;
            miscSource.ReferenceDistance = 0f;
            miscSource.MinGain = 1f;
            miscSource.MaxGain = 1f;
            miscSource.Stop();

            horrorSource = new AudioSource("Horror");
            horrorSource.SetBuffer(horrorClips[0]);
            horrorSource.Looping = false;
            horrorSource.MaxDistance = float.PositiveInfinity;
            horrorSource.RolloffFactor = 0f;
            horrorSource.ReferenceDistance = 0f;
            horrorSource.MinGain = 1f;
            horrorSource.MaxGain = 1f;
            horrorSource.Stop();

            SpawnFloors(201);
            SpawnPlayer();
        }

        public void SpawnPlayer()
        {
            if (player != null)
            {
                Entities.Remove(player);
                player.Dispose();
                player = null;
            }
            player = new BPlayerEntity();
            player.Position = new Vector3(-2.5f, -1.3f, 0.5f);
            player.lookAxis = new Vector2(-90f, 0f);
            player.UpdateLookRotation();
            player.MarkTransformDirty(TransformDirtyFlags.Position);
            Entities.Add(player);
        }

        public string GetFloor(Random rng, int floorNum, FloorEntity.FloorAction action)
        {
            switch (action)
            {
                case FloorEntity.FloorAction.Act_173:
                    return "map2.x";
                case FloorEntity.FloorAction.Cell:
                    return "map1.x";
                case FloorEntity.FloorAction.Trick1:
                    return "map4.x";
                case FloorEntity.FloorAction.Trick2:
                    return "map5.x";
                case FloorEntity.FloorAction.Flash:
                case FloorEntity.FloorAction.Run:
                case FloorEntity.FloorAction.Walk:
                case FloorEntity.FloorAction.Lights:
                case FloorEntity.FloorAction.Trap:
                case FloorEntity.FloorAction.Lock:
                    return "map.x";
                case FloorEntity.FloorAction.Proceed:
                    return "map0.x";
                case FloorEntity.FloorAction.Unknown:
                    switch (rng.Next(0, 21))
                    {
                        case 1:
                        case 2:
                            return "map1.x";
                        case 3:
                        case 4:
                            return "map2.x";
                        case 5:
                        case 6:
                            return "map3.x";
                        case 7:
                            return "map4.x";
                        case 8:
                            return "map5.x";
                        case 9:
                            return "map6.x";
                        case 10:
                            return floorNum > 40 ? "maze.x" : "map.x";
                        default:
                            return "map.x";
                    }
                default:
                    return "map.x";
            }
        }

        public void SpawnFloors(int floorCount)
        {
            string doorPath = Path.Combine(BGame.Instance.Data.GFXDir, "door.jpg");
            string cubePath = "Shaders/cube.gltf";
            Material doorMat = ResourceManager.CreateMaterial("Door", BGame.Instance.Shader);
            CompoundBuffer buffer = ResourceManager.CreateCompoundBuffer(doorPath, doorMat.Shader, UniformConsts.DiffuseTextureSet, ResourceManager.LoadTexture(doorPath), Texture2D.DefaultWhite, Texture2D.DefaultNormal);
            doorMat.SetUniforms(UniformConsts.DiffuseTextureSet, buffer);
            var door = new ModelEntity("Door", cubePath, doorMat);
            door.Model.CompoundBuffers.Add(buffer);
            door.Position = new Vector3(-3.5f, -1f, 0.5f);
            door.Scale = new Vector3(0.5f, 1f, 0.5f);
            door.MarkTransformDirty(TransformDirtyFlags.Position | TransformDirtyFlags.Rotation | TransformDirtyFlags.Scale);
            Entities.Add(door);

            floorActions = new FloorEntity.FloorAction[floorCount];
            floorTimers = new float[floorActions.Length];
            Random rng = new Random(1); // seed set to debug

            int temp = 0;
            floorActions[0] = FloorEntity.FloorAction.Proceed;
            floorTimers[0] = MinFloorTime;
            if (rng.Next(0, 2) == 0)
            {
                temp = rng.Next(2, 4);
                floorActions[temp] = FloorEntity.FloorAction.Radio2;
                floorTimers[temp] = MinFloorTime;
            }
            if (rng.Next(0, 4) != 3)
            {
                temp = rng.Next(4, 6);
                floorActions[temp] = FloorEntity.FloorAction.Radio3;
                floorTimers[temp] = MinFloorTime;
            }

            floorActions[6] = FloorEntity.FloorAction.Lock;
            floorTimers[6] = MinFloorTime;

            if (rng.Next(0, 2) == 0)
            {
                temp = rng.Next(7, 9);
                floorActions[temp] = FloorEntity.FloorAction.Radio4;
                floorTimers[temp] = MinFloorTime;
            }

            temp = rng.Next(9, 11);
            floorActions[temp] = FloorEntity.FloorAction.Breath;
            floorTimers[temp] = MinFloorTime;

            temp = rng.Next(11, 13);
            floorActions[temp] = FloorEntity.FloorAction.Steps;
            floorTimers[temp] = MinFloorTime;

            temp = rng.Next(9, 19);
            floorActions[temp] = FloorEntity.FloorAction.Flash;
            floorTimers[temp] = rng.Next(1, 4);

            temp = rng.Next(19, 22);
            temp = 1;
            floorActions[temp] = FloorEntity.FloorAction.Lights;
            floorTimers[temp] = MinFloorTime;

            // LINE 499 game.bb

            temp = rng.Next(150, 201);
            floorActions[temp] = FloorEntity.FloorAction.Lock;
            floorTimers[temp] = MinFloorTime;

            for (int i = 0; i < floorActions.Length; i++)
            {
                var floor = new FloorEntity(i+1, Path.Combine(Data.GFXDir, GetFloor(rng, i, floorActions[i])), ResourceManager.CreateMaterial("map0", Shader));
                if (i % 2 == 0)
                {
                    floor.Position = new Vector3(0f, -i*2f, 0f);
                    floor.Rotation = Quaternion.Identity;
                }
                else
                {
                    floor.Position = new Vector3(8f, -i*2f, 7f);
                    floor.Rotation = Quaternion.CreateFromAxisAngle(Vector3.UnitY, 180f * (MathF.PI / 180f));
                }
                floor.MarkTransformDirty(TransformDirtyFlags.Position | TransformDirtyFlags.Rotation);
                floors.Add(floor);
                Entities.Add(floor);
            }
        }

        public void CreateEnemy(float x, float y, float z, string texture)
        {
            ClearEnemy();
            string diffusePath = Path.Combine(Data.GFXDir, texture);
            Material material = ResourceManager.CreateMaterial(diffusePath, AnimShader);
            Texture2D diffuse = ResourceManager.LoadTexture(diffusePath);
            CompoundBuffer buffer = ResourceManager.CreateCompoundBuffer(diffusePath, AnimShader, UniformConsts.DiffuseTextureSet, diffuse, Texture2D.DefaultWhite, Texture2D.DefaultNormal);
            enemy = new EnemyEntity("Enemy", Path.Combine(Data.GFXDir, "mental.b3d"), material);
            enemy.Model.CompoundBuffers.Clear();
            enemy.Model.CompoundBuffers.Add(buffer);
            enemy.Position = new Vector3(x, y, z);
            enemy.Scale = Vector3.One * 0.17f;
            enemy.MarkTransformDirty(TransformDirtyFlags.Position | TransformDirtyFlags.Rotation | TransformDirtyFlags.Scale);
            Entities.Add(enemy);
        }

        public void ClearEnemy()
        {
            if (enemy != null)
            {
                Entities.Remove(enemy);
                enemy.Dispose();
                enemy = null;
            }
        }

        public void SetAmbient(int light)
        {
            ForwardConsts.AmbientColor = new Vector4(light / 255f, light / 255f, light / 255f, 1f);
        }

        public void KillViewMatrix(Renderer renderer)
        {
            float killTimerSec = killTimer;
            renderer.ViewMatrix = Matrix4x4.CreateLookAt(player.viewPos, player.viewPos + player.viewDirection, Vector3.UnitY);
            renderer.ViewMatrix *= Matrix4x4.CreateFromYawPitchRoll(0f, -killTimerSec * (MathF.PI / 180f), -(killTimerSec / 2f) * (MathF.PI / 180f));
        }

        public unsafe void Kill()
        {
            TimeScale = 0f;
            if (killTimer == 1)
            {
                Radio.SetBuffer(radioClips[4]);
                Radio.Play();
            }
            killTimer++;
            ForwardConsts.AmbientColor = new Vector4((255f - killTimer) / 255f, (100f - killTimer) / 255f, (100f - killTimer) / 255f, 1f);
            if (killTimer > 90)
            {
                // Silk.NET.SDL.Sdl.GetApi().ShowSimpleMessageBox(0, Name, "NO", null);
                Program.IsClosing = true;
            }
        }

        public void UpdateFloors(double dt)
        {
            float delta = (float)dt;
            currentFloor = (int)((-player.Position.Y - 0.5f) / 2f);
            var plrPos = player.Position;
            if (enemy != null)
                plrPos.Y = enemy.Position.Y;

            for (int i = 0; i < floorTimers.Length; i++)
            {
                if (floorTimers[i] > 0f)
                {
                    Vector3 floorPos = new Vector3(4f, -1f-(i)*2f, 0f);
                    float startX, endX;

                    if (i % 2 == 1)
                    {
                        floorPos.Z = 6.5f;
                        startX = 7.5f;
                        endX = 0.5f;
                    }
                    else
                    {
                        floorPos.Z = 0.5f;
                        startX = 0.5f;
                        endX = 7.5f;
                    }

                    switch (floorActions[i])
                    {
                        case FloorEntity.FloorAction.Lights:
                        {
                            if (floorTimers[i] > MinFloorTime)
                            {
                                float pastTimer = floorTimers[i];
                                floorTimers[i] += delta;
                                // LINE 772 game.bb
                                if (floorTimers[i] > 1.6f && pastTimer <= 1.6f)
                                {
                                    CreateEnemy(endX, floorPos.Y - 1f, floorPos.Z, "mental.jpg");
                                    enemy.speed = 0.01f;
                                }
                                if (floorTimers[i] > 3.5f && pastTimer <= 3.5f)
                                {
                                    miscSource.SetBuffer(fireOnClip);
                                    miscSource.Play();
                                }
                                if (floorTimers[i] > 4.16f && pastTimer <= 4.16f)
                                {
                                    SetAmbient(Brightness);
                                }
                                if (floorTimers[i] > 4.83f && pastTimer <= 4.83f)
                                {
                                    horrorSource.SetBuffer(horrorClips[2]);
                                    horrorSource.Play();
                                }
                                if (floorTimers[i] > 7.5f && pastTimer <= 7.5f)
                                {
                                    ClearEnemy();
                                    floorTimers[i] = 0f;
                                }

                                if (floorTimers[i] > 1.6f)
                                {
                                    enemy.startAnimTime = 1d;
                                    enemy.endAnimTime = 14d;
                                    enemy.animSpeed = 0.15d;
                                    if (Vector3.Distance(plrPos, enemy.Position) < 0.8f)
                                    {
                                        killTimer = Math.Max(killTimer, 1);
                                    }
                                }
                            }
                        }
                        break;
                        case FloorEntity.FloorAction.Run:
                        {
                            if (floorTimers[i] > MinFloorTime)
                            {
                                float pastTimer = floorTimers[i];
                                floorTimers[i] += delta;
                                
                                if (floorTimers[i] > 1.6f && pastTimer <= 1.6f)
                                {
                                    CreateEnemy(endX, floorPos.Y - 1f, floorPos.Z, "mental.jpg");
                                    enemy.speed = 0.01f;
                                }

                                if (floorTimers[i] > 1.6f)
                                {
                                    enemy.startAnimTime = 1d;
                                    enemy.endAnimTime = 14d;
                                    enemy.animSpeed = 0.15d;
                                    if (Vector3.Distance(player.Position, enemy.Position) < 0.8f)
                                    {
                                        killTimer = Math.Max(killTimer, 1);
                                    }
                                }
                            }
                        }
                        break;
                    }
                }
            }

            if (floorTimers[currentFloor] > 0f)
            {
                Vector3 floorPos = new Vector3(4f, -1f-(currentFloor)*2f, 0f);
                float startX, endX;

                if (currentFloor % 2 == 1)
                {
                    floorPos.Z = 6.5f;
                    startX = 7.5f;
                    endX = 0.5f;
                }
                else
                {
                    floorPos.Z = 0.5f;
                    startX = 0.5f;
                    endX = 7.5f;
                }

                switch (floorActions[currentFloor])
                {
                    case FloorEntity.FloorAction.Lights:
                    {
                        if (floorTimers[currentFloor] <= MinFloorTime)
                        {
                            if (Vector3.Distance(player.Position, floorPos) < 1f)
                            {
                                horrorSource.SetBuffer(horrorClips[1]);
                                horrorSource.Play();
                                miscSource.SetBuffer(fireOffClip);
                                miscSource.Play();
                                floorTimers[currentFloor] = MinFloorTime * 2f;
                                SetAmbient(25);
                            }
                        }
                    }
                    break;
                    case FloorEntity.FloorAction.Proceed:
                    {
                        if (floorTimers[currentFloor] + delta > 2.5f)
                        {
                            Radio.SetBuffer(radioClips[0]);
                            Radio.Play();
                            floorTimers[currentFloor] = 0f;
                        }
                        else
                            floorTimers[currentFloor] += delta;
                    }
                    break;
                    case FloorEntity.FloorAction.Radio2:
                        Radio.SetBuffer(radioClips[1]);
                        Radio.Play();
                        floorTimers[currentFloor] = 0f;
                        break;
                    case FloorEntity.FloorAction.Radio3:
                        Radio.SetBuffer(radioClips[2]);
                        Radio.Play();
                        floorTimers[currentFloor] = 0f;
                        break;
                    case FloorEntity.FloorAction.Radio4:
                        Radio.SetBuffer(radioClips[3]);
                        Radio.Play();
                        floorTimers[currentFloor] = 0f;
                        break;
                }
            }

            prevFrameFloor = currentFloor;
        }

        public override void PreDraw(Renderer renderer, double dt)
        {
            base.PreDraw(renderer, dt);
            Music.Position = renderer.ViewPosition;
            Radio.Position = renderer.ViewPosition;
            miscSource.Position = renderer.ViewPosition;
            horrorSource.Position = renderer.ViewPosition;
            if (killTimer > 0)
                KillViewMatrix(renderer);
        }

        public override void Draw(Renderer renderer, double dt)
        {
            if (DebugMode)
                Program.DrawDebugWindow();
            fogUniform.UploadData(fogData);
            foreach (var item in ResourceManager.AllResources)
            {
                if (item is Material mat && mat.Shader.HasSet(FogSetId))
                {
                    mat.SetUniforms(FogSetId, fogBuffer);
                }
            }
            base.Draw(renderer, dt);
            Music.Position = renderer.ViewPosition;
            Radio.Position = renderer.ViewPosition;
            miscSource.Position = renderer.ViewPosition;
            horrorSource.Position = renderer.ViewPosition;
        }

        public override void Tick(double dt)
        {
            var oldPlayerPos = player.Position;
            base.Tick(dt);
            TimeScale = Program.IsFocused ? 1f : 0f;
            UpdateFloors(dt);
            if (killTimer > 0)
                Kill();
        }

        public override void Dispose()
        {
            Music.Dispose();
            foreach (var ent in Entities)
            {
                ent.Dispose();
            }
            Entities.Clear();
            base.Dispose();
        }
    }
}