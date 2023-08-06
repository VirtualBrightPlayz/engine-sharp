using System;
using System.Threading.Tasks;
using Engine.Assets.Rendering;

namespace Engine.Assets
{
    public abstract class GraphicsResource : Resource
    {
        public GraphicsResource(string name) : base(name)
        {
        }

        protected abstract void BindInternal(Renderer renderer);
        protected abstract void UploadInternal(Renderer renderer);
        protected abstract bool IsValidForInternal(Renderer renderer);

        public void Bind(Renderer renderer)
        {
            BindInternal(renderer);
        }
        public void UploadToRenderer(Renderer renderer)
        {
            UploadInternal(renderer);
        }
        public bool IsValidForRenderer(Renderer renderer)
        {
            return IsValidForInternal(renderer);
        }
    }
}