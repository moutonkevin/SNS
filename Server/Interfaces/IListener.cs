using System.Net.Sockets;

namespace Server.Interfaces
{
    internal interface IListener
    {
        Socket WaitForClientToConnect(TcpListener listener);
    }
}