using System;
using System.Net.Sockets;
using Client.Interfaces;
using Shared.Interfaces;

namespace Client.Services
{
    public class ListenerService : IListener
    {
        private string _topic;
        private Type _handler;
        private Socket _serverSocket;

        private readonly IOperation _server;
        private readonly IProtocolSender _protocolSender;
        private readonly IProtocolReader _protocolReader;
        private readonly IEndpointResolver _endpointResolver;

        public ListenerService(IEndpointResolver endpointResolver, IOperation server, IProtocolSender protocolSender, IProtocolReader protocolReader)
        {
            _endpointResolver = endpointResolver;
            _server = server;
            _protocolSender = protocolSender;
            _protocolReader = protocolReader;
        }

        public IListener Listen(string topicName)
        {
            _topic = topicName;

            return this;
        }

        public IListener Handle<THandler>() where THandler : class, IHandling
        {
            _handler = typeof(THandler);

            return this;
        }

        public IListener Verify()
        {
            const string hostName = "AU-SYD-IT-018";
            const int port = 11000;

            var serverEndpoint = _endpointResolver.GetEndpoint(hostName, port);
            var ipAddress = _endpointResolver.GetIp(hostName);
            var serverSocket = _server.CreateSocket(ipAddress);

            var isConnectionSuccessful = _protocolSender.Connect(serverSocket, serverEndpoint);
            if (!isConnectionSuccessful)
            {
                //TODO
                throw new InvalidOperationException("Could not connect to the server");
            }

            var isSendTopicSuccessful = _protocolSender.Send(serverSocket, "Type=subscribe Body=this-is-the-queue-name<EOF>");
            if (!isSendTopicSuccessful)
            {
                //TODO
                throw new InvalidOperationException("Could not subscribe to the topic");
            }

            _serverSocket = serverSocket;

            return this;
        }

        public void StartListening()
        {
            while (true)
            {
                var message = _protocolReader.Receive(_serverSocket);
                
            }
        }
    }
}
