using System;
using System.Collections.Generic;

namespace ExTween
{
    public class TweenCollection
    {
        protected readonly List<ITween> Items = new List<ITween>();

        protected void ForEachItem(Action<ITween> action)
        {
            foreach (var item in this.Items)
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
            this.Items.Clear();
        }
    }
}
