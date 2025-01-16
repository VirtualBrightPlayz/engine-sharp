using System;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using System.Runtime.InteropServices;
using System.Text;
using Engine.Assets.Textures;
using ImGuiNET;
using Silk.NET.Core.Native;
using Silk.NET.SDL;
using Silk.NET.Vulkan;
using Silk.NET.Vulkan.Extensions.EXT;
using Silk.NET.Vulkan.Extensions.KHR;
using Silk.NET.Windowing;
using Veldrid.StartupUtilities;

namespace Engine.Assets.Rendering
{
    public static partial class RenderingGlobals
    {
        // public static Sdl sdl { get; private set; }
        public static Vk vk { get; private set; }
        public static Instance Instance { get; private set; }
        public static ExtDebugUtils DebugUtils { get; private set; }
        public static KhrSurface KhrSurface { get; private set; }
        public static SurfaceKHR Surface { get; private set; }
        public static PhysicalDevice PhysDevice { get; private set; }
        public static Device Device { get; private set; }
        public static Queue GfxQueue { get; private set; }
        public static Queue PresentQueue { get; private set; }
        public const string VkValidationLayer = "VK_LAYER_KHRONOS_validation";
        public const string VkDebugExt = "VK_EXT_debug_utils";
        private static List<string> EnabledLayers;

        private struct QueueFamilyIndices
        {
            public uint? Graphics;
            public uint? Present;
            public bool IsComplete => Graphics.HasValue && Present.HasValue;
        }

        private static unsafe bool ByteStringEquals(byte* bytes, string str)
        {
            string str2 = SilkMarshal.PtrToString((nint)bytes);
            return str.Equals(str2);
            /*
            byte[] data = Encoding.UTF8.GetBytes(str);
            Span<byte> span = new Span<byte>(bytes, len);
            for (int i = 0; i < data.Length; i++)
            {
                if (span[i] != data[i])
                    return false;
            }
            return true;
            */
        }

        private static void EnsureSuccess(Result result, string error = default)
        {
            if (result == Result.Success)
                return;
            if (string.IsNullOrEmpty(error))
                throw new Exception(result.ToString());
            throw new Exception($"{error}: {result}");
        }

        #region Window

        public static unsafe void InitWindow()
        {
            ViewSize = new Vector2(1280, 720);
            WindowCreateInfo winCI = new WindowCreateInfo()
            {
                X = 100,
                Y = 100,
                WindowWidth = (int)ViewSize.X,
                WindowHeight = (int)ViewSize.Y,
            };
            Window = VeldridStartup.CreateWindow(winCI);
        }

        private static unsafe void CreateSurface()
        {
            if (!vk.TryGetInstanceExtension(Instance, out KhrSurface khrSurface))
            {
                throw new Exception("KHR_surface extension not found.");
            }
            KhrSurface = khrSurface;
            // sdl = Sdl.GetApi();
            IView view = Silk.NET.Windowing.Sdl.SdlWindowing.CreateFrom((void*)Window.SdlWindowHandle);
            Surface = view.VkSurface.Create<AllocationCallbacks>(Instance.ToHandle(), null).ToSurface();
        }

        #endregion

        #region Instance

        private static unsafe uint DebugCallback(DebugUtilsMessageSeverityFlagsEXT messageSeverity, DebugUtilsMessageTypeFlagsEXT messageTypes, DebugUtilsMessengerCallbackDataEXT* pCallbackData, void* pUserData)
        {
            Log.Error(nameof(RenderingGlobals), $"validation layer: {Marshal.PtrToStringAnsi((nint)pCallbackData->PMessage)}");
            return Vk.False;
        }

        private static unsafe DebugUtilsMessengerCreateInfoEXT NewDebugUtilsCI()
        {
            return new DebugUtilsMessengerCreateInfoEXT(
                messageSeverity: DebugUtilsMessageSeverityFlagsEXT.VerboseBitExt | DebugUtilsMessageSeverityFlagsEXT.InfoBitExt | DebugUtilsMessageSeverityFlagsEXT.WarningBitExt | DebugUtilsMessageSeverityFlagsEXT.ErrorBitExt,
                messageType: DebugUtilsMessageTypeFlagsEXT.GeneralBitExt | DebugUtilsMessageTypeFlagsEXT.ValidationBitExt | DebugUtilsMessageTypeFlagsEXT.PerformanceBitExt,
                pfnUserCallback: (PfnDebugUtilsMessengerCallbackEXT)DebugCallback
            );
        }

