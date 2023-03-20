using System;
using System.IO;
using System.Threading.Tasks;
using NAudio.Vorbis;
using NAudio.Wave;
#if WEBGL || !LEGACY_START
using OpenAL;
#else
using Silk.NET.OpenAL;
#endif

namespace Engine.Assets.Audio
{
    public class AudioClip : Resource
    {
        public override bool IsValid => _handle.HasValue;
        private uint? _handle;
        public uint Handle => _handle.Value;
        private byte[] _data;
    #if WEBGL || !LEGACY_START
        private int _format;
    #else
        private AL _al => AudioGlobals.GameAudio;
        private BufferFormat _format;
    #endif
        private int _sampleRate;

        public AudioClip(string path) : base(path)
        {
            ReCreate();
        }

        public void Create(string path)
        {
            if (_handle.HasValue)
            {
                uint v2 = _handle.Value;
                AL10.alDeleteBuffers(1, ref v2);
                AudioGlobals.CheckALError("delete buffers");
            }
            if (FileManager.Exists(path))
            {
                var stream = FileManager.LoadStream(path);
                switch (Path.GetExtension(path).ToLower())
                {
                    case ".ogg":
                    {
                        using var file = new VorbisWaveReader(stream, true);
                        LoadFromStream(file, file.Length);
                    }
                    break;
                    case ".mp3":
                    {
                        using var file = new Mp3FileReader(stream);
                        LoadFromStream(file);
                    }
                    break;
                    case ".wav":
                    {
                        using var file = new WaveFileReader(stream);
                        LoadFromStream(file);
                    }
                    break;
                    default:
                        throw new NotSupportedException($"{Path.GetExtension(path).ToLower()} is not supported.");
                }
            }
            else
            {
                AL10.alGenBuffers(1, out uint v);
                AudioGlobals.CheckALError("gen buffers (empty)");
                _handle = v;
                throw new Exception($"Unable to find/load {path}");
            }
        }

        public static byte[] MixStereo32ToMono32(byte[] input)
        {
            byte[] output = new byte[input.Length / 2];
            int outputIndex = 0;
            byte[] temp = new byte[4];
            for (int i = 0; i < input.Length; i+=8)
            {
                Array.Copy(input, i, temp, 0, 4);
                long leftChannel = BitConverter.ToInt32(temp, 0);
                Array.Copy(input, i+4, temp, 0, 4);
                long rightChannel = BitConverter.ToInt32(temp, 0);
                long mixed = (leftChannel + rightChannel) / 2;
                byte[] outSample = BitConverter.GetBytes((int)mixed);

                output[outputIndex++] = outSample[0];
                output[outputIndex++] = outSample[1];
                output[outputIndex++] = outSample[2];
                output[outputIndex++] = outSample[3];
            }
            return output;
        }

        public static byte[] MixStereo16ToMono16(byte[] input)
        {
            byte[] output = new byte[input.Length / 2];
            int outputIndex = 0;
            for (int i = 0; i < input.Length; i+=4)
            {
                int leftChannel = BitConverter.ToInt16(input, i);
                int rightChannel = BitConverter.ToInt16(input, i+2);
                int mixed = (leftChannel + rightChannel) / 2;
                byte[] outSample = BitConverter.GetBytes((short)mixed);

                output[outputIndex++] = outSample[0];
                output[outputIndex++] = outSample[1];
            }
            return output;
        }

        public static float[] Convert32BitToFloat(byte[] input)
        {
            int inputSamples = input.Length / 4;
            float[] output = new float[inputSamples];
            int outputIndex = 0;
            for (int i = 0; i < inputSamples; i++)
            {
                int sample = BitConverter.ToInt32(input, i*4);
                output[outputIndex++] = sample / (float)UInt32.MaxValue;
            }
            return output;
        }

        public static float[] Convert16BitToFloat(byte[] input)
        {
            int inputSamples = input.Length / 2;
            float[] output = new float[inputSamples];
            int outputIndex = 0;
            for (int i = 0; i < inputSamples; i++)
            {
                short sample = BitConverter.ToInt16(input, i*2);
                output[outputIndex++] = sample / (float)Int16.MaxValue;
            }
            return output;
        }

        public static int GetFormat(int BitsPerSample, int Channels)
        {
            int format = AL10.AL_FORMAT_MONO8;
            if (BitsPerSample == 8)
            {
                if (Channels == 1)
                    format = AL10.AL_FORMAT_MONO8;
                else if (Channels == 2)
                    format = AL10.AL_FORMAT_STEREO8;
                else
                    throw new NotSupportedException($"{Channels} not supported. Must be 1 or 2");
            }
            else if (BitsPerSample == 16)
            {
                if (Channels == 1)
                    format = AL10.AL_FORMAT_MONO16;
                else if (Channels == 2)
                    format = AL10.AL_FORMAT_STEREO16;
                else
                    throw new NotSupportedException($"{Channels} not supported. Must be 1 or 2");
            }
            else
                throw new NotSupportedException($"{BitsPerSample} not supported. Must be 8 or 16");
            return format;
        }


        private void LoadFromStream(ISampleProvider file, long length)
        {
            byte[] buffer = new byte[length];
            IWaveProvider provider = file.ToMono().ToWaveProvider16();

            int len = provider.Read(buffer, 0, buffer.Length);
            byte[] buffer2 = new byte[len];
            Array.Copy(buffer, buffer2, len);
            buffer = buffer2;

            _data = buffer;

            _format = GetFormat(provider.WaveFormat.BitsPerSample, provider.WaveFormat.Channels);
            AL10.alGenBuffers(1, out uint v);
            AudioGlobals.CheckALError("genBuffer ogg");
            _handle = v;
            AL10.alBufferData(_handle.Value, _format, buffer, buffer.Length, provider.WaveFormat.SampleRate);
            AudioGlobals.CheckALError("buffer data ogg");
        }

        private void LoadFromStream(WaveStream file)
        {
            byte[] buffer = new byte[file.Length];
            IWaveProvider provider = file.ToSampleProvider().ToMono().ToWaveProvider16();

            int len = provider.Read(buffer, 0, buffer.Length);
            byte[] buffer2 = new byte[len];
            Array.Copy(buffer, buffer2, len);
            buffer = buffer2;
            
            _data = buffer;

            var format = _format = GetFormat(provider.WaveFormat.BitsPerSample, provider.WaveFormat.Channels);
            AL10.alGenBuffers(1, out uint v);
            AudioGlobals.CheckALError("genBuffer misc");
            _handle = v;
            AL10.alBufferData(_handle.Value, format, buffer, buffer.Length, provider.WaveFormat.SampleRate);
            AudioGlobals.CheckALError("buffer data misc");
        }

        protected override void ReCreateInternal()
        {
            if (!_handle.HasValue)
                Create(Name);
        }

        protected override Resource CloneInternal(string cloneName)
        {
            throw new NotImplementedException();
        }

        protected override void DisposeInternal()
        {
            // return;
            if (_handle.HasValue)
            {
                uint v = _handle.Value;
                AL10.alDeleteBuffers(1, ref v);
                AudioGlobals.CheckALError();
                _handle = null;
            }
        }
    }
}
