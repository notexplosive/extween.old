using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameDemo
{
    public class SpriteFontRenderedText : TweenableVisualElement
    {
        public string Text { get; set; }
        public SpriteFont Font { get; set; }
        public override Vector2 Size => Font.MeasureString(Text);

        public override void Draw(SpriteBatch spriteBatch)
        {
            if (Font == null)
            {
                Font = Demo.TitleFont;
            }
            
            var halfOfSize = Size / 2;
            spriteBatch.DrawString(Font, Text, Position + halfOfSize, Color.Black, Angle, halfOfSize,
                Scale, SpriteEffects.None, 0f);
        }
    }
}
