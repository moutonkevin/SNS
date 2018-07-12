using System.Net.Sockets;

namespace Server.Interfaces
{
    public interface IProtocolReader
    {
        void ReadAllBytes(TcpClient client);
    }
}
