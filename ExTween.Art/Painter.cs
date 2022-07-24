namespace ExTween.Art
{
    public abstract class Painter
    {
        public abstract void DrawLine(FloatXyPair p1, FloatXyPair p2, float thickness, StrokeColor strokeColor);
        public abstract void DrawFilledCircle(FloatXyPair position, float radius, int segments, StrokeColor strokeColor);
    }
}
