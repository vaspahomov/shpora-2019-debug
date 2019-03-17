using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using BenchmarkDotNet.Attributes;
using JPEG;
using JPEG.Images;

namespace JPEGBenchmarks
{
    [DisassemblyDiagnoser]
    public class JPEGBenchmark
    {
        private const int CompressionQuality = 70;
        private Matrix imageMatrix;
        private CompressedImage compressionResult;
        
        [GlobalSetup]
        public void Setup()
        {
            var fileName = @"sample.bmp";

            using (var fileStream = File.OpenRead(fileName))
            using (var bmp = (Bitmap) Image.FromStream(fileStream, false, false))
            {
                imageMatrix = (Matrix) bmp;
                compressionResult = JPEG.Program.Compress(imageMatrix, CompressionQuality);
            }
        }

        [Benchmark]
        public CompressedImage Compress()
        {
            return JPEG.Program.Compress(imageMatrix);
        }


        [Benchmark]
        public Matrix Uncompress()
        {
            return JPEG.Program.Uncompress(compressionResult);
        }

//        [Benchmark]
//        public void CompressAndDecompress()
//        {
//            try
//            {
//                var fileName = @"sample.bmp";
//                var compressedFileName = fileName + ".compressed." + CompressionQuality;
//                var uncompressedFileName = fileName + ".uncompressed." + CompressionQuality + ".bmp";
//
//                using (var fileStream = File.OpenRead(fileName))
//                using (var bmp = (Bitmap) Image.FromStream(fileStream, false, false))
//                {
//                    var imageMatrixLocal = (Matrix) bmp;
//
//                    var compressionResultLocal = JPEG.Program.Compress(imageMatrixLocal, CompressionQuality);
//                    compressionResultLocal.Save(compressedFileName);
//                }
//                
//                var compressedImage = CompressedImage.Load(compressedFileName);
//                var uncompressedImage = JPEG.Program.Uncompress(compressedImage);
//                var resultBmp = (Bitmap) uncompressedImage;
//                resultBmp.Save(uncompressedFileName, ImageFormat.Bmp);
//            }
//            catch (Exception e)
//            {
//                Console.WriteLine(e);
//            }
//        }
    }
}