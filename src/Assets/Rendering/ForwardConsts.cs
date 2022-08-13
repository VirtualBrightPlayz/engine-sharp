using System.Collections.Generic;
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

        public class ForwardLight
        {
            public Vector3 Position;
            public Vector4 Color;
            public float Range;
        }

        public const string LightBufferName = "LightInfo0";
        public const string ForwardBasePassName = "FwdBase";
        public const string ForwardAddPassName = "FwdAdd";
        public static List<ForwardLight> Lights = new List<ForwardLight>();
        public static Vector4 AmbientColor = Vector4.One;
        public static int MaxRealtimeLights = 4;

        public static float[] GetLightInfo(ForwardLight light, bool basePass)
        {
            LightInfo info = new LightInfo()
            {
                AmbientLight = basePass ? AmbientColor : Vector4.Zero,
                LightPosition = new Vector4(light.Position, light.Range),
                LightColor = light.Color,
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
