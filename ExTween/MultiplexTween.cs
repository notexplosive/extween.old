using System;

namespace ExTween
{
    public class MultiplexTween : TweenCollection, ITween
    {
        public float Update(float dt)
        {
            float totalOverflow = 0;
            var hasTried = false;
            ForEachItem(
                tween =>
                {
                    var pendingOverflow = tween.Update(dt);
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

        public void Reset()
        {
            ResetAllItems();
        }

        public void JumpTo(float time)
        {
            Reset();
            ForEachItem(item => { item.JumpTo(time); });
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
