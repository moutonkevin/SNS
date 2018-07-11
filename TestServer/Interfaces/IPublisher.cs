using System.Net.Sockets;

namespace TestServer.Interfaces
{
    public interface IPublisher
    {
        void PublishEventToAll();
        void PublishEvent(TcpClient client);
    }
}
