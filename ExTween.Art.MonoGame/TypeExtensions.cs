using Microsoft.Xna.Framework;

namespace ExTween.Art.MonoGame
{
    public static class TypeExtensions
    {
        // public static operator Vector2()
            
        public static IntXyPair ToXyPair(this Point point)
        {
            return new IntXyPair(point.X, point.Y);
        }
        
        public static FloatXyPair ToXyPair(this Vector2 point)
        {
            return new FloatXyPair(point.X, point.Y);
        }
        
        public static Point ToPoint(this IntXyPair xy)
        {
            return new Point(xy.X, xy.Y);
        }

        public static Vector2 ToVector2(this FloatXyPair xy)
        {
            return new Vector2(xy.X, xy.Y);
        }

        public static Color ToMgColor(this StrokeColor strokeColor)
        {
            if (strokeColor == StrokeColor.Black)
            {
                return Color.Black;
            }

            return Color.Transparent;
        }
    }
}
