namespace ExTween.Art
{
    public readonly struct PenState
    {
        public FloatXyPair Position { get; }
        public bool ShouldDraw { get; }

        public PenState(FloatXyPair position, bool shouldDraw)
        {
            Position = position;
            ShouldDraw = shouldDraw;
        }
    }
}
