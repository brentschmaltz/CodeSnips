using Microsoft.IdentityModel.Tokens;
using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;

namespace CodeSnips.WilsonIssues
{
	// https://github.com/AzureAD/azure-activedirectory-identitymodel-extensions-for-dotnet/issues/1815
	public class WilsonIssue1815
    {
		public static void Run()
		{
			string token;
			var audience = "www.mysite.com";
			var issuer = "me";
			string rsaKeyXml;
			RSAParameters rsaParameters;
			using (var rsaProvider = new RSACryptoServiceProvider(2048))
			{
				rsaParameters = rsaProvider.ExportParameters(false);
				var rsa = new RsaSecurityKey(rsaProvider);
				var tokenDescriptor = new SecurityTokenDescriptor()
				{
					Expires = DateTime.UtcNow.AddSeconds(3600),
					Audience = audience,
					Issuer = issuer,
					SigningCredentials = new SigningCredentials(rsa, SecurityAlgorithms.RsaSha256Signature),
					IssuedAt = DateTime.UtcNow,
				};

				var tokenhandler = new JwtSecurityTokenHandler();
				token = tokenhandler.CreateEncodedJwt(tokenDescriptor);
			}

			Authorize(token, rsaParameters, audience, issuer);
			Console.WriteLine("No problem authorizing token first time");
			Authorize(token, rsaParameters, audience, issuer);
			Console.WriteLine("No problem authorizing token second time");
		}

		private static void Authorize(string token, RSAParameters rsaParameters, string audience, string issuer)
		{
			try
			{
				var rsa = new RsaSecurityKey(rsaParameters);
				TokenValidationParameters validationParameters = new TokenValidationParameters()
				{
					ValidIssuer = issuer,
					ValidAudiences = new string[] { audience },
					IssuerSigningKey = rsa,
				};
				var tokenhandler = new JwtSecurityTokenHandler();
				tokenhandler.ValidateToken(token, validationParameters, out var _);
			}
			catch(Exception ex)
			{
				Console.WriteLine($"Exception: '{ex}'.");
            }
		}
	}
}
