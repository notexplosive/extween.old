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
            var previousShouldDraw = true;

            var keyframes = this.kit.GetKeyframes(NumberOfSegments);

            for (var i = 0; i < keyframes.Length; i++)
            {
                var color = StrokeColor.Black;

                var currentKeyframeTime = keyframes[i];
                var state = this.kit.GetStateAtTime(currentKeyframeTime);

                var currentPoint = state.Position;
                currentPoint *= this.font.FontSize / 2f;

                if (previousShouldDraw && hasStarted)
                {
                    painter.DrawLine(prevPoint + RenderOffset, currentPoint + RenderOffset, Thickness, color);
                }

                previousShouldDraw = state.ShouldDraw;
                prevPoint = currentPoint;
                hasStarted = true;
            }
        }
    }
}
