using ApprovalTests;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using ExTween.Art;
using Xunit;

namespace TestExTween.Art
{
    [UseReporter(typeof(RiderReporter))]
    [UseApprovalSubdirectory("ApprovalTests")]
    public class AsciiRendererApprovals
    {
        [Fact]
        public void ascii_basic()
        {
            var painter = new AsciiPainter(new IntXyPair(10, 10));
            painter.DrawLine(new FloatXyPair(0, 0), new FloatXyPair(10, 10), 1, StrokeColor.Black);
            painter.DrawLine(new FloatXyPair(0, 10), new FloatXyPair(10, 0), 1, StrokeColor.Black);
            painter.DrawLine(new FloatXyPair(0, 5), new FloatXyPair(10, 5), 1, StrokeColor.Black);

            Approvals.Verify(painter.RenderString());
        }

        [Fact]
        public void ascii_line_segment()
        {
            var painter = new AsciiPainter(new IntXyPair(15, 15));
            painter.DrawLine(new FloatXyPair(3, 5), new FloatXyPair(10, 7), 1, StrokeColor.Black);

            Approvals.Verify(painter.RenderStringAndRecord());
        }
    }
}
