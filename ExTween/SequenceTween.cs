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

        public void Reset()
        {
            ResetAllItems();
            this.currentItemIndex = 0;
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

        public SequenceTween Add(ITween tween)
        {
            this.items.Add(tween);
            return this;
        }

        public void JumpTo(float targetTime)
        {
            Reset();

            var adjustedTargetTime = targetTime;
            
            for (int i = 0; i < this.items.Count; i++)
            {
                var itemDuration = this.items[i].TotalDuration;
                if (adjustedTargetTime > itemDuration)
                {
                    adjustedTargetTime -= itemDuration;
                    this.items[i].Update(itemDuration);
                }
                else
                {
                    this.items[i].Update(adjustedTargetTime);
                    this.currentItemIndex = i;
                    break;
                }
            }
        }
    }
}
