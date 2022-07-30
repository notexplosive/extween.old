using ExTween;
using ExTween.Art;

namespace MonoGameDemo
{
    public class StaticText : Slide
    {
        private readonly string text;
        private readonly IFont font;

        public StaticText(IFont font, string text)
        {
            this.font = font;
            this.text = text;
        }

        protected override void BuildSlideAnimation(SequenceTween animationTween)
        {
            var textElement = new TweenGlyphString(this.text, this.font, numberOfSegments: 50, thickness: 3f, paddingBetweenLetters: 5);
            Elements.Add(textElement);
        }
    }
}
