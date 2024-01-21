using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace HomemadeServer
{
    internal class Server
    {
        public void StartServer()
        {
            TcpListener server = null;

            try
            {
                // Set the IP address and port number for the server
                IPAddress ipAddress = IPAddress.Any;
                int port = Convert.ToInt32(Program.Configuration.GetElementsByTagName("Port")[0].InnerText);

                // Create a TCP listener and start listening for incoming connections
                server = new TcpListener(ipAddress, port);
                server.Start();

                Console.WriteLine($"Server is listening on {ipAddress}:{port}");

                // Start a background thread to handle client connections
                Task.Run(() => AcceptClients(server));
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error: {e.Message}");
            }
            finally
            {
                // Stop the server if an exception occurs
               // server?.Stop();
            }
        }

        private void AcceptClients(TcpListener server)
        {
            try
            {
                //server.Start();
                while (true)
                {
                    Console.WriteLine("Waiting for a connection...");

                    // Accept a pending connection request
                    TcpClient client = server.AcceptTcpClient();

                    // Once a connection is established, handle it in a separate thread
                    Console.WriteLine($"Connected to {((IPEndPoint)client.Client.RemoteEndPoint).Address}");

                    // Create a new thread to handle the client
                    ISessionHandler sessionHandler = new SessionHandler(client);
                    Task.Run(() => sessionHandler.HandleClient());

                }
            }
            catch (Exception e)
            {
                Console.WriteLine($"Error accepting clients: {e.Message}");
            }
        }


    }
}
