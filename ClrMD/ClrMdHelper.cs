using System.Text;
using Microsoft.Diagnostics.Runtime;

namespace ClrMD
{
    public static class ClrMdHelper
    {
        public static string ToString(ClrObject clrObject)
            => ToString(clrObject.Type.Heap, clrObject.Type.GetValue(clrObject.Address));
        
        public static string ToString(ClrHeap heap, object value)
        {
            if (value is string s)
                return Escape(s);
            if (value is ulong addr)
                return ToString(heap, addr);

            return Escape(value.ToString());
        }
        
        public static string ToString(ClrHeap heap, ulong addr)
        {
            var type = heap.GetObjectType(addr);
            return Escape(type?.GetValue(addr)?.ToString() ?? string.Empty);
        }

        private static string Escape(string s)
        {
            var sb = new StringBuilder(s.Length);
            foreach (var c in s)
                sb.Append(char.IsLetterOrDigit(c) || char.IsSymbol(c) ? c : '_');

            return sb.ToString();
        }
    }
}