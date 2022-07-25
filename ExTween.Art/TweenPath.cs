﻿using System;
using System.Collections.Generic;

namespace ExTween.Art
{
    public class TweenPath : ITweenPath
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

        public TweenPathState GetPreciseStateAtTime(float time)
        {
            Tween.JumpTo(time);
            return new TweenPathState(new FloatXyPair(X.Value, Y.Value), ShouldDraw.Value == 1);
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
    }
}
