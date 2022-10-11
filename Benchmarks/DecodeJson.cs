using BenchmarkDotNet.Attributes;
using Iced.Intel;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Runtime.InteropServices;
using System.Runtime.Intrinsics.X86;
using System.Text.Json;

namespace Benchmarks
{
    [MemoryDiagnoser]
    public class DecodeJson
    {
        int loops = 150000;
        string json = "eyJlbWFpbCI6IkJvYkBjb250b3NvLmNvbSIsImdpdmVuX25hbWUiOiJCb2IiLCJpc3MiOiJodHRwOi8vRGVmYXVsdC5Jc3N1ZXIuY29tIiwiYXVkIjoiaHR0cDovL0RlZmF1bHQuQXVkaWVuY2UuY29tIiwiaWF0IjoiMTQ4OTc3NTYxNyIsIm5iZiI6IjE0ODk3NzU2MTciLCJleHAiOiIyNTM0MDIzMDA3OTkifQ";

        //[Benchmark]
        //public void GetJsonDocumentFromBase64UrlEncodedString()
        //{
        //    try
        //    {
        //        for (int i = 0; i < loops; i++)
        //        {
        //            JsonDocument document = JwtTokenUtilities.GetJsonDocumentFromBase64UrlEncodedString(json, 0, json.Length);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //    }
        //}

        //[Benchmark]
        //public void ParseDocument()
        //{
        //    try
        //    {
        //        for (int i = 0; i < loops; i++)
        //        {
        //            byte[] bytes = Base64UrlEncoder.DecodeBytes(json);
        //            JsonDocument document = JwtTokenUtilities.ParseDocument(bytes, bytes.Length);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //    }
        //}

        [Benchmark]
        public void Base64UrlEncoderDecodeBytes()
        {
            try
            {
                for (int i = 0; i < loops; i++)
                {
                    JsonDocument document = JsonDocument.Parse(Base64UrlEncoder.DecodeBytes(json));
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        // * Summary *

        //  BenchmarkDotNet=v0.13.2, OS=Windows 11 (10.0.22000.978/21H2)
        //  Intel Core i7-10610U CPU 1.80GHz, 1 CPU, 8 logical and 4 physical cores
        //  .NET SDK= 6.0.400
        //  [Host]     : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT AVX2
        //  DefaultJob : .NET 6.0.9 (6.0.922.41905), X64 RyuJIT AVX2
        //  |                                    Method |     Mean |   Error |  StdDev |       Gen0 | Allocated |
        //  |------------------------------------------ |---------:|--------:|--------:|-----------:|----------:|
        //  | GetJsonDocumentFromBase64UrlEncodedString | 223.3 ms | 6.89 ms | 19.87 ms | 215.0 ms | 27666.6667 | 111.01 MB |
        //  |                             ParseDocument | 216.0 ms | 4.28 ms |  4.00 ms | 214.2 ms | 50333.3333 | 201.42 MB |
        //  |               Base64UrlEncoderDecodeBytes | 195.5 ms | 2.07 ms |  1.83 ms | 195.6 ms | 38000.0000 | 152.21 MB |
    }
}
