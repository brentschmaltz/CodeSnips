using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.IdentityModel.Protocols;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.IdentityModel.Logging;

namespace CodeSnips.JwtTokens
{
    public class ValidateFileService
    {
        public static void Run()
        {
            // SAP Token
            string token = "eyJhbGciOiJSUzI1NiIsImprdSI6Imh0dHBzOi8vZmlsZXNlcnZpY2UtYXp1cmUtZ2EuYXV0aGVudGljYXRpb24uZXUyMC5oYW5hLm9uZGVtYW5kLmNvbS90b2tlbl9rZXlzIiwia2lkIjoiZGVmYXVsdC1qd3Qta2V5LS0xNTg5NzkzOTc1IiwidHlwIjoiSldUIn0.eyJqdGkiOiI0MTBlOWZiZjM4MDQ0ODc4Yjc0OWEyMWNhN2U0Y2RmMyIsImV4dF9hdHRyIjp7ImVuaGFuY2VyIjoiWFNVQUEiLCJzdWJhY2NvdW50aWQiOiJmOTBkMTMwOS0zNTQ2LTQxOTgtYTM3OC0yZDEwMDg3YmE1YjMiLCJ6ZG4iOiJmaWxlc2VydmljZS1henVyZS1nYSJ9LCJzdWIiOiJzYi1ocy1pbnRlcm9wLWZlZGVyYXRpb24tdWFhIXQ1OTk4IiwiYXV0aG9yaXRpZXMiOlsidWFhLnJlc291cmNlIiwiaHMtaW50ZXJvcC1mZWRlcmF0aW9uLXVhYSF0NTk5OC5yYW5kb20iXSwic2NvcGUiOlsidWFhLnJlc291cmNlIiwiaHMtaW50ZXJvcC1mZWRlcmF0aW9uLXVhYSF0NTk5OC5yYW5kb20iXSwiY2xpZW50X2lkIjoic2ItaHMtaW50ZXJvcC1mZWRlcmF0aW9uLXVhYSF0NTk5OCIsImNpZCI6InNiLWhzLWludGVyb3AtZmVkZXJhdGlvbi11YWEhdDU5OTgiLCJhenAiOiJzYi1ocy1pbnRlcm9wLWZlZGVyYXRpb24tdWFhIXQ1OTk4IiwiZ3JhbnRfdHlwZSI6ImNsaWVudF9jcmVkZW50aWFscyIsInJldl9zaWciOiJlYWM4NWFkYyIsImlhdCI6MTY0NjI1MTI5OSwiZXhwIjoxNjQ2Mjk0NDk5LCJpc3MiOiJodHRwczovL2ZpbGVzZXJ2aWNlLWF6dXJlLWdhLmF1dGhlbnRpY2F0aW9uLmV1MjAuaGFuYS5vbmRlbWFuZC5jb20vb2F1dGgvdG9rZW4iLCJ6aWQiOiJmOTBkMTMwOS0zNTQ2LTQxOTgtYTM3OC0yZDEwMDg3YmE1YjMiLCJhdWQiOlsic2ItaHMtaW50ZXJvcC1mZWRlcmF0aW9uLXVhYSF0NTk5OCIsInVhYSIsImhzLWludGVyb3AtZmVkZXJhdGlvbi11YWEhdDU5OTgiXX0.lS1ytd732DdYC02D26Ks83QyZAyaFlgyOTOVre9zu0SF9rUkwlv3hOaY7Dl5HS0BnlUl9UF0xIb08DYvgaFiU27D7Sm4u0lWPxMLuQXja8kehx7Dr84qrNpsN407fmUNba5Lrehd6KiI9A0LEuMGZV1j0QPH3xf8WmiN_Hc5B9Sf2gdFa_Jh4c5SEqo6_MIMP26Jv1B7e7A7F9_6hAGEt7ruYzQ94QSAzPBlLpdJLCm_ujL8edcrLQWZYFcwSGXIBYBCeoWoYt80lf6rlQAfM8WUwlgORgoG2ANAE3Wezq0xCUBIDvjUssmFU635CnlitTF4jpB4nvtCNNeZ368jJg";
            string modulus = "ALi6lHqcp79zfSTp-ReW_sV6uvwPaD4SRiWRo2wZ820ROpep5W389-I2My2ueRvbMhV4iCIraHekv3KFfnGgV1cs65p6ZMEIHo7IoyUJ7ULmH5S7fHjhddmQXrsy5JfisL6RSDO5CXccRLkyDdy8SWJZoCwVAdQpBe-nG49PXw1iQPRjRk6EhH1q8uqJ7bihzspiUz0OtW4Ym9BNn-9S3V_P3211-Qb1PNc_dqFKspNnnathJLtsD1qA0R-K7k5WRvRVwpUcB_Ix2w6y82GosFN4ZVMnsvmx7N2R9QDc-MlCPdvi-AXjxi_EKoWeQNm0lZtbNtrTncBCNMxupYwqYoM";
            string exponent = "AQAB";
            string keyId = "default-jwt-key--1589793975";

            //ValidateTokenOidc(token);
            ValidateTokenRsa(token, modulus, exponent, keyId);

        }

        static JwtSecurityToken ValidateTokenOidc(string token)
        {
            string stsDiscoveryEndpoint = "https://fileservice-azure-ga.authentication.eu20.hana.ondemand.com/oauth/token/.well-known/openid-configuration";

            ConfigurationManager<OpenIdConnectConfiguration> configManager = new ConfigurationManager<OpenIdConnectConfiguration>(stsDiscoveryEndpoint, new OpenIdConnectConfigurationRetriever());
            OpenIdConnectConfiguration config = configManager.GetConfigurationAsync().Result;

            TokenValidationParameters validationParameters = new TokenValidationParameters
            {
                ValidateAudience = false,
                ValidateIssuer = false,
                IssuerSigningKeys = config.SigningKeys,
                ValidateLifetime = false
            };

            JwtSecurityTokenHandler tokendHandler = new JwtSecurityTokenHandler();

            SecurityToken jwt;

            var result = tokendHandler.ValidateToken(token, validationParameters, out jwt);

            return jwt as JwtSecurityToken;
        }

        static void ValidateTokenRsa(string token, string modulus, string exponent, string keyId)
        {
            IdentityModelEventSource.ShowPII = true;

            var tokenHandler = new JwtSecurityTokenHandler();

            byte[] modBytes = Base64UrlEncoder.DecodeBytes(modulus);
            byte[] exponentBytes = Base64UrlEncoder.DecodeBytes(exponent);

            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider();
            rsa.ImportParameters(
              new RSAParameters()
              {
                  Modulus = modBytes,
                  Exponent = exponentBytes
              });

            RsaSecurityKey key = new RsaSecurityKey(new RSAParameters()
            {
                Modulus = modBytes,
                Exponent = exponentBytes
            });

            key = new RsaSecurityKey(rsa);

            key.KeyId = keyId;

            try
            {
                var result = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = key,
                    ClockSkew = TimeSpan.Zero,
                    ValidateAudience = false,
                    ValidateIssuer = false,
                    ValidateLifetime = false
                }, out SecurityToken validatedToken);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: {ex}");
            }
        }

    }
}

