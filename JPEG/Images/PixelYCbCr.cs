namespace JPEG.Images
{
    public class PixelYCbCr
    {
        public double Cb;
        public double Cr;
        public double Y;

        public PixelYCbCr(double y, double cb, double cr)
        {
            Y = y;
            Cb = cb;
            Cr = cr;
        }

        public static PixelYCbCr FromRGB(double r, double g, double b)
        {
            var y = 16.0 + (65.738 * r + 129.057 * g + 24.064 * b) / 256.0;
            var cb = 128.0 + (-37.945 * r - 74.494 * g + 112.439 * b) / 256.0;
            var cr = 128.0 + (112.439 * r - 94.154 * g - 18.285 * b) / 256.0;

            return new PixelYCbCr(y, cb, cr);
        }
    }
}