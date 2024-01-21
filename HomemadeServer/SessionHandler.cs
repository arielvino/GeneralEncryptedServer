using HomemadeCloudUtils.Security.Cryptography;
using HomemadeCloudUtils.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HomemadeServer
{
    internal class SessionHandler:ISessionHandler
    {
        TcpClient Client { get; }
        bool isActive = false;
        bool login = false;
        string userName = string.Empty;
        byte[]? secretKey = null;

        public SessionHandler(TcpClient client) { 
            Client = client;
            isActive = true;
        }

        public void HandleClient()
        {
            EstablishSecureChannel();
            Console.WriteLine(BitConverter.ToString(secretKey));
        }

        private void EstablishSecureChannel()
        {
            //Listen for the random chalange:
            byte[] coming = Client.AcceptMessage();

            //decrypt the random challenge:
            string privateKey = Program.Configuration.GetElementsByTagName("ServerRsaKey")[0].InnerXml;
            byte[] randomChallange = RsaFactory.Decrypt(privateKey, coming);

            //send the result:
            Client.Send(randomChallange);

            //get the encrypted secret key for this session:
            byte[] encryptedSecretKey = Client.AcceptMessage();

            //encrypt the secret key:
            secretKey = RsaFactory.Decrypt(privateKey, encryptedSecretKey);
        }
    }
}
