#version 450

layout(location = 0) in vec3 Position;
layout(location = 1) in vec3 Normal;
layout(location = 2) in vec2 UV0;
layout(location = 3) in vec2 UV1;
layout(location = 4) in vec4 Color;
layout(location = 5) in vec4 BoneWeights;
layout(location = 6) in uvec4 BoneIndices;

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
layout(set = 1, binding = 1) uniform BonesMatrix
{
    mat4 Bones[64];
};

layout(location = 0) out vec2 fsin_UV0;
layout(location = 1) out vec4 fsin_Color;

void main()
{
    mat4 boneTransformation = Bones[BoneIndices.x] * BoneWeights.x;
    boneTransformation += Bones[BoneIndices.y] * BoneWeights.y;
    boneTransformation += Bones[BoneIndices.z] * BoneWeights.z;
    boneTransformation += Bones[BoneIndices.w] * BoneWeights.w;
    gl_Position = Projection * View * World * boneTransformation * vec4(Position, 1);
    fsin_UV0 = UV0;
    fsin_Color = Color;
}