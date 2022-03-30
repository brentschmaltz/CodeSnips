using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.IdentityModel.Tokens;

namespace CodeSnips.Json
{
    public class jsonserializer
    {
        public static void Run()
        {
            JsonEncodedText jsonEncodedText = JsonEncodedText.Encode("THIS IS A STRING");
            string encoded = Base64UrlEncoder.Encode(jsonEncodedText.EncodedUtf8Bytes.ToArray());

            JsonProperty jsonProperty = new JsonProperty();

            var utcNow = DateTime.UtcNow;

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Email, "Bob@contoso.com", ClaimValueTypes.String, "issuer", "issuer"),
                new Claim(JwtRegisteredClaimNames.GivenName, "Bob", ClaimValueTypes.String, "issuer", "issuer"),
                new Claim(JwtRegisteredClaimNames.Iss, "issuer", ClaimValueTypes.String, "issuer", "issuer"),
                new Claim(JwtRegisteredClaimNames.Aud, "Audience", ClaimValueTypes.String, "issuer", "issuer"),
                new Claim(JwtRegisteredClaimNames.Aud.ToUpper(), "Audience", ClaimValueTypes.String, "issuer", "issuer"),
                new Claim(JwtRegisteredClaimNames.Iat, EpochTime.GetIntDate(utcNow).ToString(), ClaimValueTypes.String, "issuer", "issuer"),
                new Claim(JwtRegisteredClaimNames.Iat.ToUpper(), EpochTime.GetIntDate(utcNow).ToString(), ClaimValueTypes.String, "issuer", "issuer"),
                new Claim(JwtRegisteredClaimNames.Nbf, EpochTime.GetIntDate(utcNow).ToString(), ClaimValueTypes.String, "issuer", "issuer"),
                new Claim(JwtRegisteredClaimNames.Exp, EpochTime.GetIntDate(utcNow).ToString(), ClaimValueTypes.String, "issuer", "issuer"),
            };

            // This ClaimsIdentity has two duplicate claims (with different case): "aud"/"AUD" and "iat"/"IAT".
            var payloadClaimsIdentity = new ClaimsIdentity(new List<Claim>()
            {
                new Claim(JwtRegisteredClaimNames.Email, "Bob@contoso.com", ClaimValueTypes.String, "issuer", "issuer"),
                new Claim(JwtRegisteredClaimNames.GivenName, "Bob", ClaimValueTypes.String, "issuer", "issuer"),
                new Claim(JwtRegisteredClaimNames.Iss, "issuer", ClaimValueTypes.String, "issuer", "issuer"),
                new Claim(JwtRegisteredClaimNames.Aud, "Audience", ClaimValueTypes.String, "issuer", "issuer"),
                new Claim(JwtRegisteredClaimNames.Aud.ToUpper(), "Audience", ClaimValueTypes.String, "issuer", "issuer"),
                new Claim(JwtRegisteredClaimNames.Iat, EpochTime.GetIntDate(utcNow).ToString(), ClaimValueTypes.String, "issuer", "issuer"),
                new Claim(JwtRegisteredClaimNames.Iat.ToUpper(), EpochTime.GetIntDate(utcNow).ToString(), ClaimValueTypes.String, "issuer", "issuer"),
                new Claim(JwtRegisteredClaimNames.Nbf, EpochTime.GetIntDate(utcNow).ToString(), ClaimValueTypes.String, "issuer", "issuer"),
                new Claim(JwtRegisteredClaimNames.Exp, EpochTime.GetIntDate(utcNow).ToString(), ClaimValueTypes.String, "issuer", "issuer"),
            });

            string json = JsonSerializer.Serialize(claims);
        }
    }
}
