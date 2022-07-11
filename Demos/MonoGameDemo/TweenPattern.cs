using System;
using ExTween;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace MonoGameDemo
{
    public class TweenPattern : TweenableVisualElement
    {
        private readonly Color[] debugRainbow;
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

            this.debugRainbow = new[]
            {
                Color.Red,
                Color.Green,
                Color.Blue,
                Color.Yellow,
                Color.Orange,
                Color.Violet,
                Color.Pink
            };
        }

        public override Vector2 Size => DynamicMonospaceFont.Instance.CharacterSize(FontSize);

        public float Thickness { get; set; }
        public float FontSize { get; set; }
        public int NumberOfSegments { get; set; }
        public Vector2 RenderOffset { get; set; }

        public State GetValuesAtPercent(float percent)
        {
            var duration = this.tween.TotalDuration;
            this.tween.JumpTo(duration.Get() * percent);

            return new State(new Vector2(this.x.Value, this.y.Value), this.shouldDraw.Value == 1);
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (this.tween is TweenCollection {ChildrenWithDurationCount: 0})
            {
                // Empty tween, don't bother drawing anything
                return;
            }

            var prevPoint = new Vector2();
            var hasStarted = false;

            if (Demo.DebugMode)
            {
                spriteBatch.DrawCircle(new CircleF(Position.Value + RenderOffset, 10), 10, Color.Black, 3f);
            }

            for (var i = 0; i <= MinimumNumberOfSegments(); i++)
            {
                var color = Color.Black;

                if (Demo.DebugMode)
                {
                    color = this.debugRainbow[i % this.debugRainbow.Length];
                }

                var value = GetValuesAtPercent((float) i / MinimumNumberOfSegments());

                var currentPoint = value.Position;
                currentPoint *= FontSize / 2f;
                var radius = Thickness / 2;

                if (value.ShouldDraw)
                {
                    if (hasStarted)
                    {
                        spriteBatch.DrawLine(prevPoint + RenderOffset, currentPoint + RenderOffset, color, Thickness);
                    }

                    spriteBatch.DrawCircle(new CircleF(currentPoint + RenderOffset, radius), 10, color, radius);
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
            public Vector2 Position { get; }
            public bool ShouldDraw { get; }

            public State(Vector2 position, bool shouldDraw)
            {
                Position = position;
                ShouldDraw = shouldDraw;
            }
        }
    }
}
