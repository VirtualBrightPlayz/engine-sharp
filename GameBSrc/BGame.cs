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
        public static BGame Instance => Program.Game as BGame;
        public override string Name => "SCP-087-B CSharp";
        public GameData Data { get; private set; }
        public BPlayerEntity player;
        public AudioSource Music;
        public AudioClip musicClip;
        public AudioSource Radio;
        public AudioClip[] radioClips => new AudioClip[]
        {
            ResourceManager.LoadAudioClip(Path.Combine(BGame.Instance.Data.SFXDir, "radio1.ogg")),
            ResourceManager.LoadAudioClip(Path.Combine(BGame.Instance.Data.SFXDir, "radio2.ogg")),
            ResourceManager.LoadAudioClip(Path.Combine(BGame.Instance.Data.SFXDir, "radio3.ogg")),
            ResourceManager.LoadAudioClip(Path.Combine(BGame.Instance.Data.SFXDir, "radio4.ogg")),
        };
        public string MusicClipPath => Path.Combine(Data.SFXDir, "music.ogg");
        public List<StaticModelEntity> floors = new List<StaticModelEntity>();
        public GraphicsShader Shader => ResourceManager.LoadShader("Shaders/MainMesh");
        public int radioState = 0;
        public int currentFloor = 0;

        public BGame() : base()
        {
            Data = new GameData("GameB");
        }

        public override void Setup()
        {
            base.Setup();
            ForwardConsts.AmbientColor = new Vector4(0.2f, 0.2f, 0.2f, 1f);
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

            UpdateFloors();
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

        public void UpdateFloors()
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
            const string map0 = "map0.x";
            string map = "map.x";

            for (int i = 0; i < 5; i++)
            {
                var floor = new FloorEntity(i+1, Path.Combine(Data.GFXDir, i == 0 ? map0 : map), ResourceManager.CreateMaterial("map0", Shader));
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

        public override void Draw(Renderer renderer, double dt)
        {
            base.Draw(renderer, dt);
        }

        public override void Tick(double dt)
        {
            var oldPlayerPos = player.Position;
            base.Tick(dt);
            TimeScale = Program.IsFocused ? 1f : 0f;
            Music.Position = player.Position;
            Radio.Position = player.Position;
            if ((player.Position.Y - 0.5f) / 2f < -(currentFloor+1))
            {
                currentFloor++;
                if (currentFloor == 1)
                {
                    Radio.SetBuffer(radioClips[0]);
                    Radio.Play();
                }
            }
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