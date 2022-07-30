using ExTween;
using ExTween.Art;
using ExTween.Art.MonoGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameDemo
{
    public class FlyInTitle : Slide
    {
        private readonly SpriteFont font;
        private readonly string message;

        public FlyInTitle(SpriteFont font, string message)
        {
            this.font = font;
            this.message = message;
        }

        protected override void BuildSlideAnimation(SequenceTween animationTween)
        {
            var totalStringSize = this.font.MeasureString(this.message);

            {
                foreach (var letter in this.message)
                {
                    var text = new SpriteFontRenderedText
                    {
                        Text = letter.ToString(),
                        Font = this.font
                    };
                    text.Scale.Value = new TweenableFloat(1);
                    text.Position.Value = new TweenableFloatXyPair(new Vector2(0, 500).ToXyPair());
                    Elements.Add(text);
                }
            }

            var multiplex = new MultiplexTween();
            animationTween.Add(multiplex);

            var targetX = -totalStringSize.X / 2;
            var targetY = -totalStringSize.Y / 2;
            for (var glyphIndex = 0; glyphIndex < Elements.Count; glyphIndex++)
            {
                var duration = 0.5f;

                multiplex
                    .AddChannel(new SequenceTween()
                        .Add(new WaitSecondsTween(glyphIndex / 15f))
                        .Add(new MultiplexTween()
                            .AddChannel(
                                new SequenceTween()
                                    .Add(new Tween<FloatXyPair>(Elements[glyphIndex].Position,
                                        new FloatXyPair(targetX + 100, targetY - 200), duration / 2, Ease.QuadFastSlow))
                                    .Add(new Tween<FloatXyPair>(Elements[glyphIndex].Position,
                                        new FloatXyPair(targetX, targetY), duration / 2, Ease.QuadSlowFast))
                            )
                            .AddChannel(
                                new SequenceTween()
                                    .Add(new Tween<float>(Elements[glyphIndex].Scale, 3f, duration / 2f,
                                        Ease.Linear))
                                    .Add(new Tween<float>(Elements[glyphIndex].Scale, 0.8f, duration / 2f,
                                        Ease.Linear))
                                    .Add(new Tween<float>(Elements[glyphIndex].Scale, 1.2f, duration / 15,
                                        Ease.Linear))
                                    .Add(new Tween<float>(Elements[glyphIndex].Scale, 1f, duration / 15,
                                        Ease.Linear))
                            )
                        )
                    )
                    ;

                var glyphSize = Elements[glyphIndex].Size;
                targetX += glyphSize.X;
            }
        }
    }
}
