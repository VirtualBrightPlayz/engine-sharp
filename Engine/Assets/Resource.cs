using System;
using System.Threading.Tasks;

namespace Engine.Assets
{
    public abstract class Resource : IDisposable
    {
        public abstract bool IsValid { get; }
        public string Name { get; set; }
        public bool HasBeenInitialized { get; set; } = false;
        public abstract void Dispose();
        public abstract Task<Resource> Clone(string cloneName);
        public virtual void ReCreate()
        {
            Console.WriteLine($"ReCreate {Name} ({GetType().Name})");
            HasBeenInitialized = true;
        }
    }
}