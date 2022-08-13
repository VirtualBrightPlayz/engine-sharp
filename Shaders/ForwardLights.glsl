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

#endif