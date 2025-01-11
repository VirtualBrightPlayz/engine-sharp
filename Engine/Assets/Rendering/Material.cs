using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Engine.Assets.Models;
using Engine.Assets.Textures;
using Veldrid;

namespace Engine.Assets.Rendering
{
    public struct UniformLayout
    {
        public string name;
        public IMaterialBindable resource;
        public bool vertexUse;
        public bool fragmentUse;

        public UniformLayout(string name, IMaterialBindable resource, bool vertexUse, bool fragmentUse)
        {
            this.name = name;
            this.resource = resource;
            this.vertexUse = vertexUse;
            this.fragmentUse = fragmentUse;
        }
    }

    public class Material : Resource
    {
        public override bool IsValid => Shader != null && Shader.IsValid;
        public GraphicsShader Shader { get; private set; }
        private Dictionary<string, Dictionary<Renderer, Pipeline>> _pipelines = new Dictionary<string, Dictionary<Renderer, Pipeline>>();
        private Dictionary<uint, List<Resource>> _uniformResources = new Dictionary<uint, List<Resource>>();
        internal Dictionary<uint, UniformLayout[]> _uniformLayouts = new Dictionary<uint, UniformLayout[]>();
        private Dictionary<uint, ResourceSet> _resourceSets = new Dictionary<uint, ResourceSet>();
        private Dictionary<uint, IMaterialBindable> _bindables = new Dictionary<uint, IMaterialBindable>();
        private Dictionary<uint, CompoundBuffer> _compoundBuffers = new Dictionary<uint, CompoundBuffer>();
        private Dictionary<uint, ResourceLayout> layouts = new Dictionary<uint, ResourceLayout>();

        public Material(string name, GraphicsShader shader) : base(name)
        {
            Shader = shader;
            ReCreate();
        }

        protected override void ReCreateInternal()
        {
            foreach (var item in _pipelines)
            {
                foreach (var pipeline in item.Value)
                {
                    if (pipeline.Value != null && !pipeline.Value.IsDisposed)
                        pipeline.Value.Dispose();
                }
            }
            _pipelines.Clear();
            foreach (var res in _uniformResources)
            {
                foreach (var item in res.Value)
                {
                    item.ReCreate();
                }
            }
            foreach (var uniform in _uniformLayouts)
            {
                foreach (var item in uniform.Value)
                {
                    if (item.resource != null)
                        item.resource.ReCreate();
                }
            }
            foreach (var cbuffer in _compoundBuffers)
            {
                cbuffer.Value.ReCreate();
                SetUniforms(cbuffer.Key, cbuffer.Value);
            }
            foreach (var res in _resourceSets)
            {
                if (res.Value != null && !res.Value.IsDisposed)
                    res.Value.Dispose();
            }
            _resourceSets.Clear();
            // Shader.ReCreate();
            foreach (var uniform in _uniformLayouts)
            {
                SetUniforms(uniform.Key, true, uniform.Value);
            }
        }

        protected override Resource CloneInternal(string cloneName)
        {
            Material res = new Material(cloneName, Shader);
            return res;
        }

        public void ClearUniforms(uint setId)
        {
            if (_uniformResources.ContainsKey(setId))
                _uniformResources[setId].Clear();
            if (_uniformLayouts.ContainsKey(setId))
                _uniformLayouts[setId] = new UniformLayout[0];
            if (_resourceSets.ContainsKey(setId))
            {
                _resourceSets[setId].Dispose();
                _resourceSets.Remove(setId);
            }
        }

        public bool HasSetUniform(uint setId)
        {
            return _resourceSets.ContainsKey(setId);
        }

        public void SetUniforms(uint setId, params UniformLayout[] uniforms)
        {
            SetUniforms(setId, false, uniforms);
        }

