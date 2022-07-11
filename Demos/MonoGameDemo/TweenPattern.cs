using System;
using ExTween;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace MonoGameDemo
{
    public class TweenPattern : TweenableVisualElement
    {
        private readonly ITween tween;
        private readonly TweenableFloat x;
        private readonly TweenableFloat y;
        private readonly Color[] debugRainbow;

        public TweenPattern(ITween tween, TweenableFloat x, TweenableFloat y)
        {
            this.tween = tween;
            this.x = x;
            this.y = y;

            this.debugRainbow = new[]
            {
                Color.Red,
                Color.Green,
                Color.Blue,
                Color.Yellow,
                Color.Orange,
                Color.Violet,
                Color.Pink,
            };
        }

        public Vector2 GetValuesAtPercent(float percent)
        {
            var duration = this.tween.TotalDuration;
            this.tween.JumpTo(duration.Get() * percent);

            return new Vector2(this.x.Value, this.y.Value);
        }

        public override Vector2 Size => new Vector2(FontSize);
        public override void Draw(SpriteBatch spriteBatch)
        {
            if (this.tween is TweenCollection {ChildrenWithDurationCount: 0})
            {
                // Empty tween, don't bother drawing anything
                return;
            }
            
            var prevPoint = new Vector2();
            var hasStarted = false;
            
            for (int i = 0; i <= MinimumNumberOfSegments(); i++)
            {
                var currentPoint = GetValuesAtPercent((float) i / MinimumNumberOfSegments());
                currentPoint *= FontSize / 2f;

                if (hasStarted)
                {
                    spriteBatch.DrawLine(prevPoint + RenderOffset, currentPoint + RenderOffset, this.debugRainbow[i % this.debugRainbow.Length], Thickness);
                }

                var radius = Thickness / 2;
                spriteBatch.DrawCircle(new CircleF(currentPoint + RenderOffset, radius), 10, this.debugRainbow[i % this.debugRainbow.Length], radius);
                
                prevPoint = currentPoint;
                hasStarted = true;
            }
        }

        private int MinimumNumberOfSegments()
        {
            if (tween is TweenCollection collection)
            {
                return Math.Max(NumberOfSegments, collection.ChildrenWithDurationCount);
            }

            return NumberOfSegments;
        }

        public float Thickness { get; set; }
        public float FontSize { get; set; }
        public int NumberOfSegments { get; set; }
        public Vector2 RenderOffset { get; set; }
    }
}
