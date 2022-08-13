#version 450

layout(location = 0) in vec2 fsin_UV0;
layout(location = 1) in vec4 fsin_Color;
layout(location = 2) in vec2 fsin_UV1;
layout(location = 3) in vec3 fsin_Pos;
layout(location = 4) in vec3 fsin_TanViewPos;
layout(location = 5) in vec3 fsin_TanFragPos;
layout(location = 0) out vec4 fsout_Color;

layout(set = 2, binding = 0) uniform texture2D DiffuseTexture;
layout(set = 2, binding = 1) uniform sampler DiffuseTextureSampler;
layout(set = 2, binding = 2) uniform texture2D LightmapTexture;
layout(set = 2, binding = 3) uniform sampler LightmapTextureSampler;
layout(set = 2, binding = 4) uniform texture2D BumpTexture;
layout(set = 2, binding = 5) uniform sampler BumpTextureSampler;

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
    vec3 viewDir = normalize(fsin_TanViewPos - fsin_TanFragPos);
    vec2 uvOffset0 = ParallaxMapping(fsin_UV0, viewDir, 0.05);
    vec4 diffuseColor = texture(sampler2D(DiffuseTexture, DiffuseTextureSampler), uvOffset0);
    vec4 lightmapColor = texture(sampler2D(LightmapTexture, LightmapTextureSampler), fsin_UV1);
    fsout_Color = diffuseColor * lightmapColor * fsin_Color;
    float height = GetHeightFromBump(fsin_UV0);
    // fsout_Color = vec4(height);
}