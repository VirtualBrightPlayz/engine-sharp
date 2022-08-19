using Veldrid;

namespace Engine.Assets.Rendering
{
    public interface IMaterialBindable
    {
        BindableResource[] Bindables { get; }
        void ReCreate();
    }
}