using System;
using System.IO;
using Engine.Assets.Audio;
using Engine.Assets.Rendering;
using Engine.Game.Entities;
using ImGuiNET;

namespace GameSrc
{
    public class MusicHandler : Entity
    {
        public enum MusicType : int
        {
            None = -1,
            LightContainment = 0,
            HeavyContainment = 1,
            Entrance = 2,
            Intro = 3,
        }

        public AudioClip[] clips = new AudioClip[]
        {
            new AudioClip(Path.Combine(SCPCB.Instance.Data.SFXDir, "Music", "The Dread.ogg")),
            new AudioClip(Path.Combine(SCPCB.Instance.Data.SFXDir, "Music", "HeavyContainment.ogg")),
            new AudioClip(Path.Combine(SCPCB.Instance.Data.SFXDir, "Music", "EntranceZone.ogg")),
            new AudioClip(Path.Combine(SCPCB.Instance.Data.SFXDir, "Music", "Intro.ogg")),
        };
        public AudioSource source;

        public MusicType Music = MusicType.None;
        private MusicType lastMusic = MusicType.None;

        public MusicHandler() : base(nameof(MusicHandler))
        {
            source = new AudioSource(Name);
            // source.Gain = source.MaxGain = 2f;
            source.Looping = true;
            source.Relative = true;
        }

        public override void DrawGui(Renderer renderer, double dt)
        {
            base.DrawGui(renderer, dt);
            if (ImGui.Begin(Name))
            {
                int cur = (int)Music;
                string[] names = Enum.GetNames<MusicType>();
                ImGui.Combo("Music", ref cur, names, names.Length);
                Music = (MusicType)(cur);
            }
            ImGui.End();
        }

        public override void Tick(double dt)
        {
            base.Tick(dt);
            if (lastMusic != Music)
            {
                if ((int)Music < 0 || (int)Music >= clips.Length)
                {
                    source.Stop();
                }
                else
                {
                    source.SetBuffer(clips[(int)Music]);
                    source.Play();
                }
                lastMusic = Music;
            }
        }
    }
}