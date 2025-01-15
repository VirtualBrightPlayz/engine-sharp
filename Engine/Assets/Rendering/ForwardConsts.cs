using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
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
        public static ConcurrentBag<ForwardLight> Lights = new ConcurrentBag<ForwardLight>();
        public static Vector4 AmbientColor = Vector4.One * 0.1f;
        public static int MaxRealtimeLights = 4;
        public static UniformBuffer LightUniform;
        public static List<UniformBuffer> LightUniforms = new List<UniformBuffer>();

        public static bool IsPassValid(int pass)
        {
            if (pass > Math.Floor((float)MaxRealtimeLights / MaxLightsPerPass))
                return false;
            if (pass > Math.Floor((float)Lights.Count / MaxLightsPerPass))
                return false;
            return true;
            // return Math.Min(MaxRealtimeLights, Math.Min(Lights.Count, pass * MaxLightsPerPass)) > 0;
        }

        public static int GetPass(Renderer renderer, string pass)
        {
            if (pass == ForwardBasePassName)
                return 0;
            string[] args = pass.Split('#');
            if (args[0] == ForwardAddPassName && args.Length > 1 && int.TryParse(args[1], out int i))
            {
                return i;
            }
            return 0;
        }

        public static void UpdateUniforms(Renderer renderer)
        {
            if (LightUniform == null || !LightUniform.IsValid)
                LightUniform = new UniformBuffer(LightBufferName, LightInfo.Size);
            ForwardLight[] sortedLights = Lights.OrderBy(x => (x.Position - renderer.ViewPosition).LengthSquared()).Take(MaxRealtimeLights).ToArray();
            LightUniform.UploadData(renderer, GetLightInfo(0, true, sortedLights));
            if (MaxRealtimeLights <= 0)
            {
                for (int i = 0; i < LightUniforms.Count; i++)
                {
                    if (LightUniforms[i] == null || !LightUniforms[i].IsValid)
                        LightUniforms[i] = new UniformBuffer(LightBufferName, LightInfo.Size);
                    LightUniforms[i].UploadData(renderer, GetLightInfo(0, true, sortedLights));
                }
                LightUniforms.Clear();
                return;
            }
            int loopCount = (int)Math.Ceiling((float)MaxRealtimeLights / MaxLightsPerPass);
            if (loopCount > LightUniforms.Count)
            {
                LightUniforms.Add(null);
            }
            else if (loopCount < LightUniforms.Count)
            {
                while (loopCount < LightUniforms.Count)
                {
                    LightUniforms.RemoveAt(LightUniforms.Count - 1);
                }
            }
            for (int i = 0; i < LightUniforms.Count; i++)
            {
                if (LightUniforms[i] == null || !LightUniforms[i].IsValid)
                    LightUniforms[i] = new UniformBuffer(LightBufferName, LightInfo.Size);
                LightUniforms[i].UploadData(renderer, GetLightInfo(i, i == 0, sortedLights));
            }
        }

        public static float[] GetLightInfo(int pass, bool basePass, ForwardLight[] sortedLights)
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
            int remainingLights = sortedLights.Length - pass * MaxLightsPerPass;
            remainingLights = Math.Min(remainingLights, MaxRealtimeLights - pass * MaxLightsPerPass);
            info.AmbientLight.W = Math.Min(remainingLights, MaxLightsPerPass);
            for (int i = pass * MaxLightsPerPass; i < pass * MaxLightsPerPass + Math.Min(remainingLights, MaxLightsPerPass); i++)
            {
                info.LightPosition[i%MaxLightsPerPass] = new Vector4(sortedLights[i].Position, sortedLights[i].Range);
                info.LightColor[i%MaxLightsPerPass] = sortedLights[i].Color;
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
