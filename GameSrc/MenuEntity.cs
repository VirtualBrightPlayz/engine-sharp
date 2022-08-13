using System;
using System.Numerics;
using Engine;
using Engine.Assets;
using Engine.Assets.Audio;
using Engine.Assets.Textures;
using Engine.Game.Entities;
using Engine.VeldridSilk;
using ImGuiNET;
using Silk.NET.Input;
using static GameSrc.UIExt;

namespace GameSrc
{
    public partial class MenuEntity : Entity
    {
        public enum MenuState : int
        {
            None = -1,
            PreLoad = 0,
            MainMenu = 1,
            GameLoad = 2,
        }

        public IInputContext Input => Program.GameInputContext;
        public InputHandler InputHandler => Program.GameInputSnapshotHandler;
        public Vector2 ScreenSize => new Vector2(Program.GameWindow.Size.X, Program.GameWindow.Size.Y);
        public MenuState State { get; private set; } = MenuState.PreLoad;

        public MenuEntity() : base("Menu")
        {
            SetupLoadMenu();
            SetupMainMenu();
            SetMenuState(MenuState.PreLoad);
        }

        public void SetMenuState(MenuState state)
        {
            switch (State)
            {
                case MenuState.PreLoad:
                    DisablePreLoadMenu();
                    break;
                case MenuState.MainMenu:
                    DisableMainMenu();
                    break;
                case MenuState.GameLoad:
                    DisableGameLoadMenu();
                    break;
            }
            switch (state)
            {
                case MenuState.None:
                    _menuMusicSource.Stop();
                    break;
                case MenuState.PreLoad:
                    EnablePreLoadMenu();
                    break;
                case MenuState.MainMenu:
                    EnableMainMenu();
                    break;
                case MenuState.GameLoad:
                    EnableGameLoadMenu();
                    break;
            }
            State = state;
        }

        public override void Draw(double dt)
        {
            // ImGui.SliderFloat2("Size", ref MenuScale, 0.1f, 2f);
            base.Draw(dt);
            switch (State)
            {
                case MenuState.PreLoad:
                case MenuState.GameLoad:
                    DrawLoadMenu();
                    break;
                case MenuState.MainMenu:
                    DrawMainMenu();
                    break;
            }
        }
    }
}