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

            for (var i = 0; i < entriesLength; i++)
            {
                var entryAddr = arrayType.GetArrayElementAddress(entries.Address, i);

                var dictEntry = ClrValueClassFactory.Create(entryAddr, elementType);

                var hash = dictEntry.GetField<int>("hashCode");
                if (hash < 0)
                    continue;

                var key = ClrMdHelper.ToString(dictEntry.GetObjectField("key"));
                var value = ClrMdHelper.ToString(dictEntry.GetObjectField("value"));
                Console.WriteLine(key + ": " + value);
            }
        }
    }
}