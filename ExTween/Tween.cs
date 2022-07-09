using System;

namespace ExTween
{
    public interface ITween
    {
        public float UpdateAndGetOverflow(float dt);
        public bool IsDone();
        public void Reset();
        float TotalDuration { get; }
        
        public void Update(float dt)
        {
            UpdateAndGetOverflow(dt);
        }
    }
    
    public class Tween<T> : ITween
    {
        private T startingValue;
        private readonly float duration;
        private readonly EaseFunction easeFunction;
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

        public float CurrentTime { get; private set; }

        public float TotalDuration => this.duration;

        public float UpdateAndGetOverflow(float dt)
        {
            if (CurrentTime == 0)
            {
                // Re-set the starting value, it might have changed since constructor
                // (or we might be running the tween a second time)
                this.startingValue = this.tweenable.Value;
            }
            
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
}
