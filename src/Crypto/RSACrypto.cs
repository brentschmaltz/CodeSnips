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
using System.Globalization;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace CodeSnips.Crypto
{
    public class RSACrypto
    {
        public static void Run()
        {
            var cryptoProvider = new RSACryptoServiceProvider();
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "cryptoProvider.CspKeyContainerInfo.ProviderType: '{0}'", cryptoProvider.CspKeyContainerInfo.ProviderType));

            var SelfSigned1024_SHA1_public = "MIICRTCCAbKgAwIBAgIQ1Oe7zrx8sLhBt9A8ZwkbJTAJBgUrDgMCHQUAMDExLzAtBgNVBAMeJgBTAGUAbABmAFMAaQBnAG4AZQBkADEAMAAyADQAXwBTAEgAQQAxMB4XDTE0MTIyNjE1MjYzNloXDTM5MTIzMTIzNTk1OVowMTEvMC0GA1UEAx4mAFMAZQBsAGYAUwBpAGcAbgBlAGQAMQAwADIANABfAFMASABBADEwgZ8wDQYJKoZIhvcNAQEBBQADgY0AMIGJAoGBALYRgjPGn3XEYszItyRNIYequfWNX1lVmwRZP4cvd4WfRi8UbqWeSqLap5TPYeorCX69d8UywkAj/hnXqLZDCsHyjQX7U7Ej4o3eIXy1vzZmNYfokaAFjqE8qPnrIirPFZW4Obc1cBReRAMXspoXtS1LCwLhb8PMOJ4f40RfncF9AgMBAAGjZjBkMGIGA1UdAQRbMFmAECSNmSZfNtHWcA36SBtjLx+hMzAxMS8wLQYDVQQDHiYAUwBlAGwAZgBTAGkAZwBuAGUAZAAxADAAMgA0AF8AUwBIAEEAMYIQ1Oe7zrx8sLhBt9A8ZwkbJTAJBgUrDgMCHQUAA4GBAAi+D7RBUnrCFLzMAKLrlK2jUOdwq22j0QbEIaoWyR8aYwrqUYa+c0qtxa9zjyfYRJXBO1vstvVMtDTvPJ1RifwuA05iKo5aofBXpUg/cKa7q2/gXPHrz4S4q8QkUynWksILaNbqSuXyGd8HmWOMhNZ9nDVq01rq0upRrB4F6t2i";
            var cert = new X509Certificate2(Convert.FromBase64String(SelfSigned1024_SHA1_public), "SelfSigned1024_SHA1", X509KeyStorageFlags.PersistKeySet);
            cryptoProvider = cert.PublicKey.Key as RSACryptoServiceProvider;
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "cryptoProvider.CspKeyContainerInfo.ProviderType: '{0}'", cryptoProvider.CspKeyContainerInfo.ProviderType));

            var SelfSigned1024_SHA1 = @"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";
            cert = new X509Certificate2(Convert.FromBase64String(SelfSigned1024_SHA1), "SelfSigned1024_SHA1", X509KeyStorageFlags.PersistKeySet);
            cryptoProvider = cert.PrivateKey as RSACryptoServiceProvider;
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "cryptoProvider.CspKeyContainerInfo.ProviderType: '{0}'", cryptoProvider.CspKeyContainerInfo.ProviderType));

            var SelfSigned2048_SHA256 = @"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";
            cert = new X509Certificate2(Convert.FromBase64String(SelfSigned2048_SHA256), "SelfSigned2048_SHA256", X509KeyStorageFlags.Exportable);
            cryptoProvider = cert.PrivateKey as RSACryptoServiceProvider;
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "cryptoProvider.CspKeyContainerInfo.ProviderType: '{0}'", cryptoProvider.CspKeyContainerInfo.ProviderType));

            cryptoProvider = new RSACryptoServiceProvider();
            cryptoProvider.FromXmlString(cert.PrivateKey.ToXmlString(true));
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "cryptoProvider.CspKeyContainerInfo.ProviderType: '{0}'", cryptoProvider.CspKeyContainerInfo.ProviderType));
        }
    }
}
