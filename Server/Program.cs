using System.Runtime.InteropServices;
using Ninject;
using Server.Services;
using Ninject.Modules;
using Server.Interfaces;
using TestServer;

namespace Server
{
    public class Program
    {
        static void Main(string[] args)
        {
            IReadOnlyKernel kernel = new StandardKernel(new IocModule());
            var server = kernel.Get<IServer>();

            var endpoint = server.GetLocalEndpoint();
            server.Start(endpoint);
        }
    }
}
