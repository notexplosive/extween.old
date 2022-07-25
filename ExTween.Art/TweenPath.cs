using System;
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

        public PathBuilder Builder => new PathBuilder(this);

        public float Duration => Tween.TotalDuration.Get();

        public TweenPathState GetPreciseStateAtTime(float time)
        {
            Tween.JumpTo(time);
            return new TweenPathState(new FloatXyPair(X.Value, Y.Value), ShouldDraw.Value == 1);
        }

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

        public class PathBuilder
        {
            private const float Duration = 1f;
            private readonly TweenPath path;

            public PathBuilder(TweenPath path)
            {
                this.path = path;
            }

            private void Keyframe(ITween subTween)
            {
                this.path.AddKeyframe(this.path.Duration);
                this.path.Tween.Add(subTween);
            }

            private ITween SetXY(float x, float y)
            {
                return new CallbackTween(
                    () =>
                    {
                        this.path.X.Value = x;
                        this.path.Y.Value = y;
                    });
            }

            private ITween AxisLine(Tweenable<float> tweenable, float destination)
            {
                return new Tween<float>(tweenable, destination, PathBuilder.Duration, Ease.Linear);
            }

            private ITween ArcA(float x, float y)
            {
                return new MultiplexTween()
                    .AddChannel(new Tween<float>(this.path.X, x, PathBuilder.Duration, Ease.SineSlowFast))
                    .AddChannel(new Tween<float>(this.path.Y, y, PathBuilder.Duration, Ease.SineFastSlow));
            }

            private ITween ArcB(float x, float y)
            {
                return new MultiplexTween()
                    .AddChannel(new Tween<float>(this.path.X, x, PathBuilder.Duration, Ease.SineFastSlow))
                    .AddChannel(new Tween<float>(this.path.Y, y, PathBuilder.Duration, Ease.SineSlowFast));
            }

            private ITween LineTo(float x, float y)
            {
                return new MultiplexTween()
                    .AddChannel(new Tween<float>(this.path.X, x, PathBuilder.Duration, Ease.Linear))
                    .AddChannel(new Tween<float>(this.path.Y, y, PathBuilder.Duration, Ease.Linear));
            }

            private ITween Enable()
            {
                return new CallbackTween(() => { this.path.ShouldDraw.Value = 1; });
            }

            private ITween Disable()
            {
                return new CallbackTween(() => { this.path.ShouldDraw.Value = 0; });
            }

            private ITween DrawPercentOf(ITween subTween, float startPercent, float endPercent)
            {
                // Add a keyframe at the end of the shorter tween
                var startSeconds = subTween.TotalDuration.Get() * startPercent;
                var endSeconds = subTween.TotalDuration.Get() * endPercent;
                this.path.AddKeyframe(this.path.Duration + startSeconds);
                this.path.AddKeyframe(this.path.Duration + endSeconds);

                var result = new SequenceTween();

                if (startPercent > 0)
                {
                    result.Add(Disable());
                }

                result.Add(new MultiplexTween()
                    .AddChannel(new SequenceTween()
                        .Add(new WaitSecondsTween(startSeconds))
                        .Add(Enable())
                        .Add(new WaitSecondsTween(endSeconds - startSeconds))
                        .Add(Disable())
                    )
                    .AddChannel(subTween)
                );

                result.Add(Enable());

                return result;
            }

            private ITween WarpTo(float x, float y)
            {
                Keyframe(DrawPercentOf(LineTo(x, y), 0f, 0f));
                return Enable();
            }

            private ITween Initialize(float x, float y, bool startEnabled)
            {
                var result = new SequenceTween()
                        .Add(SetXY(x, y))
                    ;

                result.Add(startEnabled ? Enable() : Disable());

                return result;
            }

            public void KeyframeInitialize(float x, float y, bool startEnabled = true)
            {
                Keyframe(Initialize(x, y, startEnabled));
            }

            public void KeyframeAxisLine(TweenableFloat axis, float destination)
            {
                Keyframe(AxisLine(axis, destination));
            }

            public void KeyframeArcA(float x, float y)
            {
                Keyframe(ArcA(x, y));
            }

            public void KeyframeArcB(float x, float y)
            {
                Keyframe(ArcB(x, y));
            }
            
            public void KeyframeArcAPartial(float x, float y, float startPercent, float endPercent)
            {
                KeyframeDrawPercentOf(ArcA(x, y), startPercent, endPercent);
            }

            public void KeyframeArcBPartial(float x, float y, float startPercent, float endPercent)
            {
                KeyframeDrawPercentOf(ArcB(x, y), startPercent, endPercent);
            }

            public void KeyframeWarpTo(float x, float y)
            {
                Keyframe(WarpTo(x, y));
            }

            public void KeyframeLineTo(float x, float y)
            {
                Keyframe(LineTo(x, y));
            }

            private void KeyframeDrawPercentOf(ITween subTween, float startPercent, float endPercent)
            {
                Keyframe(DrawPercentOf(subTween, startPercent, endPercent));
            }
        }
    }
}
