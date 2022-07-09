namespace ExTween
{
    public class SequenceTween : TweenCollection, ITween
    {
        private int currentItemIndex;

        public SequenceTween()
        {
            this.currentItemIndex = 0;
        }
        
        public float Update(float dt)
        {
            if (IsDone())
            {
                return dt;
            }

            var overflow = this.items[this.currentItemIndex].Update(dt);

            if (this.items[this.currentItemIndex].IsDone())
            {
                this.currentItemIndex++;
                return Update(overflow);
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
