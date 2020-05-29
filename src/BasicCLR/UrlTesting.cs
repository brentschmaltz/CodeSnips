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
using System.Net;

namespace CodeSnips
{
    public class UrlTesting
    {
        public static void Run()
        {
            Display("https://www.microsoft.com:80/tenant/?query=foo");
            Display("https://www.microsoft.com:443/tenant/?query=foo");
            Display("https://www.microsoft.com/tenant/?query=foo");
            Display("http://www.microsoft.com:80/tenant/?query=foo");
            Display("http://www.microsoft.com/tenant/?query=foo");
            Display("http://www.microsoft.com:801/tenant/?query=foo");
        }

        private static void Display(string address)
        {
            var uri = new Uri(address);
            var authority = uri.Authority;
            var dnsSafeHost = uri.DnsSafeHost;
            var leftPart = uri.GetLeftPart(UriPartial.Authority);
            var port = uri.Port;
            var scheme = uri.Scheme;
            var segments = uri.Segments;
            var delimeter = Uri.SchemeDelimiter;
            var encoded = WebUtility.UrlEncode(address);

            Console.WriteLine("======================================");
            Console.WriteLine("");
            Console.WriteLine("delimeter: " + delimeter);
            Console.WriteLine("segments: " + segments.Length);
            foreach(var seg in segments)
                Console.WriteLine("segments []: " + seg);

            Console.WriteLine("address: " + address);
            Console.WriteLine("authority: " + authority);
            Console.WriteLine("dnsSafeHost: " + dnsSafeHost);
            Console.WriteLine("leftPart(UriPartial.Authority): " + leftPart);
            Console.WriteLine("port: " + port);
            Console.WriteLine("scheme: " + scheme);
            Console.WriteLine("encoded: " + encoded);

            var indexOfSlash = address.Substring(scheme.Length + delimeter.Length).IndexOf(@"/");
            var fullHost = address.Substring(0, indexOfSlash + scheme.Length + delimeter.Length);
            Console.WriteLine("fullhost: " + fullHost);
        }
    }
}
