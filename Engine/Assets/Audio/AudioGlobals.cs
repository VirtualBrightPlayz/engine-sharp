using System;
using Silk.NET.OpenAL;

namespace Engine.Assets.Audio
{
    public static unsafe class AudioGlobals
    {
        public static AL GameAudio { get; private set; }
        public static ALContext GameAudioContext { get; private set; }
        private static unsafe Device* GameAudioDevice;
        private static unsafe Context* GameAudioCtx;

        public static void InitGameAudio()
        {
            if (GameAudioContext == null)
            {
                GameAudioContext = ALContext.GetApi(true);
                GameAudio = AL.GetApi(true);
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
    }
}