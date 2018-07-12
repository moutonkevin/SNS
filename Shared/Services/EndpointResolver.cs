using System.Net;
using Shared.Interfaces;

namespace Shared.Services
{
    public class EndpointResolver : IEndpointResolver
    {
        private IPAddress GetIpShared(string hostName)
        {
            var ipHostInfo = Dns.GetHostEntry(hostName);
            var ipAddress = ipHostInfo.AddressList[0];

            return ipAddress;
        }

        public IPAddress GetIp(string hostName)
        {
            return GetIpShared(hostName);
        }

        public IPEndPoint GetEndpoint(string address, int port)
        {
            var ipAddress = GetIpShared(address);
            var endpoint = new IPEndPoint(ipAddress, port);

            return endpoint;
        }

        public IPEndPoint GetLocalEndpoint()
        {
            var server = Dns.GetHostName();
            const int port = 11000;

            var ipAddress = GetIpShared(server);
            var localEndPoint = new IPEndPoint(ipAddress, port);

            return localEndPoint;
        }
    }
}
