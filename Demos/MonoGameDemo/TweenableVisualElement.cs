using ExTween;
using ExTween.MonoGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameDemo
{
    public abstract class TweenableVisualElement
    {
        public TweenableVector2 Position { get; set; } = new TweenableVector2();
        public TweenableFloat Angle { get; set; } = new TweenableFloat();
        public TweenableFloat Scale { get; set; } = new TweenableFloat();
        public abstract Vector2 Size { get; }

        public abstract void Draw(SpriteBatch spriteBatch);
    }
}
