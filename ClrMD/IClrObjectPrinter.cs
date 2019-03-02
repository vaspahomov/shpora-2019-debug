using System.Collections.Generic;
using Microsoft.Diagnostics.Runtime;

namespace ClrMD
{
    public interface IClrObjectPrinter
    {
        bool Supports(ClrType type);
        void Print(ClrObject clrObject);
    }
}