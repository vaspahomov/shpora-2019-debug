using System.Globalization;
using System.Threading;

namespace Top
{
    public class ProcessMetricsModel
    {
        public ProcessMetricsModel(ProcessMetrics metrics)
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;
            PID = metrics.ProcessId;
            Name = FormatProcessName(metrics.Name);
            WorkingSet = $"{metrics.WorkingSet / (1024.0 * 1024):F2} MB";
            PrivateBytes = $"{metrics.PrivateBytes / (1024.0 * 1024):F2} MB";
            Threads = metrics.ThreadsCount;
            Cpu = $"{metrics.Cpu*100:F2} %";
        }

        public int PID { get; set; }
        
        public string Name { get; set; }
        
        public string Cpu { get; set; }
        
        public string WorkingSet { get; set; }
        
        public string PrivateBytes { get; set; }
        
        public int Threads { get; set; }

        private static string FormatProcessName(string name)
        {
            const int Width = 30;
            const string Ellipsis = "...";
            if (name.Length > Width)
                return name.Substring(0, Width-Ellipsis.Length) + "...";
            return name + new string(' ', Width - name.Length);
        }
    }
}