        public void SetUniforms(uint setId, CompoundBuffer buffer)
        {
            if (buffer.InternalResourceSet == null || buffer.InternalResourceSet.IsDisposed)
                throw new Exception($"Material.SetUniforms: {buffer.Name}'s InternalResourceSet is null/disposed!");
            if (_compoundBuffers.ContainsKey(setId))
                _compoundBuffers[setId] = buffer;
            else
                _compoundBuffers.Add(setId, buffer);
            if (setId >= Shader._reflResourceLayouts.Count)
            {
                throw new Exception($"Material.SetUniforms: {setId} was not found in the shader's resourceLayouts");
            }
        }

        public void SetUniforms(uint setId, bool force, params UniformLayout[] uniforms)
        {
            if (!force && HasSetUniform(setId))
            {
                return;
            }
            if (uniforms.Length == 1 && uniforms[0].resource is CompoundBuffer buffer)
            {
                if (_compoundBuffers.ContainsKey(setId))
                    _compoundBuffers[setId] = buffer;
                else
                    _compoundBuffers.Add(setId, buffer);
                return;
            }
            if (uniforms.Length == 1)
            {
                _bindables[setId] = uniforms[0].resource;
                return;
            }
            if (!force && _uniformResources.ContainsKey(setId) && _uniformResources[setId].Any(x => uniforms.Any(y => y.resource == x)))
            {
                return;
            }
            if (_uniformLayouts.ContainsKey(setId))
                _uniformLayouts[setId] = uniforms.ToArray();
            else
                _uniformLayouts.Add(setId, uniforms.ToArray());
            if (_uniformResources.ContainsKey(setId))
                _uniformResources[setId].Clear();
            else
                _uniformResources.Add(setId, new List<Resource>());
            List<ResourceLayoutElementDescription> elements = new List<ResourceLayoutElementDescription>();
            for (int i = 0; i < uniforms.Length; i++)
            {
                // TODO: IMaterialBindable
                ShaderStages stages = ShaderStages.None;
                if (uniforms[i].resource is Texture2D || uniforms[i].resource is RenderTexture2D || uniforms[i].fragmentUse)
                    stages |= ShaderStages.Fragment;
                if (uniforms[i].vertexUse)
                    stages |= ShaderStages.Vertex;
                ResourceKind kind = ResourceKind.UniformBuffer;
                if (uniforms[i].resource is Texture2D tex)
                {
                    elements.Add(new ResourceLayoutElementDescription(uniforms[i].name, ResourceKind.TextureReadOnly, stages));
                    elements.Add(new ResourceLayoutElementDescription(uniforms[i].name + "Sampler", ResourceKind.Sampler, stages));
                    _uniformResources[setId].Add(tex);
                }
                else if (uniforms[i].resource is RenderTexture2D renderTex)
                {
                    elements.Add(new ResourceLayoutElementDescription(uniforms[i].name, ResourceKind.TextureReadOnly, stages));
                    elements.Add(new ResourceLayoutElementDescription(uniforms[i].name + "Sampler", ResourceKind.Sampler, stages));
                    _uniformResources[setId].Add(renderTex);
                }
                else if (uniforms[i].resource is UniformBuffer buf)
                {
                    elements.Add(new ResourceLayoutElementDescription(uniforms[i].name, kind, stages));
                    _uniformResources[setId].Add(buf);
                }
            }

            if (setId < Shader._reflResourceLayouts.Count)
            {
                if (!_resourceSets.ContainsKey(setId))
                    _resourceSets.Add(setId, null);
                else if (_resourceSets[setId] != null && !_resourceSets[setId].IsDisposed)
                    _resourceSets[setId].Dispose();
                ResourceSetDescription desc = new ResourceSetDescription(Shader._reflResourceLayouts[(int)setId]);
                if (_uniformResources.ContainsKey(setId))
                {
                    List<BindableResource> boundResources = new List<BindableResource>();
                    int i = 0;
                    foreach (var res in _uniformResources[setId])
                    {
                        if (res is Texture2D texture)
                        {
                            boundResources.Add(texture.Tex);
                            boundResources.Add(texture.InternalSampler);
                            i += 2;
                        }
                        else if (res is RenderTexture2D renderTexture)
                        {
                            boundResources.Add(renderTexture.ColorTex);
                            boundResources.Add(renderTexture.InternalSampler);
                            i += 2;
                        }
                        else if (res is UniformBuffer buf)
                        {
                            boundResources.Add(buf.InternalBuffer);
                            i++;
                        }
                        else
                        {
                            throw new Exception();
                        }
                    }
                    desc.BoundResources = boundResources.ToArray();
                }
                _resourceSets[setId] = ResourceManager.GraphicsFactory.CreateResourceSet(desc);
                _resourceSets[setId].Name = $"{Name}_{setId}";
            }
            else
                throw new Exception($"Material.SetUniforms: {setId} was not found in the shader's resourceLayouts");
        }

