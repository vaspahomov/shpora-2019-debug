using System;
using System.Threading;

namespace WinDbg.Demo.Deadlock
{
    internal class EntryPoint
    {
        public static void Main(string[] args)
        {
            var lock1 = new object();
            var lock2 = new object();

            var barrier = new Barrier(2);
            var exitBarrier = new Barrier(3);

            ThreadPool.QueueUserWorkItem(obj =>
            {
                lock (lock1)
                {
                    barrier.SignalAndWait();

                    lock (lock2) { }
                }
                
                exitBarrier.SignalAndWait();
            });

            ThreadPool.QueueUserWorkItem(obj =>
            {
                lock (lock2)
                {
                    barrier.SignalAndWait();

                    lock (lock1) { }
                }

                exitBarrier.SignalAndWait();
            });

            exitBarrier.SignalAndWait();
        }
    }
}