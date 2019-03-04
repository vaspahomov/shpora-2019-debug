using System;
using System.Collections.Generic;
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
            var list = new List<string>();
            if (clrObject.IsNull)
                return;
            
            var items = clrObject.GetObjectField("_items");
//            var size = clrObject.GetObjectField("_size");
            
            
            var sizeVal = clrObject.GetField<int>("_size");            
            for (var i = 0; i < sizeVal; ++i)
            {
                var elementAddress = items.Type.GetArrayElementAddress(items, i);
                var value = items.Type.ComponentType.GetValue(elementAddress);
                
                Console.WriteLine(value);
            }

//            var length = clrObject.Type.GetArrayLength(clrObject);
//            for (var i = 0; i < length; ++i)
//            {
//                var elementAddress = clrObject.Type.GetArrayElementAddress(clrObject, i);
////                Console.WriteLine("dsafgsd");
////                Console.WriteLine(i + ": " + clrObject.Type.ComponentType.GetValue(elementAddress));
//            }

        }
    }
}