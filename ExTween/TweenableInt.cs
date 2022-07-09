namespace ExTween
{
    public class TweenableInt
    {
        public TweenableInt(int startingValue)
        {
            Value = startingValue;
        }

        public int Value { get; set; }

        public int Lerp(int startingValue, int targetValue, float percent)
        {
            return (int)(startingValue + (targetValue - startingValue) * percent);
        }
    }
}