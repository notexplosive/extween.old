using System;

namespace ExTween
{
    public class WaitSecondsTween : ITween
    {
        private readonly float startingTime;
        private float timer;

        public WaitSecondsTween(float startingTime)
        {
            this.startingTime = startingTime;
            this.timer = startingTime;
        }
        
        public float UpdateAndGetOverflow(float dt)
        {
            if (IsDone())
            {
                return dt;
            }
            
            this.timer -= dt;

            return Math.Max(-this.timer, 0);
        }

        public bool IsDone()
        {
            return this.timer <= 0;
        }

        public void Reset()
        {
            this.timer = this.startingTime;
        }
    }
}
