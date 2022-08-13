#version 450

layout(location = 0) in vec3 Position;
layout(location = 1) in vec3 Normal;
layout(location = 2) in vec2 UV0;
layout(location = 3) in vec2 UV1;
layout(location = 4) in vec4 Color;
layout(location = 5) in vec3 Tangent;
layout(location = 6) in vec3 BiTangent;

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

layout(set = 3, binding = 0) uniform WorldInfo0
{
    vec4 ViewPosition;
};

layout(location = 0) out vec2 fsin_UV0;
layout(location = 1) out vec4 fsin_Color;
layout(location = 2) out vec2 fsin_UV1;
layout(location = 3) out vec3 fsin_Pos;
layout(location = 4) out vec3 fsin_TanViewPos;
layout(location = 5) out vec3 fsin_TanFragPos;

void main()
{

    gl_Position = Projection * View * World * vec4(Position, 1);
    fsin_UV0 = UV0;
    fsin_Color = Color;
    fsin_UV1 = UV1;
    fsin_Pos = Position;

    vec3 T = normalize(mat3(World) * Tangent);
    vec3 B = normalize(mat3(World) * BiTangent);
    vec3 N = normalize(mat3(World) * Normal);
    mat3 TBN = transpose(mat3(T, B, N));
    fsin_TanViewPos = TBN * ViewPosition.xyz;
    fsin_TanFragPos = TBN * vec3(World * vec4(Position, 1));
}