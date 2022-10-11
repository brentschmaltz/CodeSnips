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
using Iced.Intel;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;

namespace Benchmarks
{
    [MemoryDiagnoser]
    public class Base64Encoding
    {
        //string _jwt = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJhdWQiOiJodHRwOi8vcmVseWluZ1BhcnR5LmNvbSIsImVtYWlsIjoiYm9iQGNvbnRvc28uY29tIiwiZ2l2ZW5fbmFtZSI6ImJvYiIsInN1YiI6IjEyMzQ1Njc4OSIsInJlc291cmNlIjoiMTIzNDU2Nzg5IiwibmJmIjoxNjYzMTc3MDQ5LCJleHAiOjE2NjQwNDEwNDksImlhdCI6MTY2MzE3NzA0OSwiaXNzIjoiaHR0cDovL3JlbHlpbmdQYXJ0eS5jb20ifQ._Qh41qLi1tnY4xKhCZHEMi1OT-_wx54Vuyz-ypKgh3M";
        string payload = "eyJhdWQiOiJodHRwOi8vcmVseWluZ1BhcnR5LmNvbSIsImVtYWlsIjoiYm9iQGNvbnRvc28uY29tIiwiZ2l2ZW5fbmFtZSI6ImJvYiIsInN1YiI6IjEyMzQ1Njc4OSIsInJlc291cmNlIjoiMTIzNDU2Nzg5IiwibmJmIjoxNjYzMTc3MDQ5LCJleHAiOjE2NjQwNDEwNDksImlhdCI6MTY2MzE3NzA0OSwiaXNzIjoiaHR0cDovL3JlbHlpbmdQYXJ0eS5jb20ifQ";
        int numberofLoops = 150000;

        [Benchmark]
        public void Decode()
        {
            for (int i = 0; i < numberofLoops; i++)
                Base64UrlEncoder.Decode(payload);
        }

        [Benchmark]
        public void DecodeBytes()
        {
            for (int i = 0; i < numberofLoops; i++)
                Base64UrlEncoder.DecodeBytes(payload);
        }

        //[Benchmark]
        //public void Base64UrlEncodingDecode()
        //{
        //    for (int i = 0; i < numberofLoops; i++)
        //        Base64UrlEncoding.Decode(payload);
        //}

        // 6.23.1
        // * Summary *
        //  |                  Method |     Mean |   Error |  StdDev |       Gen0 | Allocated |
        //  |------------------------ |---------:|--------:|--------:|-----------:|----------:|
        //  |                  Decode | 130.1 ms | 2.58 ms | 6.37 ms | 44000.0000 | 176.24 MB |
        //  |             DecodeBytes | 115.9 ms | 1.82 ms | 1.61 ms | 28500.0000 | 114.44 MB |
        //  | Base64UrlEncodingDecode | 102.9 ms | 1.67 ms | 1.56 ms |  8200.0000 |  33.19 MB |

        // 6.22.1
        // * Summary *
        //  |      Method |     Mean |   Error |  StdDev |   Median |       Gen0 | Allocated |
        //  |------------ |---------:|--------:|--------:|---------:|-----------:|----------:|
        //  |      Decode | 123.2 ms | 2.45 ms | 5.76 ms | 121.3 ms | 44000.0000 | 176.24 MB |
        //  | DecodeBytes | 106.0 ms | 1.17 ms | 1.10 ms | 105.9 ms | 28600.0000 | 114.44 MB |

        // 6.13.1
        // * Summary *
        //  BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.978/21H2)
        //  Intel Core i7-10610U CPU 1.80GHz, 1 CPU, 8 logical and 4 physical cores
        //  .NET SDK= 6.0.400
        //  [Host]     : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT AVX2
        //  DefaultJob : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT AVX2

        //  |      Method |     Mean |   Error |  StdDev |       Gen0 | Allocated |
        //  |------------ |---------:|--------:|--------:|-----------:|----------:|
        //  |      Decode | 121.4 ms | 2.21 ms | 2.36 ms | 44000.0000 | 176.24 MB |
        //  | DecodeBytes | 109.0 ms | 0.97 ms | 0.90 ms | 28600.0000 | 114.44 MB |

        // 6.10.1
        // * Summary *
        //  BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.978/21H2)
        //  Intel Core i7-10610U CPU 1.80GHz, 1 CPU, 8 logical and 4 physical cores
        //  .NET SDK= 6.0.400
        //  [Host]     : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT AVX2
        //  DefaultJob : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT AVX2

        //  |      Method |     Mean |   Error |  StdDev |       Gen0 | Allocated |
        //  |------------ |---------:|--------:|--------:|-----------:|----------:|
        //  |      Decode | 122.3 ms | 1.50 ms | 1.33 ms | 44000.0000 | 176.24 MB |
        //  | DecodeBytes | 116.0 ms | 1.46 ms | 1.36 ms | 28600.0000 | 114.44 MB |
    }
}
