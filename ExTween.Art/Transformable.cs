
namespace ExTween.Art
{
    public interface ITransformable
    {
        public TweenableFloatXyPair Position { get; }
        public TweenableFloat Angle { get; }
        public TweenableFloat Scale { get; }
    }
    
    /// <summary>
    /// Thing that has a Position, Angle, and Scale
    /// </summary>
    public abstract class Transformable : ITransformable
    {
        public TweenableFloatXyPair Position { get; } = new TweenableFloatXyPair();
        public TweenableFloat Angle { get; } = new TweenableFloat();
        public TweenableFloat Scale { get; } = new TweenableFloat();
    }

    public interface ISlideElement : ITransformable, IEasyDrawable, IHasSize
    {
    }
}
