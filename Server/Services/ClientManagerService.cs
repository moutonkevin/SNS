using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Sockets;
using Server.Interfaces;

namespace Server.Services
{
    public class ClientManagerService : IClientManager
    {
        private readonly ConcurrentBag<TcpClient> _tcpClients = new ConcurrentBag<TcpClient>();

        public void AddClient(TcpClient client)
        {
            Console.WriteLine($" >> Client {client.GetHashCode()} has been added to the client list");

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
                ? $" >> Client {client.GetHashCode()} has been removed from the list"
                : $" >> Client {client.GetHashCode()} could not be removed from the list");
        }
    }
}