        internal void CreatePipeline(Renderer renderer, ShaderPass pass, bool dispose = true)
        {
            /*if (_pipeline != null && !_pipeline.IsDisposed && dispose)
                _pipeline.Dispose();*/
            if (_pipelines.TryGetValue(pass.PassName, out var pipelines))
            {
                if (!pipelines.TryGetValue(renderer, out var pipeline) && pipeline != null && pipeline.IsDisposed)
                {
                    pipeline.Dispose();
                }
                pipelines.Remove(renderer);
            }
            GraphicsPipelineDescription pipelineDescription = new GraphicsPipelineDescription();
            pipelineDescription.BlendState = pass.GetBlendStateDescription();
            // BlendStateDescription.SingleOverrideBlend;
            pipelineDescription.DepthStencilState = pass.GetDepthStencilStateDescription();
            /*
            new DepthStencilStateDescription(
                depthTestEnabled: true,
                depthWriteEnabled: true,
                comparisonKind: ComparisonKind.LessEqual);
            */
            pipelineDescription.RasterizerState = new RasterizerStateDescription(
                cullMode: (FaceCullMode)pass.CullMode,
                fillMode: PolygonFillMode.Solid,
                frontFace: FrontFace.Clockwise,
                depthClipEnabled: true,
                scissorTestEnabled: true);
            pipelineDescription.PrimitiveTopology = PrimitiveTopology.TriangleList;

        #if WEBGL && false
            pipelineDescription.ResourceLayouts = layouts.OrderBy(x => x.Key).Select(x => x.Value).ToArray();
        #else
            pipelineDescription.ResourceLayouts = Shader._reflResourceLayouts.ToArray();
        #endif

            VertexLayoutDescription vertexLayout = new VertexLayoutDescription(Shader._compileResult.VertexElements);
            /*VertexLayoutDescription vertexLayout = new VertexLayoutDescription(VertexPositionColorUV.SizeInBytes,
                new VertexElementDescription("Position", VertexElementSemantic.Position, VertexElementFormat.Float3),
                new VertexElementDescription("Normal", VertexElementSemantic.Normal, VertexElementFormat.Float3),
                new VertexElementDescription("UV0", VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float2),
                new VertexElementDescription("UV1", VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float2),
                new VertexElementDescription("Color", VertexElementSemantic.Color, VertexElementFormat.Float4),
                new VertexElementDescription("BoneWeights", VertexElementSemantic.TextureCoordinate, VertexElementFormat.Float4),
                new VertexElementDescription("BoneIndices", VertexElementSemantic.TextureCoordinate, VertexElementFormat.UInt4));*/
            pipelineDescription.ShaderSet = new ShaderSetDescription(
                vertexLayouts: new VertexLayoutDescription[] { vertexLayout },
                shaders: Shader._shaders[pass.PassName]);
            pipelineDescription.Outputs = renderer.InternalRenderTexture.InternalFramebuffer?.OutputDescription ?? renderer.InternalRenderTexture.InternalSwapchain.Framebuffer.OutputDescription;
            Pipeline newPipeline = ResourceManager.GraphicsFactory.CreateGraphicsPipeline(pipelineDescription);
            newPipeline.Name = $"{Name}_{pass.PassName}";
            if (!_pipelines.ContainsKey(pass.PassName))
            {
                _pipelines.Add(pass.PassName, new Dictionary<Renderer, Pipeline>());
            }
            _pipelines[pass.PassName].Add(renderer, newPipeline);
        }

