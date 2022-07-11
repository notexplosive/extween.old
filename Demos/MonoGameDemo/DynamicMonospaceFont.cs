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

            Tween<float> AxisLine(Tweenable<float> tweenable, float destination)
            {
                return new Tween<float>(tweenable, destination, duration, Ease.Linear);
            }

            MultiplexTween ArcStart(float targetX, float targetY)
            {
                return new MultiplexTween()
                    .AddChannel(new Tween<float>(x, targetX, duration, Ease.SineSlowFast))
                    .AddChannel(new Tween<float>(y, targetY, duration, Ease.SineFastSlow));
            }

            MultiplexTween ArcEnd(float targetX, float targetY)
            {
                return new MultiplexTween()
                    .AddChannel(new Tween<float>(x, targetX, duration, Ease.SineFastSlow))
                    .AddChannel(new Tween<float>(y, targetY, duration, Ease.SineSlowFast));
            }

            switch (letter)
            {
                case 'H':
                    tween = new SequenceTween()
                            .Add(SetXY(-0.5f, -1))
                            .Add(AxisLine(y, 1))
                            .Add(AxisLine(y, 0))
                            .Add(AxisLine(x, 0.5f))
                            .Add(AxisLine(y, -1f))
                            .Add(AxisLine(y, 1f))
                        ;
                    break;

                case 'e':
                    tween = new SequenceTween()
                            .Add(SetXY(-0.5f, 0.5f))
                            .Add(AxisLine(x, 0.5f))
                            .Add(ArcStart(0, 0))
                            .Add(ArcEnd(-0.5f, 0.5f))
                            .Add(ArcStart(0, 1))
                            .Add(ArcEnd(MathF.Cos(MathF.PI / 4) * 0.5f, MathF.Sin(MathF.PI / 4) * 0.5f + 0.5f))
                        ;
                    break;

                case 'l':
                    tween = new SequenceTween()
                            .Add(SetXY(0, -1))
                            .Add(AxisLine(y, 1))
                        ;
                    break;

                case 'o':
                    tween = new SequenceTween()
                            .Add(SetXY(0.5f, 0.5f))
                            .Add(ArcStart(0, 1))
                            .Add(ArcEnd(-0.5f, 0.5f))
                            .Add(ArcStart(0f, 0f))
                            .Add(ArcEnd(0.5f, 0.5f))
                        ;
                    break;
            }

            if (tween == null)
            {
                // Default letter is just a circle
                tween = new SequenceTween()
                        .Add(SetXY(1, 0))
                        .Add(ArcStart(0, 1))
                        .Add(ArcEnd(-1, 0))
                        .Add(ArcStart(0, -1))
                        .Add(ArcEnd(1, 0))
                    ;
            }

            return new TweenPattern(tween, x, y);
        }
    }
}
