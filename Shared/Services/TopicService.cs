using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using Shared.Interfaces;

namespace Shared.Services
{
    public class TopicService : ITopicManager
    {
        private readonly ConcurrentDictionary<string, ConcurrentBag<Socket>> _topics =
            new ConcurrentDictionary<string, ConcurrentBag<Socket>>();

        public void AddClientToTopic(Socket client, string name)
        {
            if (_topics.ContainsKey(name))
                _topics[name].Add(client);
            else
                _topics.TryAdd(name, new ConcurrentBag<Socket> {client});

            Console.WriteLine($" >> Client {client.GetHashCode()} has been added to the topic {name}");

            foreach (var topic in _topics)
                Console.WriteLine($"[{topic.Key}] => {string.Join(",", topic.Value.Select(s => s.GetHashCode()))}");
        }

        public void RemoveClientFromTopic(Socket client, string name)
        {
            if (_topics.ContainsKey(name))
            {
                var isDeleteSuccessful = _topics[name].TryTake(out client);

                Console.WriteLine(isDeleteSuccessful
                    ? $" >> Client {client.GetHashCode()} has been removed from topic {name}"
                    : $" >> Client {client.GetHashCode()} could not be removed from topic {name}");
            }
            else
            {
                Console.WriteLine($" >> Client {client.GetHashCode()} did not subscribe to the topic {name}");
            }
        }

        public IEnumerable<Socket> GetAllClientFromTopic(string name)
        {
            return _topics.ContainsKey(name) ? _topics[name] : new ConcurrentBag<Socket>();
        }
    }
}