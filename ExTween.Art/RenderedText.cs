namespace ExTween.Art
{
    public class RenderedText : TweenableVisualElement
    {
        private readonly float fontSize;
        private readonly int numberOfSegments;
        private readonly float paddingBetweenLetters;
        private readonly TweenGlyph[] patterns;
        private readonly string text;
        private readonly IFont font;
        private readonly float thickness;

        public RenderedText(float fontSize, string text, IFont font, float thickness = 6,
            int numberOfSegments = 0, float paddingBetweenLetters = 20)
        {
            this.fontSize = fontSize;
            this.text = text;
            this.font = font;
            this.thickness = thickness;
            this.numberOfSegments = numberOfSegments;
            this.paddingBetweenLetters = paddingBetweenLetters;

            this.patterns = new TweenGlyph[this.text.Length];

            for (var i = 0; i < this.text.Length; i++)
            {
                this.patterns[i] = this.font.GetPatternForLetter(this.text[i]);
            }
        }

        public override FloatXyPair Size {
            get
            {
                var width = 0f;
                foreach (var character in this.text)
                {
                    width += this.font.CharacterSize(character, this.fontSize).X;
                }

                return new FloatXyPair(width + this.paddingBetweenLetters * (this.text.Length - 1), this.font.CharacterSize('x',this.fontSize).Y);
            }
        }

        public override void Draw(Painter painter)
        {
            var xPosition = 0f;

            for (var i = 0; i < this.patterns.Length; i++)
            {
                var pattern = this.patterns[i];
                xPosition += this.font.CharacterSize(this.text[i], this.fontSize).X;
                xPosition += this.paddingBetweenLetters;

                pattern.RenderOffset = new FloatXyPair(-Size.X / 2 + xPosition, 0);
                pattern.NumberOfSegments = this.numberOfSegments;
                pattern.FontSize = this.fontSize;
                pattern.Thickness = this.thickness;

                pattern.Draw(painter);
            }
        }
    }
}
