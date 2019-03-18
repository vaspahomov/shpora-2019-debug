using System;

namespace JPEG
{
    internal class complex
    {
        public double imag;

        public double real;

        //Empty constructor
        public complex()
        {
        }

        public complex(double real, double im)
        {
            this.real = real;
            imag = imag;
        }

        //Return magnitude of complex number
        public double magnitude => Math.Sqrt(Math.Pow(real, 2) + Math.Pow(imag, 2));

        public double phase => Math.Atan(imag / real);

        public string ToString()
        {
            var data = real + " " + imag + "i";
            return data;
        }

        //Convert from polar to rectangular
        public static complex from_polar(double r, double radians)
        {
            var data = new complex(r * Math.Cos(radians), r * Math.Sin(radians));
            return data;
        }

        //Override addition operator
        public static complex operator +(complex a, complex b)
        {
            var data = new complex(a.real + b.real, a.imag + b.imag);
            return data;
        }

        //Override subtraction operator
        public static complex operator -(complex a, complex b)
        {
            var data = new complex(a.real - b.real, a.imag - b.imag);
            return data;
        }

        //Override multiplication operator
        public static complex operator *(complex a, complex b)
        {
            var data = new complex(a.real * b.real - a.imag * b.imag,
                a.real * b.imag + a.imag * b.real);
            return data;
        }
    }
}