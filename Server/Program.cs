using Ninject;
using Server.Interfaces;
using Shared.Interfaces;

namespace Server
{
    public class Program
    {
        private static void Main(string[] args)
        {
            IReadOnlyKernel kernel = new StandardKernel(new IocModule());

            var endpoint = kernel.Get<IEndpointResolver>().GetLocalEndpoint();
            var server = kernel.Get<IServer>();

            server.Start(endpoint);
        }
    }
}