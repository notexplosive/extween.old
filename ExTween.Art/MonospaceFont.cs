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
                        path.X.Value = x;
                        path.Y.Value = y;
                    });
            }

            ITween AxisLine(Tweenable<float> tweenable, float destination)
            {
                return new Tween<float>(tweenable, destination, duration, Ease.Linear);
            }

            ITween ArcA(float x, float y)
            {
                return new MultiplexTween()
                    .AddChannel(new Tween<float>(path.X, x, duration, Ease.SineSlowFast))
                    .AddChannel(new Tween<float>(path.Y, y, duration, Ease.SineFastSlow));
            }

            ITween ArcB(float x, float y)
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
                return new CallbackTween(() => { path.ShouldDraw.Value = 1; });
            }

            ITween Disable()
            {
                return new CallbackTween(() => { path.ShouldDraw.Value = 0; });
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
            var trueHeight = tinyFont.CharacterSize(letter).Y;
            var sixthTrueHeight = trueHeight / 6;
            var thirdTrueHeight = trueHeight / 3;
            var effectiveHeight = trueHeight * 2 / 3;

            var halfWidth = width / 2;
            var quarterWidth = halfWidth / 2;
            var eightWidth = quarterWidth / 2;
            
            var halfHeight = effectiveHeight / 2;
            var quarterHeight = halfHeight / 2;
            var eightHeight = quarterHeight / 2;
            var sixteenthHeight = quarterHeight / 4;

            var centerX = 0;
            var centerY = -sixthTrueHeight;
            var left = centerX - halfWidth;
            var right = centerX + halfWidth;
            var top = centerY - halfHeight;
            var bottom = centerY + halfHeight;
            var lowercaseTop = centerY; // subtracting `sixthTrueHeight` here makes it look interesting
            var lowercaseBottom = trueHeight / 2;

            // y position that the lowercase 'r' arm juts out
            var armHeight = lowercaseTop + halfHeight * 0.35f;
            // y position of the horizontal line in lowercase 'e'
            var eCrossHeight = centerY + quarterHeight;
            var lowercaseVerticalCenter = centerY + quarterHeight;

            void LowercaseCircleMacro()
            {
                Keyframe(WarpTo(left, lowercaseTop + quarterHeight));
                Keyframe(ArcA(centerX, lowercaseTop));
                Keyframe(ArcB(right, lowercaseTop + quarterHeight));
                Keyframe(ArcA(centerX, bottom));
                Keyframe(ArcB(left, lowercaseTop + quarterHeight));
            }

            switch (letter)
            {
                case 'A':
                    Keyframe(Initialize(left, bottom));
                    Keyframe(AxisLine(path.Y, centerY));
                    Keyframe(ArcA(centerX, top));
                    Keyframe(ArcB(right, centerY));
                    Keyframe(AxisLine(path.Y, bottom));
                    Keyframe(WarpTo(left, centerY));
                    Keyframe(AxisLine(path.X, right));
                    break;

                case 'B':
                    Keyframe(Initialize(left, bottom));
                    Keyframe(AxisLine(path.Y, top));
                    Keyframe(ArcB(centerX + quarterWidth, centerY - quarterHeight));
                    Keyframe(ArcA(left, centerY));
                    Keyframe(ArcB(right, centerY + quarterHeight));
                    Keyframe(ArcA(left, bottom));
                    break;

                case 'C':
                    Keyframe(Initialize(right, centerY - quarterHeight));
                    Keyframe(ArcA(centerX, top));
                    Keyframe(ArcB(left, centerY));
                    Keyframe(ArcA(centerX, bottom));
                    Keyframe(ArcB(right, centerY + quarterHeight));
                    break;

                case 'D':
                    Keyframe(Initialize(left, top));
                    Keyframe(AxisLine(path.Y, bottom));
                    Keyframe(ArcB(right, centerY));
                    Keyframe(ArcA(left, top));
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
                    Keyframe(ArcA(centerX, top));
                    Keyframe(ArcB(left, centerY));
                    Keyframe(ArcA(centerX, bottom));
                    Keyframe(ArcB(right, centerY));
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
                    Keyframe(ArcA(centerX, bottom));
                    Keyframe(ArcB(left, centerY + quarterHeight));
                    break;

                case 'K':
                    Keyframe(Initialize(left, top));
                    Keyframe(AxisLine(path.Y, bottom));
                    Keyframe(AxisLine(path.Y, centerY));
                    Keyframe(ArcB(right, top));
                    Keyframe(WarpTo(left, centerY));
                    Keyframe(ArcB(right, bottom));
                    break;

                case 'L':
                    Keyframe(Initialize(left, top));
                    Keyframe(AxisLine(path.Y, bottom));
                    Keyframe(AxisLine(path.X, right));
                    break;

                case 'M':
                    Keyframe(Initialize(left, bottom));
                    Keyframe(AxisLine(path.Y, top));
                    Keyframe(ArcB(centerX, centerY));
                    Keyframe(ArcA(right, top));
                    Keyframe(AxisLine(path.Y, bottom));
                    break;

                case 'N':
                    Keyframe(Initialize(left, bottom));
                    Keyframe(AxisLine(path.Y, top));
                    Keyframe(ArcB(right, bottom));
                    Keyframe(AxisLine(path.Y, top));
                    break;

                case 'O':
                    Keyframe(Initialize(right, centerY));
                    Keyframe(ArcA(centerX, bottom));
                    Keyframe(ArcB(left, centerY));
                    Keyframe(ArcA(centerX, top));
                    Keyframe(ArcB(right, centerY));
                    break;

                case 'P':
                    Keyframe(Initialize(left, bottom));
                    Keyframe(AxisLine(path.Y, top));
                    Keyframe(ArcB(right, centerY - quarterHeight));
                    Keyframe(ArcA(left, centerY));
                    break;

                case 'Q':
                    Keyframe(Initialize(right, centerY));
                    Keyframe(ArcA(centerX, bottom));
                    Keyframe(ArcB(left, centerY));
                    Keyframe(ArcA(centerX, top));
                    Keyframe(ArcB(right, centerY));
                    Keyframe(WarpTo(centerX, centerY + quarterHeight));
                    Keyframe(ArcB(right, bottom));
                    break;

                case 'R':
                    Keyframe(Initialize(left, bottom));
                    Keyframe(AxisLine(path.Y, top));
                    Keyframe(ArcB(right, centerY - quarterHeight));
                    Keyframe(ArcA(left, centerY));
                    Keyframe(ArcB(right, bottom));
                    break;

                case 'S':
                    Keyframe(Initialize(right, centerY - quarterHeight));
                    Keyframe(ArcA(centerX, top));
                    Keyframe(ArcB(left, centerY - quarterHeight));
                    Keyframe(ArcA(centerX, centerY));
                    Keyframe(ArcB(right, centerY + quarterHeight));
                    Keyframe(ArcA(centerX, bottom));
                    Keyframe(ArcB(left, centerY + quarterHeight));
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
                    Keyframe(ArcA(centerX, bottom));
                    Keyframe(ArcB(right, centerY));
                    Keyframe(AxisLine(path.Y, top));
                    break;

                case 'V':
                    Keyframe(Initialize(left, top));
                    Keyframe(LineTo(centerX, bottom));
                    Keyframe(LineTo(right, top));
                    break;

                case 'W':
                    Keyframe(Initialize(left, top));
                    Keyframe(ArcA(centerX - quarterWidth, bottom));
                    Keyframe(ArcB(centerX, centerY));
                    Keyframe(ArcA(centerX + quarterWidth, bottom));
                    Keyframe(ArcB(right, top));
                    break;

                case 'X':
                    Keyframe(Initialize(left, top));
                    Keyframe(ArcA(centerX, centerY));
                    Keyframe(ArcB(right, bottom));
                    Keyframe(WarpTo(right, top));
                    Keyframe(ArcA(centerX, centerY));
                    Keyframe(ArcB(left, bottom));
                    break;

                case 'Y':
                    Keyframe(Initialize(left, top));
                    Keyframe(ArcA(centerX, centerY));
                    Keyframe(ArcB(right, top));
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
                    Keyframe(DrawPercentOf(ArcA(centerX, lowercaseTop), 0.5f, 1f));
                    Keyframe(ArcB(left, lowercaseVerticalCenter));
                    Keyframe(ArcA(centerX, bottom));
                    Keyframe(DrawPercentOf(ArcB(right, lowercaseVerticalCenter), 0f, 0.5f));
                    break;

                case 'd':
                    Keyframe(Initialize(right, top));
                    Keyframe(AxisLine(path.Y, bottom));
                    Keyframe(WarpTo(centerX, lowercaseTop));
                    Keyframe(ArcB(left, lowercaseVerticalCenter));
                    Keyframe(ArcA(centerX, bottom));
                    Keyframe(ArcB(right, lowercaseVerticalCenter));
                    Keyframe(ArcA(centerX, lowercaseTop));
                    break;

                case 'e':
                    Keyframe(Initialize(left, eCrossHeight));
                    Keyframe(AxisLine(path.X, right));
                    Keyframe(ArcA(centerX, lowercaseTop));
                    Keyframe(ArcB(left, eCrossHeight));
                    Keyframe(ArcA(centerX, bottom));
                    Keyframe(DrawPercentOf(ArcB(right, eCrossHeight), 0f, 0.5f));
                    break;

                case 'f':
                    Keyframe(Initialize(right, centerY - quarterHeight));
                    Keyframe(ArcA(centerX + quarterWidth, top));
                    Keyframe(ArcB(centerX, centerY - quarterHeight));
                    Keyframe(AxisLine(path.Y, bottom));
                    Keyframe(WarpTo(centerX - quarterWidth, centerY));
                    Keyframe(AxisLine(path.X, centerX + quarterWidth));
                    break;

                case 'g':
                    Keyframe(Initialize(right, centerY - quarterHeight));
                    LowercaseCircleMacro();
                    Keyframe(WarpTo(right, lowercaseTop));
                    Keyframe(AxisLine(path.Y, lowercaseBottom - quarterHeight));
                    Keyframe(ArcA(centerX, lowercaseBottom));
                    Keyframe(ArcB(left, lowercaseBottom - quarterHeight));
                    break;

                case 'h':
                    Keyframe(Initialize(left, top));
                    Keyframe(AxisLine(path.Y, bottom));
                    Keyframe(WarpTo(left, armHeight));
                    Keyframe(ArcA(centerX, lowercaseTop));
                    Keyframe(ArcB(right, armHeight));
                    Keyframe(AxisLine(path.Y, bottom));
                    break;

                case 'i':
                    Keyframe(Initialize(centerX, lowercaseTop - halfHeight));
                    Keyframe(AxisLine(path.Y, lowercaseTop - quarterHeight));
                    Keyframe(WarpTo(centerX, lowercaseTop));
                    Keyframe(AxisLine(path.Y, bottom));
                    break;

                case 'j':
                    Keyframe(Initialize(right, lowercaseTop - halfHeight));
                    Keyframe(AxisLine(path.Y, lowercaseTop - quarterHeight));
                    Keyframe(WarpTo(right, lowercaseTop));
                    Keyframe(AxisLine(path.Y, lowercaseBottom - quarterHeight));
                    Keyframe(ArcA(centerX, lowercaseBottom));
                    Keyframe(ArcB(left, lowercaseBottom - quarterHeight));
                    break;

                case 'k':
                    Keyframe(Initialize(left, top));
                    Keyframe(AxisLine(path.Y, bottom));
                    Keyframe(AxisLine(path.Y, centerY));
                    Keyframe(DrawPercentOf(ArcB(right, top), 0, 0.75f));
                    Keyframe(WarpTo(left, centerY));
                    Keyframe(ArcB(right, bottom));
                    break;

                case 'l':
                    Keyframe(Initialize(centerX, top));
                    Keyframe(AxisLine(path.Y, bottom));
                    break;

                case 'm':
                    Keyframe(Initialize(left, lowercaseTop));
                    Keyframe(AxisLine(path.Y, bottom));
                    Keyframe(WarpTo(left, armHeight));
                    Keyframe(ArcA(centerX - quarterWidth, lowercaseTop));
                    Keyframe(ArcB(centerX, armHeight));
                    Keyframe(ArcA(centerX + quarterWidth, lowercaseTop));
                    Keyframe(ArcB(right, armHeight));
                    Keyframe(AxisLine(path.Y, bottom));
                    break;

                case 'n':
                    Keyframe(Initialize(left, lowercaseTop));
                    Keyframe(AxisLine(path.Y, bottom));
                    Keyframe(WarpTo(left, armHeight));
                    Keyframe(ArcA(centerX, lowercaseTop));
                    Keyframe(ArcB(right, armHeight));
                    Keyframe(AxisLine(path.Y, bottom));
                    break;

                case 'o':
                    Keyframe(Initialize(right, eCrossHeight));
                    LowercaseCircleMacro();
                    break;

                case 'p':
                    Keyframe(Initialize(right, eCrossHeight));
                    LowercaseCircleMacro();
                    Keyframe(WarpTo(left, lowercaseTop));
                    Keyframe(AxisLine(path.Y, lowercaseBottom));
                    break;

                case 'q':
                    Keyframe(Initialize(right, eCrossHeight));
                    LowercaseCircleMacro();
                    Keyframe(WarpTo(right, lowercaseTop));
                    Keyframe(AxisLine(path.Y, lowercaseBottom));
                    break;

                case 'r':
                    Keyframe(Initialize(left, lowercaseTop));
                    Keyframe(AxisLine(path.Y, bottom));
                    Keyframe(WarpTo(left, armHeight));
                    Keyframe(ArcA(centerX, lowercaseTop));
                    Keyframe(ArcB(right, armHeight));
                    break;

                case 's':
                    Keyframe(Initialize(right, lowercaseVerticalCenter - quarterHeight / 2));
                    Keyframe(ArcA(centerX, lowercaseTop));
                    Keyframe(ArcB(left, lowercaseVerticalCenter - quarterHeight / 2));
                    Keyframe(ArcA(centerX, lowercaseVerticalCenter));
                    Keyframe(ArcB(right, lowercaseVerticalCenter + quarterHeight / 2));
                    Keyframe(ArcA(centerX, bottom));
                    Keyframe(ArcB(left, lowercaseVerticalCenter + quarterHeight / 2));
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
                    Keyframe(ArcA(centerX, bottom));
                    Keyframe(ArcB(right, lowercaseVerticalCenter));
                    Keyframe(AxisLine(path.Y, lowercaseTop));
                    break;

                case 'v':
                    Keyframe(Initialize(left, lowercaseTop));
                    Keyframe(LineTo(centerX, bottom));
                    Keyframe(LineTo(right, lowercaseTop));
                    break;

                case 'w':
                    Keyframe(Initialize(left, lowercaseTop));
                    Keyframe(ArcA(left / 2, bottom));
                    Keyframe(ArcB(centerX, lowercaseTop));
                    Keyframe(ArcA(right / 2, bottom));
                    Keyframe(ArcB(right, lowercaseTop));
                    break;

                case 'x':
                    Keyframe(Initialize(left, lowercaseTop));
                    Keyframe(ArcA(centerX, lowercaseVerticalCenter));
                    Keyframe(ArcB(right, bottom));
                    Keyframe(WarpTo(right, lowercaseTop));
                    Keyframe(ArcA(centerX, lowercaseVerticalCenter));
                    Keyframe(ArcB(left, bottom));
                    break;

                case 'y':
                    Keyframe(Initialize(left, lowercaseTop));
                    Keyframe(AxisLine(path.Y, lowercaseVerticalCenter));
                    Keyframe(ArcA(centerX, bottom));
                    Keyframe(ArcB(right, lowercaseVerticalCenter));
                    Keyframe(WarpTo(right, lowercaseTop));
                    Keyframe(AxisLine(path.Y, lowercaseBottom - quarterHeight));
                    Keyframe(ArcA(centerX, lowercaseBottom));
                    Keyframe(ArcB(left, lowercaseBottom - quarterHeight));
                    break;

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

                case '\"':
                    Keyframe(Initialize(centerX - quarterWidth, top));
                    Keyframe(AxisLine(path.Y, centerY - quarterHeight));
                    Keyframe(WarpTo(centerX + quarterWidth, top));
                    Keyframe(AxisLine(path.Y, centerY - quarterHeight));
                    break;

                case '#':
                    Keyframe(Initialize(centerX - quarterWidth, top));
                    Keyframe(AxisLine(path.Y, bottom));
                    Keyframe(WarpTo(centerX + quarterWidth, top));
                    Keyframe(AxisLine(path.Y, bottom));
                    Keyframe(WarpTo(left, centerY + quarterHeight));
                    Keyframe(AxisLine(path.X, right));
                    Keyframe(WarpTo(left, centerY - quarterHeight));
                    Keyframe(AxisLine(path.X, right));
                    break;

                case '$':
                    Keyframe(Initialize(right, centerY - quarterHeight));
                    Keyframe(ArcA(centerX, top + sixteenthHeight));
                    Keyframe(ArcB(left, centerY - quarterHeight));
                    Keyframe(ArcA(centerX, centerY));
                    Keyframe(ArcB(right, centerY + quarterHeight));
                    Keyframe(ArcA(centerX, bottom - sixteenthHeight));
                    Keyframe(ArcB(left, centerY + quarterHeight));
                    Keyframe(WarpTo(centerX, top));
                    Keyframe(AxisLine(path.Y, bottom));
                    break;

                case '%':
                    Keyframe(Initialize(right, top));
                    Keyframe(LineTo(left, bottom));
                    
                    Keyframe(WarpTo(centerX - quarterWidth, top));
                    Keyframe(ArcB(left, centerY - quarterHeight));
                    Keyframe(ArcA(centerX - quarterWidth, centerY));
                    Keyframe(ArcB(centerX, centerY - quarterHeight));
                    Keyframe(ArcA(centerX - quarterWidth, top));
                    
                    
                    Keyframe(WarpTo(centerX + quarterWidth, centerY));
                    Keyframe(ArcB(right, centerY + quarterHeight));
                    Keyframe(ArcA(centerX + quarterWidth, bottom));
                    Keyframe(ArcB(centerX, centerY + quarterHeight));
                    Keyframe(ArcA(centerX + quarterWidth, centerY));
                    
                    
                    break;

                case '&':
                    Keyframe(Initialize(centerX + quarterWidth, centerY - quarterHeight));
                    Keyframe(ArcA(centerX, top));
                    Keyframe(ArcB(left, centerY - quarterHeight));
                    Keyframe(LineTo(right, bottom));
                    Keyframe(WarpTo(centerX, centerY));
                    Keyframe(ArcB(left, bottom - quarterHeight));
                    Keyframe(ArcA(centerX, bottom));
                    Keyframe(ArcB(right, centerY + quarterHeight));
                    break;

                case '\'':
                    Keyframe(Initialize(centerX, top));
                    Keyframe(AxisLine(path.Y, centerY - quarterHeight));
                    break;

                case '(':
                    Keyframe(Initialize(centerX, top));
                    Keyframe(ArcB(left, centerY));
                    Keyframe(ArcA(centerX, bottom));
                    break;

                case ')':
                    Keyframe(Initialize(centerX, top));
                    Keyframe(ArcB(right, centerY));
                    Keyframe(ArcA(centerX, bottom));
                    break;

                case '*':
                    Keyframe(Initialize(centerX, top));
                    Keyframe(LineTo(centerX, bottom));
                    Keyframe(WarpTo(centerX, centerY));
                    Keyframe(LineTo(left, centerY - quarterHeight));
                    Keyframe(WarpTo(centerX, centerY));
                    Keyframe(LineTo(right, centerY - quarterHeight));
                    Keyframe(WarpTo(centerX, centerY));
                    Keyframe(LineTo(left, centerY + quarterHeight));
                    Keyframe(WarpTo(centerX, centerY));
                    Keyframe(LineTo(right, centerY + quarterHeight));
                    break;

                case '+':
                    Keyframe(Initialize(centerX, top));
                    Keyframe(AxisLine(path.Y, bottom));
                    Keyframe(WarpTo(left, centerY));
                    Keyframe(AxisLine(path.X, right));
                    break;

                case ',':
                    Keyframe(Initialize(centerX, bottom));
                    Keyframe(ArcA(centerX - quarterWidth, lowercaseBottom));
                    break;

                case '-':
                    Keyframe(Initialize(left, centerY));
                    Keyframe(AxisLine(path.X, right));
                    break;

                case '.':
                    Keyframe(Initialize(centerX, bottom));
                    Keyframe(LineTo(centerX, bottom - sixteenthHeight));
                    break;

                case '/':
                    Keyframe(Initialize(right, top));
                    Keyframe(LineTo(left, bottom));
                    break;

                case ':':
                    Keyframe(Initialize(centerX, centerY - quarterHeight));
                    Keyframe(LineTo(centerX, centerY - quarterHeight - sixteenthHeight));
                    Keyframe(WarpTo(centerX, bottom));
                    Keyframe(LineTo(centerX, bottom - sixteenthHeight));
                    break;

                case ';':
                    Keyframe(Initialize(centerX, centerY - quarterHeight));
                    Keyframe(LineTo(centerX, centerY - quarterHeight - sixteenthHeight));
                    Keyframe(WarpTo(centerX, bottom));
                    Keyframe(ArcA(centerX - quarterWidth, lowercaseBottom));
                    break;

                case '<':
                    Keyframe(Initialize(right, top));
                    Keyframe(LineTo(left, centerY));
                    Keyframe(LineTo(right, bottom));
                    break;

                case '=':
                    Keyframe(Initialize(left, centerY - quarterHeight));
                    Keyframe(AxisLine(path.X, right));
                    Keyframe(WarpTo(left, centerY + quarterHeight));
                    Keyframe(AxisLine(path.X, right));
                    break;

                case '>':
                    Keyframe(Initialize(left, top));
                    Keyframe(LineTo(right, centerY));
                    Keyframe(LineTo(left, bottom));
                    break;

                case '?':
                    Keyframe(Initialize(left, centerY - quarterHeight));
                    Keyframe(ArcA(centerX, top));
                    Keyframe(ArcB(right, centerY - quarterHeight));
                    Keyframe(ArcA(centerX + quarterWidth, centerY));
                    Keyframe(ArcB(centerX, centerY + sixteenthHeight * 2));
                    Keyframe(WarpTo(centerX, bottom));
                    Keyframe(LineTo(centerX, bottom - sixteenthHeight));
                    break;

                case '@':
                    Keyframe(Initialize(right, centerY));
                    Keyframe(ArcA(centerX + quarterWidth, centerY - quarterHeight));
                    Keyframe(ArcB(centerX, centerY));
                    Keyframe(ArcA(centerX + quarterWidth, centerY + quarterHeight));
                    Keyframe(ArcB(right, centerY));
                    Keyframe(ArcA(centerX, top));
                    Keyframe(ArcB(left, centerY));
                    Keyframe(ArcA(left, centerY + quarterHeight));
                    Keyframe(ArcA(centerX, bottom));
                    break;

                case '[':
                    Keyframe(Initialize(centerX, top));
                    Keyframe(AxisLine(path.X, left));
                    Keyframe(AxisLine(path.Y, bottom));
                    Keyframe(AxisLine(path.X, centerX));
                    break;

                case '\\':
                    Keyframe(Initialize(left, top));
                    Keyframe(LineTo(right, bottom));
                    break;

                case ']':
                    Keyframe(Initialize(centerX, top));
                    Keyframe(AxisLine(path.X, right));
                    Keyframe(AxisLine(path.Y, bottom));
                    Keyframe(AxisLine(path.X, centerX));
                    break;

                case '^':
                    Keyframe(Initialize(centerX - quarterWidth, centerY - quarterHeight));
                    Keyframe(LineTo(centerX, top));
                    Keyframe(LineTo(centerX + quarterWidth, centerY - quarterHeight));
                    break;

                case '`':
                    Keyframe(Initialize(centerX - quarterWidth, top));
                    Keyframe(LineTo(centerX, centerY - quarterHeight));
                    break;

                case '_':
                    Keyframe(Initialize(left, bottom));
                    Keyframe(AxisLine(path.X, right));
                    break;

                case '{':
                    Keyframe(Initialize(centerX, top));
                    Keyframe(ArcB(centerX - quarterWidth, centerY - quarterHeight));
                    Keyframe(ArcA(left, centerY));
                    Keyframe(ArcB(centerX - quarterWidth, centerY + quarterHeight));
                    Keyframe(ArcA(centerX, bottom));
                    break;

                case '|':
                    Keyframe(Initialize(centerX, top));
                    Keyframe(AxisLine(path.Y, bottom));
                    break;

                case '}':
                    Keyframe(Initialize(centerX, top));
                    Keyframe(ArcB(centerX + quarterWidth, centerY - quarterHeight));
                    Keyframe(ArcA(right, centerY));
                    Keyframe(ArcB(centerX + quarterWidth, centerY + quarterHeight));
                    Keyframe(ArcA(centerX, bottom));
                    break;

                case '~':
                    Keyframe(Initialize(left, centerY + sixteenthHeight));
                    Keyframe(ArcA(centerX - quarterWidth, centerY - sixteenthHeight));
                    Keyframe(ArcB(centerX, centerY));
                    Keyframe(ArcA(centerX + quarterWidth, centerY + sixteenthHeight));
                    Keyframe(ArcB(right, centerY - sixteenthHeight));
                    break;

                case '0':
                    Keyframe(Initialize(right, centerY));
                    Keyframe(ArcA(centerX, bottom));
                    Keyframe(ArcB(left, centerY));
                    Keyframe(ArcA(centerX, top));
                    Keyframe(ArcB(right, centerY));
                    break;
                
                case '1':
                    Keyframe(Initialize(left, centerY - quarterHeight));
                    Keyframe(ArcB(centerX, top));
                    Keyframe(AxisLine(path.Y, bottom));
                    break;

                case '2':
                    Keyframe(Initialize(left, centerY - quarterHeight));
                    Keyframe(ArcA(centerX, top));
                    Keyframe(ArcB(right, centerY - quarterHeight));
                    Keyframe(ArcA(centerX, centerY));
                    Keyframe(ArcB(left, bottom));
                    Keyframe(AxisLine(path.X, right));
                    break;

                case '3':
                    Keyframe(Initialize(left, centerY - quarterHeight));
                    Keyframe(ArcA(centerX, top));
                    Keyframe(ArcB(right, centerY - quarterHeight));
                    Keyframe(ArcA(centerX, centerY));
                    Keyframe(ArcB(right, centerY + quarterHeight));
                    Keyframe(ArcA(centerX, bottom));
                    Keyframe(ArcB(left, centerY + quarterHeight));
                    break;

                case '4':
                    Keyframe(Initialize(centerX + quarterWidth, top));
                    Keyframe(LineTo(left, centerY + quarterHeight));
                    Keyframe(AxisLine(path.X, right));
                    Keyframe(WarpTo(right - quarterWidth, top));
                    Keyframe(AxisLine(path.Y, bottom));
                    break;
                
                case '5':
                    Keyframe(Initialize(right, top));
                    Keyframe(AxisLine(path.X, left));
                    Keyframe(AxisLine(path.Y, centerY - sixteenthHeight));
                    Keyframe(AxisLine(path.X, centerX + quarterWidth));
                    Keyframe(ArcB(right, centerY + quarterHeight));
                    Keyframe(ArcA(centerX, bottom));
                    Keyframe(ArcB(left, centerY + quarterHeight));
                    break;
                
                case '6':
                    Keyframe(Initialize(right, centerY - quarterHeight));
                    Keyframe(ArcA(centerX, top));
                    Keyframe(ArcB(left, centerY - quarterHeight));
                    Keyframe(AxisLine(path.Y, lowercaseVerticalCenter));
                    LowercaseCircleMacro();
                    break;
                
                case '7':
                    Keyframe(Initialize(left, top));
                    Keyframe(AxisLine(path.X, right));
                    Keyframe(LineTo(centerX, bottom));
                    break;
                
                case '8':
                    Keyframe(Initialize(centerX, centerY));
                    Keyframe(ArcB(left, centerY - quarterHeight));
                    Keyframe(ArcA(centerX, top));
                    Keyframe(ArcB(right, centerY - quarterHeight));
                    Keyframe(ArcA(centerX, centerY));
                    Keyframe(ArcB(left, centerY + quarterHeight));
                    Keyframe(ArcA(centerX, bottom));
                    Keyframe(ArcB(right, centerY + quarterHeight));
                    Keyframe(ArcA(centerX, centerY));
                    break;
                
                case '9':
                    Keyframe(Initialize(centerX, centerY));
                    Keyframe(ArcB(left, centerY - quarterHeight));
                    Keyframe(ArcA(centerX, top));
                    Keyframe(ArcB(right, centerY - quarterHeight));
                    Keyframe(ArcA(centerX, centerY));
                    Keyframe(WarpTo(right, centerY - quarterHeight));
                    Keyframe(AxisLine(path.Y, centerY + quarterHeight));
                    Keyframe(ArcA(centerX, bottom));
                    Keyframe(ArcB(left, centerY + quarterHeight));
                    break;

                default:
                    // By default we draw a circle
                    Keyframe(SetXY(right, centerY));
                    Keyframe(ArcA(centerX, bottom));
                    Keyframe(ArcB(left, centerY));
                    Keyframe(ArcA(centerX, top));
                    Keyframe(ArcB(right, centerY));
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
