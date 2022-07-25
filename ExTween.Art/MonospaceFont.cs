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

            var builder = path.Builder;

            if (char.IsWhiteSpace(letter))
            {
                return new TweenGlyph(path, this, letter);
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
                builder.KeyframeWarpTo(left, lowercaseTop + quarterHeight);
                builder.KeyframeArcA(centerX, lowercaseTop);
                builder.KeyframeArcB(right, lowercaseTop + quarterHeight);
                builder.KeyframeArcA(centerX, bottom);
                builder.KeyframeArcB(left, lowercaseTop + quarterHeight);
            }

            switch (letter)
            {
                case 'A':
                    builder.KeyframeInitialize(left, bottom);
                    builder.KeyframeAxisLine(path.Y, centerY);
                    builder.KeyframeArcA(centerX, top);
                    builder.KeyframeArcB(right, centerY);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    builder.KeyframeWarpTo(left, centerY);
                    builder.KeyframeAxisLine(path.X, right);
                    break;

                case 'B':
                    builder.KeyframeInitialize(left, bottom);
                    builder.KeyframeAxisLine(path.Y, top);
                    builder.KeyframeArcB(centerX + quarterWidth, centerY - quarterHeight);
                    builder.KeyframeArcA(left, centerY);
                    builder.KeyframeArcB(right, centerY + quarterHeight);
                    builder.KeyframeArcA(left, bottom);
                    break;

                case 'C':
                    builder.KeyframeInitialize(right, centerY - quarterHeight);
                    builder.KeyframeArcA(centerX, top);
                    builder.KeyframeArcB(left, centerY);
                    builder.KeyframeArcA(centerX, bottom);
                    builder.KeyframeArcB(right, centerY + quarterHeight);
                    break;

                case 'D':
                    builder.KeyframeInitialize(left, top);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    builder.KeyframeArcB(right, centerY);
                    builder.KeyframeArcA(left, top);
                    break;

                case 'E':
                    builder.KeyframeInitialize(right, top);
                    builder.KeyframeAxisLine(path.X, left);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    builder.KeyframeAxisLine(path.X, right);
                    builder.KeyframeWarpTo(left, centerY);
                    builder.KeyframeAxisLine(path.X, centerX + quarterWidth);
                    break;

                case 'F':
                    builder.KeyframeInitialize(right, top);
                    builder.KeyframeAxisLine(path.X, left);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    builder.KeyframeWarpTo(left, centerY);
                    builder.KeyframeAxisLine(path.X, centerX + quarterWidth);
                    break;

                case 'G':
                    builder.KeyframeInitialize(right, centerY - quarterHeight);
                    builder.KeyframeArcA(centerX, top);
                    builder.KeyframeArcB(left, centerY);
                    builder.KeyframeArcA(centerX, bottom);
                    builder.KeyframeArcB(right, centerY);
                    builder.KeyframeAxisLine(path.X, centerX);
                    break;

                case 'H':
                    builder.KeyframeInitialize(left, top);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    builder.KeyframeWarpTo(right, top);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    builder.KeyframeWarpTo(left, centerY);
                    builder.KeyframeAxisLine(path.X, right);
                    break;

                case 'I':
                    builder.KeyframeInitialize(left, top);
                    builder.KeyframeAxisLine(path.X, right);
                    builder.KeyframeWarpTo(centerX, top);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    builder.KeyframeWarpTo(left, bottom);
                    builder.KeyframeAxisLine(path.X, right);
                    break;

                case 'J':
                    builder.KeyframeInitialize(left, top);
                    builder.KeyframeAxisLine(path.X, right);
                    builder.KeyframeAxisLine(path.Y, centerY + quarterHeight);
                    builder.KeyframeArcA(centerX, bottom);
                    builder.KeyframeArcB(left, centerY + quarterHeight);
                    break;

                case 'K':
                    builder.KeyframeInitialize(left, top);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    builder.KeyframeAxisLine(path.Y, centerY);
                    builder.KeyframeArcB(right, top);
                    builder.KeyframeWarpTo(left, centerY);
                    builder.KeyframeArcB(right, bottom);
                    break;

                case 'L':
                    builder.KeyframeInitialize(left, top);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    builder.KeyframeAxisLine(path.X, right);
                    break;

                case 'M':
                    builder.KeyframeInitialize(left, bottom);
                    builder.KeyframeAxisLine(path.Y, top);
                    builder.KeyframeArcB(centerX, centerY);
                    builder.KeyframeArcA(right, top);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    break;

                case 'N':
                    builder.KeyframeInitialize(left, bottom);
                    builder.KeyframeAxisLine(path.Y, top);
                    builder.KeyframeArcB(right, bottom);
                    builder.KeyframeAxisLine(path.Y, top);
                    break;

                case 'O':
                    builder.KeyframeInitialize(right, centerY);
                    builder.KeyframeArcA(centerX, bottom);
                    builder.KeyframeArcB(left, centerY);
                    builder.KeyframeArcA(centerX, top);
                    builder.KeyframeArcB(right, centerY);
                    break;

                case 'P':
                    builder.KeyframeInitialize(left, bottom);
                    builder.KeyframeAxisLine(path.Y, top);
                    builder.KeyframeArcB(right, centerY - quarterHeight);
                    builder.KeyframeArcA(left, centerY);
                    break;

                case 'Q':
                    builder.KeyframeInitialize(right, centerY);
                    builder.KeyframeArcA(centerX, bottom);
                    builder.KeyframeArcB(left, centerY);
                    builder.KeyframeArcA(centerX, top);
                    builder.KeyframeArcB(right, centerY);
                    builder.KeyframeWarpTo(centerX, centerY + quarterHeight);
                    builder.KeyframeArcB(right, bottom);
                    break;

                case 'R':
                    builder.KeyframeInitialize(left, bottom);
                    builder.KeyframeAxisLine(path.Y, top);
                    builder.KeyframeArcB(right, centerY - quarterHeight);
                    builder.KeyframeArcA(left, centerY);
                    builder.KeyframeArcB(right, bottom);
                    break;

                case 'S':
                    builder.KeyframeInitialize(right, centerY - quarterHeight);
                    builder.KeyframeArcA(centerX, top);
                    builder.KeyframeArcB(left, centerY - quarterHeight);
                    builder.KeyframeArcA(centerX, centerY);
                    builder.KeyframeArcB(right, centerY + quarterHeight);
                    builder.KeyframeArcA(centerX, bottom);
                    builder.KeyframeArcB(left, centerY + quarterHeight);
                    break;

                case 'T':
                    builder.KeyframeInitialize(left, top);
                    builder.KeyframeAxisLine(path.X, right);
                    builder.KeyframeAxisLine(path.X, centerX);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    break;

                case 'U':
                    builder.KeyframeInitialize(left, top);
                    builder.KeyframeAxisLine(path.Y, centerY);
                    builder.KeyframeArcA(centerX, bottom);
                    builder.KeyframeArcB(right, centerY);
                    builder.KeyframeAxisLine(path.Y, top);
                    break;

                case 'V':
                    builder.KeyframeInitialize(left, top);
                    builder.KeyframeLineTo(centerX, bottom);
                    builder.KeyframeLineTo(right, top);
                    break;

                case 'W':
                    builder.KeyframeInitialize(left, top);
                    builder.KeyframeArcA(centerX - quarterWidth, bottom);
                    builder.KeyframeArcB(centerX, centerY);
                    builder.KeyframeArcA(centerX + quarterWidth, bottom);
                    builder.KeyframeArcB(right, top);
                    break;

                case 'X':
                    builder.KeyframeInitialize(left, top);
                    builder.KeyframeArcA(centerX, centerY);
                    builder.KeyframeArcB(right, bottom);
                    builder.KeyframeWarpTo(right, top);
                    builder.KeyframeArcA(centerX, centerY);
                    builder.KeyframeArcB(left, bottom);
                    break;

                case 'Y':
                    builder.KeyframeInitialize(left, top);
                    builder.KeyframeArcA(centerX, centerY);
                    builder.KeyframeArcB(right, top);
                    builder.KeyframeWarpTo(centerX, centerY);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    break;

                case 'Z':
                    builder.KeyframeInitialize(left, top);
                    builder.KeyframeAxisLine(path.X, right);
                    builder.KeyframeLineTo(left, bottom);
                    builder.KeyframeAxisLine(path.X, right);
                    break;

                case 'a':
                    builder.KeyframeInitialize(right, lowercaseTop);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    LowercaseCircleMacro();
                    break;

                case 'b':
                    builder.KeyframeInitialize(left, top);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    LowercaseCircleMacro();
                    break;

                case 'c':
                    builder.KeyframeInitialize(right, lowercaseVerticalCenter, false);
                    builder.KeyframeArcAPartial(centerX, lowercaseTop, 0.5f, 1f);
                    builder.KeyframeArcB(left, lowercaseVerticalCenter);
                    builder.KeyframeArcA(centerX, bottom);
                    builder.KeyframeArcBPartial(right, lowercaseVerticalCenter, 0f, 0.5f);
                    break;

                case 'd':
                    builder.KeyframeInitialize(right, top);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    builder.KeyframeWarpTo(centerX, lowercaseTop);
                    builder.KeyframeArcB(left, lowercaseVerticalCenter);
                    builder.KeyframeArcA(centerX, bottom);
                    builder.KeyframeArcB(right, lowercaseVerticalCenter);
                    builder.KeyframeArcA(centerX, lowercaseTop);
                    break;

                case 'e':
                    builder.KeyframeInitialize(left, eCrossHeight);
                    builder.KeyframeAxisLine(path.X, right);
                    builder.KeyframeArcA(centerX, lowercaseTop);
                    builder.KeyframeArcB(left, eCrossHeight);
                    builder.KeyframeArcA(centerX, bottom);
                    builder.KeyframeArcBPartial(right, eCrossHeight, 0f, 0.5f);
                    break;

                case 'f':
                    builder.KeyframeInitialize(right, centerY - quarterHeight);
                    builder.KeyframeArcA(centerX + quarterWidth, top);
                    builder.KeyframeArcB(centerX, centerY - quarterHeight);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    builder.KeyframeWarpTo(centerX - quarterWidth, centerY);
                    builder.KeyframeAxisLine(path.X, centerX + quarterWidth);
                    break;

                case 'g':
                    builder.KeyframeInitialize(right, centerY - quarterHeight);
                    LowercaseCircleMacro();
                    builder.KeyframeWarpTo(right, lowercaseTop);
                    builder.KeyframeAxisLine(path.Y, lowercaseBottom - quarterHeight);
                    builder.KeyframeArcA(centerX, lowercaseBottom);
                    builder.KeyframeArcB(left, lowercaseBottom - quarterHeight);
                    break;

                case 'h':
                    builder.KeyframeInitialize(left, top);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    builder.KeyframeWarpTo(left, armHeight);
                    builder.KeyframeArcA(centerX, lowercaseTop);
                    builder.KeyframeArcB(right, armHeight);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    break;

                case 'i':
                    builder.KeyframeInitialize(centerX, lowercaseTop - halfHeight);
                    builder.KeyframeAxisLine(path.Y, lowercaseTop - quarterHeight);
                    builder.KeyframeWarpTo(centerX, lowercaseTop);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    break;

                case 'j':
                    builder.KeyframeInitialize(right, lowercaseTop - halfHeight);
                    builder.KeyframeAxisLine(path.Y, lowercaseTop - quarterHeight);
                    builder.KeyframeWarpTo(right, lowercaseTop);
                    builder.KeyframeAxisLine(path.Y, lowercaseBottom - quarterHeight);
                    builder.KeyframeArcA(centerX, lowercaseBottom);
                    builder.KeyframeArcB(left, lowercaseBottom - quarterHeight);
                    break;

                case 'k':
                    builder.KeyframeInitialize(left, top);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    builder.KeyframeAxisLine(path.Y, centerY);
                    builder.KeyframeArcBPartial(right, top, 0, 0.75f);
                    builder.KeyframeWarpTo(left, centerY);
                    builder.KeyframeArcB(right, bottom);
                    break;

                case 'l':
                    builder.KeyframeInitialize(centerX, top);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    break;

                case 'm':
                    builder.KeyframeInitialize(left, lowercaseTop);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    builder.KeyframeWarpTo(left, armHeight);
                    builder.KeyframeArcA(centerX - quarterWidth, lowercaseTop);
                    builder.KeyframeArcB(centerX, armHeight);
                    builder.KeyframeArcA(centerX + quarterWidth, lowercaseTop);
                    builder.KeyframeArcB(right, armHeight);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    break;

                case 'n':
                    builder.KeyframeInitialize(left, lowercaseTop);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    builder.KeyframeWarpTo(left, armHeight);
                    builder.KeyframeArcA(centerX, lowercaseTop);
                    builder.KeyframeArcB(right, armHeight);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    break;

                case 'o':
                    builder.KeyframeInitialize(right, eCrossHeight);
                    LowercaseCircleMacro();
                    break;

                case 'p':
                    builder.KeyframeInitialize(right, eCrossHeight);
                    LowercaseCircleMacro();
                    builder.KeyframeWarpTo(left, lowercaseTop);
                    builder.KeyframeAxisLine(path.Y, lowercaseBottom);
                    break;

                case 'q':
                    builder.KeyframeInitialize(right, eCrossHeight);
                    LowercaseCircleMacro();
                    builder.KeyframeWarpTo(right, lowercaseTop);
                    builder.KeyframeAxisLine(path.Y, lowercaseBottom);
                    break;

                case 'r':
                    builder.KeyframeInitialize(left, lowercaseTop);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    builder.KeyframeWarpTo(left, armHeight);
                    builder.KeyframeArcA(centerX, lowercaseTop);
                    builder.KeyframeArcB(right, armHeight);
                    break;

                case 's':
                    builder.KeyframeInitialize(right, lowercaseVerticalCenter - quarterHeight / 2);
                    builder.KeyframeArcA(centerX, lowercaseTop);
                    builder.KeyframeArcB(left, lowercaseVerticalCenter - quarterHeight / 2);
                    builder.KeyframeArcA(centerX, lowercaseVerticalCenter);
                    builder.KeyframeArcB(right, lowercaseVerticalCenter + quarterHeight / 2);
                    builder.KeyframeArcA(centerX, bottom);
                    builder.KeyframeArcB(left, lowercaseVerticalCenter + quarterHeight / 2);
                    break;

                case 't':
                    builder.KeyframeInitialize(centerX, top);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    builder.KeyframeWarpTo(centerX - quarterWidth, centerY);
                    builder.KeyframeAxisLine(path.X, centerX + quarterWidth);
                    break;

                case 'u':
                    builder.KeyframeInitialize(left, lowercaseTop);
                    builder.KeyframeAxisLine(path.Y, lowercaseVerticalCenter);
                    builder.KeyframeArcA(centerX, bottom);
                    builder.KeyframeArcB(right, lowercaseVerticalCenter);
                    builder.KeyframeAxisLine(path.Y, lowercaseTop);
                    break;

                case 'v':
                    builder.KeyframeInitialize(left, lowercaseTop);
                    builder.KeyframeLineTo(centerX, bottom);
                    builder.KeyframeLineTo(right, lowercaseTop);
                    break;

                case 'w':
                    builder.KeyframeInitialize(left, lowercaseTop);
                    builder.KeyframeArcA(left / 2, bottom);
                    builder.KeyframeArcB(centerX, lowercaseTop);
                    builder.KeyframeArcA(right / 2, bottom);
                    builder.KeyframeArcB(right, lowercaseTop);
                    break;

                case 'x':
                    builder.KeyframeInitialize(left, lowercaseTop);
                    builder.KeyframeArcA(centerX, lowercaseVerticalCenter);
                    builder.KeyframeArcB(right, bottom);
                    builder.KeyframeWarpTo(right, lowercaseTop);
                    builder.KeyframeArcA(centerX, lowercaseVerticalCenter);
                    builder.KeyframeArcB(left, bottom);
                    break;

                case 'y':
                    builder.KeyframeInitialize(left, lowercaseTop);
                    builder.KeyframeAxisLine(path.Y, lowercaseVerticalCenter);
                    builder.KeyframeArcA(centerX, bottom);
                    builder.KeyframeArcB(right, lowercaseVerticalCenter);
                    builder.KeyframeWarpTo(right, lowercaseTop);
                    builder.KeyframeAxisLine(path.Y, lowercaseBottom - quarterHeight);
                    builder.KeyframeArcA(centerX, lowercaseBottom);
                    builder.KeyframeArcB(left, lowercaseBottom - quarterHeight);
                    break;

                case 'z':
                    builder.KeyframeInitialize(left, lowercaseTop);
                    builder.KeyframeAxisLine(path.X, right);
                    builder.KeyframeLineTo(left, bottom);
                    builder.KeyframeAxisLine(path.X, right);
                    break;

                case '!':
                    builder.KeyframeInitialize(centerX, top);
                    builder.KeyframeAxisLine(path.Y, centerY + sixteenthHeight * 2);
                    // builder.KeyframeEnable();
                    builder.KeyframeWarpTo(centerX, bottom);
                    builder.KeyframeAxisLine(path.Y, bottom - sixteenthHeight);
                    break;

                case '\"':
                    builder.KeyframeInitialize(centerX - quarterWidth, top);
                    builder.KeyframeAxisLine(path.Y, centerY - quarterHeight);
                    builder.KeyframeWarpTo(centerX + quarterWidth, top);
                    builder.KeyframeAxisLine(path.Y, centerY - quarterHeight);
                    break;

                case '#':
                    builder.KeyframeInitialize(centerX - quarterWidth, top);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    builder.KeyframeWarpTo(centerX + quarterWidth, top);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    builder.KeyframeWarpTo(left, centerY + quarterHeight);
                    builder.KeyframeAxisLine(path.X, right);
                    builder.KeyframeWarpTo(left, centerY - quarterHeight);
                    builder.KeyframeAxisLine(path.X, right);
                    break;

                case '$':
                    builder.KeyframeInitialize(right, centerY - quarterHeight);
                    builder.KeyframeArcA(centerX, top + sixteenthHeight);
                    builder.KeyframeArcB(left, centerY - quarterHeight);
                    builder.KeyframeArcA(centerX, centerY);
                    builder.KeyframeArcB(right, centerY + quarterHeight);
                    builder.KeyframeArcA(centerX, bottom - sixteenthHeight);
                    builder.KeyframeArcB(left, centerY + quarterHeight);
                    builder.KeyframeWarpTo(centerX, top);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    break;

                case '%':
                    builder.KeyframeInitialize(right, top);
                    builder.KeyframeLineTo(left, bottom);
                    
                    builder.KeyframeWarpTo(centerX - quarterWidth, top);
                    builder.KeyframeArcB(left, centerY - quarterHeight);
                    builder.KeyframeArcA(centerX - quarterWidth, centerY);
                    builder.KeyframeArcB(centerX, centerY - quarterHeight);
                    builder.KeyframeArcA(centerX - quarterWidth, top);
                    
                    
                    builder.KeyframeWarpTo(centerX + quarterWidth, centerY);
                    builder.KeyframeArcB(right, centerY + quarterHeight);
                    builder.KeyframeArcA(centerX + quarterWidth, bottom);
                    builder.KeyframeArcB(centerX, centerY + quarterHeight);
                    builder.KeyframeArcA(centerX + quarterWidth, centerY);
                    
                    
                    break;

                case '&':
                    builder.KeyframeInitialize(centerX + quarterWidth, centerY - quarterHeight);
                    builder.KeyframeArcA(centerX, top);
                    builder.KeyframeArcB(left, centerY - quarterHeight);
                    builder.KeyframeLineTo(right, bottom);
                    builder.KeyframeWarpTo(centerX, centerY);
                    builder.KeyframeArcB(left, bottom - quarterHeight);
                    builder.KeyframeArcA(centerX, bottom);
                    builder.KeyframeArcB(right, centerY + quarterHeight);
                    break;

                case '\'':
                    builder.KeyframeInitialize(centerX, top);
                    builder.KeyframeAxisLine(path.Y, centerY - quarterHeight);
                    break;

                case '(':
                    builder.KeyframeInitialize(centerX, top);
                    builder.KeyframeArcB(left, centerY);
                    builder.KeyframeArcA(centerX, bottom);
                    break;

                case ')':
                    builder.KeyframeInitialize(centerX, top);
                    builder.KeyframeArcB(right, centerY);
                    builder.KeyframeArcA(centerX, bottom);
                    break;

                case '*':
                    builder.KeyframeInitialize(centerX, top);
                    builder.KeyframeLineTo(centerX, bottom);
                    builder.KeyframeWarpTo(centerX, centerY);
                    builder.KeyframeLineTo(left, centerY - quarterHeight);
                    builder.KeyframeWarpTo(centerX, centerY);
                    builder.KeyframeLineTo(right, centerY - quarterHeight);
                    builder.KeyframeWarpTo(centerX, centerY);
                    builder.KeyframeLineTo(left, centerY + quarterHeight);
                    builder.KeyframeWarpTo(centerX, centerY);
                    builder.KeyframeLineTo(right, centerY + quarterHeight);
                    break;

                case '+':
                    builder.KeyframeInitialize(centerX, top);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    builder.KeyframeWarpTo(left, centerY);
                    builder.KeyframeAxisLine(path.X, right);
                    break;

                case ',':
                    builder.KeyframeInitialize(centerX, bottom);
                    builder.KeyframeArcA(centerX - quarterWidth, lowercaseBottom);
                    break;

                case '-':
                    builder.KeyframeInitialize(left, centerY);
                    builder.KeyframeAxisLine(path.X, right);
                    break;

                case '.':
                    builder.KeyframeInitialize(centerX, bottom);
                    builder.KeyframeLineTo(centerX, bottom - sixteenthHeight);
                    break;

                case '/':
                    builder.KeyframeInitialize(right, top);
                    builder.KeyframeLineTo(left, bottom);
                    break;

                case ':':
                    builder.KeyframeInitialize(centerX, centerY - quarterHeight);
                    builder.KeyframeLineTo(centerX, centerY - quarterHeight - sixteenthHeight);
                    builder.KeyframeWarpTo(centerX, bottom);
                    builder.KeyframeLineTo(centerX, bottom - sixteenthHeight);
                    break;

                case ';':
                    builder.KeyframeInitialize(centerX, centerY - quarterHeight);
                    builder.KeyframeLineTo(centerX, centerY - quarterHeight - sixteenthHeight);
                    builder.KeyframeWarpTo(centerX, bottom);
                    builder.KeyframeArcA(centerX - quarterWidth, lowercaseBottom);
                    break;

                case '<':
                    builder.KeyframeInitialize(right, top);
                    builder.KeyframeLineTo(left, centerY);
                    builder.KeyframeLineTo(right, bottom);
                    break;

                case '=':
                    builder.KeyframeInitialize(left, centerY - quarterHeight);
                    builder.KeyframeAxisLine(path.X, right);
                    builder.KeyframeWarpTo(left, centerY + quarterHeight);
                    builder.KeyframeAxisLine(path.X, right);
                    break;

                case '>':
                    builder.KeyframeInitialize(left, top);
                    builder.KeyframeLineTo(right, centerY);
                    builder.KeyframeLineTo(left, bottom);
                    break;

                case '?':
                    builder.KeyframeInitialize(left, centerY - quarterHeight);
                    builder.KeyframeArcA(centerX, top);
                    builder.KeyframeArcB(right, centerY - quarterHeight);
                    builder.KeyframeArcA(centerX + quarterWidth, centerY);
                    builder.KeyframeArcB(centerX, centerY + sixteenthHeight * 2);
                    builder.KeyframeWarpTo(centerX, bottom);
                    builder.KeyframeLineTo(centerX, bottom - sixteenthHeight);
                    break;

                case '@':
                    builder.KeyframeInitialize(right, centerY);
                    builder.KeyframeArcA(centerX + quarterWidth, centerY - quarterHeight);
                    builder.KeyframeArcB(centerX, centerY);
                    builder.KeyframeArcA(centerX + quarterWidth, centerY + quarterHeight);
                    builder.KeyframeArcB(right, centerY);
                    builder.KeyframeArcA(centerX, top);
                    builder.KeyframeArcB(left, centerY);
                    builder.KeyframeArcA(left, centerY + quarterHeight);
                    builder.KeyframeArcA(centerX, bottom);
                    break;

                case '[':
                    builder.KeyframeInitialize(centerX, top);
                    builder.KeyframeAxisLine(path.X, left);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    builder.KeyframeAxisLine(path.X, centerX);
                    break;

                case '\\':
                    builder.KeyframeInitialize(left, top);
                    builder.KeyframeLineTo(right, bottom);
                    break;

                case ']':
                    builder.KeyframeInitialize(centerX, top);
                    builder.KeyframeAxisLine(path.X, right);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    builder.KeyframeAxisLine(path.X, centerX);
                    break;

                case '^':
                    builder.KeyframeInitialize(centerX - quarterWidth, centerY - quarterHeight);
                    builder.KeyframeLineTo(centerX, top);
                    builder.KeyframeLineTo(centerX + quarterWidth, centerY - quarterHeight);
                    break;

                case '`':
                    builder.KeyframeInitialize(centerX - quarterWidth, top);
                    builder.KeyframeLineTo(centerX, centerY - quarterHeight);
                    break;

                case '_':
                    builder.KeyframeInitialize(left, bottom);
                    builder.KeyframeAxisLine(path.X, right);
                    break;

                case '{':
                    builder.KeyframeInitialize(centerX, top);
                    builder.KeyframeArcB(centerX - quarterWidth, centerY - quarterHeight);
                    builder.KeyframeArcA(left, centerY);
                    builder.KeyframeArcB(centerX - quarterWidth, centerY + quarterHeight);
                    builder.KeyframeArcA(centerX, bottom);
                    break;

                case '|':
                    builder.KeyframeInitialize(centerX, top);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    break;

                case '}':
                    builder.KeyframeInitialize(centerX, top);
                    builder.KeyframeArcB(centerX + quarterWidth, centerY - quarterHeight);
                    builder.KeyframeArcA(right, centerY);
                    builder.KeyframeArcB(centerX + quarterWidth, centerY + quarterHeight);
                    builder.KeyframeArcA(centerX, bottom);
                    break;

                case '~':
                    builder.KeyframeInitialize(left, centerY + sixteenthHeight);
                    builder.KeyframeArcA(centerX - quarterWidth, centerY - sixteenthHeight);
                    builder.KeyframeArcB(centerX, centerY);
                    builder.KeyframeArcA(centerX + quarterWidth, centerY + sixteenthHeight);
                    builder.KeyframeArcB(right, centerY - sixteenthHeight);
                    break;

                case '0':
                    builder.KeyframeInitialize(right, centerY);
                    builder.KeyframeArcA(centerX, bottom);
                    builder.KeyframeArcB(left, centerY);
                    builder.KeyframeArcA(centerX, top);
                    builder.KeyframeArcB(right, centerY);
                    break;
                
                case '1':
                    builder.KeyframeInitialize(left, centerY - quarterHeight);
                    builder.KeyframeArcB(centerX, top);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    break;

                case '2':
                    builder.KeyframeInitialize(left, centerY - quarterHeight);
                    builder.KeyframeArcA(centerX, top);
                    builder.KeyframeArcB(right, centerY - quarterHeight);
                    builder.KeyframeArcA(centerX, centerY);
                    builder.KeyframeArcB(left, bottom);
                    builder.KeyframeAxisLine(path.X, right);
                    break;

                case '3':
                    builder.KeyframeInitialize(left, centerY - quarterHeight);
                    builder.KeyframeArcA(centerX, top);
                    builder.KeyframeArcB(right, centerY - quarterHeight);
                    builder.KeyframeArcA(centerX, centerY);
                    builder.KeyframeArcB(right, centerY + quarterHeight);
                    builder.KeyframeArcA(centerX, bottom);
                    builder.KeyframeArcB(left, centerY + quarterHeight);
                    break;

                case '4':
                    builder.KeyframeInitialize(centerX + quarterWidth, top);
                    builder.KeyframeLineTo(left, centerY + quarterHeight);
                    builder.KeyframeAxisLine(path.X, right);
                    builder.KeyframeWarpTo(right - quarterWidth, top);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    break;
                
                case '5':
                    builder.KeyframeInitialize(right, top);
                    builder.KeyframeAxisLine(path.X, left);
                    builder.KeyframeAxisLine(path.Y, centerY - sixteenthHeight);
                    builder.KeyframeAxisLine(path.X, centerX + quarterWidth);
                    builder.KeyframeArcB(right, centerY + quarterHeight);
                    builder.KeyframeArcA(centerX, bottom);
                    builder.KeyframeArcB(left, centerY + quarterHeight);
                    break;
                
                case '6':
                    builder.KeyframeInitialize(right, centerY - quarterHeight);
                    builder.KeyframeArcA(centerX, top);
                    builder.KeyframeArcB(left, centerY - quarterHeight);
                    builder.KeyframeAxisLine(path.Y, lowercaseVerticalCenter);
                    LowercaseCircleMacro();
                    break;
                
                case '7':
                    builder.KeyframeInitialize(left, top);
                    builder.KeyframeAxisLine(path.X, right);
                    builder.KeyframeLineTo(centerX, bottom);
                    break;
                
                case '8':
                    builder.KeyframeInitialize(centerX, centerY);
                    builder.KeyframeArcB(left, centerY - quarterHeight);
                    builder.KeyframeArcA(centerX, top);
                    builder.KeyframeArcB(right, centerY - quarterHeight);
                    builder.KeyframeArcA(centerX, centerY);
                    builder.KeyframeArcB(left, centerY + quarterHeight);
                    builder.KeyframeArcA(centerX, bottom);
                    builder.KeyframeArcB(right, centerY + quarterHeight);
                    builder.KeyframeArcA(centerX, centerY);
                    break;
                
                case '9':
                    builder.KeyframeInitialize(centerX, centerY);
                    builder.KeyframeArcB(left, centerY - quarterHeight);
                    builder.KeyframeArcA(centerX, top);
                    builder.KeyframeArcB(right, centerY - quarterHeight);
                    builder.KeyframeArcA(centerX, centerY);
                    builder.KeyframeWarpTo(right, centerY - quarterHeight);
                    builder.KeyframeAxisLine(path.Y, centerY + quarterHeight);
                    builder.KeyframeArcA(centerX, bottom);
                    builder.KeyframeArcB(left, centerY + quarterHeight);
                    break;

                default:
                    // By default we draw a circle
                    builder.KeyframeInitialize(right, centerY);
                    builder.KeyframeArcA(centerX, bottom);
                    builder.KeyframeArcB(left, centerY);
                    builder.KeyframeArcA(centerX, top);
                    builder.KeyframeArcB(right, centerY);
                    break;
            }

            // Add final builder.keyframe            path.Addbuilder.Keyframepath.Duration;

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
