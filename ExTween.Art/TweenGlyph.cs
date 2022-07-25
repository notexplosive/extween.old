namespace ExTween.Art
{
    public class TweenGlyph : Drawable, ITweenPath
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

        public override FloatXyPair Size => this.font.CharacterSize(this.letter);

        public float Thickness { get; set; }
        public int NumberOfSegments { get; set; }
        public FloatXyPair RenderOffset { get; set; }

        public TweenPathState GetPreciseStateAtTime(float time)
        {
            return ScaleState(this.path.GetPreciseStateAtTime(time));
        }
        
        private TweenPathState ScaleState(TweenPathState state)
        {
            return new TweenPathState(
                state.Position * this.font.FontSize / 2f + RenderOffset,
                state.ShouldDraw);
        }

        public float Duration => this.path.Duration;

        public override void Draw(Painter painter)
        {
            if (this.path.Tween is TweenCollection {ChildrenWithDurationCount: 0})
            {
                // Empty tween, don't bother drawing anything
                return;
            }

            var prevPoint = new FloatXyPair();
            var hasStarted = false;
            var previousShouldDraw = true;

            var keyframes = this.path.GetKeyframes(NumberOfSegments);

            for (var i = 0; i < keyframes.Length; i++)
            {
                var color = StrokeColor.Black;

                var currentKeyframeTime = keyframes[i];
                var state = GetPreciseStateAtTime(currentKeyframeTime);
                var currentPoint = state.Position;

                if (previousShouldDraw && hasStarted && prevPoint != currentPoint)
                {
                    painter.DrawLine(prevPoint, currentPoint, Thickness, color);
                }

                previousShouldDraw = state.ShouldDraw;
                prevPoint = currentPoint;
                hasStarted = true;
            }
        }
    }
}
