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

        public StaticText(float fontSize, string text)
        {
            this.fontSize = fontSize;
            this.text = text;
        }

        protected override void BuildTween(SequenceTween sequenceTween)
        {
            var tweenText = new TweenText(this.text, new MonospaceFont(this.fontSize), numberOfSegments: 0);
            var circlePrimitive = new CirclePrimitive(5)
            {
                Color = StrokeColor.Red
            };
            Elements.Add(tweenText);
            Elements.Add(circlePrimitive);
            
            UpdateFunctions.Add(() =>
            {
                var state = tweenText.StateAtTime(this.brushPositionTime);
                circlePrimitive.Position.Value = state.Position;
                circlePrimitive.Radius.Value = state.ShouldDraw ? 5f : 2f;
            });

            sequenceTween.IsLooping = true;

            sequenceTween
                .Add(new CallbackTween(() => { this.brushPositionTime.Value = 0f; }))
                .Add(new Tween<int>(tweenText.NumberOfSegmentsPerCharacter, 20, 4f, Ease.SineSlowFastSlow))
                .Add(new Tween<float>(this.brushPositionTime, tweenText.Duration, 15f, Ease.Linear))
                .Add(new Tween<int>(tweenText.NumberOfSegmentsPerCharacter, 0, 4f, Ease.SineSlowFastSlow))
                .Add(new CallbackTween(() => { this.brushPositionTime.Value = 0f; }))
                .Add(new Tween<float>(this.brushPositionTime, tweenText.Duration, 15f, Ease.Linear))
                ;
        }
    }
}
