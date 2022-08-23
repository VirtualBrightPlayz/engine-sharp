using System;
using System.Numerics;
using System.Threading.Tasks;
using Silk.NET.OpenAL;

namespace Engine.Assets.Audio
{
    public class AudioSource : Resource
    {
        public override bool IsValid => _handle.HasValue;
        private uint? _handle;
        private AL _al => AudioGlobals.GameAudio;
        public AudioClip AudioBuffer { get; private set; }
        public bool IsPlaying
        {
            get
            {
                if (!IsValid)
                    return false;
                _al.GetSourceProperty(_handle.Value, GetSourceInteger.SourceState, out int val);
                return (SourceState)val == SourceState.Playing;
            }
        }
        public float MaxDistance
        {
            get { _al.GetSourceProperty(_handle.Value, SourceFloat.MaxDistance, out var value); return value; }
            set => _al.SetSourceProperty(_handle.Value, SourceFloat.MaxDistance, value);
        }
        public float ReferenceDistance
        {
            get { _al.GetSourceProperty(_handle.Value, SourceFloat.ReferenceDistance, out var value); return value; }
            set => _al.SetSourceProperty(_handle.Value, SourceFloat.ReferenceDistance, value);
        }
        public Vector3 Position
        {
            get { _al.GetSourceProperty(_handle.Value, SourceVector3.Position, out var value); return value; }
            set => _al.SetSourceProperty(_handle.Value, SourceVector3.Position, value);
        }
        public bool Looping
        {
            get { _al.GetSourceProperty(_handle.Value, SourceBoolean.Looping, out var value); return value; }
            set => _al.SetSourceProperty(_handle.Value, SourceBoolean.Looping, value);
        }
        public float Gain
        {
            get { _al.GetSourceProperty(_handle.Value, SourceFloat.Gain, out var value); return value; }
            set => _al.SetSourceProperty(_handle.Value, SourceFloat.Gain, value);
        }
        public float MinGain
        {
            get { _al.GetSourceProperty(_handle.Value, SourceFloat.MinGain, out var value); return value; }
            set => _al.SetSourceProperty(_handle.Value, SourceFloat.MinGain, value);
        }
        public float MaxGain
        {
            get { _al.GetSourceProperty(_handle.Value, SourceFloat.MaxGain, out var value); return value; }
            set => _al.SetSourceProperty(_handle.Value, SourceFloat.MaxGain, value);
        }
        public float RolloffFactor
        {
            get { _al.GetSourceProperty(_handle.Value, SourceFloat.RolloffFactor, out var value); return value; }
            set => _al.SetSourceProperty(_handle.Value, SourceFloat.RolloffFactor, value);
        }

        public AudioSource()
        {
            _handle = _al.GenSource();
            SetDefaultParameters();
        }

        public AudioSource(string name) : this()
        {
            Name = name;
        }

        public override Task ReCreate()
        {
            if (HasBeenInitialized)
                return Task.CompletedTask;
            base.ReCreate();
            _al.DeleteSource(_handle.Value);
            _handle = _al.GenSource();
            AudioBuffer.ReCreate();
            SetBuffer(AudioBuffer);
            SetDefaultParameters();
            RolloffFactor = RolloffFactor;
            Gain = Gain;
            MinGain = MinGain;
            MaxGain = MaxGain;
            MaxDistance = MaxDistance;
            ReferenceDistance = ReferenceDistance;
            Looping = Looping;
            Position = Position;
            if (IsPlaying)
                Play();
            return Task.CompletedTask;
        }

        public override Task<Resource> Clone(string cloneName)
        {
            AudioSource src = new AudioSource(cloneName);
            src.SetBuffer(AudioBuffer);
            src.RolloffFactor = RolloffFactor;
            src.Gain = Gain;
            src.MinGain = MinGain;
            src.MaxGain = MaxGain;
            src.MaxDistance = MaxDistance;
            src.ReferenceDistance = ReferenceDistance;
            src.Looping = Looping;
            src.Position = Position;
            if (src.IsPlaying)
                src.Play();
            return Task.FromResult<Resource>(src);
        }

        public void SetBuffer(AudioClip buffer)
        {
            AudioBuffer = buffer;
            Stop();
            _al.SetSourceProperty(_handle.Value, SourceInteger.Buffer, buffer.Handle);
        }

        public void SetDefaultParameters()
        {
            RolloffFactor = 1f;
            Gain = 1f;
            MinGain = 0f;
            MaxGain = 1f;
            MaxDistance = 1f;
            ReferenceDistance = 0.1f;
            Looping = false;
        }

        public void Stop()
        {
            _al.SourceStop(_handle.Value);
        }

        public void Play()
        {
            _al.SourcePlay(_handle.Value);
        }

        public override void Dispose()
        {
            _al.DeleteSource(_handle.Value);
        }
    }
}
