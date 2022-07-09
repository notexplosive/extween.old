using ExTween;
using FluentAssertions;
using Xunit;

namespace TestExTween
{
    public class SequenceTweenTests
    {
        [Fact]
        public void sequences_can_have_just_one_item()
        {
            var tweenable = new TweenableFloat(0);
            var sequence = new SequenceTween();
            sequence.Add(new Tween<float>(tweenable, 100, 1, EaseFunctions.Linear));
        }

        [Fact]
        public void transition_to_next_item()
        {
            var tweenable = new TweenableFloat(0);
            var sequence = new SequenceTween();
            sequence.Add(new Tween<float>(tweenable, 100, 0.5f, EaseFunctions.Linear));
            sequence.Add(new Tween<float>(tweenable, 200, 1, EaseFunctions.Linear));

            sequence.UpdateAndGetOverflow(0.75f);

            tweenable.Value.Should().Be(125);
        }

        [Fact]
        public void consecutive_callback_tweens_fire_all_at_once()
        {
            var tweenable = new TweenableInt(0);
            var tween = new SequenceTween();
            var hitCount = 0;

            void Hit()
            {
                hitCount++;
            }

            tween.Add(new WaitSecondsTween(1f));
            tween.Add(new CallbackTween(Hit));
            tween.Add(new CallbackTween(Hit));
            tween.Add(new CallbackTween(Hit));
            tween.Add(new Tween<int>(tweenable, 10, 1f, EaseFunctions.Linear));

            tween.UpdateAndGetOverflow(1.5f);

            tweenable.Value.Should().Be(5);
            hitCount.Should().Be(3);
        }

        [Fact]
        public void wait_until_should_halt_sequence_when_false()
        {
            var tween = new SequenceTween();
            var hitCount = 0;

            void Hit()
            {
                hitCount++;
            }

            tween.Add(new CallbackTween(Hit));
            tween.Add(new WaitUntilTween(() => false));
            tween.Add(new CallbackTween(Hit));

            tween.UpdateAndGetOverflow(0);
            tween.UpdateAndGetOverflow(0);

            hitCount.Should().Be(1);
        }

        [Fact]
        public void wait_until_should_permit_sequence_when_true()
        {
            var tween = new SequenceTween();
            var hitCount = 0;

            void Hit()
            {
                hitCount++;
            }

            tween.Add(new CallbackTween(Hit));
            tween.Add(new WaitUntilTween(() => true));
            tween.Add(new CallbackTween(Hit));

            tween.UpdateAndGetOverflow(0);

            hitCount.Should().Be(2);
        }

        [Fact]
        public void adding_to_a_sequence_after_its_done_makes_it_not_done()
        {
            var tweenable = new TweenableInt();
            var tween = new SequenceTween();
            tween.Add(new Tween<int>(tweenable, 100, 1, EaseFunctions.Linear));

            tween.UpdateAndGetOverflow(1.2f);
            tween.Add(new Tween<int>(tweenable, 120, 1, EaseFunctions.Linear));
            tween.UpdateAndGetOverflow(0.5f);

            tweenable.Value.Should().Be(110);
        }
    }
}
