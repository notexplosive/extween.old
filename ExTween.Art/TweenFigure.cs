namespace ExTween.Art
{
    public class TweenFigure : TweenPathDrawer
    {
        public TweenFigure(TweenPath tweenPath, FloatXyPair renderScale) : base(tweenPath)
        {
            Size = renderScale;
        }

        public TweenFigure(FloatXyPair renderScale) : base (new TweenPath())
        {
            Size = renderScale;
        }
        
        public TweenPath TweenPath => this.tweenPath;
        public override FloatXyPair Size { get; }
    }
}
