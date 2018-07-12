using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Client.Interfaces;
using Shared.Interfaces;
using Shared.Models;

namespace Client.Services
{
    public class OperationService : IOperation
    {
        public Socket CreateSocket(IPAddress ipAddress)
        {
            return new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }
    }
}
