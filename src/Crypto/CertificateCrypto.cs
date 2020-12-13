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
using System.Globalization;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace CodeSnips.Crypto
{
    public class CertificateCrypto
    {
        public static void Run()
        {
            var cryptoProvider = new RSACryptoServiceProvider();
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "DEFAULT: cryptoProvider.CspKeyContainerInfo.ProviderType: '{0}'", cryptoProvider.CspKeyContainerInfo.ProviderType));
            
            cryptoProvider = new RSACryptoServiceProvider(1024);
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "1024: cryptoProvider.CspKeyContainerInfo.ProviderType: '{0}'", cryptoProvider.CspKeyContainerInfo.ProviderType));
            
            cryptoProvider = new RSACryptoServiceProvider(2048);
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "2048: cryptoProvider.CspKeyContainerInfo.ProviderType: '{0}'", cryptoProvider.CspKeyContainerInfo.ProviderType));

            // this flag will result in X509KeyStorageFlags.EphemeralKeySet the certificate2.PrivateKey == null.
            string x509Data = @"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";
            X509Certificate2 certFromData = new X509Certificate2(Convert.FromBase64String(x509Data), "abcd", X509KeyStorageFlags.MachineKeySet);

            // password: SelfSigned2048_SHA256
            string X509Data2048Sha256 = @"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";
            X509Certificate2 X509Certificate2048Sha256 = new X509Certificate2(Convert.FromBase64String(X509Data2048Sha256), "SelfSigned2048_SHA256", X509KeyStorageFlags.MachineKeySet);


            var ecccert = new X509Certificate2(@"E:\github\ecc1.pfx", "pass");
            var rsaaesCert = new X509Certificate2(@"rsaaes.pfx", "RSAAES");
            var s2sWebSiteCert = new X509Certificate2(@"S2SWebSite.pfx", "S2SWebSite");

            DisplayKeyInfo(X509Certificate2048Sha256, "X509Certificate2048Sha256");
            DisplayKeyInfo(certFromData, "certFromData");
            DisplayKeyInfo(rsaaesCert, "rsaaesCert");
            DisplayKeyInfo(s2sWebSiteCert, "s2sWebSiteCert");
            DisplayKeyInfo(ecccert, "ecccert");
        }

        public static void DisplayKeyInfo(X509Certificate2 cert, string name)
        {
            Console.WriteLine("==================================================================");
            Console.WriteLine($"cert: '{cert.GetType()}', name: '{name}'.");

            try
            {
                try
                {
                    var privateKey = cert.PrivateKey;
                    var privateKey2 = cert.PrivateKey;
                    if (object.ReferenceEquals(privateKey, privateKey2))
                        Console.WriteLine($"TRUE: object.ReferenceEquals({name}.PrivateKey, {name}2.PrivateKey)");
                    else
                        Console.WriteLine($"FALSE: object.ReferenceEquals({name}.PrivateKey, {name}2.PrivateKey)");

                    if (privateKey != null)
                        Console.WriteLine($"PrivateKey type '{privateKey.GetType()}'.");
                    else
                        Console.WriteLine($"cert.PrivateKey == null.");

                    RSACryptoServiceProvider rsaCsp = privateKey as RSACryptoServiceProvider;
                    if (rsaCsp != null)
                        Console.WriteLine($"PrivateKey: CspKeyContainerInfo.ProviderType: '{rsaCsp.CspKeyContainerInfo.ProviderType}'.");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Caught exception: '{ex}'.");
                }

                var rsaprivateKey = cert.GetRSAPrivateKey();
                if (rsaprivateKey != null)
                {
                    var rsaprivateKey2 = cert.GetRSAPrivateKey();
                    if (object.ReferenceEquals(rsaprivateKey, rsaprivateKey2))
                        Console.WriteLine($"TRUE: object.ReferenceEquals({name}.GetRSAPrivateKey(), {name}2.GetRSAPrivateKey()))");
                    else
                        Console.WriteLine($"FALSE: object.ReferenceEquals({name}.GetRSAPrivateKey(), {name}2.GetRSAPrivateKey()))");

                    Console.WriteLine($"GetRSAPrivateKey type '{rsaprivateKey.GetType()}'.");
                }
                else
                    Console.WriteLine($"cert.GetRSAPrivateKey() == null.");

                var ecdprivateKey = cert.GetECDsaPrivateKey();
                if (ecdprivateKey != null)
                {
                    var ecdprivateKey2 = cert.GetECDsaPrivateKey();
                    if (object.ReferenceEquals(ecdprivateKey, ecdprivateKey2))
                        Console.WriteLine($"TRUE: object.ReferenceEquals({name}.GetECDsaPrivateKey(), {name}2.GetECDsaPrivateKey()))");
                    else
                        Console.WriteLine($"FALSE: object.ReferenceEquals({name}.GetECDsaPrivateKey(), {name}2.GetECDsaPrivateKey()))");

                    Console.WriteLine($"GetECDsaPrivateKey type '{ecdprivateKey.GetType()}'.");
                }
                else
                    Console.WriteLine($"cert.GetECDsaPrivateKey() == null.");

            }
            catch (Exception ex)
            {
                Console.WriteLine($"Caught exception: '{ex}'.");
            }

        }
    }
}
