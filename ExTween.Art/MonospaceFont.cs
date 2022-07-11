﻿namespace ExTween.Art
{
    public interface IFont
    {
        FloatXyPair CharacterSize(char c);
        TweenGlyph GetTweenGlyphForLetter(char c);
        float FontSize { get; }
    }

    public class DrawKit
    {
        public SequenceTween Tween { get; } = new SequenceTween();
        public TweenableFloat X{ get; } = new TweenableFloat();
        public TweenableFloat Y{ get; } = new TweenableFloat();
        public TweenableInt ShouldDraw{ get; } = new TweenableInt(1);
    }
    
    public class MonospaceFont : IFont
    {
        public MonospaceFont(float fontSize)
        {
            FontSize = fontSize;
        }

        public float FontSize { get; } 
        
        public TweenGlyph GetTweenGlyphForLetter(char letter)
        {
            var kit = new DrawKit();

            var duration = 1f;

            if (char.IsWhiteSpace(letter))
            {
                return new TweenGlyph(new SequenceTween(), this, letter, kit.X, kit.Y, kit.ShouldDraw);
            }

            void Keyframe(ITween subTween)
            {
                kit.Tween.Add(subTween);
            }

            ITween SetXY(float targetX, float targetY)
            {
                return new CallbackTween(
                    () =>
                    {
                        kit.X.ForceSetValue(targetX);
                        kit.Y.ForceSetValue(targetY);
                    });
            }

            ITween AxisLine(Tweenable<float> tweenable, float destination)
            {
                return new Tween<float>(tweenable, destination, duration, Ease.Linear);
            }

            ITween ArcBegin(float destinationX, float destinationY)
            {
                return new MultiplexTween()
                    .AddChannel(new Tween<float>(kit.X, destinationX, duration, Ease.SineSlowFast))
                    .AddChannel(new Tween<float>(kit.Y, destinationY, duration, Ease.SineFastSlow));
            }

            ITween ArcEnd(float destinationX, float destinationY)
            {
                return new MultiplexTween()
                    .AddChannel(new Tween<float>(kit.X, destinationX, duration, Ease.SineFastSlow))
                    .AddChannel(new Tween<float>(kit.Y, destinationY, duration, Ease.SineSlowFast));
            }

            ITween Enable()
            {
                return new CallbackTween(() => { kit.ShouldDraw.ForceSetValue(1); });
            }

            ITween Disable()
            {
                return new CallbackTween(() => { kit.ShouldDraw.ForceSetValue(0); });
            }

            ITween DrawPercentOf(float percent, ITween subTween)
            {
                return new MultiplexTween()
                    .AddChannel(new SequenceTween()
                        .Add(new WaitSecondsTween(duration * percent))
                        .Add(Disable())
                    )
                    .AddChannel(subTween);
            }

            ITween Initialize(float destinationX, float destinationY)
            {
                return new SequenceTween()
                        .Add(SetXY(destinationX, destinationY))
                        .Add(Enable())
                    ;
            }

            var tinyFont = new MonospaceFont(2);
            
            var width = tinyFont.CharacterSize(letter).X;
            var height = tinyFont.CharacterSize(letter).Y;
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
                    Keyframe(Initialize(left, top));
                    Keyframe(AxisLine(kit.Y, bottom));
                    Keyframe(AxisLine(kit.Y, center));
                    Keyframe(AxisLine(kit.X, right));
                    Keyframe(AxisLine(kit.Y, top));
                    Keyframe(AxisLine(kit.Y, bottom));
                    break;

                case 'e':
                    Keyframe(Initialize(left, eCrossHeight));
                    Keyframe(AxisLine(kit.X, right));
                    Keyframe(ArcBegin(center, lowercaseTop));
                    Keyframe(ArcEnd(left, eCrossHeight));
                    Keyframe(ArcBegin(center, bottom));
                    Keyframe(DrawPercentOf(0.75f, ArcEnd(right, eCrossHeight)));

                    break;

                case 'l':
                    Keyframe(Initialize(center, top));
                    Keyframe(AxisLine(kit.Y, bottom));
                    break;

                case 'o':
                    Keyframe(Initialize(right, eCrossHeight));
                    Keyframe(ArcBegin(center, bottom));
                    Keyframe(ArcEnd(left, eCrossHeight));
                    Keyframe(ArcBegin(center, lowercaseTop));
                    Keyframe(ArcEnd(right, eCrossHeight));
                    break;

                case 'w':
                    Keyframe(Initialize(left, lowercaseTop));
                    Keyframe(ArcBegin(left / 2, bottom));
                    Keyframe(ArcEnd(center, lowercaseTop));
                    Keyframe(ArcBegin(right / 2, bottom));
                    Keyframe(ArcEnd(right, lowercaseTop));
                    break;

                case 'r':
                    Keyframe(Initialize(left, lowercaseTop));
                    Keyframe(AxisLine(kit.Y, bottom));
                    Keyframe(AxisLine(kit.Y, armHeight));
                    Keyframe(ArcBegin(center, lowercaseTop));
                    Keyframe(ArcEnd(right, armHeight));
                    break;

                case 'd':
                    Keyframe(Initialize(right, armHeight));
                    Keyframe(ArcBegin(center, lowercaseTop));
                    Keyframe(ArcEnd(left, armHeight));
                    Keyframe(ArcBegin(center, bottom));
                    Keyframe(AxisLine(kit.X, right));
                    Keyframe(AxisLine(kit.Y, top));
                    break;

                case '!':
                    Keyframe(Initialize(center, top));
                    Keyframe(DrawPercentOf(0.60f, AxisLine(kit.Y, bottom)));
                    Keyframe(Enable());
                    Keyframe(AxisLine(kit.Y, bottom * 0.90f));
                    break;

                default:
                    // By default we draw a circle
                    Keyframe(SetXY(right, center));
                    Keyframe(ArcBegin(center, bottom));
                    Keyframe(ArcEnd(left, center));
                    Keyframe(ArcBegin(center, top));
                    Keyframe(ArcEnd(right, center));
                    break;
            }

            return new TweenGlyph(kit.Tween, this, letter, kit.X, kit.Y, kit.ShouldDraw);
        }

        public FloatXyPair CharacterSize(char _)
        {
            return new FloatXyPair(FontSize / 2, FontSize);
        }
    }
}
