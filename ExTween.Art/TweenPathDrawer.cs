namespace ExTween.Art
{
    public abstract class TweenPathDrawer : ITweenPath, IHasSize
    {
        // ReSharper disable once InconsistentNaming
        protected readonly TweenPath tweenPath;

        protected TweenPathDrawer(TweenPath tweenPath)
        {
            this.tweenPath = tweenPath;
        }

        public abstract FloatXyPair Size { get; }

        public float Duration => this.tweenPath.Duration;

        public PenState StateAtTime(float time)
        {
            return TransformState(this.tweenPath.StateAtTime(time), FloatXyPair.Zero);
        }

        public void Draw(Painter painter, FloatXyPair renderOffset, int numberOfSegments, float thickness = 1f)
        {
            if (this.tweenPath.Tween is TweenCollection {ChildrenWithDurationCount: 0})
            {
                // Empty tween, don't bother drawing anything
                return;
            }

            var prevPoint = new FloatXyPair();
            var hasStarted = false;
            var previousShouldDraw = true;

            var keyframes = this.tweenPath.GetKeyframes(numberOfSegments);

            for (var i = 0; i < keyframes.Length; i++)
            {
                var color = StrokeColor.Black;

                var currentKeyframeTime = keyframes[i];
                var stateOffsetByRenderPosition =
                    TransformState(this.tweenPath.StateAtTime(currentKeyframeTime), renderOffset);
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

        private PenState TransformState(PenState state, FloatXyPair renderOffset)
        {
            return new PenState(
                new FloatXyPair(state.Position.X * Size.X, state.Position.Y * Size.Y) + renderOffset,
                state.ShouldDraw);
        }
    }
}
