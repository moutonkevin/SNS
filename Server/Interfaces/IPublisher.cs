using System.Net.Sockets;

namespace Server.Interfaces
{
    internal interface IPublisher
    {
        void PublishEventToAll();
        void PublishEvent(Socket client);
    }
}