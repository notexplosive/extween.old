using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;

namespace ExTween.Art
{
    public static class Typeface
    {
        private static readonly Dictionary<char, TweenPath> Cache = new Dictionary<char, TweenPath>(); 
        
        [Pure]
        public static TweenPath GetPathForLetter(char letter)
        {
            if (Typeface.Cache.ContainsKey(letter))
            {
                return Typeface.Cache[letter];
            }
            
            var path = new TweenPath();

            var builder = path.Builder;

            if (char.IsWhiteSpace(letter))
            {
                var result = new TweenPath();
                Typeface.Cache.Add(letter, result);
                return result;
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
                builder.KeyframeArcVertical(centerX, lowercaseTop);
                builder.KeyframeArcHorizontal(right, lowercaseTop + quarterHeight);
                builder.KeyframeArcVertical(centerX, bottom);
                builder.KeyframeArcHorizontal(left, lowercaseTop + quarterHeight);
            }
            
            switch (letter)
            {
                case 'A':
                    builder.KeyframeInitialize(left, bottom);
                    builder.KeyframeAxisLine(path.Y, centerY);
                    builder.KeyframeArcVertical(centerX, top);
                    builder.KeyframeArcHorizontal(right, centerY);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    builder.KeyframeWarpTo(left, centerY);
                    builder.KeyframeAxisLine(path.X, right);
                    break;

                case 'B':
                    builder.KeyframeInitialize(left, bottom);
                    builder.KeyframeAxisLine(path.Y, top);
                    builder.KeyframeArcHorizontal(rightQuarter, upperQuarter);
                    builder.KeyframeArcVertical(left, centerY);
                    builder.KeyframeArcHorizontal(right, lowerQuarter);
                    builder.KeyframeArcVertical(left, bottom);
                    break;

                case 'C':
                    builder.KeyframeInitialize(right, upperQuarter);
                    builder.KeyframeArcVertical(centerX, top);
                    builder.KeyframeArcHorizontal(left, centerY);
                    builder.KeyframeArcVertical(centerX, bottom);
                    builder.KeyframeArcHorizontal(right, lowerQuarter);
                    break;

                case 'D':
                    builder.KeyframeInitialize(left, top);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    builder.KeyframeArcHorizontal(right, centerY);
                    builder.KeyframeArcVertical(left, top);
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
                    builder.KeyframeArcVertical(centerX, top);
                    builder.KeyframeArcHorizontal(left, centerY);
                    builder.KeyframeArcVertical(centerX, bottom);
                    builder.KeyframeArcHorizontal(right, centerY);
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
                    builder.KeyframeArcVertical(centerX, bottom);
                    builder.KeyframeArcHorizontal(left, lowerQuarter);
                    break;

                case 'K':
                    builder.KeyframeInitialize(left, top);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    builder.KeyframeAxisLine(path.Y, centerY);
                    builder.KeyframeArcHorizontal(right, top);
                    builder.KeyframeWarpTo(left, centerY);
                    builder.KeyframeArcHorizontal(right, bottom);
                    break;

                case 'L':
                    builder.KeyframeInitialize(left, top);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    builder.KeyframeAxisLine(path.X, right);
                    break;

                case 'M':
                    builder.KeyframeInitialize(left, bottom);
                    builder.KeyframeAxisLine(path.Y, top);
                    builder.KeyframeArcHorizontal(centerX, centerY);
                    builder.KeyframeArcVertical(right, top);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    break;

                case 'N':
                    builder.KeyframeInitialize(left, bottom);
                    builder.KeyframeAxisLine(path.Y, top);
                    builder.KeyframeArcHorizontal(right, bottom);
                    builder.KeyframeAxisLine(path.Y, top);
                    break;

                case 'O':
                    builder.KeyframeInitialize(right, centerY);
                    builder.KeyframeArcVertical(centerX, bottom);
                    builder.KeyframeArcHorizontal(left, centerY);
                    builder.KeyframeArcVertical(centerX, top);
                    builder.KeyframeArcHorizontal(right, centerY);
                    break;

                case 'P':
                    builder.KeyframeInitialize(left, bottom);
                    builder.KeyframeAxisLine(path.Y, top);
                    builder.KeyframeArcHorizontal(right, upperQuarter);
                    builder.KeyframeArcVertical(left, centerY);
                    break;

                case 'Q':
                    builder.KeyframeInitialize(right, centerY);
                    builder.KeyframeArcVertical(centerX, bottom);
                    builder.KeyframeArcHorizontal(left, centerY);
                    builder.KeyframeArcVertical(centerX, top);
                    builder.KeyframeArcHorizontal(right, centerY);
                    builder.KeyframeWarpTo(centerX, lowerQuarter);
                    builder.KeyframeArcHorizontal(right, bottom);
                    break;

                case 'R':
                    builder.KeyframeInitialize(left, bottom);
                    builder.KeyframeAxisLine(path.Y, top);
                    builder.KeyframeArcHorizontal(right, upperQuarter);
                    builder.KeyframeArcVertical(left, centerY);
                    builder.KeyframeArcHorizontal(right, bottom);
                    break;

                case 'S':
                    builder.KeyframeInitialize(right, upperQuarter);
                    builder.KeyframeArcVertical(centerX, top);
                    builder.KeyframeArcHorizontal(left, upperQuarter);
                    builder.KeyframeArcVertical(centerX, centerY);
                    builder.KeyframeArcHorizontal(right, lowerQuarter);
                    builder.KeyframeArcVertical(centerX, bottom);
                    builder.KeyframeArcHorizontal(left, lowerQuarter);
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
                    builder.KeyframeArcVertical(centerX, bottom);
                    builder.KeyframeArcHorizontal(right, centerY);
                    builder.KeyframeAxisLine(path.Y, top);
                    break;

                case 'V':
                    builder.KeyframeInitialize(left, top);
                    builder.KeyframeLineTo(centerX, bottom);
                    builder.KeyframeLineTo(right, top);
                    break;

                case 'W':
                    builder.KeyframeInitialize(left, top);
                    builder.KeyframeArcVertical(leftQuarter, bottom);
                    builder.KeyframeArcHorizontal(centerX, centerY);
                    builder.KeyframeArcVertical(rightQuarter, bottom);
                    builder.KeyframeArcHorizontal(right, top);
                    break;

                case 'X':
                    builder.KeyframeInitialize(left, top);
                    builder.KeyframeArcVertical(centerX, centerY);
                    builder.KeyframeArcHorizontal(right, bottom);
                    builder.KeyframeWarpTo(right, top);
                    builder.KeyframeArcVertical(centerX, centerY);
                    builder.KeyframeArcHorizontal(left, bottom);
                    break;

                case 'Y':
                    builder.KeyframeInitialize(left, top);
                    builder.KeyframeArcVertical(centerX, centerY);
                    builder.KeyframeArcHorizontal(right, top);
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
                    builder.KeyframeArcHorizontal(left, lowercaseVerticalCenter);
                    builder.KeyframeArcVertical(centerX, bottom);
                    builder.KeyframeArcBPartial(right, lowercaseVerticalCenter, 0f, 0.5f);
                    break;

                case 'd':
                    builder.KeyframeInitialize(right, top);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    builder.KeyframeWarpTo(centerX, lowercaseTop);
                    builder.KeyframeArcHorizontal(left, lowercaseVerticalCenter);
                    builder.KeyframeArcVertical(centerX, bottom);
                    builder.KeyframeArcHorizontal(right, lowercaseVerticalCenter);
                    builder.KeyframeArcVertical(centerX, lowercaseTop);
                    break;

                case 'e':
                    builder.KeyframeInitialize(left, eCrossHeight);
                    builder.KeyframeAxisLine(path.X, right);
                    builder.KeyframeArcVertical(centerX, lowercaseTop);
                    builder.KeyframeArcHorizontal(left, eCrossHeight);
                    builder.KeyframeArcVertical(centerX, bottom);
                    builder.KeyframeArcBPartial(right, eCrossHeight, 0f, 0.5f);
                    break;

                case 'f':
                    builder.KeyframeInitialize(right, upperQuarter);
                    builder.KeyframeArcVertical(rightQuarter, top);
                    builder.KeyframeArcHorizontal(centerX, upperQuarter);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    builder.KeyframeWarpTo(leftQuarter, centerY);
                    builder.KeyframeAxisLine(path.X, rightQuarter);
                    break;

                case 'g':
                    builder.KeyframeInitialize(right, upperQuarter);
                    LowercaseCircleMacro();
                    builder.KeyframeWarpTo(right, lowercaseTop);
                    builder.KeyframeAxisLine(path.Y, lowercaseBottom - quarterHeight);
                    builder.KeyframeArcVertical(centerX, lowercaseBottom);
                    builder.KeyframeArcHorizontal(left, lowercaseBottom - quarterHeight);
                    break;

                case 'h':
                    builder.KeyframeInitialize(left, top);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    builder.KeyframeWarpTo(left, armHeight);
                    builder.KeyframeArcVertical(centerX, lowercaseTop);
                    builder.KeyframeArcHorizontal(right, armHeight);
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
                    builder.KeyframeArcVertical(centerX, lowercaseBottom);
                    builder.KeyframeArcHorizontal(left, lowercaseBottom - quarterHeight);
                    break;

                case 'k':
                    builder.KeyframeInitialize(left, top);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    builder.KeyframeAxisLine(path.Y, centerY);
                    builder.KeyframeArcBPartial(right, top, 0, 0.75f);
                    builder.KeyframeWarpTo(left, centerY);
                    builder.KeyframeArcHorizontal(right, bottom);
                    break;

                case 'l':
                    builder.KeyframeInitialize(centerX, top);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    break;

                case 'm':
                    builder.KeyframeInitialize(left, lowercaseTop);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    builder.KeyframeWarpTo(left, armHeight);
                    builder.KeyframeArcVertical(leftQuarter, lowercaseTop);
                    builder.KeyframeArcHorizontal(centerX, armHeight);
                    builder.KeyframeArcVertical(rightQuarter, lowercaseTop);
                    builder.KeyframeArcHorizontal(right, armHeight);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    break;

                case 'n':
                    builder.KeyframeInitialize(left, lowercaseTop);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    builder.KeyframeWarpTo(left, armHeight);
                    builder.KeyframeArcVertical(centerX, lowercaseTop);
                    builder.KeyframeArcHorizontal(right, armHeight);
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
                    builder.KeyframeArcVertical(centerX, lowercaseTop);
                    builder.KeyframeArcHorizontal(right, armHeight);
                    break;

                case 's':
                    builder.KeyframeInitialize(right, lowercaseVerticalCenter - eightHeight);
                    builder.KeyframeArcVertical(centerX, lowercaseTop);
                    builder.KeyframeArcHorizontal(left, lowercaseVerticalCenter - eightHeight);
                    builder.KeyframeArcVertical(centerX, lowercaseVerticalCenter);
                    builder.KeyframeArcHorizontal(right, lowercaseVerticalCenter + eightHeight);
                    builder.KeyframeArcVertical(centerX, bottom);
                    builder.KeyframeArcHorizontal(left, lowercaseVerticalCenter + eightHeight);
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
                    builder.KeyframeArcVertical(centerX, bottom);
                    builder.KeyframeArcHorizontal(right, lowercaseVerticalCenter);
                    builder.KeyframeAxisLine(path.Y, lowercaseTop);
                    break;

                case 'v':
                    builder.KeyframeInitialize(left, lowercaseTop);
                    builder.KeyframeLineTo(centerX, bottom);
                    builder.KeyframeLineTo(right, lowercaseTop);
                    break;

                case 'w':
                    builder.KeyframeInitialize(left, lowercaseTop);
                    builder.KeyframeArcVertical(left / 2, bottom);
                    builder.KeyframeArcHorizontal(centerX, lowercaseTop);
                    builder.KeyframeArcVertical(right / 2, bottom);
                    builder.KeyframeArcHorizontal(right, lowercaseTop);
                    break;

                case 'x':
                    builder.KeyframeInitialize(left, lowercaseTop);
                    builder.KeyframeArcVertical(centerX, lowercaseVerticalCenter);
                    builder.KeyframeArcHorizontal(right, bottom);
                    builder.KeyframeWarpTo(right, lowercaseTop);
                    builder.KeyframeArcVertical(centerX, lowercaseVerticalCenter);
                    builder.KeyframeArcHorizontal(left, bottom);
                    break;

                case 'y':
                    builder.KeyframeInitialize(left, lowercaseTop);
                    builder.KeyframeAxisLine(path.Y, lowercaseVerticalCenter);
                    builder.KeyframeArcVertical(centerX, bottom);
                    builder.KeyframeArcHorizontal(right, lowercaseVerticalCenter);
                    builder.KeyframeWarpTo(right, lowercaseTop);
                    builder.KeyframeAxisLine(path.Y, lowercaseBottom - quarterHeight);
                    builder.KeyframeArcVertical(centerX, lowercaseBottom);
                    builder.KeyframeArcHorizontal(left, lowercaseBottom - quarterHeight);
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
                    builder.KeyframeArcVertical(centerX, top + sixteenthHeight);
                    builder.KeyframeArcHorizontal(left, upperQuarter);
                    builder.KeyframeArcVertical(centerX, centerY);
                    builder.KeyframeArcHorizontal(right, lowerQuarter);
                    builder.KeyframeArcVertical(centerX, bottom - sixteenthHeight);
                    builder.KeyframeArcHorizontal(left, lowerQuarter);
                    builder.KeyframeWarpTo(centerX, top);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    break;

                case '%':
                    builder.KeyframeInitialize(right, top);
                    builder.KeyframeLineTo(left, bottom);
                    builder.KeyframeWarpTo(leftQuarter, top);
                    builder.KeyframeArcHorizontal(left, upperQuarter);
                    builder.KeyframeArcVertical(leftQuarter, centerY);
                    builder.KeyframeArcHorizontal(centerX, upperQuarter);
                    builder.KeyframeArcVertical(leftQuarter, top);
                    builder.KeyframeWarpTo(rightQuarter, centerY);
                    builder.KeyframeArcHorizontal(right, lowerQuarter);
                    builder.KeyframeArcVertical(rightQuarter, bottom);
                    builder.KeyframeArcHorizontal(centerX, lowerQuarter);
                    builder.KeyframeArcVertical(rightQuarter, centerY);
                    
                    
                    break;

                case '&':
                    builder.KeyframeInitialize(rightQuarter, upperQuarter);
                    builder.KeyframeArcVertical(centerX, top);
                    builder.KeyframeArcHorizontal(left, upperQuarter);
                    builder.KeyframeLineTo(right, bottom);
                    builder.KeyframeWarpTo(centerX, centerY);
                    builder.KeyframeArcHorizontal(left, bottom - quarterHeight);
                    builder.KeyframeArcVertical(centerX, bottom);
                    builder.KeyframeArcHorizontal(right, lowerQuarter);
                    break;

                case '\'':
                    builder.KeyframeInitialize(centerX, top);
                    builder.KeyframeAxisLine(path.Y, upperQuarter);
                    break;

                case '(':
                    builder.KeyframeInitialize(centerX, top);
                    builder.KeyframeArcHorizontal(left, centerY);
                    builder.KeyframeArcVertical(centerX, bottom);
                    break;

                case ')':
                    builder.KeyframeInitialize(centerX, top);
                    builder.KeyframeArcHorizontal(right, centerY);
                    builder.KeyframeArcVertical(centerX, bottom);
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
                    builder.KeyframeArcVertical(leftQuarter, lowercaseBottom);
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
                    builder.KeyframeArcVertical(leftQuarter, lowercaseBottom);
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
                    builder.KeyframeArcVertical(centerX, top);
                    builder.KeyframeArcHorizontal(right, upperQuarter);
                    builder.KeyframeArcVertical(rightQuarter, centerY);
                    builder.KeyframeArcHorizontal(centerX, centerY + eightHeight);
                    builder.KeyframeWarpTo(centerX, bottom);
                    builder.KeyframeLineTo(centerX, bottom - sixteenthHeight);
                    break;

                case '@':
                    builder.KeyframeInitialize(right, centerY);
                    builder.KeyframeArcVertical(rightQuarter, upperQuarter);
                    builder.KeyframeArcHorizontal(centerX, centerY);
                    builder.KeyframeArcVertical(rightQuarter, lowerQuarter);
                    builder.KeyframeArcHorizontal(right, centerY);
                    builder.KeyframeArcVertical(centerX, top);
                    builder.KeyframeArcHorizontal(left, centerY);
                    builder.KeyframeArcVertical(left, lowerQuarter);
                    builder.KeyframeArcVertical(centerX, bottom);
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
                    builder.KeyframeArcHorizontal(leftQuarter, upperQuarter);
                    builder.KeyframeArcVertical(left, centerY);
                    builder.KeyframeArcHorizontal(leftQuarter, lowerQuarter);
                    builder.KeyframeArcVertical(centerX, bottom);
                    break;

                case '|':
                    builder.KeyframeInitialize(centerX, top);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    break;

                case '}':
                    builder.KeyframeInitialize(centerX, top);
                    builder.KeyframeArcHorizontal(rightQuarter, upperQuarter);
                    builder.KeyframeArcVertical(right, centerY);
                    builder.KeyframeArcHorizontal(rightQuarter, lowerQuarter);
                    builder.KeyframeArcVertical(centerX, bottom);
                    break;

                case '~':
                    builder.KeyframeInitialize(left, centerY + sixteenthHeight);
                    builder.KeyframeArcVertical(leftQuarter, centerY - sixteenthHeight);
                    builder.KeyframeArcHorizontal(centerX, centerY);
                    builder.KeyframeArcVertical(rightQuarter, centerY + sixteenthHeight);
                    builder.KeyframeArcHorizontal(right, centerY - sixteenthHeight);
                    break;

                case '0':
                    builder.KeyframeInitialize(right, centerY);
                    builder.KeyframeArcVertical(centerX, bottom);
                    builder.KeyframeArcHorizontal(left, centerY);
                    builder.KeyframeArcVertical(centerX, top);
                    builder.KeyframeArcHorizontal(right, centerY);
                    break;
                
                case '1':
                    builder.KeyframeInitialize(left, upperQuarter);
                    builder.KeyframeArcHorizontal(centerX, top);
                    builder.KeyframeAxisLine(path.Y, bottom);
                    break;

                case '2':
                    builder.KeyframeInitialize(left, upperQuarter);
                    builder.KeyframeArcVertical(centerX, top);
                    builder.KeyframeArcHorizontal(right, upperQuarter);
                    builder.KeyframeArcVertical(centerX, centerY);
                    builder.KeyframeArcHorizontal(left, bottom);
                    builder.KeyframeAxisLine(path.X, right);
                    break;

                case '3':
                    builder.KeyframeInitialize(left, upperQuarter);
                    builder.KeyframeArcVertical(centerX, top);
                    builder.KeyframeArcHorizontal(right, upperQuarter);
                    builder.KeyframeArcVertical(centerX, centerY);
                    builder.KeyframeArcHorizontal(right, lowerQuarter);
                    builder.KeyframeArcVertical(centerX, bottom);
                    builder.KeyframeArcHorizontal(left, lowerQuarter);
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
                    builder.KeyframeArcHorizontal(right, lowerQuarter);
                    builder.KeyframeArcVertical(centerX, bottom);
                    builder.KeyframeArcHorizontal(left, lowerQuarter);
                    break;
                
                case '6':
                    builder.KeyframeInitialize(right, upperQuarter);
                    builder.KeyframeArcVertical(centerX, top);
                    builder.KeyframeArcHorizontal(left, upperQuarter);
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
                    builder.KeyframeArcHorizontal(left, upperQuarter);
                    builder.KeyframeArcVertical(centerX, top);
                    builder.KeyframeArcHorizontal(right, upperQuarter);
                    builder.KeyframeArcVertical(centerX, centerY);
                    builder.KeyframeArcHorizontal(left, lowerQuarter);
                    builder.KeyframeArcVertical(centerX, bottom);
                    builder.KeyframeArcHorizontal(right, lowerQuarter);
                    builder.KeyframeArcVertical(centerX, centerY);
                    break;
                
                case '9':
                    builder.KeyframeInitialize(centerX, centerY);
                    builder.KeyframeArcHorizontal(left, upperQuarter);
                    builder.KeyframeArcVertical(centerX, top);
                    builder.KeyframeArcHorizontal(right, upperQuarter);
                    builder.KeyframeArcVertical(centerX, centerY);
                    builder.KeyframeWarpTo(right, upperQuarter);
                    builder.KeyframeAxisLine(path.Y, lowerQuarter);
                    builder.KeyframeArcVertical(centerX, bottom);
                    builder.KeyframeArcHorizontal(left, lowerQuarter);
                    break;

                default:
                    // By default we draw a circle
                    builder.KeyframeInitialize(right, centerY);
                    builder.KeyframeArcVertical(centerX, bottom);
                    builder.KeyframeArcHorizontal(left, centerY);
                    builder.KeyframeArcVertical(centerX, top);
                    builder.KeyframeArcHorizontal(right, centerY);
                    break;
            }
            
            
            Typeface.Cache.Add(letter, path);
            return path;
        }
    }
}
