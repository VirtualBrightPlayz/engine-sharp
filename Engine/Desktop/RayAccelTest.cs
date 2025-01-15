using System;
using System.Runtime.CompilerServices;
using Engine.Assets.Models;
using Engine.Assets.Rendering;
using Silk.NET.Core;
using Silk.NET.Vulkan;
using Silk.NET.Vulkan.Extensions.KHR;
using Veldrid;
using VMASharp;
using Buffer = Silk.NET.Vulkan.Buffer;

public class RayAccelTest
{
    public static void AssertSuccess(Result result)
    {
        if (result == Result.Success)
            return;
        Log.Fatal(nameof(RayAccelTest), result.ToString());
    }

    public static Vk Api { get; private set; } = null;
    public static BackendInfoVulkan VkInfo { get; private set; } = null;
    public static VulkanMemoryAllocator Memory { get; private set; } = null;
    public static CommandPool CmdPool { get; private set; } = default;

    public static void CreateVMA()
    {
        if (Memory != null)
        {
            Memory.Dispose();
            Memory = null;
        }
        Instance inst = new Instance(VkInfo.Instance);
        PhysicalDevice physDev = new PhysicalDevice(VkInfo.PhysicalDevice);
        Device dev = new Device(VkInfo.Device);
        uint version = 0;
        Api.EnumerateInstanceVersion(ref version);
        VulkanMemoryAllocatorCreateInfo vmaCI = new VulkanMemoryAllocatorCreateInfo((Version32)version, Api, inst, physDev, dev);
        Memory = new VulkanMemoryAllocator(vmaCI);
    }

    public static unsafe void CreatePool()
    {
        Instance inst = new Instance(VkInfo.Instance);
        PhysicalDevice physDev = new PhysicalDevice(VkInfo.PhysicalDevice);
        Device dev = new Device(VkInfo.Device);
        CommandPoolCreateInfo poolCI = new CommandPoolCreateInfo(flags: CommandPoolCreateFlags.ResetCommandBufferBit, queueFamilyIndex: VkInfo.GraphicsQueueFamilyIndex);
        CommandPool pool = default;
        AssertSuccess(Api.CreateCommandPool(dev, ref poolCI, null, out pool));
        CmdPool = pool;
    }

    public static unsafe CommandBuffer CreateCmdBuffer()
    {
        CommandBufferAllocateInfo cmdAI = new CommandBufferAllocateInfo(commandPool: CmdPool, level: CommandBufferLevel.Primary, commandBufferCount: 1);
        Device dev = new Device(VkInfo.Device);
        AssertSuccess(Api.AllocateCommandBuffers(dev, ref cmdAI, out CommandBuffer buffer));
        return buffer;
    }

    public static unsafe void FreeCmdBuffer(CommandBuffer cmd)
    {
        Device dev = new Device(VkInfo.Device);
        Api.FreeCommandBuffers(dev, CmdPool, 1, ref cmd);
    }

    public static uint GetByteLength<T>(T[] arr) where T : unmanaged
    {
        return (uint)Unsafe.SizeOf<T>() * (uint)arr.Length;
    }

    public static unsafe void CreateHostBufferWith<T>(ReadOnlySpan<T> span, out Buffer buffer, out Allocation allocation) where T : unmanaged
    {
        BufferCreateInfo bufferCI = new BufferCreateInfo(usage: BufferUsageFlags.TransferSrcBit, size: (uint)Unsafe.SizeOf<T>() * (uint)span.Length);
        AllocationCreateInfo allocCI = new AllocationCreateInfo(AllocationCreateFlags.Mapped, usage: MemoryUsage.CPU_Only);
        buffer = Memory.CreateBuffer(bufferCI, allocCI, out allocation);
        if (!allocation.TryGetSpan(out Span<T> bufferSpan))
        {
            throw new InvalidOperationException("Unable to get Span<T> from allocation");
        }
        span.CopyTo(bufferSpan);
    }

    public static unsafe void CreateDeviceBuffer(BufferUsageFlags usage, uint size, out Buffer buffer, out Allocation allocation)
    {
        BufferCreateInfo bufferCI = new BufferCreateInfo(usage: usage | BufferUsageFlags.TransferDstBit, size: size);
        AllocationCreateInfo allocCI = new AllocationCreateInfo(usage: MemoryUsage.GPU_Only);
        buffer = Memory.CreateBuffer(bufferCI, allocCI, out allocation);
    }

