using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using Engine;
using Engine.Assets;
using Engine.Assets.Audio;
using Engine.Assets.Rendering;
using Engine.Assets.Textures;
using ImGuiNET;
using static GameSrc.UIExt;

namespace GameSrc
{
    public partial class MenuEntity
    {
        public AudioClip PreLoadMusic = new AudioClip(Path.Combine(SCPCB.Instance.Data.SFXDir, "Music", "EntranceZone.ogg"));
        public Texture2D BlinkMeterImg = new Texture2D(Path.Combine(SCPCB.Instance.Data.GFXDir, "BlinkMeter.jpg"));
        public AudioClip MenuLoaded = new AudioClip(Path.Combine(SCPCB.Instance.Data.SFXDir, "Horror", "Horror8.ogg"));
        public GameData.LoadingScreen[] LoadingScreens = SCPCB.Instance.Data.GetLoadingScreens();
        private AudioSource _menuMusicSource;
        private AudioSource _menuDoneLoading;
        private Vector2 LoadingScreenRectPos => new Vector2(ScreenSize.X/2 - LoadingScreenRectSize.X/2, ScreenSize.X/2 + 30 - 100);
        private Vector2 LoadingScreenRectSize => new Vector2(304, 20);
        public int LoadPercent;
        private int lastLoadPercent;
        private int currentLoadingScreen;
        private Texture2D backgroundImage;
        private Texture2D loadingScreenImage;

        public void SetupLoadMenu()
        {
            _menuMusicSource = new AudioSource("MenuMusic");
            _menuMusicSource.SetBuffer(MenuMusic);
            _menuMusicSource.Looping = true;
            _menuMusicSource.Relative = true;

            _menuDoneLoading = new AudioSource("MenuLoaded");
            _menuDoneLoading.SetBuffer(MenuLoaded);
            _menuDoneLoading.Looping = false;
            _menuDoneLoading.Relative = true;

            backgroundImage = new Texture2D(Path.Combine(SCPCB.Instance.Data.LoadingScreensDir, "loadingback.jpg"));
        }

        public void ChangeLoadingScreen()
        {
            currentLoadingScreen = Random.Shared.Next(LoadingScreens.Length);
            loadingScreenImage = new Texture2D(Path.Combine(SCPCB.Instance.Data.LoadingScreensDir, LoadingScreens[currentLoadingScreen].ImagePath));
        }

        public void EnablePreLoadMenu()
        {
            ChangeLoadingScreen();
            LoadPercent = 0;
            _menuMusicSource.SetBuffer(PreLoadMusic);
            _menuMusicSource.Play();
            // LoadPercent = 100;
            Game.AsyncTools.Run(LoadCallback(new Progress<int>((i) => LoadPercent = i)));
            // Task.Run(async () => await LoadCallback(new Progress<int>((i) => LoadPercent = i)));
        }

        public void DisablePreLoadMenu()
        {
        }

        public void EnableGameLoadMenu()
        {
            ChangeLoadingScreen();
            LoadPercent = 0;
            if (_menuMusicSource.AudioBuffer != MenuMusic)
                _menuMusicSource.SetBuffer(MenuMusic);
            if (!_menuMusicSource.IsPlaying)
                _menuMusicSource.Play();
            Game.AsyncTools.Run(SCPCB.Instance.MapGen.CreateMap(new Progress<int>((i) => LoadPercent = i), default));
            // Task.Run(async () => await SCPCB.Instance.MapGen.CreateMap(new Progress<int>((i) => LoadPercent = i), default));
        }

        public void DisableGameLoadMenu()
        {
        }

        public IEnumerator LoadCallback(IProgress<int> progress)
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
                if (!SCPCB.RMeshModels.ContainsKey(names[i]))
                    SCPCB.RMeshModels.TryAdd(names[i], new RMeshModel(names[i]));
                // await Task.Delay(75);
                progress.Report((int)(((float)i / names.Count) * 100f));
                yield return null;
            }
            progress.Report(100);
        }

        public void DrawLoadMenu()
        {
            if (!LoadingScreens[currentLoadingScreen].DisableBackground)
            {
                Vector2 imgPos = new Vector2(ScreenSize.X/2 - backgroundImage.Width/2, ScreenSize.Y/2 - backgroundImage.Height/2);
                Vector2 imgSize = new Vector2(backgroundImage.Width, backgroundImage.Height);
                Image(backgroundImage, imgPos, imgSize);
            }
            {
                float x = ScreenSize.X/2 - loadingScreenImage.Width/2;
                switch (LoadingScreens[currentLoadingScreen].AlignX)
                {
                    case 1:
                        x = ScreenSize.X - loadingScreenImage.Width;
                        break;
                    case 2:
                        x = 0f;
                        break;
                }
                float y = ScreenSize.Y/2 - loadingScreenImage.Height/2;
                switch (LoadingScreens[currentLoadingScreen].AlignY)
                {
                    case 1:
                        y = ScreenSize.Y - loadingScreenImage.Height;
                        break;
                    case 2:
                        y = 0f;
                        break;
                }
                Vector2 imgSize = new Vector2(loadingScreenImage.Width, loadingScreenImage.Height);
                Image(loadingScreenImage, new Vector2(x, y), imgSize);

                TextCentered(CourierNew24, new Vector2(ScreenSize.X / 2, ScreenSize.Y / 2 + 80) + Vector2.One, Vector2.Zero, LoadingScreens[currentLoadingScreen].Name, Color(Vector4.Zero));
                TextCentered(CourierNew24, new Vector2(ScreenSize.X / 2, ScreenSize.Y / 2 + 80), Vector2.Zero, LoadingScreens[currentLoadingScreen].Name, Color(Vector4.One));

                /*
                string loadingText = LoadingScreens[currentLoadingScreen].Text.FirstOrDefault() ?? string.Empty;
                TextCentered(CourierNew14, new Vector2(ScreenSize.X / 2 - 200, ScreenSize.Y / 2 + 120) + Vector2.One, new Vector2(400, 300), loadingText, Color(Vector4.Zero));
                TextCentered(CourierNew14, new Vector2(ScreenSize.X / 2 - 200, ScreenSize.Y / 2 + 120), new Vector2(400, 300), loadingText, Color(Vector4.One));
                */
            }
            if (InputHandler.IsMouseLocked)
                InputHandler.IsMouseLocked = false;
            Vector2 start = LoadingScreenRectPos;
            Rect(start, LoadingScreenRectSize, Color(Vector4.One));
            for (int x = 0; x < LoadPercent / 3.33333333333f; x++)
            {
                Image(BlinkMeterImg, start + new Vector2(x * 10 + 3, 3), new Vector2(8, 14));
            }
            Vector2 startText = new Vector2(ScreenSize.X/2, ScreenSize.X/2 - 100);
            string LoadText = $"LOADING - {LoadPercent}%";
            TextCentered(CourierNew14, startText + Vector2.One, Vector2.Zero, LoadText, Color(Vector4.Zero));
            TextCentered(CourierNew14, startText, Vector2.Zero, LoadText, Color(Vector4.One));
            if (lastLoadPercent != LoadPercent && LoadPercent >= 100)
            {
                _menuDoneLoading.Play();
            }
            lastLoadPercent = LoadPercent;
            if (LoadPercent >= 100)
            {
                TextCentered(CourierNew14, new Vector2(ScreenSize.X / 2, ScreenSize.Y - 50), Vector2.Zero, "PRESS ANY KEY TO CONTINUE", Color(Vector4.One));
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