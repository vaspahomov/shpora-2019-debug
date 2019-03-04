using System;
using System.Collections.Immutable;
using Microsoft.Diagnostics.Runtime;

namespace ClrMD
{
    public class ImmutableListPrinter : IClrObjectPrinter
    {
        public bool Supports(ClrType type)
        {
            var list = ImmutableList.Create<string>();
            return type.Name == "System.Collections.Immutable.ImmutableList<System.String>"; 
        }

        public void Print(ClrObject clrObject)
        {
            var root = clrObject.GetObjectField("_root");
            PrintImmutableListNode(root);
        }

        private static void PrintImmutableListNode(ClrObject clrObject)
        {
            if (clrObject.IsNull)
                return;
            var left = clrObject.GetObjectField("_left");
            var right = clrObject.GetObjectField("_right");

            PrintImmutableListNode(left);

            var key = clrObject.GetObjectField("_key");
            if (!key.IsNull) Console.WriteLine(ClrMdHelper.ToString(key));
            PrintImmutableListNode(right);
        }
    }
}