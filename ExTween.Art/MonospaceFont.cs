namespace ExTween.Art
{
    public class MonospaceFont : IFont
    {
        public MonospaceFont(float fontSize, float horizontalFactor = 1/2f)
        {
            FontSize = fontSize;
            HorizontalFactor = horizontalFactor;
        }

        public float HorizontalFactor { get; }

        public float FontSize { get; }

        public FloatXyPair CharacterSize(char _)
        {
            // monospaced fonts always return the same character size
            return new FloatXyPair(FontSize * HorizontalFactor, FontSize);
        }
    }
}
