using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Sockets;

namespace TestServer.Interfaces
{
    public interface IClientManager
    {
        void RemoveClient(TcpClient client);
        void AddClient(TcpClient client);
        IEnumerable<TcpClient> GetAll();
    }
}
