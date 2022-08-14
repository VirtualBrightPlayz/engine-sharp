#pass FwdBase
#pass FwdAdd
#blend FwdBase true One Zero Add One Zero Add
#blend FwdAdd true One One Add One One Add
#depth FwdBase true true LessEqual
#depth FwdAdd true false LessEqual

#define LIGHTS_MAX 4

struct LightInfoStruct
{
    vec4 AmbientColor;
    vec4 LightPosition[$LIGHTS_MAX$];
    vec4 LightColor[$LIGHTS_MAX$];
};

layout(set = $LIGHT_SET$, binding = 0) uniform LightInfo0
{
    LightInfoStruct LightInfo;
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
        float a = LightInfo.LightPosition[i].w / (d * d);
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
        vec4 fLight = vec4(lighting, ApplyAlphaLighting(i, lighting, a));
        col.rgb += fLight.rgb;
        col.a += fLight.a;
    }
    col.a *= $FwdAdd$;
    return col;
}

#endif