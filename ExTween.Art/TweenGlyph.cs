using System;

namespace ExTween.Art
{
    public class TweenGlyph : TweenableVisualElement
    {
        private readonly IFont font;
        private readonly char letter;
        private readonly DrawKit kit;

        public TweenGlyph(DrawKit kit, IFont font, char letter)
        {
            this.font = font;
            this.letter = letter;
            this.kit = kit;
        }

        public override FloatXyPair Size => font.CharacterSize(this.letter);

        public float Thickness { get; set; }
        public int NumberOfSegments { get; set; }
        public FloatXyPair RenderOffset { get; set; }

        public override void Draw(Painter painter)
        {
            if (this.kit.Tween is TweenCollection {ChildrenWithDurationCount: 0})
            {
                // Empty tween, don't bother drawing anything
                return;
            }

            var prevPoint = new FloatXyPair();
            var hasStarted = false;

            for (var i = 0; i <= MinimumNumberOfSegments(); i++)
            {
                var color = StrokeColor.Black;

                var value = this.kit.GetStateAtPercent((float) i / MinimumNumberOfSegments());

                var currentPoint = value.Position;
                currentPoint *= this.font.FontSize / 2f;

                if (value.ShouldDraw)
                {
                    if (hasStarted)
                    {
                        painter.DrawLine(prevPoint + RenderOffset, currentPoint + RenderOffset, Thickness, color);
                    }
                }

                prevPoint = currentPoint;
                hasStarted = true;
            }
        }

        private int MinimumNumberOfSegments()
        {
            if (this.kit.Tween is TweenCollection collection)
            {
                return Math.Max(NumberOfSegments, collection.ChildrenWithDurationCount);
            }

            return NumberOfSegments;
        }
    }
}