    // public static unsafe void UpdateDeviceBuffer

    public static unsafe void CreateDeviceBufferWith<T>(T[] arr, BufferUsageFlags usage, out Buffer buffer, out Allocation allocation) where T : unmanaged
    {
        Device dev = new Device(VkInfo.Device);
        Queue queue = new Queue(VkInfo.GraphicsQueue);
        CreateHostBufferWith<T>(arr, out Buffer hostBuffer, out Allocation hostAlloc);
        CreateDeviceBuffer(usage, GetByteLength(arr), out Buffer gpuBuffer, out Allocation gpuAlloc);
        // begin cmd
        CommandBuffer cmd = CreateCmdBuffer();
        CommandBufferBeginInfo beginInfo = new CommandBufferBeginInfo(flags: CommandBufferUsageFlags.OneTimeSubmitBit);
        AssertSuccess(Api.BeginCommandBuffer(cmd, ref beginInfo));
        // copy data
        BufferCopy* bufCopy = stackalloc BufferCopy[1];
        Api.CmdCopyBuffer(cmd, hostBuffer, gpuBuffer, 1, bufCopy);
        // end cmd
        AssertSuccess(Api.EndCommandBuffer(cmd));
        // submit
        var fenceCI = new FenceCreateInfo();
        AssertSuccess(Api.CreateFence(dev, ref fenceCI, null, out Silk.NET.Vulkan.Fence fence));
        var subInfo = new SubmitInfo(commandBufferCount: 1, pCommandBuffers: &cmd);
        AssertSuccess(Api.QueueSubmit(queue, 1, ref subInfo, fence));
        Api.WaitForFences(dev, new [] { fence, }, true, ulong.MaxValue);
        Api.DestroyFence(dev, fence, null);
        // free cmd
        FreeCmdBuffer(cmd);
        // destroy buffers
        Api.DestroyBuffer(dev, hostBuffer, null);
        hostAlloc.Dispose();
        buffer = gpuBuffer;
        allocation = gpuAlloc;
    }

    public static unsafe void CreateBLAS(Model mdl, KhrAccelerationStructure accel, KhrRayTracingPipeline rtp)
    {
        // create buffers for RT
        BufferCreateInfo bufCI = new BufferCreateInfo(usage: BufferUsageFlags.ShaderDeviceAddressBit | BufferUsageFlags.StorageBufferBit | BufferUsageFlags.AccelerationStructureBuildInputReadOnlyBitKhr);
        CreateDeviceBufferWith(mdl.CollisionPositions, BufferUsageFlags.ShaderDeviceAddressBit | BufferUsageFlags.StorageBufferBit | BufferUsageFlags.AccelerationStructureBuildInputReadOnlyBitKhr, out Buffer buffer, out Allocation allocation);
        AccelerationStructureGeometryTrianglesDataKHR tris = new AccelerationStructureGeometryTrianglesDataKHR(vertexFormat: Format.R32G32B32Sfloat);
    }

    public static void MainTest()
    {
        if (!RenderingGlobals.GameGraphics.GetVulkanInfo(out BackendInfoVulkan info))
        {
            Log.Error(nameof(RayAccelTest), "Vulkan is not in use!");
            return;
        }
        VkInfo = info;
        Api = Vk.GetApi();
        Instance inst = new Instance(info.Instance);
        Device dev = new Device(info.Device);
        if (!Api.TryGetDeviceExtension<KhrAccelerationStructure>(inst, dev, out var accel))
        {
            Log.Error(nameof(RayAccelTest), "KhrAccelerationStructure ext not found.");
            return;
        }
        if (!Api.TryGetDeviceExtension<KhrRayTracingPipeline>(inst, dev, out var rtp))
        {
            Log.Error(nameof(RayAccelTest), "KhrRayTracingPipeline ext not found.");
            return;
        }
        Log.Info(nameof(RayAccelTest), "Rays can be traced!");
        CreateVMA();
        Model mdl = new Model("Test", "Shaders/cube.gltf", new Material("Test", new GraphicsShader("Shaders/MainMesh")), true, false);
        CreateBLAS(mdl, accel, rtp);

        // vk.CmdBindPipeline(null, PipelineBindPoint.RayTracingKhr, null);
        // rtp.CmdTraceRays();
    }
}