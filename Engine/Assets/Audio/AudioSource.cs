using System;
using System.Numerics;
using System.Threading.Tasks;
#if WEBGL
using OpenAL;
#else
using Silk.NET.OpenAL;
#endif

namespace Engine.Assets.Audio
{
    public class AudioSource : Resource
    {
        public override bool IsValid => _handle.HasValue;
        private uint? _handle;
    #if !WEBGL
        private AL _al => AudioGlobals.GameAudio;
    #endif
        public AudioClip AudioBuffer { get; private set; }
        public bool IsPlaying
        {
            get
            {
                if (!IsValid)
                    return false;
            #if WEBGL
                AL10.alGetSourcei(_handle.Value, AL10.AL_SOURCE_STATE, out int val);
                return val == AL10.AL_PLAYING;
            #else
                _al.GetSourceProperty(_handle.Value, GetSourceInteger.SourceState, out int val);
                return (SourceState)val == SourceState.Playing;
            #endif
            }
        }
        public float MaxDistance
        {
            #if WEBGL
            get { AL10.alGetSourcef(_handle.Value, AL10.AL_MAX_DISTANCE, out var value); return value; }
            set => AL10.alSourcef(_handle.Value, AL10.AL_MAX_DISTANCE, value);
            #else
            get { _al.GetSourceProperty(_handle.Value, SourceFloat.MaxDistance, out var value); return value; }
            set => _al.SetSourceProperty(_handle.Value, SourceFloat.MaxDistance, value);
            #endif
        }
        public float ReferenceDistance
        {
            #if WEBGL
            get { AL10.alGetSourcef(_handle.Value, AL10.AL_REFERENCE_DISTANCE, out var value); return value; }
            set => AL10.alSourcef(_handle.Value, AL10.AL_REFERENCE_DISTANCE, value);
            #else
            get { _al.GetSourceProperty(_handle.Value, SourceFloat.ReferenceDistance, out var value); return value; }
            set => _al.SetSourceProperty(_handle.Value, SourceFloat.ReferenceDistance, value);
            #endif
        }
        public Vector3 Position
        {
            #if WEBGL
            get { AL10.alGetSource3f(_handle.Value, AL10.AL_POSITION, out var x, out var y, out var z); return new Vector3(x, y, z); }
            set => AL10.alSource3f(_handle.Value, AL10.AL_POSITION, value.X, value.Y, value.Z);
            #else
            get { _al.GetSourceProperty(_handle.Value, SourceVector3.Position, out var value); return value; }
            set => _al.SetSourceProperty(_handle.Value, SourceVector3.Position, value);
            #endif
        }
        public bool Looping
        {
            #if WEBGL
            get { AL10.alGetSourcei(_handle.Value, AL10.AL_LOOPING, out var value); return value == 1 ? true : false; }
            set => AL10.alSourcei(_handle.Value, AL10.AL_LOOPING, value ? 1 : 0);
            #else
            get { _al.GetSourceProperty(_handle.Value, SourceBoolean.Looping, out var value); return value; }
            set => _al.SetSourceProperty(_handle.Value, SourceBoolean.Looping, value);
            #endif
        }
        public float Gain
        {
            #if WEBGL
            get { AL10.alGetSourcef(_handle.Value, AL10.AL_GAIN, out var value); return value; }
            set => AL10.alSourcef(_handle.Value, AL10.AL_GAIN, value);
            #else
            get { _al.GetSourceProperty(_handle.Value, SourceFloat.Gain, out var value); return value; }
            set => _al.SetSourceProperty(_handle.Value, SourceFloat.Gain, value);
            #endif
        }
        public float MinGain
        {
            #if WEBGL
            get { AL10.alGetSourcef(_handle.Value, AL10.AL_MIN_GAIN, out var value); return value; }
            set => AL10.alSourcef(_handle.Value, AL10.AL_MIN_GAIN, value);
            #else
            get { _al.GetSourceProperty(_handle.Value, SourceFloat.MinGain, out var value); return value; }
            set => _al.SetSourceProperty(_handle.Value, SourceFloat.MinGain, value);
            #endif
        }
        public float MaxGain
        {
            #if WEBGL
            get { AL10.alGetSourcef(_handle.Value, AL10.AL_MAX_GAIN, out var value); return value; }
            set => AL10.alSourcef(_handle.Value, AL10.AL_MAX_GAIN, value);
            #else
            get { _al.GetSourceProperty(_handle.Value, SourceFloat.MaxGain, out var value); return value; }
            set => _al.SetSourceProperty(_handle.Value, SourceFloat.MaxGain, value);
            #endif
        }
        public float RolloffFactor
        {
            #if WEBGL
            get { AL10.alGetSourcef(_handle.Value, AL10.AL_ROLLOFF_FACTOR, out var value); return value; }
            set => AL10.alSourcef(_handle.Value, AL10.AL_ROLLOFF_FACTOR, value);
            #else
            get { _al.GetSourceProperty(_handle.Value, SourceFloat.RolloffFactor, out var value); return value; }
            set => _al.SetSourceProperty(_handle.Value, SourceFloat.RolloffFactor, value);
            #endif
        }

        public AudioSource()
        {
        #if WEBGL
            AL10.alGenSources(1, out uint v);
            _handle = v;
        #else
            _handle = _al.GenSource();
        #endif
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
        #if WEBGL
            uint v2 = _handle.Value;
            AL10.alDeleteSources(1, ref v2);
            AL10.alGenSources(1, out uint v);
            _handle = v;
        #else
            _al.DeleteSource(_handle.Value);
            _handle = _al.GenSource();
        #endif
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
        #if WEBGL
            AL10.alSourcei(_handle.Value, AL10.AL_BUFFER, (int)buffer.Handle);
        #else
            _al.SetSourceProperty(_handle.Value, SourceInteger.Buffer, buffer.Handle);
        #endif
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
        #if WEBGL
            AL10.alSourceStop(_handle.Value);
        #else
            _al.SourceStop(_handle.Value);
        #endif
        }

        public void Play()
        {
        #if WEBGL
            AL10.alSourcePlay(_handle.Value);
        #else
            _al.SourcePlay(_handle.Value);
        #endif
        }

        public override void Dispose()
        {
        #if WEBGL
            uint v = _handle.Value;
            AL10.alDeleteSources(1, ref v);
        #else
            _al.DeleteSource(_handle.Value);
        #endif
        }
    }
}
