using System;
using System.Diagnostics;

namespace Bits
{
    class Program
    {
        static void Main()
        {
            // BitField testing
            Console.WriteLine("BitField testing -");
            Stopwatch stopwatch = new Stopwatch();
            Console.WriteLine("First bit field...");

            ExampleBitfield bitFieldOne = new ExampleBitfield
            {
                BitOne = true,
                BitTwo = 0,
                BitThree = 0x2, // 10
                BitFour = 0x7 // 0111
            };

            stopwatch.Start();
            ulong bits = bitFieldOne.ToUInt64();
            Console.WriteLine("ulong: 0x{0:X2}", bits);
            stopwatch.Stop();

            Console.WriteLine("Elapsed ticks for ToUInt64() call: {0}", stopwatch.ElapsedTicks);
            stopwatch.Reset();

            stopwatch.Start();
            ulong bitsFast = bitFieldOne.ToUInt64Fast();
            Console.WriteLine("ulong: 0x{0:X2}", bitsFast);
            stopwatch.Stop();

            Console.WriteLine("Elapsed ticks for ToUInt64Fast() call: {0}", stopwatch.ElapsedMilliseconds);

            string s = bitFieldOne.ToBinaryString();
            Console.WriteLine("string: {0}", s);

            Console.WriteLine();
            Console.WriteLine("Second bit field...");
            ExampleBitfield bitfieldTwo = BitFieldExtensions.CreateBitField<ExampleBitfield>(0xA3);
            Console.WriteLine("ulong: 0x{0:X2}", bitfieldTwo.ToUInt64());
            Console.WriteLine("string: {0}", bitfieldTwo.ToBinaryString());
            Console.WriteLine();

            //////////////////////////////////////////////////////////////////////////////////////////////////

            // BitArray testing
            Console.WriteLine("BitArray testing -");
            bool[] array = {true, false, true, false, false};
            foreach (var bit in BitArrayWrapper.CreateBooleanBitArray(array))
            {
                Console.WriteLine(bit);
            }

            Console.WriteLine();

            BitArrayWrapper.CreateBooleanBitArray();

            Console.WriteLine();

            //////////////////////////////////////////////////////////////////////////////////////////////////

            // BitVector32 testing
            Console.WriteLine("BitVector32 testing -");
            BitVector32Wrapper.CreateBitVector32();
            
            Console.WriteLine();

            BitVector32Wrapper.CompareBoolAndBitVector32Sizes();

            Console.ReadLine();
        }
    }
}