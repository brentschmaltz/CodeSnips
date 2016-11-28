//------------------------------------------------------------------------------
//
// Copyright (c) AuthFactors.com.
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

using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace CodeSnips.Json
{
    [JsonObject]
    class Client
    {
        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "Name", Required = Required.Default)]
        public string Name { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "RedirectUri", Required = Required.Default)]
        public IList<string> RedirectUri { get; set; }

        [JsonProperty(DefaultValueHandling = DefaultValueHandling.Ignore, NullValueHandling = NullValueHandling.Ignore, PropertyName = "Tennant", Required = Required.Default)]
        public string Tenant { get; set; }
    }

    public class JsonParsing
    {
        public static void Run()
        {
            var clients = new List<Client>
            {
                new Client
                {
                    Name = "cyrano",
                    RedirectUri = new List<string>{@"https://localhost:3110", @"https://localhost:3210"},
                    Tenant = "tenant"
                },
                new Client
                {
                    Name = "gotJwt",
                    RedirectUri = new List<string>{@"https://localhost:4110", @"https://localhost:4210"},
                    Tenant = "tenant"
                }
            };

            var fileName = Guid.NewGuid().ToString();
            var serializedClients = JsonConvert.SerializeObject(clients);
            StreamWriter writer = new StreamWriter(new FileStream(@"d:\temp\" + fileName, FileMode.CreateNew));
            writer.Write(serializedClients);
            writer.Flush();
            writer.Close();               
        }
    }
}
