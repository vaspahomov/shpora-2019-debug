using System;
using System.Threading;

namespace WinDbg.Demo.Misc
{
    
    internal class EntryPoint
    {
        public static void Main(string[] args)
        {
            holder = new StateHolder
            {
                ClassState = Tuple.Create(123, "Hello"),
                StructState = (987, "WinDbg!")
            };
            //load from core soslr
            Thread.Sleep(Timeout.Infinite);
        }

        private static StateHolder holder;
    }

    internal class StateHolder
    {
        public Tuple<Int32, string> ClassState;
        public (Int32, string) StructState;
    }
}