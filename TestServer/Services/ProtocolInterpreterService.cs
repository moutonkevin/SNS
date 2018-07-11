using System;
using System.Collections.Generic;
using System.Net.Sockets;
using TestServer.Interfaces;

namespace TestServer.Services
{
    public class ProtocolInterpreterService : IProtocolInterpreter
    {
        private readonly Dictionary<MessageType, Func<TcpClient, string, bool>> _handler;
        private readonly IClientManager _clientManager;

        public ProtocolInterpreterService(IClientManager clientManager)
        {
            _clientManager = clientManager;
            _handler = new Dictionary<MessageType, Func<TcpClient, string, bool>>
                {
                    {MessageType.Subscribe, Subscribe}
                };
        }

        public void Interpret(TcpClient socket, MessageType messageType, string body)
        {
            if (_handler.ContainsKey(messageType))
            {
                _handler[messageType].Invoke(socket, body);
            }
        }

        private bool Subscribe(TcpClient socket, string body)
        {
            _clientManager.AddClient(socket);
            return true;
        }
    }
}
