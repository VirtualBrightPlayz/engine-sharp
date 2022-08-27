using System;
using System.Threading.Tasks;

namespace Engine.Assets
{
    public abstract class Resource : IDisposable
    {
        public abstract bool IsValid { get; }
        public string Name { get; set; }
        public bool HasBeenInitialized { get; set; } = false;
        public virtual void Dispose()
        {
            Console.WriteLine($"Dispose {Name} ({GetType().Name})");
        }
        public abstract Task<Resource> Clone(string cloneName);
        public virtual Task ReCreate()
        {
            Console.WriteLine($"ReCreate {Name} ({GetType().Name})");
            HasBeenInitialized = true;
            return Task.CompletedTask;
        }
        ~Resource()
        {
            Console.WriteLine($"~ctor {Name} ({GetType().Name})");
            Dispose();
        }
    }
}