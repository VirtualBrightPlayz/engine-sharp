using System;
using System.Threading.Tasks;
using Veldrid;

namespace Engine.Assets.Rendering
{
    public interface IMaterialBindable : IDisposable
    {
        BindableResource[] Bindables { get; }
        Task ReCreate();
    }
}