using System.Drawing;
using System.Drawing.Imaging;

namespace JPEG.Images
{
    internal class Matrix
    {
        public readonly int Height;
        public readonly PixelRgb[,] Pixels;
        public readonly int Width;

        public Matrix(Bitmap bmp)
        {
            Height = bmp.Height;
            Width = bmp.Width;
            Pixels = new PixelRgb[Height, Width];

            var data = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadWrite, bmp.PixelFormat);

            var bitsPerPixel = Image.GetPixelFormatSize(bmp.PixelFormat) / 8;

            unsafe
            {
                for (var y = 0; y < data.Height; ++y)
                {
                    var row = (byte*) data.Scan0 + y * data.Stride;
                    var columnOffset = 0;

                    for (var x = 0; x < data.Width; ++x)
                    {
                        var b = row[columnOffset];
                        var g = row[columnOffset + 1];
                        var r = row[columnOffset + 2];
                        Pixels[y, x] = new PixelRgb(r, g, b);

                        columnOffset += bitsPerPixel;
                    }
                }
            }
        }
        public Matrix(PixelRgb[,] pixels)
        {
            Pixels = pixels;
            Height = pixels.GetLength(0);
            Width = pixels.GetLength(1);
        }

        public static explicit operator Matrix(Bitmap bmp)
        {
            return new Matrix(bmp);
        }

        public static explicit operator Bitmap(Matrix matrix)
        {
            var bmp = new Bitmap(matrix.Width, matrix.Height, PixelFormat.Format32bppRgb);
            var bmd = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height),
                ImageLockMode.ReadWrite,
                bmp.PixelFormat);
            var pixels = matrix.Pixels;

            const int pixelSize = 4;

            unsafe
            {
                for (var y = 0; y < matrix.Height; y++)
                {
                    var row = (byte*) bmd.Scan0 + y * bmd.Stride;

                    for (var x = 0; x < matrix.Width; x++)
                    {
                        var pixel = pixels[y, x];
                        row[x * pixelSize] = (byte) ToByte(pixel.B); //Blue  0-255
                        row[x * pixelSize + 1] = (byte) ToByte(pixel.G); //Green 0-255
                        row[x * pixelSize + 2] = (byte) ToByte(pixel.R); //Red   0-255
                        row[x * pixelSize + 3] = 255; //Alpha 0-255
                    }
                }
            }

            bmp.UnlockBits(bmd);

            return bmp;
        }

        public static int ToByte(double d)
        {
            var val = (int) d;
            if (val > byte.MaxValue)
                return byte.MaxValue;
            if (val < byte.MinValue)
                return byte.MinValue;
            return val;
        }
    }
}