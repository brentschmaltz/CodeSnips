using System;
using System.Runtime.CompilerServices;

namespace CodeSnips.Utility
{
    public class Utilities
    {
        /// <summary>
        /// Compares two byte arrays for equality. Hash size is fixed normally it is 32 bytes.
        /// The attempt here is to take the same time if an attacker shortens the signature OR changes some of the signed contents.
        /// </summary>
        /// <param name="a">
        /// One set of bytes to compare.
        /// </param>
        /// <param name="b">
        /// The other set of bytes to compare with.
        /// </param>
        /// <returns>
        /// true if the bytes are equal, false otherwise.
        /// </returns>
        [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
        public static bool AreEqual(byte[] a, byte[] b)
        {
            byte[] s_bytesA = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 };
            byte[] s_bytesB = new byte[] { 31, 30, 29, 28, 27, 26, 25, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };

            int result = 0;
            byte[] a1, a2;

            if (((a == null) || (b == null))
            || (a.Length != b.Length))
            {
                a1 = s_bytesA;
                a2 = s_bytesB;
            }
            else
            {
                a1 = a;
                a2 = b;
            }

            for (int i = 0; i < a1.Length; i++)
            {
                result |= a1[i] ^ a2[i];
            }

            return result == 0;
        }

        /// <summary>
        /// Compares two byte arrays for equality. Hash size is fixed normally it is 32 bytes.
        /// The attempt here is to take the same time if an attacker shortens the signature OR changes some of the signed contents.
        /// </summary>
        /// <param name="a">
        /// One set of bytes to compare.
        /// </param>
        /// <param name="b">
        /// The other set of bytes to compare with.
        /// </param>
        /// <param name="length">length of array to check</param>
        /// <returns>
        /// true if the bytes are equal, false otherwise.
        /// </returns>
        [MethodImpl(MethodImplOptions.NoOptimization | MethodImplOptions.NoInlining)]
        internal static bool AreEqual(byte[] a, byte[] b, int length)
        {
            byte[] s_bytesA = new byte[] { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21, 22, 23, 24, 25, 26, 27, 28, 29, 30, 31 };
            byte[] s_bytesB = new byte[] { 31, 30, 29, 28, 27, 26, 25, 24, 23, 22, 21, 20, 19, 18, 17, 16, 15, 14, 13, 12, 11, 10, 9, 8, 7, 6, 5, 4, 3, 2, 1, 0 };

            int result = 0;
            int lenToUse = 0;
            byte[] a1, a2;

            if (((a == null) || (b == null))
            || (a.Length < length || b.Length < length))
            {
                a1 = s_bytesA;
                a2 = s_bytesB;
                lenToUse = a1.Length;
            }
            else
            {
                a1 = a;
                a2 = b;
                lenToUse = length;
            }

            for (int i = 0; i < lenToUse; i++)
            {
                result |= a1[i] ^ a2[i];
            }

            return result == 0;
        }

        internal static byte[] ConvertToBigEndian(long i)
        {
            byte[] temp = BitConverter.GetBytes(i);
            if (BitConverter.IsLittleEndian)
                Array.Reverse(temp);

            return temp;
        }

        internal static byte[] Xor(byte[] a, byte[] b, int offset, bool inPlace)
        {
            if (inPlace)
            {
                for (var i = 0; i < a.Length; i++)
                {
                    a[i] = (byte)(a[i] ^ b[offset + i]);
                }

                return a;
            }
            else
            {
                var result = new byte[a.Length];

                for (var i = 0; i < a.Length; i++)
                {
                    result[i] = (byte)(a[i] ^ b[offset + i]);
                }

                return result;
            }
        }
    }
}
