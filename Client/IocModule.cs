using Client.Interfaces;
using Client.Services;
using Ninject.Modules;
using Shared.Interfaces;
using Shared.Services;

namespace Client
{
    public class IocModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IListener>().To<ListenerService>();
            Bind<IEndpointResolver>().To<EndpointResolver>();
            Bind<IOperation>().To<OperationService>();
            Bind<IProtocolReader>().To<ProtocolReaderService>();
            Bind<IProtocolSender>().To<ProtocolSenderService>();
        }
    }
}