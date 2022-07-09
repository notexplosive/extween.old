using ExTween;
using FluentAssertions;
using Xunit;

namespace TestExTween
{
    public class TweenTests
    {
        [Fact]
        public void int_linear_lerp()
        {
            var tweenable = new TweenableInt(-100);
            var tween = new Tween<int>(tweenable, 100, 1, EaseFunctions.Linear);

            tween.UpdateAndGetOverflow(0.25f);
            tween.UpdateAndGetOverflow(0.25f);

            tween.CurrentTime.Should().Be(0.5f);
            tweenable.Value.Should().Be(0);
        }
        
        [Fact]
        public void captured_float()
        {
            // This represents some external data. Maybe it's your altitude off the ground?
            float captureMe = 0f;
            // To capture argument, we have to do this boilerplate, there's no way to shorten this.
            //      You must have a GETTER function that returns captured variable
            //      And a SETTER function that takes a value, and assigns it to the captured variable
            var tweenable = new TweenableFloat(() => captureMe, val => captureMe = val);
         
            // Create a tween and advance through it
            var tween = new Tween<float>(tweenable, 3.14f, 1, EaseFunctions.Linear);
            tween.UpdateAndGetOverflow(1f);

            captureMe.Should().Be(3.14f); // the value has changed!
            tweenable.Value.Should().Be(3.14f); // the tweenable agrees that the value has changed.
        }

        [Fact]
        public void long_duration()
        {
            var tweenable = new TweenableInt(0);

            var tween = new Tween<int>(tweenable, 100, 50, EaseFunctions.Linear);
            tween.UpdateAndGetOverflow(5);

            tweenable.Value.Should().Be(10);
        }
    }
}