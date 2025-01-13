using System;
using System.Numerics;
#if WEBGL || !LEGACY_START
using OpenAL;
#else
using Silk.NET.OpenAL;
#endif

namespace Engine.Assets.Audio
{
    public static unsafe class AudioGlobals
    {
    #if WEBGL || !LEGACY_START
        public static Vector3 Position
        {
            get { AL10.alGetListener3f(AL10.AL_POSITION, out var x, out var y, out var z); CheckALError(); return new Vector3(x, y, z); }
            set { AL10.alListener3f(AL10.AL_POSITION, value.X, value.Y, value.Z); CheckALError(); }
        }
        public static float[] OrientationRaw
        {
            get { float[] vals = new float[6]; AL10.alGetListenerfv(AL10.AL_ORIENTATION, vals); CheckALError(); return vals; }
            set { AL10.alListenerfv(AL10.AL_ORIENTATION, value); CheckALError(); }
        }
        /*public static Vector3[] Orientation
        {
            get { float[] vals = new float[6]; AL10.alGetListenerfv(AL10.AL_ORIENTATION, vals); return new Vector3[] { new Vector3(vals[0], vals[1], vals[2]), new Vector3(vals[0], vals[1], vals[2]) }; }
            set => AL10.alListenerfv(AL10.AL_ORIENTATION, value[0].X, value.Y, value.Z);
        }*/
        private static IntPtr GameAudioDevice;
        private static IntPtr GameAudioCtx;

        public static void CheckALError(string id = "", bool errThrow = false)
        {
            int err = AL10.alGetError();
            if (err != AL10.AL_NO_ERROR)
            {
                if (errThrow)
                    Log.Fatal(nameof(AudioGlobals), $"AL AudioError: 0x{err.ToString("X4")} at {id}");
                else
                    Log.Error(nameof(AudioGlobals), $"AL AudioError: 0x{err.ToString("X4")} at {id}");
            }
        }

        public static void InitGameAudio(bool soft = true)
        {
            GameAudioDevice = ALC10.alcOpenDevice(null);
            GameAudioCtx = ALC10.alcCreateContext(GameAudioDevice, null);
            if (!ALC10.alcMakeContextCurrent(GameAudioCtx))
            {
                Log.Fatal(nameof(AudioGlobals), "ALC AudioError");
            }
            CheckALError("Init", true);
            AL10.alDistanceModel(AL11.AL_LINEAR_DISTANCE_CLAMPED);
        }

        public static void DisposeGameAudio()
        {
            ALC10.alcDestroyContext(GameAudioCtx);
            CheckALError("Dispose", false);
        }
    #else
        public static AL GameAudio { get; private set; }
        public static ALContext GameAudioContext { get; private set; }
        private static unsafe Device* GameAudioDevice;
        private static unsafe Context* GameAudioCtx;

        public static void InitGameAudio(bool soft = true)
        {
            if (GameAudioContext == null)
            {
                GameAudioContext = ALContext.GetApi(soft);
                GameAudio = AL.GetApi(soft);
                GameAudioDevice = GameAudioContext.OpenDevice("");
                if (GameAudioDevice == null)
                    throw new Exception("GameAudioDevice was null");
                GameAudioCtx = GameAudioContext.CreateContext(GameAudioDevice, null);
                GameAudioContext.MakeContextCurrent(GameAudioCtx);
                var err = GameAudio.GetError();
                if (err != AudioError.NoError)
                    throw new Exception($"AudioError: {err.ToString()}");
                GameAudio.DistanceModel(Silk.NET.OpenAL.DistanceModel.LinearDistanceClamped);
            }
        }

        public static void DisposeGameAudio()
        {
            if (GameAudioContext != null)
            {
                GameAudioContext.DestroyContext(GameAudioCtx);
                GameAudioContext.CloseDevice(GameAudioDevice);
                GameAudio.Dispose();
                GameAudioContext.Dispose();
            }
        }
    #endif
    }
}