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

using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Certificates
{
    public class CompareTwoCerts
    {
        public static void Compare(StoreName name1, StoreLocation location1, string subjectName1, StoreName name2, StoreLocation location2, string subjectName2)
        {
            var cert1 = GetCertificteSnip.GetCertificate(name1, location1, subjectName1);
            var cert2 = GetCertificteSnip.GetCertificate(name2, location2, subjectName2);

            if (cert1.Thumbprint == cert2.Thumbprint)
                Console.WriteLine($"Thumbprints are equal: ");

            var rawData1 = cert1.RawData;
            var rawData2 = cert2.RawData;

            if (rawData1.Length != rawData2.Length)
                Console.WriteLine($"rawData different lengths: {rawData1.Length} : {rawData2.Length}");

            var sha1 = SHA1.Create();
            var hash = sha1.ComputeHash(rawData1);
            var thumbprint = Encoding.UTF8.GetString(hash);

            if (cert1.Thumbprint == thumbprint)
                Console.WriteLine($"cert1.Thumbprint == thumbprint");
            else
                Console.WriteLine($"cert1.Thumbprint != thumbprint, {cert1.Thumbprint} : {thumbprint}");

            var certHash1 = cert1.GetCertHash();

            if (certHash1.Length != hash.Length)
                Console.WriteLine($"certHash1.length !=  hash.length: {certHash1.Length} : {hash.Length}");
            else
            {
                Console.WriteLine($"certHash1.length ==  hash.length: {certHash1.Length} : {hash.Length}");
                for (int i = 0; i < certHash1.Length; i++)
                    if (certHash1[i] != hash[i])
                        Console.WriteLine($"certHash1[i] != hash[i]: {i}, {certHash1[i]} : {hash[i]}");

            }
        }
    }
}
