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

namespace CodeSnips.Certificates
{
    public class GetCertificteSnip
    {
        public static X509Certificate2 GetCertificate(StoreName name, StoreLocation location, string subjectName)
        {
            X509Store store = new X509Store(name, location);
            X509Certificate2Collection certificates = null;
            store.Open(OpenFlags.ReadOnly);
 
            try
            {
                X509Certificate2 result = null;
 
                // Every time we call store.Certificates property, a new collection will be returned.
                certificates = store.Certificates;
 
                for (int i = 0; i < certificates.Count; i++)
                {
                    X509Certificate2 cert = certificates[i];
 
                    if (cert.SubjectName.Name.Equals(subjectName, StringComparison.OrdinalIgnoreCase))
                    {
                        if (result != null)
                        {
                            throw new CryptographicException(string.Format(CultureInfo.InvariantCulture, "There are multiple certificates for subject Name {0}", subjectName));
                        }
 
                        result = new X509Certificate2(cert);
                    }
                }
 
                if (result == null)
                {
                    throw new CryptographicException(string.Format(CultureInfo.InvariantCulture, "No certificate was found for subject Name {0}", subjectName));
                }
 
                return result;
            }
            finally
            {
                if (certificates != null)
                {
                    for (int i = 0; i < certificates.Count; i++)
                    {
                        X509Certificate2 cert = certificates[i];
                        cert.Reset();
                    }
                }
 
                store.Close();
            }
        }
    }
}
