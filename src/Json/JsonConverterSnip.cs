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
using System.Collections.Generic;
using System.Diagnostics;
using Newtonsoft.Json;

namespace CodeSnips.Json
{
    public class JsonConverterSnip
    {
        public static void Run()
        {
            var client = new ClientWithJsonConverterAttribute
            {
                Name = "bob",
                Tenant = "cyrano.com",
                RedirectUri = new List<string>
                {
                    @"https://cyrano.com",
                    @"https://got.jwt.com"
                }
            };

            var json = JsonConvert.SerializeObject(client);
            Console.WriteLine(json);
            var client2 = JsonConvert.DeserializeObject<ClientWithJsonConverterAttribute>(json);

            var derivedClient = new DerivedClient
            {
                Age = "42",
                Name = "bob",
                Tenant = "cyrano.com",
                RedirectUri = new List<string>
                {
                    @"https://cyrano.com",
                    @"https://got.jwt.com"
                }
            };

            json = JsonConvert.SerializeObject(derivedClient);
            Console.WriteLine(json);
            var derivedClientDeserialized = JsonConvert.DeserializeObject<DerivedClient>(json);

        }

        private class ClientConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return objectType == typeof(ClientWithJsonConverterAttribute);
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                var client = value as ClientWithJsonConverterAttribute;

                writer.WriteStartObject();
                writer.WritePropertyName("Name");
                writer.WriteValue(client.Name);
                writer.WritePropertyName("Tenant");
                writer.WriteValue(client.Tenant);
                writer.WritePropertyName("RedirectUri");
                writer.WriteStartArray();
                foreach (var item in client.RedirectUri)
                    writer.WriteValue(item);

                writer.WriteEndArray();
                var derivedClient = value as DerivedClient;
                if (derivedClient != null)
                {
                    writer.WritePropertyName("Age");
                    writer.WriteValue(derivedClient.Age);
                }

                writer.WriteEndObject();
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                object obj;

                if (objectType != null)
                    obj = Activator.CreateInstance(objectType);
                else
                    obj = new ClientWithJsonConverterAttribute();

                var client = obj as ClientWithJsonConverterAttribute;
                var derivedClient = obj as DerivedClient;

                while (reader.Read())
                {
                    if (reader.Value != null)
                        Debug.WriteLine("Token: {0}, Value: {1}", reader.TokenType, reader.Value);
                    else
                        Debug.WriteLine("Token: {0}", reader.TokenType);

                    if (reader.TokenType == JsonToken.EndObject) continue;

                    var value = reader.Value.ToString();
                    switch (value)
                    {
                        case "Name":
                            client.Name = reader.ReadAsString();
                            break;

                        case "Tenant":
                            client.Tenant = reader.ReadAsString();
                            break;

                        case "RedirectUri":
                            client.RedirectUri = new List<string>();
                            while (reader.Read())
                            {
                                if (reader.Value != null)
                                    Debug.WriteLine("Token: {0}, Value: {1}", reader.TokenType, reader.Value);
                                else
                                    Debug.WriteLine("Token: {0}", reader.TokenType);

                                if (reader.TokenType == JsonToken.StartArray)
                                    continue;
                                else if (reader.TokenType == JsonToken.String)
                                    client.RedirectUri.Add(reader.Value as string);
                                else if (reader.TokenType == JsonToken.EndArray)
                                    break;
                                else
                                    break;
                            }
                            break;

                        case "Age":
                            derivedClient.Age = reader.ReadAsString();
                            break;
                    }
                }

                return client;
            }
        }

        private class DerivedClient : ClientWithJsonConverterAttribute
        {
            public string Age { get; set; }
        }

        [JsonConverter(typeof(ClientConverter))]
        private class ClientWithJsonConverterAttribute
        {
            public string Name { get; set; }

            public List<string> RedirectUri { get; set; }

            public string Tenant { get; set; }

            public static ClientWithJsonConverterAttribute Create(string json)
            {
                return JsonConvert.DeserializeObject<ClientWithJsonConverterAttribute>(json);
            }
        }
    }
}
