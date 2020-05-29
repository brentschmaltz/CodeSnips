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
using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Text.RegularExpressions;

namespace CodeSnips.Perf
{
    class CanReadJwt
    {

        public static void Run(string token, string tokenType)
        {
            var results = new double[3];
            results[0] = 0;
            results[1] = 0;
            results[2] = 0;

            var numberOfIterations = 25000;
            var totalIterations = 0;
            var totalLoops = 30;

            RunTests(results, 10, 1, token);
            RunTests(results, 10, 0, token);
            RunTests(results, 10, 2, token);

            Console.WriteLine($"CanReadTokenPerf: {tokenType} ===================================");
            Console.WriteLine($"");
            Console.WriteLine($"Start test: iterations per loop: {numberOfIterations}.");

            for (int loops = 0; loops < totalLoops; loops++)
            {
                Console.WriteLine($"Start    loop: {loops+1} of {totalLoops}.");
                PerfTest(results, numberOfIterations, token);
                Console.WriteLine($"Finished loop: {loops+1} of {totalLoops}.");
                totalIterations += numberOfIterations;
            }

            Console.WriteLine($"");
            Console.WriteLine($"Results ===================================");
            Console.WriteLine($"CanReadJwtSplit:         '{results[0]}', Iterations: '{totalIterations}'.");
            Console.WriteLine($"CanReadJwtSplitTwice:    '{results[1]}', Iterations: '{totalIterations}'.");
            Console.WriteLine($"CanReadJwtCount:         '{results[2]}', Iterations: '{totalIterations}'.");
        }

        static void PerfTest(double[] results, int numberOfIterations, string token)
        {
            RunTests(results, numberOfIterations, 1, token);
            RunTests(results, numberOfIterations, 0, token);
            RunTests(results, numberOfIterations, 2, token);
            RunTests(results, numberOfIterations, 2, token);
            RunTests(results, numberOfIterations, 1, token);
            RunTests(results, numberOfIterations, 1, token);
            RunTests(results, numberOfIterations, 0, token);
            RunTests(results, numberOfIterations, 2, token);
            RunTests(results, numberOfIterations, 0, token);
        }

        public static double RunTests(double[] results, int numberOfIterations, int test, string token)
        {
            Stopwatch sw = Stopwatch.StartNew();
            if (test == 0)
            {
                for (int i = 0; i < numberOfIterations; i++)
                {
                    var testRun = new TestRun();
                    testRun.CanReadJwtSplit(token);
                }
            }
            else if (test == 1)
            {
                for (int i = 0; i < numberOfIterations; i++)
                {
                    var testRun = new TestRun();
                    testRun.CanReadJwtSplitTwice(token);
                }
            }
            else if (test == 2)
            {
                for (int i = 0; i < numberOfIterations; i++)
                {
                    var testRun = new TestRun();
                    testRun.CanReadJwtCount(token);
                }
            }

            sw.Stop();
            results[test] += sw.Elapsed.TotalMilliseconds;
            return sw.Elapsed.TotalMilliseconds;
        }

        class TestRun
        {
            internal static Regex RegexJws = new Regex(JwtConstants.JsonCompactSerializationRegex, RegexOptions.Compiled | RegexOptions.CultureInvariant, TimeSpan.FromMilliseconds(100));
            internal static Regex RegexJwe = new Regex(JwtConstants.JweCompactSerializationRegex, RegexOptions.Compiled | RegexOptions.CultureInvariant, TimeSpan.FromMilliseconds(100));

            public bool CanReadJwtSplit(string token)
            {
                // Set the maximum number of segments to MaxJwtSegmentCount + 1. This controls the number of splits and allows detecting the number of segments is too large.
                // For example: "a.b.c.d.e.f.g.h" => [a], [b], [c], [d], [e], [f.g.h]. 6 segments.
                // If just MaxJwtSegmentCount was used, then [a], [b], [c], [d], [e.f.g.h] would be returned. 5 segments.
                string[] tokenParts = token.Split(new char[] { '.' }, 6);
                if (tokenParts.Length == 3)
                {
                    return RegexJws.IsMatch(token);
                }
                else if (tokenParts.Length == 5)
                {
                    return RegexJwe.IsMatch(token);
                }

                return false;
            }

            public bool CanReadJwtSplitTwice(string token)
            {
                // Set the maximum number of segments to MaxJwtSegmentCount + 1. This controls the number of splits and allows detecting the number of segments is too large.
                // For example: "a.b.c.d.e.f.g.h" => [a], [b], [c], [d], [e], [f.g.h]. 6 segments.
                // If just MaxJwtSegmentCount was used, then [a], [b], [c], [d], [e.f.g.h] would be returned. 5 segments.
                string[] tokenParts = token.Split(new char[] { '.' }, 6);
                string[] tokenParts2 = token.Split(new char[] { '.' }, 6);
                if (tokenParts.Length == 3)
                {
                    return RegexJws.IsMatch(token);
                }
                else if (tokenParts.Length == 5)
                {
                    return RegexJwe.IsMatch(token);
                }

                return false;
            }

            public bool CanReadJwtCount (string token)
            {
                // Set the maximum number of segments to MaxJwtSegmentCount + 1. This controls the number of splits and allows detecting the number of segments is too large.
                // For example: "a.b.c.d.e.f.g.h" => [a], [b], [c], [d], [e], [f.g.h]. 6 segments.
                // If just MaxJwtSegmentCount was used, then [a], [b], [c], [d], [e.f.g.h] would be returned. 5 segments.

                int count = 0;
                int next = -1;
                while((next = token.IndexOf('.', next+1)) != -1)
                {
                    count++;
                    if (count > 4)
                        break;
                }

                if (count == 2)
                {
                    return RegexJws.IsMatch(token);
                }
                else if (count == 4)
                {
                    return RegexJwe.IsMatch(token);
                }


                return false;

            }

            public bool CanReadJwtOnlyMatch(string token)
            {

                return RegexJws.IsMatch(token) || RegexJwe.IsMatch(token);
            }
        }
    }

}
