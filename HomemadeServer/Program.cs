using System.Xml;

namespace HomemadeServer
{
    public static class Program
    {
        private static Server MainServer { get; } = new Server();
        public static XmlDocument Configuration { get; } = new XmlDocument();

        public static void Main(string[] args)
        {
            //setup:
            Configuration.Load("AppConfig.xml");

            //great:
            Console.WriteLine("Welcome to the server!");
            Console.WriteLine("Type \"start\" to run the server...");


            //main loop:
            while (true)
            {
                String input = Console.ReadLine();

                //start the server:
                if(input.ToUpper() == "START")
                {
                    MainServer.StartServer();
                }

                //close the app:
                if (input.ToUpper() == "CLOSE")
                {

                    break;
                }
            }

        }

    }
}

