using System;
using System.Diagnostics;
using System.IO;
using libbmp;

namespace Grayscaler
{
    class Program
    {
        static void Main(string[] args)
        {
            var sw = Stopwatch.StartNew();
            var picture = imageprocessor.load(args[0]);
            var greyscalePicture = imageprocessor.grayscale(picture);
            var outputFile = Path.GetFileNameWithoutExtension(args[0]) + "_gray.bmp";
            imageprocessor.save(greyscalePicture, outputFile);
            Console.WriteLine(sw.Elapsed);
        }
    }
}