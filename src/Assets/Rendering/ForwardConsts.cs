using System;
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
            public Vector4[] LightPosition;
            public Vector4[] LightColor;
            public const uint Size = (4 + 4 * MaxLightsPerPass + 4 * MaxLightsPerPass) * sizeof(float);
        }

        public class ForwardLight
        {
            public Vector3 Position;
            public Vector4 Color;
            public float Range;
        }

        public const int MaxLightsPerPass = 4;
        public const string LightBufferName = "LightInfo0";
        public const string ForwardBasePassName = "FwdBase";
        public const string ForwardAddPassName = "FwdAdd";
        public static List<ForwardLight> Lights = new List<ForwardLight>();
        public static Vector4 AmbientColor = Vector4.One;
        public static int MaxRealtimeLights = 4;

        public static bool IsPassValid(int pass)
        {
            if (pass > Math.Floor((float)MaxRealtimeLights / MaxLightsPerPass))
                return false;
            if (pass > Math.Floor((float)Lights.Count / MaxLightsPerPass))
                return false;
            return true;
            // return Math.Min(MaxRealtimeLights, Math.Min(Lights.Count, pass * MaxLightsPerPass)) > 0;
        }

        public static float[] GetLightInfo(int pass, bool basePass)
        {
            LightInfo info = new LightInfo()
            {
                AmbientLight = basePass ? AmbientColor : Vector4.Zero,
                LightPosition = new Vector4[MaxLightsPerPass],
                LightColor = new Vector4[MaxLightsPerPass],
                // LightPosition = new Vector4(light.Position, light.Range),
                // LightColor = light.Color,
            };
            for (int i = 0; i < MaxLightsPerPass; i++)
            {
                info.LightPosition[i] = Vector4.Zero;
                info.LightColor[i] = Vector4.Zero;
            }
            int remainingLights = Lights.Count - pass * MaxLightsPerPass;
            remainingLights = Math.Min(remainingLights, MaxRealtimeLights - pass * MaxLightsPerPass);
            info.AmbientLight.W = Math.Min(remainingLights, MaxLightsPerPass);
            for (int i = pass * MaxLightsPerPass; i < pass * MaxLightsPerPass + Math.Min(remainingLights, MaxLightsPerPass); i++)
            {
                info.LightPosition[i%MaxLightsPerPass] = new Vector4(Lights[i].Position, Lights[i].Range);
                info.LightColor[i%MaxLightsPerPass] = Lights[i].Color;
            }
            float[] blit = new float[LightInfo.Size / sizeof(float)];
            int offset = 0;
            info.AmbientLight.CopyTo(blit, offset);
            offset += 4;
            for (int i = 0; i < MaxLightsPerPass; i++)
            {
                info.LightPosition[i].CopyTo(blit, offset);
                offset += 4;
            }
            for (int i = 0; i < MaxLightsPerPass; i++)
            {
                info.LightColor[i].CopyTo(blit, offset);
                offset += 4;
            }
            return blit;
        }
    }
}
