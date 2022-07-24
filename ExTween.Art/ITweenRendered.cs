﻿namespace ExTween.Art
{
    /// <summary>
    /// Thing that is rendered by a tween (eg: Glyphs)
    /// </summary>
    public interface ITweenRendered
    {
        public float Duration { get; }
        public TweenPathState GetPreciseStateAtTime(float time);
        public TweenPathState GetApproximateStateAtTime(float time);
    }
}
