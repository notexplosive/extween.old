namespace ExTween.Art
{
    public class TweenGlyph : ITweenPath, IHasSize
    {
        private readonly IFont font;
        private readonly char letter;
        private readonly TweenPath path;

        public TweenGlyph(TweenPath path, IFont font, char letter)
        {
            this.font = font;
            this.letter = letter;
            this.path = path;
        }

        public FloatXyPair Size => this.font.CharacterSize(this.letter);

        public TweenPathState GetPreciseStateAtTime(float time, FloatXyPair renderOffset)
        {
            return ScaleState(this.path.GetPreciseStateAtTime(time), renderOffset);
        }
        
        public TweenPathState GetPreciseStateAtTime(float time)
        {
            return ScaleState(this.path.GetPreciseStateAtTime(time), FloatXyPair.Zero);
        }
        
        private TweenPathState ScaleState(TweenPathState state, FloatXyPair renderOffset)
        {
            return new TweenPathState(
                state.Position * this.font.FontSize / 2f + renderOffset,
                state.ShouldDraw);
        }

        public float Duration => this.path.Duration;

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
                var state = GetPreciseStateAtTime(currentKeyframeTime, renderOffset);
                var currentPoint = state.Position;

                if (previousShouldDraw && hasStarted && prevPoint != currentPoint)
                {
                    painter.DrawLine(prevPoint, currentPoint, thickness, color);
                }

                previousShouldDraw = state.ShouldDraw;
                prevPoint = currentPoint;
                hasStarted = true;
            }
        }
    }
}
