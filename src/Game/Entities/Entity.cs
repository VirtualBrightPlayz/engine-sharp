using System;
using System.Numerics;
using BepuUtilities;

namespace Engine.Game.Entities
{
    [Flags]
    public enum TransformDirtyFlags : byte
    {
        Position = 1,
        Rotation = 2,
    }

    public class Entity : IDisposable
    {
        public string Name { get; set; }
        public GameApp Game => Program.Game;
        public Vector3 Position { get; set; } = Vector3.Zero;
        public Vector3 LocalUp { get; set; } = Vector3.UnitY;
        public Quaternion Rotation { get; set; } = Quaternion.Identity;
        public Vector3 Scale { get; set; } = Vector3.One;
        public Matrix4x4 WorldMatrix => Matrix4x4.CreateFromQuaternion(Rotation) * Matrix4x4.CreateScale(Scale) * Matrix4x4.CreateTranslation(Position);

        public Entity()
        {
        }

        public Entity(string name) : this()
        {
            Name = name;
        }

        public virtual void MarkTransformDirty(TransformDirtyFlags flags)
        {

        }

        public virtual void PreDraw(double dt)
        {
        }

        public virtual void Draw(double dt)
        {
        }

        public virtual void Tick(double dt)
        {
        }

        public virtual void Dispose()
        {
        }
    }
}