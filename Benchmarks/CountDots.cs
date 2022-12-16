// Copyright (c) Brent Schmaltz. All rights reserved.
// Licensed under the MIT License.

using BenchmarkDotNet.Attributes;
using Iced.Intel;
using System;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;

namespace Benchmarks
{
    [MemoryDiagnoser]
    public class CountDots
    {
        string _jwt = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhdWQiOiJodHRwOi8vcmVseWluZ1BhcnR5LmNvbSIsImVtYWlsIjoiYm9iQGNvbnRvc28uY29tIiwiZ2l2ZW5fbmFtZSI6ImJvYiIsInN1YiI6IjEyMzQ1Njc4OSIsInJlc291cmNlIjoiMTIzNDU2Nzg5IiwibmJmIjoxNjYzMTcxMTMxLCJleHAiOjE2NjQwMzUxMzEsImlhdCI6MTY2MzE3MTEzMSwiaXNzIjoiaHR0cDovL3JlbHlpbmdQYXJ0eS5jb20ifQ.O_V1zfRDqekUSgMyq66maKVbipEffWRgUqhsJpe3kMc";
        int numberofLoops = 1500000;

        [Benchmark]
        public void StringSpilt()
        {
            string[] parts;
            for (int i = 0; i < numberofLoops; i++)
            {
                parts = BplitIntoParts(_jwt);
            }
        }

        public static string[] BplitIntoParts(string jwt)
        {
            return jwt.Split('.');
        }

        [Benchmark]
        public void CountTheDots()
        {
            int numberOfDots;
            for (int i = 0; i < numberofLoops; i++)
                numberOfDots = NumberOfDots(_jwt);
        }

        public static int NumberOfDots(string jwt)
        {
            int numberOfDots = 0;
            for (int i = 0; i < jwt.Length; i++)
                if (jwt[i] == '.')
                    numberOfDots++;

            return numberOfDots;
        }

        // * Summary *

        // BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.978/21H2)
        // Intel Core i7-10610U CPU 1.80GHz, 1 CPU, 8 logical and 4 physical cores
        // .NET SDK= 6.0.400
        // [Host]     : .NET 5.0.17 (5.0.1722.21314), X64 RyuJIT AVX2
        // DefaultJob : .NET 5.0.17 (5.0.1722.21314), X64 RyuJIT AVX2
        // |       Method |     Mean |   Error |  StdDev |       Gen0 |   Allocated |
        // |------------- |---------:|--------:|--------:|-----------:|------------:|
        // |  StringSpilt | 167.0 ms | 3.26 ms | 5.96 ms | 98333.3333 | 412000000 B |
        // | CountTheDots | 139.6 ms | 2.75 ms | 5.17 ms |          - |           - |
    }
}
