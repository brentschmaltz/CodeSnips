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
    public static class Enum
    {
        public enum Enums
        {
            enum1 = 1,
            enum2
        }

        public static void Run()
        {
            if (EnumCheck((Enums)1))
                Console.WriteLine("(Enums)1) is Valid");
            else
                Console.WriteLine("(Enums)1) is NOT Valid");

            if (EnumCheck((Enums)2))
                Console.WriteLine("(Enums)2) is Valid");
            else
                Console.WriteLine("(Enums)2) is NOT Valid");

            if (EnumCheck((Enums)3))
                Console.WriteLine("(Enums)3) is Valid");
            else
                Console.WriteLine("(Enums)3) is NOT Valid");

            if (EnumCheck((Enums)10))
                Console.WriteLine("(Enums)10) is Valid");
            else
                Console.WriteLine("(Enums)10) is NOT Valid");

            if (EnumCheck(Enums.enum1))
                Console.WriteLine("(Enums.enum1) is Valid");
            else
                Console.WriteLine("(Enums.enum1) is NOT Valid");

            if (EnumCheck(Enums.enum2))
                Console.WriteLine("(Enums.enum2) is Valid");
            else
                Console.WriteLine("(Enums.enum2) is NOT Valid");
        }

        public static bool EnumCheck(Enums enumitem)
        {
            if (enumitem == Enums.enum1 || enumitem == Enums.enum2)
                return true;

            return false;
        }
    }
}
