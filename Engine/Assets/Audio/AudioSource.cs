using System;
using System.Numerics;
using System.Threading.Tasks;
using OpenAL;

namespace Engine.Assets.Audio
{
    public class AudioSource : Resource
    {
        public override bool IsValid => _handle.HasValue;
        private uint? _handle;
        public AudioClip AudioBuffer { get; private set; }
        public bool IsPlaying
        {
            get
            {
                if (!IsValid)
                    return false;
                AL10.alGetSourcei(_handle.Value, AL10.AL_SOURCE_STATE, out int val);
                AudioGlobals.CheckALError("Is playing");
                return val == AL10.AL_PLAYING;
            }
        }
        public float MaxDistance
        {
            #if WEBGL || !LEGACY_START
            get { AL10.alGetSourcef(_handle.Value, AL10.AL_MAX_DISTANCE, out var value); AudioGlobals.CheckALError("get maxdist"); return value; }
            set { AL10.alSourcef(_handle.Value, AL10.AL_MAX_DISTANCE, value); AudioGlobals.CheckALError("set maxdist"); }
            #else
            get { _al.GetSourceProperty(_handle.Value, SourceFloat.MaxDistance, out var value); return value; }
            set => _al.SetSourceProperty(_handle.Value, SourceFloat.MaxDistance, value);
            #endif
        }
        public float ReferenceDistance
        {
            #if WEBGL || !LEGACY_START
            get { AL10.alGetSourcef(_handle.Value, AL10.AL_REFERENCE_DISTANCE, out var value); AudioGlobals.CheckALError(); return value; }
            set { AL10.alSourcef(_handle.Value, AL10.AL_REFERENCE_DISTANCE, value); AudioGlobals.CheckALError(); }
            #else
            get { _al.GetSourceProperty(_handle.Value, SourceFloat.ReferenceDistance, out var value); return value; }
            set => _al.SetSourceProperty(_handle.Value, SourceFloat.ReferenceDistance, value);
            #endif
        }
        public Vector3 Position
        {
            #if WEBGL || !LEGACY_START
            get { AL10.alGetSource3f(_handle.Value, AL10.AL_POSITION, out var x, out var y, out var z); AudioGlobals.CheckALError(); return new Vector3(x, y, z); }
            set { AL10.alSource3f(_handle.Value, AL10.AL_POSITION, value.X, value.Y, value.Z); AudioGlobals.CheckALError(); }
            #else
            get { _al.GetSourceProperty(_handle.Value, SourceVector3.Position, out var value); return value; }
            set => _al.SetSourceProperty(_handle.Value, SourceVector3.Position, value);
            #endif
        }
        public bool Looping
        {
            #if WEBGL || !LEGACY_START
            get { AL10.alGetSourcei(_handle.Value, AL10.AL_LOOPING, out var value); AudioGlobals.CheckALError(); return value == 1 ? true : false; }
            set { AL10.alSourcei(_handle.Value, AL10.AL_LOOPING, value ? 1 : 0); AudioGlobals.CheckALError(); }
            #else
            get { _al.GetSourceProperty(_handle.Value, SourceBoolean.Looping, out var value); return value; }
            set => _al.SetSourceProperty(_handle.Value, SourceBoolean.Looping, value);
            #endif
        }
        public float Gain
        {
            #if WEBGL || !LEGACY_START
            get { AL10.alGetSourcef(_handle.Value, AL10.AL_GAIN, out var value); AudioGlobals.CheckALError(); return value; }
            set { AL10.alSourcef(_handle.Value, AL10.AL_GAIN, value); AudioGlobals.CheckALError(); }
            #else
            get { _al.GetSourceProperty(_handle.Value, SourceFloat.Gain, out var value); return value; }
            set => _al.SetSourceProperty(_handle.Value, SourceFloat.Gain, value);
            #endif
        }
        public float MinGain
        {
            #if WEBGL || !LEGACY_START
            get { AL10.alGetSourcef(_handle.Value, AL10.AL_MIN_GAIN, out var value); AudioGlobals.CheckALError("get min gain"); return value; }
            set { AL10.alSourcef(_handle.Value, AL10.AL_MIN_GAIN, value); AudioGlobals.CheckALError("set min gain"); }
            #else
            get { _al.GetSourceProperty(_handle.Value, SourceFloat.MinGain, out var value); return value; }
            set => _al.SetSourceProperty(_handle.Value, SourceFloat.MinGain, value);
            #endif
        }
        public float MaxGain
        {
            #if WEBGL || !LEGACY_START
            get { AL10.alGetSourcef(_handle.Value, AL10.AL_MAX_GAIN, out var value); AudioGlobals.CheckALError("get max gain"); return value; }
            set { AL10.alSourcef(_handle.Value, AL10.AL_MAX_GAIN, value); AudioGlobals.CheckALError("set max gain"); }
            #else
            get { _al.GetSourceProperty(_handle.Value, SourceFloat.MaxGain, out var value); return value; }
            set => _al.SetSourceProperty(_handle.Value, SourceFloat.MaxGain, value);
            #endif
        }
        public float RolloffFactor
        {
            #if WEBGL || !LEGACY_START
            get { AL10.alGetSourcef(_handle.Value, AL10.AL_ROLLOFF_FACTOR, out var value); AudioGlobals.CheckALError("get rolloff"); return value; }
            set { AL10.alSourcef(_handle.Value, AL10.AL_ROLLOFF_FACTOR, value); AudioGlobals.CheckALError("set rolloff"); }
            #else
            get { _al.GetSourceProperty(_handle.Value, SourceFloat.RolloffFactor, out var value); return value; }
            set => _al.SetSourceProperty(_handle.Value, SourceFloat.RolloffFactor, value);
            #endif
        }
        public bool Relative
        {
            get { AL10.alGetSourcei(_handle.Value, AL10.AL_SOURCE_RELATIVE, out var value); AudioGlobals.CheckALError(); return value == 1 ? true : false; }
            set { AL10.alSourcei(_handle.Value, AL10.AL_SOURCE_RELATIVE, value ? 1 : 0); AudioGlobals.CheckALError(); }
        }

