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
            final += (Normalized(current - target) * maxDistanceDelta);
            if ((final - target).LengthSquared() > maxDistanceDelta * maxDistanceDelta)
                final = target;
            return final;
        }

        public static Vector3 Normalized(Vector3 current)
        {
            float len = current.LengthSquared();
            if (len > 0f)
            {
                current /= MathF.Sqrt(len);
            }
            return current;
        }
    }
}