using System.Net.Sockets;

namespace Shared.Interfaces
{
    public interface IProtocolInterpreter
    {
        void Interpret(Socket socket, MessageType messageType, string body);
    }
}