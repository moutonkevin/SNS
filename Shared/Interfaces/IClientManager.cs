using System.Collections.Generic;
using System.Net.Sockets;

namespace Shared.Interfaces
{
    public interface IClientManager
    {
        void RemoveClient(Socket client);
        void AddClient(Socket client);
        IEnumerable<Socket> GetAll();
    }
}