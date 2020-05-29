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

namespace CodeSnips.Perf
{
    public class ProcessMemoryUsage
    {
        public static void Run()
        {
            RunPrivate(null);
        }

        public static void Run(string[] args)
        {
            RunPrivate(args);
        }

        private static void RunPrivate(string[] args)
        {
            var process = Process.GetCurrentProcess();

            Console.WriteLine($"{process} -");
            Console.WriteLine("-------------------------------------");

            Console.WriteLine($"  HandleCount                   : {process.HandleCount}");
            Console.WriteLine($"  WorkingSet64 memory usage     : {process.WorkingSet64}");
            Console.WriteLine($"  VirtualMemorySize64           : {process.VirtualMemorySize64}");
            Console.WriteLine($"  PrivateMemorySize64           : {process.PrivateMemorySize64}");
            Console.WriteLine($"  PeakWorkingSet64              : {process.PeakWorkingSet64}");
            Console.WriteLine($"  PeakVirtualMemorySize64       : {process.PeakVirtualMemorySize64}");
            Console.WriteLine($"  PeakPagedMemorySize64         : {process.PeakPagedMemorySize64}");
            Console.WriteLine($"  PagedSystemMemorySize64       : {process.PagedSystemMemorySize64}");
            Console.WriteLine($"  PagedMemorySize64             : {process.PagedMemorySize64}");
            Console.WriteLine($"  Threads.Count                 : {process.Threads.Count }");
            Console.WriteLine($"  BasePriority                  : {process.BasePriority}");
            Console.WriteLine($"  PriorityClass                 : {process.PriorityClass}");
            Console.WriteLine($"  UserProcessorTime             : {process.UserProcessorTime}");
            Console.WriteLine($"  PrivilegedProcessorTime       : {process.PrivilegedProcessorTime}");
            Console.WriteLine($"  TotalProcessorTime            : {process.TotalProcessorTime}");
            Console.WriteLine($"  PagedSystemMemorySize64       : {process.PagedSystemMemorySize64}");
            Console.WriteLine($"  PagedMemorySize64             : {process.PagedMemorySize64}");

            if (process.Responding)
            {
                Console.WriteLine("Status = Running");
            }
            else
            {
                Console.WriteLine("Status = Not Responding");
            }
            Console.WriteLine("-------------------------------------");
            Console.WriteLine();
        }
    }
}
