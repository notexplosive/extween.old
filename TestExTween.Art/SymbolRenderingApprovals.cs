using ApprovalTests;
using ApprovalTests.Namers;
using ApprovalTests.Reporters;
using ExTween.Art;
using Xunit;

namespace TestExTween.Art
{
    [UseReporter(typeof(RiderReporter))]
    [UseApprovalSubdirectory("ApprovalTests")]
    public class SymbolRenderingApprovals
    {
        [Fact]
        public void render_every_letter()
        {
            var font = new MonospaceFont(20);
            var glyph = font.GetTweenGlyphForLetter('H');
            glyph.RenderOffset = glyph.Size / 2;
            glyph.NumberOfSegments = 100;

            var painter = new AsciiPainter(glyph.Size.ToIntXy() + new IntXyPair(1, 0));
            glyph.Draw(painter);

            Approvals.Verify(painter.RenderString());
        }
    }
}
