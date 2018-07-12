using System.Net;
using System.Net.Sockets;

namespace Shared.Interfaces
{
    public interface IProtocolSender
    {
        Socket Connect(string hostName, int port);
        bool Send(Socket serverSocket, string message);
    }
}
