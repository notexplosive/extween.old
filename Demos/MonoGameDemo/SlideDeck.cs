using System.Collections.Generic;
using ExTween.Art;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameDemo
{
    public class SlideDeck
    {
        private readonly List<ICue> cues = new List<ICue>
        {
            new Spiral(),
            new SpinningFlower(),
            new FontTest(new MonospaceFont(75)), // Display all chars basic monospace
            new StaticText(new MonospaceFont(45), "The quick brown fox jumped over the lazy dog."), // Test a real string (1)
            
            new ClearCue(),
            new FlyInTitle(Demo.TitleFont, "Tweens"),
        };

        private readonly List<Slide> preservedSlides = new List<Slide>();
        private int slideIndex;

        public ICue CurrentCue
        {
            get
            {
                if (this.slideIndex >= 0 && this.cues.Count > this.slideIndex)
                {
                    return this.cues[this.slideIndex];
                }

                return null;
            }
        }

        public void Prepare()
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
            if (CurrentCue is ClearCue)
            {
                this.preservedSlides.Clear();
            }
            
            if (CurrentCue is Slide slide)
            {
                slide.Setup();
                this.preservedSlides.Add(slide);
            }
        }

        public void PreviousSlide()
        {
            this.slideIndex--;
            LoadCurrentSlide();
        }

        public void DrawPreservedSlides(Painter spriteBatch)
        {
            foreach (var slide in this.preservedSlides)
            {
                slide.Draw(spriteBatch);
            }
        }

        public void Update(float dt)
        {
            foreach(var slide in this.preservedSlides)
            {
                slide.UpdateTween(dt);
            }
        }

        private class ClearCue : ICue
        {
            public bool IsDone()
            {
                return true;
            }
        }

        public bool IsIdle()
        {
            return CurrentCue != null && CurrentCue.IsDone();
        }
    }

    public interface ICue
    {
        public bool IsDone();
    }
}
