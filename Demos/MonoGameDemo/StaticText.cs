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
            var tweenText = new TweenText(this.text, new MonospaceFont(this.fontSize), numberOfSegments: 0);
            Elements.Add(tweenText);

            sequenceTween.IsLooping = true;

            sequenceTween
                .Add(new Tween<int>(tweenText.NumberOfSegments, 20, 4f, Ease.SineSlowFastSlow))
                .Add(new Tween<int>(tweenText.NumberOfSegments, 0, 4f, Ease.SineSlowFastSlow))
                ;
        }
    }
}
