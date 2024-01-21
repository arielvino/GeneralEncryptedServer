using HomemadeConsoleClient___Experimental;
using System.Net.Sockets;
using System.Text;
using System.Xml;

namespace ConsoleClient
{
    static class Program
    {       
        public static XmlDocument Configuration { get; private set; } = new XmlDocument();
        static Client Client { get; } = new Client();
        public static void Main(string[] args)
        {
            //setup config:
            Configuration.Load("AppConfig.xml");

            //great:
            Console.WriteLine("Welcome to the client!");
            Console.WriteLine("Type \"connect\" to connect to the server...");

            //start main loop:
            while (true)
            {
                string input = Console.ReadLine();
                if (input != null)
                {
                    ParseCommand(input);
                }
            }
        }

        static void ParseCommand(string command)
        {
            string[] args = command.Split(' ');

            string commandName = args[0].ToUpper();

            //connect to server:
            if (commandName == "CONNECT")
            {
                Client.StartClient();
            }

            if (commandName == "CLOSE")
            {
                Client.StopClient();
            }

            if (commandName == "SEND")
            {
                if (Client.IsRunning)
                {
                    if (args.Length >= 2)
                    {
                        if (!String.IsNullOrWhiteSpace(command.Substring(command.IndexOf(' '))))
                        {
                            string message = command.Substring(command.IndexOf(' ') + 1);
                        }
                    }
                }
            }
        }
    }
}