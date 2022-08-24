using System.Threading.Tasks;
using BepuPhysics;
using BepuPhysics.Collidables;

namespace Engine.Game.Entities
{
    public class PhysicsEntity : Entity
    {
        public float Mass = 1f;
        public TypedIndex? shapeIndex;
        public BodyHandle? bodyHandle;
        public StaticHandle? staticHandle;

        public PhysicsEntity() : base()
        {
        }

        public PhysicsEntity(string name) : base(name)
        {
        }

        public override void MarkTransformDirty(TransformDirtyFlags flags)
        {
            base.MarkTransformDirty(flags);
            if (bodyHandle.HasValue)
            {
                if (flags.HasFlag(TransformDirtyFlags.Position))
                    Game.Simulation.Bodies[bodyHandle.Value].Pose.Position = Position;
                if (flags.HasFlag(TransformDirtyFlags.Rotation))
                    Game.Simulation.Bodies[bodyHandle.Value].Pose.Orientation = Rotation;
            }
            if (staticHandle.HasValue)
            {
                if (flags.HasFlag(TransformDirtyFlags.Position))
                    Game.Simulation.Statics[staticHandle.Value].Pose.Position = Position;
                if (flags.HasFlag(TransformDirtyFlags.Rotation))
                    Game.Simulation.Statics[staticHandle.Value].Pose.Orientation = Rotation;
                Game.Simulation.Statics.UpdateBounds(staticHandle.Value);
            }
        }

        public override async Task Tick(double dt)
        {
            await base.Tick(dt);
            if (bodyHandle.HasValue)
            {
                Position = Game.Simulation.Bodies[bodyHandle.Value].Pose.Position;
                Rotation = Game.Simulation.Bodies[bodyHandle.Value].Pose.Orientation;
            }
            if (staticHandle.HasValue)
            {
                Position = Game.Simulation.Statics[staticHandle.Value].Pose.Position;
                Rotation = Game.Simulation.Statics[staticHandle.Value].Pose.Orientation;
            }
        }

        public override void Dispose()
        {
            base.Dispose();
            if (shapeIndex.HasValue)
                Game.Simulation.Shapes.Remove(shapeIndex.Value);
            if (bodyHandle.HasValue)
                Game.Simulation.Bodies.Remove(bodyHandle.Value);
            if (staticHandle.HasValue)
                Game.Simulation.Statics.Remove(staticHandle.Value);
        }
    }
}