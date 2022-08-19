using System;
using System.Numerics;

namespace Engine.Game
{
    public static class MathUtils
    {
        public static float Clamp(float value, float min, float max)
        {
            return MathF.Min(max, MathF.Max(min, value));
        }

        public static float MoveTowards(float current, float target, float maxDistanceDelta)
        {
            float final = current;
            final += maxDistanceDelta;
            if (final > target)
                final = target;
            return final;
        }

        public static Vector3 MoveTowards(Vector3 current, Vector3 target, float maxDistanceDelta)
        {
            Vector3 final = current;
            if (current.LengthSquared() > 0 || target.LengthSquared() > 0)
                final += (Vector3.Normalize(target - current) * maxDistanceDelta);
            if ((current - target).LengthSquared() <= maxDistanceDelta * maxDistanceDelta)
                final = target;
            return final;
        }
    }
}