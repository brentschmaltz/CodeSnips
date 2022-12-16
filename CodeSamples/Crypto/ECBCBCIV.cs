//------------------------------------------------------------------------------
//
// Copyright (c) Brent Schmaltz
// All rights reserved.
//
// This code is licensed under the MIT License.
//
// Permission is hereby granted, free of charge, to any person obtaining a copy
// of this software and associated documentation files(the "Software"), to deal
// in the Software without restriction, including without limitation the rights
// to use, copy, modify, merge, publish, distribute, sublicense, and / or sell
// copies of the Software, and to permit persons to whom the Software is
// furnished to do so, subject to the following conditions :
//
// The above copyright notice and this permission notice shall be included in
// all copies or substantial portions of the Software.
//
// THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
// IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
// FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT.IN NO EVENT SHALL THE
// AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
// LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
// OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
// THE SOFTWARE.
//
//------------------------------------------------------------------------------

using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Cryptography;
using System.Text;

namespace CodeSnips.Crypto
{
    public class ECBCBCIV
    {
        private static readonly byte[] _defaultIV = new byte[] { 0xA6, 0xA6, 0xA6, 0xA6, 0xA6, 0xA6, 0xA6, 0xA6 };
        private const int _blockSizeInBits = 64;
        private const int _blockSizeInBytes = _blockSizeInBits >> 3;

        public static void Run()
        {
            IdentityModelEventSource.ShowPII = true;
            byte[] key = Aes.Create().Key;
            byte[] keyBytes = Aes.Create().Key;
            SymmetricSecurityKey symmetricSecurityKey = new SymmetricSecurityKey(keyBytes);
            SymmetricKeyWrapProvider keyWrapProvider = new SymmetricKeyWrapProvider(symmetricSecurityKey, SecurityAlgorithms.Aes256KeyWrap);

            byte[] wrappedKey = keyWrapProvider.WrapKey(keyBytes);
            byte[] unwrappedKey = keyWrapProvider.UnwrapKey(wrappedKey);

            bool areEqual = AreEqual(keyBytes, unwrappedKey);

            SymmetricAlgorithm symmetricAlgorithmECB = Aes.Create();
            symmetricAlgorithmECB.Mode = CipherMode.ECB;
            symmetricAlgorithmECB.Padding = PaddingMode.None;
            symmetricAlgorithmECB.KeySize = keyBytes.Length * 8;
            symmetricAlgorithmECB.Key = keyBytes;

            // Set the AES IV to Zeroes
            var aesIv = new byte[symmetricAlgorithmECB.BlockSize >> 3];
            for (var i = 0; i < aesIv.Length; i++)
                aesIv[i] = 0;

            symmetricAlgorithmECB.IV = aesIv;

            ICryptoTransform symmetricAlgorithmDecryptorECB = symmetricAlgorithmECB.CreateDecryptor();
            ICryptoTransform symmetricAlgorithmEncryptorECB = symmetricAlgorithmECB.CreateEncryptor();

            byte[] ecbWrappedBytes = null;
            try
            {
                ecbWrappedBytes = WrapKey(key, symmetricAlgorithmEncryptorECB);
            }
            catch(Exception ex)
            {
                Console.WriteLine($"byte[] ecbWrappedBytes = WrapKey(bytes, symmetricAlgorithmEncryptorECB); threw {ex}");
            }

            byte[] ecbUnwrappedBytes = null;
            try
            {
                ecbUnwrappedBytes = UnwrapKey(ecbWrappedBytes, symmetricAlgorithmDecryptorECB);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"cbcUnwrappedBytes = UnwrapKey(bytes, symmetricAlgorithmEncryptorECB); threw {ex}");
            }


            SymmetricAlgorithm symmetricAlgorithmCBC = Aes.Create();
            symmetricAlgorithmCBC.Mode = CipherMode.CBC;
            symmetricAlgorithmCBC.Padding = PaddingMode.None;
            symmetricAlgorithmCBC.KeySize = keyBytes.Length * 8;
            symmetricAlgorithmCBC.Key = keyBytes;

