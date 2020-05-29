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
using Moq;

namespace CodeSnips
{
    public class Moq
    {
        public static void Run()
        {
            var mock = new Mock<IFoo>();
            int calls = 1;
            mock.Setup(foo => foo.AreEqual("bob", "frank")).Returns(true);
            mock.Setup(foo => foo.ToUpper(It.IsAny<string>())).Returns((string s) => s.ToUpper());
            mock.Setup(foo => foo.ToUpper("frank")).Throws<InvalidOperationException>();
            mock.Setup(foo => foo.GetCountThing()).Returns(() => calls).Callback(() => calls++);
            //mock.Setup(foo => foo.Name).Returns("bar");
            mock.SetupSet(foo => foo.Name = "foo");
            RunVariation(mock, "bob", "frank");
            RunVariation(mock, "bob", "Frank");            
        }

        private static void RunVariation(Mock<IFoo> mock, string str1, string str2)
        {
            Debug.WriteLine("str1: '" + str1 + "', str2: " + str2 + ". mock.AreEqual : " + mock.Object.AreEqual(str1, str2));
            Console.WriteLine("str1: '" + str1 + "', str2: " + str2 + ". mock.AreEqual : " + mock.Object.AreEqual(str1, str2));

            Debug.WriteLine("str1: '" + str1 + "', ToUpper: " + mock.Object.ToUpper(str1));
            Console.WriteLine("str1: '" + str1 + "', ToUpper: " + mock.Object.ToUpper(str1));

            Console.WriteLine("calls: '" + mock.Object.GetCountThing() + "'");
            Console.WriteLine("mock.Name: '" + mock.Object.Name + "'");

            try
            {
                mock.Object.ToUpper(str2);
            }
            catch(Exception ex)
            {
                Console.WriteLine("mock.Object.ToUpper('" + str2 + "'), Exception: " + ex.ToString());
            }
        }
    }

    public interface IFoo
    {
        bool AreEqual(string str1, string str2);
        string ToUpper(string str1);
        int GetCountThing();
        string Name { get; set; }
    }
}
