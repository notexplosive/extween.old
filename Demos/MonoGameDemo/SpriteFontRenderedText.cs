using ExTween.Art;
using ExTween.Art.MonoGame;
using ExTween.MonoGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameDemo
{
    public class SpriteFontRenderedText : TweenableDrawable
    {
        public string Text { get; set; }
        public SpriteFont Font { get; set; }
        public override FloatXyPair Size => Font.MeasureString(Text).ToXyPair();

        public override void Draw(Painter painter)
        {
            if (painter is SpriteBatchPainter sbp)
            {
                var spriteBatch = sbp.SpriteBatch;
                if (Font == null)
                {
                    Font = Demo.TitleFont;
                }

                var halfOfSize = Size.ToVector2() / 2;
                spriteBatch.DrawString(Font, Text, Position.Value.ToVector2() + halfOfSize, Color.Black, Angle, halfOfSize,
                    Scale, SpriteEffects.None, 0f);
            }
        }
    }
}
