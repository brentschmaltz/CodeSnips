using BenchmarkDotNet.Attributes;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Text.Json;

namespace Benchmarks
{
    public class JsonDeserialize
    {
        JsonElement _rootElement;
        IDictionary<string, object> _properties;
        int loops = 10000;

        private const string encodedJson = "eyJlbWFpbCI6IkJvYkBjb250b3NvLmNvbSIsImdpdmVuX25hbWUiOiJCb2IiLCJpc3MiOiJodHRwOi8vRGVmYXVsdC5Jc3N1ZXIuY29tIiwiYXVkIjoiaHR0cDovL0RlZmF1bHQuQXVkaWVuY2UuY29tIiwiaWF0IjoiMTQ4OTc3NTYxNyIsIm5iZiI6IjE0ODk3NzU2MTciLCJleHAiOiIyNTM0MDIzMDA3OTkifQ";
        private static string json = Base64UrlEncoder.Decode(encodedJson);
        private static byte[] jsonBytes = Base64UrlEncoder.DecodeBytes(encodedJson);

        [Benchmark]
        public void JsonDocumentParse()
        {
            try
            {
                for (int i = 0; i < loops; i++)
                {
                    _rootElement = JsonDocument.Parse(json).RootElement;
                }
            }
            catch(Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        [Benchmark]
        public void JsonDocumentParseBytes()
        {
            try
            {
                for (int i = 0; i < loops; i++)
                {
                    _rootElement = JsonDocument.Parse(jsonBytes).RootElement;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        [Benchmark]
        public void JsonSerializerDeserialize()
        {
            try
            {
                for (int i = 0; i < loops; i++)
                {
                    _properties = JsonSerializer.Deserialize<IDictionary<string, object>>(json);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

        [Benchmark]
        public void JsonSerializerDeserializeBytes()
        {
            try
            {
                for (int i = 0; i < loops; i++)
                {
                    _properties = JsonSerializer.Deserialize<IDictionary<string, object>>(jsonBytes);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }
        }

    }
}
