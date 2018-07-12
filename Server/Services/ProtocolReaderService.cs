using System;
using System.Net.Sockets;
using System.Text;
using Server.Interfaces;
using Server.Models;

namespace Server.Services
{
    public class ProtocolReaderService : IProtocolReader
    {
        private IProtocolOrchestrator _protocolOrchestrator { get; }

        public ProtocolReaderService(IProtocolOrchestrator protocolOrchestrator)
        {
            _protocolOrchestrator = protocolOrchestrator;
        }

        private void ReadCallback(IAsyncResult ar)
        {
            var content = string.Empty;

            var state = (SocketStateHandler)ar.AsyncState;
            var handler = state.Socket.Client;
            var bytesRead = handler.EndReceive(ar);

            if (bytesRead > 0)
            {
                state.Content.Append(Encoding.ASCII.GetString(state.Buffer, 0, bytesRead));

                content = state.Content.ToString();
                if (content.IndexOf("<EOF>") > -1)
                {
                    Console.WriteLine($" >> Read {content.Length} bytes from socket: [{content}]");

                    content = content.Replace("<EOF>", "");

                    state.Callback?.Invoke(state.Socket, content);
                }
                else
                {
                    handler.BeginReceive(state.Buffer, 0, state.BufferSize, 0, ReadCallback, state);
                }
            }
        }

        public void ReadAllBytes(TcpClient client)
        {
            var socketHandler = new SocketStateHandler
            {
                Socket = client,
                Callback = _protocolOrchestrator.Process
            };

            client.Client.BeginReceive(socketHandler.Buffer, 0, socketHandler.BufferSize, 0, ReadCallback, socketHandler);
        }
    }
}
