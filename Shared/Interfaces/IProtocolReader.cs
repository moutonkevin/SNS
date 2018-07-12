using System.Net.Sockets;

namespace Shared.Interfaces
{
    public interface IProtocolReader
    {
        string ReadAll(Socket socket);
    }
}