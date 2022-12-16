using Microsoft.IdentityModel.Tokens;
using System;
using System.Diagnostics;
using System.Globalization;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace Stress
{
    public class GetPublicKeyCert
    {
        public static void Run(int numberLoops)
        {
            X509Certificate2 cert = GetCertificate(StoreName.My, StoreLocation.LocalMachine, "CN=SelfSignedTestCert");
            byte[] bytes = Guid.NewGuid().ToByteArray();
            X509SecurityKey x509SecurityKey = new(cert);
            var key = x509SecurityKey.PrivateKey as RSA;
            byte[] signature = key.SignData(bytes, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            Stopwatch stopwatch = Stopwatch.StartNew();

            while (true)
            {
                x509SecurityKey = new(cert);
                ValidateSignature(x509SecurityKey, bytes, signature);
                if (stopwatch.ElapsedMilliseconds > 3000)
                {
                    var process = Process.GetCurrentProcess();
                    Console.WriteLine($"NT Handles: '{process.HandleCount}', Memory: '{process.PrivateMemorySize64}'");
                    stopwatch.Restart();
                }
            }
        }

        private static void ValidateSignature(X509SecurityKey x509SecurityKey, byte[] bytes, byte[] signature)
        {
            var key = x509SecurityKey.PublicKey as RSA;
            bool isValid = key.VerifyData(bytes, signature, HashAlgorithmName.SHA256, RSASignaturePadding.Pkcs1);
            key.Dispose();
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
