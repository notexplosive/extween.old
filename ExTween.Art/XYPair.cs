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
        public FloatXyPair(float x, float y)
        {
            X = x;
            Y = y;
        }

        public float X { get; set; }
        public float Y { get; set; }

        public static FloatXyPair operator *(FloatXyPair left, float right)
        {
            return new FloatXyPair(left.X * right, left.Y * right);
        }

        public static FloatXyPair operator +(FloatXyPair left, FloatXyPair right)
        {
            return new FloatXyPair(left.X + right.X, left.Y + right.Y);
        }

        public static FloatXyPair operator -(FloatXyPair left, FloatXyPair right)
        {
            return new FloatXyPair(left.X - right.X, left.Y - right.Y);
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
            return startingValue + (targetValue - startingValue) * percent;
        }
    }
}
