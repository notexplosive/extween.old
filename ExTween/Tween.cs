using System;

namespace ExTween
{
    public interface ITween
    {
        float TotalDuration { get; }

        /// <summary>
        ///     Updates the tween and returns the overflow.
        /// </summary>
        /// <param name="dt"></param>
        /// <returns>The amount overflowed, eg: if a tween only had 0.1 seconds left but `dt` was 0.3, there is an overflow of 0.2. </returns>
        public float Update(float dt);

        public bool IsDone();
        public void Reset();
        public void JumpTo(float time);
    }

    public class Tween<T> : ITween
    {
        private readonly EaseFunctions.Delegate easeDelegate;
        private readonly T targetValue;
        private readonly Tweenable<T> tweenable;
        private T startingValue;

        public Tween(Tweenable<T> tweenable, T targetValue, float duration, EaseFunctions.Delegate easeDelegate)
        {
            this.tweenable = tweenable;
            this.targetValue = targetValue;
            this.easeDelegate = easeDelegate;
            this.startingValue = tweenable.Value;
            TotalDuration = duration;
            CurrentTime = 0;
        }

        public float CurrentTime { get; private set; }

        public float TotalDuration { get; }

        public float Update(float dt)
        {
            if (CurrentTime == 0)
            {
                // Re-set the starting value, it might have changed since constructor
                // (or we might be running the tween a second time)
                this.startingValue = this.tweenable.Value;
            }

            CurrentTime += dt;

            var overflow = CurrentTime - TotalDuration;

            if (overflow > 0)
            {
                CurrentTime -= overflow;
            }

            ApplyTimeToValue();

            return Math.Max(overflow, 0);
        }

        private void ApplyTimeToValue()
        {
            var percent = CurrentTime / TotalDuration;

            this.tweenable.ForceSetValue(
                this.tweenable.Lerp(
                    this.startingValue,
                    this.targetValue,
                    this.easeDelegate(percent)));
        }

        public bool IsDone()
        {
            return CurrentTime >= TotalDuration;
        }

        public void Reset()
        {
            CurrentTime = 0;
        }

        public void JumpTo(float time)
        {
            CurrentTime = time;
            ApplyTimeToValue();
        }
    }
}
