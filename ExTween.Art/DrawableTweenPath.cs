namespace ExTween.Art
{
    public abstract class DrawableTweenPath : ITweenPath, IHasSize
    {
        private readonly TweenPath path;

        protected DrawableTweenPath(TweenPath path)
        {
            this.path = path;
        }
        
        public float Duration => this.path.Duration;

        public PenState StateAtTime(float time)
        {
            return TransformState(this.path.StateAtTime(time), FloatXyPair.Zero);
        }

        public void Draw(Painter painter, FloatXyPair renderOffset,  int numberOfSegments, float thickness = 1f)
        {
            if (this.path.Tween is TweenCollection {ChildrenWithDurationCount: 0})
            {
                // Empty tween, don't bother drawing anything
                return;
            }

            var prevPoint = new FloatXyPair();
            var hasStarted = false;
            var previousShouldDraw = true;

            var keyframes = this.path.GetKeyframes(numberOfSegments);

            for (var i = 0; i < keyframes.Length; i++)
            {
                var color = StrokeColor.Black;

                var currentKeyframeTime = keyframes[i];
                var stateOffsetByRenderPosition = TransformState(this.path.StateAtTime(currentKeyframeTime), renderOffset);
                var currentPoint = stateOffsetByRenderPosition.Position;

                if (previousShouldDraw && hasStarted && prevPoint != currentPoint)
                {
                    painter.DrawLine(prevPoint, currentPoint, thickness, color);
                }

                previousShouldDraw = stateOffsetByRenderPosition.ShouldDraw;
                prevPoint = currentPoint;
                hasStarted = true;
            }
        }
        
        public abstract FloatXyPair Size { get; }
        protected abstract PenState TransformState(PenState state, FloatXyPair renderOffset);
    }
}
