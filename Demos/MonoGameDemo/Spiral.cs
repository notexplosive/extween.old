using System;
using ExTween;
using ExTween.Art;

namespace MonoGameDemo
{
    public class Spiral : Slide
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

                figure.TweenPath.Builder
                        .KeyframeInitialize(0, 0)
                        .KeyframeArcVertical(MathF.Cos(theta), MathF.Sin(theta))
                        .KeyframeWarpTo(0, 0f)
                    ;
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
