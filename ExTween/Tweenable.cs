﻿using System;

namespace ExTween
{
    public abstract class Tweenable<T>
    {
        public delegate T Getter();
        public delegate void Setter(T value);

        private readonly Getter getter;
        private readonly Setter setter;

        public T Value
        {
            get => this.getter();
            private set => this.setter(value);
        }

        protected Tweenable(T initializedValue)
        {
            T capturedValue = initializedValue;
            this.getter = () => capturedValue;
            this.setter = value => capturedValue = value;
            Value = initializedValue;
        }

        protected Tweenable(Getter getter, Setter setter)
        {
            this.getter = getter;
            this.setter = setter;
        }

        /// <summary>
        /// The equivalent of: `startingValue + (targetValue - startingValue) * percent` for the template type.
        /// </summary>
        /// <param name="startingValue">Starting value of the interpolation</param>
        /// <param name="targetValue">Ending value of the interpolation</param>
        /// <param name="percent">Progress along interpolation from 0f to 1f</param>
        /// <returns>The interpolated value</returns>
        public abstract T Lerp(T startingValue, T targetValue, float percent);
        
        /// <summary>
        /// Calls the tweenable's Setter with value
        /// You probably don't want to call this. It's here to cause friction on purpose.
        /// </summary>
        /// <param name="value"></param>
        public void ForceSetValue(T value)
        {
            Value = value;
        }

        public override string ToString()
        {
            return $"Tweenable: {Value.ToString()}";
        }
    }

    public class TweenableInt : Tweenable<int>
    {
        public TweenableInt() : base(0)
        {
        }

        public TweenableInt(int i) : base(i)
        {
        }
        
        public TweenableInt(Getter getter, Setter setter) : base(getter, setter)
        {
        }
        
        public override int Lerp(int startingValue, int targetValue, float percent)
        {
            return (int)(startingValue + (targetValue - startingValue) * percent);
        }
    }

    public class TweenableFloat : Tweenable<float>
    {
        public TweenableFloat() : base(0)
        {
        }

        public TweenableFloat(float i) : base(i)
        {
        }
        
        public TweenableFloat(Getter getter, Setter setter) : base(getter, setter)
        {
        }
        
        public override float Lerp(float startingValue, float targetValue, float percent)
        {
            return startingValue + (targetValue - startingValue) * percent;
        }
    }
}