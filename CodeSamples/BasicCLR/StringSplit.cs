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

namespace CodeSnips.BasicCLR
{
    public class StringSplit
    {
        public static void Run()
        {
            SplitUp("  code ");
            SplitUp("code ");
            SplitUp("  code");
            SplitUp("code");
            SplitUp("  code id_token");
            SplitUp("code id_token ");
            SplitUp("  code id_token ");
            SplitUp("code id_token");
        }

        static void SplitUp(string str)
        {
            Console.WriteLine(" ====== ");
            var split = str.Split(' ');
            Console.WriteLine($"string: '{str}', number of parts: '{split.Length}");
            Console.WriteLine("Trim ====== ");
            var trimmed = str.Trim();
            var trimmedSplit = trimmed.Split(' ');
            Console.WriteLine($"string: '{trimmed}', number of parts: '{trimmedSplit.Length}");
            Console.WriteLine(" ====== ");
        }
    }
}
