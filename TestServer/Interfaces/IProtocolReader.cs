using System;
using System.Net.Sockets;

namespace TestServer.Interfaces
{
    public interface IProtocolReader
    {
        void ReadAllBytes(TcpClient client);
    }
}
