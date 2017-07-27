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

namespace CodeSnips
{
    public class StringFormat
    {
        public static void Run()
        {
            string str = null;

            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Null string format: '{0}'", str));
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "Null string format (?? string.Empty): '{0}'", str ?? string.Empty));
            var strFormatted = string.Format(CultureInfo.InvariantCulture, "Null string format: '{0}'", str);
            Console.WriteLine(string.Format(CultureInfo.InvariantCulture, "String with null param: '{0}'.", strFormatted));

            object[] args = null;
            try
            {
                strFormatted = string.Format(CultureInfo.InvariantCulture, "Null string format: '{0}', '{1}'", args );
                Console.WriteLine("null args did NOT throw");
            }
            catch(Exception ex)
            {
                Console.WriteLine("null args threw: '{0}'", ex);
            }
        }
    }
}
