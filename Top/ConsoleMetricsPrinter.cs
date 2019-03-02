using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ConsoleTables;

namespace Top
{
    internal class ConsoleMetricsPrinter
    {
        private object sync = new object();
        private StringBuilder stringBuilder = new StringBuilder();
        private char[] NewLine = {'\r', '\n'};
        private int consoleWidth = GetConsoleWidth();


        public void Print(MetricsCollection metricsCollection, SortKind sortKind, int top)
        {
            lock (sync)
            {
                PrintInternal(metricsCollection, sortKind, top);
            }
        }

        private void PrintInternal(MetricsCollection collection, SortKind sortKind, int top)
        {
            stringBuilder.Clear();
            stringBuilder.AppendLine($"Machine: {collection.MachineName}, Timestamp: {collection.Timestamp}");
            
            var table = ConsoleTable.From(
                    Sort(collection
                        .ProcessMetricsCollection, sortKind)
                        .Take(top)
                        .Select(x => new ProcessMetricsModel(x)))
                .ToMarkDownString();

            var lines = stringBuilder.Append(table).ToString().Split(NewLine, StringSplitOptions.RemoveEmptyEntries);
            stringBuilder.Clear();

            var width = GetConsoleWidth();

            if (consoleWidth != width)
            {
                Console.Clear();
                consoleWidth = width;
            }
            
            foreach (var line in lines)
            {
                stringBuilder.Append(line, 0, Math.Min(width, line.Length));
                if (line.Length < width)
                    stringBuilder.Append(' ', width - line.Length);
                stringBuilder.AppendLine();
            }
            
            Console.SetCursorPosition(0, 0);
            Console.WriteLine(stringBuilder.ToString());
        }

        private static IEnumerable<ProcessMetrics> Sort(IEnumerable<ProcessMetrics> metrics, SortKind sortKind)
        {
            switch (sortKind)
            {
                case SortKind.Cpu:
                    return metrics.OrderByDescending(x => x.Cpu);
                case SortKind.WorkingSet:
                    return metrics.OrderByDescending(x => x.WorkingSet);
                case SortKind.PrivateBytes:
                    return metrics.OrderByDescending(x => x.PrivateBytes);
                default:
                    throw new ArgumentOutOfRangeException(nameof(sortKind), sortKind, null);
            }
        }

        private static int GetConsoleWidth() => Console.WindowWidth - 1;
    }
}