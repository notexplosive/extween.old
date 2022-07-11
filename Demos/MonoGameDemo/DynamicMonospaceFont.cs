using System;
using ExTween;
using Microsoft.Xna.Framework;

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
            var tween = new SequenceTween();

            if (char.IsWhiteSpace(letter))
            {
                return new TweenPattern(new SequenceTween(), x, y);
            }

            void SetXY(float targetX, float targetY)
            {
                tween.Add(new CallbackTween(
                    () =>
                    {
                        x.ForceSetValue(targetX);
                        y.ForceSetValue(targetY);
                    }));
            }

            void AxisLine(Tweenable<float> tweenable, float destination)
            {
                tween.Add(new Tween<float>(tweenable, destination, duration, Ease.Linear));
            }

            void ArcBegin(float destinationX, float destinationY)
            {
                tween.Add(new MultiplexTween()
                    .AddChannel(new Tween<float>(x, destinationX, duration, Ease.SineSlowFast))
                    .AddChannel(new Tween<float>(y, destinationY, duration, Ease.SineFastSlow)));
            }

            void ArcEnd(float destinationX, float destinationY)
            {
                tween.Add(new MultiplexTween()
                    .AddChannel(new Tween<float>(x, destinationX, duration, Ease.SineFastSlow))
                    .AddChannel(new Tween<float>(y, destinationY, duration, Ease.SineSlowFast)));
            }

            var width = CharacterSize(2).X;
            var height = CharacterSize(2).Y;
            var top = -height / 2;
            var bottom = height / 2;
            var left = -width / 2;
            var right = width / 2;
            var center = 0;
            var lowercaseTop = 0f;

            // y position that the lowercase 'r' arm juts out
            var armHeight = lowercaseTop + 0.35f;
            // y position of the horizontal line in lowercase 'e'
            var eCrossHeight = lowercaseTop + 0.5f;

            switch (letter)
            {
                case 'H':
                    SetXY(left, top);
                    AxisLine(y, bottom);
                    AxisLine(y, center);
                    AxisLine(x, right);
                    AxisLine(y, top);
                    AxisLine(y, bottom);
                    break;

                case 'e':
                    SetXY(left, eCrossHeight);
                    AxisLine(x, right);
                    ArcBegin(center, lowercaseTop);
                    ArcEnd(left, eCrossHeight);
                    ArcBegin(center, bottom);
                    ArcEnd(MathF.Cos(MathF.PI / 4) * 0.5f, MathF.Sin(MathF.PI / 4) * 0.5f + 0.5f);
                    break;

                case 'l':
                    SetXY(center, top);
                    AxisLine(y, bottom);
                    break;

                case 'o':
                    SetXY(right, eCrossHeight);
                    ArcBegin(center, bottom);
                    ArcEnd(left, eCrossHeight);
                    ArcBegin(center, lowercaseTop);
                    ArcEnd(right, eCrossHeight);
                    break;

                case 'w':
                    SetXY(left, lowercaseTop);
                    ArcBegin(left / 2, bottom);
                    ArcEnd(center, lowercaseTop);
                    ArcBegin(right / 2, bottom);
                    ArcEnd(right, lowercaseTop);
                    break;

                case 'r':
                    SetXY(left, lowercaseTop);
                    AxisLine(y, bottom);
                    AxisLine(y, armHeight);
                    ArcBegin(center, lowercaseTop);
                    ArcEnd(right, armHeight);
                    break;

                case 'd':
                    SetXY(right, armHeight);
                    ArcBegin(center, lowercaseTop);
                    ArcEnd(left, armHeight);
                    ArcBegin(center, bottom);
                    AxisLine(x, right);
                    AxisLine(y, top);
                    break;

                default:
                    // By default we draw a circle
                    SetXY(right, center);
                    ArcBegin(center, bottom);
                    ArcEnd(left, center);
                    ArcBegin(center, top);
                    ArcEnd(right, center);
                    break;
            }

            return new TweenPattern(tween, x, y);
        }

        public Vector2 CharacterSize(float fontSize)
        {
            return new Vector2(fontSize / 2, fontSize);
        }
    }
}
