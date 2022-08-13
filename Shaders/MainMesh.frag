#version 450

layout(location = 0) in vec2 fsin_UV0;
layout(location = 1) in vec4 fsin_Color;
layout(location = 0) out vec4 fsout_Color;

layout(set = 2, binding = 0) uniform texture2D DiffuseTexture;
layout(set = 2, binding = 1) uniform sampler DiffuseTextureSampler;

void main()
{
    vec4 diffuseColor = texture(sampler2D(DiffuseTexture, DiffuseTextureSampler), fsin_UV0);
    fsout_Color = diffuseColor * fsin_Color;
}