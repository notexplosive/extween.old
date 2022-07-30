using ExTween;
using ExTween.Art;

namespace MonoGameDemo
{
    public abstract class SimpleSlideElement : ISlideElement
    {
        public TweenableFloatXyPair Position { get; } = new TweenableFloatXyPair();
        public TweenableFloat Angle { get; } = new TweenableFloat();
        public TweenableFloat Scale { get; } = new TweenableFloat();
        public abstract void Draw(Painter painter);
        public abstract FloatXyPair Size { get; }
    }
}
