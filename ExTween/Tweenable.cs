using System;

namespace ExTween
{
    public delegate float EaseFunction(float x);

    public abstract class Tweenable<T>
    {
        public delegate T Getter();
        public delegate void Setter(T value);

        private readonly Getter getter;
        private readonly Setter setter;

        public T Value
        {
            get => this.getter();
            set => this.setter(value);
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

        public abstract T Lerp(T startingValue, T targetValue, float percent);
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

        public TweenableFloat(int i) : base(i)
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