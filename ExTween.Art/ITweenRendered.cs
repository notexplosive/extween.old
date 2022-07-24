namespace ExTween.Art
{
    /// <summary>
    /// Thing that is rendered by a tween (eg: Glyphs)
    /// </summary>
    public interface ITweenRendered
    {
        public float Duration { get; }
        public TweenPath.State GetPreciseStateAtTime(float time);
        public TweenPath.State GetApproximateStateAtTime(float time);
    }
}
