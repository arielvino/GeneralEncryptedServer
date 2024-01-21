using ConsoleClient;
using HomemadeCloudUtils.Security.Cryptography;
using HomemadeCloudUtils.Network;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace HomemadeConsoleClient___Experimental
{
    internal class Client
    {

        private TcpClient? TcpClient { get; set; }
        private byte[]? secretKey = null;
        public bool IsRunning
        {
            get; private set;
        } = false;

        public void StartClient()
        {
            try
            {
                // Set the server's IP address and port
                string serverIpAddress = Program.Configuration.GetElementsByTagName("IpAddress")[0].InnerText;
                int serverPort = Convert.ToInt32(Program.Configuration.GetElementsByTagName("Port")[0].InnerText);

                // Create a TcpClient to connect to the server
                TcpClient = new TcpClient(serverIpAddress, serverPort);
                IsRunning = true;
                Console.WriteLine("Connected");

                EstablishSecureChannel();
                Console.WriteLine(BitConverter.ToString(secretKey));

            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
        }

        private void EstablishSecureChannel()
        {
            //create random challenge:
            byte[] randomChallenge = new byte[32];
            RandomNumberGenerator.Create().GetBytes(randomChallenge);

            //encrypt it with the server's public key:
            string serverPublicKey = Program.Configuration.GetElementsByTagName("ServerPublicKey")[0].InnerXml;
            byte[] encryptedRandomChallenge = RsaFactory.Encrypt(serverPublicKey, randomChallenge);

            //send the encrypted challenge to the server:
            TcpClient.Send(encryptedRandomChallenge);

            //get the result:
            byte[] coming = TcpClient.AcceptMessage();

            if (coming.SequenceEqual(randomChallenge))
            {
                Console.WriteLine("Server identity proved");
            }
            else//disconnect
            {
                Console.WriteLine("Server identity seems to be wrong");
                StopClient();
                Console.WriteLine("Disconnected");
                Console.WriteLine("Type \"connect\" to try again");
            }

            //generate a secret key:
            secretKey = AesFactory.GenerateSecretKey();

            //encrypt the secret key with the server's public key:
            byte[] encryptedSecretKey = RsaFactory.Encrypt(serverPublicKey, secretKey);

            //send the encrypted key to the server:
            TcpClient.Send(encryptedSecretKey);
        }

        public void StopClient()
        {
            TcpClient?.Close();
            IsRunning = false;
        }
    }
}