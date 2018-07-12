using System.Net;

namespace Shared.Interfaces
{
    public interface IEndpointResolver
    {
        IPAddress GetIp(string hostName);
        IPEndPoint GetEndpoint(string hostName, int port);
        IPEndPoint GetLocalEndpoint();
    }
}
