using ExTween;
using FluentAssertions;
using Xunit;

namespace TestExTween
{
    public class TweenTests
    {
        [Fact]
        public void linear_lerp()
        {
            var tweenable = new TweenableInt(-100);
            var tween = new Tween(tweenable, 100, 1, EaseFunctions.Linear);

            tween.updateAndGetOverflow(0.5f);

            tween.CurrentTime.Should().Be(0.5f);
            tweenable.Value.Should().Be(0);
        }
    }
}