using System;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Shared.Interfaces;
using Shared.Models;

namespace Shared.Services
{
    public class ProtocolSenderService : IProtocolSender
    {
        private readonly IEndpointResolver _endpointResolver;


        private readonly ManualResetEvent _isConnectionMade = new ManualResetEvent(false);
        private readonly ManualResetEvent _isSendOver = new ManualResetEvent(false);

        public ProtocolSenderService(IEndpointResolver endpointResolver)
        {
            _endpointResolver = endpointResolver;
        }

        public Socket Connect(string hostName, int port)
        {
            var endPoint = _endpointResolver.GetEndpoint(hostName, port);
            var ipAddress = _endpointResolver.GetIp(hostName);
            var socket = new Socket(ipAddress.AddressFamily, SocketType.Stream, ProtocolType.Tcp);

            var connectionStateModel = new ConnectionStateHandler
            {
                Socket = socket,
                IsConnectionSuccessful = false
            };

            Console.WriteLine($" >> Connecting [{socket.GetHashCode()}] to {endPoint}");

            socket.BeginConnect(endPoint, ConnectCallback, connectionStateModel);
            _isConnectionMade.WaitOne();

            return connectionStateModel.IsConnectionSuccessful ? socket : null;
        }

        private void ConnectCallback(IAsyncResult ar)
        {
            try
            {
                var connectionState = (ConnectionStateHandler)ar.AsyncState;

                connectionState.Socket.EndConnect(ar);

                Console.WriteLine($" >> Connected {connectionState.Socket.GetHashCode()} to [{connectionState.Socket.RemoteEndPoint}]");

                connectionState.IsConnectionSuccessful = true;

                _isConnectionMade.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        public bool Send(Socket serverSocket, string message)
        {
            var byteData = Encoding.ASCII.GetBytes(message);
            var sendStateModel = new SendStatehandler()
            {
                Socket = serverSocket,
                IsSendSuccessful = false
            };

            Console.WriteLine($" >> Sending [{message}] to {serverSocket.GetHashCode()}");

            serverSocket.BeginSend(byteData, 0, byteData.Length, 0, SendCallback, sendStateModel);

            _isSendOver.WaitOne();

            return sendStateModel.IsSendSuccessful;
        }

        private void SendCallback(IAsyncResult ar)
        {
            try
            {
                var client = (SendStatehandler)ar.AsyncState;

                var bytesSent = client.Socket.EndSend(ar);

                Console.WriteLine($" >> Sent [{bytesSent}] bytes to {client.Socket.GetHashCode()}");

                client.IsSendSuccessful = true;

                _isSendOver.Set();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }
    }
}
