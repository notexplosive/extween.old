using System;
using System.Collections.Generic;
using ExTween;
using ExTween.Art;

namespace MonoGameDemo
{
    public abstract class Slide : ICue
    {
        private readonly SequenceTween tween = new SequenceTween();
        protected List<ISlideElement> Elements { get; } = new List<ISlideElement>();
        protected List<Action> UpdateFunctions { get; } = new List<Action>();

        public void Setup()
        {
            this.tween.Clear();
            Elements.Clear();
            UpdateFunctions.Clear();
            
            BuildSlideAnimation(this.tween);
        }

        protected abstract void BuildSlideAnimation(SequenceTween animationTween);
        
        public void Draw(Painter painter)
        {
            foreach (var updateFunction in UpdateFunctions)
            {
                updateFunction();
            }
            
            foreach (var element in Elements)
            {
                element.Draw(painter);
            }
        }

        public bool IsDone()
        {
            return this.tween.IsDone();
        }

        public void UpdateTween(float dt)
        {
            this.tween.Update(dt);
        }
    }
}
