using System.Collections.Generic;
using ExTween;
using ExTween.MonoGame;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameDemo
{
    public class TitleSlide : Slide
    {
        private readonly List<Glyph> glyphs = new List<Glyph>();

        protected override void BuildTween(SequenceTween tween)
        {
            this.glyphs.Clear();

            var totalString = "NotExplosive";
            var totalStringSize = Demo.Font.MeasureString(totalString);
            
            {
                var i = 0;
                foreach (var letter in totalString)
                {
                    this.glyphs.Add(new Glyph
                    {
                        Text = letter.ToString(),
                        Position = new TweenableVector2(new Vector2(800 / totalString.Length * i, 600)),
                        Scale = new TweenableFloat(1)
                    });
                    i++;
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
                        .Add(new WaitSecondsTween(glyphIndex / 20f))
                        .Add(new MultiplexTween()
                            .AddChannel(
                                new SequenceTween()
                                    .Add(new Tween<Vector2>(this.glyphs[glyphIndex].Position,
                                        new Vector2(targetX, targetY - 80), duration * 8 / 10, Ease.Linear))
                                    .Add(new Tween<Vector2>(this.glyphs[glyphIndex].Position,
                                        new Vector2(targetX, targetY), duration * 2 / 10, Ease.Linear))
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

                var glyphSize = Demo.Font.MeasureString(this.glyphs[glyphIndex].Text);
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
                var offset = Demo.Font.MeasureString(glyph.Text) / 2;
                spriteBatch.DrawString(Demo.Font, glyph.Text, glyph.Position + offset, Color.Black, glyph.Angle, offset,
                    glyph.Scale, SpriteEffects.None, 0f);
            }
        }

        private class Glyph
        {
            public TweenableVector2 Position { get; set; } = new TweenableVector2();
            public string Text { get; set; }
            public TweenableFloat Angle { get; } = new TweenableFloat();
            public TweenableFloat Scale { get; set; } = new TweenableFloat();
        }
    }
}
