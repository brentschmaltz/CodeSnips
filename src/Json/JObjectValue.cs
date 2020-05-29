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
using Newtonsoft.Json.Linq;

namespace CodeSnips
{
    class JObjectValue
    {
        public static void Run()
        {
            var claimNames = new List<string> {"array", "string", "intAsString", "integer", "integerZero", "integerNegative", "nill", "true", "false" };
            var jobject = JObject.Parse(@"{""array"":[1,""2"",3], ""string"":""bob"", ""intAsString"":""42"", ""integer"":42, ""integerZero"":0, ""integerNegative"": -1, ""nill"": null, ""true"" : true, ""false"" : false}");

            var i = 10;
            var o = (object)i;

            Obj(i);
            Obj(o);

            GetBool(jobject, claimNames);
            ToBool(jobject, claimNames);
            GetInt(jobject, claimNames);
            GetValue(jobject, claimNames);
            GetString(jobject, claimNames);
            GetObjectArray(jobject, claimNames);
            ToObjectArray(jobject, claimNames);
        }

        private static void Obj(object o)
        {
            Console.WriteLine($"type: {o.GetType()}");
        }

        private static void GetBool(JObject jObject, List<string> claimNames)
        {
            Console.WriteLine("================================");
            Console.WriteLine("GetBool");
            Console.WriteLine("");

            foreach (var claimName in claimNames)
            {
                Console.WriteLine($"claimName: {claimName}");
                try
                {
                    var jtoken = jObject.Value<bool>(claimName);
                    Console.WriteLine(""); 
                    Console.WriteLine($"jtoken: {jtoken}, typeof: {jtoken}.");
                    Console.WriteLine("");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("");
                    Console.WriteLine($"Exception: {ex}");
                    Console.WriteLine("");
                }
            }

            Console.WriteLine("");
        }

        private static void ToBool(JObject jObject, List<string> claimNames)
        {
            Console.WriteLine("================================");
            Console.WriteLine("ToBool");
            Console.WriteLine("");

            foreach (var claimName in claimNames)
            {
                Console.WriteLine($"claimName: {claimName}");
                try
                {

                    var jtoken = jObject.GetValue(claimName);
                    Console.WriteLine("");
                    Console.WriteLine($" jtoken: {jtoken}, typeof: {jtoken.Type}.");
                    Console.WriteLine("");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("");
                    Console.WriteLine($"Exception: {ex}");
                    Console.WriteLine("");
                }
            }
        }

        private static void GetInt(JObject jObject, List<string> claimNames)
        {
            Console.WriteLine("================================");
            Console.WriteLine("GetInt");

            foreach (var claimName in claimNames)
            {
                Console.WriteLine($"claimName: {claimName}");
                try
                {
                    var claimValue = jObject.Value<int>(claimName);
                    Console.WriteLine("");
                    Console.WriteLine($"claimValue<int>: {claimValue}.");
                    Console.WriteLine("");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("");
                    Console.WriteLine($"Exception: {ex}");
                    Console.WriteLine("");
                }
            }

            Console.WriteLine("");
        }

        private static void GetObjectArray(JObject jObject, List<string> claimNames)
        {
            Console.WriteLine("================================");
            Console.WriteLine("GetObjectArray");
            Console.WriteLine("");

            foreach (var claimName in claimNames)
            {
                Console.WriteLine($"claimName: {claimName}");
                try
                {
                    var claimValue = jObject.Value<object[]>(claimName);
                    Console.WriteLine("");
                    Console.WriteLine($"Value<object[]>: {claimValue}, typeof: {(claimValue == null ? "null" : claimValue.GetType().ToString())}.");
                    Console.WriteLine("");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("");
                    Console.WriteLine($"Exception: {ex}");
                    Console.WriteLine("");
                }
            }

            Console.WriteLine("");
        }

        private static void ToObjectArray(JObject jObject, List<string> claimNames)
        {
            Console.WriteLine("================================");
            Console.WriteLine("ToObjectArray");
            Console.WriteLine("");

            foreach (var claimName in claimNames)
            {
                Console.WriteLine($"claimName: {claimName}");
                try
                {
                    var claim = jObject.GetValue(claimName);
                    var claimValue = claim.ToObject<object[]>();
                    Console.WriteLine("");
                    Console.WriteLine($"claimValue<object[]>: {claimValue}, typeof: {(claimValue == null ? "null" : claimValue.GetType().ToString())}.");
                    Console.WriteLine("");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("");
                    Console.WriteLine($"Exception: {ex}");
                    Console.WriteLine("");
                }
            }

            Console.WriteLine("");
        }

        private static void GetValue(JObject jObject, List<string> claimNames)
        {
            Console.WriteLine("================================");
            Console.WriteLine("GetValue");
            Console.WriteLine("");

            foreach (var claimName in claimNames)
            {
                Console.WriteLine($"claimName: {claimName}"); 
                try
                {
                    var jToken = jObject.GetValue(claimName);
                    Console.WriteLine("");
                    Console.WriteLine($"jToken: {jToken}, jToken.Type: {jToken.Type}.");
                    Console.WriteLine("");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("");
                    Console.WriteLine($"Exception: {ex}");
                    Console.WriteLine("");
                }
            }

            Console.WriteLine("");
        }

        private static void GetString(JObject jObject, List<string> claimNames)
        {
            Console.WriteLine("================================");
            Console.WriteLine("GetString");
            Console.WriteLine("");

            foreach (var claimName in claimNames)
            {
                Console.WriteLine($"claimName: {claimName}"); 
                try
                {
                    var claimValue = jObject.Value<string>(claimName);
                    Console.WriteLine("");
                    Console.WriteLine($"claimValue<string>: {claimValue}, typeof: {(claimValue == null ? "null" : claimValue.GetType().ToString())}.");
                    Console.WriteLine("");
                }
                catch (Exception ex)
                {
                    Console.WriteLine("");
                    Console.WriteLine($"Exception: {ex}");
                    Console.WriteLine("");
                }
            }

            Console.WriteLine("");
        }
    }
}
