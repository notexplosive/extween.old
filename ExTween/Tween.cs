using System;

namespace ExTween
{
    public class Tween<T>
    {
        private readonly float duration;
        private readonly EaseFunction easeFunction;
        private readonly T startingValue;
        private readonly T targetValue;
        private readonly Tweenable<T> tweenable;

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

        public float UpdateAndGetOverflow(float dt)
        {
            CurrentTime += dt;
            
            float overflow = CurrentTime - this.duration;
            float currentTimeMinusOverflow = CurrentTime;

            if (overflow > 0)
            {
                currentTimeMinusOverflow -= overflow;
            }
            
            var percent = currentTimeMinusOverflow / this.duration;

            this.tweenable.ForceSetValue(
                this.tweenable.Lerp(
                    this.startingValue,
                    this.targetValue,
                    this.easeFunction(percent)));

            return Math.Max(overflow, 0);
        }

        public bool IsDone()
        {
            return CurrentTime >= this.duration;
        }

        public void Reset()
        {
            CurrentTime = 0;
        }
    }

    public class WaitSecondsTween
    {
        private readonly float startingTime;
        private float timer;

        public WaitSecondsTween(float startingTime)
        {
            this.startingTime = startingTime;
            this.timer = startingTime;
        }
        
        public void UpdateAndGetOverflow(int dt)
        {
            this.timer -= dt;
        }

        public bool IsDone()
        {
            return this.timer <= 0;
        }

        public void Reset()
        {
            this.timer = this.startingTime;
        }
    }
}
