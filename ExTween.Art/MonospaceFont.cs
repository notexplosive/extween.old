namespace ExTween.Art
{
    public class MonospaceFont : IFont
    {
        public MonospaceFont(float fontSize)
        {
            FontSize = fontSize;
        }

        public float FontSize { get; }
        
        public TweenGlyph GetTweenGlyphForLetter(char letter)
        {
            var path = Typeface.GetPathForLetter(letter);
            return new TweenGlyph(path, this, letter);
        }
        
        public FloatXyPair CharacterSize(char _)
        {
            // monospaced fonts always return the same character size
            return new FloatXyPair(FontSize / 2, FontSize);
        }
    }
}
