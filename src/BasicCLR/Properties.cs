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

namespace CodeSnips
{
    public class Properties
    {
        public static void Run()
        {
            var foo = new Foo();
            Console.WriteLine("Foo.Value (default): " + foo.Value);

            foo = new Foo
            {
                Value = "ValueSetAsProperty"
            };

            Console.WriteLine("Foo.Value (set in value): " + foo.Value);
        }

        class Foo
        {
            string _value;
            public Foo()
            {
                Console.WriteLine("Foo.Constructor");
                Value = "ValueSetInConstructor";
            }

            public string Value
            {
                get { return _value; }
                set { Console.WriteLine("Foo.Value, _value: '" + ( string.IsNullOrEmpty(_value) ? "NULL" : _value) + "', value: " + value); _value = value; }
            }
        }
    }
}
