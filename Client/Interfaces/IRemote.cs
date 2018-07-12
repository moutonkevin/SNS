using System.Net;
using System.Net.Sockets;

namespace Client.Interfaces
{
    public interface IRemote
    {
        Socket CreateSocket(IPAddress ipAddress);
        bool Connect(Socket serverSocket, IPEndPoint endpoint);
        void Receive(Socket serverSocket);
    }
}
