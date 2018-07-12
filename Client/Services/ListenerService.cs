using System;
using System.Net.Sockets;
using Client.Interfaces;
using Shared.Interfaces;
using Shared.Models;

namespace Client.Services
{
    public class ListenerService : IListener
    {
        private string _topic;
        private Type _handler;
        private Socket _serverSocket;

        private readonly IRemote _server;

        private readonly IEndpointResolver _endpointResolver;

        public ListenerService(IEndpointResolver endpointResolver, IRemote server)
        {
            _endpointResolver = endpointResolver;
            _server = server;
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

            var isConnectionSuccessful = _server.Connect(serverSocket, serverEndpoint);

            if (!isConnectionSuccessful)
            {
                //TODO
                throw new InvalidOperationException("Could not connect to the server");
            }

            _serverSocket = serverSocket;

            return this;
        }

        public void StartListening()
        {


            while (true)
            {
                _server.Receive(_serverSocket);

                
            }
        }
    }
}
