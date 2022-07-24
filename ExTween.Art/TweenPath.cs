using System;
using System.Collections.Generic;

namespace ExTween.Art
{
    public class TweenPath : ITweenRendered
    {
        private readonly List<float> keyframesInSeconds = new List<float>();
        private int cachedNumberOfSegments;
        private float[] cachedPercentKeyframes = Array.Empty<float>();
        public SequenceTween Tween { get; } = new SequenceTween();
        public TweenableFloat X { get; } = new TweenableFloat();
        public TweenableFloat Y { get; } = new TweenableFloat();
        public TweenableInt ShouldDraw { get; } = new TweenableInt(1);

        public float Duration => Tween.TotalDuration.Get();

        public void AddKeyframe(float timeInSeconds)
        {
            if (!this.keyframesInSeconds.Contains(timeInSeconds))
            {
                this.keyframesInSeconds.Add(timeInSeconds);
            }
        }

        public float[] GetKeyframes(int numberOfSegments)
        {
            if (numberOfSegments > this.cachedPercentKeyframes.Length ||
                numberOfSegments != this.cachedNumberOfSegments)
            {
                BakeKeyframes(numberOfSegments);
            }

            return this.cachedPercentKeyframes;
        }

        public void BakeKeyframes(int numberOfSegments)
        {
            var allKeyframes = new List<float>(this.keyframesInSeconds);

            var expectedTotalCount = Math.Max(this.keyframesInSeconds.Count, numberOfSegments);
            var numberOfFramesToAdd = expectedTotalCount - this.keyframesInSeconds.Count;

            for (var i = 0; i < numberOfFramesToAdd; i++)
            {
                var percent = (float) i / numberOfFramesToAdd;
                allKeyframes.Add(percent * Duration);
            }

            // Add a keyframe at the very end
            allKeyframes.Add(Duration);

            allKeyframes.Sort();

            this.cachedPercentKeyframes = allKeyframes.ToArray();
            this.cachedNumberOfSegments = numberOfSegments;
        }

        public State GetPreciseStateAtTime(float time)
        {
            Tween.JumpTo(time);
            return new State(new FloatXyPair(X.Value, Y.Value), ShouldDraw.Value == 1);
        }

        public State GetApproximateStateAtTime(float time)
        {
            if (time > Duration)
            {
                return GetPreciseStateAtTime(Duration);
            }
            
            var keyframeIndex = GetEarliestKeyframeFromTime(time);

            var stateBefore = GetPreciseStateAtTime(this.keyframesInSeconds[keyframeIndex]);
            var stateAfter = GetPreciseStateAtTime(this.keyframesInSeconds[keyframeIndex + 1]);

            var adjustedTime = time - this.keyframesInSeconds[keyframeIndex];
            var maxAdjustedTime = this.keyframesInSeconds[keyframeIndex + 1] - this.keyframesInSeconds[keyframeIndex];
            var percent = adjustedTime / maxAdjustedTime;

            return new State(FloatXyPair.Lerp(stateBefore.Position, stateAfter.Position, percent), stateBefore.ShouldDraw);
        }

        private int GetEarliestKeyframeFromTime(float time)
        {
            for (var i = 0; i < this.keyframesInSeconds.Count - 1; i++)
            {
                if (this.keyframesInSeconds[i] > time && this.keyframesInSeconds[i + 1] < time)
                {
                    return i;
                }
            }

            if (time < 0)
            {
                return 0;
            }
            
            if (time > Duration)
            {
                return this.keyframesInSeconds.Count;
            }

            // Shouldn't ever hit this return
            return 0;
        }

        public readonly struct State
        {
            public FloatXyPair Position { get; }
            public bool ShouldDraw { get; }

            public State(FloatXyPair position, bool shouldDraw)
            {
                Position = position;
                ShouldDraw = shouldDraw;
            }
        }
    }
}
