using System;
using ExTween;

namespace MonoGameDemo
{
    public class DynamicMonospaceFont
    {
        public static DynamicMonospaceFont Instance = new DynamicMonospaceFont();

        public TweenPattern GetPatternForLetter(char letter)
        {
            var x = new TweenableFloat();
            var y = new TweenableFloat();
            var duration = 1f;
            ITween tween = null;

            if (char.IsWhiteSpace(letter))
            {
                return new TweenPattern(new SequenceTween(), x, y);
            }
            
            CallbackTween SetXY(float targetX, float targetY)
            {
                return new CallbackTween(
                    () =>
                    {
                        x.ForceSetValue(targetX);
                        y.ForceSetValue(targetY);
                    });
            }

            switch (letter)
            {
                case 'H':
                    tween = new SequenceTween()
                            .Add(
                                SetXY(-0.5f, -1)
                            )
                            .Add(
                                new Tween<float>(y, 1, duration, Ease.Linear)
                            )
                            .Add(
                                new Tween<float>(y, 0, duration, Ease.Linear)
                            )
                            .Add(
                                new Tween<float>(x, 0.5f, duration, Ease.Linear)
                            )
                            .Add(
                                new Tween<float>(y, -1, duration, Ease.Linear)
                            )
                            .Add(
                                new Tween<float>(y, 1, duration, Ease.Linear)
                            )
                        ;
                    break;

                case 'e':
                    tween = new SequenceTween()
                            .Add(
                                new CallbackTween(
                                    () =>
                                    {
                                        x.ForceSetValue(-0.5f);
                                        y.ForceSetValue(0.5f);
                                    })
                            )
                            .Add(
                                new Tween<float>(x, 0.5f, duration, Ease.Linear)
                            )
                            .Add(
                                new MultiplexTween()
                                    .AddChannel(new Tween<float>(x, 0f, duration, Ease.SineSlowFast))
                                    .AddChannel(new Tween<float>(y, 0f, duration, Ease.SineFastSlow))
                            )
                            .Add(
                                new MultiplexTween()
                                    .AddChannel(new Tween<float>(x, -0.5f, duration, Ease.SineFastSlow))
                                    .AddChannel(new Tween<float>(y, 0.5f, duration, Ease.SineSlowFast))
                            )
                            .Add(
                                new MultiplexTween()
                                    .AddChannel(new Tween<float>(x, 0f, duration, Ease.SineSlowFast))
                                    .AddChannel(new Tween<float>(y, 1f, duration, Ease.SineFastSlow))
                            )
                            .Add(
                                new MultiplexTween()
                                    .AddChannel(new Tween<float>(x, MathF.Cos(MathF.PI / 4) * 0.5f, duration,
                                        Ease.SineFastSlow))
                                    .AddChannel(new Tween<float>(y, MathF.Sin(MathF.PI / 4) * 0.5f + 0.5f,
                                        duration, Ease.SineSlowFast))
                            )
                        ;
                    break;

                case 'l':
                    tween = new SequenceTween()
                            .Add(
                                new CallbackTween(
                                    () =>
                                    {
                                        x.ForceSetValue(0f);
                                        y.ForceSetValue(-1f);
                                    })
                            )
                            .Add(
                                new Tween<float>(y, 1f, duration, Ease.Linear)
                            )
                        ;
                    break;

                case 'o':
                    tween = new SequenceTween()
                            .Add(
                                new CallbackTween(
                                    () =>
                                    {
                                        x.ForceSetValue(0.5f);
                                        y.ForceSetValue(0.5f);
                                    })
                            )
                            .Add(
                                new MultiplexTween()
                                    .AddChannel(new Tween<float>(x, 0, duration, Ease.SineSlowFast))
                                    .AddChannel(new Tween<float>(y, 1f, duration, Ease.SineFastSlow))
                            )
                            .Add(
                                new MultiplexTween()
                                    .AddChannel(new Tween<float>(x, -0.5f, duration, Ease.SineFastSlow))
                                    .AddChannel(new Tween<float>(y, 0.5f, duration, Ease.SineSlowFast))
                            )
                            .Add(
                                new MultiplexTween()
                                    .AddChannel(new Tween<float>(x, 0, duration, Ease.SineSlowFast))
                                    .AddChannel(new Tween<float>(y, 0f, duration, Ease.SineFastSlow))
                            )
                            .Add(
                                new MultiplexTween()
                                    .AddChannel(new Tween<float>(x, 0.5f, duration, Ease.SineFastSlow))
                                    .AddChannel(new Tween<float>(y, 0.5f, duration, Ease.SineSlowFast))
                            )
                        ;
                    break;
            }

            if (tween == null)
            {
                // Default letter is just a circle
                tween = new SequenceTween()
                        .Add(
                            new CallbackTween(
                                () =>
                                {
                                    x.ForceSetValue(1);
                                    y.ForceSetValue(0);
                                })
                        )
                        .Add(
                            new MultiplexTween()
                                .AddChannel(new Tween<float>(x, 0, duration, Ease.SineSlowFast))
                                .AddChannel(new Tween<float>(y, 1, duration / 4, Ease.SineFastSlow))
                        )
                        .Add(
                            new MultiplexTween()
                                .AddChannel(new Tween<float>(x, -1, duration, Ease.SineFastSlow))
                                .AddChannel(new Tween<float>(y, 0, duration, Ease.SineSlowFast))
                        )
                        .Add(
                            new MultiplexTween()
                                .AddChannel(new Tween<float>(x, 0, duration, Ease.SineSlowFast))
                                .AddChannel(new Tween<float>(y, -1, duration, Ease.SineFastSlow))
                        )
                        .Add(
                            new MultiplexTween()
                                .AddChannel(new Tween<float>(x, 1, duration, Ease.SineFastSlow))
                                .AddChannel(new Tween<float>(y, 0, duration, Ease.SineSlowFast))
                        )
                    ;
            }

            return new TweenPattern(tween, x, y);
        }
    }
}
