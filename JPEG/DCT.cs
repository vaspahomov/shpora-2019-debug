using System;

namespace JPEG
{
    public class DCT
    {
        private static readonly double alphaCoef = 1 / Math.Sqrt(2);

        private double[,] CossX;
        private double[,] CossY;

        public void DCTInit(int width, int height)
        {
            CossX = new double[width,width];
            CossY = new double[height,height];
            
            for (var x = 0; x < width; x++)
            for (var u = 0; u < width; u++)
                CossX[x,u] = Math.Cos((2d * x + 1d) * u * Math.PI / (2 * width));
            
            for (var y = 0; y < height; y++)
            for (var v = 0; v < height; v++)
                CossY[y, v] = Math.Cos((2d * y + 1d) * v * Math.PI / (2 * height));
        }

        public double[,] DCT2D(int[,] input)
        {
            var height = input.GetLength(0);
            var width = input.GetLength(1);
            
            var coeffs = new double[width, height];
            var betta = 1d / width + 1d / height;
            for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
            {
                var sum = 0.0;
                for (var u = 0; u < width; u++)
                for (var v = 0; v < height; v++)
                    sum += input[u, v] * BasisFunction(x, y, u, v, height, width);
                coeffs[x, y] = sum * betta * Alpha(x) * Alpha(y);
            }

            return coeffs;
        }

        public void IDCT2D(double[,] coeffs, int[,] output)
        {
            var height = coeffs.GetLength(0);
            var width = coeffs.GetLength(1);
            
            var betta = 1d / width + 1d / height;
            for (var x = 0; x < width; x++)
            for (var y = 0; y < height; y++)
            {
                var sum = 0.0;
                for (var u = 0; u < width; u++)
                for (var v = 0; v < height; v++)
                    sum += coeffs[u, v] * BasisFunction(u, v, x, y, height, width)  * Alpha(u) * Alpha(v);
                output[x, y] = (int) (sum * betta);
            }
        }

        private double BasisFunction(int u, int v, int x, int y, int height, int width)
        {
            var b = CossX[x, u];
            var c = CossY[y, v];

            return b * c;
        }

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