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
using Iced.Intel;
using System;
using System.Buffers;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Text;
using System.Text.Json;

namespace Benchmarks
{
    #if NET6_0_OR_GREATER
    [MemoryDiagnoser]
    public class MemstreamArrayBufferWriter
    {
        int numberofLoops = 50000;

        public static ReadOnlySpan<byte> Property1 => "Property1"u8;
        public static ReadOnlySpan<byte> Property2 => "Property2"u8;
        public static ReadOnlySpan<byte> Property3 => "Property3"u8;
        public static ReadOnlySpan<byte> Property4 => "Property4"u8;
        public static ReadOnlySpan<byte> Property5 => "Property5"u8;

        public static ReadOnlySpan<byte> StringValue1 => "StringValue1"u8;
        public static ReadOnlySpan<byte> StringValue2 => "StringValue2"u8;
        public static ReadOnlySpan<byte> StringValue3 => "StringValue3"u8;
        public static ReadOnlySpan<byte> StringValue4 => "StringValue4"u8;
        public static ReadOnlySpan<byte> StringValue5 => "StringValue5"u8;

        public static string Property1Str => "Property1";
        public static string Property2Str => "Property2";
        public static string Property3Str => "Property3";
        public static string Property4Str => "Property4";
        public static string Property5Str => "Property5";

        public static string StringValue1Str => "StringValue1";
        public static string StringValue2Str => "StringValue2";
        public static string StringValue3Str => "StringValue3";
        public static string StringValue4Str => "StringValue4";
        public static string StringValue5Str => "StringValue5";

        public void CompareUsingUtf8()
        {
            for (int i = 0; i < numberofLoops; i++)
            {

            }
        }

        public void CompareUsingString()
        {
            for (int i = 0; i < numberofLoops; i++)
            {

            }

        }

        //[Benchmark]
        public void UseArrayBufferWriter()
        {
            for (int i = 0; i < numberofLoops; i++)
            {
                Utf8JsonWriter writer = null;
                ArrayBufferWriter<byte> buffer = new ArrayBufferWriter<byte>();
                try
                {
                    writer = new Utf8JsonWriter(buffer);
                    WriteJsonUtf8Partial(ref writer);
                    writer.Flush();
                }
                finally
                {
                    if (writer != null)
                        writer.Dispose();
                }
            }
        }

        public void WriteJsonUtf8(ref Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            writer.WriteString(Property1, StringValue1);
            writer.WriteString(Property2, StringValue2);
            writer.WriteString(Property3, StringValue3);
            writer.WriteString(Property4, StringValue4);
            writer.WriteString(Property5, StringValue5);
            writer.WriteString(Property1, StringValue1);
            writer.WriteString(Property2, StringValue2);
            writer.WriteString(Property3, StringValue3);
            writer.WriteString(Property4, StringValue4);
            writer.WriteString(Property5, StringValue5);
            writer.WriteEndObject();
        }

        public void WriteJsonUtf8Partial(ref Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            writer.WriteString(Property1, StringValue1Str);
            writer.WriteString(Property2, StringValue2Str);
            writer.WriteString(Property3, StringValue3Str);
            writer.WriteString(Property4, StringValue4Str);
            writer.WriteString(Property5, StringValue5Str);
            writer.WriteString(Property1, StringValue1Str);
            writer.WriteString(Property2, StringValue2Str);
            writer.WriteString(Property3, StringValue3Str);
            writer.WriteString(Property4, StringValue4Str);
            writer.WriteString(Property5, StringValue5Str);
            writer.WriteEndObject();
        }

        public void WriteJsonStr(ref Utf8JsonWriter writer)
        {
            writer.WriteStartObject();
            writer.WriteString(Property1Str, StringValue1Str);
            writer.WriteString(Property2Str, StringValue2Str);
            writer.WriteString(Property3Str, StringValue3Str);
            writer.WriteString(Property4Str, StringValue4Str);
            writer.WriteString(Property5Str, StringValue5Str);
            writer.WriteString(Property1Str, StringValue1Str);
            writer.WriteString(Property2Str, StringValue2Str);
            writer.WriteString(Property3Str, StringValue3Str);
            writer.WriteString(Property4Str, StringValue4Str);
            writer.WriteString(Property5Str, StringValue5Str);
            writer.WriteEndObject();
        }

        [Benchmark]
        public void Str()
        {

            for (int i = 0; i < numberofLoops; i++)
            {
                Utf8JsonWriter writer = null;
                using (var buffer = new System.IO.MemoryStream())
                {
                    try
                    {
                        writer = new Utf8JsonWriter(buffer);
                        WriteJsonStr(ref writer);
                        writer.Flush();
                    }
                    finally
                    {
                        if (writer != null)
                            writer.Dispose();
                    }
                }
            }
        }

        [Benchmark]
        public void Utf8()
        {

            for (int i = 0; i < numberofLoops; i++)
            {
                Utf8JsonWriter writer = null;
                using (var buffer = new System.IO.MemoryStream())
                {
                    try
                    {
                        writer = new Utf8JsonWriter(buffer);
                        WriteJsonUtf8(ref writer);
                        writer.Flush();
                    }
                    finally
                    {
                        if (writer != null)
                            writer.Dispose();
                    }
                }
            }
        }

        [Benchmark]
        public void Utf8Partial()
        {

            for (int i = 0; i < numberofLoops; i++)
            {
                Utf8JsonWriter writer = null;
                using (var buffer = new System.IO.MemoryStream())
                {
                    try
                    {
                        writer = new Utf8JsonWriter(buffer);
                        WriteJsonUtf8Partial(ref writer);
                        writer.Flush();
                    }
                    finally
                    {
                        if (writer != null)
                            writer.Dispose();
                    }
                }
            }
        }
    }
//BenchmarkDotNet=v0.13.5, OS=Windows 11 (10.0.22621.2134/22H2/2022Update/SunValley2)
//Intel Core i7-10610U CPU 1.80GHz, 1 CPU, 8 logical and 4 physical cores
//.NET SDK= 8.0.100-preview.6.23330.14
//  [Host]     : .NET 6.0.21 (6.0.2123.36311), X64 RyuJIT AVX2
//  DefaultJob : .NET 6.0.21 (6.0.2123.36311), X64 RyuJIT AVX2


//|      Method |     Mean |    Error |   StdDev |       Gen0 | Allocated |
//|------------ |---------:|---------:|---------:|-----------:|----------:|
//|         Str | 80.76 ms | 2.208 ms | 6.406 ms | 64000.0000 | 255.58 MB |
//|        Utf8 | 40.50 ms | 0.766 ms | 0.851 ms | 64666.6667 | 258.26 MB |
//| Utf8Partial | 46.56 ms | 0.840 ms | 0.656 ms | 64300.0000 | 256.73 MB |
#endif
}
