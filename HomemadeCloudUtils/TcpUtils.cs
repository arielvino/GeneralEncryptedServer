using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace HomemadeCloudUtils.Network
{
    public static class TcpUtils
    {
        public static byte[] AcceptMessage(this TcpClient client)
        {
            int size_of_size_field = 4;
            //get the size of the incoming packet:
            while (!(client.Available > size_of_size_field)) { }//wait
            byte[] bytes = new byte[size_of_size_field];
            client.GetStream().Read(bytes, 0, size_of_size_field);
            int size = BitConverter.ToInt32(bytes, 0);

            //get the packet:
            while (!(client.Available >= size)) { }//wait
            byte[] content = new byte[size];
            client.GetStream().Read(content, 0, size);

            return content;

        }

        public static void Send(this TcpClient client, byte[] content)
        {
            if (content != null)
            {
                byte[] size = BitConverter.GetBytes(content.Length);
                byte[] packet = new byte[size.Length + content.Length];
                size.CopyTo(packet, 0);
                content.CopyTo(packet, size.Length);
                client.GetStream().Write(packet, 0, packet.Length);
            }
        }



    }
}