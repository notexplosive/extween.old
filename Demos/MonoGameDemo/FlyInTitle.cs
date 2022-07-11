using System.Collections.Generic;
using ExTween;
using ExTween.Art;
using ExTween.MonoGame;
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
        
        protected override void BuildTween(SequenceTween tween)
        {
            var totalStringSize = this.font.MeasureString(this.message);
            
            {
                foreach (var letter in this.message)
                {
                    Elements.Add(new SpriteFontRenderedText
                    {
                        Text = letter.ToString(),
                        Position = new TweenableFloatXyPair(new Vector2(0, 500).ToXyPair()),
                        Scale = new TweenableFloat(1),
                        Font = this.font
                    });
                }
            }

            var multiplex = new MultiplexTween();
            tween.Add(multiplex);

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
