namespace ExTween.Art
{
    public class TweenGlyph : DrawableTweenPath
    {
        private readonly IFont font;
        private readonly char letter;

        public TweenGlyph(TweenPath path, IFont font, char letter) : base(path)
        {
            this.font = font;
            this.letter = letter;
        }

        public override FloatXyPair Size => this.font.CharacterSize(this.letter);

        protected override PenState TransformState(PenState state, FloatXyPair renderOffset)
        {
            return new PenState(
                new FloatXyPair(state.Position.X * Size.X, state.Position.Y * Size.Y) + renderOffset,
                state.ShouldDraw);
        }
    }
}
