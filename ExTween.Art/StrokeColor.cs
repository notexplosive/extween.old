namespace ExTween.Art
{
    public class StrokeColor
    {
        public uint HexARGB { get; }
        public byte B { get; }
        public byte G { get; }
        public byte R { get; }
        public byte A { get; }

        public StrokeColor(uint hexArgb)
        {
            HexARGB = hexArgb;
            A = (byte) ((0xff000000 & HexARGB) >> 24);
            R = (byte) ((0xff0000 & HexARGB) >> 16);
            G = (byte) ((0x00ff00 & HexARGB) >> 8);
            B = (byte) (0x0000ff & HexARGB);
        }
        
        public static StrokeColor Black { get; } = new StrokeColor(0xff_000000);
        public static StrokeColor Red { get; } = new StrokeColor(0xff_ff0000);
        public static StrokeColor Transparent { get; } = new StrokeColor(0x00_000000);
    }
}
