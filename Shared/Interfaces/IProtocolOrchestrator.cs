using System.Net.Sockets;

namespace Shared.Interfaces
{
    public interface IProtocolOrchestrator
    {
        bool Process(TcpClient socket, string message);
    }
}