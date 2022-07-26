using System.Collections.Generic;

namespace ExTween.Art
{
    public class CharacterWidthTable
    {
        private readonly Dictionary<char, float> lookup = new Dictionary<char, float>();

        public CharacterWidthTable(float defaultNormalizedWidth)
        {
            DefaultNormalizedWidth = defaultNormalizedWidth;
        }

        private float DefaultNormalizedWidth { get; }

        public static CharacterWidthTable Standard =>
            new CharacterWidthTable(0.50f)
                .AddEntry('m', 0.60f)
                .AddEntry('M', 0.60f)
                .AddEntry('l', 0.20f)
                .AddEntry('.', 0.20f)
                .AddEntry('!', 0.10f)
                .AddEntry('\'', 0.10f)
                .AddEntry('i', 0.20f)
                .AddEntry('t', 2f)
        ;

        public CharacterWidthTable AddEntry(char c, float normalizedWidth)
        {
            if (this.lookup.ContainsKey(c))
            {
                this.lookup.Remove(c);
            }
            
            this.lookup.Add(c, normalizedWidth);
            return this;
        }

        public float GetNormalizedWidth(char c)
        {
            if (this.lookup.ContainsKey(c))
            {
                return this.lookup[c];
            }

            return DefaultNormalizedWidth;
        }
    }
}
