using System;
using System.Net;
using System.Net.Sockets;
using Server.Interfaces;

namespace Server.Services
{
    public class ServerService : IServer
    {
        private readonly IListener _listener;
        private readonly IPublisher _publisher;
        private readonly IProtocolReader _protocolReader;

        public ServerService(IListener listener, IPublisher publisher, IProtocolReader protocolReader)
        {
            _listener = listener;
            _publisher = publisher;
            _protocolReader = protocolReader;
        }

        public IPEndPoint GetLocalEndpoint()
        {
            var server = Dns.GetHostName();
            const int port = 11000;

            var ipHostInfo = Dns.GetHostEntry(server);
            var ipAddress = ipHostInfo.AddressList[0];
            var localEndPoint = new IPEndPoint(ipAddress, port);

            return localEndPoint;
        }

        public IPEndPoint GetEndpoint(string address, int port)
        {
            var ipHostInfo = Dns.GetHostEntry(address);
            var ipAddress = ipHostInfo.AddressList[0];
            var localEndPoint = new IPEndPoint(ipAddress, port);

            return localEndPoint;
        }

        public void Start(IPEndPoint endpoint)
        {
            var serverSocket = new TcpListener(endpoint);
            serverSocket.Start();

            Console.WriteLine(" >> " + "Server Started");

            while (true)
            {
                var client = _listener.WaitForClientToConnect(serverSocket);

                _protocolReader.ReadAllBytes(client);

                //Task.Run(() => { _publisher.PublishEventToAll(); });
            }
        }
    }
}
