using System;
using System.IO;
using NAudio.Vorbis;
using NAudio.Wave;
using Silk.NET.OpenAL;

namespace Engine.Assets.Audio
{
    public class AudioClip : Resource
    {
        public override bool IsValid => _handle.HasValue;
        private uint? _handle;
        public uint Handle => _handle.Value;
        private AL _al => Program.GameAudio;
        private byte[] _data;
        private BufferFormat _format;
        private int _sampleRate;

        public AudioClip(string path)
        {
            Name = path;
            if (File.Exists(path))
            {
                var stream = File.OpenRead(path);
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
                _handle = _al.GenBuffer();
                _al.BufferData(_handle.Value, BufferFormat.Mono8, new byte[0], 0);
            }
        }

        public AudioClip(string name, byte[] data, BufferFormat format, int freq)
        {
            Name = name;
            _handle = _al.GenBuffer();
            _al.BufferData(_handle.Value, format, data, freq);
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

        private void LoadFromStream(ISampleProvider file, long length)
        {
            byte[] buffer = new byte[length];
            IWaveProvider provider = file.ToMono().ToWaveProvider16();

            int len = provider.Read(buffer, 0, buffer.Length);
            byte[] buffer2 = new byte[len];
            Array.Copy(buffer, buffer2, len);
            buffer = buffer2;

            BufferFormat format = BufferFormat.Mono8;
            if (provider.WaveFormat.BitsPerSample == 8)
            {
                if (provider.WaveFormat.Channels == 1)
                    format = BufferFormat.Mono8;
                else if (provider.WaveFormat.Channels == 2)
                    format = BufferFormat.Stereo8;
                else
                    throw new NotSupportedException($"{provider.WaveFormat.Channels} not supported. Must be 1 or 2");
            }
            else if (provider.WaveFormat.BitsPerSample == 16)
            {
                if (provider.WaveFormat.Channels == 1)
                    format = BufferFormat.Mono16;
                else if (provider.WaveFormat.Channels == 2)
                    format = BufferFormat.Stereo16;
                else
                    throw new NotSupportedException($"{provider.WaveFormat.Channels} not supported. Must be 1 or 2");
            }
            else
                throw new NotSupportedException($"{provider.WaveFormat.BitsPerSample} not supported. Must be 8 or 16");

            _data = buffer;
            _format = format;
            _sampleRate = provider.WaveFormat.SampleRate;
            _handle = _al.GenBuffer();
            _al.BufferData(_handle.Value, format, buffer, provider.WaveFormat.SampleRate);
        }

        private void LoadFromStream(WaveStream file)
        {
            byte[] buffer = new byte[file.Length];
            IWaveProvider provider = file.ToSampleProvider().ToMono().ToWaveProvider16();

            int len = provider.Read(buffer, 0, buffer.Length);
            byte[] buffer2 = new byte[len];
            Array.Copy(buffer, buffer2, len);
            buffer = buffer2;

            BufferFormat format = BufferFormat.Mono8;
            if (provider.WaveFormat.BitsPerSample == 8)
            {
                if (provider.WaveFormat.Channels == 1)
                    format = BufferFormat.Mono8;
                else if (provider.WaveFormat.Channels == 2)
                    format = BufferFormat.Stereo8;
                else
                    throw new NotSupportedException($"{provider.WaveFormat.Channels} not supported. Must be 1 or 2");
            }
            else if (provider.WaveFormat.BitsPerSample == 16)
            {
                if (provider.WaveFormat.Channels == 1)
                    format = BufferFormat.Mono16;
                else if (provider.WaveFormat.Channels == 2)
                    format = BufferFormat.Stereo16;
                else
                    throw new NotSupportedException($"{provider.WaveFormat.Channels} not supported. Must be 1 or 2");
            }
            else
                throw new NotSupportedException($"{provider.WaveFormat.BitsPerSample} not supported. Must be 8 or 16");
            
            _data = buffer;
            _format = format;
            _sampleRate = provider.WaveFormat.SampleRate;
            _handle = _al.GenBuffer();
            _al.BufferData(_handle.Value, format, buffer, provider.WaveFormat.SampleRate);
        }

        public override void ReCreate()
        {
            if (HasBeenInitialized)
                return;
            base.ReCreate();
            _al.DeleteBuffer(_handle.Value);
            _handle = _al.GenBuffer();
            _al.BufferData(_handle.Value, _format, _data, _sampleRate);
        }

        public override Resource Clone(string cloneName)
        {
            throw new NotImplementedException();
        }

        public override void Dispose()
        {
            _al.DeleteBuffer(_handle.Value);
        }
    }
}
