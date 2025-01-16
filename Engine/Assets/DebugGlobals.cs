using System;
using System.Linq;
using Engine.Assets.Audio;
using Engine.Assets.Models;
using Engine.Assets.Rendering;
using Engine.Assets.Textures;
using ImGuiNET;
using Veldrid;

namespace Engine.Assets
{
    public static class DebugGlobals
    {
        public static void DrawDebugWindow()
        {
            if (ImGui.Begin("Engine Debug"))
            {
                ImGui.Text($"FPS: {MiscGlobals.FPS}");
                ImGui.Text($"Current Rendering API: {RenderingGlobals.APIBackend}");
                if (ImGui.Button("Exit"))
                {
                    MiscGlobals.IsClosing = true;
                }
                ImGui.Text($"Mesh Count: {ResourceManager.AllResources.Count(x => x.TryGetTarget(out var res) && res is Mesh)}");
                ImGui.Text($"Material Count: {ResourceManager.AllResources.Count(x => x.TryGetTarget(out var res) && res is Material)}");
                ImGui.Text($"Texture2D Count: {ResourceManager.AllResources.Count(x => x.TryGetTarget(out var res) && res is Texture2D)}");
                ImGui.Text($"AudioClip Count: {ResourceManager.AllResources.Count(x => x.TryGetTarget(out var res) && res is AudioClip)}");
                ImGui.Text($"AudioSource Count: {ResourceManager.AllResources.Count(x => x.TryGetTarget(out var res) && res is AudioSource)}");
                if (ImGui.Button("Collect Garbage"))
                {
                    GC.Collect();
                }
                if (ImGui.Button("ReCreate Resources"))
                {
                    MiscGlobals.ReCreateAllNextFrame = true;
                }
                if (ImGui.Button("Rendering API"))
                {
                    ImGui.OpenPopup("DebugRenderAPI");
                }
                if (ImGui.BeginPopup("DebugRenderAPI"))
                {
                    var vals = Enum.GetValues<GraphicsBackend>();
                    foreach (var val in vals)
                    {
                        if (GraphicsDevice.IsBackendSupported(val) && ImGui.Button(val.ToString()))
                        {
                            RenderingGlobals.NextFrameBackend = val;
                        }
                    }
                    ImGui.EndPopup();
                }
                if (RenderingGlobals.RenderDocInstance == null)
                {
                    if (ImGui.Button("RenderDoc"))
                    {
                        RenderDoc rd = null;
                        if (RenderDoc.Load(out rd))
                        {
                            RenderingGlobals.RenderDocInstance = rd;
                            // RenderingGlobals.NextFrameBackend = RenderingGlobals.APIBackend;
                        }
                    }
                }
                else
                {
                    if (ImGui.Button("Capture"))
                    {
                        RenderingGlobals.RenderDocInstance.TriggerCapture();
                    }
                    if (ImGui.Button("Open UI"))
                    {
                        RenderingGlobals.RenderDocInstance.LaunchReplayUI();
                    }
                }
            }
            ImGui.End();
        }
    }
}