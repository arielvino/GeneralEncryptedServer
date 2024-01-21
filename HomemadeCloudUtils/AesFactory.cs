using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HomemadeCloudUtils.Security.Cryptography
{
    public static class AesFactory
    {
        public static byte[] GenerateSecretKey(int keySize = 256)
        {
            using (Aes aesAlg = Aes.Create())
            {
                aesAlg.KeySize = keySize;
                aesAlg.GenerateKey();
                return aesAlg.Key;
            }
        }

        public static byte[] Encrypt(byte[] content, byte[] key)
        {
            throw new NotImplementedException();
        }

        public static byte[] Decrypt(byte[] cipher, byte[] key)
        {
            throw new NotImplementedException();
        }
    }
}
