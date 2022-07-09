using System;
using System.Collections.Generic;

namespace ExTween
{
    public class TweenCollection
    {
        protected List<ITween> items = new();

        public void ForEachItem(Action<ITween> action)
        {
            foreach (var item in this.items)
            {
                action(item);
            }
        }

        public void Reset()
        {
            ForEachItem(item => item.Reset());
        }
    }

    public class MultiplexTween : TweenCollection, ITween
    {
        public float UpdateAndGetOverflow(float dt)
        {
            float totalOverflow = 0;
            var hasTried = false;
            ForEachItem(
                tween =>
                {
                    var pendingOverflow = tween.UpdateAndGetOverflow(dt);
                    if (!hasTried)
                    {
                        hasTried = true;
                        totalOverflow = pendingOverflow;
                    }
                    else
                    {
                        totalOverflow = MathF.Min(totalOverflow, pendingOverflow);
                    }
                });

            return totalOverflow;
        }

        public bool IsDone()
        {
            var result = true;
            ForEachItem(item => result = result && item.IsDone());
            return result;
        }

        public MultiplexTween AddChannel(ITween tween)
        {
            this.items.Add(tween);
            return this;
        }

        public float TotalDuration
        {
            get
            {
                var result = 0f;
                ForEachItem(item => result = Math.Max(result, item.TotalDuration));
                return result;
            }
        }
    }
}
