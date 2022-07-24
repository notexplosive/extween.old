using System;
using System.Collections.Generic;

namespace ExTween.Art
{
    public interface IFont
    {
        float FontSize { get; }
        FloatXyPair CharacterSize(char c);
        TweenGlyph GetTweenGlyphForLetter(char c);
    }

    public class DrawKit
    {
        private readonly List<float> keyframesInSeconds = new List<float>();
        private int cachedNumberOfSegments;
        private float[] cachedPercentKeyframes = Array.Empty<float>();
        public SequenceTween Tween { get; } = new SequenceTween();
        public TweenableFloat X { get; } = new TweenableFloat();
        public TweenableFloat Y { get; } = new TweenableFloat();
        public TweenableInt ShouldDraw { get; } = new TweenableInt(1);

        public float TweenDuration => Tween.TotalDuration.Get();

        public void AddKeyframe(float timeInSeconds)
        {
            if (!this.keyframesInSeconds.Contains(timeInSeconds))
            {
                this.keyframesInSeconds.Add(timeInSeconds);
            }
        }

        public float[] GetKeyframes(int numberOfSegments)
        {
            if (numberOfSegments > this.cachedPercentKeyframes.Length ||
                numberOfSegments != this.cachedNumberOfSegments)
            {
                BakeKeyframes(numberOfSegments);
            }

            return this.cachedPercentKeyframes;
        }

        public void BakeKeyframes(int numberOfSegments)
        {
            var allKeyframes = new List<float>(this.keyframesInSeconds);

            var expectedTotalCount = Math.Max(this.keyframesInSeconds.Count, numberOfSegments);
            var numberOfFramesToAdd = expectedTotalCount - this.keyframesInSeconds.Count;

            for (var i = 0; i < numberOfFramesToAdd; i++)
            {
                var percent = (float) i / numberOfFramesToAdd;
                allKeyframes.Add(percent * TweenDuration);
            }

            // Add a keyframe at the very end
            allKeyframes.Add(TweenDuration);

            allKeyframes.Sort();

            this.cachedPercentKeyframes = allKeyframes.ToArray();
            this.cachedNumberOfSegments = numberOfSegments;
        }

        public State GetStateAtTime(float time)
        {
            Tween.JumpTo(time);
            return new State(new FloatXyPair(X.Value, Y.Value), ShouldDraw.Value == 1);
        }

        public readonly struct State
        {
            public FloatXyPair Position { get; }
            public bool ShouldDraw { get; }

            public State(FloatXyPair position, bool shouldDraw)
            {
                Position = position;
                ShouldDraw = shouldDraw;
            }
        }
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

            const float duration = 1f;

            if (char.IsWhiteSpace(letter))
            {
                return new TweenGlyph(kit, this, letter);
            }

            void Keyframe(ITween subTween)
            {
                kit.AddKeyframe(kit.TweenDuration);
                kit.Tween.Add(subTween);
            }

            ITween SetXY(float x, float y)
            {
                return new CallbackTween(
                    () =>
                    {
                        kit.X.ForceSetValue(x);
                        kit.Y.ForceSetValue(y);
                    });
            }

            ITween AxisLine(Tweenable<float> tweenable, float destination)
            {
                return new Tween<float>(tweenable, destination, duration, Ease.Linear);
            }

            ITween ArcBegin(float x, float y)
            {
                return new MultiplexTween()
                    .AddChannel(new Tween<float>(kit.X, x, duration, Ease.SineSlowFast))
                    .AddChannel(new Tween<float>(kit.Y, y, duration, Ease.SineFastSlow));
            }

            ITween ArcEnd(float x, float y)
            {
                return new MultiplexTween()
                    .AddChannel(new Tween<float>(kit.X, x, duration, Ease.SineFastSlow))
                    .AddChannel(new Tween<float>(kit.Y, y, duration, Ease.SineSlowFast));
            }

            ITween LineTo(float x, float y)
            {
                return new MultiplexTween()
                    .AddChannel(new Tween<float>(kit.X, x, duration, Ease.Linear))
                    .AddChannel(new Tween<float>(kit.Y, y, duration, Ease.Linear));
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
                // Add a keyframe at the end of the shorter tween
                var subTweenDuration = subTween.TotalDuration.Get() * percent;
                kit.AddKeyframe(kit.TweenDuration + subTweenDuration);

                return new MultiplexTween()
                    .AddChannel(new SequenceTween()
                        .Add(new WaitSecondsTween(subTweenDuration))
                        .Add(Disable())
                    )
                    .AddChannel(subTween);
            }

