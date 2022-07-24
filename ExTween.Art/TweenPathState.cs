namespace ExTween.Art
{
    public readonly struct TweenPathState
    {
        public FloatXyPair Position { get; }
        public bool ShouldDraw { get; }

        public TweenPathState(FloatXyPair position, bool shouldDraw)
        {
            Position = position;
            ShouldDraw = shouldDraw;
        }
    }
}
