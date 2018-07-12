using System.Net.Sockets;

namespace Server.Interfaces
{
    internal interface IListener
    {
        TcpClient WaitForClientToConnect(TcpListener listener);
    }
}