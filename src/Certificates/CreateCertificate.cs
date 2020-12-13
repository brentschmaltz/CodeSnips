using System;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace CodeSnips.Certificates
{
    /// <summary>
    /// Uses CertificateRequest to create a X509Certificate
    /// ECD and RSA based certs are created one self signed, one with issuer.
    /// </summary>
    public class CreateCertificate
    {
        public static void Run()
        {

            X509Certificate2 ecdCert = CreateCert("CN=ecdsanistP256", ECDsa.Create(ECCurve.NamedCurves.nistP256), HashAlgorithmName.SHA256, null);
            X509Certificate2 rsaCert = CreateCert("CN=rsa2048", RSA.Create(2048), HashAlgorithmName.SHA256, null);
            X509Certificate2 ecdCertWithIssuer = CreateCert("CN=ecdsanistP256WithIssuer", ECDsa.Create(ECCurve.NamedCurves.nistP256), HashAlgorithmName.SHA256, ecdCert);
            X509Certificate2 rsaCertWithIssuer = CreateCert("CN=rsa2048WithIssuer", RSA.Create(2048), HashAlgorithmName.SHA256, rsaCert);
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
                return certificateRequest.CreateSelfSigned(DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddDays(20));
            }
            else
            {
                var cert = certificateRequest.Create(issuer, DateTimeOffset.UtcNow, DateTimeOffset.UtcNow.AddDays(10), Guid.NewGuid().ToByteArray());

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
