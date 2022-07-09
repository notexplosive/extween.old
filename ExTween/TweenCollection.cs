using System;
using System.Collections.Generic;

namespace ExTween
{
    public class TweenCollection
    {
        protected readonly List<ITween> items = new();

        protected void ForEachItem(Action<ITween> action)
        {
            foreach (var item in this.items)
            {
                action(item);
            }
        }

        public void ResetAllItems()
        {
            ForEachItem(item => item.Reset());
        }

        public void Clear()
        {
            this.items.Clear();
        }
    }
}
