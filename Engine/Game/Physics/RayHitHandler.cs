using System;
using System.Numerics;
using BepuPhysics;
using BepuPhysics.Collidables;
using BepuPhysics.Trees;

namespace Engine.Game.Physics
{
    public class RayHitHandler : IRayHitHandler
    {
        private Action<RayData, float, Vector3, CollidableReference, int> callback;
        private CollidableReference[] ignore;

        public RayHitHandler(Action<RayData, float, Vector3, CollidableReference, int> callback, params CollidableReference[] ignore)
        {
            this.callback = callback;
            this.ignore = ignore;
        }

        public bool AllowTest(CollidableReference collidable)
        {
            return Array.IndexOf(ignore, collidable) == -1;
        }

        public bool AllowTest(CollidableReference collidable, int childIndex)
        {
            return true;
        }

        public void OnRayHit(in RayData ray, ref float maximumT, float t, Vector3 normal, CollidableReference collidable, int childIndex)
        {
            callback?.Invoke(ray, t, normal, collidable, childIndex);
        }
    }
}