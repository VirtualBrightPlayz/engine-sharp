using System;
using System.Threading.Tasks;

namespace Engine.Assets
{
    public abstract class Resource
    {
        public abstract bool IsValid { get; }
        public string Name { get; set; }
        public bool HasBeenInitialized { get; set; } = false;
        public Resource(string name)
        {
            Name = name;
            Log.Debug(nameof(Resource), $"ctor {Name} ({GetType().Name})");
        }
        protected abstract Resource CloneInternal(string cloneName);
        protected abstract void ReCreateInternal();
        protected abstract void DisposeInternal();
        public void Dispose()
        {
            if (!HasBeenInitialized)
                return;
            Log.Debug(nameof(Resource), $"Dispose {Name} ({GetType().Name})");
            HasBeenInitialized = false;
            DisposeInternal();
            ResourceManager.UnloadInternal(this);
        }
        public Resource Clone(string cloneName)
        {
            Log.Debug(nameof(Resource), $"Clone {Name} ({GetType().Name})");
            return CloneInternal(cloneName);
        }
        public T Clone<T>(string cloneName) where T : Resource
        {
            Log.Debug(nameof(Resource), $"Clone {Name} ({GetType().Name})");
            return (T)CloneInternal(cloneName);
        }
        public void ReCreate()
        {
            if (HasBeenInitialized)
                return;
            Log.Debug(nameof(Resource), $"ReCreate {Name} ({GetType().Name})");
            HasBeenInitialized = true;
            ReCreateInternal();
            ResourceManager.AddInternal(this);
        }
        ~Resource()
        {
            Log.Debug(nameof(Resource), $"~ctor {Name} ({GetType().Name})");
            Dispose();
        }
    }
}