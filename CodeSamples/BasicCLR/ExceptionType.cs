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
    public class CurrentException : Exception
    {
        public CurrentException(string message) : base(message)
        {
        }

        public CurrentException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public class NewException : CurrentException
    {
        public NewException(string message) : base(message)
        {
        }

        public NewException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }

    public class ExceptionType
    {
        public static void Run()
        {
            try
            {
                ThrowCurrent();
            }
            catch(CurrentException)
            {
                Console.WriteLine($"Caught CurrentException");
            }

            try
            {
                ThrowNew();
            }
            catch(NewException ex)
            {
                Console.WriteLine($"Caught NewException");
                Console.WriteLine($"Is CurrentException: { ex is CurrentException }");
                var asCurrent = (ex as CurrentException == null) ? "False" : "True";
                Console.WriteLine($"As CurrentException: { asCurrent }");
            }
            catch (CurrentException ex)
            {
                Console.WriteLine($"Caught CurrentException");
                Console.WriteLine($"Is CurrentException: { ex is CurrentException }");
            }

            try
            {
                ThrowNew();
            }
            catch (CurrentException ex)
            {
                Console.WriteLine($"Caught CurrentException");
                Console.WriteLine($"Is CurrentException: { ex is CurrentException }");
                var asCurrent = (ex as CurrentException == null) ? "False" : "True";
                Console.WriteLine($"As CurrentException: { asCurrent }");
            }
        }

        public static void ThrowCurrent()
        {
            Console.WriteLine("ThrowCurrent");

            throw new CurrentException("CurrentException");
        }

        public static void ThrowNew()
        {
            Console.WriteLine("ThrowNew");

            throw new NewException("NewException");
        }

    }
}
