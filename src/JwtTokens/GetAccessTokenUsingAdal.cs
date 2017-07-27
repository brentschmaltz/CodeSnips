using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Cryptography.X509Certificates;
using Microsoft.IdentityModel.Clients.ActiveDirectory;

namespace CodeSnips
{
    public class GetAccessTokenUsingAdal
    {
        public static void Run()
        {
            string appToken;
            string resource = "http://S2SBackend";
            string authority = "https://login.microsoftonline.com/cyrano.onmicrosoft.com/";
            string clientId = "2d149917-123d-4ba3-8774-327b875f5540";
            string thumbprint = "348cd0f7b73e4d044758449ac853414b0b5a6d8b";
            // This token represents an access token between the two services.
            // Ideally it would be obtained once and refreshed, using the refresh token, when expired.
            try
            {
                X509Certificate2 certificate = FindCertificate(StoreLocation.LocalMachine, StoreName.My, thumbprint);
                AuthenticationContext authenticationContext = new AuthenticationContext(authority, false, null);
                ClientAssertionCertificate clientAssertion = new ClientAssertionCertificate(clientId, certificate);
                var authenticationResult = authenticationContext.AcquireTokenAsync(resource, clientAssertion).Result;
                appToken = authenticationResult.AccessToken;
                Console.WriteLine("");
                Console.WriteLine("=============== Access Token ================");
                Console.WriteLine($"{appToken}");
                var jwt = new JwtSecurityToken(appToken);
                Console.WriteLine("");
                Console.WriteLine("=============== Jwt Token ================");
                Console.WriteLine($"{jwt}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Adal threw: '{ex}'.");
            }
        }

        public static X509Certificate2 FindCertificate(StoreLocation storeLocation, StoreName storeName, string thumbprint)
        {
            X509Store x509Store = new X509Store(storeName, storeLocation);
            x509Store.Open(OpenFlags.ReadOnly);
            try
            {
                foreach (var cert in x509Store.Certificates)
                {
                    if (cert.Thumbprint.Equals(thumbprint, StringComparison.OrdinalIgnoreCase))
                    {
                        return cert;
                    }
                }

                throw new ArgumentException($"Unable to find certificate with thumbprint: '{thumbprint}'.");
            }
            finally
            {
                if (x509Store != null)
                {
                    x509Store.Close();
                }
            }
        }
    }
}