            ITween Initialize(float x, float y)
            {
                return new SequenceTween()
                        .Add(SetXY(x, y))
                        .Add(Enable())
                    ;
            }

            var tinyFont = new MonospaceFont(2);

            var width = tinyFont.CharacterSize(letter).X;
            var height = tinyFont.CharacterSize(letter).Y;
            var quarterWidth = width / 4;
            var quarterHeight = height / 4;
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
                case 'A':
                    Keyframe(Initialize(left, bottom));
                    Keyframe(ArcBegin(center, top));
                    Keyframe(ArcEnd(right, bottom));
                    // todo: figure out how to render the cross-bar
                    break;

                case 'B':
                    Keyframe(Initialize(left, bottom));
                    Keyframe(AxisLine(kit.Y, top));
                    Keyframe(ArcEnd(center + quarterWidth, center - quarterHeight));
                    Keyframe(ArcBegin(left, center));
                    Keyframe(ArcEnd(right, center + quarterHeight));
                    Keyframe(ArcBegin(left, bottom));
                    break;

                case 'C':
                    Keyframe(Initialize(right, center - quarterHeight));
                    Keyframe(ArcBegin(center, top));
                    Keyframe(ArcEnd(left, center));
                    Keyframe(ArcBegin(center, bottom));
                    Keyframe(ArcEnd(right, center + quarterHeight));
                    break;

                case 'D':
                    Keyframe(Initialize(left, top));
                    Keyframe(AxisLine(kit.Y, bottom));
                    Keyframe(ArcEnd(right, center));
                    Keyframe(ArcBegin(left, top));
                    break;

                case 'E':
                    Keyframe(Initialize(right, top));
                    Keyframe(AxisLine(kit.X, left));
                    Keyframe(AxisLine(kit.Y, bottom));
                    Keyframe(AxisLine(kit.X, right));
                    Keyframe(AxisLine(kit.X, left));
                    Keyframe(AxisLine(kit.Y, center));
                    Keyframe(AxisLine(kit.X, center + quarterWidth));
                    break;

                case 'F':
                    Keyframe(Initialize(right, top));
                    Keyframe(AxisLine(kit.X, left));
                    Keyframe(AxisLine(kit.Y, bottom));
                    Keyframe(AxisLine(kit.Y, center));
                    Keyframe(AxisLine(kit.X, center + quarterWidth));
                    break;

                case 'G':
                    Keyframe(Initialize(right, center - quarterHeight));
                    Keyframe(ArcBegin(center, top));
                    Keyframe(ArcEnd(left, center));
                    Keyframe(ArcBegin(center, bottom));
                    Keyframe(ArcEnd(right, center));
                    Keyframe(AxisLine(kit.X, center));
                    break;

                case 'H':
                    Keyframe(Initialize(left, top));
                    Keyframe(AxisLine(kit.Y, bottom));
                    Keyframe(AxisLine(kit.Y, center));
                    Keyframe(AxisLine(kit.X, right));
                    Keyframe(AxisLine(kit.Y, top));
                    Keyframe(AxisLine(kit.Y, bottom));
                    break;

                case 'I':
                    Keyframe(Initialize(left, top));
                    Keyframe(AxisLine(kit.X, right));
                    Keyframe(AxisLine(kit.X, center));
                    Keyframe(AxisLine(kit.Y, bottom));
                    Keyframe(AxisLine(kit.X, right));
                    Keyframe(AxisLine(kit.X, left));
                    break;

                case 'J':
                    Keyframe(Initialize(left, top));
                    Keyframe(AxisLine(kit.X, right));
                    Keyframe(AxisLine(kit.X, center));
                    Keyframe(ArcEnd(center + quarterWidth, bottom - quarterHeight));
                    Keyframe(ArcBegin(center, bottom));
                    Keyframe(ArcEnd(left, bottom - quarterHeight));
                    break;

                case 'K':
                    Keyframe(Initialize(left, top));
                    Keyframe(AxisLine(kit.Y, bottom));
                    Keyframe(AxisLine(kit.Y, center));
                    Keyframe(ArcEnd(right, top));
                    Keyframe(ArcBegin(left, center)); // todo: this is a sloppy way to teleport back to where we were
                    Keyframe(ArcEnd(right, bottom));
                    break;

                case 'L':
                    Keyframe(Initialize(left, top));
                    Keyframe(AxisLine(kit.Y, bottom));
                    Keyframe(AxisLine(kit.X, right));
                    break;

