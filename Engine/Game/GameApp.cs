using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using BepuPhysics;
using BepuUtilities;
using BepuUtilities.Memory;
using DefaultEcs;
using DefaultEcs.System;
using Engine.Assets.Rendering;
using Engine.Game.Systems;

namespace Engine.Game
{
    public abstract class GameApp : IDisposable
    {
        public static GameApp Current { get; private set; }
        public Simulation Simulation { get; protected set; }
        public BufferPool BufferPool { get; protected set; }
        public ThreadDispatcher dispatcher { get; protected set; }
        public abstract string Name { get; }
        // public List<Entity> Entities { get; protected set; } = new List<Entity>();
        public World Scene { get; protected set; }
        public List<ISystem<double>> DrawSystems { get; protected set; } = new List<ISystem<double>>();
        public float TimeScale { get; set; } = 1f;
        public MeshSystem DrawMeshSys { get; protected set; }

        public GameApp()
        {
            Scene = new World();
            Current = this;
        }

        public virtual void Setup()
        {
            DrawMeshSys = new MeshSystem(Scene);
            DrawSystems.Add(DrawMeshSys);

            BufferPool = new BufferPool();
            var targetThreadCount = Math.Max(1, Environment.ProcessorCount > 4 ? Environment.ProcessorCount - 2 : Environment.ProcessorCount - 1);
            Simulation = Simulation.Create(BufferPool, new Physics.NarrowPhaseCallbacks(), new Physics.PoseIntegratorCallbacks(new System.Numerics.Vector3(0, -10, 0)), new SolveDescription(8, 1));
            dispatcher = new ThreadDispatcher(targetThreadCount);
        }

        public virtual void PreDraw(Renderer renderer, double dt)
        {
            // Scene.Publish();
        }

        public virtual void Draw(Renderer renderer, double dt)
        {
            // Scene.Publish();
            foreach (var sys in DrawSystems)
            {
                sys.Update(dt);
            }
        }

        public virtual void Tick(double dt)
        {
            // Scene.Publish();
            if (TimeScale > 0f)
                Simulation.Timestep(MathF.Min(0.5f, (float)dt) * TimeScale, dispatcher);
        }

        public virtual void ReCreate()
        {
            // Scene.Publish();
        }

        public virtual void Unload()
        {
            // Scene.Publish();
        }

        public virtual void Dispose()
        {
            Scene.Dispose();
            dispatcher.Dispose();
            BufferPool.Clear();
        }
    }
}