using System;
using CenterSpace.NMath.Core;

namespace JPEG
{
    class FFTClass
    {
        public static (double[,], double[,]) FFT(int[,] inputData)
        {
            var fft = new DoubleComplexForward2DFFT(8, 8);
            var n = inputData.GetLength(0);
            var m = inputData.GetLength(1);
            var cfftdata = new DoubleComplexMatrix( fft.Rows, fft.Columns );
            var data = new DoubleComplexMatrix();
            for (var i = 0; i < 8; i++)
            for (var j = 0; j < 8; j++)
                data[i, j] = inputData[i, j];
            fft.FFT(data, ref cfftdata);

            var resultReal = new double[8,8];
            var resultImage = new double[8,8];
            
            for (var i = 0; i < 8; i++)
            for (var j = 0; j < 8; j++)
            {
                resultReal[i, j] = (int)cfftdata[i, j].Real;
                resultImage[i, j] = (int)cfftdata[i, j].Imag;
            }

            return (resultReal, resultImage);
        }
        public static int[,] FFTBack(double[,] real, double[,] image)
        {
            var fft = new DoubleComplexBackward2DFFT(8, 8);
            var cfftdata = new DoubleComplexMatrix( fft.Rows, fft.Columns );
            
            var data = new DoubleComplexMatrix();
            
            for (var i = 0; i < 8; i++)
            for (var j = 0; j < 8; j++)
                data[i, j] = new DoubleComplex(real[i, j], image[i,j]);
            fft.FFT(data, ref cfftdata);

            var resultReal = new int[8,8];
            
            for (var i = 0; i < 8; i++)
            for (var j = 0; j < 8; j++)
            {
                resultReal[i, j] = (int)cfftdata[i, j].Real;
            }

            return resultReal;
        }
    }                  
}