using System;
using System.Net.Sockets;
using Client.Interfaces;
using Shared.Interfaces;

namespace Client.Services
{
    public class ListenerService : IListener
    {
        private string _hostName;
        private int _port;
        private string _topic;
        private Type _handler;
        private Socket _serverSocket;

        private readonly IProtocolSender _protocolSender;
        private readonly IProtocolReader _protocolReader;

        public ListenerService(IProtocolSender protocolSender, IProtocolReader protocolReader)
        {
            _protocolSender = protocolSender;
            _protocolReader = protocolReader;
        }

        public IListener WithHostName(string hostName)
        {
            _hostName = hostName;

            return this;
        }

        public IListener WithPort(int port)
        {
            _port = port;

            return this;
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
            var socket = _protocolSender.Connect(_hostName, _port);
            if (socket == null)
            {
                //TODO
                throw new InvalidOperationException("Could not connect to the server");
            }

            var isSendTopicSuccessful = _protocolSender.Send(socket, $"Type=subscribe Body={_topic}<EOF>");
            if (!isSendTopicSuccessful)
            {
                //TODO
                throw new InvalidOperationException("Could not subscribe to the topic");
            }

            _serverSocket = socket;

            return this;
        }

        public void StartListening()
        {
            while (true)
            {
                var message = _protocolReader.Receive(_serverSocket);

                //todo
                //instantiate class
            }
        }
    }
}
