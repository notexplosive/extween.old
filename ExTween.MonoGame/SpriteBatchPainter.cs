using ExTween.Art;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace ExTween.MonoGame
{
    public class SpriteBatchPainter : Painter
    {
        public SpriteBatch SpriteBatch { get; }

        public SpriteBatchPainter(SpriteBatch spriteBatch)
        {
            this.SpriteBatch = spriteBatch;
        }

        public override void DrawLine(FloatXyPair p1, FloatXyPair p2, float thickness, StrokeColor strokeColor)
        {
            SpriteBatch.DrawLine(p1.ToVector2(), p2.ToVector2(), strokeColor.ToMgColor(), thickness);
        }

        public override void DrawCircle(FloatXyPair center, float radius, float thickness, int numberOfSegments, StrokeColor strokeColor)
        {
            SpriteBatch.DrawCircle(new CircleF(center.ToVector2(), radius), numberOfSegments, strokeColor.ToMgColor(), thickness);
        }
    }
}
