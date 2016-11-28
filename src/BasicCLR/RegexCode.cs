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
using System.Text.RegularExpressions;

namespace CodeSnips
{
    public class RegexCode
    {
        public static void Run()
        {
            string[] inputs = 
            {
                "abc?bob=ff",
                " ?bob=ff",
                "abc? = ",
                "?bob=ff",
                "abcbob=ff",
                "??abcbob=ff",
                "abcbob?ff",
            };

            RunVariation(@"\?.*=.*", inputs);
            RunVariation(@".*\?.*=.*", inputs);
            RunVariation(@"\?([^= ]+=[^= ])", inputs);
            RunVariation(@"\?([\w]+=[\w])", inputs);
            RunVariation(@"\?([\w+]+=[\w+])", inputs);
        }

        private static void RunVariation(string pattern, string[] inputs)
        {
            Debug.WriteLine("***** pattern: '" + pattern + "'");
            Console.WriteLine("***** pattern: '" + pattern + "'");

            foreach (var input in inputs)
            {
                try
                {
                    Regex regex = new Regex(pattern);
                    Match match = regex.Match(input);
                    var matches = regex.Matches(input);
                    var isMatch = regex.IsMatch(input);

                    Debug.WriteLine("input: '" + input + "', match: " + isMatch);
                    Console.WriteLine("input: '" + input + "', match: " + isMatch);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine("Exception: " + ex.ToString());
                    Console.WriteLine("Exception: " + ex.ToString());
                }
            }

            Debug.WriteLine("*****");
            Console.WriteLine("*****");
        }
    }
}
