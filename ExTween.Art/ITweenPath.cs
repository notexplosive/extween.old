namespace ExTween.Art
{
    /// <summary>
    /// Tween that tracks a PenState
    /// </summary>
    public interface ITweenPath
    {
        public float Duration { get; }
        public PenState GetPreciseStateAtTime(float time);
    }
}
