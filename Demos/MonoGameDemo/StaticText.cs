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
        private readonly TweenableFloat brushPositionTime = new TweenableFloat();
        private readonly string allSymbols;

        public StaticText(float fontSize, string text)
        {
            this.fontSize = fontSize;
            this.text = text;
            
            this.allSymbols = string.Empty;

            for (char i = char.MinValue; i < 128; i++)
            {
                if (char.IsPunctuation(i) || char.IsSymbol(i))
                {
                    this.allSymbols += i;
                } 
            }
        }

        protected override void BuildTween(SequenceTween sequenceTween)
        {
            var capitalText = new TweenGlyphString(this.text, new MonospaceFont(this.fontSize), numberOfSegments: 0);
            var lowerText = new TweenGlyphString(this.text.ToLower(), new MonospaceFont(this.fontSize), numberOfSegments: 0);
            var symbolsText = new TweenGlyphString(this.allSymbols, new MonospaceFont(this.fontSize), numberOfSegments: 0);
            var circlePrimitive = new CirclePrimitive(5)
            {
                Color = StrokeColor.Red
            };
            Elements.Add(capitalText);
            Elements.Add(lowerText);
            Elements.Add(symbolsText);
            Elements.Add(circlePrimitive);

            capitalText.Position.Value = new FloatXyPair(0, -100);
            lowerText.Position.Value = new FloatXyPair(0, 100);
            
            UpdateFunctions.Add(() =>
            {
                var state = lowerText.GetPreciseStateAtTime(this.brushPositionTime);
                circlePrimitive.Position.Value = state.Position;
                circlePrimitive.Radius.Value = state.ShouldDraw ? 5f : 2f;
            });

            sequenceTween.IsLooping = true;
            sequenceTween
                .Add(new Tween<int>(lowerText.NumberOfSegmentsPerCharacter, 40, 2f, Ease.Linear))
                .Add(new Tween<int>(capitalText.NumberOfSegmentsPerCharacter, 40, 2f, Ease.Linear))
                .Add(new WaitSecondsTween(2))
                .Add(new Tween<int>(lowerText.NumberOfSegmentsPerCharacter, 0, 2f, Ease.Linear))
                .Add(new Tween<int>(capitalText.NumberOfSegmentsPerCharacter, 0, 2f, Ease.Linear))
                .Add(new WaitSecondsTween(2))
                ;
        }
    }
}
