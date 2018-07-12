using System.Collections.Generic;
using System.Net.Sockets;

namespace Shared.Interfaces
{
    public interface ITopicManager
    {
        void AddClientToTopic(TcpClient client, string name);
        void RemoveClientFromTopic(TcpClient client, string name);
        IEnumerable<TcpClient> GetAllClientFromTopic(string name);
    }
}