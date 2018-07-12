using System;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using Shared.Interfaces;
using Shared.Models;

namespace Shared.Services
{
    public class ProtocolReaderService : IProtocolReader
    {
        private readonly ManualResetEvent _isReadComplete = new ManualResetEvent(false);

        public string Receive(Socket socket)
        {
            var socketHandler = new ReadStateHandler
            {
                Socket = socket,
            };

            Console.WriteLine($" >> Receiving for {socket.GetHashCode()}");

            socket.BeginReceive(socketHandler.Buffer, 0, 
                socketHandler.BufferSize, 0, ReadCallback,
                socketHandler);

            _isReadComplete.WaitOne();
            _isReadComplete.Reset();

            return socketHandler.Content.ToString();
        }

        private void ReadCallback(IAsyncResult ar)
        {
            var state = (ReadStateHandler) ar.AsyncState;
            var handler = state.Socket;
            var bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                state.Content.Append(Encoding.ASCII.GetString(state.Buffer, 0, bytesRead));

                var content = state.Content.ToString();
                if (content.IndexOf("<EOF>", StringComparison.Ordinal) > -1)
                {
                    Console.WriteLine($" >> Received [{content}] from [{handler.GetHashCode()}]");

                    state.Content = state.Content.Replace("<EOF>", "");

                    _isReadComplete.Set();
                }
                else
                {
                    handler.BeginReceive(state.Buffer, 0, state.BufferSize, 0, ReadCallback, state);
                }
            }
        }
    }
}