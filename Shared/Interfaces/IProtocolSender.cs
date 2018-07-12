using System.Net;
using System.Net.Sockets;

namespace Shared.Interfaces
{
    public interface IProtocolSender
    {
        bool Connect(Socket serverSocket, IPEndPoint endpoint);
        bool Send(Socket serverSocket, string message);
    }
}
