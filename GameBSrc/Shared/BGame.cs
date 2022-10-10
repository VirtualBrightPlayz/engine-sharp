using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
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
        public const uint FogSetId = 5;
        public const float MaxAudioDist = 1000f;
        public static BGame Instance => Current as BGame;
        public override string Name => "SCP-087-B CSharp";
        public GameData Data { get; private set; }
        public BPlayerEntity player;
        public AudioSource Music;
        public AudioClip musicClip;
        public AudioSource Radio;
        public AudioSource miscSource;
        public AudioSource horrorSource;
        public AudioSource soundEmitter;
        public Task<AudioClip>[] radioClips => new Task<AudioClip>[]
        {
            ResourceManager.LoadAudioClip(Path.Combine(BGame.Instance.Data.SFXDir, "radio1.ogg")),
            ResourceManager.LoadAudioClip(Path.Combine(BGame.Instance.Data.SFXDir, "radio2.ogg")),
            ResourceManager.LoadAudioClip(Path.Combine(BGame.Instance.Data.SFXDir, "radio3.ogg")),
            ResourceManager.LoadAudioClip(Path.Combine(BGame.Instance.Data.SFXDir, "radio4.ogg")),
            ResourceManager.LoadAudioClip(Path.Combine(BGame.Instance.Data.SFXDir, "death.ogg")),
        };
        public Task<AudioClip>[] horrorClips => new Task<AudioClip>[]
        {
            ResourceManager.LoadAudioClip(Path.Combine(BGame.Instance.Data.SFXDir, "horror1.ogg")),
            ResourceManager.LoadAudioClip(Path.Combine(BGame.Instance.Data.SFXDir, "horror2.ogg")),
            ResourceManager.LoadAudioClip(Path.Combine(BGame.Instance.Data.SFXDir, "horror3.ogg")),
        };
        public Task<AudioClip> stoneClip => ResourceManager.LoadAudioClip(Path.Combine(BGame.Instance.Data.SFXDir, "stone.ogg"));
        public Task<AudioClip> fireOnClip => ResourceManager.LoadAudioClip(Path.Combine(BGame.Instance.Data.SFXDir, "match.ogg"));
        public Task<AudioClip> fireOffClip => ResourceManager.LoadAudioClip(Path.Combine(BGame.Instance.Data.SFXDir, "fireout.ogg"));
        public Task<AudioClip> loudStepClip => ResourceManager.LoadAudioClip(Path.Combine(BGame.Instance.Data.SFXDir, "loudstep.ogg"));
        public Task<AudioClip> dontLookClip => ResourceManager.LoadAudioClip(Path.Combine(BGame.Instance.Data.SFXDir, "dontlook.ogg"));
        public Task<Texture2D> logo => ResourceManager.LoadTexture(Path.Combine(BGame.Instance.Data.GFXDir, "scp.jpg"));
        public string MusicClipPath => Path.Combine(Data.SFXDir, "music.ogg");
        public List<StaticModelEntity> floors = new List<StaticModelEntity>();
        public Task<GraphicsShader> Shader => ResourceManager.LoadShader("Shaders/MainMeshFog");
        public Task<GraphicsShader> AnimShader => ResourceManager.LoadShader("Shaders/MainMeshAnim");
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
        public EnemyEntity enemy = null;
        public Entity currentObject = null;
        public int Brightness = 40;
        public bool DebugMode = false;
        public uint frameCount = 0;

        public BGame() : base()
        {
            Data = new GameData("GameB");
        }

        public override Task Setup()
        {
            return base.Setup();
        }

        public async Task Init()
        {
            fogUniform = await ResourceManager.CreateUniformBuffer("WorldFogInfo", (uint)4 * 4);
            fogBuffer = await ResourceManager.CreateCompoundBuffer("WorldFogInfo", await Shader, FogSetId, fogUniform);
            if ((await Shader).Name == (await AnimShader).Name)
            {
                throw new Exception("Oops");
            }
            SetAmbient(Brightness);
            fogData = new Vector4(0f, 0f, 0f, 2.5f);
            musicClip = await ResourceManager.LoadAudioClip(MusicClipPath);
            Music = new AudioSource("Music");
            Music.SetBuffer(musicClip);
            Music.Looping = true;
            Music.MaxDistance = MaxAudioDist;
            Music.RolloffFactor = 0f;
            Music.ReferenceDistance = MaxAudioDist;
            Music.MinGain = 1f;
            Music.MaxGain = 1f;
            Music.Stop();

            Radio = new AudioSource("Radio");
            Radio.SetBuffer(await radioClips[0]);
            Radio.Looping = false;
            Radio.MaxDistance = MaxAudioDist;
            Radio.RolloffFactor = 0f;
            Radio.ReferenceDistance = MaxAudioDist;
            Radio.MinGain = 1f;
            Radio.MaxGain = 1f;
            Radio.Stop();

            miscSource = new AudioSource("Misc");
            miscSource.SetBuffer(await fireOnClip);
            miscSource.Looping = false;
            miscSource.MaxDistance = MaxAudioDist;
            miscSource.RolloffFactor = 0f;
            miscSource.ReferenceDistance = MaxAudioDist;
            miscSource.MinGain = 1f;
            miscSource.MaxGain = 1f;
            miscSource.Stop();

            horrorSource = new AudioSource("Horror");
            horrorSource.SetBuffer(await horrorClips[0]);
            horrorSource.Looping = false;
            horrorSource.MaxDistance = MaxAudioDist;
            horrorSource.RolloffFactor = 0f;
            horrorSource.ReferenceDistance = MaxAudioDist;
            horrorSource.MinGain = 1f;
            horrorSource.MaxGain = 1f;
            horrorSource.Stop();

            soundEmitter = new AudioSource("SoundEmitter");
            soundEmitter.SetBuffer(await loudStepClip);
            soundEmitter.Looping = false;
            soundEmitter.MaxDistance = MaxAudioDist;
            soundEmitter.RolloffFactor = 0f;
            soundEmitter.ReferenceDistance = MaxAudioDist;
            soundEmitter.MinGain = 1f;
            soundEmitter.MaxGain = 1f;
            soundEmitter.Stop();

            await SpawnFloors(201);
            SpawnPlayer();
            Music.Play();
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

        public async Task SpawnFloors(int floorCount)
        {
            string doorPath = Path.Combine(BGame.Instance.Data.GFXDir, "door.jpg");
            string cubePath = "Shaders/cube.gltf";
            Material doorMat = await ResourceManager.CreateMaterial("Door", await Shader);
            CompoundBuffer buffer = await ResourceManager.CreateCompoundBuffer(doorPath, await Shader, UniformConsts.DiffuseTextureSet, await ResourceManager.LoadTexture(doorPath), await Texture2D.DefaultWhite, await Texture2D.DefaultNormal);
            doorMat.SetUniforms(UniformConsts.DiffuseTextureSet, buffer);
            var door = new ModelEntity("Door", cubePath, doorMat);
            await door.Create();
            door.Model.CompoundBuffers.Clear();
            door.Model.CompoundBuffers.Add(buffer);
            door.Position = new Vector3(-3.5f, -1f, 0.5f);
            door.Scale = new Vector3(0.5f, 1f, 0.5f);
            door.MarkTransformDirty(TransformDirtyFlags.Position | TransformDirtyFlags.Rotation | TransformDirtyFlags.Scale);
            Entities.Add(door);

            floorActions = new FloorEntity.FloorAction[floorCount];
            floorTimers = new float[floorActions.Length];
            Random rng = new Random(Random.Shared.Next()); // seed set to debug

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
            floorTimers[temp] = MinFloorTime * rng.Next(1, 4);

            temp = rng.Next(19, 22);
            floorActions[temp] = FloorEntity.FloorAction.Lights;
            floorTimers[temp] = MinFloorTime;

            switch (rng.Next(1, 5))
            {
                case 1:
                    temp = rng.Next(24, 28);
                    floorActions[temp] = FloorEntity.FloorAction.Trick1;
                    floorTimers[temp] = MinFloorTime;
                    break;
                case 2:
                    temp = rng.Next(24, 28);
                    floorActions[temp] = FloorEntity.FloorAction.Trick2;
                    floorTimers[temp] = MinFloorTime;
                    break;
            }

            temp = rng.Next(28, 33);
            floorActions[temp] = FloorEntity.FloorAction.Run;
            floorTimers[temp] = MinFloorTime;

            temp = rng.Next(33, 37);
            floorActions[temp] = FloorEntity.FloorAction.Act_173;
            floorTimers[temp] = MinFloorTime;

            temp = rng.Next(39, 60);
            floorActions[temp] = FloorEntity.FloorAction.Run;
            floorTimers[temp] = MinFloorTime;

            temp = rng.Next(39, 60);
            floorActions[temp] = FloorEntity.FloorAction.Roar;
            floorTimers[temp] = MinFloorTime;

            temp = rng.Next(39, 60);
            floorActions[temp] = FloorEntity.FloorAction.Trap;
            floorTimers[temp] = MinFloorTime;

            temp = rng.Next(39, 60);
            floorActions[temp] = FloorEntity.FloorAction.Flash;
            floorTimers[temp] = MinFloorTime;

            // LINE 532 game.bb
            FloorEntity.FloorAction randAct = FloorEntity.FloorAction.Unknown;
            for (int i = 0; i < 8; i++)
            {
                switch (rng.Next(1, 11))
                {
                    case 1:
                    case 9:
                        randAct = FloorEntity.FloorAction.Cell;
                        break;
                    case 2:
                        randAct = FloorEntity.FloorAction.Flash;
                        break;
                    case 3:
                        randAct = FloorEntity.FloorAction.Trick1;
                        break;
                    case 4:
                        randAct = FloorEntity.FloorAction.Trick2;
                        break;
                    case 5:
                        randAct = FloorEntity.FloorAction.Breath;
                        break;
                    case 6:
                        randAct = FloorEntity.FloorAction.Steps;
                        break;
                    case 7:
                        randAct = FloorEntity.FloorAction.Trap;
                        break;
                    case 8:
                        randAct = FloorEntity.FloorAction.Roar;
                        break;
                }

                bool temper = false;
                while (!temper)
                {
                    temp = rng.Next(24, 69);
                    if (floorActions[temp] == FloorEntity.FloorAction.Unknown)
                    {
                        floorActions[temp] = randAct;
                        floorTimers[temp] = MinFloorTime;
                        temper = true;
                    }
                }
            }

            randAct = FloorEntity.FloorAction.Unknown;
            for (int i = 0; i < 60; i++)
            {
                switch (rng.Next(1, 11))
                {
                    case 1:
                    case 9:
                        randAct = FloorEntity.FloorAction.Cell;
                        break;
                    case 2:
                        randAct = FloorEntity.FloorAction.Lights;
                        break;
                    case 3:
                        randAct = FloorEntity.FloorAction.Trick1;
                        break;
                    case 4:
                        randAct = FloorEntity.FloorAction.Trick2;
                        break;
                    case 5:
                        randAct = FloorEntity.FloorAction.Breath;
                        break;
                    case 6:
                        randAct = FloorEntity.FloorAction.Steps;
                        break;
                    case 7:
                        randAct = FloorEntity.FloorAction.Trap;
                        break;
                    case 8:
                        randAct = FloorEntity.FloorAction.Roar;
                        break;
                }

                bool temper = false;
                while (!temper)
                {
                    temp = rng.Next(74, 200);
                    if (floorActions[temp] == FloorEntity.FloorAction.Unknown)
                    {
                        floorActions[temp] = randAct;
                        floorTimers[temp] = MinFloorTime;
                        temper = true;
                    }
                }
            }

            temp = rng.Next(150, 201);
            floorActions[temp] = FloorEntity.FloorAction.Darkness;
            floorTimers[temp] = MinFloorTime;

            for (int i = 0; i < floorActions.Length; i++)
            {
                var floor = new FloorEntity(i+1, Path.Combine(Data.GFXDir, GetFloor(rng, i, floorActions[i])), await ResourceManager.CreateMaterial("map0", await Shader));
                await floor.Create(true);
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

        public async Task CreateObject(float x, float y, float z)
        {
            ClearObject();
            string diffusePath = Path.Combine(Data.GFXDir, "brickwall.jpg");
            Material material = await ResourceManager.CreateMaterial(diffusePath, await Shader);
            Texture2D diffuse = await ResourceManager.LoadTexture(diffusePath);
            CompoundBuffer buffer = await ResourceManager.CreateCompoundBuffer(diffusePath, await Shader, UniformConsts.DiffuseTextureSet, diffuse, await Texture2D.DefaultWhite, await Texture2D.DefaultNormal);
            currentObject = new StaticModelEntity("CurrentObject", "Shaders/cube.gltf", material);
            await (currentObject as StaticModelEntity).Create(true);
            currentObject.Position = new Vector3(x, y, z);
            currentObject.Scale = new Vector3(0.5f, 1f, 0.5f);
            currentObject.MarkTransformDirty(TransformDirtyFlags.Position | TransformDirtyFlags.Rotation | TransformDirtyFlags.Scale);
            Entities.Add(currentObject);
        }

        public void ClearObject()
        {
            if (currentObject != null)
            {
                Entities.Remove(currentObject);
                currentObject.Dispose();
                currentObject = null;
            }
        }

        public async Task CreateEnemy(float x, float y, float z, string texture)
        {
            ClearEnemy();
            string diffusePath = Path.Combine(Data.GFXDir, texture);
            Material material = await ResourceManager.CreateMaterial($"{diffusePath}_Anim", await AnimShader);
            Texture2D diffuse = await ResourceManager.LoadTexture(diffusePath);
            CompoundBuffer buffer = await ResourceManager.CreateCompoundBuffer($"{diffusePath}_Anim", await AnimShader, UniformConsts.DiffuseTextureSet, diffuse, await Texture2D.DefaultWhite, await Texture2D.DefaultNormal);
            enemy = new EnemyEntity("Enemy", Path.Combine(Data.GFXDir, "mental.b3d"), material);
            enemy.buffer = buffer;
            await enemy.Create();
            enemy.Position = new Vector3(x, y, z);
            enemy.Scale = Vector3.One * 0.17f;
            enemy.MarkTransformDirty(TransformDirtyFlags.Position | TransformDirtyFlags.Rotation | TransformDirtyFlags.Scale);
            enemy.startAnimTime = 0d;
            enemy.endAnimTime = 14d;
            enemy.animSpeed = 0d;
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

        public void SetFogDist(float dist)
        {
            fogData.W = dist;
        }

        public void KillViewMatrix(Renderer renderer)
        {
            float killTimerSec = killTimer;
            renderer.ViewMatrix = Matrix4x4.CreateLookAt(player.viewPos, player.viewPos + player.viewDirection, Vector3.UnitY);
            renderer.ViewMatrix *= Matrix4x4.CreateFromYawPitchRoll(0f, -killTimerSec * (MathF.PI / 180f), -(killTimerSec / 2f) * (MathF.PI / 180f));
        }

        public async Task Kill()
        {
            TimeScale = 0f;
            if (killTimer == 1)
            {
                Radio.SetBuffer(await radioClips[4]);
                Radio.Play();
            }
            killTimer++;
            ForwardConsts.AmbientColor = new Vector4((255f - killTimer) / 255f, (100f - killTimer) / 255f, (100f - killTimer) / 255f, 1f);
            if (killTimer > 90)
            {
                // Silk.NET.SDL.Sdl.GetApi().ShowSimpleMessageBox(0, Name, "NO", null);
                MiscGlobals.IsClosing = true;
            }
        }

        public async Task UpdateFloors(double dt)
        {
            float delta = (float)dt;
            currentFloor = (int)((-player.Position.Y - 0.5f) / 2f);
            currentFloor = Math.Clamp(currentFloor, 0, 200);
            var plrPos = player.Position;
            if (enemy != null)
                plrPos.Y = enemy.Position.Y;

            for (int i = 0; i < floorTimers.Length; i++)
            {
                if (Math.Abs(i - currentFloor) > 3)
                {
                    continue;
                }
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
                                    await CreateEnemy(endX, floorPos.Y - 1f, floorPos.Z, "mental.jpg");
                                    enemy.speed = 0.01f;
                                }
                                if (floorTimers[i] > 3.5f && pastTimer <= 3.5f)
                                {
                                    miscSource.SetBuffer(await fireOnClip);
                                    miscSource.Play();
                                }
                                if (floorTimers[i] > 4.16f && pastTimer <= 4.16f)
                                {
                                    SetAmbient(Brightness);
                                }
                                if (floorTimers[i] > 4.83f && pastTimer <= 4.83f)
                                {
                                    horrorSource.SetBuffer(await horrorClips[2]);
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
                                    await CreateEnemy(endX, floorPos.Y - 1f, floorPos.Z, "mental.jpg");
                                    enemy.speed = 0.03f;
                                }
                                if ((floorTimers[i] > 2.16f && pastTimer <= 2.16f) || (floorTimers[i] > 4.3f && pastTimer <= 4.3f) || (floorTimers[i] > 6.3f && pastTimer <= 6.3f))
                                {
                                    if (pastTimer <= 2.16f)
                                        horrorSource.SetBuffer(await horrorClips[0]);
                                    horrorSource.Play();
                                    SetFogDist(20f);
                                    SetAmbient(Brightness);
                                    enemy.speed = 0f;
                                }
                                if ((floorTimers[i] > 2.83f && pastTimer <= 2.83f) || (floorTimers[i] > 5f && pastTimer <= 5f))
                                {
                                    enemy.speed = 0.03f;
                                    SetFogDist(2.5f);
                                    SetAmbient(15);
                                }
                                if ((floorTimers[i] > 7.5f && pastTimer <= 7.5f))
                                {
                                    SetFogDist(2.5f);
                                    SetAmbient(Brightness);
                                    ClearEnemy();
                                    floorTimers[i] = 0f;
                                }

                                if (floorTimers[i] > 1.6f)
                                {
                                    enemy.startAnimTime = 0d;
                                    enemy.endAnimTime = 14d;
                                    enemy.animSpeed = 0d;
                                    if (Vector3.Distance(plrPos, enemy.Position) < 0.8f)
                                    {
                                        killTimer = Math.Max(killTimer, 1);
                                    }
                                }
                            }
                        }
                        break;
                        case FloorEntity.FloorAction.Trap:
                        {
                            if (floorTimers[i] > MinFloorTime * 2f)
                            {
                                float pastTimer = floorTimers[i];
                                floorTimers[i] += delta;

                                if ((floorTimers[i] > 8.3f && pastTimer <= 8.3f))
                                {
                                    ClearObject();
                                    miscSource.SetBuffer(await stoneClip);
                                    miscSource.Play();
                                }
                                if ((floorTimers[i] > 16.6f && pastTimer <= 16.6f))
                                {
                                    ClearEnemy();
                                    floorTimers[i] = 0f;
                                }

                                if (enemy != null)
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
                        case FloorEntity.FloorAction.Act_173:
                        {
                            if (floorTimers[i] > MinFloorTime)
                            {
                                float pastTimer = floorTimers[i];
                                floorTimers[i] += delta;

                                if (floorTimers[i] > 2.5f)
                                {
                                    if (player.CanSee(enemy.Center))
                                    {
                                        enemy.speed = 0f;
                                        enemy.startAnimTime = 206f;
                                        enemy.endAnimTime = 250f;
                                        enemy.animSpeed = 0.05f;
                                        if (floorTimers[i] < 166.66f)
                                        {
                                            horrorSource.SetBuffer(await horrorClips[2]);
                                            horrorSource.Play();
                                            floorTimers[i] = 166.683f;
                                        }
                                    }
                                    else
                                    {
                                        enemy.speed = 0.02f;
                                        if (Vector3.Distance(plrPos, enemy.Position) < 0.8f)
                                        {
                                            killTimer = Math.Max(killTimer, 1);
                                            floorTimers[i] = 0f;
                                        }
                                    }
                                }
                                else
                                {
                                    enemy.speed = 0f;
                                }
                                if (floorTimers[i] % 11f > 0.25f && pastTimer % 11f <= 0.25f)
                                {
                                    soundEmitter.SetBuffer(await dontLookClip);
                                    soundEmitter.Position = enemy.Position;
                                    soundEmitter.Play();
                                }
                            }
                            if (currentFloor > i)
                            {
                                ClearEnemy();
                                floorTimers[i] = 0f;
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
                                horrorSource.SetBuffer(await horrorClips[1]);
                                horrorSource.Play();
                                miscSource.SetBuffer(await fireOffClip);
                                miscSource.Play();
                                floorTimers[currentFloor] = MinFloorTime * 2f;
                                SetAmbient(25);
                            }
                        }
                    }
                    break;
                    case FloorEntity.FloorAction.Run:
                    {
                        if (floorTimers[currentFloor] <= MinFloorTime)
                        {
                            if (Vector3.Distance(player.Position, floorPos) < 3f)
                            {
                                horrorSource.SetBuffer(await horrorClips[1]);
                                horrorSource.Play();
                                miscSource.SetBuffer(await fireOffClip);
                                miscSource.Play();
                                floorTimers[currentFloor] = MinFloorTime * 2f;
                                SetAmbient(25);
                            }
                        }
                    }
                    break;
                    case FloorEntity.FloorAction.Trap:
                    {
                        if (floorTimers[currentFloor] <= MinFloorTime)
                        {
                            await CreateObject(currentFloor % 2 == 0 ? endX : endX, floorPos.Y, floorPos.Z);
                            floorTimers[currentFloor] = MinFloorTime * 2f;
                        }
                        else if (floorTimers[currentFloor] <= MinFloorTime * 2f)
                        {
                            if (Vector3.Distance(player.Position, floorPos) < 1f)
                            {
                                await CreateEnemy(startX, floorPos.Y - 1f, floorPos.Z, "mental.jpg");
                                enemy.speed = 0.01f;
                                enemy.startAnimTime = 0d;
                                enemy.endAnimTime = 1d;
                                enemy.animSpeed = 0d;
                                horrorSource.SetBuffer(await horrorClips[Random.Shared.Next(0, 3)]);
                                horrorSource.Play();
                                floorTimers[currentFloor] = MinFloorTime * 3f;
                            }
                        }
                    }
                    break;
                    case FloorEntity.FloorAction.Proceed:
                    {
                        if (floorTimers[currentFloor] + delta > 2.5f)
                        {
                            Radio.SetBuffer(await radioClips[0]);
                            Radio.Play();
                            floorTimers[currentFloor] = 0f;
                        }
                        else
                            floorTimers[currentFloor] += delta;
                    }
                    break;
                    case FloorEntity.FloorAction.Radio2:
                        Radio.SetBuffer(await radioClips[1]);
                        Radio.Play();
                        floorTimers[currentFloor] = 0f;
                        break;
                    case FloorEntity.FloorAction.Radio3:
                        Radio.SetBuffer(await radioClips[2]);
                        Radio.Play();
                        floorTimers[currentFloor] = 0f;
                        break;
                    case FloorEntity.FloorAction.Radio4:
                        Radio.SetBuffer(await radioClips[3]);
                        Radio.Play();
                        floorTimers[currentFloor] = 0f;
                        break;
                    case FloorEntity.FloorAction.Flash:
                    {
                        if (floorTimers[currentFloor] <= MinFloorTime)
                        {
                            if (Vector3.Distance(player.Position, new Vector3(endX, floorPos.Y, floorPos.Z)) < 1.5f)
                            {
                                await CreateEnemy(endX, floorPos.Y - 1f, floorPos.Z, "mental.jpg");
                                enemy.speed = 0f;
                                enemy.startAnimTime = 0d;
                                enemy.endAnimTime = 1d;
                                enemy.animSpeed = 0d;
                                horrorSource.SetBuffer(await horrorClips[Random.Shared.Next(0, 3)]);
                                horrorSource.Play();
                                floorTimers[currentFloor] = MinFloorTime * 5f;
                            }
                        }
                        else if (floorTimers[currentFloor] <= MinFloorTime * 2)
                        {
                            if (Vector3.Distance(player.Position, new Vector3(floorPos.X, floorPos.Y, floorPos.Z)) < 1.5f)
                            {
                                await CreateEnemy(floorPos.X, floorPos.Y - 1f, floorPos.Z, "mental.jpg");
                                enemy.speed = 0f;
                                enemy.startAnimTime = 0d;
                                enemy.endAnimTime = 1d;
                                enemy.animSpeed = 0d;
                                horrorSource.SetBuffer(await horrorClips[Random.Shared.Next(0, 3)]);
                                horrorSource.Play();
                                floorTimers[currentFloor] = MinFloorTime * 5f;
                            }
                        }
                        else if (floorTimers[currentFloor] <= MinFloorTime * 3)
                        {
                            if (Vector3.Distance(player.Position, new Vector3(startX, floorPos.Y, floorPos.Z)) < 1.5f)
                            {
                                await CreateEnemy(startX, floorPos.Y - 1f, floorPos.Z, "mental.jpg");
                                enemy.speed = 0f;
                                enemy.startAnimTime = 0d;
                                enemy.endAnimTime = 1d;
                                enemy.animSpeed = 0d;
                                horrorSource.SetBuffer(await horrorClips[Random.Shared.Next(0, 3)]);
                                horrorSource.Play();
                                floorTimers[currentFloor] = MinFloorTime * 5f;
                            }
                        }
                        else
                        {
                            floorTimers[currentFloor] += delta;
                            if (floorTimers[currentFloor] > 0.5f)
                            {
                                ClearEnemy();
                                floorTimers[currentFloor] = 0f;
                            }
                        }
                    }
                    break;
                    case FloorEntity.FloorAction.Steps:
                    {
                        if (floorTimers[currentFloor] <= MinFloorTime)
                        {
                            floorTimers[currentFloor] = MinFloorTime * 2f;
                            soundEmitter.SetBuffer(await loudStepClip);
                        }
                        else if (floorTimers[currentFloor] < 50f)
                        {
                            if (Vector3.Distance(player.Position, new Vector3(endX, floorPos.Y, floorPos.Z)) < 6f)
                            {
                                soundEmitter.Position = new Vector3(floorPos.X + (floorPos.X - endX)* 1.1f, floorPos.Y, floorPos.Z);
                                float lastFrameTimer = floorTimers[currentFloor];
                                floorTimers[currentFloor] += delta;
                                if (floorTimers[currentFloor] % 2.5f < Random.Shared.Next(1, 51) / 60f)
                                {
                                    soundEmitter.Play();
                                    floorTimers[currentFloor] = 0.85f;
                                }
                            }
                        }
                    }
                    break;
                    case FloorEntity.FloorAction.Breath:
                    {
                        if (floorTimers[currentFloor] <= MinFloorTime)
                        {
                            floorTimers[currentFloor] = MinFloorTime * 2f;
                            soundEmitter.SetBuffer(await loudStepClip);
                        }
                        else if (floorTimers[currentFloor] < 50f)
                        {
                            if (Vector3.Distance(player.Position, new Vector3(endX, floorPos.Y, floorPos.Z)) < 7f)
                            {
                                soundEmitter.Position = new Vector3(floorPos.X + (floorPos.X - endX)* 1.1f, floorPos.Y, floorPos.Z);
                                float lastFrameTimer = floorTimers[currentFloor];
                                floorTimers[currentFloor] += delta;
                                if (floorTimers[currentFloor] % 10f < 0.16f)
                                {
                                    soundEmitter.Play();
                                    floorTimers[currentFloor] = 0.183f;
                                }
                            }
                        }
                    }
                    break;
                    case FloorEntity.FloorAction.Act_173:
                    {
                        if (floorTimers[currentFloor] <= MinFloorTime)
                        {
                            if (currentFloor % 2 == 0)
                            {
                                await CreateEnemy(startX - 1.8f, floorPos.Y - 1f, floorPos.Z - 6f, "173.jpg");
                            }
                            else
                            {
                                await CreateEnemy(startX + 1.8f, floorPos.Y - 1f, floorPos.Z + 6f, "173.jpg");
                            }
                            enemy.fog = false;
                            enemy.speed = 0f;
                            floorTimers[currentFloor] = MinFloorTime * 2f;
                        }
                    }
                    break;
                }
            }

            prevFrameFloor = currentFloor;
        }

        public override async Task PreDraw(Renderer renderer, double dt)
        {
            if (player == null)
                return;
            await base.PreDraw(renderer, dt);
            Music.Position = renderer.ViewPosition;
            Radio.Position = renderer.ViewPosition;
            miscSource.Position = renderer.ViewPosition;
            horrorSource.Position = renderer.ViewPosition;
            if (killTimer > 0)
                KillViewMatrix(renderer);
        }

        public override async Task Draw(Renderer renderer, double dt)
        {
            frameCount++;
            if (player == null)
            {
                renderer.Blit(await logo);
                return;
            }
            if (DebugMode)
                DebugGlobals.DrawDebugWindow();
            fogUniform.UploadData(renderer, fogData);
            foreach (var item in ResourceManager.AllResources)
            {
                if (item is Material mat)
                {
                    if (mat.Shader.HasSet(FogSetId) && mat.Shader == await Shader)
                    {
                        if (mat.Shader.GetSetName(FogSetId) == "WorldInfo1")
                            mat.SetUniforms(FogSetId, fogBuffer);
                    }
                }
            }
            await base.Draw(renderer, dt);
            Music.Position = renderer.ViewPosition;
            Radio.Position = renderer.ViewPosition;
            miscSource.Position = renderer.ViewPosition;
            horrorSource.Position = renderer.ViewPosition;
        }

        public override async Task Tick(double dt)
        {
            if (player == null)
            {
                if (frameCount > 1 && (Music == null))
                    await Init();
                return;
            }
            var oldPlayerPos = player.Position;
            await base.Tick(dt);
            TimeScale = MiscGlobals.IsFocused ? 1f : 0f;
            await UpdateFloors(dt);
            if (killTimer > 0)
                await Kill();
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