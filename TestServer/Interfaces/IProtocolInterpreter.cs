using System.Net.Sockets;

namespace TestServer.Interfaces
{
    public interface IProtocolInterpreter
    {
        void Interpret(TcpClient socket, MessageType messageType, string body);
    }
}
