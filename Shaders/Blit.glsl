#pragma vertex main
#pragma fragment main
#pass main
#blend main true One Zero Add One Zero Add
#pass alpha
#blend alpha true One InverseSourceAlpha Add One InverseSourceAlpha Add

#pragma in vertex vec3 Position
#pragma in vertex vec2 UV0
#pragma in vertex vec4 Color

#pragma in fragment vec2 fsin_UV0
#pragma in fragment vec4 fsin_Color

#uniform texture2D DiffuseTexture; 0 0
#uniform sampler DiffuseTextureSampler; 0 1

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
    vec4 diffuseColor = texture(sampler2D(DiffuseTexture, DiffuseTextureSampler), fsin_UV0) * fsin_Color;
    FragColor = diffuseColor;
}
#endif
