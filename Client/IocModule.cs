using Client.Interfaces;
using Client.Services;
using Ninject.Modules;

namespace Client
{
    public class IocModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IListener>().To<ListenerService>();
        }
    }
}