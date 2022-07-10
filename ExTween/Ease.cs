using System;

namespace ExTween
{
    public static class Ease
    {
        public delegate float Delegate(float x);

        public static float Linear(float x)
        {
            return x;
        }

        public static float SineSlowFast(float x)
        {
            return 1 - MathF.Cos((x * MathF.PI) / 2);
        }

        public static float SineFastSlow(float x)
        {
            return MathF.Sin((x * MathF.PI) / 2);
        }
    }
}
