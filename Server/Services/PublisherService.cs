using System.Net.Sockets;
using Server.Interfaces;
using Shared.Interfaces;

namespace Server.Services
{
    public class PublisherService : IPublisher
    {
        private readonly IClientManager _clientManager;
        private readonly IProtocolSender _protocolSender;

        public PublisherService(IClientManager clientManager, IProtocolSender protocolSender)
        {
            _clientManager = clientManager;
            _protocolSender = protocolSender;
        }

        public void PublishEvent(Socket client)
        {
            _protocolSender.Send(client, "Server to client <EOF>");
        }

        public void PublishEventToAll()
        {
            foreach (var tcpClient in _clientManager.GetAll()) PublishEvent(tcpClient);
        }
    }
}