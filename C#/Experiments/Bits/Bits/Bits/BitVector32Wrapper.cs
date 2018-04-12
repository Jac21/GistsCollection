using System;
using System.Collections.Specialized;

namespace Bits
{
    public class BitVector32Wrapper
    {
        private static int[] masks;

        public static BitVector32 CreateBitVector32()
        {
            // Initialize masks
            masks = new int[32];
            masks[0] = BitVector32.CreateMask();
            for (int i = 1; i < 32; i++)
            {
                masks[i] = BitVector32.CreateMask(masks[i - 1]);
            }

            // Address single bits
            BitVector32 v = new BitVector32
            {
                [masks[0]] = true,
                [masks[1]] = true,
                [masks[31]] = true
            };

            for (int i = 0; i < 32; i++)
            {
                Console.Write(v[masks[i]] ? 1 : 0);
            }

            return v;
        }

        public static void CompareBoolAndBitVector32Sizes()
        {
            Console.WriteLine("Size of bool: {0}", sizeof(bool));

            unsafe
            {
                Console.WriteLine("Size of BitVector32: {0}", sizeof(BitVector32));
            }
        }
    }
}