using System.Net;
using System.Net.Sockets;

namespace Client.Interfaces
{
    public interface IOperation
    {
        Socket CreateSocket(IPAddress ipAddress);
    }
}
