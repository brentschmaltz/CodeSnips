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

using Microsoft.IdentityModel.Tokens;
using System;
using System.IO;
using System.Security.Cryptography.X509Certificates;
using System.Xml.Linq;

namespace Certificates
{
    public class GenerateX509Data
    {
        // if password is null => it is not needed
        public static void Run(string filename, string outputFile, string password)
        {
            var cert = (string.IsNullOrEmpty(password)) ? new X509Certificate2(filename) : new X509Certificate2(filename, password);
            //var rawData = cert.Export(X509ContentType.Authenticode, password);
            var rawData = cert.GetRawCertData();
            var x509Data = Convert.ToBase64String(rawData);
            var certData = Convert.ToBase64String(cert.Export(X509ContentType.Cert));
            string pCert = "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";
            X509Certificate2 SelfSignedTestCert = new X509Certificate2(Convert.FromBase64String(pCert), "SelfSignedTestCert");
            string x509DataPublic = Convert.ToBase64String(SelfSignedTestCert.Export(X509ContentType.Cert));

            X509Certificate2 certPrivate = new X509Certificate2(Convert.FromBase64String(x509Data));

//            var certDetails = cert.Export(X509ContentType.Pfx, password);
            if (x509Data == certData)
                Console.WriteLine("x509Data == certData");
            else
                Console.WriteLine("x509Data != certData");

            Console.WriteLine("x509Data: " + x509Data);

            var encodedThumbprint = Convert.ToBase64String(cert.GetCertHash());
            var guid = Guid.NewGuid().ToString();
            var output = "\"customKeyIdentifier\": \"" + encodedThumbprint + "\"" + Environment.NewLine;
            output += ("\"keyId\": \"" + guid + "\"") + Environment.NewLine;
            output += "\"usage\": \"Verify\"" + Environment.NewLine;
            output += "\"type\": \"AsymmetricX509Cert\"" + Environment.NewLine;
            output += ("\"value\": \"" + certData + "\"") + Environment.NewLine;

            Console.WriteLine("output: " + output);
            File.WriteAllText(outputFile, output);
        }
    }
}
