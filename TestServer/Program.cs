using System;
using System.Net;
using System.Net.Sockets;
using System.Threading.Tasks;
using TestServer.Interfaces;
using TestServer.Services;

namespace TestServer
{
    public class Program
    {
        static void Main(string[] args)
        {
            var server = new ServerService();

            var endpoint = server.GetLocalEndpoint();
            server.Start(endpoint);
        }
    }
}
