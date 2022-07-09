namespace ExTween
{
    public class Tween<T>
    {
        private readonly float duration;
        private readonly EaseFunction easeFunction;
        private readonly T startingValue;
        private readonly T targetValue;
        private readonly Tweenable<T> tweenable;

        public Tween(Tweenable<T> tweenable, T targetValue, float duration, EaseFunction easeFunction)
        {
            this.tweenable = tweenable;
            this.targetValue = targetValue;
            this.duration = duration;
            this.easeFunction = easeFunction;
            this.startingValue = tweenable.Value;
            CurrentTime = 0;
        }

        public float CurrentTime { get; set; }

        public void UpdateAndGetOverflow(float dt)
        {
            CurrentTime += dt;
            var percent = CurrentTime / this.duration;

            this.tweenable.ForceSetValue(
                this.tweenable.Lerp(
                    this.startingValue,
                    this.targetValue,
                    this.easeFunction(percent)));
        }
    }

    public class WaitSecondsTween
    {
        private readonly float startingTime;
        private float timer;

        public WaitSecondsTween(float startingTime)
        {
            this.startingTime = startingTime;
            this.timer = startingTime;
        }
        
        public void UpdateAndGetOverflow(int dt)
        {
            this.timer -= dt;
        }

        public bool IsDone()
        {
            return this.timer <= 0;
        }
    }
}
