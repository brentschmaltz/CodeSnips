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
using System.Collections.Generic;
using System.Security.Claims;

namespace CodeSnips
{
    public class TypeEquals
    {
        public static void Run()
        {
            string s = "s";
            bool b = false;
            int i = 0;
            long l = 1;
            double d = 1.0;
            List<string> list = new List<string>();

            Console.WriteLine($"{s.GetType()}, GetType: {GetType(s)}.");
            Console.WriteLine($"{s.GetType()}, GetTypeIsA: {GetTypeIsA(s)}.");

            Console.WriteLine($"{b.GetType()}, GetType: {GetType(b)}.");
            Console.WriteLine($"{b.GetType()}, GetTypeIsA: {GetTypeIsA(b)}.");

            Console.WriteLine($"{i.GetType()}, GetType: {GetType(i)}.");
            Console.WriteLine($"{i.GetType()}, GetTypeIsA: {GetTypeIsA(i)}.");

            Console.WriteLine($"{l.GetType()}, GetType: {GetType(l)}.");
            Console.WriteLine($"{l.GetType()}, GetTypeIsA: {GetTypeIsA(l)}.");

            Console.WriteLine($"{d.GetType()}, GetType: {GetType(d)}.");
            Console.WriteLine($"{d.GetType()}, GetTypeIsA: {GetTypeIsA(d)}.");

            Console.WriteLine($"GetTypeIsA - NULL: {GetTypeIsA(null)}.");

            Console.WriteLine($"{list.GetType()}, GetTypeIsA: {GetTypeIsA(list)}.");

        }

        private static string GetType(object o)
        {
            if (o.GetType().Name == typeof(string).Name)
                return ClaimValueTypes.String;

            if (o.GetType().Name == typeof(bool).Name)
                return ClaimValueTypes.Boolean;

            if (o.GetType().Name == typeof(int).Name)
                return ClaimValueTypes.Integer32;

            if (o.GetType().Name == typeof(long).Name)
                return ClaimValueTypes.Integer64;

            if (o.GetType().Name == typeof(double).Name)
                return ClaimValueTypes.Double;

            if (o.GetType().Name == typeof(DateTime).Name)
                return ClaimValueTypes.DateTime;

            return o.GetType().ToString();
        }

        private static string GetTypeIsA(object o)
        {
            if (o == null)
                return "null";

            if (o is string)
                return ClaimValueTypes.String;

            if (o is bool)
                return ClaimValueTypes.Boolean;

            if (o is int)
                return ClaimValueTypes.Integer32;

            if (o is long)
                return ClaimValueTypes.Integer64;

            if (o is double)
                return ClaimValueTypes.Double;

            if (o is DateTime)
                return ClaimValueTypes.DateTime;

            return o.GetType().ToString();
        }

    }
}
