using System.Net.Sockets;

namespace TestServer.Interfaces
{
    public interface IProtocolOrchestrator
    {
        bool Process(TcpClient socket, string message);
    }
}
