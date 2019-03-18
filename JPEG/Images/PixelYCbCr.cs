namespace JPEG.Images
{
    public class PixelYCbCr
    {
        public int Cb;
        public int Cr;
        public int Y;

        public PixelYCbCr(int y, int cb, int cr)
        {
            Y = y;
            Cb = cb;
            Cr = cr;
        }

        public static PixelYCbCr FromRGB(int r, int g, int b)
        {
            var y = (int) (16.0 + (65.738 * r + 129.057 * g + 24.064 * b) / 256.0);
            var cb = (int) (128.0 + (-37.945 * r - 74.494 * g + 112.439 * b) / 256.0);
            var cr = (int) (128.0 + (112.439 * r - 94.154 * g - 18.285 * b) / 256.0);

            return new PixelYCbCr(y, cb, cr);
        }
    }
}