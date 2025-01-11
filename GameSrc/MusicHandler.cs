using System.IO;
using Engine.Assets.Audio;
using Engine.Game.Entities;

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
            source.Gain = source.MaxGain = 2f;
            source.Looping = true;
            source.Relative = true;
        }

        public override void Tick(double dt)
        {
            base.Tick(dt);
            if (SCPCB.Instance.player != null)
            {
                Music = MusicType.LightContainment;
            }
            if (lastMusic != Music)
            {
                if (Music == MusicType.None)
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