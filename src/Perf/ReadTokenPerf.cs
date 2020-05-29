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
    public class ReadTokenPerf
    {
        const string JsonCompactSerializationRegex = @"^[A-Za-z0-9-_]+\.[A-Za-z0-9-_]+\.[A-Za-z0-9-_]*$";
        public static Regex RegexJws = new Regex(JsonCompactSerializationRegex, RegexOptions.Compiled | RegexOptions.CultureInvariant, TimeSpan.FromMilliseconds(100));

        public static void Run()
        {
            string token = "eyJ0eXAiOiJKV1QiLCJhbGciOiJSUzI1NiIsIng1dCI6Ik1uQ19WWmNBVGZNNXBPWWlKSE1iYTlnb0VLWSIsImtpZCI6Ik1uQ19WWmNBVGZNNXBPWWlKSE1iYTlnb0VLWSJ9.eyJhdWQiOiJodHRwOi8vUzJTQmFja2VuZCIsImlzcyI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0L2FkZDI5NDg5LTcyNjktNDFmNC04ODQxLWI2M2M5NTU2NDQyMC8iLCJpYXQiOjE0Njc0OTczMzAsIm5iZiI6MTQ2NzQ5NzMzMCwiZXhwIjoxNDY3NTAxMjMwLCJhcHBpZCI6IjJkMTQ5OTE3LTEyM2QtNGJhMy04Nzc0LTMyN2I4NzVmNTU0MCIsImFwcGlkYWNyIjoiMiIsImlkcCI6Imh0dHBzOi8vc3RzLndpbmRvd3MubmV0L2FkZDI5NDg5LTcyNjktNDFmNC04ODQxLWI2M2M5NTU2NDQyMC8iLCJvaWQiOiI5MTkxOTZmNi0zOGZkLTQ2ZjMtODY4Ni1hNDllMjk0NGIyNzciLCJzdWIiOiI5MTkxOTZmNi0zOGZkLTQ2ZjMtODY4Ni1hNDllMjk0NGIyNzciLCJ0aWQiOiJhZGQyOTQ4OS03MjY5LTQxZjQtODg0MS1iNjNjOTU1NjQ0MjAiLCJ2ZXIiOiIxLjAifQ.QLOroZZY53Gj97VuI2X66dxZ6vDIfJlDBwsDTAMJR8FcugucpWTyMtkCm9JcOHOb78lBwaMTJlOwUcb7qrwRrtjkxGCI3hUw-LBPREqM-AowlrUk1ORvB4CV7zDqH6m6s0LL91I3JpQEhMsQxo1OfcYyDR-vKJ5ybprYUgMIKmPeqGbUMLYDCwO9-0efl3LCdyI3FRlcbDg1960z2OlgmbFSlpQiT4bDDHszx1W0G0mJjO8Ypkfh3z_aBBoclkSR34lV_htJlCcW0CM7dopOzHACljCiJWgDh_q5pULLIWeGnYFKLtJZR7wSKp18a-k28xT_S1fgMqFooZ0r-5i3kA";
            var results = new double[3];
            results[0] = 0;
            results[1] = 0;
            results[2] = 0;

            var numberOfIterations = 15000;
            var totalIterations = 0;
            var totalLoops = 10;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
            var handler = new JwtSecurityTokenHandler();

            RunTests(results, 10, 1, token, handler);
            RunTests(results, 10, 2, token, handler);
            RunTests(results, 10, 0, token, handler);

            Console.WriteLine($"ReadTokenPerf ===================================");
            Console.WriteLine($"");
            Console.WriteLine($"Start test: iterations per loop: {numberOfIterations}.");

            for (int loops = 0; loops < totalLoops; loops++)
            {
                Console.WriteLine($"Start    loop: {loops+1} of {totalLoops}.");
                PerfTest(results, numberOfIterations, token, handler);
                Console.WriteLine($"Finished loop: {loops+1} of {totalLoops}.");
                totalIterations += numberOfIterations;
            }

            Console.WriteLine($"");
            Console.WriteLine($"Results ===================================");
            Console.WriteLine($"Handler:              '{results[0]}', Iterations: '{totalIterations}'.");
            Console.WriteLine($"Parts:                '{results[1]}', Iterations: '{totalIterations}'.");
            Console.WriteLine($"new JwtSecurityToken: '{results[2]}', Iterations: '{totalIterations}'.");

        }

        static void PerfTest(double[] results, int numberOfIterations, string token, JwtSecurityTokenHandler handler)
        {
            RunTests(results, numberOfIterations, 1, token, handler);
            RunTests(results, numberOfIterations, 2, token, handler);
            RunTests(results, numberOfIterations, 0, token, handler);
            RunTests(results, numberOfIterations, 2, token, handler);
            RunTests(results, numberOfIterations, 1, token, handler);
            RunTests(results, numberOfIterations, 1, token, handler);
            RunTests(results, numberOfIterations, 0, token, handler);
            RunTests(results, numberOfIterations, 2, token, handler);
            RunTests(results, numberOfIterations, 0, token, handler);
        }

        public static double RunTests(double[] results, int numberOfIterations, int test, string token, JwtSecurityTokenHandler handler)
        {
            Stopwatch sw = Stopwatch.StartNew();
            if (test == 0)
            {
                for (int i = 0; i < numberOfIterations; i++)
                {
                    var testRun = new TestRun();
                    testRun.ReadJwtWithHandler(token, handler);
                }
            }
            else if (test == 1)
            {
                for (int i = 0; i < numberOfIterations; i++)
                {
                    var testRun = new TestRun();
                    testRun.ReadJwtInParts(token);
                }
            }
            else if (test == 2)
            {
                for (int i = 0; i < numberOfIterations; i++)
                {
                    var testRun = new TestRun();
                    testRun.ReadJwtSecurityToken(token);
                }
            }

            sw.Stop();
            results[test] += sw.Elapsed.TotalMilliseconds;
            return sw.Elapsed.TotalMilliseconds;
        }
    }

    class TestRun
    {
        public void ReadJwtWithHandler(string jwt, JwtSecurityTokenHandler handler)
        {
            var token = handler.ReadJwtToken(jwt);
        }

        public void ReadJwtInParts(string jwt)
        {
            if (!ReadTokenPerf.RegexJws.IsMatch(jwt))
                return;

            var parts = jwt.Split('.');
            var header = JwtHeader.Base64UrlDeserialize(parts[0]);
            var payload = JwtPayload.Base64UrlDeserialize(parts[1]);
            var jwtToken = new JwtSecurityToken(header, payload, parts[0], parts[1], parts[2]);
        }

        public void ReadJwtSecurityToken(string jwt)
        {
            var jwtToken = new JwtSecurityToken(jwt);
        }
    }
}
