using System.Runtime.InteropServices;
using EM_BOOL = System.Int32;
using EMSCRIPTEN_RESULT = System.Int32;
using EMSCRIPTEN_WEBGL_CONTEXT_HANDLE = System.Int32;
using EM_WEBGL_POWER_PREFERENCE = System.Int32;
using EMSCRIPTEN_WEBGL_CONTEXT_PROXY_MODE = System.Int32;
using System;

public static unsafe class Emscripten
{
    public const string NativeLibName = "emscripten";

    public const EMSCRIPTEN_RESULT EMSCRIPTEN_RESULT_SUCCESS = 0;
    public const EMSCRIPTEN_RESULT EMSCRIPTEN_RESULT_DEFERRED = 1;
    public const EMSCRIPTEN_RESULT EMSCRIPTEN_RESULT_NOT_SUPPORTED = -1;
    public const EMSCRIPTEN_RESULT EMSCRIPTEN_RESULT_FAILED_NOT_DEFERRED = -2;
    public const EMSCRIPTEN_RESULT EMSCRIPTEN_RESULT_INVALID_TARGET = -3;
    public const EMSCRIPTEN_RESULT EMSCRIPTEN_RESULT_UNKNOWN_TARGET = -4;
    public const EMSCRIPTEN_RESULT EMSCRIPTEN_RESULT_INVALID_PARAM = -5;
    public const EMSCRIPTEN_RESULT EMSCRIPTEN_RESULT_FAILED = -6;
    public const EMSCRIPTEN_RESULT EMSCRIPTEN_RESULT_NO_DATA = -7;
    public const EMSCRIPTEN_RESULT EMSCRIPTEN_RESULT_TIMED_OUT = -8;

    public const EMSCRIPTEN_WEBGL_CONTEXT_PROXY_MODE EMSCRIPTEN_WEBGL_CONTEXT_PROXY_DISALLOW = 0;
    public const EMSCRIPTEN_WEBGL_CONTEXT_PROXY_MODE EMSCRIPTEN_WEBGL_CONTEXT_PROXY_FALLBACK = 1;
    public const EMSCRIPTEN_WEBGL_CONTEXT_PROXY_MODE EMSCRIPTEN_WEBGL_CONTEXT_PROXY_ALWAYS = 2;

    public const EM_WEBGL_POWER_PREFERENCE EM_WEBGL_POWER_PREFERENCE_DEFAULT = 0;
    public const EM_WEBGL_POWER_PREFERENCE EM_WEBGL_POWER_PREFERENCE_LOW_POWER = 1;
    public const EM_WEBGL_POWER_PREFERENCE EM_WEBGL_POWER_PREFERENCE_HIGH_PERFORMANCE = 2;

    [StructLayout(LayoutKind.Sequential)]
    public struct EmscriptenWebGLContextAttributes
    {
        public EM_BOOL alpha;
        public EM_BOOL depth;
        public EM_BOOL stencil;
        public EM_BOOL antialias;
        public EM_BOOL premultipliedAlpha;
        public EM_BOOL preserveDrawingBuffer;
        public EM_WEBGL_POWER_PREFERENCE powerPreference;
        public EM_BOOL failIfMajorPerformanceCaveat;

        public int majorVersion;
        public int minorVersion;

        public EM_BOOL enableExtensionsByDefault;
        public EM_BOOL explicitSwapControl;
        public EMSCRIPTEN_WEBGL_CONTEXT_PROXY_MODE proxyContextToMainThread;
        public EM_BOOL renderViaOffscreenBackBuffer;
    }

    [DllImport(NativeLibName)]
    public static extern IntPtr emscripten_GetProcAddress(string name);

    [DllImport(NativeLibName)]
    public static extern EMSCRIPTEN_WEBGL_CONTEXT_HANDLE emscripten_webgl_create_context(string target, EmscriptenWebGLContextAttributes* attributes);
    [DllImport(NativeLibName)]
    public static extern EM_BOOL emscripten_webgl_enable_extension(EMSCRIPTEN_WEBGL_CONTEXT_HANDLE context, string extension);


    [DllImport(NativeLibName)]
    public static extern void emscripten_webgl_init_context_attributes(EmscriptenWebGLContextAttributes* attributes);
    [DllImport(NativeLibName)]
    public static extern EMSCRIPTEN_WEBGL_CONTEXT_HANDLE emscripten_webgl_create_context(char* target, EmscriptenWebGLContextAttributes* attributes);
    [DllImport(NativeLibName)]
    public static extern EMSCRIPTEN_RESULT emscripten_webgl_make_context_current(EMSCRIPTEN_WEBGL_CONTEXT_HANDLE context);
    [DllImport(NativeLibName)]
    public static extern EMSCRIPTEN_WEBGL_CONTEXT_HANDLE emscripten_webgl_get_current_context();
    [DllImport(NativeLibName)]
    public static extern EMSCRIPTEN_RESULT emscripten_webgl_get_drawing_buffer_size(EMSCRIPTEN_WEBGL_CONTEXT_HANDLE context, int* width, int* height);
    [DllImport(NativeLibName)]
    public static extern EMSCRIPTEN_RESULT emscripten_webgl_get_context_attributes(EMSCRIPTEN_WEBGL_CONTEXT_HANDLE context, EmscriptenWebGLContextAttributes* outAttributes);
    [DllImport(NativeLibName)]
    public static extern EMSCRIPTEN_RESULT emscripten_webgl_destroy_context(EMSCRIPTEN_WEBGL_CONTEXT_HANDLE context);
    [DllImport(NativeLibName)]
    public static extern EM_BOOL emscripten_webgl_enable_extension(EMSCRIPTEN_WEBGL_CONTEXT_HANDLE context, char* extension);
    [DllImport(NativeLibName)]
    public static extern EM_BOOL emscripten_webgl_enable_ANGLE_instanced_arrays(EMSCRIPTEN_WEBGL_CONTEXT_HANDLE context);
    [DllImport(NativeLibName)]
    public static extern EM_BOOL emscripten_webgl_enable_OES_vertex_array_object(EMSCRIPTEN_WEBGL_CONTEXT_HANDLE context);
    [DllImport(NativeLibName)]
    public static extern EM_BOOL emscripten_webgl_enable_WEBGL_draw_buffers(EMSCRIPTEN_WEBGL_CONTEXT_HANDLE context);
    [DllImport(NativeLibName)]
    public static extern EM_BOOL emscripten_webgl_enable_WEBGL_draw_instanced_base_vertex_base_instance(EMSCRIPTEN_WEBGL_CONTEXT_HANDLE context);
    [DllImport(NativeLibName)]
    public static extern EM_BOOL emscripten_webgl_enable_WEBGL_multi_draw(EMSCRIPTEN_WEBGL_CONTEXT_HANDLE context);
    [DllImport(NativeLibName)]
    public static extern EM_BOOL emscripten_webgl_enable_WEBGL_multi_draw_instanced_base_vertex_base_instance(EMSCRIPTEN_WEBGL_CONTEXT_HANDLE context);
}