using System;
using System.IO;
using System.Text;

namespace GameSrc
{
    public static class B3DStreamExt
    {
        public static string ReadString(this Stream stream)
        {
            int len = ReadInt(stream);
            byte[] bytes = new byte[len];
            stream.Read(bytes, 0, bytes.Length);
            return Encoding.UTF8.GetString(bytes);
        }

        public static int ReadInt(this Stream stream)
        {
            byte[] bytes = new byte[4];
            bytes[0] = (byte)stream.ReadByte();
            bytes[1] = (byte)stream.ReadByte();
            bytes[2] = (byte)stream.ReadByte();
            bytes[3] = (byte)stream.ReadByte();
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            return BitConverter.ToInt32(bytes);
        }

        public static float ReadFloat(this Stream stream)
        {
            byte[] bytes = new byte[4];
            bytes[0] = (byte)stream.ReadByte();
            bytes[1] = (byte)stream.ReadByte();
            bytes[2] = (byte)stream.ReadByte();
            bytes[3] = (byte)stream.ReadByte();
            if (!BitConverter.IsLittleEndian)
                Array.Reverse(bytes);
            return BitConverter.ToSingle(bytes);
        }
    }
}