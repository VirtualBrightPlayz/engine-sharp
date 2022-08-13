using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Engine;
using Engine.Assets;
using Engine.Assets.Audio;
using Engine.Assets.Textures;
using ImGuiNET;
using static GameSrc.UIExt;

namespace GameSrc
{
    public partial class MenuEntity
    {
        public AudioClip PreLoadMusic = ResourceManager.LoadAudioClip(Path.Combine(SCPCB.Instance.Data.SFXDir, "Music", "EntranceZone.ogg"));
        public Texture2D BlinkMeterImg = ResourceManager.LoadTexture(Path.Combine(SCPCB.Instance.Data.GFXDir, "BlinkMeter.jpg"));
        private AudioSource _menuMusicSource;
        private Vector2 LoadingScreenRectPos => new Vector2(ScreenSize.X/2 - LoadingScreenRectSize.X/2, ScreenSize.X/2 + 30 - 100);
        private Vector2 LoadingScreenRectSize => new Vector2(304, 20);
        public int LoadPercent;

        public void SetupLoadMenu()
        {
            _menuMusicSource = new AudioSource("MenuMusic");
            _menuMusicSource.SetBuffer(MenuMusic);
            _menuMusicSource.MaxDistance = float.PositiveInfinity;
            _menuMusicSource.RolloffFactor = 0f;
            _menuMusicSource.ReferenceDistance = 0f;
            _menuMusicSource.Looping = true;
        }

        public void EnablePreLoadMenu()
        {
            LoadPercent = 0;
            _menuMusicSource.SetBuffer(PreLoadMusic);
            _menuMusicSource.Play();
            LoadPercent = 100;
            // Task.Factory.StartNew(async () => await LoadCallback(new Progress<int>((i) => LoadPercent = i)));
        }

        public void DisablePreLoadMenu()
        {
        }

        public void EnableGameLoadMenu()
        {
            LoadPercent = 0;
            if (_menuMusicSource.AudioBuffer != MenuMusic)
            {
                _menuMusicSource.SetBuffer(MenuMusic);
                _menuMusicSource.Play();
            }
            Task.Factory.StartNew(async () => await SCPCB.Instance.MapGen.CreateMap(new Progress<int>((i) => LoadPercent = i), default));
        }

        public void DisableGameLoadMenu()
        {
        }

        public async Task LoadCallback(IProgress<int> progress)
        {
            List<string> names = new List<string>();
            progress.Report(0);
            foreach (var item in SCPCB.Instance.Data.RoomsIni.Sections)
            {
                if (item.Keys.ContainsKey("mesh path"))
                {
                    string key = Path.Combine(SCPCB.Instance.Data.GameDir, item.Keys["mesh path"].Replace("\\", "/"));
                    names.Add(key);
                }
            }
            for (int i = 0; i < names.Count; i++)
            {
                ResourceManagerExtensions.LoadRoomModel(names[i]);
                await Task.Delay(75);
                progress.Report((int)(((float)i / names.Count) * 100f));
            }
            progress.Report(100);
        }

        public void DrawLoadMenu()
        {
            Program.GameAudio.GetListenerProperty(Silk.NET.OpenAL.ListenerVector3.Position, out Vector3 pos);
            _menuMusicSource.Position = pos;
            if (Program.GameInputSnapshotHandler.IsMouseLocked)
                Program.GameInputSnapshotHandler.IsMouseLocked = false;
            Vector2 start = LoadingScreenRectPos;
            Rect(start, LoadingScreenRectSize, Color(Vector4.One));
            for (int x = 0; x < LoadPercent / 3.33333333333f; x++)
            {
                Image(BlinkMeterImg, start + new Vector2(x * 10 + 3, 3), new Vector2(8, 14));
            }
            Vector2 startText = new Vector2(ScreenSize.X/2, ScreenSize.X/2 - 100);
            string LoadText = $"LOADING - {LoadPercent}%";
            TextCentered(CourierNew, 14f, startText + new Vector2(1f, 1f), Vector2.Zero, LoadText, Color(Vector4.Zero));
            TextCentered(CourierNew, 14f, startText, Vector2.Zero, LoadText, Color(Vector4.One));
            if (LoadPercent >= 100)
            {
                if (InputHandler.IsMouseDown(Veldrid.MouseButton.Left) || InputHandler.KeyEvents.Any())
                {
                    if (State == MenuState.PreLoad)
                        SetMenuState(MenuState.MainMenu);
                    else
                    {
                        SetMenuState(MenuState.None);
                        SCPCB.Instance.Entities.AddRange(SCPCB.Instance.MapGen.UnspawnedEntities);
                        foreach (var ent in SCPCB.Instance.MapGen.UnspawnedEntities)
                        {
                            if (ent is RMeshEntity rm)
                            {
                                rm.PlayAudio();
                            }
                        }
                        SCPCB.Instance.SpawnPlayer();
                    }
                }
            }
        }
    }
}