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


using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace CodeSnips.WilsonIssues
{
    internal class WilsonIssue1941
    {
        public enum TokenType
        {
            Unknown,
            User,
            Application
        }

        public static void Run()
        {
            var audience = "http://relyingParty.com";
            var issuer = "http://relyingParty.com";
            var symmetricKeyString = "VbbbbmlbGJw8XH+ZoYBnUHmHga8/o/IduvU/Tht70iE=";
            var tokenHandler = new JsonWebTokenHandler();
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
                    new Claim("type", "user"),
                    new Claim("Type", "Application")
                });

            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Issuer = issuer,
                SigningCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256),
                Subject = subject
            };

            var jwt = tokenHandler.CreateToken(tokenDescriptor);
            JsonWebToken jsonWebToken = new JsonWebToken(jwt);
            jsonWebToken.TryGetPayloadValue<TokenType>("type", out var tokenType);
            jsonWebToken.TryGetPayloadValue<TokenType>("Type", out var TokenType);

            Console.WriteLine($"Created jwt: {jwt}");
            Console.WriteLine($"SymmetricKey used: {symmetricKeyString}");
        }
    }
}
