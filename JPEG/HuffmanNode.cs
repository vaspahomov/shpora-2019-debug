using System;
using System.Collections.Generic;

namespace JPEG
{
    public class HuffmanNode:IComparable
    {
        public byte? LeafLabel { get; set; }
        public int Frequency { get; set; }
        public HuffmanNode Left { get; set; }
        public HuffmanNode Right { get; set; }
        
        public int CompareTo(object obj)
        {
            if (!(obj is HuffmanNode anotherNode)) return 0;
            return Frequency.CompareTo(anotherNode.Frequency);
        }
    }
}