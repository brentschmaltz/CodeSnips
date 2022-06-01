using System.Security.Cryptography;

namespace CodeSnips.Crypto
{
    public class KeyedHash
    {
        public static void Run()
        {
            int size = 1024;
            Aes aes = Aes.Create();
            aes.KeySize = 256;
            aes.GenerateKey();

            KeyedHashAlgorithm keyedHashAlgorithm = new HMACSHA256(aes.Key);

            byte[] bytes = new byte[size];
            
            for (int i = 0; i < size; i++)
                bytes[i] = (byte)i;

            byte[] hash1 = keyedHashAlgorithm.ComputeHash(bytes);
            byte[] hash2 = keyedHashAlgorithm.ComputeHash(bytes, 0, size);
        }
    }
}
