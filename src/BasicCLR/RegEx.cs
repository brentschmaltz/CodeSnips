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
using System.Text.RegularExpressions;
using System.Threading;

namespace CodeSnips
{
    public class RegEx
    {
        public static void Run()
        {
            AppDomain domain = AppDomain.CurrentDomain;
            object timeout = domain.GetData("REGEX_DEFAULT_MATCH_TIMEOUT");
            Console.WriteLine("Default regex match timeout (before setting): {0}",
                         timeout == null ? "<null>" : timeout);

            domain.SetData("REGEX_DEFAULT_MATCH_TIMEOUT", TimeSpan.MinValue);

            var regex = new Regex("");
            Console.WriteLine("regex.MatchTimeout: " + regex.MatchTimeout.ToString());
            if (regex.MatchTimeout == Timeout.InfiniteTimeSpan)
                Console.WriteLine("regex.MatchTimeOut == Timeout.InfiniteTimeSpan");

            Console.WriteLine("regex.MatchTimeout: " + regex.MatchTimeout.ToString());
        }
    }
}
