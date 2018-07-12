using Client.Interfaces;
using Client.Services;
using Ninject;

namespace Client
{
    public class Class1
    {
        public void Test()
        {
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
