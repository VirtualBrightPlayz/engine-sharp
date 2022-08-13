using System.IO;
using System.Numerics;
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
        public Texture2D MainMenuBack = ResourceManager.LoadTexture(Path.Combine(SCPCB.Instance.Data.GFXDir, "menu", "back.jpg"));
        public AudioClip MenuMusic = ResourceManager.LoadAudioClip(Path.Combine(SCPCB.Instance.Data.SFXDir, "Music", "Menu.ogg"));

        public void SetupMainMenu()
        {
        }

        public void EnableMainMenu()
        {
            if (_menuMusicSource.AudioBuffer != MenuMusic)
            {
                _menuMusicSource.SetBuffer(MenuMusic);
                _menuMusicSource.Play();
            }
        }

        public void DisableMainMenu()
        {
        }

        public void DrawMainMenu()
        {
            Program.GameAudio.GetListenerProperty(Silk.NET.OpenAL.ListenerVector3.Position, out Vector3 pos);
            _menuMusicSource.Position = pos;
            if (Program.GameInputSnapshotHandler.IsMouseLocked)
                Program.GameInputSnapshotHandler.IsMouseLocked = false;
            Vector2 buttonSize = new Vector2(160f, 70f);
            Image(MainMenuBack, Vector2.Zero, new Vector2(MainMenuBack.Width, MainMenuBack.Height));
            if (Button("PLAY", 24f, new Vector2(200f, 300f), buttonSize))
            {
                SetMenuState(MenuState.GameLoad);
            }
            if (Button("EXIT", 24f, new Vector2(200f, 400f), buttonSize))
            {
                Program.IsClosing = true;
            }
        }
    }
}