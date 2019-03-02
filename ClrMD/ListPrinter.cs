using Microsoft.Diagnostics.Runtime;

namespace ClrMD
{
    public class ListPrinter : IClrObjectPrinter
    {
        public bool Supports(ClrType type)
        {
            return type.Name == "System.Collections.Generic.List<System.String>";
        }

        public void Print(ClrObject clrObject)
        {
            //TODO
        }
    }
}