namespace ExTween.Art
{
    public abstract class Painter
    {
        public abstract void DrawLine(FloatXyPair p1, FloatXyPair p2, float thickness, ExColor exColor);

        public abstract void DrawCircle(FloatXyPair center, float radius, float thickness, int numberOfSegments,
            ExColor exColor);
    }
}
