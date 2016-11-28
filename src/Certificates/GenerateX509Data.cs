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
using System.Diagnostics;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace CodeSnips
{
    public class GenerateX509Data
    {
        public static void Run(string filename, string certificate)
        {
            var cert = new X509Certificate2(filename);
            var rawData = cert.GetRawCertData();
            var x509Data = Convert.ToBase64String(rawData);
            var certData = Convert.ToBase64String(cert.Export(X509ContentType.Cert));

            if (x509Data == certData)
                Console.WriteLine("x509Data == certData");
            else
                Console.WriteLine("x509Data != certData");

            Debug.WriteLine("x509Data: " + x509Data);

            var encodedThumbprint = Convert.ToBase64String(cert.GetCertHash());
            var guid = Guid.NewGuid().ToString();
            var output = "\"customKeyIdentifier\": \"" + encodedThumbprint + "\"" + Environment.NewLine;
            output += ("\"keyId\": \"" + guid + "\"") + Environment.NewLine;
            output += "\"usage\": \"Verify\"" + Environment.NewLine;
            output += "\"type\": \"AsymmetricX509Cert\"" + Environment.NewLine;
            output += ("\"value\": \"" + certData + "\"") + Environment.NewLine; 

            Debug.WriteLine("output: " + output);
            File.WriteAllText(certificate, output);
        }
    }
}
