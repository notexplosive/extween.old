
namespace ExTween.Art
{
    /// <summary>
    /// Thing that has a Position, Angle, and Scale that is Drawable
    /// </summary>
    public abstract class TweenableDrawable
    {
        public TweenableFloatXyPair Position { get; set; } = new TweenableFloatXyPair();
        public TweenableFloat Angle { get; set; } = new TweenableFloat();
        public TweenableFloat Scale { get; set; } = new TweenableFloat();
        public abstract FloatXyPair Size { get; }

        public abstract void Draw(Painter painter);
    }
}
