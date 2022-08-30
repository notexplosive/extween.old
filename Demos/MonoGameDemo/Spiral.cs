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

                var x1 = MathF.Cos(theta) * 0.5f;
                var y1 = MathF.Sin(theta) * 0.5f;
                var x2 = MathF.Cos(theta + MathF.PI / 4);
                var y2 = MathF.Sin(theta + MathF.PI / 4);
                
                figure.TweenPath.Builder
                        .KeyframeInitialize(0, 0)
                        .KeyframeFullArcVerticalHorizontal(0.5f, 0.5f, x2, y2)
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