                case 'M':
                    Keyframe(Initialize(left, bottom));
                    Keyframe(AxisLine(kit.Y, top));
                    Keyframe(ArcEnd(center, center));
                    Keyframe(ArcBegin(right, top));
                    Keyframe(AxisLine(kit.Y, bottom));
                    break;

                case 'N':
                    Keyframe(Initialize(left, bottom));
                    Keyframe(AxisLine(kit.Y, top));
                    Keyframe(ArcEnd(right, bottom));
                    Keyframe(AxisLine(kit.Y, top));
                    break;

                case 'O':
                    Keyframe(Initialize(right, center));
                    Keyframe(ArcBegin(center, bottom));
                    Keyframe(ArcEnd(left, center));
                    Keyframe(ArcBegin(center, top));
                    Keyframe(ArcEnd(right, center));
                    break;

                case 'P':
                    Keyframe(Initialize(left, bottom));
                    Keyframe(AxisLine(kit.Y, top));
                    Keyframe(ArcEnd(right, center - quarterHeight));
                    Keyframe(ArcBegin(left, center));
                    break;

                case 'Q':
                    Keyframe(Initialize(right, center));
                    Keyframe(ArcBegin(center, bottom));
                    Keyframe(ArcEnd(left, center));
                    Keyframe(ArcBegin(center, top));
                    Keyframe(ArcEnd(right, center));
                    Keyframe(ArcEnd(center, center)); // todo: wish this skipped to this position
                    Keyframe(ArcBegin(right, bottom));
                    break;

                case 'R':
                    Keyframe(Initialize(left, bottom));
                    Keyframe(AxisLine(kit.Y, top));
                    Keyframe(ArcEnd(right, center - quarterHeight));
                    Keyframe(ArcBegin(left, center));
                    Keyframe(ArcEnd(right, bottom));
                    break;

                case 'S':
                    Keyframe(Initialize(right, center - quarterHeight));
                    Keyframe(ArcBegin(center, top));
                    Keyframe(ArcEnd(left, center - quarterHeight));
                    Keyframe(ArcBegin(center, center));
                    Keyframe(ArcEnd(right, center + quarterHeight));
                    Keyframe(ArcBegin(center, bottom));
                    Keyframe(ArcEnd(left, center + quarterHeight));
                    break;

                case 'T':
                    Keyframe(Initialize(left, top));
                    Keyframe(AxisLine(kit.X, right));
                    Keyframe(AxisLine(kit.X, center));
                    Keyframe(AxisLine(kit.Y, bottom));
                    break;

                case 'U':
                    Keyframe(Initialize(left, top));
                    Keyframe(AxisLine(kit.Y, center));
                    Keyframe(ArcBegin(center, bottom));
                    Keyframe(ArcEnd(right, center));
                    Keyframe(AxisLine(kit.Y, top));
                    break;

                case 'V':
                    Keyframe(Initialize(left, top));
                    Keyframe(LineTo(center, bottom));
                    Keyframe(LineTo(right, top));
                    break;

                case 'W':
                    Keyframe(Initialize(left, top));
                    Keyframe(ArcBegin(center - quarterWidth, bottom));
                    Keyframe(ArcEnd(center, center));
                    Keyframe(ArcBegin(center + quarterWidth, bottom));
                    Keyframe(ArcEnd(right, top));
                    break;

                case 'X':
                    Keyframe(Initialize(left, top));
                    Keyframe(LineTo(right, bottom));
                    Keyframe(LineTo(center, center)); // todo: wish I could warp
                    Keyframe(LineTo(left, bottom));
                    Keyframe(LineTo(right, top));
                    break;

                case 'Y':
                    Keyframe(Initialize(left, top));
                    Keyframe(ArcBegin(center, center));
                    Keyframe(ArcEnd(right, top));
                    Keyframe(DrawPercentOf(0f, ArcBegin(center, center)));
                    Keyframe(Enable());
                    Keyframe(AxisLine(kit.Y, bottom));
                    break;
                
                case 'Z':
                    Keyframe(Initialize(left, top));
                    Keyframe(AxisLine(kit.X, right));
                    Keyframe(LineTo(left, bottom));
                    Keyframe(AxisLine(kit.X, right));
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

            kit.X.Value = 0;
            kit.Y.Value = 0;
            kit.BakeKeyframes(0);
            return new TweenGlyph(kit, this, letter);
        }

        public FloatXyPair CharacterSize(char _)
        {
            return new FloatXyPair(FontSize / 2, FontSize);
        }
    }
}
