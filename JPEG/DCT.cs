using System;

namespace JPEG
{
    public class DCT
    {
        public static double[,] DCT2D(double[,] input)
        {
            var height = input.GetLength(0);
            var width = input.GetLength(1);
            var coeffs = new double[width, height];
            
            for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
            {
                var sum = 0.0;
                for (var u = 0; u < width; u++)
                for (var v = 0; v < height; v++)
                    sum += input[u, v] * BasisFunction(x, y, u, v, height, width);
                
                coeffs[x, y] = sum * Beta(height, width) * Alpha(x) * Alpha(y);
            }

            return coeffs;
        }

        public static void IDCT2D(double[,] coeffs, double[,] output)
        {
            for (var x = 0; x < coeffs.GetLength(1); x++)
            for (var y = 0; y < coeffs.GetLength(0); y++)
            {
                var sum = 0.0;
                for (var u = 0; u < coeffs.GetLength(1); u++)
                for (var v = 0; v < coeffs.GetLength(0); v++)
                    sum+=coeffs[u, v] * BasisFunction(u, v, x, y, coeffs.GetLength(0), coeffs.GetLength(1)) *
                        Alpha(u) * Alpha(v);

                output[x, y] = sum * Beta(coeffs.GetLength(0), coeffs.GetLength(1));
            }
        }

        private static double BasisFunction(double u, double v, double x, double y, int height, int width)
        {
            var b = Math.Cos((2d * x + 1d) * u * Math.PI / (2 * width));
            var c = Math.Cos((2d * y + 1d) * v * Math.PI / (2 * height));

            return b * c;
        }

        
        private static readonly double alphaCoef = 1 / Math.Sqrt(2);

        private static double Alpha(int u)
        {
            if (u == 0)
                return alphaCoef;
            return 1;
        }


        private static double Beta(int height, int width)
        {
            return 1d / width + 1d / height;
        }
    }
}