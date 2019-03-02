using System;
using System.Linq;

namespace Top
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            var printer = new ConsoleMetricsPrinter();
            var metricsCollector = new MetricsCollector();
            var sortKind = args.Length > 0 ? ParseSortKind(args[0]) : SortKind.WorkingSet;
            var limit = ParseLimit(args);
            metricsCollector.MetricsAvailable += (_, m) => printer.Print(m, sortKind, limit);
            metricsCollector.Run(TimeSpan.FromSeconds(1));
        }

        private static SortKind ParseSortKind(string arg)
        {
            switch (arg)
            {
                case "--cpu":
                    return SortKind.Cpu;
                case "--ws":
                    return SortKind.WorkingSet;
                case "--pb":
                    return SortKind.PrivateBytes;
                default:
                    return SortKind.WorkingSet;
            }
        }
        
        private static int ParseLimit(string[] args)
        {
            var limit = args.FirstOrDefault(x => int.TryParse(x, out _));
            return limit == null ? 20 : int.Parse(limit);
        }
    }
}