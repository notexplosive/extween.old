namespace ExTween
{
    public static class Ease
    {
        public delegate float Delegate(float x);

        public static float Linear(float x)
        {
            return x;
        }
    }
}
