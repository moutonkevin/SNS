namespace TestServer.Interfaces
{
    public enum MessageType
    {
        Subscribe,
        Publish,
        Unknown
    }

    public interface IProtocolParser
    {
        MessageType GetMessageType(string message);
        string GetBody(string message);
    }
}
