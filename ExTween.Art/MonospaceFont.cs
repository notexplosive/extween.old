namespace ExTween.Art
{
    public class MonospaceFont : IFont
    {
        public MonospaceFont(float fontSize)
        {
            FontSize = fontSize;
        }

        public float FontSize { get; }

        public TweenGlyph GetTweenGlyphForLetter(char letter)
        {
            var path = new TweenPath();

            const float duration = 1f;

            if (char.IsWhiteSpace(letter))
            {
                return new TweenGlyph(path, this, letter);
            }

            void Keyframe(ITween subTween)
            {
                path.AddKeyframe(path.Duration);
                path.Tween.Add(subTween);
            }

            ITween SetXY(float x, float y)
            {
                return new CallbackTween(
                    () =>
                    {
                        path.X.ForceSetValue(x);
                        path.Y.ForceSetValue(y);
                    });
            }

            ITween AxisLine(Tweenable<float> tweenable, float destination)
            {
                return new Tween<float>(tweenable, destination, duration, Ease.Linear);
            }

            ITween ArcBegin(float x, float y)
            {
                return new MultiplexTween()
                    .AddChannel(new Tween<float>(path.X, x, duration, Ease.SineSlowFast))
                    .AddChannel(new Tween<float>(path.Y, y, duration, Ease.SineFastSlow));
            }

            ITween ArcEnd(float x, float y)
            {
                return new MultiplexTween()
                    .AddChannel(new Tween<float>(path.X, x, duration, Ease.SineFastSlow))
                    .AddChannel(new Tween<float>(path.Y, y, duration, Ease.SineSlowFast));
            }

            ITween LineTo(float x, float y)
            {
                return new MultiplexTween()
                    .AddChannel(new Tween<float>(path.X, x, duration, Ease.Linear))
                    .AddChannel(new Tween<float>(path.Y, y, duration, Ease.Linear));
            }

            ITween Enable()
            {
                return new CallbackTween(() => { path.ShouldDraw.ForceSetValue(1); });
            }

            ITween Disable()
            {
                return new CallbackTween(() => { path.ShouldDraw.ForceSetValue(0); });
            }

            ITween DrawPercentOf(ITween subTween, float startPercent, float endPercent)
            {
                // Add a keyframe at the end of the shorter tween
                var startSeconds = subTween.TotalDuration.Get() * startPercent;
                var endSeconds = subTween.TotalDuration.Get() * endPercent;
                path.AddKeyframe(path.Duration + startSeconds);
                path.AddKeyframe(path.Duration + endSeconds);

                var result = new SequenceTween();

                if (startPercent > 0)
                {
                    result.Add(Disable());
                }

                result.Add(new MultiplexTween()
                    .AddChannel(new SequenceTween()
                        .Add(new WaitSecondsTween(startSeconds))
                        .Add(Enable())
                        .Add(new WaitSecondsTween(endSeconds - startSeconds))
                        .Add(Disable())
                    )
                    .AddChannel(subTween)
                );

                result.Add(Enable());

                return result;
            }

            ITween WarpTo(float x, float y)
            {
                Keyframe(DrawPercentOf(LineTo(x, y), 0f, 0f));
                return Enable();
            }

            ITween Initialize(float x, float y, bool startEnabled = true)
            {
                var result = new SequenceTween()
                        .Add(SetXY(x, y))
                    ;

                result.Add(startEnabled ? Enable() : Disable());

                return result;
            }

            var tinyFont = new MonospaceFont(2);

            var width = tinyFont.CharacterSize(letter).X;
            var height = tinyFont.CharacterSize(letter).Y;
            var halfHeight = height / 2;
            var halfWidth = width / 2;
            var quarterWidth = halfWidth / 2;
            var quarterHeight = halfHeight / 2;
            var sixteenthHeight = quarterHeight / 4;
            var centerX = 0;
            var centerY = 0;
            var left = centerX - halfWidth;
            var right = centerX + halfWidth;
            var top = centerY - halfHeight;
            var bottom = centerY + halfHeight;
            var lowercaseTop = 0f;

            // y position that the lowercase 'r' arm juts out
            var armHeight = lowercaseTop + halfHeight * 0.35f;
            // y position of the horizontal line in lowercase 'e'
            var eCrossHeight = lowercaseTop + quarterHeight;
            var lowercaseVerticalCenter = lowercaseTop + quarterHeight;

            void LowercaseCircleMacro(float verticalOffset = 0f)
            {
                Keyframe(WarpTo(left, centerY + quarterHeight + verticalOffset));
                Keyframe(ArcBegin(centerX, lowercaseTop + verticalOffset));
                Keyframe(ArcEnd(right, centerY + quarterHeight + verticalOffset));
                Keyframe(ArcBegin(centerX, bottom + verticalOffset));
                Keyframe(ArcEnd(left, centerY + quarterHeight + verticalOffset));
            }

            switch (letter)
            {
                case 'A':
                    Keyframe(Initialize(left, bottom));
                    Keyframe(ArcBegin(centerX, top));
                    Keyframe(ArcEnd(right, bottom));
                    // todo: figure out how to render the cross-bar
                    break;

                case 'B':
                    Keyframe(Initialize(left, bottom));
                    Keyframe(AxisLine(path.Y, top));
                    Keyframe(ArcEnd(centerX + quarterWidth, centerY - quarterHeight));
                    Keyframe(ArcBegin(left, centerY));
                    Keyframe(ArcEnd(right, centerY + quarterHeight));
                    Keyframe(ArcBegin(left, bottom));
                    break;

                case 'C':
                    Keyframe(Initialize(right, centerY - quarterHeight));
                    Keyframe(ArcBegin(centerX, top));
                    Keyframe(ArcEnd(left, centerY));
                    Keyframe(ArcBegin(centerX, bottom));
                    Keyframe(ArcEnd(right, centerY + quarterHeight));
                    break;

                case 'D':
                    Keyframe(Initialize(left, top));
                    Keyframe(AxisLine(path.Y, bottom));
                    Keyframe(ArcEnd(right, centerY));
                    Keyframe(ArcBegin(left, top));
                    break;

                case 'E':
                    Keyframe(Initialize(right, top));
                    Keyframe(AxisLine(path.X, left));
                    Keyframe(AxisLine(path.Y, bottom));
                    Keyframe(AxisLine(path.X, right));
                    Keyframe(WarpTo(left, centerY));
                    Keyframe(AxisLine(path.X, centerX + quarterWidth));
                    break;

                case 'F':
                    Keyframe(Initialize(right, top));
                    Keyframe(AxisLine(path.X, left));
                    Keyframe(AxisLine(path.Y, bottom));
                    Keyframe(WarpTo(left, centerY));
                    Keyframe(AxisLine(path.X, centerX + quarterWidth));
                    break;

                case 'G':
                    Keyframe(Initialize(right, centerY - quarterHeight));
                    Keyframe(ArcBegin(centerX, top));
                    Keyframe(ArcEnd(left, centerY));
                    Keyframe(ArcBegin(centerX, bottom));
                    Keyframe(ArcEnd(right, centerY));
                    Keyframe(AxisLine(path.X, centerX));
                    break;

                case 'H':
                    Keyframe(Initialize(left, top));
                    Keyframe(AxisLine(path.Y, bottom));
                    Keyframe(WarpTo(right, top));
                    Keyframe(AxisLine(path.Y, bottom));
                    Keyframe(WarpTo(left, centerY));
                    Keyframe(AxisLine(path.X, right));
                    break;

                case 'I':
                    Keyframe(Initialize(left, top));
                    Keyframe(AxisLine(path.X, right));
                    Keyframe(WarpTo(centerX, top));
                    Keyframe(AxisLine(path.Y, bottom));
                    Keyframe(WarpTo(left, bottom));
                    Keyframe(AxisLine(path.X, right));
                    break;

                case 'J':
                    Keyframe(Initialize(left, top));
                    Keyframe(AxisLine(path.X, right));
                    Keyframe(AxisLine(path.Y, centerY + quarterHeight));
                    Keyframe(ArcBegin(centerX, bottom));
                    Keyframe(ArcEnd(left, centerY + quarterHeight));
                    break;

                case 'K':
                    Keyframe(Initialize(left, top));
                    Keyframe(AxisLine(path.Y, bottom));
                    Keyframe(AxisLine(path.Y, centerY));
                    Keyframe(ArcEnd(right, top));
                    Keyframe(WarpTo(left, centerY));
                    Keyframe(ArcEnd(right, bottom));
                    break;

                case 'L':
                    Keyframe(Initialize(left, top));
                    Keyframe(AxisLine(path.Y, bottom));
                    Keyframe(AxisLine(path.X, right));
                    break;

                case 'M':
                    Keyframe(Initialize(left, bottom));
                    Keyframe(AxisLine(path.Y, top));
                    Keyframe(ArcEnd(centerX, centerY));
                    Keyframe(ArcBegin(right, top));
                    Keyframe(AxisLine(path.Y, bottom));
                    break;

                case 'N':
                    Keyframe(Initialize(left, bottom));
                    Keyframe(AxisLine(path.Y, top));
                    Keyframe(ArcEnd(right, bottom));
                    Keyframe(AxisLine(path.Y, top));
                    break;

                case 'O':
                    Keyframe(Initialize(right, centerY));
                    Keyframe(ArcBegin(centerX, bottom));
                    Keyframe(ArcEnd(left, centerY));
                    Keyframe(ArcBegin(centerX, top));
                    Keyframe(ArcEnd(right, centerY));
                    break;

                case 'P':
                    Keyframe(Initialize(left, bottom));
                    Keyframe(AxisLine(path.Y, top));
                    Keyframe(ArcEnd(right, centerY - quarterHeight));
                    Keyframe(ArcBegin(left, centerY));
                    break;

                case 'Q':
                    Keyframe(Initialize(right, centerY));
                    Keyframe(ArcBegin(centerX, bottom));
                    Keyframe(ArcEnd(left, centerY));
                    Keyframe(ArcBegin(centerX, top));
                    Keyframe(ArcEnd(right, centerY));
                    Keyframe(WarpTo(centerX, centerY + quarterHeight));
                    Keyframe(ArcEnd(right, bottom));
                    break;

                case 'R':
                    Keyframe(Initialize(left, bottom));
                    Keyframe(AxisLine(path.Y, top));
                    Keyframe(ArcEnd(right, centerY - quarterHeight));
                    Keyframe(ArcBegin(left, centerY));
                    Keyframe(ArcEnd(right, bottom));
                    break;

                case 'S':
                    Keyframe(Initialize(right, centerY - quarterHeight));
                    Keyframe(ArcBegin(centerX, top));
                    Keyframe(ArcEnd(left, centerY - quarterHeight));
                    Keyframe(ArcBegin(centerX, centerY));
                    Keyframe(ArcEnd(right, centerY + quarterHeight));
                    Keyframe(ArcBegin(centerX, bottom));
                    Keyframe(ArcEnd(left, centerY + quarterHeight));
                    break;

                case 'T':
                    Keyframe(Initialize(left, top));
                    Keyframe(AxisLine(path.X, right));
                    Keyframe(AxisLine(path.X, centerX));
                    Keyframe(AxisLine(path.Y, bottom));
                    break;

                case 'U':
                    Keyframe(Initialize(left, top));
                    Keyframe(AxisLine(path.Y, centerY));
                    Keyframe(ArcBegin(centerX, bottom));
                    Keyframe(ArcEnd(right, centerY));
                    Keyframe(AxisLine(path.Y, top));
                    break;

                case 'V':
                    Keyframe(Initialize(left, top));
                    Keyframe(LineTo(centerX, bottom));
                    Keyframe(LineTo(right, top));
                    break;

                case 'W':
                    Keyframe(Initialize(left, top));
                    Keyframe(ArcBegin(centerX - quarterWidth, bottom));
                    Keyframe(ArcEnd(centerX, centerY));
                    Keyframe(ArcBegin(centerX + quarterWidth, bottom));
                    Keyframe(ArcEnd(right, top));
                    break;

                case 'X':
                    Keyframe(Initialize(left, top));
                    Keyframe(ArcBegin(centerX, centerY));
                    Keyframe(ArcEnd(right, bottom));
                    Keyframe(WarpTo(right, top));
                    Keyframe(ArcBegin(centerX, centerY));
                    Keyframe(ArcEnd(left, bottom));
                    break;

                case 'Y':
                    Keyframe(Initialize(left, top));
                    Keyframe(ArcBegin(centerX, centerY));
                    Keyframe(ArcEnd(right, top));
                    Keyframe(WarpTo(centerX, centerY));
                    Keyframe(AxisLine(path.Y, bottom));
                    break;

                case 'Z':
                    Keyframe(Initialize(left, top));
                    Keyframe(AxisLine(path.X, right));
                    Keyframe(LineTo(left, bottom));
                    Keyframe(AxisLine(path.X, right));
                    break;

                case 'a':
                    Keyframe(Initialize(right, lowercaseTop));
                    Keyframe(AxisLine(path.Y, bottom));
                    LowercaseCircleMacro();
                    break;

                case 'b':
                    Keyframe(Initialize(left, top));
                    Keyframe(AxisLine(path.Y, bottom));
                    LowercaseCircleMacro();
                    break;

                case 'c':
                    Keyframe(Initialize(right, lowercaseVerticalCenter, false));
                    Keyframe(DrawPercentOf(ArcBegin(centerX, lowercaseTop), 0.5f, 1f));
                    Keyframe(ArcEnd(left, lowercaseVerticalCenter));
                    Keyframe(ArcBegin(centerX, bottom));
                    Keyframe(DrawPercentOf(ArcEnd(right, lowercaseVerticalCenter), 0f, 0.5f));
                    break;

                case 'd':
                    Keyframe(Initialize(right, top));
                    Keyframe(AxisLine(path.Y, bottom));
                    Keyframe(WarpTo(centerX, lowercaseTop));
                    Keyframe(ArcEnd(left, lowercaseVerticalCenter));
                    Keyframe(ArcBegin(centerX, bottom));
                    Keyframe(ArcEnd(right, lowercaseVerticalCenter));
                    Keyframe(ArcBegin(centerX, lowercaseTop));
                    break;

                case 'e':
                    Keyframe(Initialize(left, eCrossHeight));
                    Keyframe(AxisLine(path.X, right));
                    Keyframe(ArcBegin(centerX, lowercaseTop));
                    Keyframe(ArcEnd(left, eCrossHeight));
                    Keyframe(ArcBegin(centerX, bottom));
                    Keyframe(DrawPercentOf(ArcEnd(right, eCrossHeight), 0f, 0.5f));
                    break;

                case 'f':
                    Keyframe(Initialize(right, centerY - quarterHeight));
                    Keyframe(ArcBegin(centerX + quarterWidth, top));
                    Keyframe(ArcEnd(centerX, centerY - quarterHeight));
                    Keyframe(AxisLine(path.Y, bottom));
                    Keyframe(WarpTo(centerX - quarterWidth, centerY));
                    Keyframe(AxisLine(path.X, centerX + quarterWidth));
                    break;

                case 'g':
                    Keyframe(Initialize(right, centerY - quarterHeight));
                    LowercaseCircleMacro(-quarterHeight);
                    WarpTo(right, centerY - quarterHeight);
                    Keyframe(AxisLine(path.Y, bottom));
                    Keyframe(ArcEnd(left, centerY + quarterHeight + quarterHeight / 2));
                    break;

                case 'h':
                    Keyframe(Initialize(left, top));
                    Keyframe(AxisLine(path.Y, bottom));
                    Keyframe(WarpTo(left, armHeight));
                    Keyframe(ArcBegin(centerX, lowercaseTop));
                    Keyframe(ArcEnd(right, armHeight));
                    Keyframe(AxisLine(path.Y, bottom));
                    break;

                case 'i':
                    Keyframe(Initialize(centerX, lowercaseTop));
                    Keyframe(AxisLine(path.Y, lowercaseTop + sixteenthHeight));
                    Keyframe(WarpTo(centerX, centerY + quarterHeight));
                    Keyframe(AxisLine(path.Y, bottom));
                    break;

                case 'j':
                    Keyframe(Initialize(centerX + quarterWidth, top));
                    Keyframe(AxisLine(path.Y, top + sixteenthHeight));
                    Keyframe(WarpTo(centerX + quarterWidth, centerY - quarterHeight));
                    Keyframe(AxisLine(path.Y, centerY + quarterHeight));
                    Keyframe(ArcBegin(centerX, bottom));
                    Keyframe(ArcEnd(centerX - quarterWidth, centerY + quarterHeight));
                    break;

                case 'k':
                    Keyframe(Initialize(left, top));
                    Keyframe(AxisLine(path.Y, bottom));
                    Keyframe(AxisLine(path.Y, centerY));
                    Keyframe(DrawPercentOf(ArcEnd(right, top), 0, 0.75f));
                    Keyframe(WarpTo(left, centerY));
                    Keyframe(ArcEnd(right, bottom));
                    break;

                case 'l':
                    Keyframe(Initialize(centerX, top));
                    Keyframe(AxisLine(path.Y, bottom));
                    break;

                case 'm':
                    Keyframe(Initialize(left, lowercaseTop));
                    Keyframe(AxisLine(path.Y, bottom));
                    Keyframe(WarpTo(left, armHeight));
                    Keyframe(ArcBegin(centerX - quarterWidth, lowercaseTop));
                    Keyframe(ArcEnd(centerX, armHeight));
                    Keyframe(ArcBegin(centerX + quarterWidth, lowercaseTop));
                    Keyframe(ArcEnd(right, armHeight));
                    Keyframe(AxisLine(path.Y, bottom));
                    break;

                case 'n':
                    Keyframe(Initialize(left, lowercaseTop));
                    Keyframe(AxisLine(path.Y, bottom));
                    Keyframe(WarpTo(left, armHeight));
                    Keyframe(ArcBegin(centerX, lowercaseTop));
                    Keyframe(ArcEnd(right, armHeight));
                    Keyframe(AxisLine(path.Y, bottom));
                    break;

                case 'o':
                    Keyframe(Initialize(right, eCrossHeight));
                    LowercaseCircleMacro();
                    break;

                case 'p':
                    Keyframe(Initialize(right, eCrossHeight));
                    LowercaseCircleMacro(-quarterHeight);
                    Keyframe(WarpTo(left, centerY - quarterHeight));
                    Keyframe(AxisLine(path.Y, bottom));
                    break;

                case 'q':
                    Keyframe(Initialize(right, eCrossHeight));
                    LowercaseCircleMacro(-quarterHeight);
                    Keyframe(WarpTo(right, centerY - quarterHeight));
                    Keyframe(AxisLine(path.Y, bottom));
                    break;

                case 'r':
                    Keyframe(Initialize(left, lowercaseTop));
                    Keyframe(AxisLine(path.Y, bottom));
                    Keyframe(WarpTo(left, armHeight));
                    Keyframe(ArcBegin(centerX, lowercaseTop));
                    Keyframe(ArcEnd(right, armHeight));
                    break;

                case 's':
                    Keyframe(Initialize(right, lowercaseVerticalCenter - quarterHeight / 2));
                    Keyframe(ArcBegin(centerX, lowercaseTop));
                    Keyframe(ArcEnd(left, lowercaseVerticalCenter - quarterHeight / 2));
                    Keyframe(ArcBegin(centerX, lowercaseVerticalCenter));
                    Keyframe(ArcEnd(right, lowercaseVerticalCenter + quarterHeight / 2));
                    Keyframe(ArcBegin(centerX, bottom));
                    Keyframe(ArcEnd(left, lowercaseVerticalCenter + quarterHeight / 2));
                    break;

                case 't':
                    Keyframe(Initialize(centerX, top));
                    Keyframe(AxisLine(path.Y, bottom));
                    Keyframe(WarpTo(centerX - quarterWidth, centerY));
                    Keyframe(AxisLine(path.X, centerX + quarterWidth));
                    break;

                case 'u':
                    Keyframe(Initialize(left, lowercaseTop));
                    Keyframe(AxisLine(path.Y, lowercaseVerticalCenter));
                    Keyframe(ArcBegin(centerX, bottom));
                    Keyframe(ArcEnd(right, lowercaseVerticalCenter));
                    Keyframe(AxisLine(path.Y, lowercaseTop));
                    break;

                case 'v':
                    Keyframe(Initialize(left, lowercaseTop));
                    Keyframe(LineTo(centerX, bottom));
                    Keyframe(LineTo(right, lowercaseTop));
                    break;

                case 'w':
                    Keyframe(Initialize(left, lowercaseTop));
                    Keyframe(ArcBegin(left / 2, bottom));
                    Keyframe(ArcEnd(centerX, lowercaseTop));
                    Keyframe(ArcBegin(right / 2, bottom));
                    Keyframe(ArcEnd(right, lowercaseTop));
                    break;

                case 'x':
                    Keyframe(Initialize(left, lowercaseTop));
                    Keyframe(ArcBegin(centerX, lowercaseVerticalCenter));
                    Keyframe(ArcEnd(right, bottom));
                    Keyframe(WarpTo(right, lowercaseTop));
                    Keyframe(ArcBegin(centerX, lowercaseVerticalCenter));
                    Keyframe(ArcEnd(left, bottom));
                    break;

                // case y:

                case 'z':
                    Keyframe(Initialize(left, lowercaseTop));
                    Keyframe(AxisLine(path.X, right));
                    Keyframe(LineTo(left, bottom));
                    Keyframe(AxisLine(path.X, right));
                    break;

                case '!':
                    Keyframe(Initialize(centerX, top));
                    Keyframe(DrawPercentOf(AxisLine(path.Y, bottom), 0f, 0.60f));
                    Keyframe(Enable());
                    Keyframe(AxisLine(path.Y, bottom * 0.90f));
                    break;

                default:
                    // By default we draw a circle
                    Keyframe(SetXY(right, centerY));
                    Keyframe(ArcBegin(centerX, bottom));
                    Keyframe(ArcEnd(left, centerY));
                    Keyframe(ArcBegin(centerX, top));
                    Keyframe(ArcEnd(right, centerY));
                    break;
            }

            // Add final keyframe
            path.AddKeyframe(path.Duration);

            path.X.Value = 0;
            path.Y.Value = 0;
            path.BakeKeyframes(0);
            return new TweenGlyph(path, this, letter);
        }

        public FloatXyPair CharacterSize(char _)
        {
            // monospaced fonts always return the same character size
            return new FloatXyPair(FontSize / 2, FontSize);
        }
    }
}
