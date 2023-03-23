using System.Drawing;
using System.Numerics;
using System.Runtime.InteropServices;
using Engine.Assets.Models;
using Engine.Assets.Rendering;
using Engine.Assets.Textures;
using FontStashSharp;
using FontStashSharp.Interfaces;
using Myra.Graphics2D;
using Myra.Platform;
using Veldrid;

public class VeldridDrawCtx : IMyraRenderer
{
    [StructLayout(LayoutKind.Sequential)]
    public struct Vertex : IVertex
    {
        public Vector3 Position;
        public Vector2 UV0;
        public Vector4 Color;

        public Vertex(VertexPositionColorTexture og)
        {
            Position = og.Position;
            Color = new Vector4(og.Color.R, og.Color.G, og.Color.B, og.Color.A) / 255f;
            UV0 = og.TextureCoordinate;
        }

        public Vertex(VertexPositionColorTexture og, Matrix4x4 mat)
        {
            Position = Vector3.Transform(og.Position, mat);
            // Position.Z = 1f;
            // Position = mat * og.Position;// / new Vector3(scalePos.X, scalePos.Y, 1f);
            // Position = (og.Position / new Vector3(scalePos.X, scalePos.Y * 0.5f, 1f) - new Vector3(1f, 0.5f, 0f)) * new Vector3(2f, -1f, 1f) + new Vector3(1f, 0.5f, 0f);
            Color = new Vector4(og.Color.R, og.Color.G, og.Color.B, og.Color.A) / 255f;
            UV0 = og.TextureCoordinate;
        }
    }

    public ITexture2DManager TextureManager { get; private set; } = new VeldridTexMan();
    public RendererType RendererType => RendererType.Quad;
    private System.Drawing.Rectangle rect;
    public System.Drawing.Rectangle Scissor
    {
        get => rect;
        set
        {
            rect = value;
            Flush();
            if (rend.IsDrawing)
                rend.CommandList.SetScissorRect(0, (uint)rect.X, (uint)rect.Y, (uint)rect.Width, (uint)rect.Height);
        }
    }
    public Mesh mesh;
    public Material mat;
    public Renderer rend;
    private Renderer temp;
    private RenderTexture2D rt;

    public VeldridDrawCtx()
    {
        mat = new Material("UI_Blit", new GraphicsShader("Shaders/Blit"));
        mesh = new Mesh("UI_Mesh", false, mat);
        /*
        mesh.Indices.AddRange(new uint[]
        {
            2, 1, 0,
            3, 1, 2,
        });
        */
        // /*
        mesh.Indices.AddRange(new uint[]
        {
            0, 1, 2,
            2, 1, 3,
        });
        // */
        rend = new Renderer("UI_Renderer");
        rt = new RenderTexture2D("UI_RenderTexture", (uint)RenderingGlobals.ViewSize.X, (uint)RenderingGlobals.ViewSize.Y);
        mat.ClearUniforms(0);
        mat.SetUniforms(0, new UniformLayout("Diffuse", rt, false, true));
    }

    public void Begin(TextureFiltering textureFiltering)
    {
        foreach (var tex in ((VeldridTexMan)TextureManager).textures)
        {
            tex.UpdateSamplerInfo(textureFiltering == TextureFiltering.Nearest ? Engine.Assets.Textures.SamplerInfo.Point : Engine.Assets.Textures.SamplerInfo.Linear);
        }
        Begin();
        rend.Clear();
    }

    public void Begin()
    {
        if (temp != null)
            return;
        temp = Renderer.Current;
        Renderer.Current = rend;
        rend.SetRenderTarget(rt);
        rend.Begin();
        rend.CommandList.SetViewport(0, new Viewport(0f, 0f, RenderingGlobals.ViewSize.X, RenderingGlobals.ViewSize.Y, 0f, 1f));
        // rend.Clear();
        var man = (VeldridTexMan)TextureManager;
        for (int i = 0; i < man.textures.Count; i++)
        {
            // man.textures[i].UpdateSamplerInfo(textureFiltering == TextureFiltering.Nearest ? Engine.Assets.Textures.SamplerInfo.Point : Engine.Assets.Textures.SamplerInfo.Linear);
            man.mats[i].PreDraw(Renderer.Current);
            man.mats[i].ClearUniforms(0);
            man.mats[i].SetUniforms(0, new UniformLayout("Diffuse", man.textures[i], false, true));
        }
        // foreach (var tex in ((VeldridTexMan)TextureManager).textures)
        // {
        //     tex.UpdateSamplerInfo(textureFiltering == TextureFiltering.Nearest ? Engine.Assets.Textures.SamplerInfo.Point : Engine.Assets.Textures.SamplerInfo.Linear);
        // }
        // mat.PreDraw(Renderer.Current);
        // mat.ClearUniforms(0);
        // mat.SetUniforms(0, new UniformLayout("Diffuse", null, false, true));
        // mat.Bind(Renderer.Current);
    }

    public void DrawQuad(object texture, ref VertexPositionColorTexture topLeft, ref VertexPositionColorTexture topRight, ref VertexPositionColorTexture bottomLeft, ref VertexPositionColorTexture bottomRight)
    {
        var man = (VeldridTexMan)TextureManager;
        var tex = man.textures[(int)texture];
        var mat = man.mats[(int)texture];

        mesh.ClearVertexList();
        var matrix = Matrix4x4.CreateOrthographicOffCenter(0, RenderingGlobals.ViewSize.X, RenderingGlobals.ViewSize.Y, 0f, 0f, 1f);
        mesh.AddVertex(new Vertex(topLeft, matrix));
        mesh.AddVertex(new Vertex(topRight, matrix));
        mesh.AddVertex(new Vertex(bottomLeft, matrix));
        mesh.AddVertex(new Vertex(bottomRight, matrix));
        mesh.UploadData<Vertex>(Renderer.Current);

        mat.Bind(Renderer.Current, "main");
        // rend.CommandList.SetScissorRect(0, (uint)rect.X, (uint)rect.Y, (uint)rect.Width, (uint)rect.Height);
        mesh.DrawNow(Renderer.Current);
    }

    public void DrawSprite(object texture, Vector2 pos, System.Drawing.Rectangle? src, FSColor color, float rotation, Vector2 scale, float depth)
    {
        throw new System.NotImplementedException();
    }

    public void End()
    {
        rend.End();
        rend.Submit();
        Renderer.Current = temp;
        temp = null;
        Renderer.Current.Blit(mat);
    }

    public void Flush()
    {
        // return;
        if (temp == null)
            return;
        if (rend.IsDrawing)
        {
            rend.End();
            rend.Submit();
        }
        Renderer.Current = temp;
        temp = null;
        // Renderer.Current.Blit(mat);
        // Renderer.Current.NewPass();
        Begin();
    }
}