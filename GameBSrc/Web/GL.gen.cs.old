using System;
using System.Runtime.InteropServices;
using GLenum = System.UInt32;
using GLboolean = System.Byte;
using GLbitfield = System.UInt32;

using GLbyte = System.Char;
using GLubyte = System.Byte;
using GLshort = System.Int16;
using GLushort = System.UInt16;
using GLint = System.Int32;
using GLuint = System.UInt32;
using GLclampx = System.Int32;
using GLsizei = System.Int32;
using GLfloat = System.Single;
using GLclampf = System.Single;
using GLdouble = System.Double;
using GLclampd = System.Double;
using GLeglClientBufferEXT = System.IntPtr;
using GLeglImageOES = System.IntPtr;
using GLchar = System.Char;
using GLcharARB = System.Char;
using GLhandleARB = System.UInt32;

using GLhalf = System.UInt16;
using GLhalfARB = System.UInt16;
using GLfixed = System.Int32;
using GLintptr = System.IntPtr;
using GLintptrARB = System.IntPtr;
using GLsizeiptr = System.IntPtr;
using GLsizeiptrARB = System.IntPtr;
using GLint64 = System.Int64;
using GLint64EXT = System.Int64;
using GLuint64 = System.UInt64;
using GLuint64EXT = System.UInt64;
using GLsync = System.IntPtr;

using GLDEBUGPROC = System.IntPtr;

