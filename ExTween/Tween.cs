﻿using System;

namespace ExTween
{
    public class Tween<T>
    {
        private Tweenable<T> tweenable;
        private T startingValue;
        private readonly T targetValue;
        private readonly float duration;
        private readonly EaseFunction easeFunction;

        public Tween(Tweenable<T> tweenable, T targetValue, float duration, EaseFunction easeFunction)
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
