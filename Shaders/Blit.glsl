#pragma vertex main
#pragma fragment main
#pass main
#blend main true One Zero Add One Zero Add

#pragma in vertex vec3 Position
#pragma in vertex vec2 UV0
#pragma in vertex vec4 Color

#pragma in fragment vec2 fsin_UV0
#pragma in fragment vec4 fsin_Color

layout(set = 0, binding = 0) uniform texture2D DiffuseTexture;
layout(set = 0, binding = 1) uniform sampler DiffuseTextureSampler;

#if vertex
void main()
{
    gl_Position = vec4(Position, 1);
    fsin_UV0 = UV0;
    fsin_Color = Color;
}
#endif

#if fragment
void main()
{
    vec4 diffuseColor = texture(sampler2D(DiffuseTexture, DiffuseTextureSampler), fsin_UV0);
    FragColor = diffuseColor;
}
#endif
