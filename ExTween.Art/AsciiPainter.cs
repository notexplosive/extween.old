namespace ExTween.Art
{
    public class AsciiPainter : Painter
    {
        private readonly char[,] canvas;
        private readonly IntXyPair size;

        public AsciiPainter(IntXyPair size)
        {
            this.size = size;
            this.canvas = new char[size.X, size.Y];
        }

        public void DrawPixel(IntXyPair point, char pixel)
        {
            this.canvas[point.X, point.Y] = pixel;
        }

        public override void DrawLine(FloatXyPair p1, FloatXyPair p2, float thickness, StrokeColor strokeColor)
        {
            DrawAsciiLine(p1, p2, strokeColor == StrokeColor.Black ? 'x' : '.');
        }

        private void DrawAsciiLine(FloatXyPair start, FloatXyPair end, char pixel)
        {
            var tweenable = new TweenableFloatXyPair(start);

            var tween = new Tween<FloatXyPair>(tweenable, end, 1, Ease.Linear);
            var length = (start - end).Length();

            if (length > 0)
            {
                for (float f = 0; f < length; f += 1 / length / 2f)
                {
                    tween.JumpTo(f);
                    DrawPixel(tweenable.Value.ToIntXy(), pixel);
                }
            }
        }
    }
}
