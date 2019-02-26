using System;
using System.Collections;

namespace Bits
{
    public class BitArrayWrapper
    {
        /// <summary>
        /// Creates static BitArray with some bits flipped, logs to console
        /// </summary>
        /// <returns></returns>
        public static BitArray CreateBooleanBitArray()
        {
            BitArray bitArray = new BitArray(32)
            {
                // set some bits using indexer
                [3] = true,
                [5] = true
            };

            // set using built-in method
            bitArray.Set(10, true);

            // Count returns the total of all bits (1s and 0s)
            Console.WriteLine("Total bits: {0}", bitArray.Count);

            Console.WriteLine();

            // Loop to count set bits
            Console.WriteLine("Total bits set to 1: {0}", CountBitArray(bitArray));


            Console.WriteLine();

            // Loop to display all bits
            Console.WriteLine("Display bits:");
            DisplayBitArray(bitArray);

            return bitArray;
        }

        /// <summary>
        /// Count set bits in BitArray.
        /// </summary>
        static int CountBitArray(BitArray bitArray)
        {
            int count = 0;
            foreach (bool bit in bitArray)
            {
                if (bit)
                {
                    count++;
                }
            }

            return count;
        }

        /// <summary>
        /// Display bits as 0s and 1s.
        /// </summary>
        static void DisplayBitArray(BitArray bitArray)
        {
            for (int i = 0; i < bitArray.Count; i++)
            {
                bool bit = bitArray.Get(i);
                Console.Write(bit ? 1 : 0);
            }

            Console.WriteLine();
        }

        /// <summary>
        /// Creates BitArray from array parameter
        /// </summary>
        /// <param name="array"></param>
        /// <returns></returns>
        public static BitArray CreateBooleanBitArray(bool[] array)
        {
            return new BitArray(array);
        }
    }
}