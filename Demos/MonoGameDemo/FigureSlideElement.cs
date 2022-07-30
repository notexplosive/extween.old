using ExTween;
using ExTween.Art;

namespace MonoGameDemo
{
    public class FigureSlideElement : SimpleSlideElement
    {
        private readonly TweenFigure figure;

        public FigureSlideElement(TweenFigure figure)
        {
            this.figure = figure;
        }

        public TweenableInt NumberOfSegments { get; } = new TweenableInt();
        public TweenableFloat Thickness { get; } = new TweenableFloat(1);

        public override FloatXyPair Size => this.figure.Size;

        public override void Draw(Painter painter)
        {
            this.figure.Draw(painter, Position, NumberOfSegments, Thickness);
        }
    }
}
