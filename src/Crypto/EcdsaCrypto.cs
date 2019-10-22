﻿using System;
using System.Security.Cryptography;
using System.Text;
using CodeSnips.Utility;

namespace CodeSnips.Crypto
{
    public class EcdsaCrypto
    {
        public static void Run()
        {
            var ecdsa = ECDsa.Create();
            var hashAlgorithmName = HashAlgorithmName.SHA256;
            var bytes = Encoding.UTF8.GetBytes("Here are some bytes to sign.");
            var hashAlgoritm = SHA256.Create();
            var hashBytes = hashAlgoritm.ComputeHash(bytes);

            var ecdsaSignature = ecdsa.SignData(bytes, hashAlgorithmName);
            var ecdsaSignature2 = ecdsa.SignData(bytes, hashAlgorithmName);
            var ecdsaSignedHashBytes = ecdsa.SignHash(hashBytes);

            var verifyBytes = ecdsa.VerifyHash(hashBytes, ecdsaSignedHashBytes);
            var verifySignatureHashBytes = ecdsa.VerifyHash(hashBytes, ecdsaSignature);
            var verifySignature = ecdsa.VerifyData(bytes, ecdsaSignature, hashAlgorithmName);

            var rsa = new RSACryptoServiceProvider();
            var rsaSignHashBytes = rsa.SignHash(hashBytes, "SHA256");
            var rsaSignature = rsa.SignData(bytes, hashAlgoritm);

            Console.WriteLine($"ECDsa default SignatureAlgorithm: '{ecdsa.SignatureAlgorithm}'.");
            Console.WriteLine($"ecdsa.VerifyHash(hashBytes, ecdsaSignedHashBytes): '{ecdsa.VerifyHash(hashBytes, ecdsaSignedHashBytes)}'.");
            Console.WriteLine($"ecdsa.VerifyHash(hashBytes, ecdsaSignature): '{ecdsa.VerifyHash(hashBytes, ecdsaSignature)}'.");
            Console.WriteLine($"ecdsa.VerifyHash(hashBytes, ecdsaSignature2): '{ecdsa.VerifyHash(hashBytes, ecdsaSignature2)}'.");
            Console.WriteLine($"ecdsa.VerifyData(bytes, ecdsaSignature, hashAlgorithmName): '{ecdsa.VerifyData(bytes, ecdsaSignature, hashAlgorithmName)}'.");
            Console.WriteLine($"ecdsa.VerifyData(bytes, ecdsaSignature2, hashAlgorithmName): '{ecdsa.VerifyData(bytes, ecdsaSignature2, hashAlgorithmName)}'.");
            Console.WriteLine($"ecdsa.VerifyData(bytes, ecdsaSignedHashBytes, hashAlgorithmName): '{ecdsa.VerifyData(bytes, ecdsaSignedHashBytes, hashAlgorithmName)}'.");
            Console.WriteLine($"rsa.VerifyData(bytes, 'SHA256', rsaSignature): '{rsa.VerifyData(bytes, "SHA256", rsaSignature)}'.");
            Console.WriteLine($"rsa.VerifyHash(hashBytes, 'SHA256', rsaSignature): '{rsa.VerifyHash(hashBytes, "SHA256", rsaSignature)}'.");
            Console.WriteLine($"Are bytes equal ecdsaSignature, ecdsaSignature2: '{Utilities.AreEqual(ecdsaSignature, ecdsaSignature2)}'.");
            Console.WriteLine($"Are bytes equal rsaSignHashBytes, rsaSignature: '{Utilities.AreEqual(rsaSignHashBytes, rsaSignature)}'.");
        }
    }
}