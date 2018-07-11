using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Sockets;
using TestServer.Interfaces;

namespace TestServer.Services
{
    public class ClientManagerService : IClientManager
    {
        private readonly ConcurrentBag<TcpClient> _tcpClients = new ConcurrentBag<TcpClient>();

        public void AddClient(TcpClient client)
        {
            _tcpClients.Add(client);
        }

        public IEnumerable<TcpClient> GetAll()
        {
            return _tcpClients;
        }

        public void RemoveClient(TcpClient client)
        {
            var isDeleteSuccessful = _tcpClients.TryTake(out client);

            Console.WriteLine(isDeleteSuccessful
                ? $" >> Client {client.GetHashCode()} has been removed"
                : $" >> Client {client.GetHashCode()} could not be removed");
        }
    }
}