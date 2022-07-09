namespace ExTween
{
    public delegate float EaseFunction(float x);

    public static class EaseFunctions
    {
        public static float Linear(float x)
        {
            return x;
        }
    }
}
