using System;
using System.Linq;
using Microsoft.Diagnostics.Runtime;

namespace ClrMD
{
    public static class ClrObjectPrinter
    {
        public static void Print(ClrHeap heap, IClrObjectPrinter[] printers)
        {
            foreach (var clrObject in heap.EnumerateObjects())
            {
                if (clrObject.Type == null)
                    continue;
                
                var printer = printers.FirstOrDefault(x => x.Supports(clrObject.Type));
                if (printer == null)
                    continue;

                Console.WriteLine($"** {clrObject.Type.Name} at {clrObject.HexAddress}");
                printer.Print(clrObject);
            }
        }
    }
}