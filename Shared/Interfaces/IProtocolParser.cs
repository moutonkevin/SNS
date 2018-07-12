namespace Shared.Interfaces
{
    public enum MessageType
    {
        Subscribe,
        Unsubscribe,
        Publish,
        Unknown
    }

    public interface IProtocolParser
    {
        MessageType GetMessageType(string message);
        string GetBody(string message);
    }
}