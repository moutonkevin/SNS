using System.Collections.Generic;
using System.Net.Sockets;

namespace Shared.Interfaces
{
    public interface ITopicManager
    {
        void AddClientToTopic(Socket client, string name);
        void RemoveClientFromTopic(Socket client, string name);
        IEnumerable<Socket> GetAllClientFromTopic(string name);
    }
}