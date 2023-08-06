using System;
using System.Collections.Generic;
using System.Linq;
using Engine.Assets.Textures;
using Veldrid;

namespace Engine.Assets.Rendering
{
    public sealed class DrawCall : Resource
    {
        public override bool IsValid => _commandList != null && !_commandList.IsDisposed;
        public CommandList CommandList => _commandList;
        private CommandList _commandList;
        private RenderTexture2D _renderer;
        private Pipeline _pipeline;
        private Material _material;
        private Dictionary<uint, ResourceSet> _resSets = new Dictionary<uint, ResourceSet>();

        public DrawCall(RenderTexture2D renderer, string name) : base(name)
        {
            _renderer = renderer;
        }

        public void SetMaterial(Material material)
        {
            SetMaterialPass(material, "main");
        }

        public void SetMaterialPass(Material material, string pass)
        {
            _material = material;
            _pipeline = CreatePipeline((_renderer.InternalFramebuffer ?? _renderer.InternalSwapchain.Framebuffer).OutputDescription, material.Shader, material.Shader.Passes.FirstOrDefault(x => x.PassName == pass));
            _commandList.SetPipeline(_pipeline);
            foreach (var uniforms in _material._uniformLayouts)
            {
                SetResources(uniforms.Key, uniforms.Value.Select(x => x.resource as IMaterialBindable).ToArray());
            }
        }

        public void Begin()
        {
            _commandList.Begin();
            _commandList.SetFramebuffer(_renderer.InternalFramebuffer ?? _renderer.InternalSwapchain.Framebuffer);
        }

        public void End()
        {
            _commandList.End();
        }

        public void Submit()
        {
            RenderingGlobals.GameGraphics.SubmitCommands(_commandList);
        }

        private void SetResources(string name, IMaterialBindable[] bindable)
        {
            int id = _material.Shader.GetSetIndex(name);
            SetResources((uint)id, bindable);
        }

        private void SetResources(uint id, IMaterialBindable[] bindable)
        {
            // if (_resSets.TryGetValue(id, out ResourceSet val))
            {
                // _commandList.SetGraphicsResourceSet(id, val);
            }
            // else
            {
                ResourceSetDescription desc = new ResourceSetDescription(_material.Shader._reflResourceLayouts[(int)id]);
                desc.BoundResources = bindable.SelectMany(x => x.Bindables).ToArray();
                ResourceSet set = ResourceManager.GraphicsFactory.CreateResourceSet(desc);
                // _resSets.Add(id, set);
                _commandList.SetGraphicsResourceSet(id, set);
            }
        }

        public void DrawIndexedNow(uint indexCount, uint indexStart, int vertexOffset)
        {
            _commandList.DrawIndexed(indexCount, 1, indexStart, vertexOffset, 0);
        }

        private Pipeline CreatePipeline(OutputDescription outDesc, GraphicsShader shader, ShaderPass pass)
        {
            GraphicsPipelineDescription pipelineDescription = new GraphicsPipelineDescription();
            pipelineDescription.BlendState = pass.GetBlendStateDescription();
            pipelineDescription.DepthStencilState = pass.GetDepthStencilStateDescription();
            pipelineDescription.RasterizerState = new RasterizerStateDescription(
                cullMode: (FaceCullMode)pass.CullMode,
                fillMode: PolygonFillMode.Solid,
                frontFace: FrontFace.Clockwise,
                depthClipEnabled: true,
                scissorTestEnabled: true);
            pipelineDescription.ResourceLayouts = shader._reflResourceLayouts.ToArray();
            VertexLayoutDescription vertexLayout = new VertexLayoutDescription(shader._compileResult.VertexElements);
            pipelineDescription.ShaderSet = new ShaderSetDescription(
                vertexLayouts: new VertexLayoutDescription[] { vertexLayout },
                shaders: shader._shaders[pass.PassName]);
            // pipelineDescription.Outputs = renderer.InternalRenderTexture.InternalFramebuffer?.OutputDescription ?? renderer.InternalRenderTexture.InternalSwapchain.Framebuffer.OutputDescription;
            pipelineDescription.Outputs = outDesc;
            Pipeline newPipeline = ResourceManager.GraphicsFactory.CreateGraphicsPipeline(pipelineDescription);
            newPipeline.Name = $"{shader.Name}_{pass.PassName}";
            return newPipeline;
        }

        protected override Resource CloneInternal(string cloneName)
        {
            throw new NotImplementedException();
        }

        protected override void DisposeInternal()
        {
            throw new NotImplementedException();
        }

        protected override void ReCreateInternal()
        {
            throw new NotImplementedException();
        }
    }
}