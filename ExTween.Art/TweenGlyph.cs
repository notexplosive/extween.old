namespace ExTween.Art
{
    public class TweenGlyph : TweenPathDrawer
    {
        private readonly IFont font;
        private readonly char letter;

        public TweenGlyph(TweenPath tweenPath, IFont font, char letter) : base(tweenPath)
        {
            this.font = font;
            this.letter = letter;
        }

        public override FloatXyPair Size => this.font.CharacterSize(this.letter);
        
        // TODO: NOTE TO SELF to make kerning work!
        // public FloatXyPair GlyphBounds { get; }
        // TweenGlyph (NOT parent class, NOT TweenPathDrawer!) should have a "Bounds" field, TweenFigure does not need a bounds
    }
}
