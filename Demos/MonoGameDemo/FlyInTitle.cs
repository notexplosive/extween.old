using System.Collections.Generic;
using ExTween;
using ExTween.MonoGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameDemo
{
    public class FlyInTitle : Slide
    {
        private readonly SpriteFont font;
        private readonly string message;
        private readonly List<SpriteFontGlyph> glyphs = new List<SpriteFontGlyph>();

        public FlyInTitle(SpriteFont font, string message)
        {
            this.font = font;
            this.message = message;
        }
        
        protected override void BuildTween(SequenceTween tween)
        {
            this.glyphs.Clear();

            var totalStringSize = this.font.MeasureString(this.message);
            
            {
                foreach (var letter in this.message)
                {
                    this.glyphs.Add(new SpriteFontGlyph
                    {
                        Text = letter.ToString(),
                        Position = new TweenableVector2(new Vector2(0, 500)),
                        Scale = new TweenableFloat(1),
                        Font = this.font
                    });
                }
            }

            var multiplex = new MultiplexTween();
            tween.Add(multiplex);

            var targetX = -totalStringSize.X / 2;
            var targetY = -totalStringSize.Y / 2;
            for (var glyphIndex = 0; glyphIndex < this.glyphs.Count; glyphIndex++)
            {
                var duration = 0.5f;

                multiplex
                    .AddChannel(new SequenceTween()
                        .Add(new WaitSecondsTween(glyphIndex / 15f))
                        .Add(new MultiplexTween()
                            .AddChannel(
                                new SequenceTween()
                                    .Add(new Tween<Vector2>(this.glyphs[glyphIndex].Position,
                                        new Vector2(targetX + 100, targetY - 200), duration / 2, Ease.QuadFastSlow))
                                    .Add(new Tween<Vector2>(this.glyphs[glyphIndex].Position,
                                        new Vector2(targetX, targetY), duration / 2, Ease.QuadSlowFast))
                            )
                            .AddChannel(
                                new SequenceTween()
                                    .Add(new Tween<float>(this.glyphs[glyphIndex].Scale, 3f, duration / 2f,
                                        Ease.Linear))
                                    .Add(new Tween<float>(this.glyphs[glyphIndex].Scale, 0.8f, duration / 2f,
                                        Ease.Linear))
                                    .Add(new Tween<float>(this.glyphs[glyphIndex].Scale, 1.2f, duration / 15,
                                        Ease.Linear))
                                    .Add(new Tween<float>(this.glyphs[glyphIndex].Scale, 1f, duration / 15,
                                        Ease.Linear))
                            )
                        )
                    )
                    ;

                var glyphSize = this.font.MeasureString(this.glyphs[glyphIndex].Text);
                targetX += glyphSize.X;
            }
        }

        protected override void OnTweenBegin()
        {
        }

        public override void Draw(SpriteBatch spriteBatch)
        {
            foreach (var glyph in this.glyphs)
            {
                glyph.Draw(spriteBatch);
            }
        }
    }
}
