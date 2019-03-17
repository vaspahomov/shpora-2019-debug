using BenchmarkDotNet.Running;

namespace JPEGBenchmarks
{
    class Program
    {
        static void Main(string[] args)
        {
            BenchmarkRunner.Run<JPEGBenchmark>();
        }
    }
}