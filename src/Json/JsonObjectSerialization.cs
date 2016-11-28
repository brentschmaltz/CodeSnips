using System;
using System.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;

namespace CodeSnips.Json
{
    class JsonObjectSerialization
    {
        public static void Run()
        {
            var jobject = JObject.Parse(@"{""name"":[1,2,3]}");
            var jsonString = @"{""claims"":[{""claim1"":""claimValue1""}, {""claim2"":""claimValue2""}],""code"":""codevalue""}";
            var jObj = JObject.Parse(jsonString);
            var encodedJson = Base64UrlEncoder.Encode(jsonString);

            Console.WriteLine($"jObj: {jObj}");
            Debug.WriteLine($"encodedJson: {encodedJson}");

            foreach (var obj in jObj)
                Console.WriteLine($"obj: {obj}");
        }
    }
}
