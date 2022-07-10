using System;
using System.Diagnostics;

namespace ExTween
{
    public interface ITween
    {
        public ITweenDuration TotalDuration { get; }

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

    [DebuggerDisplay("({startingValue}) -> ({targetValue}), Progress: {(int)(CurrentTime / TotalDuration * 100)}%")]
    public class Tween<T> : ITween
    {
        private readonly Ease.Delegate ease;
        private readonly T targetValue;
        private readonly Tweenable<T> tweenable;
        private T startingValue;

        public Tween(Tweenable<T> tweenable, T targetValue, float duration, Ease.Delegate ease)
        {
            this.tweenable = tweenable;
            this.targetValue = targetValue;
            this.ease = ease;
            this.startingValue = tweenable.Value;
            TotalDuration = new KnownTweenDuration(duration);
            CurrentTime = 0;
        }

        public float CurrentTime { get; private set; }

        public ITweenDuration TotalDuration { get; }

        public float Update(float dt)
        {
            if (CurrentTime == 0)
            {
                // Re-set the starting value, it might have changed since constructor
                // (or we might be running the tween a second time)
                this.startingValue = this.tweenable.Value;
            }

            CurrentTime += dt;

            var overflow = CurrentTime - TotalDuration.Get();

            if (overflow > 0)
            {
                CurrentTime -= overflow;
            }

            ApplyTimeToValue();

            return Math.Max(overflow, 0);
        }

        private void ApplyTimeToValue()
        {
            var percent = CurrentTime / TotalDuration.Get();

            this.tweenable.ForceSetValue(
                this.tweenable.Lerp(
                    this.startingValue,
                    this.targetValue,
                    this.ease(percent)));
        }

        public bool IsDone()
        {
            return CurrentTime >= TotalDuration.Get();
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
    
    public interface ITweenDuration
    {
        public float Get();
    }
    
    public readonly struct KnownTweenDuration : ITweenDuration
    {
        public KnownTweenDuration(float duration)
        {
            Value = duration;
        }

        private float Value { get; }

        public float Get()
        {
            return Value;
        }

        public static implicit operator float(KnownTweenDuration me)
        {
            return me.Get();
        }
    }
    
    
    public class UnknownTweenDuration : ITweenDuration
    {
        public float Get()
        {
            throw new Exception("Value unknown");
        }
    }
}
