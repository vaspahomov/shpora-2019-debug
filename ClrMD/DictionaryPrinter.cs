using System;
using Microsoft.Diagnostics.Runtime;

namespace ClrMD
{
    public class DictionaryPrinter : IClrObjectPrinter
    {
        public bool Supports(ClrType type)
        {
            return type.Name == "System.Collections.Generic.Dictionary<System.String,System.String>";
        }

        public void Print(ClrObject clrObject)
        {
            var entries = clrObject.GetObjectField("_entries");
         
            if (entries.IsNull)
                return;
            
            var count = clrObject.GetField<int>("_count");
            var arrayType = entries.Type;
            var elementType = arrayType.ComponentType;
            var entriesLength = count;
            var heap = arrayType.Heap;
            
            var keyField = elementType.GetFieldByName("key");
            var valueField = elementType.GetFieldByName("value");
            var hashField = elementType.GetFieldByName("hashCode");
            
            for (var i = 0; i < entriesLength; i++)
            {
                var entryAddr = arrayType.GetArrayElementAddress(entries.Address, i);
                var hash = (int) hashField.GetValue(entryAddr, true);
                if (hash < 0)
                    continue;

                var key = ClrMdHelper.Repr(heap, keyField.GetValue(entryAddr, true));
                var value = ClrMdHelper.Repr(heap, valueField.GetValue(entryAddr, true));
                Console.WriteLine(key + ": " + value);
            }
        }
    }
}