namespace Client.Interfaces
{
    public interface IListener
    {
        IListener WithHostName(string hostName);
        IListener WithPort(int port);
        IListener Listen(string topicName);
        IListener Handle<THandler>() where THandler : class, IHandling;
        IListener Verify();
        void StartListening();
    }
}
