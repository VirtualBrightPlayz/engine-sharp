using System;
using System.Threading.Tasks;

namespace Engine.Assets
{
    public abstract class Resource //: IDisposable
    {
        public abstract bool IsValid { get; }
        public string Name { get; set; }
        public bool HasBeenInitialized { get; set; } = false;
        public Resource(string name)
        {
            Name = name;
        }
        protected abstract Resource CloneInternal(string cloneName);
        protected abstract void ReCreateInternal();
        protected abstract void DisposeInternal();
        public void Dispose()
        {
            if (!HasBeenInitialized)
                return;
            Console.WriteLine($"Dispose {Name} ({GetType().Name})");
            HasBeenInitialized = false;
            DisposeInternal();
            ResourceManager.UnloadInternal(this);
        }
        public Resource Clone(string cloneName)
        {
            Console.WriteLine($"Clone {Name} ({GetType().Name})");
            return CloneInternal(cloneName);
        }
        public T Clone<T>(string cloneName) where T : Resource
        {
            Console.WriteLine($"Clone {Name} ({GetType().Name})");
            return (T)CloneInternal(cloneName);
        }
        public void ReCreate()
        {
            if (HasBeenInitialized)
                return;
            Console.WriteLine($"ReCreate {Name} ({GetType().Name})");
            HasBeenInitialized = true;
            ReCreateInternal();
            ResourceManager.AddInternal(this);
        }
        ~Resource()
        {
            Console.WriteLine($"~ctor {Name} ({GetType().Name})");
            Dispose();
        }
    }
}