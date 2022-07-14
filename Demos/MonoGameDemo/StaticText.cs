using System;
using ExTween;
using ExTween.Art;
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
            var text = new TweenText(this.text, new MonospaceFont(this.fontSize), numberOfSegments: 0);
            Elements.Add(text);

            var resetCount = 0;
            
            sequenceTween
                .Add(new Tween<int>(text.NumberOfSegments, 50, 3f, Ease.Linear))
                .Add(new Tween<int>(text.NumberOfSegments, 0, 3f, Ease.Linear))
                ;
        }
    }
}
