using System;

namespace ExTween
{
    public class DynamicTween : ITween
    {
        private readonly Func<ITween> generatorFunction;
        private ITween? internalTween;
        private bool hasGenerated;

        public ITweenDuration TotalDuration
        {
            get
            {
                BuildTweenIfNotAlready();
                return this.internalTween!.TotalDuration;
            }
        }

        public DynamicTween(Func<ITween> generatorFunction)
        {
            this.internalTween = null;
            this.generatorFunction = generatorFunction;
            this.hasGenerated = false;
        }
        
        public float Update(float dt)
        {
            BuildTweenIfNotAlready();
            return this.internalTween!.Update(dt);
        }

        private void BuildTweenIfNotAlready()
        {
            if (!this.hasGenerated)
            {
                this.internalTween = this.generatorFunction();
                this.hasGenerated = true;
            }
        }

        public bool IsDone()
        {
            BuildTweenIfNotAlready();
            return this.internalTween!.IsDone();
        }

        public void Reset()
        {
            this.hasGenerated = false;
            this.internalTween = null;
        }

        public void JumpTo(float time)
        {
            BuildTweenIfNotAlready();
            this.internalTween!.JumpTo(time);
        }
    }
}
