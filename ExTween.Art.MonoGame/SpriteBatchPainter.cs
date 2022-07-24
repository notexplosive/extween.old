using ExTween.Art;
using Microsoft.Xna.Framework.Graphics;
using MonoGame.Extended;

namespace ExTween.Art.MonoGame
{
    public class SpriteBatchPainter : Painter
    {
        public SpriteBatchPainter(SpriteBatch spriteBatch)
        {
            SpriteBatch = spriteBatch;
        }

        public SpriteBatch SpriteBatch { get; }

        public override void DrawLine(FloatXyPair p1, FloatXyPair p2, float thickness, StrokeColor strokeColor)
        {
            SpriteBatch.DrawLine(p1.ToVector2(), p2.ToVector2(), strokeColor.ToMgColor(), thickness);
            SpriteBatch.DrawCircle(new CircleF(p1.ToVector2(), thickness / 2), 10, strokeColor.ToMgColor(),
                thickness / 2);
            SpriteBatch.DrawCircle(new CircleF(p2.ToVector2(), thickness / 2), 10, strokeColor.ToMgColor(),
                thickness / 2);
        }

        public override void DrawFilledCircle(FloatXyPair position, float radius, int segments, StrokeColor strokeColor)
        {
            SpriteBatch.DrawCircle(new CircleF(position.ToVector2(), radius), segments, strokeColor.ToMgColor(), radius);
        }
    }
}
