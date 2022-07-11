namespace ExTween.Art
{
    public class TweenText : TweenableVisualElement
    {
        private readonly int numberOfSegments;
        private readonly float paddingBetweenLetters;
        private readonly TweenGlyph[] patterns;
        private readonly string text;
        private readonly IFont font;
        private readonly float thickness;

        public TweenText(string text, IFont font, float thickness = 6,
            int numberOfSegments = 0, float paddingBetweenLetters = 20)
        {
            this.text = text;
            this.font = font;
            this.thickness = thickness;
            this.numberOfSegments = numberOfSegments;
            this.paddingBetweenLetters = paddingBetweenLetters;

            this.patterns = new TweenGlyph[this.text.Length];

            for (var i = 0; i < this.text.Length; i++)
            {
                this.patterns[i] = this.font.GetTweenGlyphForLetter(this.text[i]);
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

        public override void Draw(Painter painter)
        {
            var xPosition = 0f;

            for (var i = 0; i < this.patterns.Length; i++)
            {
                var pattern = this.patterns[i];
                xPosition += this.font.CharacterSize(this.text[i]).X;
                xPosition += this.paddingBetweenLetters;

                pattern.RenderOffset = new FloatXyPair(-Size.X / 2 + xPosition, 0);
                pattern.NumberOfSegments = this.numberOfSegments;
                pattern.Thickness = this.thickness;

                pattern.Draw(painter);
            }
        }
    }
}