            // Set the AES IV to Zeroes
            aesIv = new byte[symmetricAlgorithmCBC.BlockSize >> 3];
            for (var i = 0; i < aesIv.Length; i++)
                aesIv[i] = 0;

            symmetricAlgorithmCBC.IV = aesIv;

            ICryptoTransform symmetricAlgorithmDecryptorCBC = symmetricAlgorithmCBC.CreateDecryptor();
            ICryptoTransform symmetricAlgorithmEncryptorCBC = symmetricAlgorithmCBC.CreateEncryptor();

            byte[] cbcWrappedBytes = null;
            try
            {
                cbcWrappedBytes = WrapKey(key, symmetricAlgorithmEncryptorCBC);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"byte[] cbcWrappedBytes = WrapKey(bytes, symmetricAlgorithmEncryptorCBC); threw {ex}");
            }

            byte[] cbcUnwrappedBytes = null;
            try
            {
                cbcUnwrappedBytes = UnwrapKey(cbcWrappedBytes, symmetricAlgorithmDecryptorCBC);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"cbcUnwrappedBytes = UnwrapKey(bytes, symmetricAlgorithmDecryptorCBC); threw {ex}");
            }

            byte[] ecb_cbcUnwrappedBytes = null;
            try
            {
                ecb_cbcUnwrappedBytes = UnwrapKey(ecbWrappedBytes, symmetricAlgorithmDecryptorCBC);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"ecb_cbcUnwrappedBytes = UnwrapKey(bytes, symmetricAlgorithmDecryptorECB); threw {ex}");
            }

            byte[] cbc_ecbUnwrappedBytes = null;
            try
            {
                cbc_ecbUnwrappedBytes = UnwrapKey(cbcWrappedBytes, symmetricAlgorithmDecryptorECB);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"cbc_ecbUnwrappedBytes = UnwrapKey(bytes, symmetricAlgorithmDecryptorCBC); threw {ex}");
            }
        }

        private static byte[] UnwrapKey(byte[] keyBytes, ICryptoTransform symmetricAlgorithmDecryptor)
        {
            return UnwrapKeyPrivate(keyBytes, 0, keyBytes.Length, symmetricAlgorithmDecryptor);
        }

        private static byte[] UnwrapKeyPrivate(byte[] inputBuffer, int inputOffset, int inputCount, ICryptoTransform symmetricAlgorithmDecryptor)
        {
            /*
                1) Initialize variables.

                    Set A = C[0]
                    For i = 1 to n
                        R[i] = C[i]

                2) Compute intermediate values.

                    For j = 5 to 0
                        For i = n to 1
                            B = AES-1(K, (A ^ t) | R[i]) where t = n*j+i
                            A = MSB(64, B)
                            R[i] = LSB(64, B)

                3) Output results.

                If A is an appropriate initial value (see 2.2.3),
                Then
                    For i = 1 to n
                        P[i] = R[i]
                Else
                    Return an error
            */

            // A = C[0]
            byte[] a = new byte[_blockSizeInBytes];

            Array.Copy(inputBuffer, inputOffset, a, 0, _blockSizeInBytes);

            // The number of input blocks
            var n = (inputCount - _blockSizeInBytes) >> 3;

            // The set of input blocks
            byte[] r = new byte[n << 3];

            Array.Copy(inputBuffer, inputOffset + _blockSizeInBytes, r, 0, inputCount - _blockSizeInBytes);


            byte[] block = new byte[16];

            // Calculate intermediate values
            for (var j = 5; j >= 0; j--)
            {
                for (var i = n; i > 0; i--)
                {
                    // T = ( n * j ) + i
                    var t = (ulong)((n * j) + i);

                    // B = AES-1(K, (A ^ t) | R[i] )

                    // First, A = ( A ^ t )
                    Xor(a, GetBytes(t), 0, true);

                    // Second, block = ( A | R[i] )
                    Array.Copy(a, block, _blockSizeInBytes);
                    Array.Copy(r, (i - 1) << 3, block, _blockSizeInBytes, _blockSizeInBytes);

                    // Third, b = AES-1( block )
                    var b = symmetricAlgorithmDecryptor.TransformFinalBlock(block, 0, 16);

                    // A = MSB(64, B)
                    Array.Copy(b, a, _blockSizeInBytes);

                    // R[i] = LSB(64, B)
                    Array.Copy(b, _blockSizeInBytes, r, (i - 1) << 3, _blockSizeInBytes);
                }
            }

            if (AreEqual(a, _defaultIV))
            {
                var keyBytes = new byte[n << 3];

                for (var i = 0; i < n; i++)
                {
                    Array.Copy(r, i << 3, keyBytes, i << 3, 8);
                }

                return keyBytes;
            }
            else
            {
                throw new InvalidOperationException("bytes are not equal");
            }
        }

        /// <summary>
        /// Wrap a key using Symmetric encryption.
        /// </summary>
        /// <param name="keyBytes">the key to be wrapped</param>
        /// <returns>The wrapped key result</returns>
        /// <exception cref="ArgumentNullException">'keyBytes' is null or has length 0.</exception>
        /// <exception cref="ArgumentException">'keyBytes' is not a multiple of 8.</exception>
        /// <exception cref="ObjectDisposedException">If <see cref="KeyWrapProvider.Dispose(bool)"/> has been called.</exception>
        /// <exception cref="SecurityTokenKeyWrapException">Failed to wrap 'keyBytes'.</exception>
        public static byte[] WrapKey(byte[] keyBytes, ICryptoTransform symmetricAlgorithmEncryptor)
        {
            return WrapKeyPrivate(keyBytes, 0, keyBytes.Length, symmetricAlgorithmEncryptor);
        }

        private static byte[] WrapKeyPrivate(byte[] inputBuffer, int inputOffset, int inputCount, ICryptoTransform symmetricAlgorithmEncryptor)
        {
            /*
               1) Initialize variables.

                   Set A = IV, an initial value (see 2.2.3)
                   For i = 1 to n
                       R[i] = P[i]

               2) Calculate intermediate values.

                   For j = 0 to 5
                       For i=1 to n
                           B = AES(K, A | R[i])
                           A = MSB(64, B) ^ t where t = (n*j)+i
                           R[i] = LSB(64, B)

               3) Output the results.

                   Set C[0] = A
                   For i = 1 to n
                       C[i] = R[i]
            */

            // The default initialization vector from RFC3394
            byte[] a = _defaultIV.Clone() as byte[];

            // The number of input blocks
            var n = inputCount >> 3;

            // The set of input blocks
            byte[] r = new byte[n << 3];

            Array.Copy(inputBuffer, inputOffset, r, 0, inputCount);
            byte[] block = new byte[16];

            // Calculate intermediate values
            for (var j = 0; j < 6; j++)
            {
                for (var i = 0; i < n; i++)
                {
                    // T = ( n * j ) + i
                    var t = (ulong)((n * j) + i + 1);

                    // B = AES( K, A | R[i] )

                    // First, block = A | R[i]
                    Array.Copy(a, block, a.Length);
                    Array.Copy(r, i << 3, block, 64 >> 3, 64 >> 3);

                    // Second, AES( K, block )
                    var b = symmetricAlgorithmEncryptor.TransformFinalBlock(block, 0, 16);

                    // A = MSB( 64, B )
                    Array.Copy(b, a, 64 >> 3);

                    // A = A ^ t
                    Xor(a, GetBytes(t), 0, true);

                    // R[i] = LSB( 64, B )
                    Array.Copy(b, 64 >> 3, r, i << 3, 64 >> 3);
                }
            }

            var keyBytes = new byte[(n + 1) << 3];

            Array.Copy(a, keyBytes, a.Length);

            for (var i = 0; i < n; i++)
            {
                Array.Copy(r, i << 3, keyBytes, (i + 1) << 3, 8);
            }

            return keyBytes;
        }

        private static bool AreEqual(byte[] a, byte[] b)
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

        private static bool AreEqual(byte[] a, byte[] b, int length)
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
        
        private static byte[] GetBytes(ulong i)
        {
            byte[] temp = BitConverter.GetBytes(i);

            if (BitConverter.IsLittleEndian)
            {
                Array.Reverse(temp);
            }

            return temp;
        }

        private static byte[] Xor(byte[] a, byte[] b, int offset, bool inPlace)
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
