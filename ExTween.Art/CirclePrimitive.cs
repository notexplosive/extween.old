namespace ExTween.Art
{
    public class CirclePrimitive : Drawable
    {
        public TweenableFloat Radius { get; set; } = new TweenableFloat(1);
        public TweenableInt Segments { get; } = new TweenableInt(3);
        public StrokeColor Color { get; set; } = StrokeColor.Black;

        public CirclePrimitive(float radius)
        {
            Radius.Value = radius;
        }
        
        public override FloatXyPair Size => new FloatXyPair(Radius * 2);
        public override void Draw(Painter painter)
        {
            painter.DrawFilledCircle(Position, Radius, Segments, Color);
        }
    }
}
