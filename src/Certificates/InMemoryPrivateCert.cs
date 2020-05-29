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
using System.Security.Cryptography.X509Certificates;
using Microsoft.IdentityModel.Tokens;

namespace CodeSnips
{
    public class InMemoryPrivateCert
    {
        public static void Run()
        {
            string CertPassword = "abcd";
            string DefaultX509Data_2048 = @"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";
            X509Certificate2 cert = new X509Certificate2(Convert.FromBase64String(DefaultX509Data_2048), CertPassword, X509KeyStorageFlags.MachineKeySet);
            //string IdentityServer3Metadata = "MIIEyDCCArCgAwIBAgIIHm9sSbojhkYwDQYJKoZIhvcNAQELBQAwEzERMA8GA1UEAwwIYmVxb20gQ0EwHhcNMTcwNzExMDAwMDAwWhcNMTkwNzExMDAwMDAwWjAgMR4wHAYDVQQDDBVwYXNzcG9ydF9zaWduaW5nX2NlcnQwggIiMA0GCSqGSIb3DQEBAQUAA4ICDwAwggIKAoICAQC3pXxC03pOjTGzf/FuHqxvOsyc/AuKPzBRF0BL7wRK19bC2+ZExwzm3haHW4Gf1HmalvAkrcusmVChtE/lk5j873kD64eH8YI7vKdFFbrk3hj4WklG1Uzedqe2TsRH4kakj6Z1y1Kt/mz3yj02YnyTdbsgsj8UoqjfAelbjGqw0I8AH97W5mFNx9eIxBvE7Vey3HRv3rCtSesRii6APJghReGJcZHNX9rx+wkM76UrdwVGOEKcdo2Fx0uEGUxXQ06fNJf9QwUqKKATq9Rbp9Ur1IfpYq5o1IMvxg4kslo5l5nmgMTKYInAi1LzY2IR6z6qqkNXuPYUCT6n1y4xgYBr6v30FZAQ8ZtJgwfuY7At3MVyjzVIqlofUPMHcTWxb3UmZIstZlSIRxw69YH0PQCcsuEiELbIbxOn5j4+NMR80wxHY+xLPmaIXVCJMgjlojx1kAaRwmJL5HbCQpFJXNQ9o2pqWJwIDAQABoxMwETAPBgNVHREECDAGhwR/AAABMA0GCSqGSIb3DQEBCwUAA4ICAQBlcZ7g2+qYDiW8i2Yb9n7ly4xDSygUi87kj6hBQqVy2RXW+H4jR9+1lXTwP/hBt4AyLOQq14lU3t0FFzCzOkll5s6DtJAze++6UQjK4+8MPDv6sSNprnM/eAzPQ42xnAF/fDuz1qyINB2RtO6n1UwcT4ZZ1UamGaoSZXRZ9l+TOabtCVkEUgOUfPl4WLJo+DuigSrey3hmb931cMB1Axyud0I+cDjhUz7ZM0aVvKKc3jAs9Agz56AJUejjOlcZ+SlraSVln8uewLC4ujnLoBncUfgCzDcc6Za7sB7wB/LTlIhsNY6NHhHTD3shhuo0usZF2liQ0QvUty+c9iKnicJ9nyZxLSfNQh6ddv70n4cXuP+0m/d+TeeyQx6dbmKKSz1vEzGYRJpIElUWJGsa4WrRLrI01lPXRzypmNaMwXfoLQf3ftPbPxFoZL5T9iPh7aUGMZr15GT1tHc6HWuudU5UmmQXZm1oPIOL9kV3P+A3pJdkAe0ZnuRNwUUQ+PLFvLh5sjXlcsmfB5LsqR9DeNNadnz5VEKQtRLO56rdYUP2LinJG+L9NRULVhy6IYbywtDLkyjESawhsyXNj3CLfXfA6GPa+dZK+3uhXte/UJPIO13jq9zdPEaM9uww24JSjrqhCsU4126+vteonxju4LNky4K/wdO33Z42tn0FLKbY4w==";
            //var bytes = Convert.FromBase64String(IdentityServer3Metadata);
            //var cert = new X509Certificate2(bytes);

            var thumbprint = "C980C1CF26EEF5E197EBB89BB25EB3CB2EC250D1";
            var encodedThumprint = Base64UrlEncoder.Encode(thumbprint);
            var x5t = Base64UrlEncoder.Encode(cert.GetCertHash());
            var certThumbprint = cert.Thumbprint;

            Console.WriteLine($"cert-xft: '{x5t}'.");
            Console.WriteLine($"cert-Thumbprint: '{certThumbprint}'.");
            Console.WriteLine($"thumbprint: '{thumbprint}'.");
            Console.WriteLine($"encodedThumprint: '{encodedThumprint}'.");
        }
    }
}
