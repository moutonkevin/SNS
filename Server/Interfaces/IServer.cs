using System.Net;

namespace Server.Interfaces
{
    public interface IServer
    {
        IPEndPoint GetLocalEndpoint();
        IPEndPoint GetEndpoint(string address, int port);
        void Start(IPEndPoint endpoint);
    }
}