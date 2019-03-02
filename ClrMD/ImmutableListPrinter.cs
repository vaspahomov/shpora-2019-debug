using Microsoft.Diagnostics.Runtime;

namespace ClrMD
{
    public class ImmutableListPrinter : IClrObjectPrinter
    {
        public bool Supports(ClrType type)
        {
            return type.Name == "..."; //TODO
        }

        public void Print(ClrObject clrObject)
        {
            //TODO
        }
        
        private static void PrintImmutableListNode(ClrObject clrObject)
        {
            //TODO
        }
    }
}