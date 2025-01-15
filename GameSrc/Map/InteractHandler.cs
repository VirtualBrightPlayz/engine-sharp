using System.Collections.Concurrent;
using System.Numerics;
using BepuPhysics;
using BepuPhysics.Collidables;
using BepuPhysics.Trees;
using Engine.Game.Entities;
using GameSrc;

namespace GameSrc.Map
{
    public class InteractHandler : IRayHitHandler
    {
        public static ConcurrentDictionary<StaticHandle, IInteractable> Interactables = new ConcurrentDictionary<StaticHandle, IInteractable>();
        public SCPCBPlayerEntity player;

        public bool AllowTest(CollidableReference collidable)
        {
            return collidable.Mobility == CollidableMobility.Static;
        }

        public bool AllowTest(CollidableReference collidable, int childIndex)
        {
            return collidable.Mobility == CollidableMobility.Static;
        }

        public void OnRayHit(in RayData ray, ref float maximumT, float t, Vector3 normal, CollidableReference collidable, int childIndex)
        {
            if (collidable.Mobility == CollidableMobility.Static && Interactables.TryGetValue(collidable.StaticHandle, out IInteractable ent))
            {
                switch (ray.Id)
                {
                    case 0: // press
                        ent.Press();
                        break;
                    case 1: // highlight
                        break;
                }
            }
        }
    }
}