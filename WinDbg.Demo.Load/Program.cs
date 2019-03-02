using System;
using System.Threading;

namespace WinDbg.Demo.Load
{
    class Program
    {
        static void Main(string[] args)
        {
            new Thread(() =>
            {
                while (true)
                {
                    new object().GetHashCode();
                }
            }).Start();
            while (true)
            {
                Console.WriteLine("...");
                Thread.Sleep(10);
            }
        }
    }
}