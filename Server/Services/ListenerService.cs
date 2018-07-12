using System;
using System.Net.Sockets;
using Server.Interfaces;
using Shared.Interfaces;

namespace Server.Services
{
    public class ListenerService : IListener
    {
        private readonly IClientManager _clientManager;

        public ListenerService(IClientManager clientManager)
        {
            _clientManager = clientManager;
        }

        public Socket WaitForClientToConnect(TcpListener listener)
        {
            var socket = listener.AcceptSocket();

            _clientManager.AddClient(socket);

            Console.WriteLine(" >> Client connection received");

            return socket;
        }
    }
}