using System;
using ExTween;
using ExTween.Art;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameDemo
{
    public class FontTest : Slide
    {
        private readonly float fontSize;
        private readonly string text;
        private readonly string allSymbols;
        private readonly string allNumbers;

        public FontTest(float fontSize, string text)
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
            
            this.allNumbers = string.Empty;

            for (int i = 0; i < 10; i++)
            {
                this.allNumbers += i.ToString();
            }
        }

        protected override void BuildTween(SequenceTween sequenceTween)
        {
            var capitalText = new TweenGlyphString(this.text, new MonospaceFont(this.fontSize), numberOfSegments: 0);
            var lowerText = new TweenGlyphString(this.text.ToLower(), new MonospaceFont(this.fontSize), numberOfSegments: 0);
            var symbolsText = new TweenGlyphString(this.allSymbols, new MonospaceFont(this.fontSize), numberOfSegments: 0);
            var numbersText = new TweenGlyphString(this.allNumbers, new MonospaceFont(this.fontSize), numberOfSegments: 0);
           
            Elements.Add(capitalText);
            Elements.Add(lowerText);
            Elements.Add(symbolsText);
            Elements.Add(numbersText);

            capitalText.Position.Value = new FloatXyPair(0, -100);
            lowerText.Position.Value = new FloatXyPair(0, 100);
            symbolsText.Position.Value = new FloatXyPair(0, -200);
            symbolsText.Position.Value = new FloatXyPair(0, 200);
            
            UpdateFunctions.Add(() =>
            {
            });

            sequenceTween.IsLooping = true;
            sequenceTween
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
