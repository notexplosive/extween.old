namespace ExTween.Art
{
    public interface IFont
    {
        float FontSize { get; }
        FloatXyPair CharacterSize(char c);
        public TweenGlyph GetTweenGlyphForLetter(char letter)
        {
            var path = Typeface.GetPathForLetter(letter);
            return new TweenGlyph(path, this, letter);
        }
    }
}
