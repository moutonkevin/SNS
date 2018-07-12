using System.Collections.Generic;
using System.Net.Sockets;

namespace Server.Interfaces
{
    public interface IClientManager
    {
        void RemoveClient(TcpClient client);
        void AddClient(TcpClient client);
        IEnumerable<TcpClient> GetAll();
    }
}
