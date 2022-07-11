
namespace ExTween.Text
{
    public abstract class TweenableVisualElement
    {
        public TweenableFloatXyPair Position { get; set; } = new TweenableFloatXyPair();
        public TweenableFloat Angle { get; set; } = new TweenableFloat();
        public TweenableFloat Scale { get; set; } = new TweenableFloat();
        public abstract FloatXyPair Size { get; }

        public abstract void Draw(Painter painter);
    }
}
