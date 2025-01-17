using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using Engine.Assets.Textures;
using Engine.Game;

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

        [StructLayout(LayoutKind.Sequential)]
        public struct ShadowInfo
        {
            public Vector4[] LightPosition;
            public Matrix4x4[] LightProjection;
            public const uint Size = (4 + 16) * MaxLightsPerPass * 6 * sizeof(float);
        }

        public class ForwardLight
        {
            public Vector3 Position;
            public Vector4 Color;
            public float Range;
        }

        public const int MaxLightsPerPass = 4;
        public const string LightBufferName = "LightInfo0";
        public const string ShadowAtlasName = "ShadowAtlasTexture";
        public const string ForwardBasePassName = "FwdBase";
        public const string ForwardAddPassName = "FwdAdd";
        public const string ForwardDepthPassName = "FwdDepth";
        public static List<ForwardLight> Lights = new List<ForwardLight>();
        public static Vector4 AmbientColor = Vector4.One * 0.1f;
        public static int MaxRealtimeLights = 4;
        public static uint ShadowResolution = 2048;
        public static UniformBuffer LightUniform;
        public static List<UniformBuffer> LightUniforms = new List<UniformBuffer>();
        public static List<UniformBuffer> ShadowUniforms = new List<UniformBuffer>();
        public static List<RenderTexture2D> ShadowAtlasTextures = new List<RenderTexture2D>();

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

        public static void RenderShadows(GameApp app, Renderer renderer)
        {
            if (MaxRealtimeLights <= 0)
            {
                ShadowAtlasTextures.Clear();
                return;
            }
            int loopCount = (int)Math.Ceiling((float)MaxRealtimeLights / MaxLightsPerPass);
            if (loopCount > ShadowAtlasTextures.Count)
            {
                ShadowAtlasTextures.Add(null);
            }
            else if (loopCount < ShadowAtlasTextures.Count)
            {
                while (loopCount < ShadowAtlasTextures.Count)
                {
                    ShadowAtlasTextures.RemoveAt(ShadowAtlasTextures.Count - 1);
                }
            }
            ForwardLight[] sortedLights = Lights.OrderBy(x => (x.Position - Renderer.Main.ViewPosition).LengthSquared()).Take(MaxRealtimeLights).ToArray();
            for (int i = 0; i < ShadowAtlasTextures.Count; i++)
            {
                if (ShadowAtlasTextures[i] == null || !ShadowAtlasTextures[i].IsValid)
                    ShadowAtlasTextures[i] = new RenderTexture2D(ShadowAtlasName, ShadowResolution, ShadowResolution);
                RenderShadowsPass(i, app, renderer, sortedLights);
            }
        }

        public static void RenderShadowsPass(int pass, GameApp app, Renderer renderer, ForwardLight[] sortedLights)
        {
            Vector3[] fwds = new Vector3[]
            {
                Vector3.UnitX,
                -Vector3.UnitX,
                Vector3.UnitY,
                -Vector3.UnitY,
                Vector3.UnitZ,
                -Vector3.UnitZ,
            };
            Vector3[] ups = new Vector3[]
            {
                -Vector3.UnitY,
                -Vector3.UnitY,
                Vector3.UnitZ,
                -Vector3.UnitZ,
                -Vector3.UnitY,
                -Vector3.UnitY,
            };
            renderer.SetRenderTarget(ShadowAtlasTextures[pass]);
            renderer.Begin();
            renderer.Clear();
            renderer.End();
            renderer.Submit();
            uint texWidth = ShadowResolution;
            uint div = 5;
            uint texDiv = texWidth / div;
            ShadowInfo data = new ShadowInfo()
            {
                LightPosition = new Vector4[MaxLightsPerPass * 6],
                LightProjection = new Matrix4x4[MaxLightsPerPass * 6],
            };
            for (int i = 0; i < MaxLightsPerPass*6; i++)
            {
                int lightIdx = (pass * MaxLightsPerPass) + (i/6);
                if (lightIdx >= sortedLights.Length)
                    break;
                ForwardLight light = sortedLights[lightIdx];
                renderer.ViewPosition = Renderer.Main.ViewPosition;
                renderer.ProjectionMatrix = Matrix4x4.CreatePerspectiveFieldOfView(90f * (MathF.PI / 180f), 1f, 0.01f, 25f);
                renderer.ViewMatrix = Matrix4x4.CreateLookAt(light.Position, light.Position + fwds[i%6], ups[i%6]);
                app.PreDraw(renderer, 0d);
                renderer.Begin();
                uint x = (uint)(i % div) * texDiv;
                uint y = (uint)(i / div) * texDiv;
                renderer.SetRect(x, y, texDiv, texDiv);
                app.DrawDepth(renderer);
                renderer.End();
                renderer.Submit();
            }
            ShadowUniforms[pass].UploadData(renderer, GetShadowInfo(data));
        }

        public unsafe static float[] GetShadowInfo(ShadowInfo info)
        {
            float[] blit = new float[ShadowInfo.Size / sizeof(float)];
            int offset = 0;
            for (int i = 0; i < MaxLightsPerPass*6; i++)
            {
                info.LightPosition[i].CopyTo(blit, offset);
                offset += 4;
            }
            for (int i = 0; i < MaxLightsPerPass*6; i++)
            {
                fixed (void* data = &info.LightProjection[i])
                {
                    Marshal.Copy((nint)data, blit, offset, sizeof(Matrix4x4));
                }
                offset += 16;
            }
            return blit;
        }

        public static void UpdateLightUniforms(Renderer renderer)
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
