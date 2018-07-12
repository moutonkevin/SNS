using System.Net.Sockets;

namespace Shared.Interfaces
{
    public interface IProtocolOrchestrator
    {
        bool Process(Socket socket, string message);
    }
}