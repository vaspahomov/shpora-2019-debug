using System;
using System.Collections.Generic;
using System.Linq;

namespace JPEG
{
    internal static class HuffmanCodec
    {
        public static byte[] Encode(byte[] data, out Dictionary<BitsWithLength, byte> decodeTable,
            out long bitsCount)
        {
            var frequences = CalcFrequences(data);

            var root = BuildHuffmanTree(frequences);

            var encodeTable = FillEncodeTable(root);

            var bitsBuffer = new BitsBuffer();
            foreach (var b in data)
                bitsBuffer.Add(encodeTable[b]);

            decodeTable = CreateDecodeTable(encodeTable);

            return bitsBuffer.ToArray(out bitsCount);
        }

        public static byte[] Decode(byte[] encodedData, Dictionary<BitsWithLength, byte> decodeTable, long bitsCount)
        {
            var result = new List<byte>();

            var sample = new BitsWithLength {Bits = 0, BitsCount = 0};
            for (var byteNum = 0; byteNum < encodedData.Length; byteNum++)
            {
                var b = encodedData[byteNum];
                for (var bitNum = 0; bitNum < 8 && byteNum * 8 + bitNum < bitsCount; bitNum++)
                {
                    sample.Bits = (sample.Bits << 1) + ((b & (1 << (8 - bitNum - 1))) != 0 ? 1 : 0);
                    sample.BitsCount++;
                    
                    if (!decodeTable.ContainsKey(sample)) continue;
                    var decodedByte = decodeTable[sample];
                    
                    result.Add(decodedByte);

                    sample.BitsCount = 0;
                    sample.Bits = 0;
                }
            }

            return result.ToArray();
        }

        private static Dictionary<BitsWithLength, byte> CreateDecodeTable(BitsWithLength[] encodeTable)
        {
            var result = new Dictionary<BitsWithLength, byte>();
            for (var b = 0; b < encodeTable.Length; b++)
            {
                var bitsWithLength = encodeTable[b];
                
                result[bitsWithLength] = (byte) b;
            }

            return result;
        }

        private static BitsWithLength[] FillEncodeTable(HuffmanNode root)
        {
            var encodeTable = new BitsWithLength[byte.MaxValue + 1];
            var nodesStack = new Stack<(HuffmanNode Node, int Bitvector, int Depth)>();
            nodesStack.Push((root, 0, 0));

            while (nodesStack.Count > 0)
            {
                var currentNode = nodesStack.Pop();
                if (currentNode.Node.LeafLabel != null)
                {
                    encodeTable[currentNode.Node.LeafLabel.Value] =
                        new BitsWithLength {Bits = currentNode.Bitvector, BitsCount = currentNode.Depth};
                }
                else
                {
                    if (root.Left == null) continue;
                    nodesStack.Push((currentNode.Node.Left, (currentNode.Bitvector << 1) + 1, currentNode.Depth + 1));
                    nodesStack.Push((currentNode.Node.Right, (currentNode.Bitvector << 1) + 0, currentNode.Depth + 1));
                }
            }

            return encodeTable;
        }

        private static HuffmanNode BuildHuffmanTree(byte[] frequences)
        {
            var nodes = GetNodes(frequences);

            while (nodes.Count > 1)
            {
                var firstMin = nodes.FindMin();
                nodes.Remove(firstMin);
                var secondMin = nodes.FindMin();
                nodes.Remove(secondMin);

                var newNode = new HuffmanNode
                {
                    Frequency = firstMin.Frequency + secondMin.Frequency,
                    Left = secondMin,
                    Right = firstMin
                };

                nodes.Add(newNode);
            }

            return nodes.Single();
        }

        private static HuffmanNode FindMin(this IEnumerable<HuffmanNode> nodes)
        {
            var minNode = default(HuffmanNode);
            foreach (var huffmanNode in nodes)
                if (minNode == default(HuffmanNode) || minNode.Frequency > huffmanNode.Frequency)
                    minNode = huffmanNode;

            return minNode;
        }

        private static HashSet<HuffmanNode> GetNodes(byte[] frequences)
        {
            var result = new HashSet<HuffmanNode>();
            for (var i = 0; i < byte.MaxValue + 1; i++)
                if (frequences[i] > 0)
                    result.Add(new HuffmanNode {Frequency = frequences[i], LeafLabel = (byte) i});
            return result;
//            return Enumerable.Range(0, byte.MaxValue + 1)
//                .Select(num => new HuffmanNode {Frequency = frequences[num], LeafLabel = (byte) num})
//                .Where(node => node.Frequency > 0);
        }

        private static byte[] CalcFrequences(byte[] data)
        {
            var result = new byte[byte.MaxValue + 1];
            foreach (var t in data)
            {
                result[t]++;
            }
//            foreach (var singleByte in data)
//                result[singleByte]++;
            return result;
        }
    }
}