        public AudioSource(string name) : base(name)
        {
            AL10.alGenSources(1, out uint v);
            AudioGlobals.CheckALError("gen sources");
            _handle = v;
            SetDefaultParameters();
            // ReCreate();
        }

        protected override void ReCreateInternal()
        {
            if (_handle.HasValue)
            {
                uint v2 = _handle.Value;
                AL10.alDeleteSources(1, ref v2);
                AudioGlobals.CheckALError();
                _handle = null;
            }
            AL10.alGenSources(1, out uint v);
            AudioGlobals.CheckALError();
            _handle = v;

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
            Relative = Relative;
            if (IsPlaying)
                Play();
        }

        protected override Resource CloneInternal(string cloneName)
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
            src.Relative = Relative;
            if (src.IsPlaying)
                src.Play();
            return src;
        }

        public void SetBuffer(AudioClip buffer)
        {
            AudioBuffer = buffer;
            Stop();
            AL10.alSourcei(_handle.Value, AL10.AL_BUFFER, (int)buffer.Handle);
            AudioGlobals.CheckALError();
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
            Relative = false;
        }

        public void Stop()
        {
        #if WEBGL || !LEGACY_START
            AL10.alSourceStop(_handle.Value);
            AudioGlobals.CheckALError();
        #else
            _al.SourceStop(_handle.Value);
        #endif
        }

        public void Play()
        {
        #if WEBGL || !LEGACY_START
            AL10.alSourcePlay(_handle.Value);
            AudioGlobals.CheckALError();
        #else
            _al.SourcePlay(_handle.Value);
        #endif
        }

        protected override void DisposeInternal()
        {
            uint v = _handle.Value;
            AL10.alDeleteSources(1, ref v);
            AudioGlobals.CheckALError();
            _handle = null;
        }
    }
}