        public static unsafe void InitVulkanInstance(bool debug)
        {
            EnabledLayers = new List<string>();
            if (debug)
                EnabledLayers.Add(VkValidationLayer);
            vk = Vk.GetApi();
            uint layerCount = 0;
            vk.EnumerateInstanceLayerProperties(ref layerCount, null);
            LayerProperties[] layerProps = new LayerProperties[layerCount];
            vk.EnumerateInstanceLayerProperties(ref layerCount, ref layerProps[0]);
            if (debug)
            {
                if (!layerProps.Any(x => ByteStringEquals(x.LayerName, VkValidationLayer)))
                {
                    throw new Exception($"{VkValidationLayer} not found.");
                }
            }
            // create instance
            ApplicationInfo appInfo = new ApplicationInfo();
            appInfo.ApiVersion = Vk.Version13;
            appInfo.EngineVersion = Vk.MakeVersion(1, 0);
            appInfo.ApplicationVersion = Vk.MakeVersion(1, 0);

            List<string> layers = new List<string>();
            if (debug)
                layers.Add(VkValidationLayer);
            List<string> extensions = new List<string>();
            if (debug)
                extensions.Add(VkDebugExt);

            InstanceCreateInfo createInfo = new InstanceCreateInfo(
                pApplicationInfo: &appInfo,
                enabledLayerCount: (uint)layers.Count, ppEnabledLayerNames: (byte**)SilkMarshal.StringArrayToPtr(layers),
                enabledExtensionCount: (uint)extensions.Count, ppEnabledExtensionNames: (byte**)SilkMarshal.StringArrayToPtr(extensions)
            );
            if (debug)
            {
                DebugUtilsMessengerCreateInfoEXT debugCI = NewDebugUtilsCI();
                createInfo.PNext = &debugCI;
            }
            EnsureSuccess(vk.CreateInstance(in createInfo, null, out Instance instance));
            Instance = instance;
            SilkMarshal.Free((nint)createInfo.PpEnabledExtensionNames);
            SilkMarshal.Free((nint)createInfo.PpEnabledLayerNames);

            if (debug)
            {
                if (!vk.TryGetInstanceExtension(Instance, out ExtDebugUtils debugUtils))
                {
                    throw new Exception();
                }
                DebugUtils = debugUtils;

                DebugUtilsMessengerCreateInfoEXT debugCI = NewDebugUtilsCI();
                EnsureSuccess(DebugUtils.CreateDebugUtilsMessenger(Instance, in debugCI, null, out DebugUtilsMessengerEXT messenger));
            }
        }

        #endregion

        #region Physical Device

        private static unsafe QueueFamilyIndices FindQueueFamilies(PhysicalDevice device)
        {
            QueueFamilyIndices indices = new QueueFamilyIndices();
            uint queueFamilyCount = 0;
            vk.GetPhysicalDeviceQueueFamilyProperties(device, ref queueFamilyCount, null);
            var queueFamilies = new QueueFamilyProperties[queueFamilyCount];
            fixed (QueueFamilyProperties* ptr = queueFamilies)
            {
                vk.GetPhysicalDeviceQueueFamilyProperties(device, ref queueFamilyCount, ptr);
            }

            uint i = 0;
            foreach (var queueFamily in queueFamilies)
            {
                if (queueFamily.QueueFlags.HasFlag(QueueFlags.GraphicsBit))
                    indices.Graphics = i;
                KhrSurface.GetPhysicalDeviceSurfaceSupport(device, i, Surface, out var presentSupport);
                if (presentSupport)
                    indices.Present = i;
                if (indices.IsComplete)
                    break;
                i++;
            }
            return indices;
        }

        private static unsafe bool IsDeviceSuitable(PhysicalDevice device)
        {
            var inds = FindQueueFamilies(device);
            return inds.IsComplete;
        }

        public static unsafe void InitVulkanPhysicalDevice()
        {
            var devices = vk.GetPhysicalDevices(Instance);

            foreach (var device in devices)
            {
                if (IsDeviceSuitable(device))
                {
                    PhysDevice = device;
                    break;
                }
            }

            if (PhysDevice.Handle == 0)
            {
                throw new Exception("Valid GPU not found!");
            }
        }

        #endregion

        #region Logical Device

        public static unsafe void InitVulkanLogicalDevice()
        {
            var inds = FindQueueFamilies(PhysDevice);

            uint[] queueFamilies = new uint[]
            {
                inds.Graphics.Value,
                inds.Present.Value,
            }.Distinct().ToArray();
            float queuePriority = 1f;
            DeviceQueueCreateInfo* queueCreateInfos = stackalloc DeviceQueueCreateInfo[queueFamilies.Length];
            for (int i = 0; i < queueFamilies.Length; i++)
            {
                DeviceQueueCreateInfo queueCreateInfo = new DeviceQueueCreateInfo(
                    queueCount: 1, queueFamilyIndex: queueFamilies[i], pQueuePriorities: &queuePriority
                );
                queueCreateInfos[i] = queueCreateInfo;
            }

            PhysicalDeviceBufferDeviceAddressFeatures devAddrBufFeature = new PhysicalDeviceBufferDeviceAddressFeatures(bufferDeviceAddress: true);
            PhysicalDeviceFeatures2 deviceFeatures2 = new PhysicalDeviceFeatures2(pNext: &devAddrBufFeature);

            DeviceCreateInfo createInfo = new DeviceCreateInfo(
                queueCreateInfoCount: (uint)queueFamilies.Length, pQueueCreateInfos: queueCreateInfos,
                pNext: &deviceFeatures2,
                enabledLayerCount: (uint)EnabledLayers.Count, ppEnabledLayerNames: (byte**)SilkMarshal.StringArrayToPtr(EnabledLayers)
            );
            EnsureSuccess(vk.CreateDevice(PhysDevice, in createInfo, null, out Device device));
            vk.GetDeviceQueue(device, inds.Graphics.Value, 0, out Queue gfxQueue);
            vk.GetDeviceQueue(device, inds.Present.Value, 0, out Queue presentQueue);
            SilkMarshal.Free((nint)createInfo.PpEnabledLayerNames);
            Device = device;
            GfxQueue = gfxQueue;
            PresentQueue = presentQueue;
        }

        #endregion
    }
}