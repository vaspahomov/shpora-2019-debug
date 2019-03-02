using System;
using Microsoft.Diagnostics.Runtime;

namespace ClrMD.Demo.Crash
{
    class Program
    {
        static void Main(string[] args)
        {
            //DataTarget dataTarget = DataTarget.AttachToProcess(pid, msTimeout, AttachFlag.NonInvasive);
            using (var dt = DataTarget.LoadCrashDump(@"C:\dumps\dotnet.exe.11412.dmp"))
            {
                foreach (ClrInfo version in dt.ClrVersions)
                {
                    Console.WriteLine("Found CLR Version: " + version.Version);
                }

                Console.WriteLine(dt.PointerSize);

                var runtime = dt.ClrVersions[0].CreateRuntime();

                var heap = runtime.Heap;

                Console.WriteLine(heap);

                foreach (var clrObject in heap.EnumerateObjects())
                {
                    if (!string.Equals(clrObject.Type?.Name, "System.String"))
                        continue;

                    var length = clrObject.GetField<int>("_stringLength");
                    Console.WriteLine("address {0}, length: {1}, str: {2}", clrObject.HexAddress, length, clrObject.Type?.GetValue(clrObject.Address));
                }

                var thread = runtime.Threads[3];

                Console.WriteLine(thread.OSThreadId);
                Console.WriteLine(thread.ManagedThreadId);

                foreach (ClrStackFrame frame in thread.StackTrace)
                    Console.WriteLine(frame.DisplayString);
            }
        }
    }
}
