﻿using System.Collections.Generic;
using ExTween;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Graphics;

namespace MonoGameDemo
{
    public class SlideDeck
    {
        private readonly List<ICue> cues = new List<ICue>
        {
            new FlyInTitle(Demo.TitleFont, "Tweens"),
            new FlyInTitle(Demo.SubtitleFont, "A presentation by NotExplosive"),
            new ClearCue()
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

        public void DrawPreservedSlides(SpriteBatch spriteBatch)
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
            return CurrentCue.IsDone();
        }
    }

    public interface ICue
    {
        public bool IsDone();
    }

    public abstract class Slide : ICue
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
