using Ninject.Modules;
using Server.Interfaces;
using Server.Services;

namespace TestServer
{
    public class IocModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IClientManager>().To<ClientManagerService>().InSingletonScope();
            Bind<ITopicManager>().To<TopicService>().InSingletonScope();

            Bind<IListener>().To<ListenerService>();
            Bind<IProtocolInterpreter>().To<ProtocolInterpreterService>();
            Bind<IProtocolOrchestrator>().To<ProtocolOrchestratorService>();
            Bind<IProtocolParser>().To<ProtocolParserService>();
            Bind<IProtocolReader>().To<ProtocolReaderService>();
            Bind<IPublisher>().To<PublisherService>();
            Bind<IServer>().To<ServerService>();
        }
    }
}
