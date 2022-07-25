namespace ExTween.Art
{
    /// <summary>
    /// Thing that is rendered by a tween (eg: Glyphs)
    /// </summary>
    public interface ITweenPath
    {
        public float Duration { get; }
        public TweenPathState GetPreciseStateAtTime(float time);
    }
}
