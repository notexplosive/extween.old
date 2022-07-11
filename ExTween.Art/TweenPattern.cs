﻿using System;

namespace ExTween.Art
{
    public class TweenPattern : TweenableVisualElement
    {
        private readonly TweenableInt shouldDraw;
        private readonly ITween tween;
        private readonly TweenableFloat x;
        private readonly TweenableFloat y;

        public TweenPattern(ITween tween, TweenableFloat x, TweenableFloat y, TweenableInt shouldDraw)
        {
            this.tween = tween;
            this.x = x;
            this.y = y;
            this.shouldDraw = shouldDraw;
        }

        public override FloatXyPair Size => DynamicMonospaceFont.Instance.CharacterSize(FontSize);

        public float Thickness { get; set; }
        public float FontSize { get; set; }
        public int NumberOfSegments { get; set; }
        public FloatXyPair RenderOffset { get; set; }

        public State GetValuesAtPercent(float percent)
        {
            var duration = this.tween.TotalDuration;
            this.tween.JumpTo(duration.Get() * percent);

            return new State(new FloatXyPair(this.x.Value, this.y.Value), this.shouldDraw.Value == 1);
        }

        public override void Draw(Painter painter)
        {
            if (this.tween is TweenCollection {ChildrenWithDurationCount: 0})
            {
                // Empty tween, don't bother drawing anything
                return;
            }

            var prevPoint = new FloatXyPair();
            var hasStarted = false;

            for (var i = 0; i <= MinimumNumberOfSegments(); i++)
            {
                var color = ExColor.Black;

                var value = GetValuesAtPercent((float) i / MinimumNumberOfSegments());

                var currentPoint = value.Position;
                currentPoint *= FontSize / 2f;
                var radius = Thickness / 2;

                if (value.ShouldDraw)
                {
                    if (hasStarted)
                    {
                        painter.DrawLine(prevPoint + RenderOffset, currentPoint + RenderOffset, Thickness, color);
                    }

                    painter.DrawCircle(currentPoint + RenderOffset, radius, radius, 10, color);
                }

                prevPoint = currentPoint;
                hasStarted = true;
            }
        }

        private int MinimumNumberOfSegments()
        {
            if (this.tween is TweenCollection collection)
            {
                return Math.Max(NumberOfSegments, collection.ChildrenWithDurationCount);
            }

            return NumberOfSegments;
        }

        public struct State
        {
            public FloatXyPair Position { get; }
            public bool ShouldDraw { get; }

            public State(FloatXyPair position, bool shouldDraw)
            {
                Position = position;
                ShouldDraw = shouldDraw;
            }
        }
    }
}
