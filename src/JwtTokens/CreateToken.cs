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
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using JwtRegisteredClaimNames = Microsoft.IdentityModel.JsonWebTokens.JwtRegisteredClaimNames;

namespace CodeSnips
{
    /// <summary>
    /// Creates a JwtToken
    /// </summary>
    public class CreateToken
    {
        public static void Run()
        {
            var audience = "http://relyingParty.com";
            var issuer = "http://relyingParty.com";
            var symmetricKeyString = "VbbbbmlbGJw8XH+ZoYBnUHmHga8/o/IduvU/Tht70iE=";
            var tokenHandler = new JwtSecurityTokenHandler();
            // ten days
            tokenHandler.TokenLifetimeInMinutes = 14400;
            var symmetricSecurityKey = new SymmetricSecurityKey(Convert.FromBase64String(symmetricKeyString));
            var subject = new ClaimsIdentity(
                new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Aud, audience),
                    new Claim(JwtRegisteredClaimNames.Email, "bob@contoso.com"),
                    new Claim(JwtRegisteredClaimNames.GivenName, "bob"),
                    new Claim(JwtRegisteredClaimNames.Sub, "123456789"),
                    new Claim("resource", "123456789")
                });

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = issuer,
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256),
                Subject = subject
            };

            var jwt = tokenHandler.CreateEncodedJwt(tokenDescriptor);

            Console.WriteLine($"Created jwt: {jwt}");
            Console.WriteLine($"SymmetricKey used: {symmetricKeyString}");
            Console.WriteLine($"JwtSecurityToken: {new JwtSecurityToken(jwt)}");
        }

        public static void AddSubClaim()
        {

            var symmetricKeyString = "VbbbbmlbGJw8XH+ZoYBnUHmHga8/o/IduvU/Tht70iE=";
            var tokenHandler = new JwtSecurityTokenHandler();
            // ten days
            tokenHandler.TokenLifetimeInMinutes = 14400;
            var symmetricSecurityKey = new SymmetricSecurityKey(Convert.FromBase64String(symmetricKeyString));

            var subject = new ClaimsIdentity(
                new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Email, "bob@contoso.com"),
                    new Claim(JwtRegisteredClaimNames.GivenName, "bob"),
                });

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Audience = "https://localhost:44369",
                Issuer = "https://localhost:44369",
                Subject = subject,
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256Signature)
            };

            //Token specific claims
            tokenDescriptor.Subject.AddClaim(new Claim(ClaimTypes.Name, "user@EmaiL.com", ClaimValueTypes.String));
            tokenDescriptor.Subject.AddClaim(new Claim("my.random.claim", "user@EmaiL.com"));
            tokenDescriptor.Subject.AddClaim(new Claim("sub", "user@EmaiL.com"));

            var token = tokenHandler.CreateJwtSecurityToken(tokenDescriptor);
            var jsonToken = new JsonWebToken(token.RawData);
        }

        public static void JwtWithActClaim()
        {
            JsonExtensions.Serializer = JsonConvert.SerializeObject;
            JsonExtensions.Deserializer = JsonConvert.DeserializeObject;

            var delegationClaim1 = new DelegationActorClaim("client1", string.Empty);
            var delegationClaim2 = new DelegationActorClaim("client2", System.Text.Json.JsonSerializer.Serialize(delegationClaim1));
            var delegationClaim3 = new DelegationActorClaim("client3", System.Text.Json.JsonSerializer.Serialize(delegationClaim2));
            var delegationClaim4 = new DelegationActorClaim("client4", System.Text.Json.JsonSerializer.Serialize(delegationClaim3));
            var claim = delegationClaim4.ToClaim();

            var jwtSecurityToken = CreateJwtSecurityToken(delegationClaim4);
            var jsonWebToken = CreateJsonWebToken(delegationClaim4);

            Console.WriteLine($"JwtSecurityToken: '{jwtSecurityToken}'.");
            Console.WriteLine($"JsonWebToken: '{jsonWebToken.Claims}'.");
        }

        private static JwtSecurityToken CreateJwtSecurityToken(DelegationActorClaim delegationActorClaim)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtPayload = new JwtPayload("http://localhost:5001", null, null, DateTime.UtcNow, DateTime.UtcNow.AddMinutes(2));

            // jwtPayload.AddClaim(claim);
            jwtPayload.Add("act", JToken.FromObject(delegationActorClaim));

            //jwtPayload.Add("act", delegationActorClaim.ToJson());
            var jwt = new JwtSecurityToken(new JwtHeader(), jwtPayload);
            var token = tokenHandler.WriteToken(jwt);
            var claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters { ValidateAudience = false, ValidateIssuer = false, RequireSignedTokens = false }, out SecurityToken securityToken);
            return new JwtSecurityToken(token);
        }

        private static JsonWebToken CreateJsonWebToken(DelegationActorClaim delegationActorClaim)
        {

            var tokenHandler = new JsonWebTokenHandler();
            var token = tokenHandler.CreateToken(
                new SecurityTokenDescriptor
                {
                    Claims = new Dictionary<string, object> { { "act", delegationActorClaim.ToJson() } },
                    Issuer = "http://localhost:5001",
                    NotBefore = DateTime.UtcNow,
                    Expires = DateTime.UtcNow.AddMinutes(2)
                }
            );

            var tokenValidationResult = tokenHandler.ValidateToken(token, new TokenValidationParameters { ValidateAudience = false, ValidateIssuer = false, RequireSignedTokens = false });
            return tokenValidationResult.SecurityToken as JsonWebToken;
        }
    }

    public class DelegationActorClaim
    {
        [JsonPropertyName("sub")]
        public string ClientId { get; set; } = null;

        [JsonPropertyName("act")]
        public DelegationActorClaim Actor { get; set; }

        public DelegationActorClaim() { }

        public DelegationActorClaim(string clientId, string previousActor)
        {
            ClientId = clientId;
            if (string.IsNullOrWhiteSpace(previousActor))
            {
                return;
            }

            Actor = System.Text.Json.JsonSerializer.Deserialize<DelegationActorClaim>(previousActor);
        }

        public Claim ToClaim()
        {
            return new Claim("act", System.Text.Json.JsonSerializer.Serialize(this), "json");
        }

        public string ToJson()
        {
            return System.Text.Json.JsonSerializer.Serialize(this);
        }
    }
}
