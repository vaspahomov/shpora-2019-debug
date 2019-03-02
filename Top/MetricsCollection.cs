using System;

namespace Top
{
    public class MetricsCollection
    {
        public DateTime Timestamp { get; set; }
        public ProcessMetrics[] ProcessMetricsCollection { get; set; }
        public string MachineName { get; set; }
    }
}