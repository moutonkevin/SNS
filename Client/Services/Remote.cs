using System;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using Client.Interfaces;
using Client.Models;
using Shared.Interfaces;
using Shared.Models;

namespace Client.Services
{
    public class Remote : IRemote
    {
        private readonly IProtocolReader _protocolReader;

        private readonly ManualResetEvent _isConnectionMade = new ManualResetEvent(false);

        public Remote(IProtocolReader protocolReader)
        {
            _protocolReader = protocolReader;
        }

        public Socket CreateSocket(IPAddress ipAddress)
        {
            return new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
        }

        public bool Connect(Socket serverSocket, IPEndPoint serverEndPoint)
        {
            var connectionStateModel = new ConnectionStateHandler
            {
                Socket = serverSocket,
                IsConnectionSuccessful = false
            };

            serverSocket.BeginConnect(serverEndPoint, ConnectCallback, connectionStateModel);
            _isConnectionMade.WaitOne();

            return connectionStateModel.IsConnectionSuccessful;
        }

        public void Receive(Socket serverSocket)
        {
            var message = _protocolReader.ReadAll(serverSocket);


        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                var connectionState = (ConnectionStateHandler)ar.AsyncState;

                connectionState.Socket.EndConnect(ar);

                Console.WriteLine("Socket connected to {0}", connectionState.Socket.RemoteEndPoint);

                connectionState.IsConnectionSuccessful = true;

                _isConnectionMade.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
