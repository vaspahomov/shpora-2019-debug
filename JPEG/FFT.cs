using System;
using System.Numerics;

namespace JPEG
{
    public class FFT
    {
        /// <summary>
        ///     Вычисление поворачивающего модуля e^(-i*2*PI*k/N)
        /// </summary>
        /// <param name="k"></param>
        /// <param name="N"></param>
        /// <returns></returns>
        private static Complex w(int k, int N)
        {
            if (k % N == 0) return 1;
            var arg = -2 * Math.PI * k / N;
            return new Complex(Math.Cos(arg), Math.Sin(arg));
        }

        /// <summary>
        ///     Возвращает спектр сигнала
        /// </summary>
        /// <param name="x">Массив значений сигнала. Количество значений должно быть степенью 2</param>
        /// <returns>Массив со значениями спектра сигнала</returns>
        public static Complex[] fft(Complex[] x)
        {
            Complex[] X;
            var N = x.Length;
            if (N == 2)
            {
                X = new Complex[2];
                X[0] = x[0] + x[1];
                X[1] = x[0] - x[1];
            }
            else
            {
                var x_even = new Complex[N / 2];
                var x_odd = new Complex[N / 2];
                for (var i = 0; i < N / 2; i++)
                {
                    x_even[i] = x[2 * i];
                    x_odd[i] = x[2 * i + 1];
                }

                var X_even = fft(x_even);
                var X_odd = fft(x_odd);
                X = new Complex[N];
                for (var i = 0; i < N / 2; i++)
                {
                    X[i] = X_even[i] + w(i, N) * X_odd[i];
                    X[i + N / 2] = X_even[i] - w(i, N) * X_odd[i];
                }
            }

            return X;
        }

        /// <summary>
        ///     Центровка массива значений полученных в fft (спектральная составляющая при нулевой частоте будет в центре массива)
        /// </summary>
        /// <param name="X">Массив значений полученный в fft</param>
        /// <returns></returns>
        public static Complex[] nfft(Complex[] X)
        {
            var N = X.Length;
            var X_n = new Complex[N];
            for (var i = 0; i < N / 2; i++)
            {
                X_n[i] = X[N / 2 + i];
                X_n[N / 2 + i] = X[i];
            }

            return X_n;
        }
    }
}