public static unsafe class GL
{
    public const string NativeLib = "gl";
    public const CallingConvention callConv = CallingConvention.Cdecl;
    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glAccum(GLenum op, GLfloat value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glAccumxOES(GLenum op, GLfixed value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glActiveProgramEXT(GLuint program);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glActiveShaderProgram(GLuint pipel_ine, GLuint program);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glActiveShaderProgramEXT(GLuint pipel_ine, GLuint program);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glActiveStencilFaceEXT(GLenum face);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glActiveTexture(GLenum texture);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glActiveTextureARB(GLenum texture);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glActiveVaryingNV(GLuint program, GLchar* name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glAlphaFragmentOp1ATI(GLenum op, GLuint dst, GLuint dstMod, GLuint arg1, GLuint arg1Rep, GLuint arg1Mod);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glAlphaFragmentOp2ATI(GLenum op, GLuint dst, GLuint dstMod, GLuint arg1, GLuint arg1Rep, GLuint arg1Mod, GLuint arg2, GLuint arg2Rep, GLuint arg2Mod);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glAlphaFragmentOp3ATI(GLenum op, GLuint dst, GLuint dstMod, GLuint arg1, GLuint arg1Rep, GLuint arg1Mod, GLuint arg2, GLuint arg2Rep, GLuint arg2Mod, GLuint arg3, GLuint arg3Rep, GLuint arg3Mod);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glAlphaFunc(GLenum func, GLfloat refer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glAlphaFuncQCOM(GLenum func, GLclampf refer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glAlphaFuncx(GLenum func, GLfixed refer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glAlphaFuncxOES(GLenum func, GLfixed refer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glAlphaToCoverageDitherControlNV(GLenum mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glApplyFramebufferAttachmentCMAAINTEL();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glApplyTextureEXT(GLenum mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glAcquireKeyedMutexWin32EXT(GLuint memory, GLuint64 key, GLuint time_out);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glAreProgramsResidentNV(GLsizei n, GLuint* programs, GLboolean* residences);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glAreTexturesResident(GLsizei n, GLuint* textures, GLboolean* residences);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glAreTexturesResidentEXT(GLsizei n, GLuint* textures, GLboolean* residences);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glArrayElement(GLint i);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glArrayElementEXT(GLint i);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glArrayObjectATI(GLenum array, GLint size, GLenum type, GLsizei stride, GLuint buffer, GLuint offset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLuint glAsyncCopyBufferSubDataNVX(GLsizei waitSemaphoreCount, GLuint* waitSemaphoreArray, GLuint64* fenceValueArray, GLuint readGpu, GLbitfield writeGpuMask, GLuint readBuffer, GLuint writeBuffer, GLintptr readOffset, GLintptr writeOffset, GLsizeiptr size, GLsizei signalSemaphoreCount, GLuint* signalSemaphoreArray, GLuint64* signalValueArray);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLuint glAsyncCopyImageSubDataNVX(GLsizei waitSemaphoreCount, GLuint* waitSemaphoreArray, GLuint64* waitValueArray, GLuint srcGpu, GLbitfield dstGpuMask, GLuint srcName, GLenum srcTarget, GLint srcLevel, GLint srcX, GLint srcY, GLint srcZ, GLuint dstName, GLenum dstTarget, GLint dstLevel, GLint dstX, GLint dstY, GLint dstZ, GLsizei srcWidth, GLsizei srcHeight, GLsizei srcDepth, GLsizei signalSemaphoreCount, GLuint* signalSemaphoreArray, GLuint64* signalValueArray);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glAsyncMarkerSGIX(GLuint marker);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glAttachObjectARB(GLhandleARB conta_inerObj, GLhandleARB obj);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glAttachShader(GLuint program, GLuint shader);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBegin(GLenum mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBeginConditionalRender(GLuint id, GLenum mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBeginConditionalRenderNV(GLuint id, GLenum mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBeginConditionalRenderNVX(GLuint id);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBeginFragmentShaderATI();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBeginOcclusionQueryNV(GLuint id);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBeginPerfMonitorAMD(GLuint monitor);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBeginPerfQueryINTEL(GLuint queryHandle);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBeginQuery(GLenum target, GLuint id);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBeginQueryARB(GLenum target, GLuint id);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBeginQueryEXT(GLenum target, GLuint id);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBeginQueryIndexed(GLenum target, GLuint _index, GLuint id);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBeginTransformFeedback(GLenum primitiveMode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBeginTransformFeedbackEXT(GLenum primitiveMode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBeginTransformFeedbackNV(GLenum primitiveMode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBeginVertexShaderEXT();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBeginVideoCaptureNV(GLuint video_capture_slot);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindAttribLocation(GLuint program, GLuint _index, GLchar* name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindAttribLocationARB(GLhandleARB programObj, GLuint _index, GLcharARB* name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindBuffer(GLenum target, GLuint buffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindBufferARB(GLenum target, GLuint buffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindBufferBase(GLenum target, GLuint _index, GLuint buffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindBufferBaseEXT(GLenum target, GLuint _index, GLuint buffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindBufferBaseNV(GLenum target, GLuint _index, GLuint buffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindBufferOffsetEXT(GLenum target, GLuint _index, GLuint buffer, GLintptr offset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindBufferOffsetNV(GLenum target, GLuint _index, GLuint buffer, GLintptr offset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindBufferRange(GLenum target, GLuint _index, GLuint buffer, GLintptr offset, GLsizeiptr size);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindBufferRangeEXT(GLenum target, GLuint _index, GLuint buffer, GLintptr offset, GLsizeiptr size);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindBufferRangeNV(GLenum target, GLuint _index, GLuint buffer, GLintptr offset, GLsizeiptr size);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindBuffersBase(GLenum target, GLuint first, GLsizei count, GLuint* buffers);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindBuffersRange(GLenum target, GLuint first, GLsizei count, GLuint* buffers, GLintptr* offsets, GLsizeiptr* sizes);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindFragDataLocation(GLuint program, GLuint color, GLchar* name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindFragDataLocationEXT(GLuint program, GLuint color, GLchar* name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindFragDataLocationIndexed(GLuint program, GLuint colorNumber, GLuint _index, GLchar* name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindFragDataLocationIndexedEXT(GLuint program, GLuint colorNumber, GLuint _index, GLchar* name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindFragmentShaderATI(GLuint id);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindFramebuffer(GLenum target, GLuint framebuffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindFramebufferEXT(GLenum target, GLuint framebuffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindFramebufferOES(GLenum target, GLuint framebuffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindImageTexture(GLuint unit, GLuint texture, GLint level, GLboolean layered, GLint layer, GLenum access, GLenum format);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindImageTextureEXT(GLuint _index, GLuint texture, GLint level, GLboolean layered, GLint layer, GLenum access, GLint format);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindImageTextures(GLuint first, GLsizei count, GLuint* textures);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLuint glBindLightParameterEXT(GLenum light, GLenum value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLuint glBindMaterialParameterEXT(GLenum face, GLenum value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindMultiTextureEXT(GLenum texunit, GLenum target, GLuint texture);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLuint glBindParameterEXT(GLenum value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindProgramARB(GLenum target, GLuint program);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindProgramNV(GLenum target, GLuint id);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindProgramPipeline(GLuint pipel_ine);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindProgramPipelineEXT(GLuint pipel_ine);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindRenderbuffer(GLenum target, GLuint renderbuffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindRenderbufferEXT(GLenum target, GLuint renderbuffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindRenderbufferOES(GLenum target, GLuint renderbuffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindSampler(GLuint unit, GLuint sampler);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindSamplers(GLuint first, GLsizei count, GLuint* samplers);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindShadingRateImageNV(GLuint texture);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLuint glBindTexGenParameterEXT(GLenum unit, GLenum coord, GLenum value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindTexture(GLenum target, GLuint texture);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindTextureEXT(GLenum target, GLuint texture);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindTextureUnit(GLuint unit, GLuint texture);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLuint glBindTextureUnitParameterEXT(GLenum unit, GLenum value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindTextures(GLuint first, GLsizei count, GLuint* textures);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindTransformFeedback(GLenum target, GLuint id);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindTransformFeedbackNV(GLenum target, GLuint id);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindVertexArray(GLuint array);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindVertexArrayAPPLE(GLuint array);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindVertexArrayOES(GLuint array);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindVertexBuffer(GLuint b_ind_ing_index, GLuint buffer, GLintptr offset, GLsizei stride);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindVertexBuffers(GLuint first, GLsizei count, GLuint* buffers, GLintptr* offsets, GLsizei* strides);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindVertexShaderEXT(GLuint id);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindVideoCaptureStreamBufferNV(GLuint video_capture_slot, GLuint stream, GLenum frame_region, GLintptrARB offset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBindVideoCaptureStreamTextureNV(GLuint video_capture_slot, GLuint stream, GLenum frame_region, GLenum target, GLuint texture);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBinormal3bEXT(GLbyte bx, GLbyte by, GLbyte bz);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBinormal3bvEXT(GLbyte* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBinormal3dEXT(GLdouble bx, GLdouble by, GLdouble bz);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBinormal3dvEXT(GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBinormal3fEXT(GLfloat bx, GLfloat by, GLfloat bz);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBinormal3fvEXT(GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBinormal3iEXT(GLint bx, GLint by, GLint bz);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBinormal3ivEXT(GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBinormal3sEXT(GLshort bx, GLshort by, GLshort bz);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBinormal3svEXT(GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBinormalPointerEXT(GLenum type, GLsizei stride, void* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBitmap(GLsizei width, GLsizei height, GLfloat xorig, GLfloat yorig, GLfloat xmove, GLfloat ymove, GLubyte* bitmap);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBitmapxOES(GLsizei width, GLsizei height, GLfixed xorig, GLfixed yorig, GLfixed xmove, GLfixed ymove, GLubyte* bitmap);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlendBarrier();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlendBarrierKHR();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlendBarrierNV();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlendColor(GLfloat red, GLfloat green, GLfloat blue, GLfloat alpha);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlendColorEXT(GLfloat red, GLfloat green, GLfloat blue, GLfloat alpha);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlendColorxOES(GLfixed red, GLfixed green, GLfixed blue, GLfixed alpha);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlendEquation(GLenum mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlendEquationEXT(GLenum mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlendEquationIndexedAMD(GLuint buf, GLenum mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlendEquationOES(GLenum mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlendEquationSeparate(GLenum modeRGB, GLenum modeAlpha);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlendEquationSeparateEXT(GLenum modeRGB, GLenum modeAlpha);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlendEquationSeparateIndexedAMD(GLuint buf, GLenum modeRGB, GLenum modeAlpha);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlendEquationSeparateOES(GLenum modeRGB, GLenum modeAlpha);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlendEquationSeparatei(GLuint buf, GLenum modeRGB, GLenum modeAlpha);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlendEquationSeparateiARB(GLuint buf, GLenum modeRGB, GLenum modeAlpha);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlendEquationSeparateiEXT(GLuint buf, GLenum modeRGB, GLenum modeAlpha);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlendEquationSeparateiOES(GLuint buf, GLenum modeRGB, GLenum modeAlpha);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlendEquationi(GLuint buf, GLenum mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlendEquationiARB(GLuint buf, GLenum mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlendEquationiEXT(GLuint buf, GLenum mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlendEquationiOES(GLuint buf, GLenum mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlendFunc(GLenum sfactor, GLenum dfactor);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlendFuncIndexedAMD(GLuint buf, GLenum src, GLenum dst);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlendFuncSeparate(GLenum sfactorRGB, GLenum dfactorRGB, GLenum sfactorAlpha, GLenum dfactorAlpha);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlendFuncSeparateEXT(GLenum sfactorRGB, GLenum dfactorRGB, GLenum sfactorAlpha, GLenum dfactorAlpha);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlendFuncSeparateINGR(GLenum sfactorRGB, GLenum dfactorRGB, GLenum sfactorAlpha, GLenum dfactorAlpha);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlendFuncSeparateIndexedAMD(GLuint buf, GLenum srcRGB, GLenum dstRGB, GLenum srcAlpha, GLenum dstAlpha);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlendFuncSeparateOES(GLenum srcRGB, GLenum dstRGB, GLenum srcAlpha, GLenum dstAlpha);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlendFuncSeparatei(GLuint buf, GLenum srcRGB, GLenum dstRGB, GLenum srcAlpha, GLenum dstAlpha);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlendFuncSeparateiARB(GLuint buf, GLenum srcRGB, GLenum dstRGB, GLenum srcAlpha, GLenum dstAlpha);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlendFuncSeparateiEXT(GLuint buf, GLenum srcRGB, GLenum dstRGB, GLenum srcAlpha, GLenum dstAlpha);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlendFuncSeparateiOES(GLuint buf, GLenum srcRGB, GLenum dstRGB, GLenum srcAlpha, GLenum dstAlpha);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlendFunci(GLuint buf, GLenum src, GLenum dst);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlendFunciARB(GLuint buf, GLenum src, GLenum dst);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlendFunciEXT(GLuint buf, GLenum src, GLenum dst);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlendFunciOES(GLuint buf, GLenum src, GLenum dst);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlendParameteriNV(GLenum pname, GLint value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlitFramebuffer(GLint srcX0, GLint srcY0, GLint srcX1, GLint srcY1, GLint dstX0, GLint dstY0, GLint dstX1, GLint dstY1, GLbitfield mask, GLenum filter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlitFramebufferANGLE(GLint srcX0, GLint srcY0, GLint srcX1, GLint srcY1, GLint dstX0, GLint dstY0, GLint dstX1, GLint dstY1, GLbitfield mask, GLenum filter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlitFramebufferEXT(GLint srcX0, GLint srcY0, GLint srcX1, GLint srcY1, GLint dstX0, GLint dstY0, GLint dstX1, GLint dstY1, GLbitfield mask, GLenum filter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlitFramebufferNV(GLint srcX0, GLint srcY0, GLint srcX1, GLint srcY1, GLint dstX0, GLint dstY0, GLint dstX1, GLint dstY1, GLbitfield mask, GLenum filter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBlitNamedFramebuffer(GLuint readFramebuffer, GLuint drawFramebuffer, GLint srcX0, GLint srcY0, GLint srcX1, GLint srcY1, GLint dstX0, GLint dstY0, GLint dstX1, GLint dstY1, GLbitfield mask, GLenum filter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBufferAddressRangeNV(GLenum pname, GLuint _index, GLuint64EXT address, GLsizeiptr length);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBufferAttachMemoryNV(GLenum target, GLuint memory, GLuint64 offset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBufferData(GLenum target, GLsizeiptr size, void* data, GLenum usage);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBufferDataARB(GLenum target, GLsizeiptrARB size, void* data, GLenum usage);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBufferPageCommitmentARB(GLenum target, GLintptr offset, GLsizeiptr size, GLboolean commit);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBufferPageCommitmentMemNV(GLenum target, GLintptr offset, GLsizeiptr size, GLuint memory, GLuint64 memOffset, GLboolean commit);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBufferParameteriAPPLE(GLenum target, GLenum pname, GLint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBufferStorage(GLenum target, GLsizeiptr size, void* data, GLbitfield flags);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBufferStorageEXT(GLenum target, GLsizeiptr size, void* data, GLbitfield flags);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBufferStorageExternalEXT(GLenum target, GLintptr offset, GLsizeiptr size, GLeglClientBufferEXT clientBuffer, GLbitfield flags);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBufferStorageMemEXT(GLenum target, GLsizeiptr size, GLuint memory, GLuint64 offset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBufferSubData(GLenum target, GLintptr offset, GLsizeiptr size, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glBufferSubDataARB(GLenum target, GLintptrARB offset, GLsizeiptrARB size, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCallCommandListNV(GLuint list);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCallList(GLuint list);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCallLists(GLsizei n, GLenum type, void* lists);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLenum glCheckFramebufferStatus(GLenum target);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLenum glCheckFramebufferStatusEXT(GLenum target);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLenum glCheckFramebufferStatusOES(GLenum target);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLenum glCheckNamedFramebufferStatus(GLuint framebuffer, GLenum target);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLenum glCheckNamedFramebufferStatusEXT(GLuint framebuffer, GLenum target);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClampColor(GLenum target, GLenum clamp);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClampColorARB(GLenum target, GLenum clamp);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClear(GLbitfield mask);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClearAccum(GLfloat red, GLfloat green, GLfloat blue, GLfloat alpha);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClearAccumxOES(GLfixed red, GLfixed green, GLfixed blue, GLfixed alpha);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClearBufferData(GLenum target, GLenum _internalformat, GLenum format, GLenum type, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClearBufferSubData(GLenum target, GLenum _internalformat, GLintptr offset, GLsizeiptr size, GLenum format, GLenum type, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClearBufferfi(GLenum buffer, GLint drawbuffer, GLfloat depth, GLint stencil);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClearBufferfv(GLenum buffer, GLint drawbuffer, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClearBufferiv(GLenum buffer, GLint drawbuffer, GLint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClearBufferuiv(GLenum buffer, GLint drawbuffer, GLuint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClearColor(GLfloat red, GLfloat green, GLfloat blue, GLfloat alpha);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClearColorIiEXT(GLint red, GLint green, GLint blue, GLint alpha);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClearColorIuiEXT(GLuint red, GLuint green, GLuint blue, GLuint alpha);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClearColorx(GLfixed red, GLfixed green, GLfixed blue, GLfixed alpha);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClearColorxOES(GLfixed red, GLfixed green, GLfixed blue, GLfixed alpha);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClearDepth(GLdouble depth);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClearDepthdNV(GLdouble depth);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClearDepthf(GLfloat d);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClearDepthfOES(GLclampf depth);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClearDepthx(GLfixed depth);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClearDepthxOES(GLfixed depth);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClearIndex(GLfloat c);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClearNamedBufferData(GLuint buffer, GLenum _internalformat, GLenum format, GLenum type, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClearNamedBufferDataEXT(GLuint buffer, GLenum _internalformat, GLenum format, GLenum type, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClearNamedBufferSubData(GLuint buffer, GLenum _internalformat, GLintptr offset, GLsizeiptr size, GLenum format, GLenum type, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClearNamedBufferSubDataEXT(GLuint buffer, GLenum _internalformat, GLsizeiptr offset, GLsizeiptr size, GLenum format, GLenum type, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClearNamedFramebufferfi(GLuint framebuffer, GLenum buffer, GLint drawbuffer, GLfloat depth, GLint stencil);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClearNamedFramebufferfv(GLuint framebuffer, GLenum buffer, GLint drawbuffer, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClearNamedFramebufferiv(GLuint framebuffer, GLenum buffer, GLint drawbuffer, GLint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClearNamedFramebufferuiv(GLuint framebuffer, GLenum buffer, GLint drawbuffer, GLuint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClearPixelLocalStorageuiEXT(GLsizei offset, GLsizei n, GLuint* values);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClearStencil(GLint s);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClearTexImage(GLuint texture, GLint level, GLenum format, GLenum type, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClearTexImageEXT(GLuint texture, GLint level, GLenum format, GLenum type, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClearTexSubImage(GLuint texture, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLsizei width, GLsizei height, GLsizei depth, GLenum format, GLenum type, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClearTexSubImageEXT(GLuint texture, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLsizei width, GLsizei height, GLsizei depth, GLenum format, GLenum type, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClientActiveTexture(GLenum texture);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClientActiveTextureARB(GLenum texture);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClientActiveVertexStreamATI(GLenum stream);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClientAttribDefaultEXT(GLbitfield mask);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClientWaitSemaphoreui64NVX(GLsizei fenceObjectCount, GLuint* semaphoreArray, GLuint64* fenceValueArray);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLenum glClientWaitSync(GLsync sync, GLbitfield flags, GLuint64 time_out);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLenum glClientWaitSyncAPPLE(GLsync sync, GLbitfield flags, GLuint64 time_out);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClipControl(GLenum orig_in, GLenum depth);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClipControlEXT(GLenum orig_in, GLenum depth);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClipPlane(GLenum plane, GLdouble* equation);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClipPlanef(GLenum p, GLfloat* eqn);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClipPlanefIMG(GLenum p, GLfloat* eqn);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClipPlanefOES(GLenum plane, GLfloat* equation);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClipPlanex(GLenum plane, GLfixed* equation);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClipPlanexIMG(GLenum p, GLfixed* eqn);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glClipPlanexOES(GLenum plane, GLfixed* equation);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor3b(GLbyte red, GLbyte green, GLbyte blue);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor3bv(GLbyte* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor3d(GLdouble red, GLdouble green, GLdouble blue);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor3dv(GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor3f(GLfloat red, GLfloat green, GLfloat blue);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor3fVertex3fSUN(GLfloat r, GLfloat g, GLfloat b, GLfloat x, GLfloat y, GLfloat z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor3fVertex3fvSUN(GLfloat* c, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor3fv(GLfloat* v);



    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor3i(GLint red, GLint green, GLint blue);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor3iv(GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor3s(GLshort red, GLshort green, GLshort blue);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor3sv(GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor3ub(GLubyte red, GLubyte green, GLubyte blue);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor3ubv(GLubyte* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor3ui(GLuint red, GLuint green, GLuint blue);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor3uiv(GLuint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor3us(GLushort red, GLushort green, GLushort blue);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor3usv(GLushort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor3xOES(GLfixed red, GLfixed green, GLfixed blue);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor3xvOES(GLfixed* components);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor4b(GLbyte red, GLbyte green, GLbyte blue, GLbyte alpha);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor4bv(GLbyte* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor4d(GLdouble red, GLdouble green, GLdouble blue, GLdouble alpha);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor4dv(GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor4f(GLfloat red, GLfloat green, GLfloat blue, GLfloat alpha);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor4fNormal3fVertex3fSUN(GLfloat r, GLfloat g, GLfloat b, GLfloat a, GLfloat nx, GLfloat ny, GLfloat nz, GLfloat x, GLfloat y, GLfloat z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor4fNormal3fVertex3fvSUN(GLfloat* c, GLfloat* n, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor4fv(GLfloat* v);



    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor4i(GLint red, GLint green, GLint blue, GLint alpha);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor4iv(GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor4s(GLshort red, GLshort green, GLshort blue, GLshort alpha);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor4sv(GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor4ub(GLubyte red, GLubyte green, GLubyte blue, GLubyte alpha);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor4ubVertex2fSUN(GLubyte r, GLubyte g, GLubyte b, GLubyte a, GLfloat x, GLfloat y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor4ubVertex2fvSUN(GLubyte* c, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor4ubVertex3fSUN(GLubyte r, GLubyte g, GLubyte b, GLubyte a, GLfloat x, GLfloat y, GLfloat z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor4ubVertex3fvSUN(GLubyte* c, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor4ubv(GLubyte* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor4ui(GLuint red, GLuint green, GLuint blue, GLuint alpha);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor4uiv(GLuint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor4us(GLushort red, GLushort green, GLushort blue, GLushort alpha);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor4usv(GLushort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor4x(GLfixed red, GLfixed green, GLfixed blue, GLfixed alpha);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor4xOES(GLfixed red, GLfixed green, GLfixed blue, GLfixed alpha);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColor4xvOES(GLfixed* components);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColorFormatNV(GLint size, GLenum type, GLsizei stride);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColorFragmentOp1ATI(GLenum op, GLuint dst, GLuint dstMask, GLuint dstMod, GLuint arg1, GLuint arg1Rep, GLuint arg1Mod);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColorFragmentOp2ATI(GLenum op, GLuint dst, GLuint dstMask, GLuint dstMod, GLuint arg1, GLuint arg1Rep, GLuint arg1Mod, GLuint arg2, GLuint arg2Rep, GLuint arg2Mod);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColorFragmentOp3ATI(GLenum op, GLuint dst, GLuint dstMask, GLuint dstMod, GLuint arg1, GLuint arg1Rep, GLuint arg1Mod, GLuint arg2, GLuint arg2Rep, GLuint arg2Mod, GLuint arg3, GLuint arg3Rep, GLuint arg3Mod);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColorMask(GLboolean red, GLboolean green, GLboolean blue, GLboolean alpha);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColorMaskIndexedEXT(GLuint _index, GLboolean r, GLboolean g, GLboolean b, GLboolean a);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColorMaski(GLuint _index, GLboolean r, GLboolean g, GLboolean b, GLboolean a);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColorMaskiEXT(GLuint _index, GLboolean r, GLboolean g, GLboolean b, GLboolean a);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColorMaskiOES(GLuint _index, GLboolean r, GLboolean g, GLboolean b, GLboolean a);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColorMaterial(GLenum face, GLenum mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColorP3ui(GLenum type, GLuint color);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColorP3uiv(GLenum type, GLuint* color);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColorP4ui(GLenum type, GLuint color);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColorP4uiv(GLenum type, GLuint* color);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColorPointer(GLint size, GLenum type, GLsizei stride, void* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColorPointerEXT(GLint size, GLenum type, GLsizei stride, GLsizei count, void* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColorPointerListIBM(GLint size, GLenum type, GLint stride, void** po_inter, GLint ptrstride);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColorPointervINTEL(GLint size, GLenum type, void** po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColorSubTable(GLenum target, GLsizei start, GLsizei count, GLenum format, GLenum type, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColorSubTableEXT(GLenum target, GLsizei start, GLsizei count, GLenum format, GLenum type, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColorTable(GLenum target, GLenum _internalformat, GLsizei width, GLenum format, GLenum type, void* table);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColorTableEXT(GLenum target, GLenum _internalFormat, GLsizei width, GLenum format, GLenum type, void* table);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColorTableParameterfv(GLenum target, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColorTableParameterfvSGI(GLenum target, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColorTableParameteriv(GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColorTableParameterivSGI(GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glColorTableSGI(GLenum target, GLenum _internalformat, GLsizei width, GLenum format, GLenum type, void* table);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCombinerInputNV(GLenum stage, GLenum portion, GLenum variable, GLenum _input, GLenum mapp_ing, GLenum componentUsage);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCombinerOutputNV(GLenum stage, GLenum portion, GLenum abOutput, GLenum cdOutput, GLenum sumOutput, GLenum scale, GLenum bias, GLboolean abDotProduct, GLboolean cdDotProduct, GLboolean muxSum);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCombinerParameterfNV(GLenum pname, GLfloat param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCombinerParameterfvNV(GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCombinerParameteriNV(GLenum pname, GLint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCombinerParameterivNV(GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCombinerStageParameterfvNV(GLenum stage, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCommandListSegmentsNV(GLuint list, GLuint segments);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCompileCommandListNV(GLuint list);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCompileShader(GLuint shader);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCompileShaderARB(GLhandleARB shaderObj);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCompileShaderIncludeARB(GLuint shader, GLsizei count, GLchar** path, GLint* length);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCompressedMultiTexImage1DEXT(GLenum texunit, GLenum target, GLint level, GLenum _internalformat, GLsizei width, GLint border, GLsizei imageSize, void* bits);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCompressedMultiTexImage2DEXT(GLenum texunit, GLenum target, GLint level, GLenum _internalformat, GLsizei width, GLsizei height, GLint border, GLsizei imageSize, void* bits);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCompressedMultiTexImage3DEXT(GLenum texunit, GLenum target, GLint level, GLenum _internalformat, GLsizei width, GLsizei height, GLsizei depth, GLint border, GLsizei imageSize, void* bits);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCompressedMultiTexSubImage1DEXT(GLenum texunit, GLenum target, GLint level, GLint xoffset, GLsizei width, GLenum format, GLsizei imageSize, void* bits);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCompressedMultiTexSubImage2DEXT(GLenum texunit, GLenum target, GLint level, GLint xoffset, GLint yoffset, GLsizei width, GLsizei height, GLenum format, GLsizei imageSize, void* bits);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCompressedMultiTexSubImage3DEXT(GLenum texunit, GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLsizei width, GLsizei height, GLsizei depth, GLenum format, GLsizei imageSize, void* bits);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCompressedTexImage1D(GLenum target, GLint level, GLenum _internalformat, GLsizei width, GLint border, GLsizei imageSize, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCompressedTexImage1DARB(GLenum target, GLint level, GLenum _internalformat, GLsizei width, GLint border, GLsizei imageSize, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCompressedTexImage2D(GLenum target, GLint level, GLenum _internalformat, GLsizei width, GLsizei height, GLint border, GLsizei imageSize, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCompressedTexImage2DARB(GLenum target, GLint level, GLenum _internalformat, GLsizei width, GLsizei height, GLint border, GLsizei imageSize, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCompressedTexImage3D(GLenum target, GLint level, GLenum _internalformat, GLsizei width, GLsizei height, GLsizei depth, GLint border, GLsizei imageSize, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCompressedTexImage3DARB(GLenum target, GLint level, GLenum _internalformat, GLsizei width, GLsizei height, GLsizei depth, GLint border, GLsizei imageSize, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCompressedTexImage3DOES(GLenum target, GLint level, GLenum _internalformat, GLsizei width, GLsizei height, GLsizei depth, GLint border, GLsizei imageSize, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCompressedTexSubImage1D(GLenum target, GLint level, GLint xoffset, GLsizei width, GLenum format, GLsizei imageSize, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCompressedTexSubImage1DARB(GLenum target, GLint level, GLint xoffset, GLsizei width, GLenum format, GLsizei imageSize, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCompressedTexSubImage2D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLsizei width, GLsizei height, GLenum format, GLsizei imageSize, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCompressedTexSubImage2DARB(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLsizei width, GLsizei height, GLenum format, GLsizei imageSize, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCompressedTexSubImage3D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLsizei width, GLsizei height, GLsizei depth, GLenum format, GLsizei imageSize, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCompressedTexSubImage3DARB(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLsizei width, GLsizei height, GLsizei depth, GLenum format, GLsizei imageSize, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCompressedTexSubImage3DOES(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLsizei width, GLsizei height, GLsizei depth, GLenum format, GLsizei imageSize, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCompressedTextureImage1DEXT(GLuint texture, GLenum target, GLint level, GLenum _internalformat, GLsizei width, GLint border, GLsizei imageSize, void* bits);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCompressedTextureImage2DEXT(GLuint texture, GLenum target, GLint level, GLenum _internalformat, GLsizei width, GLsizei height, GLint border, GLsizei imageSize, void* bits);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCompressedTextureImage3DEXT(GLuint texture, GLenum target, GLint level, GLenum _internalformat, GLsizei width, GLsizei height, GLsizei depth, GLint border, GLsizei imageSize, void* bits);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCompressedTextureSubImage1D(GLuint texture, GLint level, GLint xoffset, GLsizei width, GLenum format, GLsizei imageSize, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCompressedTextureSubImage1DEXT(GLuint texture, GLenum target, GLint level, GLint xoffset, GLsizei width, GLenum format, GLsizei imageSize, void* bits);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCompressedTextureSubImage2D(GLuint texture, GLint level, GLint xoffset, GLint yoffset, GLsizei width, GLsizei height, GLenum format, GLsizei imageSize, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCompressedTextureSubImage2DEXT(GLuint texture, GLenum target, GLint level, GLint xoffset, GLint yoffset, GLsizei width, GLsizei height, GLenum format, GLsizei imageSize, void* bits);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCompressedTextureSubImage3D(GLuint texture, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLsizei width, GLsizei height, GLsizei depth, GLenum format, GLsizei imageSize, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCompressedTextureSubImage3DEXT(GLuint texture, GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLsizei width, GLsizei height, GLsizei depth, GLenum format, GLsizei imageSize, void* bits);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glConservativeRasterParameterfNV(GLenum pname, GLfloat value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glConservativeRasterParameteriNV(GLenum pname, GLint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glConvolutionFilter1D(GLenum target, GLenum _internalformat, GLsizei width, GLenum format, GLenum type, void* image);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glConvolutionFilter1DEXT(GLenum target, GLenum _internalformat, GLsizei width, GLenum format, GLenum type, void* image);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glConvolutionFilter2D(GLenum target, GLenum _internalformat, GLsizei width, GLsizei height, GLenum format, GLenum type, void* image);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glConvolutionFilter2DEXT(GLenum target, GLenum _internalformat, GLsizei width, GLsizei height, GLenum format, GLenum type, void* image);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glConvolutionParameterf(GLenum target, GLenum pname, GLfloat parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glConvolutionParameterfEXT(GLenum target, GLenum pname, GLfloat parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glConvolutionParameterfv(GLenum target, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glConvolutionParameterfvEXT(GLenum target, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glConvolutionParameteri(GLenum target, GLenum pname, GLint parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glConvolutionParameteriEXT(GLenum target, GLenum pname, GLint parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glConvolutionParameteriv(GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glConvolutionParameterivEXT(GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glConvolutionParameterxOES(GLenum target, GLenum pname, GLfixed param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glConvolutionParameterxvOES(GLenum target, GLenum pname, GLfixed* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyBufferSubData(GLenum readTarget, GLenum writeTarget, GLintptr readOffset, GLintptr writeOffset, GLsizeiptr size);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyBufferSubDataNV(GLenum readTarget, GLenum writeTarget, GLintptr readOffset, GLintptr writeOffset, GLsizeiptr size);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyColorSubTable(GLenum target, GLsizei start, GLint x, GLint y, GLsizei width);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyColorSubTableEXT(GLenum target, GLsizei start, GLint x, GLint y, GLsizei width);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyColorTable(GLenum target, GLenum _internalformat, GLint x, GLint y, GLsizei width);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyColorTableSGI(GLenum target, GLenum _internalformat, GLint x, GLint y, GLsizei width);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyConvolutionFilter1D(GLenum target, GLenum _internalformat, GLint x, GLint y, GLsizei width);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyConvolutionFilter1DEXT(GLenum target, GLenum _internalformat, GLint x, GLint y, GLsizei width);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyConvolutionFilter2D(GLenum target, GLenum _internalformat, GLint x, GLint y, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyConvolutionFilter2DEXT(GLenum target, GLenum _internalformat, GLint x, GLint y, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyImageSubData(GLuint srcName, GLenum srcTarget, GLint srcLevel, GLint srcX, GLint srcY, GLint srcZ, GLuint dstName, GLenum dstTarget, GLint dstLevel, GLint dstX, GLint dstY, GLint dstZ, GLsizei srcWidth, GLsizei srcHeight, GLsizei srcDepth);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyImageSubDataEXT(GLuint srcName, GLenum srcTarget, GLint srcLevel, GLint srcX, GLint srcY, GLint srcZ, GLuint dstName, GLenum dstTarget, GLint dstLevel, GLint dstX, GLint dstY, GLint dstZ, GLsizei srcWidth, GLsizei srcHeight, GLsizei srcDepth);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyImageSubDataNV(GLuint srcName, GLenum srcTarget, GLint srcLevel, GLint srcX, GLint srcY, GLint srcZ, GLuint dstName, GLenum dstTarget, GLint dstLevel, GLint dstX, GLint dstY, GLint dstZ, GLsizei width, GLsizei height, GLsizei depth);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyImageSubDataOES(GLuint srcName, GLenum srcTarget, GLint srcLevel, GLint srcX, GLint srcY, GLint srcZ, GLuint dstName, GLenum dstTarget, GLint dstLevel, GLint dstX, GLint dstY, GLint dstZ, GLsizei srcWidth, GLsizei srcHeight, GLsizei srcDepth);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyMultiTexImage1DEXT(GLenum texunit, GLenum target, GLint level, GLenum _internalformat, GLint x, GLint y, GLsizei width, GLint border);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyMultiTexImage2DEXT(GLenum texunit, GLenum target, GLint level, GLenum _internalformat, GLint x, GLint y, GLsizei width, GLsizei height, GLint border);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyMultiTexSubImage1DEXT(GLenum texunit, GLenum target, GLint level, GLint xoffset, GLint x, GLint y, GLsizei width);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyMultiTexSubImage2DEXT(GLenum texunit, GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint x, GLint y, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyMultiTexSubImage3DEXT(GLenum texunit, GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLint x, GLint y, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyNamedBufferSubData(GLuint readBuffer, GLuint writeBuffer, GLintptr readOffset, GLintptr writeOffset, GLsizeiptr size);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyPathNV(GLuint resultPath, GLuint srcPath);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyPixels(GLint x, GLint y, GLsizei width, GLsizei height, GLenum type);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyTexImage1D(GLenum target, GLint level, GLenum _internalformat, GLint x, GLint y, GLsizei width, GLint border);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyTexImage1DEXT(GLenum target, GLint level, GLenum _internalformat, GLint x, GLint y, GLsizei width, GLint border);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyTexImage2D(GLenum target, GLint level, GLenum _internalformat, GLint x, GLint y, GLsizei width, GLsizei height, GLint border);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyTexImage2DEXT(GLenum target, GLint level, GLenum _internalformat, GLint x, GLint y, GLsizei width, GLsizei height, GLint border);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyTexSubImage1D(GLenum target, GLint level, GLint xoffset, GLint x, GLint y, GLsizei width);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyTexSubImage1DEXT(GLenum target, GLint level, GLint xoffset, GLint x, GLint y, GLsizei width);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyTexSubImage2D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint x, GLint y, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyTexSubImage2DEXT(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint x, GLint y, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyTexSubImage3D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLint x, GLint y, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyTexSubImage3DEXT(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLint x, GLint y, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyTexSubImage3DOES(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLint x, GLint y, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyTextureImage1DEXT(GLuint texture, GLenum target, GLint level, GLenum _internalformat, GLint x, GLint y, GLsizei width, GLint border);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyTextureImage2DEXT(GLuint texture, GLenum target, GLint level, GLenum _internalformat, GLint x, GLint y, GLsizei width, GLsizei height, GLint border);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyTextureLevelsAPPLE(GLuint dest_inationTexture, GLuint sourceTexture, GLint sourceBaseLevel, GLsizei sourceLevelCount);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyTextureSubImage1D(GLuint texture, GLint level, GLint xoffset, GLint x, GLint y, GLsizei width);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyTextureSubImage1DEXT(GLuint texture, GLenum target, GLint level, GLint xoffset, GLint x, GLint y, GLsizei width);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyTextureSubImage2D(GLuint texture, GLint level, GLint xoffset, GLint yoffset, GLint x, GLint y, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyTextureSubImage2DEXT(GLuint texture, GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint x, GLint y, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyTextureSubImage3D(GLuint texture, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLint x, GLint y, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCopyTextureSubImage3DEXT(GLuint texture, GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLint x, GLint y, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCoverFillPathInstancedNV(GLsizei numPaths, GLenum pathNameType, void* paths, GLuint pathBase, GLenum coverMode, GLenum transformType, GLfloat* transformValues);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCoverFillPathNV(GLuint path, GLenum coverMode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCoverStrokePathInstancedNV(GLsizei numPaths, GLenum pathNameType, void* paths, GLuint pathBase, GLenum coverMode, GLenum transformType, GLfloat* transformValues);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCoverStrokePathNV(GLuint path, GLenum coverMode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCoverageMaskNV(GLboolean mask);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCoverageModulationNV(GLenum components);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCoverageModulationTableNV(GLsizei n, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCoverageOperationNV(GLenum operation);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCreateBuffers(GLsizei n, GLuint* buffers);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCreateCommandListsNV(GLsizei n, GLuint* lists);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCreateFramebuffers(GLsizei n, GLuint* framebuffers);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCreateMemoryObjectsEXT(GLsizei n, GLuint* memoryObjects);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCreatePerfQueryINTEL(GLuint queryId, GLuint* queryHandle);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLuint glCreateProgram();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLhandleARB glCreateProgramObjectARB();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCreateProgramPipelines(GLsizei n, GLuint* pipel_ines);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLuint glCreateProgressFenceNVX();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCreateQueries(GLenum target, GLsizei n, GLuint* ids);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCreateRenderbuffers(GLsizei n, GLuint* renderbuffers);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCreateSamplers(GLsizei n, GLuint* samplers);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCreateSemaphoresNV(GLsizei n, GLuint* semaphores);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLuint glCreateShader(GLenum type);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLhandleARB glCreateShaderObjectARB(GLenum shaderType);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLuint glCreateShaderProgramEXT(GLenum type, GLchar* str);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLuint glCreateShaderProgramv(GLenum type, GLsizei count, GLchar** strs);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLuint glCreateShaderProgramvEXT(GLenum type, GLsizei count, GLchar** strs);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCreateStatesNV(GLsizei n, GLuint* states);


    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCreateTextures(GLenum target, GLsizei n, GLuint* textures);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCreateTransformFeedbacks(GLsizei n, GLuint* ids);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCreateVertexArrays(GLsizei n, GLuint* arrays);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCullFace(GLenum mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCullParameterdvEXT(GLenum pname, GLdouble* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCullParameterfvEXT(GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCurrentPaletteMatrixARB(GLint _index);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glCurrentPaletteMatrixOES(GLuint matrixpalette_index);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDebugMessageCallback(GLDEBUGPROC callback, void* userParam);




    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDebugMessageControl(GLenum source, GLenum type, GLenum severity, GLsizei count, GLuint* ids, GLboolean enabled);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDebugMessageControlARB(GLenum source, GLenum type, GLenum severity, GLsizei count, GLuint* ids, GLboolean enabled);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDebugMessageControlKHR(GLenum source, GLenum type, GLenum severity, GLsizei count, GLuint* ids, GLboolean enabled);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDebugMessageEnableAMD(GLenum category, GLenum severity, GLsizei count, GLuint* ids, GLboolean enabled);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDebugMessageInsert(GLenum source, GLenum type, GLuint id, GLenum severity, GLsizei length, GLchar* buf);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDebugMessageInsertAMD(GLenum category, GLenum severity, GLuint id, GLsizei length, GLchar* buf);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDebugMessageInsertARB(GLenum source, GLenum type, GLuint id, GLenum severity, GLsizei length, GLchar* buf);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDebugMessageInsertKHR(GLenum source, GLenum type, GLuint id, GLenum severity, GLsizei length, GLchar* buf);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeformSGIX(GLbitfield mask);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeformationMap3dSGIX(GLenum target, GLdouble u1, GLdouble u2, GLint ustride, GLint uorder, GLdouble v1, GLdouble v2, GLint vstride, GLint vorder, GLdouble w1, GLdouble w2, GLint wstride, GLint worder, GLdouble* po_ints);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeformationMap3fSGIX(GLenum target, GLfloat u1, GLfloat u2, GLint ustride, GLint uorder, GLfloat v1, GLfloat v2, GLint vstride, GLint vorder, GLfloat w1, GLfloat w2, GLint wstride, GLint worder, GLfloat* po_ints);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteAsyncMarkersSGIX(GLuint marker, GLsizei range);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteBuffers(GLsizei n, GLuint* buffers);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteBuffersARB(GLsizei n, GLuint* buffers);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteCommandListsNV(GLsizei n, GLuint* lists);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteFencesAPPLE(GLsizei n, GLuint* fences);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteFencesNV(GLsizei n, GLuint* fences);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteFragmentShaderATI(GLuint id);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteFramebuffers(GLsizei n, GLuint* framebuffers);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteFramebuffersEXT(GLsizei n, GLuint* framebuffers);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteFramebuffersOES(GLsizei n, GLuint* framebuffers);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteLists(GLuint list, GLsizei range);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteMemoryObjectsEXT(GLsizei n, GLuint* memoryObjects);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteNamedStringARB(GLint namelen, GLchar* name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteNamesAMD(GLenum identifier, GLuint num, GLuint* names);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteObjectARB(GLhandleARB obj);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteOcclusionQueriesNV(GLsizei n, GLuint* ids);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeletePathsNV(GLuint path, GLsizei range);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeletePerfMonitorsAMD(GLsizei n, GLuint* monitors);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeletePerfQueryINTEL(GLuint queryHandle);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteProgram(GLuint program);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteProgramPipelines(GLsizei n, GLuint* pipel_ines);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteProgramPipelinesEXT(GLsizei n, GLuint* pipel_ines);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteProgramsARB(GLsizei n, GLuint* programs);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteProgramsNV(GLsizei n, GLuint* programs);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteQueries(GLsizei n, GLuint* ids);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteQueriesARB(GLsizei n, GLuint* ids);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteQueriesEXT(GLsizei n, GLuint* ids);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteQueryResourceTagNV(GLsizei n, GLint* tagIds);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteRenderbuffers(GLsizei n, GLuint* renderbuffers);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteRenderbuffersEXT(GLsizei n, GLuint* renderbuffers);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteRenderbuffersOES(GLsizei n, GLuint* renderbuffers);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteSamplers(GLsizei count, GLuint* samplers);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteSemaphoresEXT(GLsizei n, GLuint* semaphores);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteShader(GLuint shader);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteStatesNV(GLsizei n, GLuint* states);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteSync(GLsync sync);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteSyncAPPLE(GLsync sync);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteTextures(GLsizei n, GLuint* textures);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteTexturesEXT(GLsizei n, GLuint* textures);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteTransformFeedbacks(GLsizei n, GLuint* ids);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteTransformFeedbacksNV(GLsizei n, GLuint* ids);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteVertexArrays(GLsizei n, GLuint* arrays);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteVertexArraysAPPLE(GLsizei n, GLuint* arrays);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteVertexArraysOES(GLsizei n, GLuint* arrays);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDeleteVertexShaderEXT(GLuint id);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDepthBoundsEXT(GLclampd zm_in, GLclampd zmax);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDepthBoundsdNV(GLdouble zm_in, GLdouble zmax);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDepthFunc(GLenum func);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDepthMask(GLboolean flag);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDepthRange(GLdouble n, GLdouble f);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDepthRangeArraydvNV(GLuint first, GLsizei count, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDepthRangeArrayfvNV(GLuint first, GLsizei count, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDepthRangeArrayfvOES(GLuint first, GLsizei count, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDepthRangeArrayv(GLuint first, GLsizei count, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDepthRangeIndexed(GLuint _index, GLdouble n, GLdouble f);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDepthRangeIndexeddNV(GLuint _index, GLdouble n, GLdouble f);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDepthRangeIndexedfNV(GLuint _index, GLfloat n, GLfloat f);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDepthRangeIndexedfOES(GLuint _index, GLfloat n, GLfloat f);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDepthRangedNV(GLdouble zNear, GLdouble zFar);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDepthRangef(GLfloat n, GLfloat f);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDepthRangefOES(GLclampf n, GLclampf f);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDepthRangex(GLfixed n, GLfixed f);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDepthRangexOES(GLfixed n, GLfixed f);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDetachObjectARB(GLhandleARB conta_inerObj, GLhandleARB attachedObj);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDetachShader(GLuint program, GLuint shader);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDetailTexFuncSGIS(GLenum target, GLsizei n, GLfloat* po_ints);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDisable(GLenum cap);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDisableClientState(GLenum array);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDisableClientStateIndexedEXT(GLenum array, GLuint _index);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDisableClientStateiEXT(GLenum array, GLuint _index);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDisableDriverControlQCOM(GLuint driverControl);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDisableIndexedEXT(GLenum target, GLuint _index);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDisableVariantClientStateEXT(GLuint id);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDisableVertexArrayAttrib(GLuint vaobj, GLuint _index);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDisableVertexArrayAttribEXT(GLuint vaobj, GLuint _index);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDisableVertexArrayEXT(GLuint vaobj, GLenum array);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDisableVertexAttribAPPLE(GLuint _index, GLenum pname);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDisableVertexAttribArray(GLuint _index);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDisableVertexAttribArrayARB(GLuint _index);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDisablei(GLenum target, GLuint _index);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDisableiEXT(GLenum target, GLuint _index);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDisableiNV(GLenum target, GLuint _index);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDisableiOES(GLenum target, GLuint _index);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDiscardFramebufferEXT(GLenum target, GLsizei numAttachments, GLenum* attachments);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDispatchCompute(GLuint num_groups_x, GLuint num_groups_y, GLuint num_groups_z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDispatchComputeGroupSizeARB(GLuint num_groups_x, GLuint num_groups_y, GLuint num_groups_z, GLuint group_size_x, GLuint group_size_y, GLuint group_size_z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDispatchComputeIndirect(GLintptr _indirect);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawArrays(GLenum mode, GLint first, GLsizei count);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawArraysEXT(GLenum mode, GLint first, GLsizei count);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawArraysIndirect(GLenum mode, void* _indirect);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawArraysInstanced(GLenum mode, GLint first, GLsizei count, GLsizei _instancecount);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawArraysInstancedANGLE(GLenum mode, GLint first, GLsizei count, GLsizei primcount);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawArraysInstancedARB(GLenum mode, GLint first, GLsizei count, GLsizei primcount);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawArraysInstancedBaseInstance(GLenum mode, GLint first, GLsizei count, GLsizei _instancecount, GLuint _base_instance);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawArraysInstancedBaseInstanceEXT(GLenum mode, GLint first, GLsizei count, GLsizei _instancecount, GLuint _base_instance);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawArraysInstancedEXT(GLenum mode, GLint start, GLsizei count, GLsizei primcount);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawArraysInstancedNV(GLenum mode, GLint first, GLsizei count, GLsizei primcount);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawBuffer(GLenum buf);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawBuffers(GLsizei n, GLenum* bufs);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawBuffersARB(GLsizei n, GLenum* bufs);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawBuffersATI(GLsizei n, GLenum* bufs);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawBuffersEXT(GLsizei n, GLenum* bufs);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawBuffersIndexedEXT(GLint n, GLenum* location, GLint* _indices);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawBuffersNV(GLsizei n, GLenum* bufs);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawCommandsAddressNV(GLenum primitiveMode, GLuint64* _indirects, GLsizei* sizes, GLuint count);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawCommandsNV(GLenum primitiveMode, GLuint buffer, GLintptr* _indirects, GLsizei* sizes, GLuint count);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawCommandsStatesAddressNV(GLuint64* _indirects, GLsizei* sizes, GLuint* states, GLuint* fbos, GLuint count);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawCommandsStatesNV(GLuint buffer, GLintptr* _indirects, GLsizei* sizes, GLuint* states, GLuint* fbos, GLuint count);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawElementArrayAPPLE(GLenum mode, GLint first, GLsizei count);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawElementArrayATI(GLenum mode, GLsizei count);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawElements(GLenum mode, GLsizei count, GLenum type, void* _indices);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawElementsBaseVertex(GLenum mode, GLsizei count, GLenum type, void* _indices, GLint _basevertex);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawElementsBaseVertexEXT(GLenum mode, GLsizei count, GLenum type, void* _indices, GLint _basevertex);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawElementsBaseVertexOES(GLenum mode, GLsizei count, GLenum type, void* _indices, GLint _basevertex);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawElementsIndirect(GLenum mode, GLenum type, void* _indirect);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawElementsInstanced(GLenum mode, GLsizei count, GLenum type, void* _indices, GLsizei _instancecount);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawElementsInstancedANGLE(GLenum mode, GLsizei count, GLenum type, void* _indices, GLsizei primcount);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawElementsInstancedARB(GLenum mode, GLsizei count, GLenum type, void* _indices, GLsizei primcount);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawElementsInstancedBaseInstance(GLenum mode, GLsizei count, GLenum type, void* _indices, GLsizei _instancecount, GLuint _base_instance);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawElementsInstancedBaseInstanceEXT(GLenum mode, GLsizei count, GLenum type, void* _indices, GLsizei _instancecount, GLuint _base_instance);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawElementsInstancedBaseVertex(GLenum mode, GLsizei count, GLenum type, void* _indices, GLsizei _instancecount, GLint _basevertex);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawElementsInstancedBaseVertexBaseInstance(GLenum mode, GLsizei count, GLenum type, void* _indices, GLsizei _instancecount, GLint _basevertex, GLuint _base_instance);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawElementsInstancedBaseVertexBaseInstanceEXT(GLenum mode, GLsizei count, GLenum type, void* _indices, GLsizei _instancecount, GLint _basevertex, GLuint _base_instance);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawElementsInstancedBaseVertexEXT(GLenum mode, GLsizei count, GLenum type, void* _indices, GLsizei _instancecount, GLint _basevertex);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawElementsInstancedBaseVertexOES(GLenum mode, GLsizei count, GLenum type, void* _indices, GLsizei _instancecount, GLint _basevertex);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawElementsInstancedEXT(GLenum mode, GLsizei count, GLenum type, void* _indices, GLsizei primcount);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawElementsInstancedNV(GLenum mode, GLsizei count, GLenum type, void* _indices, GLsizei primcount);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawMeshArraysSUN(GLenum mode, GLint first, GLsizei count, GLsizei width);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawMeshTasksNV(GLuint first, GLuint count);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawMeshTasksIndirectNV(GLintptr _indirect);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawPixels(GLsizei width, GLsizei height, GLenum format, GLenum type, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawRangeElementArrayAPPLE(GLenum mode, GLuint start, GLuint end, GLint first, GLsizei count);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawRangeElementArrayATI(GLenum mode, GLuint start, GLuint end, GLsizei count);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawRangeElements(GLenum mode, GLuint start, GLuint end, GLsizei count, GLenum type, void* _indices);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawRangeElementsBaseVertex(GLenum mode, GLuint start, GLuint end, GLsizei count, GLenum type, void* _indices, GLint _basevertex);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawRangeElementsBaseVertexEXT(GLenum mode, GLuint start, GLuint end, GLsizei count, GLenum type, void* _indices, GLint _basevertex);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawRangeElementsBaseVertexOES(GLenum mode, GLuint start, GLuint end, GLsizei count, GLenum type, void* _indices, GLint _basevertex);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawRangeElementsEXT(GLenum mode, GLuint start, GLuint end, GLsizei count, GLenum type, void* _indices);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawTexfOES(GLfloat x, GLfloat y, GLfloat z, GLfloat width, GLfloat height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawTexfvOES(GLfloat* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawTexiOES(GLint x, GLint y, GLint z, GLint width, GLint height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawTexivOES(GLint* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawTexsOES(GLshort x, GLshort y, GLshort z, GLshort width, GLshort height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawTexsvOES(GLshort* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawTextureNV(GLuint texture, GLuint sampler, GLfloat x0, GLfloat y0, GLfloat x1, GLfloat y1, GLfloat z, GLfloat s0, GLfloat t0, GLfloat s1, GLfloat t1);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawTexxOES(GLfixed x, GLfixed y, GLfixed z, GLfixed width, GLfixed height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawTexxvOES(GLfixed* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawTransformFeedback(GLenum mode, GLuint id);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawTransformFeedbackEXT(GLenum mode, GLuint id);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawTransformFeedbackInstanced(GLenum mode, GLuint id, GLsizei _instancecount);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawTransformFeedbackInstancedEXT(GLenum mode, GLuint id, GLsizei _instancecount);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawTransformFeedbackNV(GLenum mode, GLuint id);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawTransformFeedbackStream(GLenum mode, GLuint id, GLuint stream);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawTransformFeedbackStreamInstanced(GLenum mode, GLuint id, GLuint stream, GLsizei _instancecount);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEGLImageTargetRenderbufferStorageOES(GLenum target, GLeglImageOES image);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEGLImageTargetTexStorageEXT(GLenum target, GLeglImageOES image, GLint* attrib_list);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEGLImageTargetTexture2DOES(GLenum target, GLeglImageOES image);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEGLImageTargetTextureStorageEXT(GLuint texture, GLeglImageOES image, GLint* attrib_list);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEdgeFlag(GLboolean flag);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEdgeFlagFormatNV(GLsizei stride);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEdgeFlagPointer(GLsizei stride, void* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEdgeFlagPointerEXT(GLsizei stride, GLsizei count, GLboolean* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEdgeFlagPointerListIBM(GLint stride, GLboolean** po_inter, GLint ptrstride);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEdgeFlagv(GLboolean* flag);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glElementPointerAPPLE(GLenum type, void* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glElementPointerATI(GLenum type, void* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEnable(GLenum cap);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEnableClientState(GLenum array);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEnableClientStateIndexedEXT(GLenum array, GLuint _index);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEnableClientStateiEXT(GLenum array, GLuint _index);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEnableDriverControlQCOM(GLuint driverControl);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEnableIndexedEXT(GLenum target, GLuint _index);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEnableVariantClientStateEXT(GLuint id);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEnableVertexArrayAttrib(GLuint vaobj, GLuint _index);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEnableVertexArrayAttribEXT(GLuint vaobj, GLuint _index);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEnableVertexArrayEXT(GLuint vaobj, GLenum array);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEnableVertexAttribAPPLE(GLuint _index, GLenum pname);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEnableVertexAttribArray(GLuint _index);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEnableVertexAttribArrayARB(GLuint _index);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEnablei(GLenum target, GLuint _index);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEnableiEXT(GLenum target, GLuint _index);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEnableiNV(GLenum target, GLuint _index);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEnableiOES(GLenum target, GLuint _index);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEnd();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEndConditionalRender();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEndConditionalRenderNV();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEndConditionalRenderNVX();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEndFragmentShaderATI();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEndList();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEndOcclusionQueryNV();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEndPerfMonitorAMD(GLuint monitor);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEndPerfQueryINTEL(GLuint queryHandle);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEndQuery(GLenum target);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEndQueryARB(GLenum target);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEndQueryEXT(GLenum target);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEndQueryIndexed(GLenum target, GLuint _index);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEndTilingQCOM(GLbitfield preserveMask);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEndTransformFeedback();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEndTransformFeedbackEXT();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEndTransformFeedbackNV();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEndVertexShaderEXT();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEndVideoCaptureNV(GLuint video_capture_slot);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEvalCoord1d(GLdouble u);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEvalCoord1dv(GLdouble* u);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEvalCoord1f(GLfloat u);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEvalCoord1fv(GLfloat* u);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEvalCoord1xOES(GLfixed u);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEvalCoord1xvOES(GLfixed* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEvalCoord2d(GLdouble u, GLdouble v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEvalCoord2dv(GLdouble* u);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEvalCoord2f(GLfloat u, GLfloat v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEvalCoord2fv(GLfloat* u);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEvalCoord2xOES(GLfixed u, GLfixed v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEvalCoord2xvOES(GLfixed* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEvalMapsNV(GLenum target, GLenum mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEvalMesh1(GLenum mode, GLint i1, GLint i2);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEvalMesh2(GLenum mode, GLint i1, GLint i2, GLint j1, GLint j2);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEvalPoint1(GLint i);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEvalPoint2(GLint i, GLint j);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glEvaluateDepthValuesARB();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glExecuteProgramNV(GLenum target, GLuint id, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glExtGetBufferPointervQCOM(GLenum target, void** parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glExtGetBuffersQCOM(GLuint* buffers, GLint maxBuffers, GLint* numBuffers);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glExtGetFramebuffersQCOM(GLuint* framebuffers, GLint maxFramebuffers, GLint* numFramebuffers);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glExtGetProgramBinarySourceQCOM(GLuint program, GLenum shadertype, GLchar* source, GLint* length);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glExtGetProgramsQCOM(GLuint* programs, GLint maxPrograms, GLint* numPrograms);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glExtGetRenderbuffersQCOM(GLuint* renderbuffers, GLint maxRenderbuffers, GLint* numRenderbuffers);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glExtGetShadersQCOM(GLuint* shaders, GLint maxShaders, GLint* numShaders);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glExtGetTexLevelParameterivQCOM(GLuint texture, GLenum face, GLint level, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glExtGetTexSubImageQCOM(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLsizei width, GLsizei height, GLsizei depth, GLenum format, GLenum type, void* texels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glExtGetTexturesQCOM(GLuint* textures, GLint maxTextures, GLint* numTextures);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glExtIsProgramBinaryQCOM(GLuint program);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glExtTexObjectStateOverrideiQCOM(GLenum target, GLenum pname, GLint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glExtractComponentEXT(GLuint res, GLuint src, GLuint num);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFeedbackBuffer(GLsizei size, GLenum type, GLfloat* buffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFeedbackBufferxOES(GLsizei n, GLenum type, GLfixed* buffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLsync glFenceSync(GLenum condition, GLbitfield flags);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLsync glFenceSyncAPPLE(GLenum condition, GLbitfield flags);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFinalCombinerInputNV(GLenum variable, GLenum _input, GLenum mapp_ing, GLenum componentUsage);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFinish();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLint glFinishAsyncSGIX(GLuint* markerp);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFinishFenceAPPLE(GLuint fence);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFinishFenceNV(GLuint fence);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFinishObjectAPPLE(GLenum obj, GLint name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFinishTextureSUNX();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFlush();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFlushMappedBufferRange(GLenum target, GLintptr offset, GLsizeiptr length);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFlushMappedBufferRangeAPPLE(GLenum target, GLintptr offset, GLsizeiptr size);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFlushMappedBufferRangeEXT(GLenum target, GLintptr offset, GLsizeiptr length);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFlushMappedNamedBufferRange(GLuint buffer, GLintptr offset, GLsizeiptr length);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFlushMappedNamedBufferRangeEXT(GLuint buffer, GLintptr offset, GLsizeiptr length);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFlushPixelDataRangeNV(GLenum target);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFlushRasterSGIX();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFlushStaticDataIBM(GLenum target);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFlushVertexArrayRangeAPPLE(GLsizei length, void* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFlushVertexArrayRangeNV();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFogCoordFormatNV(GLenum type, GLsizei stride);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFogCoordPointer(GLenum type, GLsizei stride, void* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFogCoordPointerEXT(GLenum type, GLsizei stride, void* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFogCoordPointerListIBM(GLenum type, GLint stride, void** po_inter, GLint ptrstride);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFogCoordd(GLdouble coord);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFogCoorddEXT(GLdouble coord);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFogCoorddv(GLdouble* coord);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFogCoorddvEXT(GLdouble* coord);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFogCoordf(GLfloat coord);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFogCoordfEXT(GLfloat coord);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFogCoordfv(GLfloat* coord);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFogCoordfvEXT(GLfloat* coord);



    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFogFuncSGIS(GLsizei n, GLfloat* po_ints);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFogf(GLenum pname, GLfloat param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFogfv(GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFogi(GLenum pname, GLint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFogiv(GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFogx(GLenum pname, GLfixed param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFogxOES(GLenum pname, GLfixed param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFogxv(GLenum pname, GLfixed* param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFogxvOES(GLenum pname, GLfixed* param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFragmentColorMaterialSGIX(GLenum face, GLenum mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFragmentCoverageColorNV(GLuint color);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFragmentLightModelfSGIX(GLenum pname, GLfloat param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFragmentLightModelfvSGIX(GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFragmentLightModeliSGIX(GLenum pname, GLint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFragmentLightModelivSGIX(GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFragmentLightfSGIX(GLenum light, GLenum pname, GLfloat param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFragmentLightfvSGIX(GLenum light, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFragmentLightiSGIX(GLenum light, GLenum pname, GLint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFragmentLightivSGIX(GLenum light, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFragmentMaterialfSGIX(GLenum face, GLenum pname, GLfloat param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFragmentMaterialfvSGIX(GLenum face, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFragmentMaterialiSGIX(GLenum face, GLenum pname, GLint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFragmentMaterialivSGIX(GLenum face, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFrameTerminatorGREMEDY();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFrameZoomSGIX(GLint factor);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferDrawBufferEXT(GLuint framebuffer, GLenum mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferDrawBuffersEXT(GLuint framebuffer, GLsizei n, GLenum* bufs);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferFetchBarrierEXT();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferFetchBarrierQCOM();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferFoveationConfigQCOM(GLuint framebuffer, GLuint numLayers, GLuint focalPo_intsPerLayer, GLuint requestedFeatures, GLuint* providedFeatures);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferFoveationParametersQCOM(GLuint framebuffer, GLuint layer, GLuint focalPo_int, GLfloat focalX, GLfloat focalY, GLfloat ga_inX, GLfloat ga_inY, GLfloat foveaArea);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferParameteri(GLenum target, GLenum pname, GLint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferPixelLocalStorageSizeEXT(GLuint target, GLsizei size);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferReadBufferEXT(GLuint framebuffer, GLenum mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferRenderbuffer(GLenum target, GLenum attachment, GLenum renderbuffertarget, GLuint renderbuffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferRenderbufferEXT(GLenum target, GLenum attachment, GLenum renderbuffertarget, GLuint renderbuffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferRenderbufferOES(GLenum target, GLenum attachment, GLenum renderbuffertarget, GLuint renderbuffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferSampleLocationsfvARB(GLenum target, GLuint start, GLsizei count, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferSampleLocationsfvNV(GLenum target, GLuint start, GLsizei count, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferSamplePositionsfvAMD(GLenum target, GLuint numsamples, GLuint pixel_index, GLfloat* values);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferShadingRateEXT(GLenum target, GLenum attachment, GLuint texture, GLint _baseLayer, GLsizei numLayers, GLsizei texelWidth, GLsizei texelHeight);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferTexture(GLenum target, GLenum attachment, GLuint texture, GLint level);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferTexture1D(GLenum target, GLenum attachment, GLenum textarget, GLuint texture, GLint level);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferTexture1DEXT(GLenum target, GLenum attachment, GLenum textarget, GLuint texture, GLint level);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferTexture2D(GLenum target, GLenum attachment, GLenum textarget, GLuint texture, GLint level);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferTexture2DEXT(GLenum target, GLenum attachment, GLenum textarget, GLuint texture, GLint level);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferTexture2DDownsampleIMG(GLenum target, GLenum attachment, GLenum textarget, GLuint texture, GLint level, GLint xscale, GLint yscale);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferTexture2DMultisampleEXT(GLenum target, GLenum attachment, GLenum textarget, GLuint texture, GLint level, GLsizei samples);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferTexture2DMultisampleIMG(GLenum target, GLenum attachment, GLenum textarget, GLuint texture, GLint level, GLsizei samples);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferTexture2DOES(GLenum target, GLenum attachment, GLenum textarget, GLuint texture, GLint level);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferTexture3D(GLenum target, GLenum attachment, GLenum textarget, GLuint texture, GLint level, GLint zoffset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferTexture3DEXT(GLenum target, GLenum attachment, GLenum textarget, GLuint texture, GLint level, GLint zoffset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferTexture3DOES(GLenum target, GLenum attachment, GLenum textarget, GLuint texture, GLint level, GLint zoffset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferTextureARB(GLenum target, GLenum attachment, GLuint texture, GLint level);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferTextureEXT(GLenum target, GLenum attachment, GLuint texture, GLint level);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferTextureFaceARB(GLenum target, GLenum attachment, GLuint texture, GLint level, GLenum face);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferTextureFaceEXT(GLenum target, GLenum attachment, GLuint texture, GLint level, GLenum face);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferTextureLayer(GLenum target, GLenum attachment, GLuint texture, GLint level, GLint layer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferTextureLayerARB(GLenum target, GLenum attachment, GLuint texture, GLint level, GLint layer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferTextureLayerEXT(GLenum target, GLenum attachment, GLuint texture, GLint level, GLint layer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferTextureLayerDownsampleIMG(GLenum target, GLenum attachment, GLuint texture, GLint level, GLint layer, GLint xscale, GLint yscale);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferTextureMultisampleMultiviewOVR(GLenum target, GLenum attachment, GLuint texture, GLint level, GLsizei samples, GLint _baseViewIndex, GLsizei numViews);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferTextureMultiviewOVR(GLenum target, GLenum attachment, GLuint texture, GLint level, GLint _baseViewIndex, GLsizei numViews);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferTextureOES(GLenum target, GLenum attachment, GLuint texture, GLint level);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFreeObjectBufferATI(GLuint buffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFrontFace(GLenum mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFrustum(GLdouble left, GLdouble right, GLdouble bottom, GLdouble top, GLdouble zNear, GLdouble zFar);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFrustumf(GLfloat l, GLfloat r, GLfloat b, GLfloat t, GLfloat n, GLfloat f);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFrustumfOES(GLfloat l, GLfloat r, GLfloat b, GLfloat t, GLfloat n, GLfloat f);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFrustumx(GLfixed l, GLfixed r, GLfixed b, GLfixed t, GLfixed n, GLfixed f);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFrustumxOES(GLfixed l, GLfixed r, GLfixed b, GLfixed t, GLfixed n, GLfixed f);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLuint glGenAsyncMarkersSGIX(GLsizei range);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGenBuffers(GLsizei n, GLuint* buffers);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGenBuffersARB(GLsizei n, GLuint* buffers);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGenFencesAPPLE(GLsizei n, GLuint* fences);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGenFencesNV(GLsizei n, GLuint* fences);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLuint glGenFragmentShadersATI(GLuint range);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGenFramebuffers(GLsizei n, GLuint* framebuffers);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGenFramebuffersEXT(GLsizei n, GLuint* framebuffers);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGenFramebuffersOES(GLsizei n, GLuint* framebuffers);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLuint glGenLists(GLsizei range);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGenNamesAMD(GLenum identifier, GLuint num, GLuint* names);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGenOcclusionQueriesNV(GLsizei n, GLuint* ids);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLuint glGenPathsNV(GLsizei range);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGenPerfMonitorsAMD(GLsizei n, GLuint* monitors);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGenProgramPipelines(GLsizei n, GLuint* pipel_ines);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGenProgramPipelinesEXT(GLsizei n, GLuint* pipel_ines);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGenProgramsARB(GLsizei n, GLuint* programs);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGenProgramsNV(GLsizei n, GLuint* programs);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGenQueries(GLsizei n, GLuint* ids);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGenQueriesARB(GLsizei n, GLuint* ids);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGenQueriesEXT(GLsizei n, GLuint* ids);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGenQueryResourceTagNV(GLsizei n, GLint* tagIds);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGenRenderbuffers(GLsizei n, GLuint* renderbuffers);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGenRenderbuffersEXT(GLsizei n, GLuint* renderbuffers);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGenRenderbuffersOES(GLsizei n, GLuint* renderbuffers);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGenSamplers(GLsizei count, GLuint* samplers);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGenSemaphoresEXT(GLsizei n, GLuint* semaphores);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLuint glGenSymbolsEXT(GLenum datatype, GLenum storagetype, GLenum range, GLuint components);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGenTextures(GLsizei n, GLuint* textures);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGenTexturesEXT(GLsizei n, GLuint* textures);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGenTransformFeedbacks(GLsizei n, GLuint* ids);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGenTransformFeedbacksNV(GLsizei n, GLuint* ids);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGenVertexArrays(GLsizei n, GLuint* arrays);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGenVertexArraysAPPLE(GLsizei n, GLuint* arrays);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGenVertexArraysOES(GLsizei n, GLuint* arrays);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLuint glGenVertexShadersEXT(GLuint range);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGenerateMipmap(GLenum target);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGenerateMipmapEXT(GLenum target);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGenerateMipmapOES(GLenum target);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGenerateMultiTexMipmapEXT(GLenum texunit, GLenum target);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGenerateTextureMipmap(GLuint texture);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGenerateTextureMipmapEXT(GLuint texture, GLenum target);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetActiveAtomicCounterBufferiv(GLuint program, GLuint bufferIndex, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetActiveAttrib(GLuint program, GLuint _index, GLsizei bufSize, GLsizei* length, GLint* size, GLenum* type, GLchar* name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetActiveAttribARB(GLhandleARB programObj, GLuint _index, GLsizei maxLength, GLsizei* length, GLint* size, GLenum* type, GLcharARB* name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetActiveSubroutineName(GLuint program, GLenum shadertype, GLuint _index, GLsizei bufSize, GLsizei* length, GLchar* name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetActiveSubroutineUniformName(GLuint program, GLenum shadertype, GLuint _index, GLsizei bufSize, GLsizei* length, GLchar* name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetActiveSubroutineUniformiv(GLuint program, GLenum shadertype, GLuint _index, GLenum pname, GLint* values);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetActiveUniform(GLuint program, GLuint _index, GLsizei bufSize, GLsizei* length, GLint* size, GLenum* type, GLchar* name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetActiveUniformARB(GLhandleARB programObj, GLuint _index, GLsizei maxLength, GLsizei* length, GLint* size, GLenum* type, GLcharARB* name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetActiveUniformBlockName(GLuint program, GLuint uniformBlockIndex, GLsizei bufSize, GLsizei* length, GLchar* uniformBlockName);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetActiveUniformBlockiv(GLuint program, GLuint uniformBlockIndex, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetActiveUniformName(GLuint program, GLuint uniformIndex, GLsizei bufSize, GLsizei* length, GLchar* uniformName);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetActiveUniformsiv(GLuint program, GLsizei uniformCount, GLuint* uniformIndices, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetActiveVaryingNV(GLuint program, GLuint _index, GLsizei bufSize, GLsizei* length, GLsizei* size, GLenum* type, GLchar* name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetArrayObjectfvATI(GLenum array, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetArrayObjectivATI(GLenum array, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetAttachedObjectsARB(GLhandleARB conta_inerObj, GLsizei maxCount, GLsizei* count, GLhandleARB* obj);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetAttachedShaders(GLuint program, GLsizei maxCount, GLsizei* count, GLuint* shaders);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLint glGetAttribLocation(GLuint program, GLchar* name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLint glGetAttribLocationARB(GLhandleARB programObj, GLcharARB* name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetBooleanIndexedvEXT(GLenum target, GLuint _index, GLboolean* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetBooleani_v(GLenum target, GLuint _index, GLboolean* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetBooleanv(GLenum pname, GLboolean* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetBufferParameteri64v(GLenum target, GLenum pname, GLint64* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetBufferParameteriv(GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetBufferParameterivARB(GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetBufferParameterui64vNV(GLenum target, GLenum pname, GLuint64EXT* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetBufferPointerv(GLenum target, GLenum pname, void** parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetBufferPointervARB(GLenum target, GLenum pname, void** parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetBufferPointervOES(GLenum target, GLenum pname, void** parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetBufferSubData(GLenum target, GLintptr offset, GLsizeiptr size, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetBufferSubDataARB(GLenum target, GLintptrARB offset, GLsizeiptrARB size, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetClipPlane(GLenum plane, GLdouble* equation);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetClipPlanef(GLenum plane, GLfloat* equation);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetClipPlanefOES(GLenum plane, GLfloat* equation);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetClipPlanex(GLenum plane, GLfixed* equation);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetClipPlanexOES(GLenum plane, GLfixed* equation);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetColorTable(GLenum target, GLenum format, GLenum type, void* table);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetColorTableEXT(GLenum target, GLenum format, GLenum type, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetColorTableParameterfv(GLenum target, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetColorTableParameterfvEXT(GLenum target, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetColorTableParameterfvSGI(GLenum target, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetColorTableParameteriv(GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetColorTableParameterivEXT(GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetColorTableParameterivSGI(GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetColorTableSGI(GLenum target, GLenum format, GLenum type, void* table);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetCombinerInputParameterfvNV(GLenum stage, GLenum portion, GLenum variable, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetCombinerInputParameterivNV(GLenum stage, GLenum portion, GLenum variable, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetCombinerOutputParameterfvNV(GLenum stage, GLenum portion, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetCombinerOutputParameterivNV(GLenum stage, GLenum portion, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetCombinerStageParameterfvNV(GLenum stage, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLuint glGetCommandHeaderNV(GLenum tokenID, GLuint size);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetCompressedMultiTexImageEXT(GLenum texunit, GLenum target, GLint lod, void* img);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetCompressedTexImage(GLenum target, GLint level, void* img);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetCompressedTexImageARB(GLenum target, GLint level, void* img);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetCompressedTextureImage(GLuint texture, GLint level, GLsizei bufSize, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetCompressedTextureImageEXT(GLuint texture, GLenum target, GLint lod, void* img);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetCompressedTextureSubImage(GLuint texture, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLsizei width, GLsizei height, GLsizei depth, GLsizei bufSize, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetConvolutionFilter(GLenum target, GLenum format, GLenum type, void* image);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetConvolutionFilterEXT(GLenum target, GLenum format, GLenum type, void* image);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetConvolutionParameterfv(GLenum target, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetConvolutionParameterfvEXT(GLenum target, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetConvolutionParameteriv(GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetConvolutionParameterivEXT(GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetConvolutionParameterxvOES(GLenum target, GLenum pname, GLfixed* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetCoverageModulationTableNV(GLsizei bufSize, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLuint glGetDebugMessageLog(GLuint count, GLsizei bufSize, GLenum* sources, GLenum* types, GLuint* ids, GLenum* severities, GLsizei* lengths, GLchar* messageLog);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLuint glGetDebugMessageLogAMD(GLuint count, GLsizei bufSize, GLenum* categories, GLenum* severities, GLuint* ids, GLsizei* lengths, GLchar* message);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLuint glGetDebugMessageLogARB(GLuint count, GLsizei bufSize, GLenum* sources, GLenum* types, GLuint* ids, GLenum* severities, GLsizei* lengths, GLchar* messageLog);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLuint glGetDebugMessageLogKHR(GLuint count, GLsizei bufSize, GLenum* sources, GLenum* types, GLuint* ids, GLenum* severities, GLsizei* lengths, GLchar* messageLog);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetDetailTexFuncSGIS(GLenum target, GLfloat* po_ints);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetDoubleIndexedvEXT(GLenum target, GLuint _index, GLdouble* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetDoublei_v(GLenum target, GLuint _index, GLdouble* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetDoublei_vEXT(GLenum pname, GLuint _index, GLdouble* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetDoublev(GLenum pname, GLdouble* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetDriverControlStringQCOM(GLuint driverControl, GLsizei bufSize, GLsizei* length, GLchar* driverControlStr_ing);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetDriverControlsQCOM(GLint* num, GLsizei size, GLuint* driverControls);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLenum glGetError();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetFenceivNV(GLuint fence, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetFinalCombinerInputParameterfvNV(GLenum variable, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetFinalCombinerInputParameterivNV(GLenum variable, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetFirstPerfQueryIdINTEL(GLuint* queryId);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetFixedv(GLenum pname, GLfixed* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetFixedvOES(GLenum pname, GLfixed* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetFloatIndexedvEXT(GLenum target, GLuint _index, GLfloat* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetFloati_v(GLenum target, GLuint _index, GLfloat* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetFloati_vEXT(GLenum pname, GLuint _index, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetFloati_vNV(GLenum target, GLuint _index, GLfloat* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetFloati_vOES(GLenum target, GLuint _index, GLfloat* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetFloatv(GLenum pname, GLfloat* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetFogFuncSGIS(GLfloat* po_ints);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLint glGetFragDataIndex(GLuint program, GLchar* name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLint glGetFragDataIndexEXT(GLuint program, GLchar* name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLint glGetFragDataLocation(GLuint program, GLchar* name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLint glGetFragDataLocationEXT(GLuint program, GLchar* name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetFragmentLightfvSGIX(GLenum light, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetFragmentLightivSGIX(GLenum light, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetFragmentMaterialfvSGIX(GLenum face, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetFragmentMaterialivSGIX(GLenum face, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetFragmentShadingRatesEXT(GLsizei samples, GLsizei maxCount, GLsizei* count, GLenum* shad_ingRates);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetFramebufferAttachmentParameteriv(GLenum target, GLenum attachment, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetFramebufferAttachmentParameterivEXT(GLenum target, GLenum attachment, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetFramebufferAttachmentParameterivOES(GLenum target, GLenum attachment, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetFramebufferParameterfvAMD(GLenum target, GLenum pname, GLuint numsamples, GLuint pixel_index, GLsizei size, GLfloat* values);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetFramebufferParameteriv(GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetFramebufferParameterivEXT(GLuint framebuffer, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLsizei glGetFramebufferPixelLocalStorageSizeEXT(GLuint target);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLenum glGetGraphicsResetStatus();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLenum glGetGraphicsResetStatusARB();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLenum glGetGraphicsResetStatusEXT();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLenum glGetGraphicsResetStatusKHR();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLhandleARB glGetHandleARB(GLenum pname);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetHistogram(GLenum target, GLboolean reset, GLenum format, GLenum type, void* values);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetHistogramEXT(GLenum target, GLboolean reset, GLenum format, GLenum type, void* values);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetHistogramParameterfv(GLenum target, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetHistogramParameterfvEXT(GLenum target, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetHistogramParameteriv(GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetHistogramParameterivEXT(GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetHistogramParameterxvOES(GLenum target, GLenum pname, GLfixed* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLuint64 glGetImageHandleARB(GLuint texture, GLint level, GLboolean layered, GLint layer, GLenum format);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLuint64 glGetImageHandleNV(GLuint texture, GLint level, GLboolean layered, GLint layer, GLenum format);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetImageTransformParameterfvHP(GLenum target, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetImageTransformParameterivHP(GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetInfoLogARB(GLhandleARB obj, GLsizei maxLength, GLsizei* length, GLcharARB* _infoLog);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLint glGetInstrumentsSGIX();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetInteger64i_v(GLenum target, GLuint _index, GLint64* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetInteger64v(GLenum pname, GLint64* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetInteger64vAPPLE(GLenum pname, GLint64* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetInteger64vEXT(GLenum pname, GLint64* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetIntegerIndexedvEXT(GLenum target, GLuint _index, GLint* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetIntegeri_v(GLenum target, GLuint _index, GLint* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetIntegeri_vEXT(GLenum target, GLuint _index, GLint* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetIntegerui64i_vNV(GLenum value, GLuint _index, GLuint64EXT* result);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetIntegerui64vNV(GLenum value, GLuint64EXT* result);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetIntegerv(GLenum pname, GLint* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetInternalformatSampleivNV(GLenum target, GLenum _internalformat, GLsizei samples, GLenum pname, GLsizei count, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetInternalformati64v(GLenum target, GLenum _internalformat, GLenum pname, GLsizei count, GLint64* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetInternalformativ(GLenum target, GLenum _internalformat, GLenum pname, GLsizei count, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetInvariantBooleanvEXT(GLuint id, GLenum value, GLboolean* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetInvariantFloatvEXT(GLuint id, GLenum value, GLfloat* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetInvariantIntegervEXT(GLuint id, GLenum value, GLint* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetLightfv(GLenum light, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetLightiv(GLenum light, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetLightxOES(GLenum light, GLenum pname, GLfixed* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetLightxv(GLenum light, GLenum pname, GLfixed* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetLightxvOES(GLenum light, GLenum pname, GLfixed* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetListParameterfvSGIX(GLuint list, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetListParameterivSGIX(GLuint list, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetLocalConstantBooleanvEXT(GLuint id, GLenum value, GLboolean* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetLocalConstantFloatvEXT(GLuint id, GLenum value, GLfloat* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetLocalConstantIntegervEXT(GLuint id, GLenum value, GLint* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetMapAttribParameterfvNV(GLenum target, GLuint _index, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetMapAttribParameterivNV(GLenum target, GLuint _index, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetMapControlPointsNV(GLenum target, GLuint _index, GLenum type, GLsizei ustride, GLsizei vstride, GLboolean packed, void* po_ints);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetMapParameterfvNV(GLenum target, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetMapParameterivNV(GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetMapdv(GLenum target, GLenum query, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetMapfv(GLenum target, GLenum query, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetMapiv(GLenum target, GLenum query, GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetMapxvOES(GLenum target, GLenum query, GLfixed* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetMaterialfv(GLenum face, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetMaterialiv(GLenum face, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetMaterialxOES(GLenum face, GLenum pname, GLfixed param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetMaterialxv(GLenum face, GLenum pname, GLfixed* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetMaterialxvOES(GLenum face, GLenum pname, GLfixed* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetMemoryObjectDetachedResourcesuivNV(GLuint memory, GLenum pname, GLint first, GLsizei count, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetMemoryObjectParameterivEXT(GLuint memoryObject, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetMinmax(GLenum target, GLboolean reset, GLenum format, GLenum type, void* values);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetMinmaxEXT(GLenum target, GLboolean reset, GLenum format, GLenum type, void* values);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetMinmaxParameterfv(GLenum target, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetMinmaxParameterfvEXT(GLenum target, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetMinmaxParameteriv(GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetMinmaxParameterivEXT(GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetMultiTexEnvfvEXT(GLenum texunit, GLenum target, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetMultiTexEnvivEXT(GLenum texunit, GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetMultiTexGendvEXT(GLenum texunit, GLenum coord, GLenum pname, GLdouble* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetMultiTexGenfvEXT(GLenum texunit, GLenum coord, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetMultiTexGenivEXT(GLenum texunit, GLenum coord, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetMultiTexImageEXT(GLenum texunit, GLenum target, GLint level, GLenum format, GLenum type, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetMultiTexLevelParameterfvEXT(GLenum texunit, GLenum target, GLint level, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetMultiTexLevelParameterivEXT(GLenum texunit, GLenum target, GLint level, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetMultiTexParameterIivEXT(GLenum texunit, GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetMultiTexParameterIuivEXT(GLenum texunit, GLenum target, GLenum pname, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetMultiTexParameterfvEXT(GLenum texunit, GLenum target, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetMultiTexParameterivEXT(GLenum texunit, GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetMultisamplefv(GLenum pname, GLuint _index, GLfloat* val);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetMultisamplefvNV(GLenum pname, GLuint _index, GLfloat* val);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetNamedBufferParameteri64v(GLuint buffer, GLenum pname, GLint64* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetNamedBufferParameteriv(GLuint buffer, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetNamedBufferParameterivEXT(GLuint buffer, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetNamedBufferParameterui64vNV(GLuint buffer, GLenum pname, GLuint64EXT* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetNamedBufferPointerv(GLuint buffer, GLenum pname, void** parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetNamedBufferPointervEXT(GLuint buffer, GLenum pname, void** parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetNamedBufferSubData(GLuint buffer, GLintptr offset, GLsizeiptr size, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetNamedBufferSubDataEXT(GLuint buffer, GLintptr offset, GLsizeiptr size, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetNamedFramebufferParameterfvAMD(GLuint framebuffer, GLenum pname, GLuint numsamples, GLuint pixel_index, GLsizei size, GLfloat* values);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetNamedFramebufferAttachmentParameteriv(GLuint framebuffer, GLenum attachment, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetNamedFramebufferAttachmentParameterivEXT(GLuint framebuffer, GLenum attachment, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetNamedFramebufferParameteriv(GLuint framebuffer, GLenum pname, GLint* param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetNamedFramebufferParameterivEXT(GLuint framebuffer, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetNamedProgramLocalParameterIivEXT(GLuint program, GLenum target, GLuint _index, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetNamedProgramLocalParameterIuivEXT(GLuint program, GLenum target, GLuint _index, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetNamedProgramLocalParameterdvEXT(GLuint program, GLenum target, GLuint _index, GLdouble* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetNamedProgramLocalParameterfvEXT(GLuint program, GLenum target, GLuint _index, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetNamedProgramStringEXT(GLuint program, GLenum target, GLenum pname, void* str);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetNamedProgramivEXT(GLuint program, GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetNamedRenderbufferParameteriv(GLuint renderbuffer, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetNamedRenderbufferParameterivEXT(GLuint renderbuffer, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetNamedStringARB(GLint namelen, GLchar* name, GLsizei bufSize, GLint* strlen, GLchar* str);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetNamedStringivARB(GLint namelen, GLchar* name, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetNextPerfQueryIdINTEL(GLuint queryId, GLuint* nextQueryId);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetObjectBufferfvATI(GLuint buffer, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetObjectBufferivATI(GLuint buffer, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetObjectLabel(GLenum identifier, GLuint name, GLsizei bufSize, GLsizei* length, GLchar* label);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetObjectLabelEXT(GLenum type, GLuint obj, GLsizei bufSize, GLsizei* length, GLchar* label);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetObjectLabelKHR(GLenum identifier, GLuint name, GLsizei bufSize, GLsizei* length, GLchar* label);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetObjectParameterfvARB(GLhandleARB obj, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetObjectParameterivAPPLE(GLenum objType, GLuint name, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetObjectParameterivARB(GLhandleARB obj, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetObjectPtrLabel(void* ptr, GLsizei bufSize, GLsizei* length, GLchar* label);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetObjectPtrLabelKHR(void* ptr, GLsizei bufSize, GLsizei* length, GLchar* label);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetOcclusionQueryivNV(GLuint id, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetOcclusionQueryuivNV(GLuint id, GLenum pname, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetPathColorGenfvNV(GLenum color, GLenum pname, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetPathColorGenivNV(GLenum color, GLenum pname, GLint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetPathCommandsNV(GLuint path, GLubyte* commands);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetPathCoordsNV(GLuint path, GLfloat* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetPathDashArrayNV(GLuint path, GLfloat* dashArray);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLfloat glGetPathLengthNV(GLuint path, GLsizei startSegment, GLsizei numSegments);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetPathMetricRangeNV(GLbitfield metricQueryMask, GLuint firstPathName, GLsizei numPaths, GLsizei stride, GLfloat* metrics);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetPathMetricsNV(GLbitfield metricQueryMask, GLsizei numPaths, GLenum pathNameType, void* paths, GLuint pathBase, GLsizei stride, GLfloat* metrics);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetPathParameterfvNV(GLuint path, GLenum pname, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetPathParameterivNV(GLuint path, GLenum pname, GLint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetPathSpacingNV(GLenum pathListMode, GLsizei numPaths, GLenum pathNameType, void* paths, GLuint pathBase, GLfloat advanceScale, GLfloat kern_ingScale, GLenum transformType, GLfloat* returnedSpac_ing);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetPathTexGenfvNV(GLenum texCoordSet, GLenum pname, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetPathTexGenivNV(GLenum texCoordSet, GLenum pname, GLint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetPerfCounterInfoINTEL(GLuint queryId, GLuint counterId, GLuint counterNameLength, GLchar* counterName, GLuint counterDescLength, GLchar* counterDesc, GLuint* counterOffset, GLuint* counterDataSize, GLuint* counterTypeEnum, GLuint* counterDataTypeEnum, GLuint64* rawCounterMaxValue);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetPerfMonitorCounterDataAMD(GLuint monitor, GLenum pname, GLsizei dataSize, GLuint* data, GLint* bytesWritten);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetPerfMonitorCounterInfoAMD(GLuint group, GLuint counter, GLenum pname, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetPerfMonitorCounterStringAMD(GLuint group, GLuint counter, GLsizei bufSize, GLsizei* length, GLchar* counterStr_ing);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetPerfMonitorCountersAMD(GLuint group, GLint* numCounters, GLint* maxActiveCounters, GLsizei counterSize, GLuint* counters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetPerfMonitorGroupStringAMD(GLuint group, GLsizei bufSize, GLsizei* length, GLchar* groupStr_ing);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetPerfMonitorGroupsAMD(GLint* numGroups, GLsizei groupsSize, GLuint* groups);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetPerfQueryDataINTEL(GLuint queryHandle, GLuint flags, GLsizei dataSize, void* data, GLuint* bytesWritten);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetPerfQueryIdByNameINTEL(GLchar* queryName, GLuint* queryId);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetPerfQueryInfoINTEL(GLuint queryId, GLuint queryNameLength, GLchar* queryName, GLuint* dataSize, GLuint* noCounters, GLuint* noInstances, GLuint* capsMask);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetPixelMapfv(GLenum map, GLfloat* values);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetPixelMapuiv(GLenum map, GLuint* values);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetPixelMapusv(GLenum map, GLushort* values);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetPixelMapxv(GLenum map, GLint size, GLfixed* values);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetPixelTexGenParameterfvSGIS(GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetPixelTexGenParameterivSGIS(GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetPixelTransformParameterfvEXT(GLenum target, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetPixelTransformParameterivEXT(GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetPointerIndexedvEXT(GLenum target, GLuint _index, void** data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetPointeri_vEXT(GLenum pname, GLuint _index, void** parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetPointerv(GLenum pname, void** parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetPointervEXT(GLenum pname, void** parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetPointervKHR(GLenum pname, void** parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetPolygonStipple(GLubyte* mask);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetProgramBinary(GLuint program, GLsizei bufSize, GLsizei* length, GLenum* b_inaryFormat, void* b_inary);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetProgramBinaryOES(GLuint program, GLsizei bufSize, GLsizei* length, GLenum* b_inaryFormat, void* b_inary);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetProgramEnvParameterIivNV(GLenum target, GLuint _index, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetProgramEnvParameterIuivNV(GLenum target, GLuint _index, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetProgramEnvParameterdvARB(GLenum target, GLuint _index, GLdouble* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetProgramEnvParameterfvARB(GLenum target, GLuint _index, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetProgramInfoLog(GLuint program, GLsizei bufSize, GLsizei* length, GLchar* _infoLog);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetProgramInterfaceiv(GLuint program, GLenum programInterface, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetProgramLocalParameterIivNV(GLenum target, GLuint _index, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetProgramLocalParameterIuivNV(GLenum target, GLuint _index, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetProgramLocalParameterdvARB(GLenum target, GLuint _index, GLdouble* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetProgramLocalParameterfvARB(GLenum target, GLuint _index, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetProgramNamedParameterdvNV(GLuint id, GLsizei len, GLubyte* name, GLdouble* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetProgramNamedParameterfvNV(GLuint id, GLsizei len, GLubyte* name, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetProgramParameterdvNV(GLenum target, GLuint _index, GLenum pname, GLdouble* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetProgramParameterfvNV(GLenum target, GLuint _index, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetProgramPipelineInfoLog(GLuint pipel_ine, GLsizei bufSize, GLsizei* length, GLchar* _infoLog);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetProgramPipelineInfoLogEXT(GLuint pipel_ine, GLsizei bufSize, GLsizei* length, GLchar* _infoLog);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetProgramPipelineiv(GLuint pipel_ine, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetProgramPipelineivEXT(GLuint pipel_ine, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLuint glGetProgramResourceIndex(GLuint program, GLenum programInterface, GLchar* name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLint glGetProgramResourceLocation(GLuint program, GLenum programInterface, GLchar* name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLint glGetProgramResourceLocationIndex(GLuint program, GLenum programInterface, GLchar* name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLint glGetProgramResourceLocationIndexEXT(GLuint program, GLenum programInterface, GLchar* name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetProgramResourceName(GLuint program, GLenum programInterface, GLuint _index, GLsizei bufSize, GLsizei* length, GLchar* name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetProgramResourcefvNV(GLuint program, GLenum programInterface, GLuint _index, GLsizei propCount, GLenum* props, GLsizei count, GLsizei* length, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetProgramResourceiv(GLuint program, GLenum programInterface, GLuint _index, GLsizei propCount, GLenum* props, GLsizei count, GLsizei* length, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetProgramStageiv(GLuint program, GLenum shadertype, GLenum pname, GLint* values);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetProgramStringARB(GLenum target, GLenum pname, void* str);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetProgramStringNV(GLuint id, GLenum pname, GLubyte* program);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetProgramSubroutineParameteruivNV(GLenum target, GLuint _index, GLuint* param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetProgramiv(GLuint program, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetProgramivARB(GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetProgramivNV(GLuint id, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetQueryBufferObjecti64v(GLuint id, GLuint buffer, GLenum pname, GLintptr offset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetQueryBufferObjectiv(GLuint id, GLuint buffer, GLenum pname, GLintptr offset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetQueryBufferObjectui64v(GLuint id, GLuint buffer, GLenum pname, GLintptr offset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetQueryBufferObjectuiv(GLuint id, GLuint buffer, GLenum pname, GLintptr offset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetQueryIndexediv(GLenum target, GLuint _index, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetQueryObjecti64v(GLuint id, GLenum pname, GLint64* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetQueryObjecti64vEXT(GLuint id, GLenum pname, GLint64* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetQueryObjectiv(GLuint id, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetQueryObjectivARB(GLuint id, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetQueryObjectivEXT(GLuint id, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetQueryObjectui64v(GLuint id, GLenum pname, GLuint64* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetQueryObjectui64vEXT(GLuint id, GLenum pname, GLuint64* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetQueryObjectuiv(GLuint id, GLenum pname, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetQueryObjectuivARB(GLuint id, GLenum pname, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetQueryObjectuivEXT(GLuint id, GLenum pname, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetQueryiv(GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetQueryivARB(GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetQueryivEXT(GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetRenderbufferParameteriv(GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetRenderbufferParameterivEXT(GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetRenderbufferParameterivOES(GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetSamplerParameterIiv(GLuint sampler, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetSamplerParameterIivEXT(GLuint sampler, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetSamplerParameterIivOES(GLuint sampler, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetSamplerParameterIuiv(GLuint sampler, GLenum pname, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetSamplerParameterIuivEXT(GLuint sampler, GLenum pname, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetSamplerParameterIuivOES(GLuint sampler, GLenum pname, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetSamplerParameterfv(GLuint sampler, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetSamplerParameteriv(GLuint sampler, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetSemaphoreParameterivNV(GLuint semaphore, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetSemaphoreParameterui64vEXT(GLuint semaphore, GLenum pname, GLuint64* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetSeparableFilter(GLenum target, GLenum format, GLenum type, void* row, void* column, void* span);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetSeparableFilterEXT(GLenum target, GLenum format, GLenum type, void* row, void* column, void* span);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetShaderInfoLog(GLuint shader, GLsizei bufSize, GLsizei* length, GLchar* _infoLog);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetShaderPrecisionFormat(GLenum shadertype, GLenum precisiontype, GLint* range, GLint* precision);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetShaderSource(GLuint shader, GLsizei bufSize, GLsizei* length, GLchar* source);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetShaderSourceARB(GLhandleARB obj, GLsizei maxLength, GLsizei* length, GLcharARB* source);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetShaderiv(GLuint shader, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetShadingRateImagePaletteNV(GLuint viewport, GLuint entry, GLenum* rate);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetShadingRateSampleLocationivNV(GLenum rate, GLuint samples, GLuint _index, GLint* location);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetSharpenTexFuncSGIS(GLenum target, GLfloat* po_ints);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLushort glGetStageIndexNV(GLenum shadertype);


    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLubyte* glGetString(GLenum name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLubyte* glGetStringi(GLenum name, GLuint _index);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLuint glGetSubroutineIndex(GLuint program, GLenum shadertype, GLchar* name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLint glGetSubroutineUniformLocation(GLuint program, GLenum shadertype, GLchar* name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetSynciv(GLsync sync, GLenum pname, GLsizei count, GLsizei* length, GLint* values);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetSyncivAPPLE(GLsync sync, GLenum pname, GLsizei count, GLsizei* length, GLint* values);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTexBumpParameterfvATI(GLenum pname, GLfloat* param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTexBumpParameterivATI(GLenum pname, GLint* param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTexEnvfv(GLenum target, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTexEnviv(GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTexEnvxv(GLenum target, GLenum pname, GLfixed* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTexEnvxvOES(GLenum target, GLenum pname, GLfixed* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTexFilterFuncSGIS(GLenum target, GLenum filter, GLfloat* weights);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTexGendv(GLenum coord, GLenum pname, GLdouble* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTexGenfv(GLenum coord, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTexGenfvOES(GLenum coord, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTexGeniv(GLenum coord, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTexGenivOES(GLenum coord, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTexGenxvOES(GLenum coord, GLenum pname, GLfixed* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTexImage(GLenum target, GLint level, GLenum format, GLenum type, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTexLevelParameterfv(GLenum target, GLint level, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTexLevelParameteriv(GLenum target, GLint level, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTexLevelParameterxvOES(GLenum target, GLint level, GLenum pname, GLfixed* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTexParameterIiv(GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTexParameterIivEXT(GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTexParameterIivOES(GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTexParameterIuiv(GLenum target, GLenum pname, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTexParameterIuivEXT(GLenum target, GLenum pname, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTexParameterIuivOES(GLenum target, GLenum pname, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTexParameterPointervAPPLE(GLenum target, GLenum pname, void** parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTexParameterfv(GLenum target, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTexParameteriv(GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTexParameterxv(GLenum target, GLenum pname, GLfixed* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTexParameterxvOES(GLenum target, GLenum pname, GLfixed* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLuint64 glGetTextureHandleARB(GLuint texture);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLuint64 glGetTextureHandleIMG(GLuint texture);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLuint64 glGetTextureHandleNV(GLuint texture);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTextureImage(GLuint texture, GLint level, GLenum format, GLenum type, GLsizei bufSize, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTextureImageEXT(GLuint texture, GLenum target, GLint level, GLenum format, GLenum type, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTextureLevelParameterfv(GLuint texture, GLint level, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTextureLevelParameterfvEXT(GLuint texture, GLenum target, GLint level, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTextureLevelParameteriv(GLuint texture, GLint level, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTextureLevelParameterivEXT(GLuint texture, GLenum target, GLint level, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTextureParameterIiv(GLuint texture, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTextureParameterIivEXT(GLuint texture, GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTextureParameterIuiv(GLuint texture, GLenum pname, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTextureParameterIuivEXT(GLuint texture, GLenum target, GLenum pname, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTextureParameterfv(GLuint texture, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTextureParameterfvEXT(GLuint texture, GLenum target, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTextureParameteriv(GLuint texture, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTextureParameterivEXT(GLuint texture, GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLuint64 glGetTextureSamplerHandleARB(GLuint texture, GLuint sampler);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLuint64 glGetTextureSamplerHandleIMG(GLuint texture, GLuint sampler);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLuint64 glGetTextureSamplerHandleNV(GLuint texture, GLuint sampler);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTextureSubImage(GLuint texture, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLsizei width, GLsizei height, GLsizei depth, GLenum format, GLenum type, GLsizei bufSize, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTrackMatrixivNV(GLenum target, GLuint address, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTransformFeedbackVarying(GLuint program, GLuint _index, GLsizei bufSize, GLsizei* length, GLsizei* size, GLenum* type, GLchar* name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTransformFeedbackVaryingEXT(GLuint program, GLuint _index, GLsizei bufSize, GLsizei* length, GLsizei* size, GLenum* type, GLchar* name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTransformFeedbackVaryingNV(GLuint program, GLuint _index, GLint* location);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTransformFeedbacki64_v(GLuint xfb, GLenum pname, GLuint _index, GLint64* param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTransformFeedbacki_v(GLuint xfb, GLenum pname, GLuint _index, GLint* param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTransformFeedbackiv(GLuint xfb, GLenum pname, GLint* param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetTranslatedShaderSourceANGLE(GLuint shader, GLsizei bufSize, GLsizei* length, GLchar* source);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLuint glGetUniformBlockIndex(GLuint program, GLchar* uniformBlockName);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLint glGetUniformBufferSizeEXT(GLuint program, GLint location);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetUniformIndices(GLuint program, GLsizei uniformCount, GLchar** uniformNames, GLuint* uniformIndices);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLint glGetUniformLocation(GLuint program, GLchar* name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLint glGetUniformLocationARB(GLhandleARB programObj, GLcharARB* name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLintptr glGetUniformOffsetEXT(GLuint program, GLint location);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetUniformSubroutineuiv(GLenum shadertype, GLint location, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetUniformdv(GLuint program, GLint location, GLdouble* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetUniformfv(GLuint program, GLint location, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetUniformfvARB(GLhandleARB programObj, GLint location, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetUniformi64vARB(GLuint program, GLint location, GLint64* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetUniformi64vNV(GLuint program, GLint location, GLint64EXT* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetUniformiv(GLuint program, GLint location, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetUniformivARB(GLhandleARB programObj, GLint location, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetUniformui64vARB(GLuint program, GLint location, GLuint64* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetUniformui64vNV(GLuint program, GLint location, GLuint64EXT* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetUniformuiv(GLuint program, GLint location, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetUniformuivEXT(GLuint program, GLint location, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetUnsignedBytevEXT(GLenum pname, GLubyte* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetUnsignedBytei_vEXT(GLenum target, GLuint _index, GLubyte* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVariantArrayObjectfvATI(GLuint id, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVariantArrayObjectivATI(GLuint id, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVariantBooleanvEXT(GLuint id, GLenum value, GLboolean* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVariantFloatvEXT(GLuint id, GLenum value, GLfloat* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVariantIntegervEXT(GLuint id, GLenum value, GLint* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVariantPointervEXT(GLuint id, GLenum value, void** data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLint glGetVaryingLocationNV(GLuint program, GLchar* name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVertexArrayIndexed64iv(GLuint vaobj, GLuint _index, GLenum pname, GLint64* param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVertexArrayIndexediv(GLuint vaobj, GLuint _index, GLenum pname, GLint* param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVertexArrayIntegeri_vEXT(GLuint vaobj, GLuint _index, GLenum pname, GLint* param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVertexArrayIntegervEXT(GLuint vaobj, GLenum pname, GLint* param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVertexArrayPointeri_vEXT(GLuint vaobj, GLuint _index, GLenum pname, void** param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVertexArrayPointervEXT(GLuint vaobj, GLenum pname, void** param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVertexArrayiv(GLuint vaobj, GLenum pname, GLint* param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVertexAttribArrayObjectfvATI(GLuint _index, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVertexAttribArrayObjectivATI(GLuint _index, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVertexAttribIiv(GLuint _index, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVertexAttribIivEXT(GLuint _index, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVertexAttribIuiv(GLuint _index, GLenum pname, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVertexAttribIuivEXT(GLuint _index, GLenum pname, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVertexAttribLdv(GLuint _index, GLenum pname, GLdouble* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVertexAttribLdvEXT(GLuint _index, GLenum pname, GLdouble* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVertexAttribLi64vNV(GLuint _index, GLenum pname, GLint64EXT* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVertexAttribLui64vARB(GLuint _index, GLenum pname, GLuint64EXT* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVertexAttribLui64vNV(GLuint _index, GLenum pname, GLuint64EXT* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVertexAttribPointerv(GLuint _index, GLenum pname, void** po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVertexAttribPointervARB(GLuint _index, GLenum pname, void** po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVertexAttribPointervNV(GLuint _index, GLenum pname, void** po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVertexAttribdv(GLuint _index, GLenum pname, GLdouble* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVertexAttribdvARB(GLuint _index, GLenum pname, GLdouble* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVertexAttribdvNV(GLuint _index, GLenum pname, GLdouble* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVertexAttribfv(GLuint _index, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVertexAttribfvARB(GLuint _index, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVertexAttribfvNV(GLuint _index, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVertexAttribiv(GLuint _index, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVertexAttribivARB(GLuint _index, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVertexAttribivNV(GLuint _index, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVideoCaptureStreamdvNV(GLuint video_capture_slot, GLuint stream, GLenum pname, GLdouble* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVideoCaptureStreamfvNV(GLuint video_capture_slot, GLuint stream, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVideoCaptureStreamivNV(GLuint video_capture_slot, GLuint stream, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVideoCaptureivNV(GLuint video_capture_slot, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVideoi64vNV(GLuint video_slot, GLenum pname, GLint64EXT* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVideoivNV(GLuint video_slot, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVideoui64vNV(GLuint video_slot, GLenum pname, GLuint64EXT* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetVideouivNV(GLuint video_slot, GLenum pname, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnColorTable(GLenum target, GLenum format, GLenum type, GLsizei bufSize, void* table);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnColorTableARB(GLenum target, GLenum format, GLenum type, GLsizei bufSize, void* table);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnCompressedTexImage(GLenum target, GLint lod, GLsizei bufSize, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnCompressedTexImageARB(GLenum target, GLint lod, GLsizei bufSize, void* img);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnConvolutionFilter(GLenum target, GLenum format, GLenum type, GLsizei bufSize, void* image);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnConvolutionFilterARB(GLenum target, GLenum format, GLenum type, GLsizei bufSize, void* image);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnHistogram(GLenum target, GLboolean reset, GLenum format, GLenum type, GLsizei bufSize, void* values);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnHistogramARB(GLenum target, GLboolean reset, GLenum format, GLenum type, GLsizei bufSize, void* values);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnMapdv(GLenum target, GLenum query, GLsizei bufSize, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnMapdvARB(GLenum target, GLenum query, GLsizei bufSize, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnMapfv(GLenum target, GLenum query, GLsizei bufSize, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnMapfvARB(GLenum target, GLenum query, GLsizei bufSize, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnMapiv(GLenum target, GLenum query, GLsizei bufSize, GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnMapivARB(GLenum target, GLenum query, GLsizei bufSize, GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnMinmax(GLenum target, GLboolean reset, GLenum format, GLenum type, GLsizei bufSize, void* values);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnMinmaxARB(GLenum target, GLboolean reset, GLenum format, GLenum type, GLsizei bufSize, void* values);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnPixelMapfv(GLenum map, GLsizei bufSize, GLfloat* values);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnPixelMapfvARB(GLenum map, GLsizei bufSize, GLfloat* values);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnPixelMapuiv(GLenum map, GLsizei bufSize, GLuint* values);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnPixelMapuivARB(GLenum map, GLsizei bufSize, GLuint* values);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnPixelMapusv(GLenum map, GLsizei bufSize, GLushort* values);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnPixelMapusvARB(GLenum map, GLsizei bufSize, GLushort* values);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnPolygonStipple(GLsizei bufSize, GLubyte* pattern);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnPolygonStippleARB(GLsizei bufSize, GLubyte* pattern);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnSeparableFilter(GLenum target, GLenum format, GLenum type, GLsizei rowBufSize, void* row, GLsizei columnBufSize, void* column, void* span);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnSeparableFilterARB(GLenum target, GLenum format, GLenum type, GLsizei rowBufSize, void* row, GLsizei columnBufSize, void* column, void* span);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnTexImage(GLenum target, GLint level, GLenum format, GLenum type, GLsizei bufSize, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnTexImageARB(GLenum target, GLint level, GLenum format, GLenum type, GLsizei bufSize, void* img);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnUniformdv(GLuint program, GLint location, GLsizei bufSize, GLdouble* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnUniformdvARB(GLuint program, GLint location, GLsizei bufSize, GLdouble* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnUniformfv(GLuint program, GLint location, GLsizei bufSize, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnUniformfvARB(GLuint program, GLint location, GLsizei bufSize, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnUniformfvEXT(GLuint program, GLint location, GLsizei bufSize, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnUniformfvKHR(GLuint program, GLint location, GLsizei bufSize, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnUniformi64vARB(GLuint program, GLint location, GLsizei bufSize, GLint64* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnUniformiv(GLuint program, GLint location, GLsizei bufSize, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnUniformivARB(GLuint program, GLint location, GLsizei bufSize, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnUniformivEXT(GLuint program, GLint location, GLsizei bufSize, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnUniformivKHR(GLuint program, GLint location, GLsizei bufSize, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnUniformui64vARB(GLuint program, GLint location, GLsizei bufSize, GLuint64* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnUniformuiv(GLuint program, GLint location, GLsizei bufSize, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnUniformuivARB(GLuint program, GLint location, GLsizei bufSize, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetnUniformuivKHR(GLuint program, GLint location, GLsizei bufSize, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGlobalAlphaFactorbSUN(GLbyte factor);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGlobalAlphaFactordSUN(GLdouble factor);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGlobalAlphaFactorfSUN(GLfloat factor);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGlobalAlphaFactoriSUN(GLint factor);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGlobalAlphaFactorsSUN(GLshort factor);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGlobalAlphaFactorubSUN(GLubyte factor);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGlobalAlphaFactoruiSUN(GLuint factor);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGlobalAlphaFactorusSUN(GLushort factor);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glHint(GLenum target, GLenum mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glHintPGI(GLenum target, GLint mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glHistogram(GLenum target, GLsizei width, GLenum _internalformat, GLboolean s_ink);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glHistogramEXT(GLenum target, GLsizei width, GLenum _internalformat, GLboolean s_ink);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glIglooInterfaceSGIX(GLenum pname, void* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glImageTransformParameterfHP(GLenum target, GLenum pname, GLfloat param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glImageTransformParameterfvHP(GLenum target, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glImageTransformParameteriHP(GLenum target, GLenum pname, GLint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glImageTransformParameterivHP(GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glImportMemoryFdEXT(GLuint memory, GLuint64 size, GLenum handleType, GLint fd);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glImportMemoryWin32HandleEXT(GLuint memory, GLuint64 size, GLenum handleType, void* handle);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glImportMemoryWin32NameEXT(GLuint memory, GLuint64 size, GLenum handleType, void* name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glImportSemaphoreFdEXT(GLuint semaphore, GLenum handleType, GLint fd);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glImportSemaphoreWin32HandleEXT(GLuint semaphore, GLenum handleType, void* handle);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glImportSemaphoreWin32NameEXT(GLuint semaphore, GLenum handleType, void* name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLsync glImportSyncEXT(GLenum external_sync_type, GLintptr external_sync, GLbitfield flags);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glIndexFormatNV(GLenum type, GLsizei stride);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glIndexFuncEXT(GLenum func, GLclampf refer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glIndexMask(GLuint mask);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glIndexMaterialEXT(GLenum face, GLenum mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glIndexPointer(GLenum type, GLsizei stride, void* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glIndexPointerEXT(GLenum type, GLsizei stride, GLsizei count, void* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glIndexPointerListIBM(GLenum type, GLint stride, void** po_inter, GLint ptrstride);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glIndexd(GLdouble c);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glIndexdv(GLdouble* c);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glIndexf(GLfloat c);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glIndexfv(GLfloat* c);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glIndexi(GLint c);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glIndexiv(GLint* c);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glIndexs(GLshort c);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glIndexsv(GLshort* c);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glIndexub(GLubyte c);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glIndexubv(GLubyte* c);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glIndexxOES(GLfixed component);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glIndexxvOES(GLfixed* component);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glInitNames();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glInsertComponentEXT(GLuint res, GLuint src, GLuint num);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glInsertEventMarkerEXT(GLsizei length, GLchar* marker);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glInstrumentsBufferSGIX(GLsizei size, GLint* buffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glInterleavedArrays(GLenum format, GLsizei stride, void* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glInterpolatePathsNV(GLuint resultPath, GLuint pathA, GLuint pathB, GLfloat weight);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glInvalidateBufferData(GLuint buffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glInvalidateBufferSubData(GLuint buffer, GLintptr offset, GLsizeiptr length);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glInvalidateFramebuffer(GLenum target, GLsizei numAttachments, GLenum* attachments);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glInvalidateNamedFramebufferData(GLuint framebuffer, GLsizei numAttachments, GLenum* attachments);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glInvalidateNamedFramebufferSubData(GLuint framebuffer, GLsizei numAttachments, GLenum* attachments, GLint x, GLint y, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glInvalidateSubFramebuffer(GLenum target, GLsizei numAttachments, GLenum* attachments, GLint x, GLint y, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glInvalidateTexImage(GLuint texture, GLint level);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glInvalidateTexSubImage(GLuint texture, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLsizei width, GLsizei height, GLsizei depth);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsAsyncMarkerSGIX(GLuint marker);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsBuffer(GLuint buffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsBufferARB(GLuint buffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsBufferResidentNV(GLenum target);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsCommandListNV(GLuint list);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsEnabled(GLenum cap);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsEnabledIndexedEXT(GLenum target, GLuint _index);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsEnabledi(GLenum target, GLuint _index);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsEnablediEXT(GLenum target, GLuint _index);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsEnablediNV(GLenum target, GLuint _index);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsEnablediOES(GLenum target, GLuint _index);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsFenceAPPLE(GLuint fence);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsFenceNV(GLuint fence);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsFramebuffer(GLuint framebuffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsFramebufferEXT(GLuint framebuffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsFramebufferOES(GLuint framebuffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsImageHandleResidentARB(GLuint64 handle);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsImageHandleResidentNV(GLuint64 handle);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsList(GLuint list);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsMemoryObjectEXT(GLuint memoryObject);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsNameAMD(GLenum identifier, GLuint name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsNamedBufferResidentNV(GLuint buffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsNamedStringARB(GLint namelen, GLchar* name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsObjectBufferATI(GLuint buffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsOcclusionQueryNV(GLuint id);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsPathNV(GLuint path);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsPointInFillPathNV(GLuint path, GLuint mask, GLfloat x, GLfloat y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsPointInStrokePathNV(GLuint path, GLfloat x, GLfloat y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsProgram(GLuint program);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsProgramARB(GLuint program);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsProgramNV(GLuint id);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsProgramPipeline(GLuint pipel_ine);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsProgramPipelineEXT(GLuint pipel_ine);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsQuery(GLuint id);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsQueryARB(GLuint id);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsQueryEXT(GLuint id);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsRenderbuffer(GLuint renderbuffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsRenderbufferEXT(GLuint renderbuffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsRenderbufferOES(GLuint renderbuffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsSemaphoreEXT(GLuint semaphore);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsSampler(GLuint sampler);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsShader(GLuint shader);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsStateNV(GLuint state);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsSync(GLsync sync);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsSyncAPPLE(GLsync sync);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsTexture(GLuint texture);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsTextureEXT(GLuint texture);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsTextureHandleResidentARB(GLuint64 handle);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsTextureHandleResidentNV(GLuint64 handle);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsTransformFeedback(GLuint id);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsTransformFeedbackNV(GLuint id);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsVariantEnabledEXT(GLuint id, GLenum cap);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsVertexArray(GLuint array);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsVertexArrayAPPLE(GLuint array);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsVertexArrayOES(GLuint array);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glIsVertexAttribEnabledAPPLE(GLuint _index, GLenum pname);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLGPUCopyImageSubDataNVX(GLuint sourceGpu, GLbitfield dest_inationGpuMask, GLuint srcName, GLenum srcTarget, GLint srcLevel, GLint srcX, GLint srxY, GLint srcZ, GLuint dstName, GLenum dstTarget, GLint dstLevel, GLint dstX, GLint dstY, GLint dstZ, GLsizei width, GLsizei height, GLsizei depth);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLGPUInterlockNVX();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLGPUNamedBufferSubDataNVX(GLbitfield gpuMask, GLuint buffer, GLintptr offset, GLsizeiptr size, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLabelObjectEXT(GLenum type, GLuint obj, GLsizei length, GLchar* label);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLightEnviSGIX(GLenum pname, GLint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLightModelf(GLenum pname, GLfloat param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLightModelfv(GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLightModeli(GLenum pname, GLint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLightModeliv(GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLightModelx(GLenum pname, GLfixed param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLightModelxOES(GLenum pname, GLfixed param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLightModelxv(GLenum pname, GLfixed* param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLightModelxvOES(GLenum pname, GLfixed* param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLightf(GLenum light, GLenum pname, GLfloat param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLightfv(GLenum light, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLighti(GLenum light, GLenum pname, GLint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLightiv(GLenum light, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLightx(GLenum light, GLenum pname, GLfixed param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLightxOES(GLenum light, GLenum pname, GLfixed param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLightxv(GLenum light, GLenum pname, GLfixed* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLightxvOES(GLenum light, GLenum pname, GLfixed* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLineStipple(GLint factor, GLushort pattern);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLineWidth(GLfloat width);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLineWidthx(GLfixed width);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLineWidthxOES(GLfixed width);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLinkProgram(GLuint program);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLinkProgramARB(GLhandleARB programObj);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glListBase(GLuint _base);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glListDrawCommandsStatesClientNV(GLuint list, GLuint segment, void** _indirects, GLsizei* sizes, GLuint* states, GLuint* fbos, GLuint count);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glListParameterfSGIX(GLuint list, GLenum pname, GLfloat param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glListParameterfvSGIX(GLuint list, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glListParameteriSGIX(GLuint list, GLenum pname, GLint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glListParameterivSGIX(GLuint list, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLoadIdentity();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLoadIdentityDeformationMapSGIX(GLbitfield mask);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLoadMatrixd(GLdouble* m);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLoadMatrixf(GLfloat* m);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLoadMatrixx(GLfixed* m);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLoadMatrixxOES(GLfixed* m);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLoadName(GLuint name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLoadPaletteFromModelViewMatrixOES();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLoadProgramNV(GLenum target, GLuint id, GLsizei len, GLubyte* program);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLoadTransposeMatrixd(GLdouble* m);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLoadTransposeMatrixdARB(GLdouble* m);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLoadTransposeMatrixf(GLfloat* m);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLoadTransposeMatrixfARB(GLfloat* m);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLoadTransposeMatrixxOES(GLfixed* m);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLockArraysEXT(GLint first, GLsizei count);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glLogicOp(GLenum opcode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMakeBufferNonResidentNV(GLenum target);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMakeBufferResidentNV(GLenum target, GLenum access);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMakeImageHandleNonResidentARB(GLuint64 handle);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMakeImageHandleNonResidentNV(GLuint64 handle);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMakeImageHandleResidentARB(GLuint64 handle, GLenum access);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMakeImageHandleResidentNV(GLuint64 handle, GLenum access);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMakeNamedBufferNonResidentNV(GLuint buffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMakeNamedBufferResidentNV(GLuint buffer, GLenum access);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMakeTextureHandleNonResidentARB(GLuint64 handle);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMakeTextureHandleNonResidentNV(GLuint64 handle);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMakeTextureHandleResidentARB(GLuint64 handle);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMakeTextureHandleResidentNV(GLuint64 handle);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMap1d(GLenum target, GLdouble u1, GLdouble u2, GLint stride, GLint order, GLdouble* po_ints);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMap1f(GLenum target, GLfloat u1, GLfloat u2, GLint stride, GLint order, GLfloat* po_ints);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMap1xOES(GLenum target, GLfixed u1, GLfixed u2, GLint stride, GLint order, GLfixed po_ints);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMap2d(GLenum target, GLdouble u1, GLdouble u2, GLint ustride, GLint uorder, GLdouble v1, GLdouble v2, GLint vstride, GLint vorder, GLdouble* po_ints);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMap2f(GLenum target, GLfloat u1, GLfloat u2, GLint ustride, GLint uorder, GLfloat v1, GLfloat v2, GLint vstride, GLint vorder, GLfloat* po_ints);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMap2xOES(GLenum target, GLfixed u1, GLfixed u2, GLint ustride, GLint uorder, GLfixed v1, GLfixed v2, GLint vstride, GLint vorder, GLfixed po_ints);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void* glMapBuffer(GLenum target, GLenum access);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void* glMapBufferARB(GLenum target, GLenum access);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void* glMapBufferOES(GLenum target, GLenum access);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void* glMapBufferRange(GLenum target, GLintptr offset, GLsizeiptr length, GLbitfield access);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void* glMapBufferRangeEXT(GLenum target, GLintptr offset, GLsizeiptr length, GLbitfield access);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMapControlPointsNV(GLenum target, GLuint _index, GLenum type, GLsizei ustride, GLsizei vstride, GLint uorder, GLint vorder, GLboolean packed, void* po_ints);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMapGrid1d(GLint un, GLdouble u1, GLdouble u2);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMapGrid1f(GLint un, GLfloat u1, GLfloat u2);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMapGrid1xOES(GLint n, GLfixed u1, GLfixed u2);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMapGrid2d(GLint un, GLdouble u1, GLdouble u2, GLint vn, GLdouble v1, GLdouble v2);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMapGrid2f(GLint un, GLfloat u1, GLfloat u2, GLint vn, GLfloat v1, GLfloat v2);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMapGrid2xOES(GLint n, GLfixed u1, GLfixed u2, GLfixed v1, GLfixed v2);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void* glMapNamedBuffer(GLuint buffer, GLenum access);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void* glMapNamedBufferEXT(GLuint buffer, GLenum access);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void* glMapNamedBufferRange(GLuint buffer, GLintptr offset, GLsizeiptr length, GLbitfield access);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void* glMapNamedBufferRangeEXT(GLuint buffer, GLintptr offset, GLsizeiptr length, GLbitfield access);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void* glMapObjectBufferATI(GLuint buffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMapParameterfvNV(GLenum target, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMapParameterivNV(GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void* glMapTexture2DINTEL(GLuint texture, GLint level, GLbitfield access, GLint* stride, GLenum* lay_out);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMapVertexAttrib1dAPPLE(GLuint _index, GLuint size, GLdouble u1, GLdouble u2, GLint stride, GLint order, GLdouble* po_ints);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMapVertexAttrib1fAPPLE(GLuint _index, GLuint size, GLfloat u1, GLfloat u2, GLint stride, GLint order, GLfloat* po_ints);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMapVertexAttrib2dAPPLE(GLuint _index, GLuint size, GLdouble u1, GLdouble u2, GLint ustride, GLint uorder, GLdouble v1, GLdouble v2, GLint vstride, GLint vorder, GLdouble* po_ints);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMapVertexAttrib2fAPPLE(GLuint _index, GLuint size, GLfloat u1, GLfloat u2, GLint ustride, GLint uorder, GLfloat v1, GLfloat v2, GLint vstride, GLint vorder, GLfloat* po_ints);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMaterialf(GLenum face, GLenum pname, GLfloat param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMaterialfv(GLenum face, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMateriali(GLenum face, GLenum pname, GLint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMaterialiv(GLenum face, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMaterialx(GLenum face, GLenum pname, GLfixed param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMaterialxOES(GLenum face, GLenum pname, GLfixed param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMaterialxv(GLenum face, GLenum pname, GLfixed* param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMaterialxvOES(GLenum face, GLenum pname, GLfixed* param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMatrixFrustumEXT(GLenum mode, GLdouble left, GLdouble right, GLdouble bottom, GLdouble top, GLdouble zNear, GLdouble zFar);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMatrixIndexPointerARB(GLint size, GLenum type, GLsizei stride, void* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMatrixIndexPointerOES(GLint size, GLenum type, GLsizei stride, void* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMatrixIndexubvARB(GLint size, GLubyte* _indices);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMatrixIndexuivARB(GLint size, GLuint* _indices);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMatrixIndexusvARB(GLint size, GLushort* _indices);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMatrixLoad3x2fNV(GLenum matrixMode, GLfloat* m);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMatrixLoad3x3fNV(GLenum matrixMode, GLfloat* m);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMatrixLoadIdentityEXT(GLenum mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMatrixLoadTranspose3x3fNV(GLenum matrixMode, GLfloat* m);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMatrixLoadTransposedEXT(GLenum mode, GLdouble* m);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMatrixLoadTransposefEXT(GLenum mode, GLfloat* m);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMatrixLoaddEXT(GLenum mode, GLdouble* m);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMatrixLoadfEXT(GLenum mode, GLfloat* m);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMatrixMode(GLenum mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMatrixMult3x2fNV(GLenum matrixMode, GLfloat* m);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMatrixMult3x3fNV(GLenum matrixMode, GLfloat* m);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMatrixMultTranspose3x3fNV(GLenum matrixMode, GLfloat* m);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMatrixMultTransposedEXT(GLenum mode, GLdouble* m);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMatrixMultTransposefEXT(GLenum mode, GLfloat* m);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMatrixMultdEXT(GLenum mode, GLdouble* m);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMatrixMultfEXT(GLenum mode, GLfloat* m);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMatrixOrthoEXT(GLenum mode, GLdouble left, GLdouble right, GLdouble bottom, GLdouble top, GLdouble zNear, GLdouble zFar);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMatrixPopEXT(GLenum mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMatrixPushEXT(GLenum mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMatrixRotatedEXT(GLenum mode, GLdouble angle, GLdouble x, GLdouble y, GLdouble z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMatrixRotatefEXT(GLenum mode, GLfloat angle, GLfloat x, GLfloat y, GLfloat z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMatrixScaledEXT(GLenum mode, GLdouble x, GLdouble y, GLdouble z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMatrixScalefEXT(GLenum mode, GLfloat x, GLfloat y, GLfloat z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMatrixTranslatedEXT(GLenum mode, GLdouble x, GLdouble y, GLdouble z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMatrixTranslatefEXT(GLenum mode, GLfloat x, GLfloat y, GLfloat z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMaxShaderCompilerThreadsKHR(GLuint count);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMaxShaderCompilerThreadsARB(GLuint count);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMemoryBarrier(GLbitfield barriers);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMemoryBarrierByRegion(GLbitfield barriers);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMemoryBarrierEXT(GLbitfield barriers);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMemoryObjectParameterivEXT(GLuint memoryObject, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMinSampleShading(GLfloat value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMinSampleShadingARB(GLfloat value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMinSampleShadingOES(GLfloat value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMinmax(GLenum target, GLenum _internalformat, GLboolean s_ink);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMinmaxEXT(GLenum target, GLenum _internalformat, GLboolean s_ink);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultMatrixd(GLdouble* m);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultMatrixf(GLfloat* m);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultMatrixx(GLfixed* m);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultMatrixxOES(GLfixed* m);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultTransposeMatrixd(GLdouble* m);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultTransposeMatrixdARB(GLdouble* m);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultTransposeMatrixf(GLfloat* m);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultTransposeMatrixfARB(GLfloat* m);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultTransposeMatrixxOES(GLfixed* m);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiDrawArrays(GLenum mode, GLint* first, GLsizei* count, GLsizei drawcount);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiDrawArraysEXT(GLenum mode, GLint* first, GLsizei* count, GLsizei primcount);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiDrawArraysIndirect(GLenum mode, void* _indirect, GLsizei drawcount, GLsizei stride);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiDrawArraysIndirectAMD(GLenum mode, void* _indirect, GLsizei primcount, GLsizei stride);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiDrawArraysIndirectBindlessCountNV(GLenum mode, void* _indirect, GLsizei drawCount, GLsizei maxDrawCount, GLsizei stride, GLint vertexBufferCount);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiDrawArraysIndirectBindlessNV(GLenum mode, void* _indirect, GLsizei drawCount, GLsizei stride, GLint vertexBufferCount);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiDrawArraysIndirectCount(GLenum mode, void* _indirect, GLintptr drawcount, GLsizei maxdrawcount, GLsizei stride);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiDrawArraysIndirectCountARB(GLenum mode, void* _indirect, GLintptr drawcount, GLsizei maxdrawcount, GLsizei stride);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiDrawArraysIndirectEXT(GLenum mode, void* _indirect, GLsizei drawcount, GLsizei stride);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiDrawElementArrayAPPLE(GLenum mode, GLint* first, GLsizei* count, GLsizei primcount);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiDrawElements(GLenum mode, GLsizei* count, GLenum type, void** _indices, GLsizei drawcount);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiDrawElementsBaseVertex(GLenum mode, GLsizei* count, GLenum type, void** _indices, GLsizei drawcount, GLint* _basevertex);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiDrawElementsBaseVertexEXT(GLenum mode, GLsizei* count, GLenum type, void** _indices, GLsizei drawcount, GLint* _basevertex);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiDrawElementsEXT(GLenum mode, GLsizei* count, GLenum type, void** _indices, GLsizei primcount);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiDrawElementsIndirect(GLenum mode, GLenum type, void* _indirect, GLsizei drawcount, GLsizei stride);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiDrawElementsIndirectAMD(GLenum mode, GLenum type, void* _indirect, GLsizei primcount, GLsizei stride);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiDrawElementsIndirectBindlessCountNV(GLenum mode, GLenum type, void* _indirect, GLsizei drawCount, GLsizei maxDrawCount, GLsizei stride, GLint vertexBufferCount);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiDrawElementsIndirectBindlessNV(GLenum mode, GLenum type, void* _indirect, GLsizei drawCount, GLsizei stride, GLint vertexBufferCount);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiDrawElementsIndirectCount(GLenum mode, GLenum type, void* _indirect, GLintptr drawcount, GLsizei maxdrawcount, GLsizei stride);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiDrawElementsIndirectCountARB(GLenum mode, GLenum type, void* _indirect, GLintptr drawcount, GLsizei maxdrawcount, GLsizei stride);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiDrawElementsIndirectEXT(GLenum mode, GLenum type, void* _indirect, GLsizei drawcount, GLsizei stride);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiDrawMeshTasksIndirectNV(GLintptr _indirect, GLsizei drawcount, GLsizei stride);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiDrawMeshTasksIndirectCountNV(GLintptr _indirect, GLintptr drawcount, GLsizei maxdrawcount, GLsizei stride);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiDrawRangeElementArrayAPPLE(GLenum mode, GLuint start, GLuint end, GLint* first, GLsizei* count, GLsizei primcount);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiModeDrawArraysIBM(GLenum* mode, GLint* first, GLsizei* count, GLsizei primcount, GLint modestride);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiModeDrawElementsIBM(GLenum* mode, GLsizei* count, GLenum type, void** _indices, GLsizei primcount, GLint modestride);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexBufferEXT(GLenum texunit, GLenum target, GLenum _internalformat, GLuint buffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord1bOES(GLenum texture, GLbyte s);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord1bvOES(GLenum texture, GLbyte* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord1d(GLenum target, GLdouble s);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord1dARB(GLenum target, GLdouble s);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord1dv(GLenum target, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord1dvARB(GLenum target, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord1f(GLenum target, GLfloat s);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord1fARB(GLenum target, GLfloat s);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord1fv(GLenum target, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord1fvARB(GLenum target, GLfloat* v);



    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord1i(GLenum target, GLint s);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord1iARB(GLenum target, GLint s);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord1iv(GLenum target, GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord1ivARB(GLenum target, GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord1s(GLenum target, GLshort s);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord1sARB(GLenum target, GLshort s);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord1sv(GLenum target, GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord1svARB(GLenum target, GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord1xOES(GLenum texture, GLfixed s);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord1xvOES(GLenum texture, GLfixed* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord2bOES(GLenum texture, GLbyte s, GLbyte t);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord2bvOES(GLenum texture, GLbyte* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord2d(GLenum target, GLdouble s, GLdouble t);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord2dARB(GLenum target, GLdouble s, GLdouble t);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord2dv(GLenum target, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord2dvARB(GLenum target, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord2f(GLenum target, GLfloat s, GLfloat t);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord2fARB(GLenum target, GLfloat s, GLfloat t);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord2fv(GLenum target, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord2fvARB(GLenum target, GLfloat* v);



    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord2i(GLenum target, GLint s, GLint t);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord2iARB(GLenum target, GLint s, GLint t);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord2iv(GLenum target, GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord2ivARB(GLenum target, GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord2s(GLenum target, GLshort s, GLshort t);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord2sARB(GLenum target, GLshort s, GLshort t);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord2sv(GLenum target, GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord2svARB(GLenum target, GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord2xOES(GLenum texture, GLfixed s, GLfixed t);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord2xvOES(GLenum texture, GLfixed* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord3bOES(GLenum texture, GLbyte s, GLbyte t, GLbyte r);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord3bvOES(GLenum texture, GLbyte* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord3d(GLenum target, GLdouble s, GLdouble t, GLdouble r);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord3dARB(GLenum target, GLdouble s, GLdouble t, GLdouble r);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord3dv(GLenum target, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord3dvARB(GLenum target, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord3f(GLenum target, GLfloat s, GLfloat t, GLfloat r);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord3fARB(GLenum target, GLfloat s, GLfloat t, GLfloat r);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord3fv(GLenum target, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord3fvARB(GLenum target, GLfloat* v);



    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord3i(GLenum target, GLint s, GLint t, GLint r);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord3iARB(GLenum target, GLint s, GLint t, GLint r);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord3iv(GLenum target, GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord3ivARB(GLenum target, GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord3s(GLenum target, GLshort s, GLshort t, GLshort r);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord3sARB(GLenum target, GLshort s, GLshort t, GLshort r);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord3sv(GLenum target, GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord3svARB(GLenum target, GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord3xOES(GLenum texture, GLfixed s, GLfixed t, GLfixed r);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord3xvOES(GLenum texture, GLfixed* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord4bOES(GLenum texture, GLbyte s, GLbyte t, GLbyte r, GLbyte q);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord4bvOES(GLenum texture, GLbyte* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord4d(GLenum target, GLdouble s, GLdouble t, GLdouble r, GLdouble q);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord4dARB(GLenum target, GLdouble s, GLdouble t, GLdouble r, GLdouble q);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord4dv(GLenum target, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord4dvARB(GLenum target, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord4f(GLenum target, GLfloat s, GLfloat t, GLfloat r, GLfloat q);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord4fARB(GLenum target, GLfloat s, GLfloat t, GLfloat r, GLfloat q);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord4fv(GLenum target, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord4fvARB(GLenum target, GLfloat* v);



    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord4i(GLenum target, GLint s, GLint t, GLint r, GLint q);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord4iARB(GLenum target, GLint s, GLint t, GLint r, GLint q);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord4iv(GLenum target, GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord4ivARB(GLenum target, GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord4s(GLenum target, GLshort s, GLshort t, GLshort r, GLshort q);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord4sARB(GLenum target, GLshort s, GLshort t, GLshort r, GLshort q);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord4sv(GLenum target, GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord4svARB(GLenum target, GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord4x(GLenum texture, GLfixed s, GLfixed t, GLfixed r, GLfixed q);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord4xOES(GLenum texture, GLfixed s, GLfixed t, GLfixed r, GLfixed q);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoord4xvOES(GLenum texture, GLfixed* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoordP1ui(GLenum texture, GLenum type, GLuint coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoordP1uiv(GLenum texture, GLenum type, GLuint* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoordP2ui(GLenum texture, GLenum type, GLuint coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoordP2uiv(GLenum texture, GLenum type, GLuint* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoordP3ui(GLenum texture, GLenum type, GLuint coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoordP3uiv(GLenum texture, GLenum type, GLuint* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoordP4ui(GLenum texture, GLenum type, GLuint coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoordP4uiv(GLenum texture, GLenum type, GLuint* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexCoordPointerEXT(GLenum texunit, GLint size, GLenum type, GLsizei stride, void* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexEnvfEXT(GLenum texunit, GLenum target, GLenum pname, GLfloat param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexEnvfvEXT(GLenum texunit, GLenum target, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexEnviEXT(GLenum texunit, GLenum target, GLenum pname, GLint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexEnvivEXT(GLenum texunit, GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexGendEXT(GLenum texunit, GLenum coord, GLenum pname, GLdouble param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexGendvEXT(GLenum texunit, GLenum coord, GLenum pname, GLdouble* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexGenfEXT(GLenum texunit, GLenum coord, GLenum pname, GLfloat param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexGenfvEXT(GLenum texunit, GLenum coord, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexGeniEXT(GLenum texunit, GLenum coord, GLenum pname, GLint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexGenivEXT(GLenum texunit, GLenum coord, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexImage1DEXT(GLenum texunit, GLenum target, GLint level, GLint _internalformat, GLsizei width, GLint border, GLenum format, GLenum type, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexImage2DEXT(GLenum texunit, GLenum target, GLint level, GLint _internalformat, GLsizei width, GLsizei height, GLint border, GLenum format, GLenum type, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexImage3DEXT(GLenum texunit, GLenum target, GLint level, GLint _internalformat, GLsizei width, GLsizei height, GLsizei depth, GLint border, GLenum format, GLenum type, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexParameterIivEXT(GLenum texunit, GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexParameterIuivEXT(GLenum texunit, GLenum target, GLenum pname, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexParameterfEXT(GLenum texunit, GLenum target, GLenum pname, GLfloat param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexParameterfvEXT(GLenum texunit, GLenum target, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexParameteriEXT(GLenum texunit, GLenum target, GLenum pname, GLint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexParameterivEXT(GLenum texunit, GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexRenderbufferEXT(GLenum texunit, GLenum target, GLuint renderbuffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexSubImage1DEXT(GLenum texunit, GLenum target, GLint level, GLint xoffset, GLsizei width, GLenum format, GLenum type, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexSubImage2DEXT(GLenum texunit, GLenum target, GLint level, GLint xoffset, GLint yoffset, GLsizei width, GLsizei height, GLenum format, GLenum type, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMultiTexSubImage3DEXT(GLenum texunit, GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLsizei width, GLsizei height, GLsizei depth, GLenum format, GLenum type, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMulticastBarrierNV();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMulticastBlitFramebufferNV(GLuint srcGpu, GLuint dstGpu, GLint srcX0, GLint srcY0, GLint srcX1, GLint srcY1, GLint dstX0, GLint dstY0, GLint dstX1, GLint dstY1, GLbitfield mask, GLenum filter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMulticastBufferSubDataNV(GLbitfield gpuMask, GLuint buffer, GLintptr offset, GLsizeiptr size, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMulticastCopyBufferSubDataNV(GLuint readGpu, GLbitfield writeGpuMask, GLuint readBuffer, GLuint writeBuffer, GLintptr readOffset, GLintptr writeOffset, GLsizeiptr size);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMulticastCopyImageSubDataNV(GLuint srcGpu, GLbitfield dstGpuMask, GLuint srcName, GLenum srcTarget, GLint srcLevel, GLint srcX, GLint srcY, GLint srcZ, GLuint dstName, GLenum dstTarget, GLint dstLevel, GLint dstX, GLint dstY, GLint dstZ, GLsizei srcWidth, GLsizei srcHeight, GLsizei srcDepth);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMulticastFramebufferSampleLocationsfvNV(GLuint gpu, GLuint framebuffer, GLuint start, GLsizei count, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMulticastGetQueryObjecti64vNV(GLuint gpu, GLuint id, GLenum pname, GLint64* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMulticastGetQueryObjectivNV(GLuint gpu, GLuint id, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMulticastGetQueryObjectui64vNV(GLuint gpu, GLuint id, GLenum pname, GLuint64* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMulticastGetQueryObjectuivNV(GLuint gpu, GLuint id, GLenum pname, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMulticastScissorArrayvNVX(GLuint gpu, GLuint first, GLsizei count, GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMulticastViewportArrayvNVX(GLuint gpu, GLuint first, GLsizei count, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMulticastViewportPositionWScaleNVX(GLuint gpu, GLuint _index, GLfloat xcoeff, GLfloat ycoeff);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glMulticastWaitSyncNV(GLuint signalGpu, GLbitfield waitGpuMask);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedBufferAttachMemoryNV(GLuint buffer, GLuint memory, GLuint64 offset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedBufferData(GLuint buffer, GLsizeiptr size, void* data, GLenum usage);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedBufferDataEXT(GLuint buffer, GLsizeiptr size, void* data, GLenum usage);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedBufferPageCommitmentARB(GLuint buffer, GLintptr offset, GLsizeiptr size, GLboolean commit);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedBufferPageCommitmentEXT(GLuint buffer, GLintptr offset, GLsizeiptr size, GLboolean commit);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedBufferPageCommitmentMemNV(GLuint buffer, GLintptr offset, GLsizeiptr size, GLuint memory, GLuint64 memOffset, GLboolean commit);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedBufferStorage(GLuint buffer, GLsizeiptr size, void* data, GLbitfield flags);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedBufferStorageExternalEXT(GLuint buffer, GLintptr offset, GLsizeiptr size, GLeglClientBufferEXT clientBuffer, GLbitfield flags);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedBufferStorageEXT(GLuint buffer, GLsizeiptr size, void* data, GLbitfield flags);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedBufferStorageMemEXT(GLuint buffer, GLsizeiptr size, GLuint memory, GLuint64 offset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedBufferSubData(GLuint buffer, GLintptr offset, GLsizeiptr size, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedBufferSubDataEXT(GLuint buffer, GLintptr offset, GLsizeiptr size, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedCopyBufferSubDataEXT(GLuint readBuffer, GLuint writeBuffer, GLintptr readOffset, GLintptr writeOffset, GLsizeiptr size);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedFramebufferDrawBuffer(GLuint framebuffer, GLenum buf);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedFramebufferDrawBuffers(GLuint framebuffer, GLsizei n, GLenum* bufs);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedFramebufferParameteri(GLuint framebuffer, GLenum pname, GLint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedFramebufferParameteriEXT(GLuint framebuffer, GLenum pname, GLint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedFramebufferReadBuffer(GLuint framebuffer, GLenum src);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedFramebufferRenderbuffer(GLuint framebuffer, GLenum attachment, GLenum renderbuffertarget, GLuint renderbuffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedFramebufferRenderbufferEXT(GLuint framebuffer, GLenum attachment, GLenum renderbuffertarget, GLuint renderbuffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedFramebufferSampleLocationsfvARB(GLuint framebuffer, GLuint start, GLsizei count, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedFramebufferSampleLocationsfvNV(GLuint framebuffer, GLuint start, GLsizei count, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedFramebufferTexture(GLuint framebuffer, GLenum attachment, GLuint texture, GLint level);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedFramebufferSamplePositionsfvAMD(GLuint framebuffer, GLuint numsamples, GLuint pixel_index, GLfloat* values);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedFramebufferTexture1DEXT(GLuint framebuffer, GLenum attachment, GLenum textarget, GLuint texture, GLint level);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedFramebufferTexture2DEXT(GLuint framebuffer, GLenum attachment, GLenum textarget, GLuint texture, GLint level);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedFramebufferTexture3DEXT(GLuint framebuffer, GLenum attachment, GLenum textarget, GLuint texture, GLint level, GLint zoffset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedFramebufferTextureEXT(GLuint framebuffer, GLenum attachment, GLuint texture, GLint level);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedFramebufferTextureFaceEXT(GLuint framebuffer, GLenum attachment, GLuint texture, GLint level, GLenum face);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedFramebufferTextureLayer(GLuint framebuffer, GLenum attachment, GLuint texture, GLint level, GLint layer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedFramebufferTextureLayerEXT(GLuint framebuffer, GLenum attachment, GLuint texture, GLint level, GLint layer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedProgramLocalParameter4dEXT(GLuint program, GLenum target, GLuint _index, GLdouble x, GLdouble y, GLdouble z, GLdouble w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedProgramLocalParameter4dvEXT(GLuint program, GLenum target, GLuint _index, GLdouble* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedProgramLocalParameter4fEXT(GLuint program, GLenum target, GLuint _index, GLfloat x, GLfloat y, GLfloat z, GLfloat w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedProgramLocalParameter4fvEXT(GLuint program, GLenum target, GLuint _index, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedProgramLocalParameterI4iEXT(GLuint program, GLenum target, GLuint _index, GLint x, GLint y, GLint z, GLint w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedProgramLocalParameterI4ivEXT(GLuint program, GLenum target, GLuint _index, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedProgramLocalParameterI4uiEXT(GLuint program, GLenum target, GLuint _index, GLuint x, GLuint y, GLuint z, GLuint w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedProgramLocalParameterI4uivEXT(GLuint program, GLenum target, GLuint _index, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedProgramLocalParameters4fvEXT(GLuint program, GLenum target, GLuint _index, GLsizei count, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedProgramLocalParametersI4ivEXT(GLuint program, GLenum target, GLuint _index, GLsizei count, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedProgramLocalParametersI4uivEXT(GLuint program, GLenum target, GLuint _index, GLsizei count, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedProgramStringEXT(GLuint program, GLenum target, GLenum format, GLsizei len, void* str);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedRenderbufferStorage(GLuint renderbuffer, GLenum _internalformat, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedRenderbufferStorageEXT(GLuint renderbuffer, GLenum _internalformat, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedRenderbufferStorageMultisample(GLuint renderbuffer, GLsizei samples, GLenum _internalformat, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedRenderbufferStorageMultisampleAdvancedAMD(GLuint renderbuffer, GLsizei samples, GLsizei storageSamples, GLenum _internalformat, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedRenderbufferStorageMultisampleCoverageEXT(GLuint renderbuffer, GLsizei coverageSamples, GLsizei colorSamples, GLenum _internalformat, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedRenderbufferStorageMultisampleEXT(GLuint renderbuffer, GLsizei samples, GLenum _internalformat, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNamedStringARB(GLenum type, GLint namelen, GLchar* name, GLint strlen, GLchar* str);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNewList(GLuint list, GLenum mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLuint glNewObjectBufferATI(GLsizei size, void* po_inter, GLenum usage);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNormal3b(GLbyte nx, GLbyte ny, GLbyte nz);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNormal3bv(GLbyte* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNormal3d(GLdouble nx, GLdouble ny, GLdouble nz);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNormal3dv(GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNormal3f(GLfloat nx, GLfloat ny, GLfloat nz);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNormal3fVertex3fSUN(GLfloat nx, GLfloat ny, GLfloat nz, GLfloat x, GLfloat y, GLfloat z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNormal3fVertex3fvSUN(GLfloat* n, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNormal3fv(GLfloat* v);



    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNormal3i(GLint nx, GLint ny, GLint nz);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNormal3iv(GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNormal3s(GLshort nx, GLshort ny, GLshort nz);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNormal3sv(GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNormal3x(GLfixed nx, GLfixed ny, GLfixed nz);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNormal3xOES(GLfixed nx, GLfixed ny, GLfixed nz);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNormal3xvOES(GLfixed* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNormalFormatNV(GLenum type, GLsizei stride);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNormalP3ui(GLenum type, GLuint coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNormalP3uiv(GLenum type, GLuint* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNormalPointer(GLenum type, GLsizei stride, void* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNormalPointerEXT(GLenum type, GLsizei stride, GLsizei count, void* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNormalPointerListIBM(GLenum type, GLint stride, void** po_inter, GLint ptrstride);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNormalPointervINTEL(GLenum type, void** po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNormalStream3bATI(GLenum stream, GLbyte nx, GLbyte ny, GLbyte nz);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNormalStream3bvATI(GLenum stream, GLbyte* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNormalStream3dATI(GLenum stream, GLdouble nx, GLdouble ny, GLdouble nz);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNormalStream3dvATI(GLenum stream, GLdouble* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNormalStream3fATI(GLenum stream, GLfloat nx, GLfloat ny, GLfloat nz);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNormalStream3fvATI(GLenum stream, GLfloat* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNormalStream3iATI(GLenum stream, GLint nx, GLint ny, GLint nz);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNormalStream3ivATI(GLenum stream, GLint* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNormalStream3sATI(GLenum stream, GLshort nx, GLshort ny, GLshort nz);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glNormalStream3svATI(GLenum stream, GLshort* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glObjectLabel(GLenum identifier, GLuint name, GLsizei length, GLchar* label);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glObjectLabelKHR(GLenum identifier, GLuint name, GLsizei length, GLchar* label);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glObjectPtrLabel(void* ptr, GLsizei length, GLchar* label);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glObjectPtrLabelKHR(void* ptr, GLsizei length, GLchar* label);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLenum glObjectPurgeableAPPLE(GLenum objType, GLuint name, GLenum option);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLenum glObjectUnpurgeableAPPLE(GLenum objType, GLuint name, GLenum option);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glOrtho(GLdouble left, GLdouble right, GLdouble bottom, GLdouble top, GLdouble zNear, GLdouble zFar);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glOrthof(GLfloat l, GLfloat r, GLfloat b, GLfloat t, GLfloat n, GLfloat f);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glOrthofOES(GLfloat l, GLfloat r, GLfloat b, GLfloat t, GLfloat n, GLfloat f);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glOrthox(GLfixed l, GLfixed r, GLfixed b, GLfixed t, GLfixed n, GLfixed f);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glOrthoxOES(GLfixed l, GLfixed r, GLfixed b, GLfixed t, GLfixed n, GLfixed f);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPNTrianglesfATI(GLenum pname, GLfloat param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPNTrianglesiATI(GLenum pname, GLint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPassTexCoordATI(GLuint dst, GLuint coord, GLenum swizzle);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPassThrough(GLfloat token);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPassThroughxOES(GLfixed token);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPatchParameterfv(GLenum pname, GLfloat* values);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPatchParameteri(GLenum pname, GLint value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPatchParameteriEXT(GLenum pname, GLint value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPatchParameteriOES(GLenum pname, GLint value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPathColorGenNV(GLenum color, GLenum genMode, GLenum colorFormat, GLfloat* coeffs);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPathCommandsNV(GLuint path, GLsizei numCommands, GLubyte* commands, GLsizei numCoords, GLenum coordType, void* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPathCoordsNV(GLuint path, GLsizei numCoords, GLenum coordType, void* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPathCoverDepthFuncNV(GLenum func);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPathDashArrayNV(GLuint path, GLsizei dashCount, GLfloat* dashArray);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPathFogGenNV(GLenum genMode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLenum glPathGlyphIndexArrayNV(GLuint firstPathName, GLenum fontTarget, void* fontName, GLbitfield fontStyle, GLuint firstGlyphIndex, GLsizei numGlyphs, GLuint pathParameterTemplate, GLfloat emScale);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLenum glPathGlyphIndexRangeNV(GLenum fontTarget, void* fontName, GLbitfield fontStyle, GLuint pathParameterTemplate, GLfloat emScale, GLuint* _baseAndCount);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPathGlyphRangeNV(GLuint firstPathName, GLenum fontTarget, void* fontName, GLbitfield fontStyle, GLuint firstGlyph, GLsizei numGlyphs, GLenum handleMiss_ingGlyphs, GLuint pathParameterTemplate, GLfloat emScale);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPathGlyphsNV(GLuint firstPathName, GLenum fontTarget, void* fontName, GLbitfield fontStyle, GLsizei numGlyphs, GLenum type, void* charcodes, GLenum handleMiss_ingGlyphs, GLuint pathParameterTemplate, GLfloat emScale);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLenum glPathMemoryGlyphIndexArrayNV(GLuint firstPathName, GLenum fontTarget, GLsizeiptr fontSize, void* fontData, GLsizei faceIndex, GLuint firstGlyphIndex, GLsizei numGlyphs, GLuint pathParameterTemplate, GLfloat emScale);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPathParameterfNV(GLuint path, GLenum pname, GLfloat value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPathParameterfvNV(GLuint path, GLenum pname, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPathParameteriNV(GLuint path, GLenum pname, GLint value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPathParameterivNV(GLuint path, GLenum pname, GLint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPathStencilDepthOffsetNV(GLfloat factor, GLfloat units);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPathStencilFuncNV(GLenum func, GLint refer, GLuint mask);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPathStringNV(GLuint path, GLenum format, GLsizei length, void* pathStr_ing);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPathSubCommandsNV(GLuint path, GLsizei commandStart, GLsizei commandsToDelete, GLsizei numCommands, GLubyte* commands, GLsizei numCoords, GLenum coordType, void* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPathSubCoordsNV(GLuint path, GLsizei coordStart, GLsizei numCoords, GLenum coordType, void* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPathTexGenNV(GLenum texCoordSet, GLenum genMode, GLint components, GLfloat* coeffs);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPauseTransformFeedback();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPauseTransformFeedbackNV();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPixelDataRangeNV(GLenum target, GLsizei length, void* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPixelMapfv(GLenum map, GLsizei mapsize, GLfloat* values);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPixelMapuiv(GLenum map, GLsizei mapsize, GLuint* values);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPixelMapusv(GLenum map, GLsizei mapsize, GLushort* values);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPixelMapx(GLenum map, GLint size, GLfixed* values);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPixelStoref(GLenum pname, GLfloat param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPixelStorei(GLenum pname, GLint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPixelStorex(GLenum pname, GLfixed param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPixelTexGenParameterfSGIS(GLenum pname, GLfloat param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPixelTexGenParameterfvSGIS(GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPixelTexGenParameteriSGIS(GLenum pname, GLint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPixelTexGenParameterivSGIS(GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPixelTexGenSGIX(GLenum mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPixelTransferf(GLenum pname, GLfloat param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPixelTransferi(GLenum pname, GLint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPixelTransferxOES(GLenum pname, GLfixed param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPixelTransformParameterfEXT(GLenum target, GLenum pname, GLfloat param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPixelTransformParameterfvEXT(GLenum target, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPixelTransformParameteriEXT(GLenum target, GLenum pname, GLint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPixelTransformParameterivEXT(GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPixelZoom(GLfloat xfactor, GLfloat yfactor);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPixelZoomxOES(GLfixed xfactor, GLfixed yfactor);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glPointAlongPathNV(GLuint path, GLsizei startSegment, GLsizei numSegments, GLfloat distance, GLfloat* x, GLfloat* y, GLfloat* tangentX, GLfloat* tangentY);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPointParameterf(GLenum pname, GLfloat param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPointParameterfARB(GLenum pname, GLfloat param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPointParameterfEXT(GLenum pname, GLfloat param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPointParameterfSGIS(GLenum pname, GLfloat param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPointParameterfv(GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPointParameterfvARB(GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPointParameterfvEXT(GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPointParameterfvSGIS(GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPointParameteri(GLenum pname, GLint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPointParameteriNV(GLenum pname, GLint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPointParameteriv(GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPointParameterivNV(GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPointParameterx(GLenum pname, GLfixed param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPointParameterxOES(GLenum pname, GLfixed param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPointParameterxv(GLenum pname, GLfixed* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPointParameterxvOES(GLenum pname, GLfixed* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPointSize(GLfloat size);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPointSizePointerOES(GLenum type, GLsizei stride, void* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPointSizex(GLfixed size);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPointSizexOES(GLfixed size);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLint glPollAsyncSGIX(GLuint* markerp);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLint glPollInstrumentsSGIX(GLint* marker_p);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPolygonMode(GLenum face, GLenum mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPolygonModeNV(GLenum face, GLenum mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPolygonOffset(GLfloat factor, GLfloat units);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPolygonOffsetClamp(GLfloat factor, GLfloat units, GLfloat clamp);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPolygonOffsetClampEXT(GLfloat factor, GLfloat units, GLfloat clamp);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPolygonOffsetEXT(GLfloat factor, GLfloat bias);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPolygonOffsetx(GLfixed factor, GLfixed units);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPolygonOffsetxOES(GLfixed factor, GLfixed units);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPolygonStipple(GLubyte* mask);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPopAttrib();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPopClientAttrib();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPopDebugGroup();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPopDebugGroupKHR();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPopGroupMarkerEXT();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPopMatrix();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPopName();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPresentFrameDualFillNV(GLuint video_slot, GLuint64EXT m_inPresentTime, GLuint beg_inPresentTimeId, GLuint presentDurationId, GLenum type, GLenum target0, GLuint fill0, GLenum target1, GLuint fill1, GLenum target2, GLuint fill2, GLenum target3, GLuint fill3);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPresentFrameKeyedNV(GLuint video_slot, GLuint64EXT m_inPresentTime, GLuint beg_inPresentTimeId, GLuint presentDurationId, GLenum type, GLenum target0, GLuint fill0, GLuint key0, GLenum target1, GLuint fill1, GLuint key1);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPrimitiveBoundingBox(GLfloat m_inX, GLfloat m_inY, GLfloat m_inZ, GLfloat m_inW, GLfloat maxX, GLfloat maxY, GLfloat maxZ, GLfloat maxW);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPrimitiveBoundingBoxARB(GLfloat m_inX, GLfloat m_inY, GLfloat m_inZ, GLfloat m_inW, GLfloat maxX, GLfloat maxY, GLfloat maxZ, GLfloat maxW);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPrimitiveBoundingBoxEXT(GLfloat m_inX, GLfloat m_inY, GLfloat m_inZ, GLfloat m_inW, GLfloat maxX, GLfloat maxY, GLfloat maxZ, GLfloat maxW);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPrimitiveBoundingBoxOES(GLfloat m_inX, GLfloat m_inY, GLfloat m_inZ, GLfloat m_inW, GLfloat maxX, GLfloat maxY, GLfloat maxZ, GLfloat maxW);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPrimitiveRestartIndex(GLuint _index);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPrimitiveRestartIndexNV(GLuint _index);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPrimitiveRestartNV();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPrioritizeTextures(GLsizei n, GLuint* textures, GLfloat* priorities);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPrioritizeTexturesEXT(GLsizei n, GLuint* textures, GLclampf* priorities);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPrioritizeTexturesxOES(GLsizei n, GLuint* textures, GLfixed* priorities);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramBinary(GLuint program, GLenum b_inaryFormat, void* b_inary, GLsizei length);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramBinaryOES(GLuint program, GLenum b_inaryFormat, void* b_inary, GLint length);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramBufferParametersIivNV(GLenum target, GLuint b_ind_ingIndex, GLuint wordIndex, GLsizei count, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramBufferParametersIuivNV(GLenum target, GLuint b_ind_ingIndex, GLuint wordIndex, GLsizei count, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramBufferParametersfvNV(GLenum target, GLuint b_ind_ingIndex, GLuint wordIndex, GLsizei count, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramEnvParameter4dARB(GLenum target, GLuint _index, GLdouble x, GLdouble y, GLdouble z, GLdouble w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramEnvParameter4dvARB(GLenum target, GLuint _index, GLdouble* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramEnvParameter4fARB(GLenum target, GLuint _index, GLfloat x, GLfloat y, GLfloat z, GLfloat w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramEnvParameter4fvARB(GLenum target, GLuint _index, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramEnvParameterI4iNV(GLenum target, GLuint _index, GLint x, GLint y, GLint z, GLint w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramEnvParameterI4ivNV(GLenum target, GLuint _index, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramEnvParameterI4uiNV(GLenum target, GLuint _index, GLuint x, GLuint y, GLuint z, GLuint w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramEnvParameterI4uivNV(GLenum target, GLuint _index, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramEnvParameters4fvEXT(GLenum target, GLuint _index, GLsizei count, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramEnvParametersI4ivNV(GLenum target, GLuint _index, GLsizei count, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramEnvParametersI4uivNV(GLenum target, GLuint _index, GLsizei count, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramLocalParameter4dARB(GLenum target, GLuint _index, GLdouble x, GLdouble y, GLdouble z, GLdouble w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramLocalParameter4dvARB(GLenum target, GLuint _index, GLdouble* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramLocalParameter4fARB(GLenum target, GLuint _index, GLfloat x, GLfloat y, GLfloat z, GLfloat w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramLocalParameter4fvARB(GLenum target, GLuint _index, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramLocalParameterI4iNV(GLenum target, GLuint _index, GLint x, GLint y, GLint z, GLint w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramLocalParameterI4ivNV(GLenum target, GLuint _index, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramLocalParameterI4uiNV(GLenum target, GLuint _index, GLuint x, GLuint y, GLuint z, GLuint w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramLocalParameterI4uivNV(GLenum target, GLuint _index, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramLocalParameters4fvEXT(GLenum target, GLuint _index, GLsizei count, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramLocalParametersI4ivNV(GLenum target, GLuint _index, GLsizei count, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramLocalParametersI4uivNV(GLenum target, GLuint _index, GLsizei count, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramNamedParameter4dNV(GLuint id, GLsizei len, GLubyte* name, GLdouble x, GLdouble y, GLdouble z, GLdouble w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramNamedParameter4dvNV(GLuint id, GLsizei len, GLubyte* name, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramNamedParameter4fNV(GLuint id, GLsizei len, GLubyte* name, GLfloat x, GLfloat y, GLfloat z, GLfloat w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramNamedParameter4fvNV(GLuint id, GLsizei len, GLubyte* name, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramParameter4dNV(GLenum target, GLuint _index, GLdouble x, GLdouble y, GLdouble z, GLdouble w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramParameter4dvNV(GLenum target, GLuint _index, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramParameter4fNV(GLenum target, GLuint _index, GLfloat x, GLfloat y, GLfloat z, GLfloat w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramParameter4fvNV(GLenum target, GLuint _index, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramParameteri(GLuint program, GLenum pname, GLint value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramParameteriARB(GLuint program, GLenum pname, GLint value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramParameteriEXT(GLuint program, GLenum pname, GLint value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramParameters4dvNV(GLenum target, GLuint _index, GLsizei count, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramParameters4fvNV(GLenum target, GLuint _index, GLsizei count, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramPathFragmentInputGenNV(GLuint program, GLint location, GLenum genMode, GLint components, GLfloat* coeffs);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramStringARB(GLenum target, GLenum format, GLsizei len, void* str);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramSubroutineParametersuivNV(GLenum target, GLsizei count, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform1d(GLuint program, GLint location, GLdouble v0);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform1dEXT(GLuint program, GLint location, GLdouble x);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform1dv(GLuint program, GLint location, GLsizei count, GLdouble* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform1dvEXT(GLuint program, GLint location, GLsizei count, GLdouble* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform1f(GLuint program, GLint location, GLfloat v0);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform1fEXT(GLuint program, GLint location, GLfloat v0);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform1fv(GLuint program, GLint location, GLsizei count, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform1fvEXT(GLuint program, GLint location, GLsizei count, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform1i(GLuint program, GLint location, GLint v0);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform1i64ARB(GLuint program, GLint location, GLint64 x);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform1i64NV(GLuint program, GLint location, GLint64EXT x);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform1i64vARB(GLuint program, GLint location, GLsizei count, GLint64* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform1i64vNV(GLuint program, GLint location, GLsizei count, GLint64EXT* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform1iEXT(GLuint program, GLint location, GLint v0);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform1iv(GLuint program, GLint location, GLsizei count, GLint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform1ivEXT(GLuint program, GLint location, GLsizei count, GLint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform1ui(GLuint program, GLint location, GLuint v0);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform1ui64ARB(GLuint program, GLint location, GLuint64 x);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform1ui64NV(GLuint program, GLint location, GLuint64EXT x);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform1ui64vARB(GLuint program, GLint location, GLsizei count, GLuint64* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform1ui64vNV(GLuint program, GLint location, GLsizei count, GLuint64EXT* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform1uiEXT(GLuint program, GLint location, GLuint v0);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform1uiv(GLuint program, GLint location, GLsizei count, GLuint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform1uivEXT(GLuint program, GLint location, GLsizei count, GLuint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform2d(GLuint program, GLint location, GLdouble v0, GLdouble v1);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform2dEXT(GLuint program, GLint location, GLdouble x, GLdouble y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform2dv(GLuint program, GLint location, GLsizei count, GLdouble* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform2dvEXT(GLuint program, GLint location, GLsizei count, GLdouble* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform2f(GLuint program, GLint location, GLfloat v0, GLfloat v1);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform2fEXT(GLuint program, GLint location, GLfloat v0, GLfloat v1);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform2fv(GLuint program, GLint location, GLsizei count, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform2fvEXT(GLuint program, GLint location, GLsizei count, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform2i(GLuint program, GLint location, GLint v0, GLint v1);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform2i64ARB(GLuint program, GLint location, GLint64 x, GLint64 y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform2i64NV(GLuint program, GLint location, GLint64EXT x, GLint64EXT y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform2i64vARB(GLuint program, GLint location, GLsizei count, GLint64* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform2i64vNV(GLuint program, GLint location, GLsizei count, GLint64EXT* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform2iEXT(GLuint program, GLint location, GLint v0, GLint v1);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform2iv(GLuint program, GLint location, GLsizei count, GLint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform2ivEXT(GLuint program, GLint location, GLsizei count, GLint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform2ui(GLuint program, GLint location, GLuint v0, GLuint v1);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform2ui64ARB(GLuint program, GLint location, GLuint64 x, GLuint64 y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform2ui64NV(GLuint program, GLint location, GLuint64EXT x, GLuint64EXT y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform2ui64vARB(GLuint program, GLint location, GLsizei count, GLuint64* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform2ui64vNV(GLuint program, GLint location, GLsizei count, GLuint64EXT* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform2uiEXT(GLuint program, GLint location, GLuint v0, GLuint v1);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform2uiv(GLuint program, GLint location, GLsizei count, GLuint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform2uivEXT(GLuint program, GLint location, GLsizei count, GLuint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform3d(GLuint program, GLint location, GLdouble v0, GLdouble v1, GLdouble v2);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform3dEXT(GLuint program, GLint location, GLdouble x, GLdouble y, GLdouble z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform3dv(GLuint program, GLint location, GLsizei count, GLdouble* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform3dvEXT(GLuint program, GLint location, GLsizei count, GLdouble* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform3f(GLuint program, GLint location, GLfloat v0, GLfloat v1, GLfloat v2);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform3fEXT(GLuint program, GLint location, GLfloat v0, GLfloat v1, GLfloat v2);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform3fv(GLuint program, GLint location, GLsizei count, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform3fvEXT(GLuint program, GLint location, GLsizei count, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform3i(GLuint program, GLint location, GLint v0, GLint v1, GLint v2);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform3i64ARB(GLuint program, GLint location, GLint64 x, GLint64 y, GLint64 z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform3i64NV(GLuint program, GLint location, GLint64EXT x, GLint64EXT y, GLint64EXT z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform3i64vARB(GLuint program, GLint location, GLsizei count, GLint64* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform3i64vNV(GLuint program, GLint location, GLsizei count, GLint64EXT* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform3iEXT(GLuint program, GLint location, GLint v0, GLint v1, GLint v2);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform3iv(GLuint program, GLint location, GLsizei count, GLint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform3ivEXT(GLuint program, GLint location, GLsizei count, GLint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform3ui(GLuint program, GLint location, GLuint v0, GLuint v1, GLuint v2);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform3ui64ARB(GLuint program, GLint location, GLuint64 x, GLuint64 y, GLuint64 z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform3ui64NV(GLuint program, GLint location, GLuint64EXT x, GLuint64EXT y, GLuint64EXT z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform3ui64vARB(GLuint program, GLint location, GLsizei count, GLuint64* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform3ui64vNV(GLuint program, GLint location, GLsizei count, GLuint64EXT* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform3uiEXT(GLuint program, GLint location, GLuint v0, GLuint v1, GLuint v2);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform3uiv(GLuint program, GLint location, GLsizei count, GLuint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform3uivEXT(GLuint program, GLint location, GLsizei count, GLuint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform4d(GLuint program, GLint location, GLdouble v0, GLdouble v1, GLdouble v2, GLdouble v3);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform4dEXT(GLuint program, GLint location, GLdouble x, GLdouble y, GLdouble z, GLdouble w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform4dv(GLuint program, GLint location, GLsizei count, GLdouble* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform4dvEXT(GLuint program, GLint location, GLsizei count, GLdouble* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform4f(GLuint program, GLint location, GLfloat v0, GLfloat v1, GLfloat v2, GLfloat v3);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform4fEXT(GLuint program, GLint location, GLfloat v0, GLfloat v1, GLfloat v2, GLfloat v3);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform4fv(GLuint program, GLint location, GLsizei count, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform4fvEXT(GLuint program, GLint location, GLsizei count, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform4i(GLuint program, GLint location, GLint v0, GLint v1, GLint v2, GLint v3);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform4i64ARB(GLuint program, GLint location, GLint64 x, GLint64 y, GLint64 z, GLint64 w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform4i64NV(GLuint program, GLint location, GLint64EXT x, GLint64EXT y, GLint64EXT z, GLint64EXT w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform4i64vARB(GLuint program, GLint location, GLsizei count, GLint64* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform4i64vNV(GLuint program, GLint location, GLsizei count, GLint64EXT* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform4iEXT(GLuint program, GLint location, GLint v0, GLint v1, GLint v2, GLint v3);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform4iv(GLuint program, GLint location, GLsizei count, GLint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform4ivEXT(GLuint program, GLint location, GLsizei count, GLint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform4ui(GLuint program, GLint location, GLuint v0, GLuint v1, GLuint v2, GLuint v3);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform4ui64ARB(GLuint program, GLint location, GLuint64 x, GLuint64 y, GLuint64 z, GLuint64 w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform4ui64NV(GLuint program, GLint location, GLuint64EXT x, GLuint64EXT y, GLuint64EXT z, GLuint64EXT w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform4ui64vARB(GLuint program, GLint location, GLsizei count, GLuint64* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform4ui64vNV(GLuint program, GLint location, GLsizei count, GLuint64EXT* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform4uiEXT(GLuint program, GLint location, GLuint v0, GLuint v1, GLuint v2, GLuint v3);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform4uiv(GLuint program, GLint location, GLsizei count, GLuint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniform4uivEXT(GLuint program, GLint location, GLsizei count, GLuint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformHandleui64ARB(GLuint program, GLint location, GLuint64 value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformHandleui64IMG(GLuint program, GLint location, GLuint64 value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformHandleui64NV(GLuint program, GLint location, GLuint64 value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformHandleui64vARB(GLuint program, GLint location, GLsizei count, GLuint64* values);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformHandleui64vIMG(GLuint program, GLint location, GLsizei count, GLuint64* values);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformHandleui64vNV(GLuint program, GLint location, GLsizei count, GLuint64* values);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformMatrix2dv(GLuint program, GLint location, GLsizei count, GLboolean transpose, GLdouble* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformMatrix2dvEXT(GLuint program, GLint location, GLsizei count, GLboolean transpose, GLdouble* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformMatrix2fv(GLuint program, GLint location, GLsizei count, GLboolean transpose, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformMatrix2fvEXT(GLuint program, GLint location, GLsizei count, GLboolean transpose, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformMatrix2x3dv(GLuint program, GLint location, GLsizei count, GLboolean transpose, GLdouble* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformMatrix2x3dvEXT(GLuint program, GLint location, GLsizei count, GLboolean transpose, GLdouble* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformMatrix2x3fv(GLuint program, GLint location, GLsizei count, GLboolean transpose, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformMatrix2x3fvEXT(GLuint program, GLint location, GLsizei count, GLboolean transpose, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformMatrix2x4dv(GLuint program, GLint location, GLsizei count, GLboolean transpose, GLdouble* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformMatrix2x4dvEXT(GLuint program, GLint location, GLsizei count, GLboolean transpose, GLdouble* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformMatrix2x4fv(GLuint program, GLint location, GLsizei count, GLboolean transpose, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformMatrix2x4fvEXT(GLuint program, GLint location, GLsizei count, GLboolean transpose, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformMatrix3dv(GLuint program, GLint location, GLsizei count, GLboolean transpose, GLdouble* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformMatrix3dvEXT(GLuint program, GLint location, GLsizei count, GLboolean transpose, GLdouble* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformMatrix3fv(GLuint program, GLint location, GLsizei count, GLboolean transpose, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformMatrix3fvEXT(GLuint program, GLint location, GLsizei count, GLboolean transpose, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformMatrix3x2dv(GLuint program, GLint location, GLsizei count, GLboolean transpose, GLdouble* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformMatrix3x2dvEXT(GLuint program, GLint location, GLsizei count, GLboolean transpose, GLdouble* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformMatrix3x2fv(GLuint program, GLint location, GLsizei count, GLboolean transpose, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformMatrix3x2fvEXT(GLuint program, GLint location, GLsizei count, GLboolean transpose, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformMatrix3x4dv(GLuint program, GLint location, GLsizei count, GLboolean transpose, GLdouble* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformMatrix3x4dvEXT(GLuint program, GLint location, GLsizei count, GLboolean transpose, GLdouble* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformMatrix3x4fv(GLuint program, GLint location, GLsizei count, GLboolean transpose, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformMatrix3x4fvEXT(GLuint program, GLint location, GLsizei count, GLboolean transpose, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformMatrix4dv(GLuint program, GLint location, GLsizei count, GLboolean transpose, GLdouble* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformMatrix4dvEXT(GLuint program, GLint location, GLsizei count, GLboolean transpose, GLdouble* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformMatrix4fv(GLuint program, GLint location, GLsizei count, GLboolean transpose, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformMatrix4fvEXT(GLuint program, GLint location, GLsizei count, GLboolean transpose, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformMatrix4x2dv(GLuint program, GLint location, GLsizei count, GLboolean transpose, GLdouble* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformMatrix4x2dvEXT(GLuint program, GLint location, GLsizei count, GLboolean transpose, GLdouble* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformMatrix4x2fv(GLuint program, GLint location, GLsizei count, GLboolean transpose, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformMatrix4x2fvEXT(GLuint program, GLint location, GLsizei count, GLboolean transpose, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformMatrix4x3dv(GLuint program, GLint location, GLsizei count, GLboolean transpose, GLdouble* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformMatrix4x3dvEXT(GLuint program, GLint location, GLsizei count, GLboolean transpose, GLdouble* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformMatrix4x3fv(GLuint program, GLint location, GLsizei count, GLboolean transpose, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformMatrix4x3fvEXT(GLuint program, GLint location, GLsizei count, GLboolean transpose, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformui64NV(GLuint program, GLint location, GLuint64EXT value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramUniformui64vNV(GLuint program, GLint location, GLsizei count, GLuint64EXT* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProgramVertexLimitNV(GLenum target, GLint limit);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProvokingVertex(GLenum mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glProvokingVertexEXT(GLenum mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPushAttrib(GLbitfield mask);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPushClientAttrib(GLbitfield mask);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPushClientAttribDefaultEXT(GLbitfield mask);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPushDebugGroup(GLenum source, GLuint id, GLsizei length, GLchar* message);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPushDebugGroupKHR(GLenum source, GLuint id, GLsizei length, GLchar* message);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPushGroupMarkerEXT(GLsizei length, GLchar* marker);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPushMatrix();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glPushName(GLuint name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glQueryCounter(GLuint id, GLenum target);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glQueryCounterEXT(GLuint id, GLenum target);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLbitfield glQueryMatrixxOES(GLfixed* mantissa, GLint* exponent);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glQueryObjectParameteruiAMD(GLenum target, GLuint id, GLenum pname, GLuint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLint glQueryResourceNV(GLenum queryType, GLint tagId, GLuint count, GLint* buffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glQueryResourceTagNV(GLint tagId, GLchar* tagStr_ing);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRasterPos2d(GLdouble x, GLdouble y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRasterPos2dv(GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRasterPos2f(GLfloat x, GLfloat y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRasterPos2fv(GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRasterPos2i(GLint x, GLint y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRasterPos2iv(GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRasterPos2s(GLshort x, GLshort y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRasterPos2sv(GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRasterPos2xOES(GLfixed x, GLfixed y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRasterPos2xvOES(GLfixed* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRasterPos3d(GLdouble x, GLdouble y, GLdouble z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRasterPos3dv(GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRasterPos3f(GLfloat x, GLfloat y, GLfloat z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRasterPos3fv(GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRasterPos3i(GLint x, GLint y, GLint z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRasterPos3iv(GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRasterPos3s(GLshort x, GLshort y, GLshort z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRasterPos3sv(GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRasterPos3xOES(GLfixed x, GLfixed y, GLfixed z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRasterPos3xvOES(GLfixed* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRasterPos4d(GLdouble x, GLdouble y, GLdouble z, GLdouble w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRasterPos4dv(GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRasterPos4f(GLfloat x, GLfloat y, GLfloat z, GLfloat w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRasterPos4fv(GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRasterPos4i(GLint x, GLint y, GLint z, GLint w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRasterPos4iv(GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRasterPos4s(GLshort x, GLshort y, GLshort z, GLshort w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRasterPos4sv(GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRasterPos4xOES(GLfixed x, GLfixed y, GLfixed z, GLfixed w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRasterPos4xvOES(GLfixed* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRasterSamplesEXT(GLuint samples, GLboolean fixedsamplelocations);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glReadBuffer(GLenum src);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glReadBufferIndexedEXT(GLenum src, GLint _index);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glReadBufferNV(GLenum mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glReadInstrumentsSGIX(GLint marker);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glReadPixels(GLint x, GLint y, GLsizei width, GLsizei height, GLenum format, GLenum type, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glReadnPixels(GLint x, GLint y, GLsizei width, GLsizei height, GLenum format, GLenum type, GLsizei bufSize, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glReadnPixelsARB(GLint x, GLint y, GLsizei width, GLsizei height, GLenum format, GLenum type, GLsizei bufSize, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glReadnPixelsEXT(GLint x, GLint y, GLsizei width, GLsizei height, GLenum format, GLenum type, GLsizei bufSize, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glReadnPixelsKHR(GLint x, GLint y, GLsizei width, GLsizei height, GLenum format, GLenum type, GLsizei bufSize, void* data);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glReleaseKeyedMutexWin32EXT(GLuint memory, GLuint64 key);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRectd(GLdouble x1, GLdouble y1, GLdouble x2, GLdouble y2);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRectdv(GLdouble* v1, GLdouble* v2);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRectf(GLfloat x1, GLfloat y1, GLfloat x2, GLfloat y2);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRectfv(GLfloat* v1, GLfloat* v2);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRecti(GLint x1, GLint y1, GLint x2, GLint y2);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRectiv(GLint* v1, GLint* v2);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRects(GLshort x1, GLshort y1, GLshort x2, GLshort y2);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRectsv(GLshort* v1, GLshort* v2);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRectxOES(GLfixed x1, GLfixed y1, GLfixed x2, GLfixed y2);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRectxvOES(GLfixed* v1, GLfixed* v2);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glReferencePlaneSGIX(GLdouble* equation);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glReleaseShaderCompiler();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRenderGpuMaskNV(GLbitfield mask);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLint glRenderMode(GLenum mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRenderbufferStorage(GLenum target, GLenum _internalformat, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRenderbufferStorageEXT(GLenum target, GLenum _internalformat, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRenderbufferStorageMultisample(GLenum target, GLsizei samples, GLenum _internalformat, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRenderbufferStorageMultisampleANGLE(GLenum target, GLsizei samples, GLenum _internalformat, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRenderbufferStorageMultisampleAPPLE(GLenum target, GLsizei samples, GLenum _internalformat, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRenderbufferStorageMultisampleAdvancedAMD(GLenum target, GLsizei samples, GLsizei storageSamples, GLenum _internalformat, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRenderbufferStorageMultisampleCoverageNV(GLenum target, GLsizei coverageSamples, GLsizei colorSamples, GLenum _internalformat, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRenderbufferStorageMultisampleEXT(GLenum target, GLsizei samples, GLenum _internalformat, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRenderbufferStorageMultisampleIMG(GLenum target, GLsizei samples, GLenum _internalformat, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRenderbufferStorageMultisampleNV(GLenum target, GLsizei samples, GLenum _internalformat, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRenderbufferStorageOES(GLenum target, GLenum _internalformat, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glReplacementCodePointerSUN(GLenum type, GLsizei stride, void** po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glReplacementCodeubSUN(GLubyte code);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glReplacementCodeubvSUN(GLubyte* code);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glReplacementCodeuiColor3fVertex3fSUN(GLuint rc, GLfloat r, GLfloat g, GLfloat b, GLfloat x, GLfloat y, GLfloat z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glReplacementCodeuiColor3fVertex3fvSUN(GLuint* rc, GLfloat* c, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glReplacementCodeuiColor4fNormal3fVertex3fSUN(GLuint rc, GLfloat r, GLfloat g, GLfloat b, GLfloat a, GLfloat nx, GLfloat ny, GLfloat nz, GLfloat x, GLfloat y, GLfloat z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glReplacementCodeuiColor4fNormal3fVertex3fvSUN(GLuint* rc, GLfloat* c, GLfloat* n, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glReplacementCodeuiColor4ubVertex3fSUN(GLuint rc, GLubyte r, GLubyte g, GLubyte b, GLubyte a, GLfloat x, GLfloat y, GLfloat z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glReplacementCodeuiColor4ubVertex3fvSUN(GLuint* rc, GLubyte* c, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glReplacementCodeuiNormal3fVertex3fSUN(GLuint rc, GLfloat nx, GLfloat ny, GLfloat nz, GLfloat x, GLfloat y, GLfloat z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glReplacementCodeuiNormal3fVertex3fvSUN(GLuint* rc, GLfloat* n, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glReplacementCodeuiSUN(GLuint code);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glReplacementCodeuiTexCoord2fColor4fNormal3fVertex3fSUN(GLuint rc, GLfloat s, GLfloat t, GLfloat r, GLfloat g, GLfloat b, GLfloat a, GLfloat nx, GLfloat ny, GLfloat nz, GLfloat x, GLfloat y, GLfloat z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glReplacementCodeuiTexCoord2fColor4fNormal3fVertex3fvSUN(GLuint* rc, GLfloat* tc, GLfloat* c, GLfloat* n, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glReplacementCodeuiTexCoord2fNormal3fVertex3fSUN(GLuint rc, GLfloat s, GLfloat t, GLfloat nx, GLfloat ny, GLfloat nz, GLfloat x, GLfloat y, GLfloat z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glReplacementCodeuiTexCoord2fNormal3fVertex3fvSUN(GLuint* rc, GLfloat* tc, GLfloat* n, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glReplacementCodeuiTexCoord2fVertex3fSUN(GLuint rc, GLfloat s, GLfloat t, GLfloat x, GLfloat y, GLfloat z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glReplacementCodeuiTexCoord2fVertex3fvSUN(GLuint* rc, GLfloat* tc, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glReplacementCodeuiVertex3fSUN(GLuint rc, GLfloat x, GLfloat y, GLfloat z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glReplacementCodeuiVertex3fvSUN(GLuint* rc, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glReplacementCodeuivSUN(GLuint* code);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glReplacementCodeusSUN(GLushort code);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glReplacementCodeusvSUN(GLushort* code);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRequestResidentProgramsNV(GLsizei n, GLuint* programs);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glResetHistogram(GLenum target);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glResetHistogramEXT(GLenum target);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glResetMemoryObjectParameterNV(GLuint memory, GLenum pname);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glResetMinmax(GLenum target);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glResetMinmaxEXT(GLenum target);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glResizeBuffersMESA();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glResolveDepthValuesNV();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glResolveMultisampleFramebufferAPPLE();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glResumeTransformFeedback();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glResumeTransformFeedbackNV();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRotated(GLdouble angle, GLdouble x, GLdouble y, GLdouble z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRotatef(GLfloat angle, GLfloat x, GLfloat y, GLfloat z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRotatex(GLfixed angle, GLfixed x, GLfixed y, GLfixed z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glRotatexOES(GLfixed angle, GLfixed x, GLfixed y, GLfixed z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSampleCoverage(GLfloat value, GLboolean _invert);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSampleCoverageARB(GLfloat value, GLboolean _invert);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSampleCoveragex(GLclampx value, GLboolean _invert);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSampleCoveragexOES(GLclampx value, GLboolean _invert);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSampleMapATI(GLuint dst, GLuint _interp, GLenum swizzle);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSampleMaskEXT(GLclampf value, GLboolean _invert);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSampleMaskIndexedNV(GLuint _index, GLbitfield mask);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSampleMaskSGIS(GLclampf value, GLboolean _invert);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSampleMaski(GLuint maskNumber, GLbitfield mask);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSamplePatternEXT(GLenum pattern);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSamplePatternSGIS(GLenum pattern);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSamplerParameterIiv(GLuint sampler, GLenum pname, GLint* param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSamplerParameterIivEXT(GLuint sampler, GLenum pname, GLint* param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSamplerParameterIivOES(GLuint sampler, GLenum pname, GLint* param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSamplerParameterIuiv(GLuint sampler, GLenum pname, GLuint* param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSamplerParameterIuivEXT(GLuint sampler, GLenum pname, GLuint* param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSamplerParameterIuivOES(GLuint sampler, GLenum pname, GLuint* param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSamplerParameterf(GLuint sampler, GLenum pname, GLfloat param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSamplerParameterfv(GLuint sampler, GLenum pname, GLfloat* param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSamplerParameteri(GLuint sampler, GLenum pname, GLint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSamplerParameteriv(GLuint sampler, GLenum pname, GLint* param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glScaled(GLdouble x, GLdouble y, GLdouble z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glScalef(GLfloat x, GLfloat y, GLfloat z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glScalex(GLfixed x, GLfixed y, GLfixed z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glScalexOES(GLfixed x, GLfixed y, GLfixed z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glScissor(GLint x, GLint y, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glScissorArrayv(GLuint first, GLsizei count, GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glScissorArrayvNV(GLuint first, GLsizei count, GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glScissorArrayvOES(GLuint first, GLsizei count, GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glScissorExclusiveArrayvNV(GLuint first, GLsizei count, GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glScissorExclusiveNV(GLint x, GLint y, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glScissorIndexed(GLuint _index, GLint left, GLint bottom, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glScissorIndexedNV(GLuint _index, GLint left, GLint bottom, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glScissorIndexedOES(GLuint _index, GLint left, GLint bottom, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glScissorIndexedv(GLuint _index, GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glScissorIndexedvNV(GLuint _index, GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glScissorIndexedvOES(GLuint _index, GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSecondaryColor3b(GLbyte red, GLbyte green, GLbyte blue);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSecondaryColor3bEXT(GLbyte red, GLbyte green, GLbyte blue);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSecondaryColor3bv(GLbyte* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSecondaryColor3bvEXT(GLbyte* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSecondaryColor3d(GLdouble red, GLdouble green, GLdouble blue);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSecondaryColor3dEXT(GLdouble red, GLdouble green, GLdouble blue);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSecondaryColor3dv(GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSecondaryColor3dvEXT(GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSecondaryColor3f(GLfloat red, GLfloat green, GLfloat blue);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSecondaryColor3fEXT(GLfloat red, GLfloat green, GLfloat blue);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSecondaryColor3fv(GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSecondaryColor3fvEXT(GLfloat* v);



    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSecondaryColor3i(GLint red, GLint green, GLint blue);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSecondaryColor3iEXT(GLint red, GLint green, GLint blue);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSecondaryColor3iv(GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSecondaryColor3ivEXT(GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSecondaryColor3s(GLshort red, GLshort green, GLshort blue);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSecondaryColor3sEXT(GLshort red, GLshort green, GLshort blue);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSecondaryColor3sv(GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSecondaryColor3svEXT(GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSecondaryColor3ub(GLubyte red, GLubyte green, GLubyte blue);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSecondaryColor3ubEXT(GLubyte red, GLubyte green, GLubyte blue);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSecondaryColor3ubv(GLubyte* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSecondaryColor3ubvEXT(GLubyte* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSecondaryColor3ui(GLuint red, GLuint green, GLuint blue);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSecondaryColor3uiEXT(GLuint red, GLuint green, GLuint blue);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSecondaryColor3uiv(GLuint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSecondaryColor3uivEXT(GLuint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSecondaryColor3us(GLushort red, GLushort green, GLushort blue);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSecondaryColor3usEXT(GLushort red, GLushort green, GLushort blue);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSecondaryColor3usv(GLushort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSecondaryColor3usvEXT(GLushort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSecondaryColorFormatNV(GLint size, GLenum type, GLsizei stride);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSecondaryColorP3ui(GLenum type, GLuint color);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSecondaryColorP3uiv(GLenum type, GLuint* color);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSecondaryColorPointer(GLint size, GLenum type, GLsizei stride, void* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSecondaryColorPointerEXT(GLint size, GLenum type, GLsizei stride, void* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSecondaryColorPointerListIBM(GLint size, GLenum type, GLint stride, void** po_inter, GLint ptrstride);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSelectBuffer(GLsizei size, GLuint* buffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSelectPerfMonitorCountersAMD(GLuint monitor, GLboolean enable, GLuint group, GLint numCounters, GLuint* counterList);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSemaphoreParameterivNV(GLuint semaphore, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSemaphoreParameterui64vEXT(GLuint semaphore, GLenum pname, GLuint64* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSeparableFilter2D(GLenum target, GLenum _internalformat, GLsizei width, GLsizei height, GLenum format, GLenum type, void* row, void* column);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSeparableFilter2DEXT(GLenum target, GLenum _internalformat, GLsizei width, GLsizei height, GLenum format, GLenum type, void* row, void* column);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSetFenceAPPLE(GLuint fence);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSetFenceNV(GLuint fence, GLenum condition);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSetFragmentShaderConstantATI(GLuint dst, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSetInvariantEXT(GLuint id, GLenum type, void* addr);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSetLocalConstantEXT(GLuint id, GLenum type, void* addr);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSetMultisamplefvAMD(GLenum pname, GLuint _index, GLfloat* val);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glShadeModel(GLenum mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glShaderBinary(GLsizei count, GLuint* shaders, GLenum b_inaryFormat, void* b_inary, GLsizei length);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glShaderOp1EXT(GLenum op, GLuint res, GLuint arg1);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glShaderOp2EXT(GLenum op, GLuint res, GLuint arg1, GLuint arg2);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glShaderOp3EXT(GLenum op, GLuint res, GLuint arg1, GLuint arg2, GLuint arg3);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glShaderSource(GLuint shader, GLsizei count, GLchar** str, GLint* length);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glShaderSourceARB(GLhandleARB shaderObj, GLsizei count, GLcharARB** str, GLint* length);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glShaderStorageBlockBinding(GLuint program, GLuint storageBlockIndex, GLuint storageBlockB_ind_ing);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glShadingRateEXT(GLenum rate);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glShadingRateCombinerOpsEXT(GLenum comb_inerOp0, GLenum comb_inerOp1);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glShadingRateImageBarrierNV(GLboolean synchronize);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glShadingRateQCOM(GLenum rate);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glShadingRateImagePaletteNV(GLuint viewport, GLuint first, GLsizei count, GLenum* rates);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glShadingRateSampleOrderNV(GLenum order);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glShadingRateSampleOrderCustomNV(GLenum rate, GLuint samples, GLint* locations);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSharpenTexFuncSGIS(GLenum target, GLsizei n, GLfloat* po_ints);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSignalSemaphoreEXT(GLuint semaphore, GLuint numBufferBarriers, GLuint* buffers, GLuint numTextureBarriers, GLuint* textures, GLenum* dstLay_outs);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSignalSemaphoreui64NVX(GLuint signalGpu, GLsizei fenceObjectCount, GLuint* semaphoreArray, GLuint64* fenceValueArray);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSpecializeShader(GLuint shader, GLchar* pEntryPo_int, GLuint numSpecializationConstants, GLuint* pConstantIndex, GLuint* pConstantValue);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSpecializeShaderARB(GLuint shader, GLchar* pEntryPo_int, GLuint numSpecializationConstants, GLuint* pConstantIndex, GLuint* pConstantValue);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSpriteParameterfSGIX(GLenum pname, GLfloat param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSpriteParameterfvSGIX(GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSpriteParameteriSGIX(GLenum pname, GLint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSpriteParameterivSGIX(GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glStartInstrumentsSGIX();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glStartTilingQCOM(GLuint x, GLuint y, GLuint width, GLuint height, GLbitfield preserveMask);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glStateCaptureNV(GLuint state, GLenum mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glStencilClearTagEXT(GLsizei stencilTagBits, GLuint stencilClearTag);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glStencilFillPathInstancedNV(GLsizei numPaths, GLenum pathNameType, void* paths, GLuint pathBase, GLenum fillMode, GLuint mask, GLenum transformType, GLfloat* transformValues);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glStencilFillPathNV(GLuint path, GLenum fillMode, GLuint mask);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glStencilFunc(GLenum func, GLint refer, GLuint mask);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glStencilFuncSeparate(GLenum face, GLenum func, GLint refer, GLuint mask);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glStencilFuncSeparateATI(GLenum frontfunc, GLenum backfunc, GLint refer, GLuint mask);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glStencilMask(GLuint mask);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glStencilMaskSeparate(GLenum face, GLuint mask);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glStencilOp(GLenum fail, GLenum zfail, GLenum zpass);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glStencilOpSeparate(GLenum face, GLenum sfail, GLenum dpfail, GLenum dppass);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glStencilOpSeparateATI(GLenum face, GLenum sfail, GLenum dpfail, GLenum dppass);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glStencilOpValueAMD(GLenum face, GLuint value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glStencilStrokePathInstancedNV(GLsizei numPaths, GLenum pathNameType, void* paths, GLuint pathBase, GLint refererence, GLuint mask, GLenum transformType, GLfloat* transformValues);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glStencilStrokePathNV(GLuint path, GLint refererence, GLuint mask);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glStencilThenCoverFillPathInstancedNV(GLsizei numPaths, GLenum pathNameType, void* paths, GLuint pathBase, GLenum fillMode, GLuint mask, GLenum coverMode, GLenum transformType, GLfloat* transformValues);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glStencilThenCoverFillPathNV(GLuint path, GLenum fillMode, GLuint mask, GLenum coverMode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glStencilThenCoverStrokePathInstancedNV(GLsizei numPaths, GLenum pathNameType, void* paths, GLuint pathBase, GLint refererence, GLuint mask, GLenum coverMode, GLenum transformType, GLfloat* transformValues);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glStencilThenCoverStrokePathNV(GLuint path, GLint refererence, GLuint mask, GLenum coverMode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glStopInstrumentsSGIX(GLint marker);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glStringMarkerGREMEDY(GLsizei len, void* str);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSubpixelPrecisionBiasNV(GLuint xbits, GLuint ybits);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSwizzleEXT(GLuint res, GLuint _in, GLenum _outX, GLenum _outY, GLenum _outZ, GLenum _outW);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSyncTextureINTEL(GLuint texture);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTagSampleBufferSGIX();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTangent3bEXT(GLbyte tx, GLbyte ty, GLbyte tz);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTangent3bvEXT(GLbyte* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTangent3dEXT(GLdouble tx, GLdouble ty, GLdouble tz);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTangent3dvEXT(GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTangent3fEXT(GLfloat tx, GLfloat ty, GLfloat tz);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTangent3fvEXT(GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTangent3iEXT(GLint tx, GLint ty, GLint tz);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTangent3ivEXT(GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTangent3sEXT(GLshort tx, GLshort ty, GLshort tz);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTangent3svEXT(GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTangentPointerEXT(GLenum type, GLsizei stride, void* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTbufferMask3DFX(GLuint mask);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTessellationFactorAMD(GLfloat factor);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTessellationModeAMD(GLenum mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glTestFenceAPPLE(GLuint fence);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glTestFenceNV(GLuint fence);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glTestObjectAPPLE(GLenum obj, GLuint name);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexAttachMemoryNV(GLenum target, GLuint memory, GLuint64 offset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexBuffer(GLenum target, GLenum _internalformat, GLuint buffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexBufferARB(GLenum target, GLenum _internalformat, GLuint buffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexBufferEXT(GLenum target, GLenum _internalformat, GLuint buffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexBufferOES(GLenum target, GLenum _internalformat, GLuint buffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexBufferRange(GLenum target, GLenum _internalformat, GLuint buffer, GLintptr offset, GLsizeiptr size);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexBufferRangeEXT(GLenum target, GLenum _internalformat, GLuint buffer, GLintptr offset, GLsizeiptr size);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexBufferRangeOES(GLenum target, GLenum _internalformat, GLuint buffer, GLintptr offset, GLsizeiptr size);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexBumpParameterfvATI(GLenum pname, GLfloat* param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexBumpParameterivATI(GLenum pname, GLint* param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord1bOES(GLbyte s);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord1bvOES(GLbyte* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord1d(GLdouble s);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord1dv(GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord1f(GLfloat s);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord1fv(GLfloat* v);



    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord1i(GLint s);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord1iv(GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord1s(GLshort s);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord1sv(GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord1xOES(GLfixed s);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord1xvOES(GLfixed* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord2bOES(GLbyte s, GLbyte t);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord2bvOES(GLbyte* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord2d(GLdouble s, GLdouble t);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord2dv(GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord2f(GLfloat s, GLfloat t);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord2fColor3fVertex3fSUN(GLfloat s, GLfloat t, GLfloat r, GLfloat g, GLfloat b, GLfloat x, GLfloat y, GLfloat z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord2fColor3fVertex3fvSUN(GLfloat* tc, GLfloat* c, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord2fColor4fNormal3fVertex3fSUN(GLfloat s, GLfloat t, GLfloat r, GLfloat g, GLfloat b, GLfloat a, GLfloat nx, GLfloat ny, GLfloat nz, GLfloat x, GLfloat y, GLfloat z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord2fColor4fNormal3fVertex3fvSUN(GLfloat* tc, GLfloat* c, GLfloat* n, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord2fColor4ubVertex3fSUN(GLfloat s, GLfloat t, GLubyte r, GLubyte g, GLubyte b, GLubyte a, GLfloat x, GLfloat y, GLfloat z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord2fColor4ubVertex3fvSUN(GLfloat* tc, GLubyte* c, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord2fNormal3fVertex3fSUN(GLfloat s, GLfloat t, GLfloat nx, GLfloat ny, GLfloat nz, GLfloat x, GLfloat y, GLfloat z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord2fNormal3fVertex3fvSUN(GLfloat* tc, GLfloat* n, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord2fVertex3fSUN(GLfloat s, GLfloat t, GLfloat x, GLfloat y, GLfloat z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord2fVertex3fvSUN(GLfloat* tc, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord2fv(GLfloat* v);



    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord2i(GLint s, GLint t);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord2iv(GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord2s(GLshort s, GLshort t);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord2sv(GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord2xOES(GLfixed s, GLfixed t);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord2xvOES(GLfixed* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord3bOES(GLbyte s, GLbyte t, GLbyte r);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord3bvOES(GLbyte* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord3d(GLdouble s, GLdouble t, GLdouble r);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord3dv(GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord3f(GLfloat s, GLfloat t, GLfloat r);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord3fv(GLfloat* v);



    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord3i(GLint s, GLint t, GLint r);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord3iv(GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord3s(GLshort s, GLshort t, GLshort r);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord3sv(GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord3xOES(GLfixed s, GLfixed t, GLfixed r);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord3xvOES(GLfixed* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord4bOES(GLbyte s, GLbyte t, GLbyte r, GLbyte q);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord4bvOES(GLbyte* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord4d(GLdouble s, GLdouble t, GLdouble r, GLdouble q);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord4dv(GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord4f(GLfloat s, GLfloat t, GLfloat r, GLfloat q);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord4fColor4fNormal3fVertex4fSUN(GLfloat s, GLfloat t, GLfloat p, GLfloat q, GLfloat r, GLfloat g, GLfloat b, GLfloat a, GLfloat nx, GLfloat ny, GLfloat nz, GLfloat x, GLfloat y, GLfloat z, GLfloat w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord4fColor4fNormal3fVertex4fvSUN(GLfloat* tc, GLfloat* c, GLfloat* n, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord4fVertex4fSUN(GLfloat s, GLfloat t, GLfloat p, GLfloat q, GLfloat x, GLfloat y, GLfloat z, GLfloat w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord4fVertex4fvSUN(GLfloat* tc, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord4fv(GLfloat* v);



    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord4i(GLint s, GLint t, GLint r, GLint q);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord4iv(GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord4s(GLshort s, GLshort t, GLshort r, GLshort q);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord4sv(GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord4xOES(GLfixed s, GLfixed t, GLfixed r, GLfixed q);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoord4xvOES(GLfixed* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoordFormatNV(GLint size, GLenum type, GLsizei stride);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoordP1ui(GLenum type, GLuint coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoordP1uiv(GLenum type, GLuint* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoordP2ui(GLenum type, GLuint coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoordP2uiv(GLenum type, GLuint* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoordP3ui(GLenum type, GLuint coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoordP3uiv(GLenum type, GLuint* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoordP4ui(GLenum type, GLuint coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoordP4uiv(GLenum type, GLuint* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoordPointer(GLint size, GLenum type, GLsizei stride, void* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoordPointerEXT(GLint size, GLenum type, GLsizei stride, GLsizei count, void* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoordPointerListIBM(GLint size, GLenum type, GLint stride, void** po_inter, GLint ptrstride);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexCoordPointervINTEL(GLint size, GLenum type, void** po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexEnvf(GLenum target, GLenum pname, GLfloat param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexEnvfv(GLenum target, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexEnvi(GLenum target, GLenum pname, GLint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexEnviv(GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexEnvx(GLenum target, GLenum pname, GLfixed param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexEnvxOES(GLenum target, GLenum pname, GLfixed param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexEnvxv(GLenum target, GLenum pname, GLfixed* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexEnvxvOES(GLenum target, GLenum pname, GLfixed* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexEstimateMotionQCOM(GLuint refer, GLuint target, GLuint _output);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexEstimateMotionRegionsQCOM(GLuint refer, GLuint target, GLuint _output, GLuint mask);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glExtrapolateTex2DQCOM(GLuint src1, GLuint src2, GLuint _output, GLfloat scaleFactor);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexFilterFuncSGIS(GLenum target, GLenum filter, GLsizei n, GLfloat* weights);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexGend(GLenum coord, GLenum pname, GLdouble param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexGendv(GLenum coord, GLenum pname, GLdouble* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexGenf(GLenum coord, GLenum pname, GLfloat param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexGenfOES(GLenum coord, GLenum pname, GLfloat param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexGenfv(GLenum coord, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexGenfvOES(GLenum coord, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexGeni(GLenum coord, GLenum pname, GLint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexGeniOES(GLenum coord, GLenum pname, GLint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexGeniv(GLenum coord, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexGenivOES(GLenum coord, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexGenxOES(GLenum coord, GLenum pname, GLfixed param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexGenxvOES(GLenum coord, GLenum pname, GLfixed* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexImage1D(GLenum target, GLint level, GLint _internalformat, GLsizei width, GLint border, GLenum format, GLenum type, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexImage2D(GLenum target, GLint level, GLint _internalformat, GLsizei width, GLsizei height, GLint border, GLenum format, GLenum type, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexImage2DMultisample(GLenum target, GLsizei samples, GLenum _internalformat, GLsizei width, GLsizei height, GLboolean fixedsamplelocations);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexImage2DMultisampleCoverageNV(GLenum target, GLsizei coverageSamples, GLsizei colorSamples, GLint _internalFormat, GLsizei width, GLsizei height, GLboolean fixedSampleLocations);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexImage3D(GLenum target, GLint level, GLint _internalformat, GLsizei width, GLsizei height, GLsizei depth, GLint border, GLenum format, GLenum type, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexImage3DEXT(GLenum target, GLint level, GLenum _internalformat, GLsizei width, GLsizei height, GLsizei depth, GLint border, GLenum format, GLenum type, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexImage3DMultisample(GLenum target, GLsizei samples, GLenum _internalformat, GLsizei width, GLsizei height, GLsizei depth, GLboolean fixedsamplelocations);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexImage3DMultisampleCoverageNV(GLenum target, GLsizei coverageSamples, GLsizei colorSamples, GLint _internalFormat, GLsizei width, GLsizei height, GLsizei depth, GLboolean fixedSampleLocations);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexImage3DOES(GLenum target, GLint level, GLenum _internalformat, GLsizei width, GLsizei height, GLsizei depth, GLint border, GLenum format, GLenum type, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexImage4DSGIS(GLenum target, GLint level, GLenum _internalformat, GLsizei width, GLsizei height, GLsizei depth, GLsizei size4d, GLint border, GLenum format, GLenum type, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexPageCommitmentARB(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLsizei width, GLsizei height, GLsizei depth, GLboolean commit);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexPageCommitmentEXT(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLsizei width, GLsizei height, GLsizei depth, GLboolean commit);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexPageCommitmentMemNV(GLenum target, GLint layer, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLsizei width, GLsizei height, GLsizei depth, GLuint memory, GLuint64 offset, GLboolean commit);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexParameterIiv(GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexParameterIivEXT(GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexParameterIivOES(GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexParameterIuiv(GLenum target, GLenum pname, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexParameterIuivEXT(GLenum target, GLenum pname, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexParameterIuivOES(GLenum target, GLenum pname, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexParameterf(GLenum target, GLenum pname, GLfloat param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexParameterfv(GLenum target, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexParameteri(GLenum target, GLenum pname, GLint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexParameteriv(GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexParameterx(GLenum target, GLenum pname, GLfixed param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexParameterxOES(GLenum target, GLenum pname, GLfixed param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexParameterxv(GLenum target, GLenum pname, GLfixed* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexParameterxvOES(GLenum target, GLenum pname, GLfixed* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexRenderbufferNV(GLenum target, GLuint renderbuffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexStorage1D(GLenum target, GLsizei levels, GLenum _internalformat, GLsizei width);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexStorage1DEXT(GLenum target, GLsizei levels, GLenum _internalformat, GLsizei width);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexStorage2D(GLenum target, GLsizei levels, GLenum _internalformat, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexStorage2DEXT(GLenum target, GLsizei levels, GLenum _internalformat, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexStorage2DMultisample(GLenum target, GLsizei samples, GLenum _internalformat, GLsizei width, GLsizei height, GLboolean fixedsamplelocations);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexStorage3D(GLenum target, GLsizei levels, GLenum _internalformat, GLsizei width, GLsizei height, GLsizei depth);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexStorage3DEXT(GLenum target, GLsizei levels, GLenum _internalformat, GLsizei width, GLsizei height, GLsizei depth);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexStorage3DMultisample(GLenum target, GLsizei samples, GLenum _internalformat, GLsizei width, GLsizei height, GLsizei depth, GLboolean fixedsamplelocations);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexStorage3DMultisampleOES(GLenum target, GLsizei samples, GLenum _internalformat, GLsizei width, GLsizei height, GLsizei depth, GLboolean fixedsamplelocations);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexStorageAttribs2DEXT(GLenum target, GLsizei levels, GLenum _internalformat, GLsizei width, GLsizei height, GLint* attrib_list);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexStorageAttribs3DEXT(GLenum target, GLsizei levels, GLenum _internalformat, GLsizei width, GLsizei height, GLsizei depth, GLint* attrib_list);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexStorageMem1DEXT(GLenum target, GLsizei levels, GLenum _internalFormat, GLsizei width, GLuint memory, GLuint64 offset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexStorageMem2DEXT(GLenum target, GLsizei levels, GLenum _internalFormat, GLsizei width, GLsizei height, GLuint memory, GLuint64 offset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexStorageMem2DMultisampleEXT(GLenum target, GLsizei samples, GLenum _internalFormat, GLsizei width, GLsizei height, GLboolean fixedSampleLocations, GLuint memory, GLuint64 offset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexStorageMem3DEXT(GLenum target, GLsizei levels, GLenum _internalFormat, GLsizei width, GLsizei height, GLsizei depth, GLuint memory, GLuint64 offset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexStorageMem3DMultisampleEXT(GLenum target, GLsizei samples, GLenum _internalFormat, GLsizei width, GLsizei height, GLsizei depth, GLboolean fixedSampleLocations, GLuint memory, GLuint64 offset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexStorageSparseAMD(GLenum target, GLenum _internalFormat, GLsizei width, GLsizei height, GLsizei depth, GLsizei layers, GLbitfield flags);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexSubImage1D(GLenum target, GLint level, GLint xoffset, GLsizei width, GLenum format, GLenum type, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexSubImage1DEXT(GLenum target, GLint level, GLint xoffset, GLsizei width, GLenum format, GLenum type, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexSubImage2D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLsizei width, GLsizei height, GLenum format, GLenum type, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexSubImage2DEXT(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLsizei width, GLsizei height, GLenum format, GLenum type, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexSubImage3D(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLsizei width, GLsizei height, GLsizei depth, GLenum format, GLenum type, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexSubImage3DEXT(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLsizei width, GLsizei height, GLsizei depth, GLenum format, GLenum type, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexSubImage3DOES(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLsizei width, GLsizei height, GLsizei depth, GLenum format, GLenum type, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexSubImage4DSGIS(GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLint woffset, GLsizei width, GLsizei height, GLsizei depth, GLsizei size4d, GLenum format, GLenum type, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureAttachMemoryNV(GLuint texture, GLuint memory, GLuint64 offset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureBarrier();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureBarrierNV();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureBuffer(GLuint texture, GLenum _internalformat, GLuint buffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureBufferEXT(GLuint texture, GLenum target, GLenum _internalformat, GLuint buffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureBufferRange(GLuint texture, GLenum _internalformat, GLuint buffer, GLintptr offset, GLsizeiptr size);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureBufferRangeEXT(GLuint texture, GLenum target, GLenum _internalformat, GLuint buffer, GLintptr offset, GLsizeiptr size);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureColorMaskSGIS(GLboolean red, GLboolean green, GLboolean blue, GLboolean alpha);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureFoveationParametersQCOM(GLuint texture, GLuint layer, GLuint focalPo_int, GLfloat focalX, GLfloat focalY, GLfloat ga_inX, GLfloat ga_inY, GLfloat foveaArea);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureImage1DEXT(GLuint texture, GLenum target, GLint level, GLint _internalformat, GLsizei width, GLint border, GLenum format, GLenum type, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureImage2DEXT(GLuint texture, GLenum target, GLint level, GLint _internalformat, GLsizei width, GLsizei height, GLint border, GLenum format, GLenum type, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureImage2DMultisampleCoverageNV(GLuint texture, GLenum target, GLsizei coverageSamples, GLsizei colorSamples, GLint _internalFormat, GLsizei width, GLsizei height, GLboolean fixedSampleLocations);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureImage2DMultisampleNV(GLuint texture, GLenum target, GLsizei samples, GLint _internalFormat, GLsizei width, GLsizei height, GLboolean fixedSampleLocations);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureImage3DEXT(GLuint texture, GLenum target, GLint level, GLint _internalformat, GLsizei width, GLsizei height, GLsizei depth, GLint border, GLenum format, GLenum type, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureImage3DMultisampleCoverageNV(GLuint texture, GLenum target, GLsizei coverageSamples, GLsizei colorSamples, GLint _internalFormat, GLsizei width, GLsizei height, GLsizei depth, GLboolean fixedSampleLocations);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureImage3DMultisampleNV(GLuint texture, GLenum target, GLsizei samples, GLint _internalFormat, GLsizei width, GLsizei height, GLsizei depth, GLboolean fixedSampleLocations);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureLightEXT(GLenum pname);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureMaterialEXT(GLenum face, GLenum mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureNormalEXT(GLenum mode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexturePageCommitmentEXT(GLuint texture, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLsizei width, GLsizei height, GLsizei depth, GLboolean commit);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTexturePageCommitmentMemNV(GLuint texture, GLint layer, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLsizei width, GLsizei height, GLsizei depth, GLuint memory, GLuint64 offset, GLboolean commit);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureParameterIiv(GLuint texture, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureParameterIivEXT(GLuint texture, GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureParameterIuiv(GLuint texture, GLenum pname, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureParameterIuivEXT(GLuint texture, GLenum target, GLenum pname, GLuint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureParameterf(GLuint texture, GLenum pname, GLfloat param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureParameterfEXT(GLuint texture, GLenum target, GLenum pname, GLfloat param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureParameterfv(GLuint texture, GLenum pname, GLfloat* param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureParameterfvEXT(GLuint texture, GLenum target, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureParameteri(GLuint texture, GLenum pname, GLint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureParameteriEXT(GLuint texture, GLenum target, GLenum pname, GLint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureParameteriv(GLuint texture, GLenum pname, GLint* param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureParameterivEXT(GLuint texture, GLenum target, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureRangeAPPLE(GLenum target, GLsizei length, void* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureRenderbufferEXT(GLuint texture, GLenum target, GLuint renderbuffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureStorage1D(GLuint texture, GLsizei levels, GLenum _internalformat, GLsizei width);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureStorage1DEXT(GLuint texture, GLenum target, GLsizei levels, GLenum _internalformat, GLsizei width);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureStorage2D(GLuint texture, GLsizei levels, GLenum _internalformat, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureStorage2DEXT(GLuint texture, GLenum target, GLsizei levels, GLenum _internalformat, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureStorage2DMultisample(GLuint texture, GLsizei samples, GLenum _internalformat, GLsizei width, GLsizei height, GLboolean fixedsamplelocations);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureStorage2DMultisampleEXT(GLuint texture, GLenum target, GLsizei samples, GLenum _internalformat, GLsizei width, GLsizei height, GLboolean fixedsamplelocations);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureStorage3D(GLuint texture, GLsizei levels, GLenum _internalformat, GLsizei width, GLsizei height, GLsizei depth);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureStorage3DEXT(GLuint texture, GLenum target, GLsizei levels, GLenum _internalformat, GLsizei width, GLsizei height, GLsizei depth);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureStorage3DMultisample(GLuint texture, GLsizei samples, GLenum _internalformat, GLsizei width, GLsizei height, GLsizei depth, GLboolean fixedsamplelocations);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureStorage3DMultisampleEXT(GLuint texture, GLenum target, GLsizei samples, GLenum _internalformat, GLsizei width, GLsizei height, GLsizei depth, GLboolean fixedsamplelocations);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureStorageMem1DEXT(GLuint texture, GLsizei levels, GLenum _internalFormat, GLsizei width, GLuint memory, GLuint64 offset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureStorageMem2DEXT(GLuint texture, GLsizei levels, GLenum _internalFormat, GLsizei width, GLsizei height, GLuint memory, GLuint64 offset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureStorageMem2DMultisampleEXT(GLuint texture, GLsizei samples, GLenum _internalFormat, GLsizei width, GLsizei height, GLboolean fixedSampleLocations, GLuint memory, GLuint64 offset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureStorageMem3DEXT(GLuint texture, GLsizei levels, GLenum _internalFormat, GLsizei width, GLsizei height, GLsizei depth, GLuint memory, GLuint64 offset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureStorageMem3DMultisampleEXT(GLuint texture, GLsizei samples, GLenum _internalFormat, GLsizei width, GLsizei height, GLsizei depth, GLboolean fixedSampleLocations, GLuint memory, GLuint64 offset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureStorageSparseAMD(GLuint texture, GLenum target, GLenum _internalFormat, GLsizei width, GLsizei height, GLsizei depth, GLsizei layers, GLbitfield flags);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureSubImage1D(GLuint texture, GLint level, GLint xoffset, GLsizei width, GLenum format, GLenum type, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureSubImage1DEXT(GLuint texture, GLenum target, GLint level, GLint xoffset, GLsizei width, GLenum format, GLenum type, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureSubImage2D(GLuint texture, GLint level, GLint xoffset, GLint yoffset, GLsizei width, GLsizei height, GLenum format, GLenum type, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureSubImage2DEXT(GLuint texture, GLenum target, GLint level, GLint xoffset, GLint yoffset, GLsizei width, GLsizei height, GLenum format, GLenum type, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureSubImage3D(GLuint texture, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLsizei width, GLsizei height, GLsizei depth, GLenum format, GLenum type, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureSubImage3DEXT(GLuint texture, GLenum target, GLint level, GLint xoffset, GLint yoffset, GLint zoffset, GLsizei width, GLsizei height, GLsizei depth, GLenum format, GLenum type, void* pixels);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureView(GLuint texture, GLenum target, GLuint origtexture, GLenum _internalformat, GLuint m_inlevel, GLuint numlevels, GLuint m_inlayer, GLuint numlayers);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureViewEXT(GLuint texture, GLenum target, GLuint origtexture, GLenum _internalformat, GLuint m_inlevel, GLuint numlevels, GLuint m_inlayer, GLuint numlayers);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTextureViewOES(GLuint texture, GLenum target, GLuint origtexture, GLenum _internalformat, GLuint m_inlevel, GLuint numlevels, GLuint m_inlayer, GLuint numlayers);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTrackMatrixNV(GLenum target, GLuint address, GLenum matrix, GLenum transform);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTransformFeedbackAttribsNV(GLsizei count, GLint* attribs, GLenum bufferMode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTransformFeedbackBufferBase(GLuint xfb, GLuint _index, GLuint buffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTransformFeedbackBufferRange(GLuint xfb, GLuint _index, GLuint buffer, GLintptr offset, GLsizeiptr size);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTransformFeedbackStreamAttribsNV(GLsizei count, GLint* attribs, GLsizei nbuffers, GLint* bufstreams, GLenum bufferMode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTransformFeedbackVaryings(GLuint program, GLsizei count, GLchar** vary_ings, GLenum bufferMode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTransformFeedbackVaryingsEXT(GLuint program, GLsizei count, GLchar** vary_ings, GLenum bufferMode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTransformFeedbackVaryingsNV(GLuint program, GLsizei count, GLint* locations, GLenum bufferMode);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTransformPathNV(GLuint resultPath, GLuint srcPath, GLenum transformType, GLfloat* transformValues);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTranslated(GLdouble x, GLdouble y, GLdouble z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTranslatef(GLfloat x, GLfloat y, GLfloat z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTranslatex(GLfixed x, GLfixed y, GLfixed z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glTranslatexOES(GLfixed x, GLfixed y, GLfixed z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform1d(GLint location, GLdouble x);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform1dv(GLint location, GLsizei count, GLdouble* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform1f(GLint location, GLfloat v0);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform1fARB(GLint location, GLfloat v0);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform1fv(GLint location, GLsizei count, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform1fvARB(GLint location, GLsizei count, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform1i(GLint location, GLint v0);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform1i64ARB(GLint location, GLint64 x);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform1i64NV(GLint location, GLint64EXT x);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform1i64vARB(GLint location, GLsizei count, GLint64* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform1i64vNV(GLint location, GLsizei count, GLint64EXT* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform1iARB(GLint location, GLint v0);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform1iv(GLint location, GLsizei count, GLint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform1ivARB(GLint location, GLsizei count, GLint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform1ui(GLint location, GLuint v0);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform1ui64ARB(GLint location, GLuint64 x);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform1ui64NV(GLint location, GLuint64EXT x);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform1ui64vARB(GLint location, GLsizei count, GLuint64* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform1ui64vNV(GLint location, GLsizei count, GLuint64EXT* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform1uiEXT(GLint location, GLuint v0);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform1uiv(GLint location, GLsizei count, GLuint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform1uivEXT(GLint location, GLsizei count, GLuint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform2d(GLint location, GLdouble x, GLdouble y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform2dv(GLint location, GLsizei count, GLdouble* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform2f(GLint location, GLfloat v0, GLfloat v1);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform2fARB(GLint location, GLfloat v0, GLfloat v1);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform2fv(GLint location, GLsizei count, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform2fvARB(GLint location, GLsizei count, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform2i(GLint location, GLint v0, GLint v1);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform2i64ARB(GLint location, GLint64 x, GLint64 y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform2i64NV(GLint location, GLint64EXT x, GLint64EXT y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform2i64vARB(GLint location, GLsizei count, GLint64* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform2i64vNV(GLint location, GLsizei count, GLint64EXT* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform2iARB(GLint location, GLint v0, GLint v1);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform2iv(GLint location, GLsizei count, GLint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform2ivARB(GLint location, GLsizei count, GLint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform2ui(GLint location, GLuint v0, GLuint v1);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform2ui64ARB(GLint location, GLuint64 x, GLuint64 y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform2ui64NV(GLint location, GLuint64EXT x, GLuint64EXT y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform2ui64vARB(GLint location, GLsizei count, GLuint64* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform2ui64vNV(GLint location, GLsizei count, GLuint64EXT* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform2uiEXT(GLint location, GLuint v0, GLuint v1);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform2uiv(GLint location, GLsizei count, GLuint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform2uivEXT(GLint location, GLsizei count, GLuint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform3d(GLint location, GLdouble x, GLdouble y, GLdouble z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform3dv(GLint location, GLsizei count, GLdouble* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform3f(GLint location, GLfloat v0, GLfloat v1, GLfloat v2);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform3fARB(GLint location, GLfloat v0, GLfloat v1, GLfloat v2);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform3fv(GLint location, GLsizei count, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform3fvARB(GLint location, GLsizei count, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform3i(GLint location, GLint v0, GLint v1, GLint v2);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform3i64ARB(GLint location, GLint64 x, GLint64 y, GLint64 z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform3i64NV(GLint location, GLint64EXT x, GLint64EXT y, GLint64EXT z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform3i64vARB(GLint location, GLsizei count, GLint64* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform3i64vNV(GLint location, GLsizei count, GLint64EXT* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform3iARB(GLint location, GLint v0, GLint v1, GLint v2);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform3iv(GLint location, GLsizei count, GLint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform3ivARB(GLint location, GLsizei count, GLint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform3ui(GLint location, GLuint v0, GLuint v1, GLuint v2);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform3ui64ARB(GLint location, GLuint64 x, GLuint64 y, GLuint64 z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform3ui64NV(GLint location, GLuint64EXT x, GLuint64EXT y, GLuint64EXT z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform3ui64vARB(GLint location, GLsizei count, GLuint64* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform3ui64vNV(GLint location, GLsizei count, GLuint64EXT* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform3uiEXT(GLint location, GLuint v0, GLuint v1, GLuint v2);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform3uiv(GLint location, GLsizei count, GLuint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform3uivEXT(GLint location, GLsizei count, GLuint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform4d(GLint location, GLdouble x, GLdouble y, GLdouble z, GLdouble w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform4dv(GLint location, GLsizei count, GLdouble* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform4f(GLint location, GLfloat v0, GLfloat v1, GLfloat v2, GLfloat v3);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform4fARB(GLint location, GLfloat v0, GLfloat v1, GLfloat v2, GLfloat v3);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform4fv(GLint location, GLsizei count, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform4fvARB(GLint location, GLsizei count, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform4i(GLint location, GLint v0, GLint v1, GLint v2, GLint v3);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform4i64ARB(GLint location, GLint64 x, GLint64 y, GLint64 z, GLint64 w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform4i64NV(GLint location, GLint64EXT x, GLint64EXT y, GLint64EXT z, GLint64EXT w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform4i64vARB(GLint location, GLsizei count, GLint64* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform4i64vNV(GLint location, GLsizei count, GLint64EXT* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform4iARB(GLint location, GLint v0, GLint v1, GLint v2, GLint v3);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform4iv(GLint location, GLsizei count, GLint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform4ivARB(GLint location, GLsizei count, GLint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform4ui(GLint location, GLuint v0, GLuint v1, GLuint v2, GLuint v3);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform4ui64ARB(GLint location, GLuint64 x, GLuint64 y, GLuint64 z, GLuint64 w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform4ui64NV(GLint location, GLuint64EXT x, GLuint64EXT y, GLuint64EXT z, GLuint64EXT w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform4ui64vARB(GLint location, GLsizei count, GLuint64* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform4ui64vNV(GLint location, GLsizei count, GLuint64EXT* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform4uiEXT(GLint location, GLuint v0, GLuint v1, GLuint v2, GLuint v3);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform4uiv(GLint location, GLsizei count, GLuint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniform4uivEXT(GLint location, GLsizei count, GLuint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniformBlockBinding(GLuint program, GLuint uniformBlockIndex, GLuint uniformBlockB_ind_ing);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniformBufferEXT(GLuint program, GLint location, GLuint buffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniformHandleui64ARB(GLint location, GLuint64 value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniformHandleui64IMG(GLint location, GLuint64 value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniformHandleui64NV(GLint location, GLuint64 value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniformHandleui64vARB(GLint location, GLsizei count, GLuint64* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniformHandleui64vIMG(GLint location, GLsizei count, GLuint64* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniformHandleui64vNV(GLint location, GLsizei count, GLuint64* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniformMatrix2dv(GLint location, GLsizei count, GLboolean transpose, GLdouble* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniformMatrix2fv(GLint location, GLsizei count, GLboolean transpose, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniformMatrix2fvARB(GLint location, GLsizei count, GLboolean transpose, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniformMatrix2x3dv(GLint location, GLsizei count, GLboolean transpose, GLdouble* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniformMatrix2x3fv(GLint location, GLsizei count, GLboolean transpose, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniformMatrix2x3fvNV(GLint location, GLsizei count, GLboolean transpose, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniformMatrix2x4dv(GLint location, GLsizei count, GLboolean transpose, GLdouble* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniformMatrix2x4fv(GLint location, GLsizei count, GLboolean transpose, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniformMatrix2x4fvNV(GLint location, GLsizei count, GLboolean transpose, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniformMatrix3dv(GLint location, GLsizei count, GLboolean transpose, GLdouble* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniformMatrix3fv(GLint location, GLsizei count, GLboolean transpose, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniformMatrix3fvARB(GLint location, GLsizei count, GLboolean transpose, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniformMatrix3x2dv(GLint location, GLsizei count, GLboolean transpose, GLdouble* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniformMatrix3x2fv(GLint location, GLsizei count, GLboolean transpose, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniformMatrix3x2fvNV(GLint location, GLsizei count, GLboolean transpose, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniformMatrix3x4dv(GLint location, GLsizei count, GLboolean transpose, GLdouble* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniformMatrix3x4fv(GLint location, GLsizei count, GLboolean transpose, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniformMatrix3x4fvNV(GLint location, GLsizei count, GLboolean transpose, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniformMatrix4dv(GLint location, GLsizei count, GLboolean transpose, GLdouble* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniformMatrix4fv(GLint location, GLsizei count, GLboolean transpose, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniformMatrix4fvARB(GLint location, GLsizei count, GLboolean transpose, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniformMatrix4x2dv(GLint location, GLsizei count, GLboolean transpose, GLdouble* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniformMatrix4x2fv(GLint location, GLsizei count, GLboolean transpose, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniformMatrix4x2fvNV(GLint location, GLsizei count, GLboolean transpose, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniformMatrix4x3dv(GLint location, GLsizei count, GLboolean transpose, GLdouble* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniformMatrix4x3fv(GLint location, GLsizei count, GLboolean transpose, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniformMatrix4x3fvNV(GLint location, GLsizei count, GLboolean transpose, GLfloat* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniformSubroutinesuiv(GLenum shadertype, GLsizei count, GLuint* _indices);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniformui64NV(GLint location, GLuint64EXT value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUniformui64vNV(GLint location, GLsizei count, GLuint64EXT* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUnlockArraysEXT();

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glUnmapBuffer(GLenum target);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glUnmapBufferARB(GLenum target);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glUnmapBufferOES(GLenum target);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glUnmapNamedBuffer(GLuint buffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLboolean glUnmapNamedBufferEXT(GLuint buffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUnmapObjectBufferATI(GLuint buffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUnmapTexture2DINTEL(GLuint texture, GLint level);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUpdateObjectBufferATI(GLuint buffer, GLuint offset, GLsizei size, void* po_inter, GLenum preserve);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUploadGpuMaskNVX(GLbitfield mask);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUseProgram(GLuint program);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUseProgramObjectARB(GLhandleARB programObj);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUseProgramStages(GLuint pipel_ine, GLbitfield stages, GLuint program);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUseProgramStagesEXT(GLuint pipel_ine, GLbitfield stages, GLuint program);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glUseShaderProgramEXT(GLenum type, GLuint program);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVDPAUFiniNV();


    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVDPAUInitNV(void* vdpDevice, void* getProcAddress);









    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glValidateProgram(GLuint program);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glValidateProgramARB(GLhandleARB programObj);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glValidateProgramPipeline(GLuint pipel_ine);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glValidateProgramPipelineEXT(GLuint pipel_ine);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVariantArrayObjectATI(GLuint id, GLenum type, GLsizei stride, GLuint buffer, GLuint offset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVariantPointerEXT(GLuint id, GLenum type, GLuint stride, void* addr);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVariantbvEXT(GLuint id, GLbyte* addr);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVariantdvEXT(GLuint id, GLdouble* addr);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVariantfvEXT(GLuint id, GLfloat* addr);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVariantivEXT(GLuint id, GLint* addr);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVariantsvEXT(GLuint id, GLshort* addr);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVariantubvEXT(GLuint id, GLubyte* addr);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVariantuivEXT(GLuint id, GLuint* addr);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVariantusvEXT(GLuint id, GLushort* addr);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertex2bOES(GLbyte x, GLbyte y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertex2bvOES(GLbyte* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertex2d(GLdouble x, GLdouble y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertex2dv(GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertex2f(GLfloat x, GLfloat y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertex2fv(GLfloat* v);



    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertex2i(GLint x, GLint y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertex2iv(GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertex2s(GLshort x, GLshort y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertex2sv(GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertex2xOES(GLfixed x);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertex2xvOES(GLfixed* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertex3bOES(GLbyte x, GLbyte y, GLbyte z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertex3bvOES(GLbyte* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertex3d(GLdouble x, GLdouble y, GLdouble z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertex3dv(GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertex3f(GLfloat x, GLfloat y, GLfloat z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertex3fv(GLfloat* v);



    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertex3i(GLint x, GLint y, GLint z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertex3iv(GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertex3s(GLshort x, GLshort y, GLshort z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertex3sv(GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertex3xOES(GLfixed x, GLfixed y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertex3xvOES(GLfixed* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertex4bOES(GLbyte x, GLbyte y, GLbyte z, GLbyte w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertex4bvOES(GLbyte* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertex4d(GLdouble x, GLdouble y, GLdouble z, GLdouble w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertex4dv(GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertex4f(GLfloat x, GLfloat y, GLfloat z, GLfloat w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertex4fv(GLfloat* v);



    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertex4i(GLint x, GLint y, GLint z, GLint w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertex4iv(GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertex4s(GLshort x, GLshort y, GLshort z, GLshort w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertex4sv(GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertex4xOES(GLfixed x, GLfixed y, GLfixed z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertex4xvOES(GLfixed* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexArrayAttribBinding(GLuint vaobj, GLuint attrib_index, GLuint b_ind_ing_index);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexArrayAttribFormat(GLuint vaobj, GLuint attrib_index, GLint size, GLenum type, GLboolean normalized, GLuint relativeoffset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexArrayAttribIFormat(GLuint vaobj, GLuint attrib_index, GLint size, GLenum type, GLuint relativeoffset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexArrayAttribLFormat(GLuint vaobj, GLuint attrib_index, GLint size, GLenum type, GLuint relativeoffset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexArrayBindVertexBufferEXT(GLuint vaobj, GLuint b_ind_ing_index, GLuint buffer, GLintptr offset, GLsizei stride);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexArrayBindingDivisor(GLuint vaobj, GLuint b_ind_ing_index, GLuint divisor);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexArrayColorOffsetEXT(GLuint vaobj, GLuint buffer, GLint size, GLenum type, GLsizei stride, GLintptr offset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexArrayEdgeFlagOffsetEXT(GLuint vaobj, GLuint buffer, GLsizei stride, GLintptr offset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexArrayElementBuffer(GLuint vaobj, GLuint buffer);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexArrayFogCoordOffsetEXT(GLuint vaobj, GLuint buffer, GLenum type, GLsizei stride, GLintptr offset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexArrayIndexOffsetEXT(GLuint vaobj, GLuint buffer, GLenum type, GLsizei stride, GLintptr offset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexArrayMultiTexCoordOffsetEXT(GLuint vaobj, GLuint buffer, GLenum texunit, GLint size, GLenum type, GLsizei stride, GLintptr offset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexArrayNormalOffsetEXT(GLuint vaobj, GLuint buffer, GLenum type, GLsizei stride, GLintptr offset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexArrayParameteriAPPLE(GLenum pname, GLint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexArrayRangeAPPLE(GLsizei length, void* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexArrayRangeNV(GLsizei length, void* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexArraySecondaryColorOffsetEXT(GLuint vaobj, GLuint buffer, GLint size, GLenum type, GLsizei stride, GLintptr offset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexArrayTexCoordOffsetEXT(GLuint vaobj, GLuint buffer, GLint size, GLenum type, GLsizei stride, GLintptr offset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexArrayVertexAttribBindingEXT(GLuint vaobj, GLuint attrib_index, GLuint b_ind_ing_index);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexArrayVertexAttribDivisorEXT(GLuint vaobj, GLuint _index, GLuint divisor);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexArrayVertexAttribFormatEXT(GLuint vaobj, GLuint attrib_index, GLint size, GLenum type, GLboolean normalized, GLuint relativeoffset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexArrayVertexAttribIFormatEXT(GLuint vaobj, GLuint attrib_index, GLint size, GLenum type, GLuint relativeoffset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexArrayVertexAttribIOffsetEXT(GLuint vaobj, GLuint buffer, GLuint _index, GLint size, GLenum type, GLsizei stride, GLintptr offset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexArrayVertexAttribLFormatEXT(GLuint vaobj, GLuint attrib_index, GLint size, GLenum type, GLuint relativeoffset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexArrayVertexAttribLOffsetEXT(GLuint vaobj, GLuint buffer, GLuint _index, GLint size, GLenum type, GLsizei stride, GLintptr offset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexArrayVertexAttribOffsetEXT(GLuint vaobj, GLuint buffer, GLuint _index, GLint size, GLenum type, GLboolean normalized, GLsizei stride, GLintptr offset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexArrayVertexBindingDivisorEXT(GLuint vaobj, GLuint b_ind_ing_index, GLuint divisor);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexArrayVertexBuffer(GLuint vaobj, GLuint b_ind_ing_index, GLuint buffer, GLintptr offset, GLsizei stride);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexArrayVertexBuffers(GLuint vaobj, GLuint first, GLsizei count, GLuint* buffers, GLintptr* offsets, GLsizei* strides);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexArrayVertexOffsetEXT(GLuint vaobj, GLuint buffer, GLint size, GLenum type, GLsizei stride, GLintptr offset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib1d(GLuint _index, GLdouble x);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib1dARB(GLuint _index, GLdouble x);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib1dNV(GLuint _index, GLdouble x);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib1dv(GLuint _index, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib1dvARB(GLuint _index, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib1dvNV(GLuint _index, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib1f(GLuint _index, GLfloat x);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib1fARB(GLuint _index, GLfloat x);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib1fNV(GLuint _index, GLfloat x);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib1fv(GLuint _index, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib1fvARB(GLuint _index, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib1fvNV(GLuint _index, GLfloat* v);



    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib1s(GLuint _index, GLshort x);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib1sARB(GLuint _index, GLshort x);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib1sNV(GLuint _index, GLshort x);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib1sv(GLuint _index, GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib1svARB(GLuint _index, GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib1svNV(GLuint _index, GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib2d(GLuint _index, GLdouble x, GLdouble y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib2dARB(GLuint _index, GLdouble x, GLdouble y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib2dNV(GLuint _index, GLdouble x, GLdouble y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib2dv(GLuint _index, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib2dvARB(GLuint _index, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib2dvNV(GLuint _index, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib2f(GLuint _index, GLfloat x, GLfloat y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib2fARB(GLuint _index, GLfloat x, GLfloat y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib2fNV(GLuint _index, GLfloat x, GLfloat y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib2fv(GLuint _index, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib2fvARB(GLuint _index, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib2fvNV(GLuint _index, GLfloat* v);



    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib2s(GLuint _index, GLshort x, GLshort y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib2sARB(GLuint _index, GLshort x, GLshort y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib2sNV(GLuint _index, GLshort x, GLshort y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib2sv(GLuint _index, GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib2svARB(GLuint _index, GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib2svNV(GLuint _index, GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib3d(GLuint _index, GLdouble x, GLdouble y, GLdouble z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib3dARB(GLuint _index, GLdouble x, GLdouble y, GLdouble z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib3dNV(GLuint _index, GLdouble x, GLdouble y, GLdouble z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib3dv(GLuint _index, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib3dvARB(GLuint _index, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib3dvNV(GLuint _index, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib3f(GLuint _index, GLfloat x, GLfloat y, GLfloat z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib3fARB(GLuint _index, GLfloat x, GLfloat y, GLfloat z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib3fNV(GLuint _index, GLfloat x, GLfloat y, GLfloat z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib3fv(GLuint _index, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib3fvARB(GLuint _index, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib3fvNV(GLuint _index, GLfloat* v);



    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib3s(GLuint _index, GLshort x, GLshort y, GLshort z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib3sARB(GLuint _index, GLshort x, GLshort y, GLshort z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib3sNV(GLuint _index, GLshort x, GLshort y, GLshort z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib3sv(GLuint _index, GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib3svARB(GLuint _index, GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib3svNV(GLuint _index, GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4Nbv(GLuint _index, GLbyte* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4NbvARB(GLuint _index, GLbyte* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4Niv(GLuint _index, GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4NivARB(GLuint _index, GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4Nsv(GLuint _index, GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4NsvARB(GLuint _index, GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4Nub(GLuint _index, GLubyte x, GLubyte y, GLubyte z, GLubyte w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4NubARB(GLuint _index, GLubyte x, GLubyte y, GLubyte z, GLubyte w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4Nubv(GLuint _index, GLubyte* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4NubvARB(GLuint _index, GLubyte* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4Nuiv(GLuint _index, GLuint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4NuivARB(GLuint _index, GLuint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4Nusv(GLuint _index, GLushort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4NusvARB(GLuint _index, GLushort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4bv(GLuint _index, GLbyte* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4bvARB(GLuint _index, GLbyte* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4d(GLuint _index, GLdouble x, GLdouble y, GLdouble z, GLdouble w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4dARB(GLuint _index, GLdouble x, GLdouble y, GLdouble z, GLdouble w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4dNV(GLuint _index, GLdouble x, GLdouble y, GLdouble z, GLdouble w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4dv(GLuint _index, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4dvARB(GLuint _index, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4dvNV(GLuint _index, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4f(GLuint _index, GLfloat x, GLfloat y, GLfloat z, GLfloat w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4fARB(GLuint _index, GLfloat x, GLfloat y, GLfloat z, GLfloat w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4fNV(GLuint _index, GLfloat x, GLfloat y, GLfloat z, GLfloat w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4fv(GLuint _index, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4fvARB(GLuint _index, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4fvNV(GLuint _index, GLfloat* v);



    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4iv(GLuint _index, GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4ivARB(GLuint _index, GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4s(GLuint _index, GLshort x, GLshort y, GLshort z, GLshort w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4sARB(GLuint _index, GLshort x, GLshort y, GLshort z, GLshort w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4sNV(GLuint _index, GLshort x, GLshort y, GLshort z, GLshort w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4sv(GLuint _index, GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4svARB(GLuint _index, GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4svNV(GLuint _index, GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4ubNV(GLuint _index, GLubyte x, GLubyte y, GLubyte z, GLubyte w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4ubv(GLuint _index, GLubyte* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4ubvARB(GLuint _index, GLubyte* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4ubvNV(GLuint _index, GLubyte* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4uiv(GLuint _index, GLuint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4uivARB(GLuint _index, GLuint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4usv(GLuint _index, GLushort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttrib4usvARB(GLuint _index, GLushort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribArrayObjectATI(GLuint _index, GLint size, GLenum type, GLboolean normalized, GLsizei stride, GLuint buffer, GLuint offset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribBinding(GLuint attrib_index, GLuint b_ind_ing_index);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribDivisor(GLuint _index, GLuint divisor);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribDivisorANGLE(GLuint _index, GLuint divisor);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribDivisorARB(GLuint _index, GLuint divisor);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribDivisorEXT(GLuint _index, GLuint divisor);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribDivisorNV(GLuint _index, GLuint divisor);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribFormat(GLuint attrib_index, GLint size, GLenum type, GLboolean normalized, GLuint relativeoffset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribFormatNV(GLuint _index, GLint size, GLenum type, GLboolean normalized, GLsizei stride);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI1i(GLuint _index, GLint x);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI1iEXT(GLuint _index, GLint x);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI1iv(GLuint _index, GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI1ivEXT(GLuint _index, GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI1ui(GLuint _index, GLuint x);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI1uiEXT(GLuint _index, GLuint x);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI1uiv(GLuint _index, GLuint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI1uivEXT(GLuint _index, GLuint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI2i(GLuint _index, GLint x, GLint y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI2iEXT(GLuint _index, GLint x, GLint y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI2iv(GLuint _index, GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI2ivEXT(GLuint _index, GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI2ui(GLuint _index, GLuint x, GLuint y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI2uiEXT(GLuint _index, GLuint x, GLuint y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI2uiv(GLuint _index, GLuint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI2uivEXT(GLuint _index, GLuint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI3i(GLuint _index, GLint x, GLint y, GLint z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI3iEXT(GLuint _index, GLint x, GLint y, GLint z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI3iv(GLuint _index, GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI3ivEXT(GLuint _index, GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI3ui(GLuint _index, GLuint x, GLuint y, GLuint z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI3uiEXT(GLuint _index, GLuint x, GLuint y, GLuint z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI3uiv(GLuint _index, GLuint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI3uivEXT(GLuint _index, GLuint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI4bv(GLuint _index, GLbyte* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI4bvEXT(GLuint _index, GLbyte* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI4i(GLuint _index, GLint x, GLint y, GLint z, GLint w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI4iEXT(GLuint _index, GLint x, GLint y, GLint z, GLint w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI4iv(GLuint _index, GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI4ivEXT(GLuint _index, GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI4sv(GLuint _index, GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI4svEXT(GLuint _index, GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI4ubv(GLuint _index, GLubyte* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI4ubvEXT(GLuint _index, GLubyte* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI4ui(GLuint _index, GLuint x, GLuint y, GLuint z, GLuint w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI4uiEXT(GLuint _index, GLuint x, GLuint y, GLuint z, GLuint w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI4uiv(GLuint _index, GLuint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI4uivEXT(GLuint _index, GLuint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI4usv(GLuint _index, GLushort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribI4usvEXT(GLuint _index, GLushort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribIFormat(GLuint attrib_index, GLint size, GLenum type, GLuint relativeoffset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribIFormatNV(GLuint _index, GLint size, GLenum type, GLsizei stride);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribIPointer(GLuint _index, GLint size, GLenum type, GLsizei stride, void* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribIPointerEXT(GLuint _index, GLint size, GLenum type, GLsizei stride, void* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribL1d(GLuint _index, GLdouble x);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribL1dEXT(GLuint _index, GLdouble x);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribL1dv(GLuint _index, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribL1dvEXT(GLuint _index, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribL1i64NV(GLuint _index, GLint64EXT x);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribL1i64vNV(GLuint _index, GLint64EXT* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribL1ui64ARB(GLuint _index, GLuint64EXT x);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribL1ui64NV(GLuint _index, GLuint64EXT x);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribL1ui64vARB(GLuint _index, GLuint64EXT* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribL1ui64vNV(GLuint _index, GLuint64EXT* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribL2d(GLuint _index, GLdouble x, GLdouble y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribL2dEXT(GLuint _index, GLdouble x, GLdouble y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribL2dv(GLuint _index, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribL2dvEXT(GLuint _index, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribL2i64NV(GLuint _index, GLint64EXT x, GLint64EXT y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribL2i64vNV(GLuint _index, GLint64EXT* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribL2ui64NV(GLuint _index, GLuint64EXT x, GLuint64EXT y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribL2ui64vNV(GLuint _index, GLuint64EXT* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribL3d(GLuint _index, GLdouble x, GLdouble y, GLdouble z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribL3dEXT(GLuint _index, GLdouble x, GLdouble y, GLdouble z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribL3dv(GLuint _index, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribL3dvEXT(GLuint _index, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribL3i64NV(GLuint _index, GLint64EXT x, GLint64EXT y, GLint64EXT z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribL3i64vNV(GLuint _index, GLint64EXT* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribL3ui64NV(GLuint _index, GLuint64EXT x, GLuint64EXT y, GLuint64EXT z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribL3ui64vNV(GLuint _index, GLuint64EXT* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribL4d(GLuint _index, GLdouble x, GLdouble y, GLdouble z, GLdouble w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribL4dEXT(GLuint _index, GLdouble x, GLdouble y, GLdouble z, GLdouble w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribL4dv(GLuint _index, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribL4dvEXT(GLuint _index, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribL4i64NV(GLuint _index, GLint64EXT x, GLint64EXT y, GLint64EXT z, GLint64EXT w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribL4i64vNV(GLuint _index, GLint64EXT* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribL4ui64NV(GLuint _index, GLuint64EXT x, GLuint64EXT y, GLuint64EXT z, GLuint64EXT w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribL4ui64vNV(GLuint _index, GLuint64EXT* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribLFormat(GLuint attrib_index, GLint size, GLenum type, GLuint relativeoffset);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribLFormatNV(GLuint _index, GLint size, GLenum type, GLsizei stride);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribLPointer(GLuint _index, GLint size, GLenum type, GLsizei stride, void* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribLPointerEXT(GLuint _index, GLint size, GLenum type, GLsizei stride, void* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribP1ui(GLuint _index, GLenum type, GLboolean normalized, GLuint value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribP1uiv(GLuint _index, GLenum type, GLboolean normalized, GLuint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribP2ui(GLuint _index, GLenum type, GLboolean normalized, GLuint value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribP2uiv(GLuint _index, GLenum type, GLboolean normalized, GLuint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribP3ui(GLuint _index, GLenum type, GLboolean normalized, GLuint value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribP3uiv(GLuint _index, GLenum type, GLboolean normalized, GLuint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribP4ui(GLuint _index, GLenum type, GLboolean normalized, GLuint value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribP4uiv(GLuint _index, GLenum type, GLboolean normalized, GLuint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribParameteriAMD(GLuint _index, GLenum pname, GLint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribPointer(GLuint _index, GLint size, GLenum type, GLboolean normalized, GLsizei stride, void* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribPointerARB(GLuint _index, GLint size, GLenum type, GLboolean normalized, GLsizei stride, void* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribPointerNV(GLuint _index, GLint fsize, GLenum type, GLsizei stride, void* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribs1dvNV(GLuint _index, GLsizei count, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribs1fvNV(GLuint _index, GLsizei count, GLfloat* v);


    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribs1svNV(GLuint _index, GLsizei count, GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribs2dvNV(GLuint _index, GLsizei count, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribs2fvNV(GLuint _index, GLsizei count, GLfloat* v);


    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribs2svNV(GLuint _index, GLsizei count, GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribs3dvNV(GLuint _index, GLsizei count, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribs3fvNV(GLuint _index, GLsizei count, GLfloat* v);


    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribs3svNV(GLuint _index, GLsizei count, GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribs4dvNV(GLuint _index, GLsizei count, GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribs4fvNV(GLuint _index, GLsizei count, GLfloat* v);


    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribs4svNV(GLuint _index, GLsizei count, GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexAttribs4ubvNV(GLuint _index, GLsizei count, GLubyte* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexBindingDivisor(GLuint b_ind_ing_index, GLuint divisor);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexBlendARB(GLint count);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexBlendEnvfATI(GLenum pname, GLfloat param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexBlendEnviATI(GLenum pname, GLint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexFormatNV(GLint size, GLenum type, GLsizei stride);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexP2ui(GLenum type, GLuint value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexP2uiv(GLenum type, GLuint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexP3ui(GLenum type, GLuint value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexP3uiv(GLenum type, GLuint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexP4ui(GLenum type, GLuint value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexP4uiv(GLenum type, GLuint* value);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexPointer(GLint size, GLenum type, GLsizei stride, void* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexPointerEXT(GLint size, GLenum type, GLsizei stride, GLsizei count, void* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexPointerListIBM(GLint size, GLenum type, GLint stride, void** po_inter, GLint ptrstride);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexPointervINTEL(GLint size, GLenum type, void** po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexStream1dATI(GLenum stream, GLdouble x);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexStream1dvATI(GLenum stream, GLdouble* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexStream1fATI(GLenum stream, GLfloat x);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexStream1fvATI(GLenum stream, GLfloat* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexStream1iATI(GLenum stream, GLint x);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexStream1ivATI(GLenum stream, GLint* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexStream1sATI(GLenum stream, GLshort x);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexStream1svATI(GLenum stream, GLshort* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexStream2dATI(GLenum stream, GLdouble x, GLdouble y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexStream2dvATI(GLenum stream, GLdouble* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexStream2fATI(GLenum stream, GLfloat x, GLfloat y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexStream2fvATI(GLenum stream, GLfloat* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexStream2iATI(GLenum stream, GLint x, GLint y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexStream2ivATI(GLenum stream, GLint* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexStream2sATI(GLenum stream, GLshort x, GLshort y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexStream2svATI(GLenum stream, GLshort* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexStream3dATI(GLenum stream, GLdouble x, GLdouble y, GLdouble z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexStream3dvATI(GLenum stream, GLdouble* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexStream3fATI(GLenum stream, GLfloat x, GLfloat y, GLfloat z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexStream3fvATI(GLenum stream, GLfloat* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexStream3iATI(GLenum stream, GLint x, GLint y, GLint z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexStream3ivATI(GLenum stream, GLint* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexStream3sATI(GLenum stream, GLshort x, GLshort y, GLshort z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexStream3svATI(GLenum stream, GLshort* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexStream4dATI(GLenum stream, GLdouble x, GLdouble y, GLdouble z, GLdouble w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexStream4dvATI(GLenum stream, GLdouble* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexStream4fATI(GLenum stream, GLfloat x, GLfloat y, GLfloat z, GLfloat w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexStream4fvATI(GLenum stream, GLfloat* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexStream4iATI(GLenum stream, GLint x, GLint y, GLint z, GLint w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexStream4ivATI(GLenum stream, GLint* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexStream4sATI(GLenum stream, GLshort x, GLshort y, GLshort z, GLshort w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexStream4svATI(GLenum stream, GLshort* coords);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexWeightPointerEXT(GLint size, GLenum type, GLsizei stride, void* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexWeightfEXT(GLfloat weight);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVertexWeightfvEXT(GLfloat* weight);



    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern GLenum glVideoCaptureNV(GLuint video_capture_slot, GLuint* sequence_num, GLuint64EXT* capture_time);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVideoCaptureStreamParameterdvNV(GLuint video_capture_slot, GLuint stream, GLenum pname, GLdouble* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVideoCaptureStreamParameterfvNV(GLuint video_capture_slot, GLuint stream, GLenum pname, GLfloat* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glVideoCaptureStreamParameterivNV(GLuint video_capture_slot, GLuint stream, GLenum pname, GLint* parameters);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glViewport(GLint x, GLint y, GLsizei width, GLsizei height);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glViewportArrayv(GLuint first, GLsizei count, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glViewportArrayvNV(GLuint first, GLsizei count, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glViewportArrayvOES(GLuint first, GLsizei count, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glViewportIndexedf(GLuint _index, GLfloat x, GLfloat y, GLfloat w, GLfloat h);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glViewportIndexedfOES(GLuint _index, GLfloat x, GLfloat y, GLfloat w, GLfloat h);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glViewportIndexedfNV(GLuint _index, GLfloat x, GLfloat y, GLfloat w, GLfloat h);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glViewportIndexedfv(GLuint _index, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glViewportIndexedfvOES(GLuint _index, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glViewportIndexedfvNV(GLuint _index, GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glViewportPositionWScaleNV(GLuint _index, GLfloat xcoeff, GLfloat ycoeff);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glViewportSwizzleNV(GLuint _index, GLenum swizzlex, GLenum swizzley, GLenum swizzlez, GLenum swizzlew);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWaitSemaphoreEXT(GLuint semaphore, GLuint numBufferBarriers, GLuint* buffers, GLuint numTextureBarriers, GLuint* textures, GLenum* srcLay_outs);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWaitSemaphoreui64NVX(GLuint waitGpu, GLsizei fenceObjectCount, GLuint* semaphoreArray, GLuint64* fenceValueArray);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWaitSync(GLsync sync, GLbitfield flags, GLuint64 time_out);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWaitSyncAPPLE(GLsync sync, GLbitfield flags, GLuint64 time_out);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWeightPathsNV(GLuint resultPath, GLsizei numPaths, GLuint* paths, GLfloat* weights);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWeightPointerARB(GLint size, GLenum type, GLsizei stride, void* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWeightPointerOES(GLint size, GLenum type, GLsizei stride, void* po_inter);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWeightbvARB(GLint size, GLbyte* weights);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWeightdvARB(GLint size, GLdouble* weights);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWeightfvARB(GLint size, GLfloat* weights);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWeightivARB(GLint size, GLint* weights);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWeightsvARB(GLint size, GLshort* weights);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWeightubvARB(GLint size, GLubyte* weights);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWeightuivARB(GLint size, GLuint* weights);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWeightusvARB(GLint size, GLushort* weights);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos2d(GLdouble x, GLdouble y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos2dARB(GLdouble x, GLdouble y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos2dMESA(GLdouble x, GLdouble y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos2dv(GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos2dvARB(GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos2dvMESA(GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos2f(GLfloat x, GLfloat y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos2fARB(GLfloat x, GLfloat y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos2fMESA(GLfloat x, GLfloat y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos2fv(GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos2fvARB(GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos2fvMESA(GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos2i(GLint x, GLint y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos2iARB(GLint x, GLint y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos2iMESA(GLint x, GLint y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos2iv(GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos2ivARB(GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos2ivMESA(GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos2s(GLshort x, GLshort y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos2sARB(GLshort x, GLshort y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos2sMESA(GLshort x, GLshort y);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos2sv(GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos2svARB(GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos2svMESA(GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos3d(GLdouble x, GLdouble y, GLdouble z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos3dARB(GLdouble x, GLdouble y, GLdouble z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos3dMESA(GLdouble x, GLdouble y, GLdouble z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos3dv(GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos3dvARB(GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos3dvMESA(GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos3f(GLfloat x, GLfloat y, GLfloat z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos3fARB(GLfloat x, GLfloat y, GLfloat z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos3fMESA(GLfloat x, GLfloat y, GLfloat z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos3fv(GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos3fvARB(GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos3fvMESA(GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos3i(GLint x, GLint y, GLint z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos3iARB(GLint x, GLint y, GLint z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos3iMESA(GLint x, GLint y, GLint z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos3iv(GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos3ivARB(GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos3ivMESA(GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos3s(GLshort x, GLshort y, GLshort z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos3sARB(GLshort x, GLshort y, GLshort z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos3sMESA(GLshort x, GLshort y, GLshort z);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos3sv(GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos3svARB(GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos3svMESA(GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos4dMESA(GLdouble x, GLdouble y, GLdouble z, GLdouble w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos4dvMESA(GLdouble* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos4fMESA(GLfloat x, GLfloat y, GLfloat z, GLfloat w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos4fvMESA(GLfloat* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos4iMESA(GLint x, GLint y, GLint z, GLint w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos4ivMESA(GLint* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos4sMESA(GLshort x, GLshort y, GLshort z, GLshort w);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowPos4svMESA(GLshort* v);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWindowRectanglesEXT(GLenum mode, GLsizei count, GLint* box);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWriteMaskEXT(GLuint res, GLuint _in, GLenum _outX, GLenum _outY, GLenum _outZ, GLenum _outW);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glDrawVkImageNV(GLuint64 vkImage, GLuint sampler, GLfloat x0, GLfloat y0, GLfloat x1, GLfloat y1, GLfloat z, GLfloat s0, GLfloat t0, GLfloat s1, GLfloat t1);


    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glWaitVkSemaphoreNV(GLuint64 vkSemaphore);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSignalVkSemaphoreNV(GLuint64 vkSemaphore);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glSignalVkFenceNV(GLuint64 vkFence);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glFramebufferParameteriMESA(GLenum target, GLenum pname, GLint param);

    [DllImport(NativeLib, CallingConvention = callConv)]
    public static extern void glGetFramebufferParameterivMESA(GLenum target, GLenum pname, GLint* parameters);

}
