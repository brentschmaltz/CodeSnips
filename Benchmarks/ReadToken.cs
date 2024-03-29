﻿//------------------------------------------------------------------------------
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

using BenchmarkDotNet.Attributes;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Benchmarks
{
    [MemoryDiagnoser]
    public class ReadToken
    {
        string _jwt = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhdWQiOiJodHRwOi8vcmVseWluZ1BhcnR5LmNvbSIsImVtYWlsIjoiYm9iQGNvbnRvc28uY29tIiwiZ2l2ZW5fbmFtZSI6ImJvYiIsInN1YiI6IjEyMzQ1Njc4OSIsInJlc291cmNlIjoiMTIzNDU2Nzg5IiwibmJmIjoxNjYzMTc3MDQ5LCJleHAiOjE2NjQwNDEwNDksImlhdCI6MTY2MzE3NzA0OSwiaXNzIjoiaHR0cDovL3JlbHlpbmdQYXJ0eS5jb20ifQ._Qh41qLi1tnY4xKhCZHEMi1OT-_wx54Vuyz-ypKgh3M";
        int numberofLoops = 150000;

        [Benchmark]
        public void Read()
        {
            for (int i = 0; i < numberofLoops; i++)
               new JsonWebToken(_jwt);
        }

        // 6.23.1
        // * Summary *
        //  BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.978/21H2)
        //  Intel Core i7-10610U CPU 1.80GHz, 1 CPU, 8 logical and 4 physical cores
        //  .NET SDK= 6.0.400
        //  [Host]     : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT AVX2
        //  DefaultJob : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT AVX2
        //  | Method |     Mean |    Error |   StdDev |       Gen0 | Allocated |
        //  |------- |---------:|---------:|---------:|-----------:|----------:|
        //  |   Read | 410.9 ms | 20.39 ms | 57.84 ms | 59000.0000 | 235.75 MB |

        // 6.22.1
        // * Summary *
        //  BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.978/21H2)
        //  Intel Core i7-10610U CPU 1.80GHz, 1 CPU, 8 logical and 4 physical cores
        //  .NET SDK= 6.0.400
        //  [Host]     : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT AVX2
        //  DefaultJob : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT AVX2

        //  | Method |    Mean |    Error |   StdDev |        Gen0 | Allocated |
        //  |------- |--------:|---------:|---------:|------------:|----------:|
        //  |   Read | 1.101 s | 0.0112 s | 0.0087 s | 456000.0000 |   1.78 GB |

        // 6.13.1
        // * Summary *
        //  BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.978/21H2)
        //  Intel Core i7-10610U CPU 1.80GHz, 1 CPU, 8 logical and 4 physical cores
        //  .NET SDK= 6.0.400
        //  [Host]     : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT AVX2
        //  DefaultJob : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT AVX2

        //  | Method |    Mean |    Error |   StdDev |        Gen0 | Allocated |
        //  |------- |--------:|---------:|---------:|------------:|----------:|
        //  |   Read | 1.087 s | 0.0106 s | 0.0089 s | 457000.0000 |   1.78 GB |

        // 6.10.1
        // * Summary *
        //  BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.978/21H2)
        //  Intel Core i7-10610U CPU 1.80GHz, 1 CPU, 8 logical and 4 physical cores
        //  .NET SDK= 6.0.400
        //  [Host]     : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT AVX2
        //  DefaultJob : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT AVX2

        //| Method |    Mean |    Error |   StdDev |  Median |        Gen0 | Allocated |
        //|------- |--------:|---------:|---------:|--------:|------------:|----------:|
        //|   Read | 1.395 s | 0.0900 s | 0.2508 s | 1.325 s | 457000.0000 |   1.78 GB |
    }
}
