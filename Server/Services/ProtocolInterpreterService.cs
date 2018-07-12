using System;
using System.Collections.Generic;
using System.Net.Sockets;
using Server.Interfaces;

namespace Server.Services
{
    public class ProtocolInterpreterService : IProtocolInterpreter
    {
        private readonly Dictionary<MessageType, Func<TcpClient, string, bool>> _handler;
        private readonly IClientManager _clientManager;
        private readonly ITopicManager _topicManager;

        public ProtocolInterpreterService(IClientManager clientManager, ITopicManager topicManager)
        {
            _clientManager = clientManager;
            _topicManager = topicManager;
            _handler = new Dictionary<MessageType, Func<TcpClient, string, bool>>
                {
                    {MessageType.Subscribe, Subscribe},
                    {MessageType.Unsubscribe , Unsubscribe}
                };
        }

        public void Interpret(TcpClient socket, MessageType messageType, string body)
        {
            if (_handler.ContainsKey(messageType))
            {
                _handler[messageType].Invoke(socket, body);
            }
        }

        private bool Subscribe(TcpClient client, string topicName)
        {
            _clientManager.AddClient(client);
            _topicManager.AddClientToTopic(client, topicName);

            return true;
        }

        private bool Unsubscribe(TcpClient client, string topicName)
        {
            _clientManager.RemoveClient(client);
            _topicManager.RemoveClientFromTopic(client, topicName);

            return true;
        }
    }
}
