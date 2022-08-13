#pass FwdBase
#pass FwdAdd
#blend FwdBase true One Zero Add One Zero Add
#blend FwdAdd true One One Add One One Add
#depth FwdBase true true LessEqual
#depth FwdAdd true false Equal

struct LightInfoStruct
{
    vec4 AmbientColor;
    vec4 LightPosition;
    vec4 LightColor;
};

layout(set = $LIGHT_SET$, binding = 0) uniform LightInfo0
{
    LightInfoStruct LightInfo;
};

#pragma in fragment vec3 fsinLight_TanLightPos

#if vertex

void TransferLightInfo()
{
    fsinLight_TanLightPos = $OBJ_TO_TAN$ * LightInfo.LightPosition.xyz;
}

#endif

#if fragment

float GetLightAttenuation()
{
    float d = length(LightInfo.LightPosition.xyz - $POSITION$.xyz);
    // float d = length(fsinLight_TanLightPos.xyz - $TAN_POSITION$.xyz);
    float attenuation = LightInfo.LightPosition.w / d;
    return clamp(attenuation, 0, 1);
}

vec3 ApplyAmbientLighting()
{
    return LightInfo.AmbientColor.rgb * LightInfo.AmbientColor.a;
}

vec3 ApplyDiffuseLighting()
{
    vec3 norm = -normalize($TAN_NORMAL$.xyz);
    vec3 outDiff = vec3(0);
    {
        vec3 lightDir = normalize(fsinLight_TanLightPos.xyz - $TAN_POSITION$.xyz);
        float diff = max(dot(norm, lightDir), 0);
        outDiff += (LightInfo.LightColor.rgb * LightInfo.LightColor.a * diff);
    }
    return outDiff;
}

vec3 ApplySpecularLighting()
{
    vec3 norm = -normalize($TAN_NORMAL$.xyz);
    vec3 outDiff = vec3(0);
    {
        vec3 lightDir = normalize(fsinLight_TanLightPos.xyz - $TAN_POSITION$.xyz);
        vec3 viewDir = normalize($TAN_VIEW_POSITION$.xyz - $TAN_POSITION$.xyz);
        vec3 halfwayDir = normalize(lightDir + viewDir);
        float spec = pow(max(dot(norm, halfwayDir), 0), $SPECULAR_EXP$);
        vec3 specular = LightInfo.LightColor.rgb * LightInfo.LightColor.a * spec;
        outDiff += (specular);
    }
    return outDiff;
}

float ApplyAlphaLighting()
{
    vec3 norm = -normalize($TAN_NORMAL$.xyz);
    vec3 lightDir = normalize(fsinLight_TanLightPos.xyz - $TAN_POSITION$.xyz);
    vec3 viewDir = normalize($TAN_VIEW_POSITION$.xyz - $TAN_POSITION$.xyz);
    vec3 halfwayDir = normalize(lightDir + viewDir);
    float spec = pow(max(dot(norm, halfwayDir), 0), $SPECULAR_EXP$);
    float diff = max(dot(norm, lightDir), 0);
    return (1 - $FwdAdd$) * (diff + spec);
}

vec4 ApplyLighting()
{
    float a = GetLightAttenuation();
    vec3 lighting = ApplyAmbientLighting() + ApplyDiffuseLighting() * a + ApplySpecularLighting() * a;
    // lighting /= 3;
    return vec4(lighting, ApplyAlphaLighting() * a);
}

#endif