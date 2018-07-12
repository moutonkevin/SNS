using System.Net.Sockets;

namespace Server.Interfaces
{
    public interface IPublisher
    {
        void PublishEventToAll();
        void PublishEvent(TcpClient client);
    }
}
