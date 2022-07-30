using ExTween;
using ExTween.Art;

namespace MonoGameDemo
{
    public class StaticPicture : Slide
    {
        protected override void BuildSlideAnimation(SequenceTween animationTween)
        {
            var figure = new TweenFigure(new FloatXyPair(500));

            animationTween.IsLooping = true;

            var oscillator = new TweenableFloat();

            animationTween
                .Add(new Tween<float>(oscillator, -1f, 1f, Ease.Linear))
                .Add(new Tween<float>(oscillator, 1f, 1f, Ease.Linear))
                ;

            UpdateFunctions.Add(() =>
            {
                figure.TweenPath.ClearKeyframes();
                figure.TweenPath.Builder
                    .KeyframeInitialize(0, 0)
                    .KeyframeWarpTo(0, 0f)
                    .KeyframeArcA(1f, oscillator)
                    ;
            });

            // why do I need an element when I already have a figure?
            // because "Slide" and "Transform" are client-land concepts

            var element = new FigureSlideElement(figure)
            {
                NumberOfSegments =
                {
                    Value = 500
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
