namespace ExTween.Art
{
    public interface IFont
    {
        float FontSize { get; }
        FloatXyPair CharacterSize(char c);
        TweenGlyph GetTweenGlyphForLetter(char letter);
    }
}
