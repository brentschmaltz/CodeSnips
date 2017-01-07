using System;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;

namespace CodeSnips
{
    public class WriteJwtSecurityToken
    {
        public static void Run()
        {
            const string sec = "401b09eab3c013d4ca54922bb802bec8fd5318192b0a75f201d8b3727429090fb337591abd3e44453b954555b7a0812e1081c39b740293f765eae731f5a65ed1";
            var keyBytes = Encoding.Default.GetBytes(sec);
            var base64Key = Convert.ToBase64String(keyBytes);
            var now = DateTime.UtcNow;
            var securityKey = new SymmetricSecurityKey(keyBytes);
            var signingCredentials = new SigningCredentials(
                securityKey,
                SecurityAlgorithms.HmacSha256);

            var header = new JwtHeader(signingCredentials);
            var payload = new JwtPayload
            {
                { "iss", "a5fgde64-e84d-485a-be51-56e293d09a69"},
                { "scope", "https://example.com/ws"},
                { "aud", "https://example.com/oauth2/v1"},
                { "iat", now},
            };

            var secToken = new JwtSecurityToken(header, payload);

            var handler = new JwtSecurityTokenHandler();
            // ten days
            handler.TokenLifetimeInMinutes = 14400;
            var tokenString = handler.WriteToken(secToken);
            Console.WriteLine($"jwt: {tokenString}");
            Console.WriteLine($"base64Key: {base64Key}");
        }
    }
}
