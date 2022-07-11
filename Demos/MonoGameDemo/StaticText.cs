using ExTween;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameDemo
{
    public class StaticText : Slide
    {
        private readonly float fontSize;
        private readonly string text;

        public StaticText(float fontSize, string text)
        {
            this.fontSize = fontSize;
            this.text = text;
        }

        protected override void BuildTween(SequenceTween sequenceTween)
        {
            Elements.Add(new DynamicMonospaceFontRenderedText(this.fontSize, this.text));
        }
    }
}
