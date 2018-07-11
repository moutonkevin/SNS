using System.Net.Sockets;

namespace TestServer.Interfaces
{
    public interface IListener
    {
        TcpClient WaitForClientToConnect(TcpListener listener);
    }
}
