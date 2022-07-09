namespace ExTween
{
    public static class EaseFunctions
    {
        public delegate float Delegate(float x);

        public static float Linear(float x)
        {
            return x;
        }
    }
}
