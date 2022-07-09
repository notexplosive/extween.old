using System.Collections.Generic;
using ExTween;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameDemo
{
    public class SlideDeck
    {
        private int slideIndex;

        private readonly List<Slide> slides = new List<Slide>
        {
            new TitleSlide()
        };

        public void Begin()
        {
            LoadCurrentSlide();
        }

        public void NextSlide()
        {
            this.slideIndex++;
            LoadCurrentSlide();
        }

        private void LoadCurrentSlide()
        {
            if (this.slideIndex >= 0 && this.slides.Count > this.slideIndex)
            {
                var slide = this.slides[this.slideIndex];
                slide.Setup();
            }
        }

        public void PreviousSlide()
        {
            this.slideIndex--;
            LoadCurrentSlide();
        }

        public void DrawCurrentSlide(SpriteBatch spriteBatch)
        {
            if (this.slideIndex >= 0 && this.slides.Count > this.slideIndex)
            {
                var slide = this.slides[this.slideIndex];
                slide.Draw(spriteBatch);
            }
        }

        public void UpdateCurrentSlide(float dt)
        {
            if (this.slideIndex >= 0 && this.slides.Count > this.slideIndex)
            {
                this.slides[this.slideIndex].UpdateTween(dt);
            }
        }
    }

    public abstract class Slide
    {
        private readonly SequenceTween tween = new SequenceTween();

        public void Setup()
        {
            this.tween.Clear();
            this.tween.Add(new CallbackTween(OnTweenBegin));
            BuildTween(this.tween);
        }
        
        protected abstract void BuildTween(SequenceTween sequenceTween);
        protected abstract void OnTweenBegin();
        public abstract void Draw(SpriteBatch spriteBatch);

        public void UpdateTween(float dt)
        {
            this.tween.Update(dt);
        }
    }
}
