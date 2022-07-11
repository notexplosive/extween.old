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
    }
}