        internal void PreDraw(Renderer renderer)
        {
            Pipeline pipeline = _pipelines.FirstOrDefault().Value?.FirstOrDefault(x => x.Key == renderer).Value;
            if (_pipelines.Count == 0 || pipeline == null)
            {
                foreach (var pass in Shader.Passes)
                    CreatePipeline(renderer, pass);
            }
        }

        internal void Bind(Renderer renderer, string passName = "", bool bindPipeline = true)
        {
            Pipeline pipeline = null;//_pipelines.FirstOrDefault().Value?.FirstOrDefault(x => x.Key == renderer).Value;
            if (!string.IsNullOrEmpty(passName))
            {
                if (!_pipelines.ContainsKey(passName))
                    CreatePipeline(renderer, Shader.Passes.First(x => x.PassName == passName));
                pipeline = _pipelines[passName][renderer];
            }
            if (pipeline == null)
            {
                Log.Error(nameof(Material), $"{Name} Missing a pipeline");
            }
            if (renderer == null || renderer.CommandList == null)
            {
                Log.Error(nameof(Material), $"{Name} Missing renderer.CommandList");
            }
            if (bindPipeline)
                renderer.CommandList.SetPipeline(pipeline);
            uint maxId = 0;
            foreach (var resSet in _resourceSets.OrderBy(x => x.Key))
            {
                if (resSet.Value == null || resSet.Value.IsDisposed)
                {
                    Log.Warn(nameof(Material), $"{Name} {resSet.Key} is null/disposed!");
                    continue;
                }
                maxId++;
                renderer.CommandList.SetGraphicsResourceSet(resSet.Key, resSet.Value);
            }
            foreach (var resSet in _compoundBuffers.OrderBy(x => x.Key))
            {
                if (resSet.Value.InternalResourceSet == null || resSet.Value.InternalResourceSet.IsDisposed)
                {
                    Log.Warn(nameof(Material), $"{Name} {resSet.Key} is null/disposed!");
                    continue;
                }
                maxId++;
                renderer.CommandList.SetGraphicsResourceSet(resSet.Key, resSet.Value.InternalResourceSet);
            }
            foreach (var resSet in _bindables.OrderBy(x => x.Key))
            {
                if (resSet.Value == null)
                {
                    Log.Warn(nameof(Material), $"{Name} {resSet.Key} is null/disposed!");
                    continue;
                }
                maxId++;
                resSet.Value.Bind(renderer, this, resSet.Key);
            }
            if (maxId != Shader._reflResourceLayouts.Count)
            {
                // Log.Error(nameof(Material), $"{Name} Missing a resource set, found {maxId}, {Shader._reflResourceLayouts.Count} required");
            }
        }

        protected override void DisposeInternal()
        {
            foreach (var resSet in _resourceSets)
            {
                resSet.Value.Dispose();
            }
            _resourceSets.Clear();
            foreach (var cmp in _compoundBuffers)
            {
                cmp.Value.Dispose();
            }
            _compoundBuffers.Clear();
            foreach (var cmp in _bindables)
            {
                cmp.Value.Dispose();
            }
            _bindables.Clear();
            foreach (var item in _pipelines)
            {
                foreach (var pipeline in item.Value)
                {
                    if (pipeline.Value != null && !pipeline.Value.IsDisposed)
                        pipeline.Value.Dispose();
                }
            }
            _pipelines.Clear();
        }
    }
}