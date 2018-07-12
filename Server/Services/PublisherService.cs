using System;
using System.Net.Sockets;
using System.Text;
using Server.Interfaces;
using Shared.Interfaces;

namespace Server.Services
{
    public class PublisherService : IPublisher
    {
        private readonly IClientManager _clientManager;

        public PublisherService(IClientManager clientManager)
        {
            _clientManager = clientManager;
        }

        public void PublishEvent(TcpClient client)
        {
            try
            {
                var networkStream = client.GetStream();

                var sendBytes = Encoding.ASCII.GetBytes("Server to client <EOF>");
                networkStream.Write(sendBytes, 0, sendBytes.Length);
                networkStream.Flush();

                Console.WriteLine($" >> Message sent to client {client.GetHashCode()}");
            }
            catch (Exception ex)
            {
                Console.WriteLine($" >> Could not sent to client {client.GetHashCode()}");

                _clientManager.RemoveClient(client);
            }
        }

        public void PublishEventToAll()
        {
            foreach (var tcpClient in _clientManager.GetAll()) PublishEvent(tcpClient);
        }
    }
}