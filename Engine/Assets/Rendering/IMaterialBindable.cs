using System.Threading.Tasks;
using Veldrid;

namespace Engine.Assets.Rendering
{
    public interface IMaterialBindable
    {
        BindableResource[] Bindables { get; }
        Task ReCreate();
    }
}