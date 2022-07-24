
namespace ExTween.Art
{
    public class TweenText : TweenableDrawable, ITweenRendered
    {
        public TweenableInt NumberOfSegmentsPerCharacter { get; } = new TweenableInt();
        private readonly float paddingBetweenLetters;
        private readonly TweenGlyph[] glyphs;
        private readonly string text;
        private readonly IFont font;
        private readonly float thickness;

        public TweenText(string text, IFont font, float thickness = 6,
            int numberOfSegments = 0, float paddingBetweenLetters = 20)
        {
            this.text = text;
            this.font = font;
            this.thickness = thickness;
            NumberOfSegmentsPerCharacter.Value = numberOfSegments;
            this.paddingBetweenLetters = paddingBetweenLetters;

            this.glyphs = new TweenGlyph[this.text.Length];

            for (var i = 0; i < this.text.Length; i++)
            {
                this.glyphs[i] = this.font.GetTweenGlyphForLetter(this.text[i]);
            }
        }

        public override FloatXyPair Size {
            get
            {
                var width = 0f;
                foreach (var character in this.text)
                {
                    width += this.font.CharacterSize(character).X;
                }

                return new FloatXyPair(width + this.paddingBetweenLetters * (this.text.Length - 1), this.font.CharacterSize('x').Y);
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

        public TweenPath.State StateAtTime(float time)
        {
            foreach (var glyph in this.glyphs)
            {
                if (time > glyph.Duration)
                {
                    time -= glyph.Duration;
                }
                else
                {
                    return glyph.GetStateAtTime(time);
                }
            }

            return new TweenPath.State();
        }

        public override void Draw(Painter painter)
        {
            var xPosition = 0f;

            for (var i = 0; i < this.glyphs.Length; i++)
            {
                var pattern = this.glyphs[i];
                xPosition += this.font.CharacterSize(this.text[i]).X;
                xPosition += this.paddingBetweenLetters;

                pattern.RenderOffset = new FloatXyPair(-Size.X / 2 + xPosition, 0);
                pattern.NumberOfSegments = NumberOfSegmentsPerCharacter;
                pattern.Thickness = this.thickness;

                pattern.Draw(painter);
            }
        }
    }
}
