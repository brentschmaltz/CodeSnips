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

using System.IdentityModel.Tokens;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using CodeSnips.Certificates;

namespace CodeSnips.Crypto
{
    public class CryptoConfigSnip
    {
        public static void Run()
        {
            var cert = GetCertificteSnip.GetCertificate(StoreName.My, StoreLocation.LocalMachine, "CN=identityfail.local");
            var key = new X509AsymmetricSecurityKey(cert);
            var formatterSha1 = key.GetSignatureFormatter("http://www.w3.org/2000/09/xmldsig#rsa-sha1");
            var formatterSha2 = key.GetSignatureFormatter("http://www.w3.org/2001/04/xmldsig-more#rsa-sha256");
            object obj = CryptoConfig.CreateFromName("http://www.w3.org/2001/04/xmldsig-more#rsa-sha256");
        }
    }
}
