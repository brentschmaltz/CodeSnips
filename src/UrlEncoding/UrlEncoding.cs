using System;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace CodeSnips.UrlEncoding
{
    public class Base64UrlEncoding
    {
        public static void Run()
        {
            var payload = "It\u2019s a dangerous business, Frodo, going out your door. You step onto the road, and if you don't keep your feet, there\u2019s no knowing where you might be swept off to.";
            var payloadEscaped = "It's a dangerous business, Frodo, going out your door. You step onto the road, and if you don't keep your feet, there's no knowing where you might be swept off to.";
            var encodedPayload = "SXTigJlzIGEgZGFuZ2Vyb3VzIGJ1c2luZXNzLCBGcm9kbywgZ29pbmcgb3V0IHlvdXIgZG9vci4gWW91IHN0ZXAgb250byB0aGUgcm9hZCwgYW5kIGlmIHlvdSBkb24ndCBrZWVwIHlvdXIgZmVldCwgdGhlcmXigJlzIG5vIGtub3dpbmcgd2hlcmUgeW91IG1pZ2h0IGJlIHN3ZXB0IG9mZiB0by4";
            var header = @"{""alg"":""RS256"",""kid"":""bilbo.baggins@hobbiton.example""}";
            var encodedHeader = "eyJhbGciOiJSUzI1NiIsImtpZCI6ImJpbGJvLmJhZ2dpbnNAaG9iYml0b24uZXhhbXBsZSJ9";

            var symmetricHeader = "{\"alg\":\"HS256\",\"kid\":\"018c0ae5-4d9b-471b-bfd6-eef314bc7037\"}";
            var symmetricHeaderEncoded = "eyJhbGciOiJIUzI1NiIsImtpZCI6IjAxOGMwYWU1LTRkOWItNDcxYi1iZmQ2LWVlZjMxNGJjNzAzNyJ9";
            var symmetricEncoded = Base64UrlEncoder.Encode(symmetricHeader);

            Console.WriteLine(symmetricEncoded);
            Console.WriteLine(symmetricHeaderEncoded);
            if (symmetricHeaderEncoded.Equals(symmetricEncoded, StringComparison.Ordinal))
                Console.WriteLine("symmetricHeaderEncoded == symmetricEncoded, StringComparison.Ordinal");
            else
                Console.WriteLine("symmetricHeaderEncoded != symmetricEncoded, StringComparison.Ordinal");

            var headerEncoded = Base64UrlEncoder.Encode(header);
            Console.WriteLine(headerEncoded);
            Console.WriteLine(encodedHeader);
            if (encodedHeader.Equals(headerEncoded, StringComparison.Ordinal))
                Console.WriteLine("encodedHeader == headerEncoded, StringComparison.Ordinal");
            else
                Console.WriteLine("encodedHeader != headerEncoded, StringComparison.Ordinal");


            var payloadEncoded = Base64UrlEncoder.Encode(payload);
            var payloadEscapedEncoded = Base64UrlEncoder.Encode(payloadEscaped);

            Console.WriteLine(encodedPayload);
            Console.WriteLine(payloadEncoded);
            Console.WriteLine(payloadEscapedEncoded);

            if (encodedPayload.Equals(payloadEncoded, StringComparison.Ordinal))
                Console.WriteLine("encodedPayload == payloadEncoded, StringComparison.Ordinal");
            else
                Console.WriteLine("encodedPayload != payloadEncoded, StringComparison.Ordinal");

            if (encodedPayload.Equals(payload, StringComparison.Ordinal))
                Console.WriteLine("encodedPayload == payloadEscapedEncoded, StringComparison.Ordinal");
            else
                Console.WriteLine("encodedPayload != payloadEscapedEncoded, StringComparison.Ordinal");

            var decodedPayload = Base64UrlEncoder.Decode(encodedPayload);
            Console.WriteLine(payload);
            Console.WriteLine(payloadEscaped);
            Console.WriteLine(decodedPayload);

            if (decodedPayload.Equals(payloadEscaped, StringComparison.Ordinal))
                Console.WriteLine("decodedPayload == payloadEscaped, StringComparison.Ordinal");
            else
                Console.WriteLine("decodedPayload != payloadEscaped, StringComparison.Ordinal");

            if (decodedPayload.Equals(payload, StringComparison.Ordinal))
                Console.WriteLine("decodedPayload == payload, StringComparison.Ordinal");
            else
                Console.WriteLine("decodedPayload != payload, StringComparison.Ordinal");
        }
    }
}
