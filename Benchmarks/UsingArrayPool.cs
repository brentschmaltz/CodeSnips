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

using BenchmarkDotNet.Attributes;
using System;
using System.Buffers;
using System.Text;

namespace Benchmarks
{
    [MemoryDiagnoser]
    public class UsingArrayPool
    {
        string _jwt = "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9eyJhdWQiOiJodHRwOi8vcmVseWluZ1BhcnR5LmNvbSIsImVtYWlsIjoiYm9iQGNvbnRvc28uY29tIiwiZ2l2ZW5fbmFtZSI6ImJvYiIsInN1YiI6IjEyMzQ1Njc4OSIsInJlc291cmNlIjoiMTIzNDU2Nzg5IiwibmJmIjoxNjYzMTcxMTMxLCJleHAiOjE2NjQwMzUxMzEsImlhdCI6MTY2MzE3MTEzMSwiaXNzIjoiaHR0cDovL3JlbHlpbmdQYXJ0eS5jb20ifQO_V1zfRDqekUSgMyq66maKVbipEffWRgUqhsJpe3kMc";
        int numberofLoops = 1500000;

        [Benchmark]
        public void UseArrayPool()
        {
            byte[] bytesToCopy = Encoding.UTF8.GetBytes(_jwt);
            for (int i = 0; i < numberofLoops; i++)
            {
                byte[] bytes = GetBytesFromArrayPool(_jwt, bytesToCopy.Length, bytesToCopy);
                UseBytes(bytes);
                ReleaseFromArrayPool(bytes);
            }
        }

        public static byte[] GetBytesFromArrayPool(string str, int size, byte[] bytesToCopy)
        {
            byte[] bytes = ArrayPool<byte>.Shared.Rent(size);
            Encoding.UTF8.GetBytes(str, 0, str.Length, bytes, 0);
            return bytes;
        }

        public static void ReleaseFromArrayPool(byte[] bytes)
        {
            ArrayPool<byte>.Shared.Return(bytes);
        }

        [Benchmark]
        public void CreatingNewArray()
        {
            byte[] bytesToCopy = Encoding.UTF8.GetBytes(_jwt);
            byte[] bytes;
            for (int i = 0; i < numberofLoops; i++)
            {
                bytes = GetBytesCreateArray(_jwt, bytesToCopy.Length, bytesToCopy);
                UseBytes(bytes);
            }
        }

        public static byte[] GetBytesCreateArray(string str, int size, byte[] bytesToCopy)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(str);
            return bytes;
        }

        public int UseBytes(byte[] bytes)
        {
            int count = 0;
            for(int i = 0; i < bytes.Length; i++)
                count++;

            return count;
        }
    }
}
