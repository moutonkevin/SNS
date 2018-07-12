using Ninject.Modules;
using Server.Interfaces;
using Server.Services;
using Shared.Interfaces;
using Shared.Services;

namespace Server
{
    public class IocModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IClientManager>().To<ClientManagerService>().InSingletonScope();
            Bind<ITopicManager>().To<TopicService>().InSingletonScope();

            Bind<IEndpointResolver>().To<EndpointResolver>();
            Bind<IListener>().To<ListenerService>();
            Bind<IProtocolInterpreter>().To<ProtocolInterpreterService>();
            Bind<IProtocolOrchestrator>().To<ProtocolOrchestratorService>();
            Bind<IProtocolParser>().To<ProtocolParserService>();
            Bind<IProtocolReader>().To<ProtocolReaderService>();
            Bind<IProtocolSender>().To<ProtocolSenderService>();
            Bind<IPublisher>().To<PublisherService>();
            Bind<IServer>().To<ServerService>();
        }
    }
}