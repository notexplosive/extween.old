
namespace ExTween.Art
{
    public class TweenGlyphString : Transformable, ITweenPath, ISlideElement
    {
        public TweenableInt NumberOfSegmentsPerCharacter { get; } = new TweenableInt();
        public TweenableFloat PaddingBetweenLetters { get; } = new TweenableFloat();
        public TweenableFloat Thickness { get; } = new TweenableFloat();
        private readonly TweenGlyph[] glyphs;
        private readonly string text;
        private readonly IFont font;

        public TweenGlyphString(string text, IFont font, float thickness = 6,
            int numberOfSegments = 0, float paddingBetweenLetters = 0)
        {
            this.text = text;
            this.font = font;
            Thickness.Value = thickness;
            NumberOfSegmentsPerCharacter.Value = numberOfSegments;
            PaddingBetweenLetters.Value = paddingBetweenLetters;

            this.glyphs = new TweenGlyph[this.text.Length];

            for (var i = 0; i < this.text.Length; i++)
            {
                this.glyphs[i] = this.font.GetTweenGlyphForLetter(this.text[i]);
            }
        }

        public FloatXyPair Size {
            get
            {
                var totalWidth = 0f;
                foreach (var character in this.text)
                {
                    totalWidth += this.font.CharacterSize(character).X;
                }

                var characterHeight = this.font.CharacterSize('x').Y;
                return new FloatXyPair(totalWidth + PaddingBetweenLetters * (this.text.Length - 1), characterHeight);
            }
        }

        public float Duration
        {
            get
            {
                var total = 0f;
                foreach (var pattern in this.glyphs)
                {
                    total += pattern.Duration;
                }
                return total;
            }
        }

        public PenState StateAtTime(float time)
        {
            foreach (var glyph in this.glyphs)
            {
                if (time > glyph.Duration)
                {
                    time -= glyph.Duration;
                }
                else
                {
                    return glyph.StateAtTime(time);
                }
            }

            return new PenState();
        }
        
        public void Draw(Painter painter)
        {
            var xPosition = 0f;

            for (var i = 0; i < this.glyphs.Length; i++)
            {
                var glyph = this.glyphs[i];
                xPosition += this.font.CharacterSize(this.text[i]).X / 2;
                glyph.Draw(painter, new FloatXyPair(-Size.X / 2 + xPosition, 0) + Position, NumberOfSegmentsPerCharacter, Thickness);
                xPosition += this.font.CharacterSize(this.text[i]).X / 2;
                xPosition += PaddingBetweenLetters;
            }
        }
    }
}
