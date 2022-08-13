using System.Numerics;
using System.Runtime.InteropServices;

namespace Engine.Assets.Rendering
{
    public static class ForwardConsts
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct LightInfo
        {
            public Vector4 AmbientLight;
            public Vector4 LightPosition;
            public Vector4 LightColor;
            public const uint Size = (4 + 4 + 4) * sizeof(float);
        }

        public const int MaxLightsPerPass = 1;
        public const string LightBufferName = "LightInfo0";
        public static Vector4 AmbientColor = Vector4.One;
        public static Vector3 LightPosition = Vector3.Zero;
        public static Vector4 LightColor = Vector4.One;

        public static float[] GetLightInfo()
        {
            LightInfo info = new LightInfo()
            {
                AmbientLight = AmbientColor,
                LightPosition = new Vector4(LightPosition, 0f),
                LightColor = LightColor,
            };
            float[] blit = new float[LightInfo.Size / sizeof(float)];
            int offset = 0;
            info.AmbientLight.CopyTo(blit, offset);
            offset += 4;
            info.LightPosition.CopyTo(blit, offset);
            offset += 4;
            info.LightColor.CopyTo(blit, offset);
            offset += 4;
            return blit;
        }
    }
}
