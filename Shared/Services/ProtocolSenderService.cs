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
        private readonly ManualResetEvent _isConnectionMade = new ManualResetEvent(false);
        private readonly ManualResetEvent _isSendOver = new ManualResetEvent(false);

        public bool Connect(Socket serverSocket, IPEndPoint endpoint)
        {
            var connectionStateModel = new ConnectionStateHandler
            {
                Socket = serverSocket,
                IsConnectionSuccessful = false
            };

            Console.WriteLine($" >> Connecting [{serverSocket.GetHashCode()}] to {endpoint}");

            serverSocket.BeginConnect(endpoint, ConnectCallback, connectionStateModel);
            _isConnectionMade.WaitOne();

            return connectionStateModel.IsConnectionSuccessful;
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
