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

        protected override TweenPathState ScaleState(TweenPathState state, FloatXyPair renderOffset)
        {
            return new TweenPathState(state.Position * this.font.FontSize / 2f + renderOffset, state.ShouldDraw);
        }
    }
}
