namespace JPEG.Images
{
    public struct PixelRgb
    {
        public int R;
        public int G;
        public int B;

        public PixelRgb(int r, int g, int b)
        {
            R = r;
            G = g;
            B = b;
        }

        public static PixelRgb FromYCbCr(int y, int cb, int cr)
        {
            var r = (int)((298.082 * y + 408.583 * cr) / 256.0 - 222.921);

            var g = (int) ((298.082 * y - 100.291 * cb - 208.120 * cr) / 256.0 + 135.576);

            var b = (int) ((298.082 * y + 516.412 * cb) / 256.0 - 276.836);

            return new PixelRgb(r,g,b);
        }
    }
}