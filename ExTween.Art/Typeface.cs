namespace ExTween.Art
{
    public static class Typeface
    {
        public static TweenPath GetPathForLetter(char letter)
        {
            var path = new TweenPath();

            var builder = path.Builder;

            if (char.IsWhiteSpace(letter))
            {
                return new TweenPath();
            }

            // All figures are rendered to a 1x1 virtual canvas and are stretched and scaled at draw-time
            const float width = 1f;
            const float trueHeight = 1f;
            
            const float sixthTrueHeight = trueHeight / 6;
            const float thirdTrueHeight = trueHeight / 3;
            const float effectiveHeight = trueHeight * 2 / 3;

            const float halfWidth = width / 2;
            const float quarterWidth = halfWidth / 2;
            const float eightWidth = quarterWidth / 2;
            
            const float halfHeight = effectiveHeight / 2;
            const float quarterHeight = halfHeight / 2;
            const float eightHeight = quarterHeight / 2;
            const float sixteenthHeight = quarterHeight / 4;

            const float centerX = 0;
            const float centerY = -sixthTrueHeight;
            const float left = centerX - halfWidth;
            const float right = centerX + halfWidth;
            const float top = centerY - halfHeight;
            const float bottom = centerY + halfHeight;
            const float lowercaseTop = centerY; // subtracting `sixthTrueHeight` here makes it look interesting
            const float lowercaseBottom = trueHeight / 2;
            
            const float lowerQuarter = centerY + quarterHeight;
            const float upperQuarter = centerY - quarterHeight;
            const float rightQuarter = centerX + quarterWidth;
            const float leftQuarter = centerX - quarterWidth;
            
            
            // y position that the lowercase 'r' arm juts out
            const float armHeight = lowercaseTop + halfHeight * 0.35f;
            // y position of the horizontal line in lowercase 'e'
            const float eCrossHeight = lowerQuarter;
            const float lowercaseVerticalCenter = lowerQuarter;

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
                    builder.KeyframeArcB(rightQuarter, upperQuarter);
                    builder.KeyframeArcA(left, centerY);
                    builder.KeyframeArcB(right, lowerQuarter);
                    builder.KeyframeArcA(left, bottom);
                    break;

                case 'C':
                    builder.KeyframeInitialize(right, upperQuarter);
                    builder.KeyframeArcA(centerX, top);
                    builder.KeyframeArcB(left, centerY);
                    builder.KeyframeArcA(centerX, bottom);
                    builder.KeyframeArcB(right, lowerQuarter);
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
                    builder.KeyframeAxisLine(path.X, rightQuarter);
                    break;

                case 'F':
                    builder.KeyframeInitialize(right, top);
                    builder.KeyframeAxisLine(path.X, left);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    builder.KeyframeWarpTo(left, centerY);
                    builder.KeyframeAxisLine(path.X, rightQuarter);
                    break;

                case 'G':
                    builder.KeyframeInitialize(right, upperQuarter);
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
                    builder.KeyframeAxisLine(path.Y, lowerQuarter);
                    builder.KeyframeArcA(centerX, bottom);
                    builder.KeyframeArcB(left, lowerQuarter);
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
                    builder.KeyframeArcB(right, upperQuarter);
                    builder.KeyframeArcA(left, centerY);
                    break;

                case 'Q':
                    builder.KeyframeInitialize(right, centerY);
                    builder.KeyframeArcA(centerX, bottom);
                    builder.KeyframeArcB(left, centerY);
                    builder.KeyframeArcA(centerX, top);
                    builder.KeyframeArcB(right, centerY);
                    builder.KeyframeWarpTo(centerX, lowerQuarter);
                    builder.KeyframeArcB(right, bottom);
                    break;

                case 'R':
                    builder.KeyframeInitialize(left, bottom);
                    builder.KeyframeAxisLine(path.Y, top);
                    builder.KeyframeArcB(right, upperQuarter);
                    builder.KeyframeArcA(left, centerY);
                    builder.KeyframeArcB(right, bottom);
                    break;

                case 'S':
                    builder.KeyframeInitialize(right, upperQuarter);
                    builder.KeyframeArcA(centerX, top);
                    builder.KeyframeArcB(left, upperQuarter);
                    builder.KeyframeArcA(centerX, centerY);
                    builder.KeyframeArcB(right, lowerQuarter);
                    builder.KeyframeArcA(centerX, bottom);
                    builder.KeyframeArcB(left, lowerQuarter);
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
                    builder.KeyframeArcA(leftQuarter, bottom);
                    builder.KeyframeArcB(centerX, centerY);
                    builder.KeyframeArcA(rightQuarter, bottom);
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
                    builder.KeyframeInitialize(right, upperQuarter);
                    builder.KeyframeArcA(rightQuarter, top);
                    builder.KeyframeArcB(centerX, upperQuarter);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    builder.KeyframeWarpTo(leftQuarter, centerY);
                    builder.KeyframeAxisLine(path.X, rightQuarter);
                    break;

                case 'g':
                    builder.KeyframeInitialize(right, upperQuarter);
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
                    builder.KeyframeArcA(leftQuarter, lowercaseTop);
                    builder.KeyframeArcB(centerX, armHeight);
                    builder.KeyframeArcA(rightQuarter, lowercaseTop);
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
                    builder.KeyframeInitialize(right, lowercaseVerticalCenter - eightHeight);
                    builder.KeyframeArcA(centerX, lowercaseTop);
                    builder.KeyframeArcB(left, lowercaseVerticalCenter - eightHeight);
                    builder.KeyframeArcA(centerX, lowercaseVerticalCenter);
                    builder.KeyframeArcB(right, lowercaseVerticalCenter + eightHeight);
                    builder.KeyframeArcA(centerX, bottom);
                    builder.KeyframeArcB(left, lowercaseVerticalCenter + eightHeight);
                    break;

                case 't':
                    builder.KeyframeInitialize(centerX, top);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    builder.KeyframeWarpTo(leftQuarter, centerY);
                    builder.KeyframeAxisLine(path.X, rightQuarter);
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
                    builder.KeyframeAxisLine(path.Y, centerY + eightHeight);
                    // builder.KeyframeEnable();
                    builder.KeyframeWarpTo(centerX, bottom);
                    builder.KeyframeAxisLine(path.Y, bottom - sixteenthHeight);
                    break;

                case '\"':
                    builder.KeyframeInitialize(leftQuarter, top);
                    builder.KeyframeAxisLine(path.Y, upperQuarter);
                    builder.KeyframeWarpTo(rightQuarter, top);
                    builder.KeyframeAxisLine(path.Y, upperQuarter);
                    break;

                case '#':
                    builder.KeyframeInitialize(leftQuarter, top);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    builder.KeyframeWarpTo(rightQuarter, top);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    builder.KeyframeWarpTo(left, lowerQuarter);
                    builder.KeyframeAxisLine(path.X, right);
                    builder.KeyframeWarpTo(left, upperQuarter);
                    builder.KeyframeAxisLine(path.X, right);
                    break;

                case '$':
                    builder.KeyframeInitialize(right, upperQuarter);
                    builder.KeyframeArcA(centerX, top + sixteenthHeight);
                    builder.KeyframeArcB(left, upperQuarter);
                    builder.KeyframeArcA(centerX, centerY);
                    builder.KeyframeArcB(right, lowerQuarter);
                    builder.KeyframeArcA(centerX, bottom - sixteenthHeight);
                    builder.KeyframeArcB(left, lowerQuarter);
                    builder.KeyframeWarpTo(centerX, top);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    break;

                case '%':
                    builder.KeyframeInitialize(right, top);
                    builder.KeyframeLineTo(left, bottom);
                    builder.KeyframeWarpTo(leftQuarter, top);
                    builder.KeyframeArcB(left, upperQuarter);
                    builder.KeyframeArcA(leftQuarter, centerY);
                    builder.KeyframeArcB(centerX, upperQuarter);
                    builder.KeyframeArcA(leftQuarter, top);
                    builder.KeyframeWarpTo(rightQuarter, centerY);
                    builder.KeyframeArcB(right, lowerQuarter);
                    builder.KeyframeArcA(rightQuarter, bottom);
                    builder.KeyframeArcB(centerX, lowerQuarter);
                    builder.KeyframeArcA(rightQuarter, centerY);
                    
                    
                    break;

                case '&':
                    builder.KeyframeInitialize(rightQuarter, upperQuarter);
                    builder.KeyframeArcA(centerX, top);
                    builder.KeyframeArcB(left, upperQuarter);
                    builder.KeyframeLineTo(right, bottom);
                    builder.KeyframeWarpTo(centerX, centerY);
                    builder.KeyframeArcB(left, bottom - quarterHeight);
                    builder.KeyframeArcA(centerX, bottom);
                    builder.KeyframeArcB(right, lowerQuarter);
                    break;

                case '\'':
                    builder.KeyframeInitialize(centerX, top);
                    builder.KeyframeAxisLine(path.Y, upperQuarter);
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
                    builder.KeyframeLineTo(left, upperQuarter);
                    builder.KeyframeWarpTo(centerX, centerY);
                    builder.KeyframeLineTo(right, upperQuarter);
                    builder.KeyframeWarpTo(centerX, centerY);
                    builder.KeyframeLineTo(left, lowerQuarter);
                    builder.KeyframeWarpTo(centerX, centerY);
                    builder.KeyframeLineTo(right, lowerQuarter);
                    break;

                case '+':
                    builder.KeyframeInitialize(centerX, top);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    builder.KeyframeWarpTo(left, centerY);
                    builder.KeyframeAxisLine(path.X, right);
                    break;

                case ',':
                    builder.KeyframeInitialize(centerX, bottom);
                    builder.KeyframeArcA(leftQuarter, lowercaseBottom);
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
                    builder.KeyframeInitialize(centerX, upperQuarter);
                    builder.KeyframeLineTo(centerX, upperQuarter - sixteenthHeight);
                    builder.KeyframeWarpTo(centerX, bottom);
                    builder.KeyframeLineTo(centerX, bottom - sixteenthHeight);
                    break;

                case ';':
                    builder.KeyframeInitialize(centerX, upperQuarter);
                    builder.KeyframeLineTo(centerX, upperQuarter - sixteenthHeight);
                    builder.KeyframeWarpTo(centerX, bottom);
                    builder.KeyframeArcA(leftQuarter, lowercaseBottom);
                    break;

                case '<':
                    builder.KeyframeInitialize(right, top);
                    builder.KeyframeLineTo(left, centerY);
                    builder.KeyframeLineTo(right, bottom);
                    break;

                case '=':
                    builder.KeyframeInitialize(left, upperQuarter);
                    builder.KeyframeAxisLine(path.X, right);
                    builder.KeyframeWarpTo(left, lowerQuarter);
                    builder.KeyframeAxisLine(path.X, right);
                    break;

                case '>':
                    builder.KeyframeInitialize(left, top);
                    builder.KeyframeLineTo(right, centerY);
                    builder.KeyframeLineTo(left, bottom);
                    break;

                case '?':
                    builder.KeyframeInitialize(left, upperQuarter);
                    builder.KeyframeArcA(centerX, top);
                    builder.KeyframeArcB(right, upperQuarter);
                    builder.KeyframeArcA(rightQuarter, centerY);
                    builder.KeyframeArcB(centerX, centerY + eightHeight);
                    builder.KeyframeWarpTo(centerX, bottom);
                    builder.KeyframeLineTo(centerX, bottom - sixteenthHeight);
                    break;

                case '@':
                    builder.KeyframeInitialize(right, centerY);
                    builder.KeyframeArcA(rightQuarter, upperQuarter);
                    builder.KeyframeArcB(centerX, centerY);
                    builder.KeyframeArcA(rightQuarter, lowerQuarter);
                    builder.KeyframeArcB(right, centerY);
                    builder.KeyframeArcA(centerX, top);
                    builder.KeyframeArcB(left, centerY);
                    builder.KeyframeArcA(left, lowerQuarter);
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
                    builder.KeyframeInitialize(leftQuarter, upperQuarter);
                    builder.KeyframeLineTo(centerX, top);
                    builder.KeyframeLineTo(rightQuarter, upperQuarter);
                    break;

                case '`':
                    builder.KeyframeInitialize(leftQuarter, top);
                    builder.KeyframeLineTo(centerX, upperQuarter);
                    break;

                case '_':
                    builder.KeyframeInitialize(left, bottom);
                    builder.KeyframeAxisLine(path.X, right);
                    break;

                case '{':
                    builder.KeyframeInitialize(centerX, top);
                    builder.KeyframeArcB(leftQuarter, upperQuarter);
                    builder.KeyframeArcA(left, centerY);
                    builder.KeyframeArcB(leftQuarter, lowerQuarter);
                    builder.KeyframeArcA(centerX, bottom);
                    break;

                case '|':
                    builder.KeyframeInitialize(centerX, top);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    break;

                case '}':
                    builder.KeyframeInitialize(centerX, top);
                    builder.KeyframeArcB(rightQuarter, upperQuarter);
                    builder.KeyframeArcA(right, centerY);
                    builder.KeyframeArcB(rightQuarter, lowerQuarter);
                    builder.KeyframeArcA(centerX, bottom);
                    break;

                case '~':
                    builder.KeyframeInitialize(left, centerY + sixteenthHeight);
                    builder.KeyframeArcA(leftQuarter, centerY - sixteenthHeight);
                    builder.KeyframeArcB(centerX, centerY);
                    builder.KeyframeArcA(rightQuarter, centerY + sixteenthHeight);
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
                    builder.KeyframeInitialize(left, upperQuarter);
                    builder.KeyframeArcB(centerX, top);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    break;

                case '2':
                    builder.KeyframeInitialize(left, upperQuarter);
                    builder.KeyframeArcA(centerX, top);
                    builder.KeyframeArcB(right, upperQuarter);
                    builder.KeyframeArcA(centerX, centerY);
                    builder.KeyframeArcB(left, bottom);
                    builder.KeyframeAxisLine(path.X, right);
                    break;

                case '3':
                    builder.KeyframeInitialize(left, upperQuarter);
                    builder.KeyframeArcA(centerX, top);
                    builder.KeyframeArcB(right, upperQuarter);
                    builder.KeyframeArcA(centerX, centerY);
                    builder.KeyframeArcB(right, lowerQuarter);
                    builder.KeyframeArcA(centerX, bottom);
                    builder.KeyframeArcB(left, lowerQuarter);
                    break;

                case '4':
                    builder.KeyframeInitialize(rightQuarter, top);
                    builder.KeyframeLineTo(left, lowerQuarter);
                    builder.KeyframeAxisLine(path.X, right);
                    builder.KeyframeWarpTo(right - quarterWidth, top);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    break;
                
                case '5':
                    builder.KeyframeInitialize(right, top);
                    builder.KeyframeAxisLine(path.X, left);
                    builder.KeyframeAxisLine(path.Y, centerY - sixteenthHeight);
                    builder.KeyframeAxisLine(path.X, rightQuarter);
                    builder.KeyframeArcB(right, lowerQuarter);
                    builder.KeyframeArcA(centerX, bottom);
                    builder.KeyframeArcB(left, lowerQuarter);
                    break;
                
                case '6':
                    builder.KeyframeInitialize(right, upperQuarter);
                    builder.KeyframeArcA(centerX, top);
                    builder.KeyframeArcB(left, upperQuarter);
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
                    builder.KeyframeArcB(left, upperQuarter);
                    builder.KeyframeArcA(centerX, top);
                    builder.KeyframeArcB(right, upperQuarter);
                    builder.KeyframeArcA(centerX, centerY);
                    builder.KeyframeArcB(left, lowerQuarter);
                    builder.KeyframeArcA(centerX, bottom);
                    builder.KeyframeArcB(right, lowerQuarter);
                    builder.KeyframeArcA(centerX, centerY);
                    break;
                
                case '9':
                    builder.KeyframeInitialize(centerX, centerY);
                    builder.KeyframeArcB(left, upperQuarter);
                    builder.KeyframeArcA(centerX, top);
                    builder.KeyframeArcB(right, upperQuarter);
                    builder.KeyframeArcA(centerX, centerY);
                    builder.KeyframeWarpTo(right, upperQuarter);
                    builder.KeyframeAxisLine(path.Y, lowerQuarter);
                    builder.KeyframeArcA(centerX, bottom);
                    builder.KeyframeArcB(left, lowerQuarter);
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
            
            return path;
        }
    }
}
