namespace JPEG.Images
{
    public struct PixelRgb
    {
        public double R;
        public double G;
        public double B;

        public PixelRgb(double r, double g, double b)
        {
            R = r;
            G = g;
            B = b;
        }

        public static PixelRgb FromYCbCr(double y, double cb, double cr)
        {
            var r = (298.082 * y + 408.583 * cr) / 256.0 - 222.921;

            var g = (298.082 * y - 100.291 * cb - 208.120 * cr) / 256.0 + 135.576;

            var b = (298.082 * y + 516.412 * cb) / 256.0 - 276.836;

            return new PixelRgb(r,g,b);
        }
    }
}