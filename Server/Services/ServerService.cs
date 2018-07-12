using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using Server.Interfaces;
using Shared.Interfaces;

namespace Server.Services
{
    internal class ServerService : IServer
    {
        private readonly IListener _listener;
        private readonly IProtocolReader _protocolReader;
        private readonly IPublisher _publisher;
        private readonly IProtocolOrchestrator _protocolOrchestrator;

        public ServerService(IListener listener, IPublisher publisher, IProtocolReader protocolReader, IProtocolOrchestrator protocolOrchestrator)
        {
            _listener = listener;
            _publisher = publisher;
            _protocolReader = protocolReader;
            _protocolOrchestrator = protocolOrchestrator;
        }

        public void Start(IPEndPoint endpoint)
        {
            var serverSocket = new TcpListener(endpoint);
            serverSocket.Start();

            Console.WriteLine(" >> " + "Server Started");

            while (true)
            {
                var client = _listener.WaitForClientToConnect(serverSocket);

                var message = _protocolReader.Receive(client.Client);
                _protocolOrchestrator.Process(client, message);

                Task.Run(() => { _publisher.PublishEventToAll(); });
            }
        }
    }
}