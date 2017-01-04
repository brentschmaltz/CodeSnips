using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace CodeSnips
{
    /// <summary>
    /// Creates a JwtToken
    /// </summary>
    public class CreateToken
    {
        public static void Run()
        {
            var audience = "microsoft.com";
            var issuer = "contoso.com";
            var symmetricKeyString = "VbbbbmlbGJw8XH+ZoYBnUHmHga8/o/IduvU/Tht70iE=";
            var tokenHandler = new JwtSecurityTokenHandler();
            var symmetricSecurityKey = new SymmetricSecurityKey(Convert.FromBase64String(symmetricKeyString));
            var subject = new ClaimsIdentity(
                new List<Claim>
                {
                    new Claim(JwtRegisteredClaimNames.Aud, audience),
                    new Claim(JwtRegisteredClaimNames.Email, "bob@contoso.com"),
                    new Claim(JwtRegisteredClaimNames.GivenName, "bob"),
                    new Claim(JwtRegisteredClaimNames.Sub, "123456789")
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
    }
}
