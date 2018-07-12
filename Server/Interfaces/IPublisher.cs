using System.Net.Sockets;

namespace Server.Interfaces
{
    internal interface IPublisher
    {
        void PublishEventToAll();
        void PublishEvent(TcpClient client);
    }
}