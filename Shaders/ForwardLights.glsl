#pass FwdBase
#pass FwdAdd
#pass FwdDepth
#blend FwdBase true One Zero Add One Zero Add
#blend FwdAdd true One One Add One One Add
#blend FwdDepth true One Zero Add One Zero Add
#depth FwdBase true true LessEqual
#depth FwdAdd true false LessEqual
#depth FwdDepth true true LessEqual

#define LIGHTS_MAX 4

struct LightInfoStruct
{
    vec4 AmbientColor;
    vec4 LightPosition[$LIGHTS_MAX$];
    vec4 LightColor[$LIGHTS_MAX$];
};

struct ShadowInfoStruct
{
    vec4 LightPosition[$LIGHTS_MAX$*6];
    mat4 LightProjection[$LIGHTS_MAX$*6];
};

layout(set = $LIGHT_SET$, binding = 0) uniform LightInfo0
{
    LightInfoStruct LightInfo;
};

layout(set = 10, binding = 1) uniform texture2D ShadowAtlasTexture;
layout(set = 10, binding = 2) uniform sampler ShadowAtlasTextureSampler;
layout(set = 11, binding = 0) uniform LightInfo1
{
    ShadowInfoStruct ShadowInfo;
};

#pragma in fragment vec3 fsinLight_TanLightPos[$LIGHTS_MAX$]

#if vertex

void TransferLightInfo()
{
    for (int i = 0; i < $LIGHTS_MAX$; i++)
    {
        fsinLight_TanLightPos[i] = $OBJ_TO_TAN$ * LightInfo.LightPosition[i].xyz;
    }
}

#endif

#if fragment

float GetLightAttenuation(int i)
{
    // return 1;
    float attenuation = 0;
    // for (int i = 0; i < $LIGHTS_MAX$; i++)
    {
        float d = length(fsinLight_TanLightPos[i].xyz - $TAN_POSITION$.xyz);
        float a = mix(1, 0, d / LightInfo.LightPosition[i].w);
        // float a = 1 / max(d, 0.0001) / LightInfo.LightPosition[i].w;
        attenuation += clamp(a, 0, 1);
    }
    return attenuation;
}

vec3 ApplyAmbientLighting()
{
    return LightInfo.AmbientColor.rgb;
}

vec3 ApplyDiffuseLighting(int i)
{
    vec3 norm = -normalize($TAN_NORMAL$.xyz);
    vec3 outDiff = vec3(0);
    // for (int i = 0; i < $LIGHTS_MAX$; i++)
    {
        // vec4 lightPos = LightInfo.LightPosition[i].xyz;
        vec4 lightPos = vec4(fsinLight_TanLightPos[i].xyz, LightInfo.LightPosition[i].w);
        vec3 lightDir = normalize(lightPos.xyz - $TAN_POSITION$.xyz);
        float diff = max(dot(norm, lightDir), 0);
        outDiff += (LightInfo.LightColor[i].rgb * diff);
    }
    // outDiff /= LightInfo.AmbientColor.w;
    return outDiff;
}

vec3 ApplySpecularLighting(int i)
{
    vec3 norm = -normalize($TAN_NORMAL$.xyz);
    vec3 outDiff = vec3(0);
    // for (int i = 0; i < $LIGHTS_MAX$; i++)
    {
        // vec4 lightPos = LightInfo.LightPosition[i].xyz;
        vec4 lightPos = vec4(fsinLight_TanLightPos[i].xyz, LightInfo.LightPosition[i].w);
        vec3 lightDir = normalize(lightPos.xyz - $TAN_POSITION$.xyz);
        vec3 viewDir = normalize($TAN_VIEW_POSITION$.xyz - $TAN_POSITION$.xyz);
        vec3 halfwayDir = normalize(lightDir + viewDir);
        float spec = pow(max(dot(norm, halfwayDir), 0), $SPECULAR_EXP$);
        vec3 specular = LightInfo.LightColor[i].rgb * spec;
        outDiff += (specular);
    }
    // outDiff /= LightInfo.AmbientColor.w;
    return outDiff;
}

float ApplyAlphaLighting(int i, vec3 lighting, float a)
{
    // return (1 - $FwdAdd$);
    // return a;

    vec3 norm = -normalize($TAN_NORMAL$.xyz);
    vec4 lightPos = vec4(fsinLight_TanLightPos[i].xyz, LightInfo.LightPosition[i].w);
    vec3 lightDir = normalize(lightPos.xyz - $TAN_POSITION$.xyz);
    vec3 viewDir = normalize($TAN_VIEW_POSITION$.xyz - $TAN_POSITION$.xyz);
    vec3 halfwayDir = normalize(lightDir + viewDir);
    float diff = max(dot(norm, lightDir), 0);
    float spec = pow(max(dot(norm, halfwayDir), 0), $SPECULAR_EXP$);

    return (diff * a + spec * a);
}

vec4 ApplyLighting()
{
    vec4 col = vec4(ApplyAmbientLighting(), 0);
    for (int i = 0; i < LightInfo.AmbientColor.w; i++)
    {
        float a = GetLightAttenuation(i);
        vec3 lighting = ApplyDiffuseLighting(i) * a + ApplySpecularLighting(i) * a;
        vec4 fLight = vec4(lighting, a);// vec4(lighting, ApplyAlphaLighting(i, lighting, a));
        col.rgb += fLight.rgb;
        col.a += fLight.a;
    }
    col.a *= $FwdAdd$;
    return col;
}

vec4 ApplyLightingNoSpecular()
{
    vec4 col = vec4(ApplyAmbientLighting(), 0);
    for (int i = 0; i < LightInfo.AmbientColor.w; i++)
    {
        float a = GetLightAttenuation(i);
        vec3 lighting = ApplyDiffuseLighting(i) * a;
        vec4 fLight = vec4(lighting, a);// vec4(lighting, ApplyAlphaLighting(i, lighting, a));
        col.rgb += fLight.rgb;
        col.a += fLight.a;
    }
    col.a *= $FwdAdd$;
    return col;
}

vec2 GetAttenuation()
{
    vec2 col = vec2(0);
    for (int i = 0; i < LightInfo.AmbientColor.w; i++)
    {
        float a = GetLightAttenuation(i);
        col.x += a;
    }
    col.y *= $FwdAdd$;
    return col;
}

vec2 GetNormalVector()
{
    vec3 norm = -normalize($TAN_NORMAL$.xyz);
    vec2 col = vec2(0);
    for (int i = 0; i < LightInfo.AmbientColor.w; i++)
    {
        float a = GetLightAttenuation(i);
        vec4 lightPos = vec4(fsinLight_TanLightPos[i].xyz, LightInfo.LightPosition[i].w);
        vec3 lightDir = normalize(lightPos.xyz - $TAN_POSITION$.xyz);
        float diff = max(dot(norm, lightDir), 0);
        col.x += diff * a;
    }
    col.y *= $FwdAdd$;
    return col;
}

#endif