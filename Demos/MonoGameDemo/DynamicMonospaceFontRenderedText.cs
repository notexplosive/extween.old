using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameDemo
{
    public class DynamicMonospaceFontRenderedText : TweenableVisualElement
    {
        private readonly float fontSize;
        private readonly int numberOfSegments;
        private readonly float paddingBetweenLetters;
        private readonly TweenPattern[] patterns;
        private readonly string text;
        private readonly float thickness;

        public DynamicMonospaceFontRenderedText(float fontSize, string text, float thickness = 6,
            int numberOfSegments = 0, float paddingBetweenLetters = 20)
        {
            this.fontSize = fontSize;
            this.text = text;
            this.thickness = thickness;
            this.numberOfSegments = numberOfSegments;
            this.paddingBetweenLetters = paddingBetweenLetters;

            this.patterns = new TweenPattern[this.text.Length];

            for (var i = 0; i < this.text.Length; i++)
            {
                this.patterns[i] = DynamicMonospaceFont.Instance.GetPatternForLetter(this.text[i]);
            }
        }

        public override Vector2 Size =>
            new Vector2(CharSize.X * this.text.Length + this.paddingBetweenLetters * (this.text.Length - 1),
                CharSize.Y);

        public Vector2 CharSize => DynamicMonospaceFont.Instance.CharacterSize(this.fontSize);

        public override void Draw(SpriteBatch spriteBatch)
        {
            for (var i = 0; i < this.patterns.Length; i++)
            {
                var pattern = this.patterns[i];

                pattern.RenderOffset = new Vector2(-Size.X / 2 + (CharSize.X + this.paddingBetweenLetters) * i, 0);
                pattern.NumberOfSegments = this.numberOfSegments;
                pattern.FontSize = this.fontSize;
                pattern.Thickness = this.thickness;

                pattern.Draw(spriteBatch);
            }
        }
    }
}
