using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using BenchmarkDotNet.Attributes;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;

namespace Benchmarks
{
    public class JsonClaims
    {
        JsonElement _rootElement;
        IDictionary<string, object> _properties;
        int loops = 10000;

        private const string _encodedJson = "eyJlbWFpbCI6IkJvYkBjb250b3NvLmNvbSIsImdpdmVuX25hbWUiOiJCb2IiLCJpc3MiOiJodHRwOi8vRGVmYXVsdC5Jc3N1ZXIuY29tIiwiYXVkIjoiaHR0cDovL0RlZmF1bHQuQXVkaWVuY2UuY29tIiwiaWF0IjoiMTQ4OTc3NTYxNyIsIm5iZiI6IjE0ODk3NzU2MTciLCJleHAiOiIyNTM0MDIzMDA3OTkifQ";
        static JsonWebToken jsonWebToken = new JsonWebToken(_encodedJson);
        static byte[] jsonBytes = Encoding.UTF8.GetBytes(Base64UrlEncoder.Decode(jsonWebToken.EncodedPayload));
        static string json = Base64UrlEncoder.Decode(jsonWebToken.EncodedPayload);

        [Benchmark]
        public void GetAsDictionary()
        {
            for (int i = 0; i < loops; i++)
            {
                var rootElement = JsonDocument.Parse(jsonWebToken.EncodedPayload).RootElement;
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
