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

        [Fact]
        public void multiplex_tween_continues_when_one_tween_is_done()
        {
            var tweenableA = new TweenableInt(0);
            var tweenableB = new TweenableInt(0);

            var tween = new MultiplexTween()
                .AddChannel(new Tween<int>(tweenableA, 100, 1, EaseFunctions.Linear))
                .AddChannel(new Tween<int>(tweenableB, 200, 2, EaseFunctions.Linear));

            tween.UpdateAndGetOverflow(1.5f);

            tweenableA.Value.Should().Be(100);
            tweenableB.Value.Should().Be(150);
        }
        
        [Fact]
        public void multiplex_tween_reports_correct_overflow()
        {
            var tweenableA = new TweenableInt(0);
            var tweenableB = new TweenableInt(0);

            var tween = new MultiplexTween()
                .AddChannel(new Tween<int>(tweenableA, 100, 0.25f, EaseFunctions.Linear))
                .AddChannel(new Tween<int>(tweenableB, 200, 1f, EaseFunctions.Linear));

            var overflow = tween.UpdateAndGetOverflow(1.2f);

            overflow.Should().BeApproximately(0.2f, 0.00001f);
        }

        [Fact]
        public void multiplex_only_fires_callback_once_when_stalled()
        {
            int hitCount = 0;

            var tween = new MultiplexTween()
                .AddChannel(new WaitSecondsTween(2))
                .AddChannel(new CallbackTween(() => { hitCount++; }));

            tween.UpdateAndGetOverflow(0.25f);
            tween.UpdateAndGetOverflow(0.25f);

            hitCount.Should().Be(1);
        }

        [Fact]
        public void multiplex_tween_reports_correct_duration()
        {
            var tween = new MultiplexTween()
                .AddChannel(new WaitSecondsTween(2))
                .AddChannel(new WaitSecondsTween(7))
                .AddChannel(new WaitSecondsTween(4))
                .AddChannel(new WaitSecondsTween(5));

            tween.TotalDuration.Should().Be(7);
        }
        
        [Fact]
        public void adding_to_a_multiplex_after_its_done_makes_it_not_done()
        {
            var tweenableA = new TweenableInt(0);
            var tweenableB = new TweenableInt(0);
            var tween = new MultiplexTween();
            tween.AddChannel(new Tween<int>(tweenableA, 100, 1, EaseFunctions.Linear));

            tween.UpdateAndGetOverflow(1.2f);
            tween.AddChannel(new Tween<int>(tweenableB, 100, 1, EaseFunctions.Linear));
            tween.UpdateAndGetOverflow(0.5f);

            tweenableA.Value.Should().Be(100);
            tweenableB.Value.Should().Be(50);
        }
    }
}
