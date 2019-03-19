namespace JPEG.Images
{
    public struct PixelRgb
    {
        public byte R;
        public byte G;
        public byte B;

        public PixelRgb(int r, int g, int b)
        {
            R = ToByte(r);
            G = ToByte(g);
            B = ToByte(b);
        }

        public static PixelRgb FromYCbCr(int y, int cb, int cr)
        {
            var r = ToByte((298.082 * y + 408.583 * cr) / 256.0 - 222.921);

            var g = ToByte((298.082 * y - 100.291 * cb - 208.120 * cr) / 256.0 + 135.576);

            var b = ToByte((298.082 * y + 516.412 * cb) / 256.0 - 276.836);

            return new PixelRgb(r,g,b);
        }
        
        
        public static byte ToByte(double d)
        {
            var val = (int) d;
            if (val > byte.MaxValue)
                return byte.MaxValue;
            if (val < byte.MinValue)
                return byte.MinValue;
            return (byte)val;
        }
    }
}