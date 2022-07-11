namespace ExTween.Art
{
    public interface IFont
    {
        FloatXyPair CharacterSize(char c, float fontSize);
        TweenGlyph GetTweenGlyphForLetter(char c);
    }

    public class MonospaceFont : IFont
    {
        public static readonly MonospaceFont Instance = new MonospaceFont();

        public TweenGlyph GetTweenGlyphForLetter(char letter)
        {
            var x = new TweenableFloat();
            var y = new TweenableFloat();
            var shouldDraw = new TweenableInt(1);
            var duration = 1f;
            var primaryTween = new SequenceTween();

            if (char.IsWhiteSpace(letter))
            {
                return new TweenGlyph(new SequenceTween(), letter, x, y, shouldDraw);
            }

            void Keyframe(ITween subTween)
            {
                primaryTween.Add(subTween);
            }

            ITween SetXY(float targetX, float targetY)
            {
                return new CallbackTween(
                    () =>
                    {
                        x.ForceSetValue(targetX);
                        y.ForceSetValue(targetY);
                    });
            }

            ITween AxisLine(Tweenable<float> tweenable, float destination)
            {
                return new Tween<float>(tweenable, destination, duration, Ease.Linear);
            }

            ITween ArcBegin(float destinationX, float destinationY)
            {
                return new MultiplexTween()
                    .AddChannel(new Tween<float>(x, destinationX, duration, Ease.SineSlowFast))
                    .AddChannel(new Tween<float>(y, destinationY, duration, Ease.SineFastSlow));
            }

            ITween ArcEnd(float destinationX, float destinationY)
            {
                return new MultiplexTween()
                    .AddChannel(new Tween<float>(x, destinationX, duration, Ease.SineFastSlow))
                    .AddChannel(new Tween<float>(y, destinationY, duration, Ease.SineSlowFast));
            }

            ITween Enable()
            {
                return new CallbackTween(() => { shouldDraw.ForceSetValue(1); });
            }

            ITween Disable()
            {
                return new CallbackTween(() => { shouldDraw.ForceSetValue(0); });
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

            var width = CharacterSize(letter, 2).X;
            var height = CharacterSize(letter, 2).Y;
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
                    Keyframe(AxisLine(y, bottom));
                    Keyframe(AxisLine(y, center));
                    Keyframe(AxisLine(x, right));
                    Keyframe(AxisLine(y, top));
                    Keyframe(AxisLine(y, bottom));
                    break;

                case 'e':
                    Keyframe(Initialize(left, eCrossHeight));
                    Keyframe(AxisLine(x, right));
                    Keyframe(ArcBegin(center, lowercaseTop));
                    Keyframe(ArcEnd(left, eCrossHeight));
                    Keyframe(ArcBegin(center, bottom));
                    Keyframe(DrawPercentOf(0.75f, ArcEnd(right, eCrossHeight)));

                    break;

                case 'l':
                    Keyframe(Initialize(center, top));
                    Keyframe(AxisLine(y, bottom));
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
                    Keyframe(AxisLine(y, bottom));
                    Keyframe(AxisLine(y, armHeight));
                    Keyframe(ArcBegin(center, lowercaseTop));
                    Keyframe(ArcEnd(right, armHeight));
                    break;

                case 'd':
                    Keyframe(Initialize(right, armHeight));
                    Keyframe(ArcBegin(center, lowercaseTop));
                    Keyframe(ArcEnd(left, armHeight));
                    Keyframe(ArcBegin(center, bottom));
                    Keyframe(AxisLine(x, right));
                    Keyframe(AxisLine(y, top));
                    break;

                case '!':
                    Keyframe(Initialize(center, top));
                    Keyframe(DrawPercentOf(0.60f, AxisLine(y, bottom)));
                    Keyframe(Enable());
                    Keyframe(AxisLine(y, bottom * 0.90f));
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

            return new TweenGlyph(primaryTween, letter, x, y, shouldDraw);
        }

        public FloatXyPair CharacterSize(char _, float fontSize)
        {
            return new FloatXyPair(fontSize / 2, fontSize);
        }
    }
}
