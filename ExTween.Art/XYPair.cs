using System;

namespace ExTween.Art
{
    public interface IXyPair<T>
    {
        public T X { get; set; }
        public T Y { get; set; }
    }

    public struct IntXyPair : IXyPair<int>
    {
        public IntXyPair(int x, int y)
        {
            X = x;
            Y = y;
        }

        public int X { get; set; }
        public int Y { get; set; }

        public static IntXyPair operator *(IntXyPair left, float right)
        {
            return new IntXyPair((int) (left.X * right), (int) (left.Y * right));
        }

        public static IntXyPair operator +(IntXyPair left, IntXyPair right)
        {
            return new IntXyPair(left.X + right.X, left.Y + right.Y);
        }

        public static IntXyPair operator -(IntXyPair left, IntXyPair right)
        {
            return new IntXyPair(left.X - right.X, left.Y - right.Y);
        }

        public override string ToString()
        {
            return $"{X}, {Y}";
        }
    }

    public class TweenableIntXyPair : Tweenable<IntXyPair>
    {
        public TweenableIntXyPair() : base(new IntXyPair())
        {
        }

        public TweenableIntXyPair(IntXyPair initializedValue) : base(initializedValue)
        {
        }

        public TweenableIntXyPair(Getter getter, Setter setter) : base(getter, setter)
        {
        }

        public override IntXyPair Lerp(IntXyPair startingValue, IntXyPair targetValue, float percent)
        {
            return startingValue + (targetValue - startingValue) * percent;
        }
    }

    public struct FloatXyPair : IXyPair<float>
    {
        public bool Equals(FloatXyPair other)
        {
            return X.Equals(other.X) && Y.Equals(other.Y);
        }

        public override bool Equals(object? obj)
        {
            return obj is FloatXyPair other && Equals(other);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(X, Y);
        }

        public static bool operator ==(FloatXyPair left, FloatXyPair right)
        {
            return left.Equals(right);
        }

        public static bool operator !=(FloatXyPair left, FloatXyPair right)
        {
            return !left.Equals(right);
        }

        public FloatXyPair(float x, float y)
        {
            X = x;
            Y = y;
        }

        public FloatXyPair(float xy)
        {
            X = xy;
            Y = xy;
        }

        public float X { get; set; }
        public float Y { get; set; }

        public static FloatXyPair operator *(FloatXyPair left, float right)
        {
            return new FloatXyPair(left.X * right, left.Y * right);
        }

        public static FloatXyPair operator /(FloatXyPair left, float right)
        {
            return new FloatXyPair(left.X / right, left.Y / right);
        }

        public static FloatXyPair operator +(FloatXyPair left, FloatXyPair right)
        {
            return new FloatXyPair(left.X + right.X, left.Y + right.Y);
        }

        public static FloatXyPair operator -(FloatXyPair left, FloatXyPair right)
        {
            return new FloatXyPair(left.X - right.X, left.Y - right.Y);
        }

        public float Length()
        {
            return MathF.Sqrt(X * X + Y * Y);
        }

        public IntXyPair ToIntXy()
        {
            return new IntXyPair((int) X, (int) Y);
        }

        public override string ToString()
        {
            return $"{X}, {Y}";
        }

        public static FloatXyPair Lerp(FloatXyPair start, FloatXyPair end, float percent)
        {
            return start + (end - start) * percent;
        }
    }

    public class TweenableFloatXyPair : Tweenable<FloatXyPair>
    {
        public TweenableFloatXyPair() : base(new FloatXyPair())
        {
        }

        public TweenableFloatXyPair(FloatXyPair initializedValue) : base(initializedValue)
        {
        }

        public TweenableFloatXyPair(Getter getter, Setter setter) : base(getter, setter)
        {
        }

        public override FloatXyPair Lerp(FloatXyPair startingValue, FloatXyPair targetValue, float percent)
        {
            return FloatXyPair.Lerp(startingValue, targetValue, percent);
        }
    }
}
