using Microsoft.IdentityModel.Tokens;
using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Certificates
{
    /// <summary>
    /// Uses CertificateRequest to create a X509Certificate
    /// ECD and RSA based certs are created one self signed, one with issuer.
    /// </summary>
    public class CreateCertificate
    {
        public static void Run()
        {
            X509Certificate2 certificate = CreateCert("CN=IDP6", RSA.Create(2048), HashAlgorithmName.SHA256, null);
            DisplayParameters(certificate, "IDP6");
            //DisplayParameters(CreateCert("CN=LocalSTS2", RSA.Create(2048), HashAlgorithmName.SHA256, null), "LocalSTS2");
            //X509Certificate2 ecdCert = CreateCert("CN=ecdsanistP256", ECDsa.Create(ECCurve.NamedCurves.nistP256), HashAlgorithmName.SHA256, null);
            //X509Certificate2 rsaCert = CreateCert("CN=rsa2048", RSA.Create(2048), HashAlgorithmName.SHA256, null);
            //X509Certificate2 ecdCertWithIssuer = CreateCert("CN=ecdsanistP256WithIssuer", ECDsa.Create(ECCurve.NamedCurves.nistP256), HashAlgorithmName.SHA256, ecdCert);
            //X509Certificate2 rsaCertWithIssuer = CreateCert("CN=rsa2048WithIssuer", RSA.Create(2048), HashAlgorithmName.SHA256, rsaCert);
        }

        private static void DisplayParameters(X509Certificate2 x509Certificate2, string name)
        {
            RSA privateKey = x509Certificate2.GetRSAPrivateKey();
            RSAParameters rsaParameters = privateKey.ExportParameters(true);
            Console.WriteLine();
            Console.WriteLine($"// Subject: {x509Certificate2.Subject}");
            Console.WriteLine($"// Issuer: {x509Certificate2.Issuer}");
            Console.WriteLine($"// Serial Number: {x509Certificate2.GetSerialNumberString()}");
            Console.WriteLine($"// Not Before: {x509Certificate2.NotBefore}");
            Console.WriteLine($"// Not After: {x509Certificate2.NotAfter}");
            Console.WriteLine($"// Thumbprint: {x509Certificate2.Thumbprint}");

            Console.WriteLine($"public static X509Certificate2 {name}X509CertificatePrivate = new X509Certificate2(Convert.FromBase64String(\"{Convert.ToBase64String(x509Certificate2.Export(X509ContentType.Pfx, name))}\"), \"{name}\", X509KeyStorageFlags.EphemeralKeySet);");
            Console.WriteLine();
            Console.WriteLine($"public static X509Certificate2 {name}X509CertificatePublic = new X509Certificate2(Convert.FromBase64String(\"{Convert.ToBase64String(x509Certificate2.Export(X509ContentType.Cert))}\"));");
            Console.WriteLine();
            Console.WriteLine($"public static string {name}Modulus = \"{Convert.ToBase64String(rsaParameters.Modulus)}\";");
            Console.WriteLine();
            Console.WriteLine($"public static string {name}Exponent = \"{Convert.ToBase64String(rsaParameters.Exponent)}\";");
            Console.WriteLine();
            Console.WriteLine($"public static string {name}X5t =  \"{Base64UrlEncoder.Encode(x509Certificate2.GetCertHash())}\";");
            Console.WriteLine();
            Console.WriteLine($"public static RSAParameters {name}RsaParametersPrivate = new RSAParameters{{");
            Console.WriteLine($"Exponent = Convert.FromBase64String(\"{Convert.ToBase64String(rsaParameters.Exponent)}\"),");
            Console.WriteLine($"Modulus = Convert.FromBase64String(\"{Convert.ToBase64String(rsaParameters.Modulus)}\"),");
            Console.WriteLine($"D = Convert.FromBase64String(\"{Convert.ToBase64String(rsaParameters.D)}\"),");
            Console.WriteLine($"DP = Convert.FromBase64String(\"{Convert.ToBase64String(rsaParameters.DP)}\"),");
            Console.WriteLine($"DQ  = Convert.FromBase64String(\"{Convert.ToBase64String(rsaParameters.DQ)}\"),");
            Console.WriteLine($"InverseQ = Convert.FromBase64String(\"{Convert.ToBase64String(rsaParameters.InverseQ)}\"),");
            Console.WriteLine($"P = Convert.FromBase64String(\"{Convert.ToBase64String(rsaParameters.P)}\"),");
            Console.WriteLine($"Q = Convert.FromBase64String(\"{Convert.ToBase64String(rsaParameters.Q)}\"),");
            Console.WriteLine("};");
            Console.WriteLine();
            Console.WriteLine($"public static RSAParameters {name}RsaParametersPublic = new RSAParameters{{");
            Console.WriteLine($"Exponent = Convert.FromBase64String(\"{Convert.ToBase64String(rsaParameters.Exponent)}\"),");
            Console.WriteLine($"Modulus = Convert.FromBase64String(\"{Convert.ToBase64String(rsaParameters.Modulus)}\"),");
            Console.WriteLine("};");

            try
            {
                X509Certificate2 certPrivate = new X509Certificate2(Convert.FromBase64String(Convert.ToBase64String(x509Certificate2.GetRawCertData())));
                X509Certificate2 certPrivate2 = new X509Certificate2(Convert.FromBase64String(Convert.ToBase64String(x509Certificate2.Export(X509ContentType.Pfx, $"{name}"))), $"{name}");
                X509Certificate2 certPublic = new X509Certificate2(Convert.FromBase64String(Convert.ToBase64String(x509Certificate2.Export(X509ContentType.Cert))));
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Exception: '{ex}'.");
            }
        }

        private static X509Certificate2 CreateCert(string x509DistinguishedName, object key, HashAlgorithmName hashAlgorithmName, X509Certificate2 issuer)
        {
            CertificateRequest certificateRequest = null;
            if (key is ECDsa)
                certificateRequest = new CertificateRequest(x509DistinguishedName, key as ECDsa, hashAlgorithmName);

            if (key is RSA)
                certificateRequest = new CertificateRequest(x509DistinguishedName, key as RSA, hashAlgorithmName, RSASignaturePadding.Pkcs1);

            if (issuer == null)
            {
                // this is needed to use the cert as an issuer.
                certificateRequest.CertificateExtensions.Add(new X509BasicConstraintsExtension(true, true, 0, true));
                return certificateRequest.CreateSelfSigned(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddYears(2));
            }
            else
            {
                var cert = certificateRequest.Create(issuer, DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddYears(2), Guid.NewGuid().ToByteArray());

                // add private key
                if (key is ECDsa)
                    return cert.CopyWithPrivateKey(key as ECDsa);

                if (key is RSA)
                    return cert.CopyWithPrivateKey(key as RSA);

                return cert;
            }
        }

        private static string PemEncodeSigningRequest(CertificateRequest request)
        {
            byte[] pkcs10 = request.CreateSigningRequest();
            StringBuilder builder = new StringBuilder();

            builder.AppendLine("-----BEGIN CERTIFICATE REQUEST-----");

            string base64 = Convert.ToBase64String(pkcs10);

            int offset = 0;
            const int LineLength = 64;

            while (offset < base64.Length)
            {
                int lineEnd = Math.Min(offset + LineLength, base64.Length);
                builder.AppendLine(base64.Substring(offset, lineEnd - offset));
                offset = lineEnd;
            }

            builder.AppendLine("-----END CERTIFICATE REQUEST-----");
            return builder.ToString();
        }
    }
}
