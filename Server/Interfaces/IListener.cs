using System.Net.Sockets;

namespace Server.Interfaces
{
    public interface IListener
    {
        TcpClient WaitForClientToConnect(TcpListener listener);
    }
}
