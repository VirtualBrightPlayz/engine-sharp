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

// global vars

#if vertex
mat3 TBN;
#endif

#if fragment
vec3 bumpNormal;
#endif

// includes

layout(set = 0, binding = 0) uniform ViewMatrix
{
    mat4 View;
};
layout(set = 0, binding = 1) uniform ProjectionMatrix
{
    mat4 Projection;
};
layout(set = 0, binding = 2) uniform WorldMatrix
{
    mat4 World;
};

layout(set = 1, binding = 0) uniform texture2D DiffuseTexture;
layout(set = 1, binding = 1) uniform sampler DiffuseTextureSampler;

#if vertex
void main()
{
    gl_Position = Projection * View * World * vec4(Position, 1);
    fsin_UV0 = UV0;
    fsin_Color = Color;
}
#endif

#if fragment
void main()
{
    vec4 diffuseColor = texture(sampler2D(DiffuseTexture, DiffuseTextureSampler), fsin_UV0);
    FragColor = diffuseColor * fsin_Color;
}
#endif
