using System;

namespace ExTween
{
    public delegate float EaseFunction(float x);
    
    public class Tween
    {
        private TweenableInt tweenable;
        private int startingValue;
        private readonly int targetValue;
        private readonly float duration;
        private readonly EaseFunction easeFunction;

        public Tween(TweenableInt tweenable, int targetValue, float duration, EaseFunction easeFunction)
        {
            this.tweenable = tweenable;
            this.targetValue = targetValue;
            this.duration = duration;
            this.easeFunction = easeFunction;
            this.startingValue = tweenable.Value;
            CurrentTime = 0;
        }

        public float CurrentTime { get; set; }

        public void updateAndGetOverflow(float f)
        {
            CurrentTime += f;
            var percent = CurrentTime / this.duration;

            this.tweenable.Value = this.tweenable.Lerp(this.startingValue, this.targetValue, this.easeFunction(percent));
        }
    }

    public static class EaseFunctions
    {
        public static float Linear(float x)
        {
            return x;
        }
    }
}
