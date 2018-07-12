using System.Collections.Generic;
using System.Net.Sockets;

namespace Server.Interfaces
{
    public interface ITopicManager
    {
        void AddClientToTopic(TcpClient client, string name);
        void RemoveClientFromTopic(TcpClient client, string name);
        IEnumerable<TcpClient> GetAllClientFromTopic(string name);
    }
}
