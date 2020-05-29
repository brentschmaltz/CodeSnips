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
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CodeSnips.Json
{
    public class JsonSnip
    {
        public static void Run()
        {
            var jArray = JToken.FromObject(new List<int> { 1, 2, 3 });
            var jBool = JToken.FromObject(true);
            var jHex = JToken.FromObject(0xab);
            var jInt = JToken.FromObject(5);
            var jNull = JToken.FromObject("null");
            var jNumber = JToken.FromObject(4.56);
            var jString = JToken.FromObject("value4.2");
            var jwk = JToken.FromObject(new JsonWebKey
            {
                K = "fpdOQGL9hFCg2d3cLNDnPK9qbHA25zYPcfLpk3gKFzU=",
                KeyId = "SigningKey",
                Kty = JsonWebAlgorithmsKeyTypes.Octet
            });

            var jobj = new JObject();
            jobj.Add("jArray", jArray);
            jobj.Add("jBool", jBool);
            jobj.Add("jHex", jHex);
            jobj.Add("jInt", jInt);
            jobj.Add("jNull", jNull);
            jobj.Add("jNumber", jNumber);
            jobj.Add("jString", jString);
            jobj.Add("jwk", jwk);

            Console.WriteLine($"jobj: '{jobj}'");
            Console.WriteLine("");
            Console.WriteLine($"jobj (Formatting.None): '{jobj.ToString(Formatting.None)}'");
            Console.WriteLine("");

            var jsonSettings = new JsonSerializerSettings
            {
                DefaultValueHandling = DefaultValueHandling.Ignore,
                Formatting = Formatting.None,
                NullValueHandling = NullValueHandling.Ignore
            };

            Console.WriteLine("jwk:");
            Console.WriteLine(JsonConvert.SerializeObject(jwk, jsonSettings));
            Console.WriteLine("");

            var sb = new StringBuilder();
            foreach(var prop in jobj)
                sb.Append(prop.ToString());

            Console.WriteLine($"jobj each prop:");
            Console.WriteLine($"{sb}");
            Console.WriteLine("");

            sb.Clear();
            foreach (var prop in jobj)
                sb.Append($"\"{prop.Key}\":\"{prop.Value}\"");

            Console.WriteLine($"jobj by prop.Key : prop.Value:");
            Console.WriteLine($"{sb}");
            Console.WriteLine("");
        }
    }
}
