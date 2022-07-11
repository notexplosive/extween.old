using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameDemo
{
    public class DynamicMonospaceFontRenderedText : TweenableVisualElement
    {
        private readonly float fontSize;
        private readonly int numberOfSegments;
        private readonly TweenPattern[] patterns;
        private readonly string text;
        private readonly float thickness;

        public DynamicMonospaceFontRenderedText(float fontSize, string text, float thickness = 6,
            int numberOfSegments = 0)
        {
            this.fontSize = fontSize;
            this.text = text;
            this.thickness = thickness;
            this.numberOfSegments = numberOfSegments;

            this.patterns = new TweenPattern[this.text.Length];

            for (var i = 0; i < this.text.Length; i++)
            {
                this.patterns[i] = DynamicMonospaceFont.Instance.GetPatternForLetter(this.text[i]);
            }
        }

        public override Vector2 Size => new Vector2(this.fontSize * this.text.Length, this.fontSize);

        public override void Draw(SpriteBatch spriteBatch)
        {
            for(var i = 0; i < this.patterns.Length; i++)
            {
                var pattern = this.patterns[i];

                pattern.RenderOffset = new Vector2(-Size.X / 2 + this.fontSize * i, 0);
                pattern.NumberOfSegments = this.numberOfSegments;
                pattern.FontSize = this.fontSize;
                pattern.Thickness = this.thickness;

                pattern.Draw(spriteBatch);
            }
        }
    }
}
