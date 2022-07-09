using ExTween;
using FluentAssertions;
using Xunit;

namespace TestExTween
{
    public class MultiplexTweenTests
    {
        [Fact]
        public void multiplex_tweens_increment_both_children()
        {
            var tweenableA = new TweenableInt(0);
            var tweenableB = new TweenableInt(0);

            var tween = new MultiplexTween()
                .AddChannel(new Tween<int>(tweenableA, 100, 1, EaseFunctions.Linear))
                .AddChannel(new Tween<int>(tweenableB, 200, 1, EaseFunctions.Linear));

            tween.UpdateAndGetOverflow(0.5f);

            tweenableA.Value.Should().Be(50);
            tweenableB.Value.Should().Be(100);
        } 
    }
}
