using System.Net;

namespace TestServer.Interfaces
{
    public interface IServer
    {
        IPEndPoint GetLocalEndpoint();
        IPEndPoint GetEndpoint(string address, int port);
        void Start(IPEndPoint endpoint);
    }
}