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
using System.Threading;

namespace CodeSnips.Perf
{
    public class CopyBytes
    {
        static int buffSize = 50000;

        //Use different buffers to help avoid CPU cache effects
        static byte[]
            aSource = new byte[buffSize], aTarget = new byte[buffSize],
            bSource = new byte[buffSize], bTarget = new byte[buffSize],
            cSource = new byte[buffSize], cTarget = new byte[buffSize];

        public static void Run()
        {
            int count = 100000;
            TestArrayCopy(count, buffSize);
            TestBlockCopy(count, buffSize);

            Thread.Sleep(1000);

            TestBlockCopy(count, buffSize);
            TestArrayCopy(count, buffSize);

            Thread.Sleep(1000);

            TestArrayCopy(count, buffSize);
            TestBlockCopy(count, buffSize);

            Thread.Sleep(1000);

            TestArrayCopy(count, buffSize);
            TestBlockCopy(count, buffSize);

        }

        static void TestBlockCopy(int count, int size)
        {
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            for (int i = 0; i < count; i++)
                Buffer.BlockCopy(bSource, 0, bTarget, 0, size);
            sw.Stop();
            Console.WriteLine("Buffer.BlockCopy: {0:N0} ticks", sw.ElapsedTicks);
        }

        static void TestArrayCopy(int count, int size)
        {
            Stopwatch sw = Stopwatch.StartNew();
            sw.Start();
            for (int i = 0; i < count; i++)
                Array.Copy(cSource, 0, cTarget, 0, size);
            sw.Stop();
            Console.WriteLine("Array.Copy: {0:N0} ticks", sw.ElapsedTicks);
        }
    }
}
