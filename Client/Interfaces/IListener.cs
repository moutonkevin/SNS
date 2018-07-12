using Client.Services;

namespace Client.Interfaces
{
    public interface IListener
    {
        IListener Listen(string topicName);
        IListener Handle<THandler>() where THandler : class, IHandling;
        IListener Verify();
        void StartListening();
    }
}
