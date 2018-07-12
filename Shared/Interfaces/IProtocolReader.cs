using System.Net.Sockets;

namespace Shared.Interfaces
{
    public interface IProtocolReader
    {
        string Receive(Socket socket);
    }
}