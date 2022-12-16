using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CodeSnips.BasicCLR
{
    public class RsaEncryptCreateDelegate
    {
        delegate byte[] EncryptDelegate(byte[] bytes);
        private static bool _useRSAOeapPadding;
        private static readonly Type _rsaEncryptionPaddingType = typeof(object).Assembly.GetType("System.Security.Cryptography.RSAEncryptionPadding", false)!;
        private static volatile Func<RSA, byte[], byte[]> _rsaEncrypt45Method;
        private static volatile Func<RSA, byte[], byte[]> _rsaDecrypt45Method;

        public static void Run()
        {
            string str = "System.Security.Cryptography.RSAEncryptionPadding";
            RunOnce(RSACng.Create(2048), str, false);
            RunOnce(RSACng.Create(2048), str, true);
        }

        private static void RunOnce(RSA rsa, string strIn, bool useRSAOeap)
        {
            _useRSAOeapPadding = useRSAOeap;
            RSA = rsa;
            _rsaEncrypt45Method = null;
            _rsaDecrypt45Method = null;
            byte[] bytes = UTF8Encoding.UTF8.GetBytes(strIn);
            byte[] encryptedBytes = EncryptNet45(bytes);
            RSA = RSACng.Create(2048);
            byte[] decryptedBytes = DecryptNet45(encryptedBytes);

            string clearText = UTF8Encoding.UTF8.GetString(decryptedBytes);
            Console.WriteLine($"useRSAOeap: '{useRSAOeap}'.");
            if (clearText.Equals(strIn))
                Console.WriteLine($"strings are the same: '{strIn}', '{clearText}'.");
            else
                Console.WriteLine($"strings are NOT the same: '{strIn}', '{clearText}'.");
        }

        private static RSA RSA { get; set; }

        private static byte[] EncryptNet45(byte[] bytes)
        {
            if (_rsaEncrypt45Method == null)
            {
                // Encrypt(byte[] data, RSAEncryptionPadding padding)
                Type[] encryptionTypes = { typeof(byte[]), _rsaEncryptionPaddingType };
                MethodInfo encryptMethod = typeof(RSA).GetMethod(
                    "Encrypt",
                    BindingFlags.Public | BindingFlags.Instance,
                    null,
                    encryptionTypes,
                    null);

                Type delegateType = typeof(Func<,,,>).MakeGenericType(
                            typeof(RSA),
                            typeof(byte[]),
                            _rsaEncryptionPaddingType,
                            typeof(byte[]));

                PropertyInfo prop;
                if (_useRSAOeapPadding)
                    prop = _rsaEncryptionPaddingType.GetProperty("OaepSHA1", BindingFlags.Static | BindingFlags.Public);
                else
                    prop = _rsaEncryptionPaddingType.GetProperty("Pkcs1", BindingFlags.Static | BindingFlags.Public);

                Delegate openDelegate = Delegate.CreateDelegate(delegateType, encryptMethod);
                _rsaEncrypt45Method = (rsaArg, bytesArg) =>
                {
                    object[] args =
                    {
                        rsaArg,
                        bytesArg,
                        prop.GetValue(null)
                    };

                    return (byte[])openDelegate.DynamicInvoke(args);
                };
            }

            return _rsaEncrypt45Method(RSA!, bytes);
        }

        private static byte[] DecryptNet45(byte[] bytes)
        {
            if (_rsaDecrypt45Method == null)
            {
                // Decrypt(byte[] data, RSAEncryptionPadding padding)
                Type[] encryptionTypes = { typeof(byte[]), _rsaEncryptionPaddingType };
                MethodInfo encryptMethod = typeof(RSA).GetMethod(
                    "Decrypt",
                    BindingFlags.Public | BindingFlags.Instance,
                    null,
                    encryptionTypes,
                    null)!;

                Type delegateType = typeof(Func<,,,>).MakeGenericType(
                            typeof(RSA),
                            typeof(byte[]),
                            _rsaEncryptionPaddingType,
                            typeof(byte[]));

                PropertyInfo prop;
                if (_useRSAOeapPadding)
                    prop = _rsaEncryptionPaddingType.GetProperty("OaepSHA1", BindingFlags.Static | BindingFlags.Public)!;
                else
                    prop = _rsaEncryptionPaddingType.GetProperty("Pkcs1", BindingFlags.Static | BindingFlags.Public)!;
                _rsaDecrypt45Method = (rsaArg, bytesArg) =>
                {
                    object[] args = { rsaArg, bytesArg, prop.GetValue(null) };
                    Delegate openDelegate = Delegate.CreateDelegate(delegateType, encryptMethod)!;
                    return (byte[])openDelegate.DynamicInvoke(args);
                };
            }

            return _rsaDecrypt45Method(RSA!, bytes);
        }
    }
}
