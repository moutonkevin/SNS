using System.Threading;
using Client.Interfaces;
using Client.Services;
using Ninject;

namespace Client
{
    public class Program
    {
        private static void Main(string[] args)
        {
            Thread.Sleep(2000);

            IReadOnlyKernel kernel = new StandardKernel(new IocModule());

            var listener = kernel.Get<IListener>();

            listener
                .Listen("queue-name-1")
                .Handle<Queue1Handler>()
                .Verify()
                .StartListening();
        }
    }
}
