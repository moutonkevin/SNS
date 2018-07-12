using System.Net.Sockets;

namespace Server.Interfaces
{
    public interface IProtocolInterpreter
    {
        void Interpret(TcpClient socket, MessageType messageType, string body);
    }
}
