using ExTween;
using ExTween.MonoGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameDemo
{
    public class SpriteFontGlyph
    {
        public TweenableVector2 Position { get; set; } = new TweenableVector2();
        public string Text { get; set; }
        public TweenableFloat Angle { get; } = new TweenableFloat();
        public TweenableFloat Scale { get; set; } = new TweenableFloat();
        public SpriteFont Font { get; set; }

        public void Draw(SpriteBatch spriteBatch)
        {
            if (Font == null)
            {
                Font = Demo.TitleFont;
            }
            
            var offset = Font.MeasureString(Text) / 2;
            spriteBatch.DrawString(Font, Text, Position + offset, Color.Black, Angle, offset,
                Scale, SpriteEffects.None, 0f);
        }
    }
}
