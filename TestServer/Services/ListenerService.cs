using System;
using System.Net.Sockets;
using TestServer.Interfaces;

namespace TestServer.Services
{
    public class ListenerService : IListener
    {
        private readonly IClientManager _clientManager;

        public ListenerService(IClientManager clientManager)
        {
            _clientManager = clientManager;
        }

        public TcpClient WaitForClientToConnect(TcpListener listener)
        {
            var clientSocket = listener.AcceptTcpClient();

            _clientManager.AddClient(clientSocket);

            Console.WriteLine(" >> Client connection received");

            return clientSocket;
        }
    }
}
