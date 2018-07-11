using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using TestServer.Interfaces;

namespace TestServer.Services
{
    public class ServerService : IServer
    {
        private readonly IListener _listener;
        private readonly IPublisher _publisher;
        private readonly IProtocolReader _protocolReader;

        public ServerService()
        {
            IClientManager clientManager = new ClientManagerService();
            _listener = new ListenerService(clientManager);
            _publisher = new PublisherService(clientManager);
            IProtocolInterpreter protocolInterpreter = new ProtocolInterpreterService(clientManager);
            IProtocolParser protocolParser = new ProtocolParserService();
            IProtocolOrchestrator protocolOrchestrator = new ProtocolOrchestratorService(protocolParser, protocolInterpreter);
            _protocolReader = new ProtocolReaderService(protocolOrchestrator);
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

                Task.Run(() => { _publisher.PublishEventToAll(); });
            }
        }
    }
}
