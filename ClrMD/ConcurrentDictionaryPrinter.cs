using Microsoft.Diagnostics.Runtime;

namespace ClrMD
{
    public class ConcurrentDictionaryPrinter : IClrObjectPrinter
    {
        public bool Supports(ClrType type)
        {
            return type.Name == "..."; //TODO
        }

        public void Print(ClrObject clrObject)
        {
            //TODO
        }
    }
}