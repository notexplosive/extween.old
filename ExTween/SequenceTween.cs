namespace ExTween
{
    public class SequenceTween : TweenCollection, ITween
    {
        private int currentItemIndex;

        public SequenceTween()
        {
            this.currentItemIndex = 0;
        }
        
        public float UpdateAndGetOverflow(float dt)
        {
            if (IsDone())
            {
                return dt;
            }

            var overflow = this.items[this.currentItemIndex].UpdateAndGetOverflow(dt);

            if (this.items[this.currentItemIndex].IsDone())
            {
                this.currentItemIndex++;
                return UpdateAndGetOverflow(overflow);
            }

            return overflow;
        }

        public bool IsDone()
        {
            return this.currentItemIndex >= this.items.Count;
        }

        public float TotalDuration
        {
            get
            {
                var total = 0f;
                ForEachItem(item => total += item.TotalDuration);
                return total;
            }
        }

        public void Add(ITween tween)
        {
            this.items.Add(tween);
        }
    }
}
