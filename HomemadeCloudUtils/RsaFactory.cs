using System.Security.Cryptography;

namespace HomemadeCloudUtils.Security.Cryptography
{
    public static class RsaFactory
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="keySize"></param>
        /// <returns>Pair of xml strings. The first one is the public key, and the second is the private key.</returns>
        public static Tuple<string, string> GenerateKeyPair(int keySize = 4096)
        {
            RSACryptoServiceProvider rsa = new RSACryptoServiceProvider(keySize);

            string publicKey = rsa.ToXmlString(false);
            string privateKey = rsa.ToXmlString(true);

            return Tuple.Create(publicKey, privateKey);
        }

        public static byte[] Encrypt(string publicKey, byte[] content)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(publicKey);
                byte[] encryptedBytes = rsa.Encrypt(content, false);
                return encryptedBytes;
            }
        }

        public static byte[] Decrypt(string privateKey, byte[] encryptedMessage)
        {
            using (RSACryptoServiceProvider rsa = new RSACryptoServiceProvider())
            {
                rsa.FromXmlString(privateKey);
                byte[] decryptedBytes = rsa.Decrypt(encryptedMessage, false);
                return decryptedBytes;
            }
        }
    }
}
