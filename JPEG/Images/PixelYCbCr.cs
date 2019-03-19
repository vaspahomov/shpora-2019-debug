namespace JPEG.Images
{
    public class PixelYCbCr
    {
        public byte Cb;
        public byte Cr;
        public byte Y;

        public PixelYCbCr(int y, int cb, int cr)
        {
            Y = ToByte(y);
            Cb = ToByte(cb);
            Cr = ToByte(cr);
        }

        public static PixelYCbCr FromRGB(int r, int g, int b)
        {
            var y = ToByte((16.0 + (65.738 * r + 129.057 * g + 24.064 * b) / 256.0));
            var cb = ToByte((128.0 + (-37.945 * r - 74.494 * g + 112.439 * b) / 256.0));
            var cr = ToByte((128.0 + (112.439 * r - 94.154 * g - 18.285 * b) / 256.0));

            return new PixelYCbCr(y, cb, cr);
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