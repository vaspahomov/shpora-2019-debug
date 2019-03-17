using System.Collections.Generic;

namespace JPEG
{
    public struct BitsWithLength
    {
        public BitsWithLength(int bits, int bitsCount)
        {
            Bits = bits;
            BitsCount = bitsCount;
        }

        public int Bits { get; set; }
        public int BitsCount { get; set;}
        
        public override bool Equals(object o)
        {
//            return GetHashCode() == o.GetHashCode();
            if (GetHashCode() != o.GetHashCode()) return false;
            var bitsWithLength = default(BitsWithLength);
            if ((o is BitsWithLength length))
                bitsWithLength = length;
                return bitsWithLength.BitsCount == BitsCount && bitsWithLength.Bits == Bits;
        }
        
        public override int GetHashCode()
        {
            unchecked
            {
                return (Bits * 397) ^ BitsCount;
            }
        }

//
//        public class Comparer : IEqualityComparer<BitsWithLength>
//        {
//            public bool Equals(BitsWithLength x, BitsWithLength y)
//            {
//                if (GetHashCode(x) != GetHashCode(y)) return false;
//                return x.BitsCount == y.BitsCount && x.Bits == y.Bits;
//            }
//        }
    }
}