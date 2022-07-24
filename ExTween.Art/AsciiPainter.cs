using System;
using System.Collections.Generic;
using System.Text;

namespace ExTween.Art
{
    public class AsciiPainter : Painter
    {
        private readonly char[,] canvas;
        private readonly IntXyPair size;
        private readonly List<Tuple<IntXyPair, char>> record = new List<Tuple<IntXyPair, char>>();
        private readonly List<Tuple<IntXyPair, char>> offscreenRecord = new List<Tuple<IntXyPair, char>>();

        public AsciiPainter(IntXyPair size)
        {
            this.size = size + new IntXyPair(1, 1);
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
            var tuple = new Tuple<IntXyPair, char>(point, pixel);
            if (point.X < this.size.X && point.X >= 0 && point.Y < this.size.Y && point.Y >= 0)
            {
                this.canvas[point.X, point.Y] = pixel;
                this.record.Add(tuple);
            }
            else
            {
                this.offscreenRecord.Add(tuple);
            }
        }

        public override void DrawLine(FloatXyPair p1, FloatXyPair p2, float thickness, StrokeColor strokeColor)
        {
            DrawAsciiLine(p1, p2, strokeColor == StrokeColor.Black ? 'x' : '.');
        }

        public override void DrawFilledCircle(FloatXyPair position, float radius, int segments, StrokeColor strokeColor)
        {
            for (var y = 0; y < this.size.Y; y++)
            {
                for (var x = 0; x < this.size.X; x++)
                {
                    if ((position - new FloatXyPair(x, y)).Length() < radius)
                    {
                        DrawPixel(new IntXyPair(x, y), strokeColor == StrokeColor.Black ? 'x' : '.');
                    }
                }
            }
        }

        private void DrawAsciiLine(FloatXyPair start, FloatXyPair end, char pixel)
        {
            var tweenable = new TweenableFloatXyPair(start);

            var length = (start - end).Length();
            var duration = MathF.Ceiling(length);
            var tween = new Tween<FloatXyPair>(tweenable, end, duration, Ease.Linear);

            if (length > 0)
            {
                for (float f = 0; f <= duration; f += 1f)
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

            string extra = string.Empty;
            if (this.offscreenRecord.Count > 0)
            {
                extra = "\nOffscreen:\n" + RenderRecordString(this.offscreenRecord);
            }
            
            return output.ToString() + extra;
        }

        public string RenderStringAndRecord()
        {
            return RenderString() + "\nRecord:\n" + RenderRecordString(this.record);
        }

        private string? RenderRecordString(List<Tuple<IntXyPair, char>> recordEntries)
        {
            var recordString = string.Empty;

            foreach (var recordEntry in recordEntries)
            {
                recordString += $"{recordEntry.Item1}, {recordEntry.Item2}\n";
            }

            return recordString;
        }
    }
}
