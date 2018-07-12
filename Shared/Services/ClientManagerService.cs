using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Net.Sockets;
using Shared.Interfaces;

namespace Shared.Services
{
    public class ClientManagerService : IClientManager
    {
        private readonly ConcurrentBag<Socket> _tcpClients = new ConcurrentBag<Socket>();

        public void AddClient(Socket client)
        {
            Console.WriteLine($" >> Client {client.GetHashCode()} has been added to the client list");

            _tcpClients.Add(client);
        }

        public IEnumerable<Socket> GetAll()
        {
            return _tcpClients;
        }

        public void RemoveClient(Socket client)
        {
            var isDeleteSuccessful = _tcpClients.TryTake(out client);

            Console.WriteLine(isDeleteSuccessful
                ? $" >> Client {client.GetHashCode()} has been removed from the list"
                : $" >> Client {client.GetHashCode()} could not be removed from the list");
        }
    }
}