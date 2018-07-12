using System.Net.Sockets;

namespace Server.Interfaces
{
    public interface IProtocolOrchestrator
    {
        bool Process(TcpClient socket, string message);
    }
}
