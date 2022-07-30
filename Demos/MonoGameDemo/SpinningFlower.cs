using System;
using ExTween;
using ExTween.Art;

namespace MonoGameDemo
{
    public class SpinningFlower : Slide
    {
        protected override void BuildSlideAnimation(SequenceTween animationTween)
        {
            var figure = new TweenFigure(new FloatXyPair(500));

            animationTween.IsLooping = true;

            float theta = 0;

            UpdateFunctions.Add(dt =>
            {
                theta += dt;
                figure.TweenPath.ClearKeyframes();
                
                
                var builder = figure.TweenPath.Builder
                    .KeyframeInitialize(0, 0);

                for (float i = 0; i < MathF.PI * 2f; i += MathF.PI / 2)
                {
                    builder
                        .KeyframeWarpTo(0, 0f)
                        .KeyframeArcA(MathF.Cos(theta + i), MathF.Sin(theta + i))
                        .KeyframeWarpTo(0, 0f)
                        .KeyframeArcB(MathF.Cos(theta + i), MathF.Sin(theta + i))
                        ;
                }
            });

            // why do I need an element when I already have a figure?
            // because "Slide" and "Transform" are client-land concepts

            var element = new FigureSlideElement(figure)
            {
                NumberOfSegments =
                {
                    Value = 250
                },

                Thickness =
                {
                    Value = 10
                }
            };

            Elements.Add(element);
        }
    }
}
