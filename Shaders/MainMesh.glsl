#define LIGHT_SET 4
#define NORMAL fsin_Normal
#define TAN_NORMAL bumpNormal
#define POSITION fsin_Pos
#define TAN_POSITION fsin_TanFragPos
#define SPECULAR_EXP 32
#define VIEW_POSITION fsin_ViewPos
#define OBJ_TO_TAN TBN
#define TAN_VIEW_POSITION fsin_TanViewPos

#pragma vertex main
#pragma fragment main
#pass main
#blend main true One Zero Add One Zero Add

#pragma in vertex vec3 Position
#pragma in vertex vec3 Normal
#pragma in vertex vec2 UV0
#pragma in vertex vec2 UV1
#pragma in vertex vec4 Color
#pragma in vertex vec3 Tangent
#pragma in vertex vec3 BiTangent

#pragma in fragment vec2 fsin_UV0
#pragma in fragment vec4 fsin_Color
#pragma in fragment vec2 fsin_UV1
#pragma in fragment vec3 fsin_Pos
#pragma in fragment vec3 fsin_TanViewPos
#pragma in fragment vec3 fsin_TanFragPos
#pragma in fragment vec3 fsin_Normal
#pragma in fragment vec3 fsin_ViewPos

// global vars

#if vertex
mat3 TBN;
#endif

#if fragment
vec3 bumpNormal;
#endif

// includes

#include "ForwardLights.glsl"
#draw FwdBase None
#draw FwdAdd None
#draw FwdDepth None

layout(set = 0, binding = 0) uniform ViewMatrix
{
    mat4 View;
};
layout(set = 0, binding = 1) uniform ProjectionMatrix
{
    mat4 Projection;
};
layout(set = 1, binding = 0) uniform WorldMatrix
{
    mat4 World;
};

layout(set = 2, binding = 0) uniform texture2D DiffuseTexture;
layout(set = 2, binding = 1) uniform sampler DiffuseTextureSampler;
layout(set = 2, binding = 2) uniform texture2D LightmapTexture;
layout(set = 2, binding = 3) uniform sampler LightmapTextureSampler;
layout(set = 2, binding = 4) uniform texture2D BumpTexture;
layout(set = 2, binding = 5) uniform sampler BumpTextureSampler;

layout(set = 3, binding = 0) uniform WorldInfo0
{
    vec4 ViewPosition;
};

#if vertex
void main()
{
    gl_Position = Projection * View * World * vec4(Position, 1);
    fsin_UV0 = UV0;
    fsin_Color = Color;
    fsin_UV1 = UV1;
    fsin_Pos = vec3(World * vec4(Position, 1));
    fsin_Normal = mat3(transpose(inverse(World))) * Normal;
    fsin_ViewPos = ViewPosition.xyz;

    vec3 T = normalize(mat3(World) * Tangent);
    vec3 B = normalize(mat3(World) * BiTangent);
    vec3 N = normalize(mat3(World) * Normal);
    TBN = transpose(mat3(T, B, N));
    TransferLightInfo();
    fsin_TanViewPos = TBN * ViewPosition.xyz;
    fsin_TanFragPos = TBN * vec3(World * vec4(Position, 1));
}
#endif

#if fragment
float GetHeightFromBump(vec2 texCoords)
{
    vec3 bump = texture(sampler2D(BumpTexture, BumpTextureSampler), texCoords).xyz;
    bump = normalize(bump * 2.0 - 1.0);
    float height = dot(bump, vec3(0, 0, 1));
    return height;
}

vec2 ParallaxMapping(vec2 texCoords, vec3 viewDir, float multi)
{
    const float numLayers = 10;
    float layerDepth = 1.0 / numLayers;
    float currentLayerDepth = 0.0;
    vec2 p = viewDir.xy * multi;
    vec2 deltaCoords = p / numLayers;

    vec2 currentCoords = texCoords;
    float currentDepthValue = GetHeightFromBump(currentCoords);

    while (currentLayerDepth < currentDepthValue)
    {
        currentCoords -= deltaCoords;
        currentDepthValue = GetHeightFromBump(currentCoords);
        currentLayerDepth += layerDepth;
    }

    vec2 prevCoords = currentCoords + deltaCoords;

    float afterDepth = currentDepthValue - currentLayerDepth;
    float beforeDepth = GetHeightFromBump(prevCoords) - currentLayerDepth + layerDepth;

    float weight = afterDepth / (afterDepth - beforeDepth);
    vec2 finalCoords = prevCoords * weight + currentCoords * (1.0 - weight);

    return finalCoords;
}

void main()
{
    bumpNormal = texture(sampler2D(BumpTexture, BumpTextureSampler), fsin_UV0).xyz;
    bumpNormal = normalize(bumpNormal * 2.0 - 1.0);

    vec3 viewDir = normalize(fsin_TanViewPos - fsin_TanFragPos);
    vec2 uvOffset0 = ParallaxMapping(fsin_UV0, viewDir, 0.05);
    // const float gamma = 2.2;
    const float gamma = 1.0;
    vec4 diffuseColor = pow(texture(sampler2D(DiffuseTexture, DiffuseTextureSampler), fsin_UV0), vec4(gamma));
    vec4 lightmapColor = texture(sampler2D(LightmapTexture, LightmapTextureSampler), fsin_UV1);
    vec4 lighting = ApplyLighting();
    vec4 finalLighting = lighting;
    FragColor = diffuseColor * vec4(finalLighting.rgb, 1) * fsin_Color;// * lightmapColor * fsin_Color;
    FragColor.a = lighting.a;
}
#endif
