using System.Text;

namespace ExTween.Art
{
    public class AsciiPainter : Painter
    {
        private readonly char[,] canvas;
        private readonly IntXyPair size;

        public AsciiPainter(IntXyPair size)
        {
            this.size = size;
            this.canvas = new char[this.size.X, this.size.Y];

            for (var y = 0; y < this.size.Y; y++)
            {
                for (var x = 0; x < this.size.X; x++)
                {
                    this.canvas[x, y] = ' ';
                }
            }
        }

        public void DrawPixel(IntXyPair point, char pixel)
        {
            if (point.X < this.size.X && point.X >= 0 && point.Y < this.size.Y && point.Y >= 0)
            {
                this.canvas[point.X, point.Y] = pixel;
            }
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
                for (float f = 0; f < length; f += 1 / length / 4f)
                {
                    tween.JumpTo(f);
                    DrawPixel(tweenable.Value.ToIntXy(), pixel);
                }
            }
        }

        public string RenderString()
        {
            var output = new StringBuilder();
            for (var y = 0; y < this.size.Y; y++)
            {
                for (var x = 0; x < this.size.X; x++)
                {
                    output.Append(this.canvas[x, y]);
                }

                output.AppendLine();
            }

            return output.ToString();
        }
    }
}
