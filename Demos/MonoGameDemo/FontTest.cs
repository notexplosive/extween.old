using System;
using ExTween;
using ExTween.Art;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameDemo
{
    public class FontTest : Slide
    {
        private readonly string text;
        private readonly string allSymbols;
        private readonly string allNumbers;
        private readonly IFont font;

        public FontTest(IFont font)
        {
            this.font = font;
            this.text = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            
            this.allSymbols = string.Empty;

            for (char i = char.MinValue; i < 128; i++)
            {
                if (char.IsPunctuation(i) || char.IsSymbol(i))
                {
                    this.allSymbols += i;
                } 
            }
            
            this.allNumbers = string.Empty;

            for (int i = 0; i < 10; i++)
            {
                this.allNumbers += i.ToString();
            }
        }

        protected override void BuildSlideAnimation(SequenceTween animationTween)
        {
            var capitalText = new TweenGlyphString(this.text, this.font, numberOfSegments: 0);
            var lowerText = new TweenGlyphString(this.text.ToLower(), this.font, numberOfSegments: 0);
            var symbolsText = new TweenGlyphString(this.allSymbols, this.font, numberOfSegments: 0);
            var numbersText = new TweenGlyphString(this.allNumbers, this.font, numberOfSegments: 0);
           
            Elements.Add(capitalText);
            Elements.Add(lowerText);
            Elements.Add(symbolsText);
            Elements.Add(numbersText);

            capitalText.Position.Value = new FloatXyPair(0, -100);
            lowerText.Position.Value = new FloatXyPair(0, 100);
            symbolsText.Position.Value = new FloatXyPair(0, -200);
            symbolsText.Position.Value = new FloatXyPair(0, 200);
            
            UpdateFunctions.Add((dt) =>
            {
            });

            animationTween.IsLooping = true;
            animationTween
                .Add(
                    new MultiplexTween()
                        .AddChannel(new Tween<int>(lowerText.NumberOfSegmentsPerCharacter, 40, 2f, Ease.Linear))
                        .AddChannel(new Tween<int>(capitalText.NumberOfSegmentsPerCharacter, 40, 2f, Ease.Linear))
                        .AddChannel(new Tween<int>(numbersText.NumberOfSegmentsPerCharacter, 40, 2f, Ease.Linear))
                        .AddChannel(new Tween<int>(symbolsText.NumberOfSegmentsPerCharacter, 40, 2f, Ease.Linear))
                )
                .Add(new WaitSecondsTween(2))
                .Add(
                    new MultiplexTween()
                        .AddChannel(new Tween<int>(lowerText.NumberOfSegmentsPerCharacter, 0, 2f, Ease.Linear))
                        .AddChannel(new Tween<int>(capitalText.NumberOfSegmentsPerCharacter, 0, 2f, Ease.Linear))
                        .AddChannel(new Tween<int>(numbersText.NumberOfSegmentsPerCharacter, 0, 2f, Ease.Linear))
                        .AddChannel(new Tween<int>(symbolsText.NumberOfSegmentsPerCharacter, 0, 2f, Ease.Linear))
                )
                .Add(new WaitSecondsTween(2))
                ;
        }
    }
}
