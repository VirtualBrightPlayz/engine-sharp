using System;
using System.Collections.Generic;
using BepuPhysics;
using BepuUtilities;
using BepuUtilities.Memory;
using Engine.Assets.Rendering;
using Engine.Game.Entities;

namespace Engine.Game
{
    public abstract class GameApp : IDisposable
    {
        public static GameApp Current { get; private set; }
        public Simulation Simulation { get; protected set; }
        public BufferPool BufferPool { get; protected set; }
        public ThreadDispatcher dispatcher { get; protected set; }
        public abstract string Name { get; }
        public List<Entity> Entities { get; protected set; } = new List<Entity>();
        public float TimeScale { get; set; } = 1f;

        public GameApp()
        {
            Current = this;
        }

        public virtual void Setup()
        {
            BufferPool = new BufferPool();
            var targetThreadCount = Math.Max(1, Environment.ProcessorCount > 4 ? Environment.ProcessorCount - 2 : Environment.ProcessorCount - 1);
            Simulation = Simulation.Create(BufferPool, new Physics.NarrowPhaseCallbacks(), new Physics.PoseIntegratorCallbacks(new System.Numerics.Vector3(0, -10, 0)), new SolveDescription(8, 1));
            dispatcher = new ThreadDispatcher(targetThreadCount);
        }

        public virtual void PreDraw(Renderer renderer, double dt)
        {
            foreach (var ent in Entities.ToArray())
            {
                ent.PreDraw(renderer, dt);
            }
        }

        public virtual void Draw(Renderer renderer, double dt)
        {
            foreach (var ent in Entities.ToArray())
            {
                ent.Draw(renderer, dt);
            }
        }

        public virtual void Tick(double dt)
        {
            foreach (var ent in Entities.ToArray())
            {
                ent.Tick(dt);
            }
            if (TimeScale > 0f)
                Simulation.Timestep(MathF.Min(0.5f, (float)dt) * TimeScale, dispatcher);
        }

        public virtual void Dispose()
        {
            dispatcher.Dispose();
            BufferPool.Clear();
        }
    }
}