using BenchmarkDotNet.Attributes;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Text.Json;

namespace Benchmarks
{
    [MemoryDiagnoser]
    public class DecodeJson
    {
        int loops = 100000;
        string json = "eyJlbWFpbCI6IkJvYkBjb250b3NvLmNvbSIsImdpdmVuX25hbWUiOiJCb2IiLCJpc3MiOiJodHRwOi8vRGVmYXVsdC5Jc3N1ZXIuY29tIiwiYXVkIjoiaHR0cDovL0RlZmF1bHQuQXVkaWVuY2UuY29tIiwiaWF0IjoiMTQ4OTc3NTYxNyIsIm5iZiI6IjE0ODk3NzU2MTciLCJleHAiOiIyNTM0MDIzMDA3OTkifQ";

        //[Benchmark]
        //public void DecodeUsingBase64UrlEncoding()
        //{
        //    try
        //    {
        //        for (int i = 0; i < loops; i++)
        //        {
        //            JsonDocument document = JsonUtils.GetJsonDocumentFromBase64UrlEncodedString(json, 0, json.Length);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //    }
        //}

        //[Benchmark]
        //public void DecodeUsingBase64UrlEncoder()
        //{
        //    try
        //    {
        //        for (int i = 0; i < loops; i++)
        //        {
        //           JsonDocument document = JsonDocument.Parse(Base64UrlEncoder.UnsafeDecode(json.ToCharArray()));
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        Console.WriteLine(ex.ToString());
        //    }
        //}
    }
}
