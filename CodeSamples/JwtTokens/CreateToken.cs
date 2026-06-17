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

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.Json.Serialization;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Protocols.SignedHttpRequest;
using Microsoft.IdentityModel.Tokens;
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
            var audience = "https://relyingParty.com";
            var issuer = "https://issuer.com";
            var tokenHandler = new JsonWebTokenHandler();

            // Create unsigned token
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Claims = new Dictionary<string, object>
                {
                    { JwtRegisteredClaimNames.Aud, audience },
                    { JwtRegisteredClaimNames.Email, "bob@contoso.com" },
                    { JwtRegisteredClaimNames.GivenName, "bob" },
                    { JwtRegisteredClaimNames.Sub, "123456789" }
                }
            };

            string jwt = tokenHandler.CreateToken(tokenDescriptor);

            // Signed with RsaSecurityKey
            RSA rsa = RSA.Create(2048);
            RsaSecurityKey rsaSecurityKey = new(rsa);

            tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Claims = new Dictionary<string, object>
                {
                    { JwtRegisteredClaimNames.Aud, audience },
                    { JwtRegisteredClaimNames.Email, "bob@contoso.com" },
                    { JwtRegisteredClaimNames.GivenName, "bob" },
                    { JwtRegisteredClaimNames.Sub, "123456789" }
                },
                SigningCredentials = new SigningCredentials(rsaSecurityKey, SecurityAlgorithms.RsaSha256)
            };

            string jws = tokenHandler.CreateToken(tokenDescriptor);

            // Add PoP key
            RSA rsaForPoP = RSA.Create(2048);
            RsaSecurityKey rsaPoPSecurityKey = new(rsaForPoP);

            string jwkClaim = SignedHttpRequestUtilities.CreateJwkClaim(JsonWebKeyConverter.ConvertFromRSASecurityKey(rsaPoPSecurityKey));
            tokenDescriptor = new SecurityTokenDescriptor
            {
                Issuer = issuer,
                Claims = new Dictionary<string, object>
                {
                    { JwtRegisteredClaimNames.Aud, audience },
                    { JwtRegisteredClaimNames.Email, "bob@contoso.com" },
                    { JwtRegisteredClaimNames.GivenName, "bob" },
                    { JwtRegisteredClaimNames.Sub, "123456789" },
                    { Microsoft.IdentityModel.Tokens.ConfirmationClaimTypes.Cnf, jwkClaim }
                },
                SigningCredentials = new SigningCredentials(rsaSecurityKey, SecurityAlgorithms.RsaSha256)
            };

            string popToken = tokenHandler.CreateToken(tokenDescriptor);


            Console.WriteLine($"=================");
            Console.WriteLine($"JsonWebToken (JWT): {jwt}");
            Console.WriteLine($"=================");
            Console.WriteLine($"JsonWebToken (JWS): {jws}");
            Console.WriteLine($"=================");
            Console.WriteLine($"JsonWebToken (PoP): {popToken}");
            Console.WriteLine($"=================");
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
    }

    public class DelegationActorClaim
    {
        [JsonPropertyName("sub")]
        public string ClientId { get; set; } = string.Empty;

        [JsonPropertyName("act")]
        public DelegationActorClaim? Actor { get; set; }

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
