using Microsoft.IdentityModel.Tokens;
using System;
using System.Globalization;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Stress
{
    public class GetPrivateRSAKey
    {
        public static void Run(int numberLoops)
        {
            X509Certificate2 cert = GetCertificate(StoreName.My, StoreLocation.LocalMachine, "CN=TestingSTS");
            byte[] bytes = Guid.NewGuid().ToByteArray();
            
            X509SecurityKey x509SecurityKey = new(cert);
            X509SigningCredentials x509SigningCredentials = new X509SigningCredentials(cert);

            while (true)
            {
                //GetPrivateKey(x509SigningCredentials, bytes);
                GetPrivateKey(new X509SigningCredentials(cert), bytes);
            }
        }

        private static void GetPrivateKey(X509Certificate2 cert, byte[] bytes)
        {
            var key = cert.GetRSAPrivateKey();
            key.SignData(bytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }

        private static void GetPrivateKey(X509SecurityKey x509SecurityKey, byte[] bytes)
        {
            var key = x509SecurityKey.PrivateKey as RSA;
            key.SignData(bytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }

        private static void GetPrivateKey(X509SigningCredentials x509SigningCredentials, byte[] bytes)
        {
            var key = ((x509SigningCredentials.Key) as X509SecurityKey).PrivateKey as RSA;
            key.SignData(bytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
        }

        public static X509Certificate2 GetCertificate(StoreName name, StoreLocation location, string subjectName)
        {
            X509Store store = new X509Store(name, location);
            X509Certificate2Collection certificates = null;
            store.Open(OpenFlags.ReadOnly);
            try
            {
                X509Certificate2 result = null;

                // Every time we call store.Certificates property, a new collection will be returned.
                certificates = store.Certificates;

                for (int i = 0; i < certificates.Count; i++)
                {
                    X509Certificate2 cert = certificates[i];

                    if (cert.SubjectName.Name.Equals(subjectName, StringComparison.OrdinalIgnoreCase))
                    {
                        if (result != null)
                        {
                            throw new CryptographicException(string.Format(CultureInfo.InvariantCulture, "There are multiple certificates for subject Name {0}", subjectName));
                        }

                        result = new X509Certificate2(cert);
                    }
                }

                if (result == null)
                {
                    throw new CryptographicException(string.Format(CultureInfo.InvariantCulture, "No certificate was found for subject Name {0}", subjectName));
                }

                return result;
            }
            finally
            {
                if (certificates != null)
                {
                    for (int i = 0; i < certificates.Count; i++)
                    {
                        X509Certificate2 cert = certificates[i];
                        cert.Reset();
                    }
                }

                store.Close();
            }
        }
    }

}
