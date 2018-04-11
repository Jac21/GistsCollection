using System;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Text;

namespace Bits
{
    /// <summary>
    /// Specifies the number of bits in the bit field structure
    /// Maximum number of bits are 64
    /// </summary>
    [AttributeUsage(AttributeTargets.Struct | AttributeTargets.Class)]
    public sealed class BitFieldNumberOfBitsAttribute : Attribute
    {
        /// <summary>
        /// The number of bits the field will contain
        /// </summary>
        public byte BitCount { get; }

        public BitFieldNumberOfBitsAttribute(byte bitCount)
        {
            if (bitCount < 1 || bitCount > 64)
            {
                throw new ArgumentOutOfRangeException(nameof(bitCount), "The number of bits must be between 1 and 64");
            }

            BitCount = bitCount;
        }
    }

    /// <summary>
    /// Specifies the length of each bit field
    /// </summary>
    [AttributeUsage(AttributeTargets.Field | AttributeTargets.Property)]
    public sealed class BitFieldInfoAttribute : Attribute
    {
        /// <summary>
        /// The offset of the bit field
        /// </summary>
        public byte Offset { get; set; }

        /// <summary>
        /// The number of bits the bit field occupies
        /// </summary>
        public byte Length { get; set; }

        /// <summary>
        /// Initializes new instance of BitFieldInfoAttribute with the specified offset and length
        /// </summary>
        /// <param name="offset"></param>
        /// <param name="length"></param>
        public BitFieldInfoAttribute(byte offset, byte length)
        {
            Offset = offset;
            Length = length;
        }
    }

    /// <summary>
    /// Interface is used as a marker in order to create extension methods on a struct
    /// that is used to emulate bit fields
    /// </summary>
    public interface IBitField
    {
    }

    // ReSharper disable UnusedAutoPropertyAccessor.Local
    [BitFieldNumberOfBits(8)]
    public struct ExampleBitfield : IBitField
    {
        [BitFieldInfo(0, 1)] public bool BitOne { get; set; }
        [BitFieldInfo(1, 1)] public byte BitTwo { get; set; }
        [BitFieldInfo(2, 2)] public byte BitThree { get; set; }
        [BitFieldInfo(4, 4)] public byte BitFour { get; set; }
    }

    public static class BitFieldExtensions
    {
        /// <summary>
        /// Converts the members of the bit field to an integer value
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static ulong ToUInt64(this IBitField obj)
        {
            ulong result = 0;

            // loop through all the properties
            foreach (PropertyInfo pi in obj.GetType().GetProperties())
            {
                // check if property has an attribute of type BitFieldLengthAttribute
                if (pi.GetCustomAttribute(typeof(BitFieldInfoAttribute)) is BitFieldInfoAttribute bitField)
                {
                    // calculate a bitmask using the length of the bit field
                    ulong mask = 0;
                    for (byte i = 0; i < bitField.Length; i++)
                    {
                        mask |= 1UL << i;
                    }

                    // this conversion makes it possible to use different types in the bit field
                    ulong value = Convert.ToUInt64(pi.GetValue(obj));

                    result |= (value & mask) << bitField.Offset;
                }
            }

            return result;
        }

#pragma warning disable CS0675 // Bitwise-or operator used on a sign-extended operand
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static UInt64 ToUInt64Fast(this ExampleBitfield bitFields)
        {
            ulong value = 0;
            value |= (ulong) (bitFields.BitOne ? 1 : 0);
            value |= (ulong) ((bitFields.BitTwo << 1) & 0x02);
            value |= (ulong) ((bitFields.BitThree << 2) & 0x0C);
            value |= (ulong) ((bitFields.BitFour << 4) & 0xF0);
            return value;
        }

        /// <summary>
        /// This method converts the struct into a string of binary values.
        /// The length of the string will be equal to the number of bits in the struct.
        /// The least significant bit will be on the right of the string.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToBinaryString(this IBitField obj)
        {
            if (!(obj.GetType().GetCustomAttribute(typeof(BitFieldNumberOfBitsAttribute)) is
                BitFieldNumberOfBitsAttribute bitField))
            {
                throw new Exception(
                    $@"The attribute 'BitFieldNumberOfBitsAttribute' has to be added to the struct '{
                            obj.GetType().Name
                        }.");
            }

            StringBuilder sb = new StringBuilder(bitField.BitCount);

            ulong bitFieldValue = obj.ToUInt64();
            for (int i = bitField.BitCount - 1; i >= 0; i--)
            {
                sb.Append((bitFieldValue & (1UL << i)) > 0 ? "1" : "0");
            }

            return sb.ToString();
        }

        public static T CreateBitField<T>(ulong value) where T : struct
        {
            // the created struct has to be boxed, otherwise, PropertyInfo.SetValue
            // will work on a copy instead of the actual object
            object boxedValue = new T();

            // loop through the properties and set a value to each one
            foreach (PropertyInfo pi in boxedValue.GetType().GetProperties())
            {
                if (pi.GetCustomAttribute(typeof(BitFieldInfoAttribute)) is BitFieldInfoAttribute bitField)
                {
                    ulong mask = (ulong) Math.Pow(2, bitField.Length) - 1;
                    object setVal = Convert.ChangeType((value >> bitField.Offset) & mask, pi.PropertyType);
                    pi.SetValue(boxedValue, setVal);
                }
            }

            // unboxing the object
            return (T) boxedValue;
        }
    }
}