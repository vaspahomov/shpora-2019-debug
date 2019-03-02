namespace Top
{
    public class ProcessMetrics
    {

        public int BasePriority { get; set; }

        public int SessionId { get; set; }

        public int HandlesCount { get; set; }

        public long PeakPrivateBytes { get; set; }

        public long PeakWorkingSet { get; set; }

        public int ProcessId { get; set; }
        
        public string Name { get; set; }
        
        public long WorkingSet { get; set; }
        
        public long PrivateBytes { get; set; }
        
        public int ThreadsCount { get; set; }
        
        public double Cpu { get; set; }
    }
}