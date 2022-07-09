using System;
using System.Collections.Generic;

namespace ExTween
{
    public class TweenCollection
    {
        protected List<ITween> items = new List<ITween>();

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
            float overflow = 0;
            ForEachItem(
                tween => { overflow = MathF.Max(overflow, tween.UpdateAndGetOverflow(dt)); });

            return overflow;
        }

        public MultiplexTween AddChannel(ITween tween)
        {
            this.items.Add(tween);
            return this;
        }
        
        public bool IsDone()
        {
            var result = true;
            ForEachItem(item => result = result && item.IsDone());
            return result;
        }
    }
}
