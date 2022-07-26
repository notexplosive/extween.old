namespace ExTween.Art
{
    public class KernedFont : IFont
    {
        public KernedFont(float fontSize, CharacterWidthTable characterWidthTable)
        {
            FontSize = fontSize;
            CharacterWidthTable = characterWidthTable;
        }
        
        public float FontSize { get; }
        private CharacterWidthTable CharacterWidthTable { get; }
        
        public FloatXyPair CharacterSize(char c)
        {
            return new FloatXyPair(CharacterWidthTable.GetNormalizedWidth(c) * FontSize, FontSize);
        }
    }
}
