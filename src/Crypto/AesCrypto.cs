//------------------------------------------------------------------------------
//
// Copyright (c) AuthFactors.com.
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

using System;
using System.Security.Cryptography;

namespace CodeSnips.Crypto
{
    public class AesCrypto
    {
        public static void Run()
        {
            Display(new AesCryptoServiceProvider(), "Default AesCryptoServiceProvider");
            Display(new AesManaged(), "Default AesManaged");
            var aes = new AesCryptoServiceProvider
            {
                KeySize = 256
            };

            Display(aes, "KeySize = 256, AesCryptoServiceProvider");


            // not supported in 4.5.1
            //Display(CngKey.Create(CngAlgorithm.Rsa), "Default CngAlgorithm.Rsa");
            Display(CngKey.Create(CngAlgorithm.ECDsaP256), "Default CngAlgorithm.ECDsaP256");
            Display(CngKey.Create(CngAlgorithm.ECDsaP384), "Default CngAlgorithm.ECDsaP384");
            Display(CngKey.Create(CngAlgorithm.ECDsaP521), "Default CngAlgorithm.ECDsaP521");
        }

        private static void Display(Aes aes, string name)
        {
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine(name);
            Console.WriteLine("LegalBlockSizes");
            foreach (var blocksize in aes.LegalBlockSizes)
                Console.WriteLine("LegalBlockSize: MaxSize: '{0}', MinSize: '{1}', SkipSize: '{2}'", blocksize.MaxSize, blocksize.MinSize, blocksize.SkipSize);

            Console.WriteLine("LegalKeySizes");
            foreach (var keysize in aes.LegalKeySizes)
                Console.WriteLine("LegalKeySize: MaxSize: '{0}', MinSize: '{1}', SkipSize: '{2}'", keysize.MaxSize, keysize.MinSize, keysize.SkipSize);

            Console.WriteLine("BlockSize: '{0}'", aes.BlockSize);
            Console.WriteLine("CipherMode: '{0}'", aes.Mode);
            Console.WriteLine("IV length: '{0}'", aes.IV.Length);
            Console.WriteLine("KeySize: '{0}'", aes.KeySize);
            Console.WriteLine("PaddingMode: '{0}'", aes.Padding);
        }

        private static void Display(CngKey key, string name)
        {
            Console.WriteLine(Environment.NewLine);
            Console.WriteLine(name);
            Console.WriteLine("Algorithm : '{0}'", key.Algorithm);
            Console.WriteLine("AlgorithmGroup : '{0}'", key.AlgorithmGroup);
            Console.WriteLine("KeySize : '{0}'", key.KeySize);
            Console.WriteLine("KeyUsage : '{0}'", key.KeyUsage);
            Console.WriteLine("UniqueName : '{0}'", key.UniqueName);
        }
    }
}
