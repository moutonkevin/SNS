using System;
using System.Net.Sockets;
using System.Text;

namespace TestServer.Models
{
    public class SocketStateHandler
    {
        public StringBuilder Content { get; set; }
        public byte[] Buffer { get; set; }
        public TcpClient Socket { get; set; }
        public int BufferSize { get; set; }
        public Func<TcpClient, string, bool> Callback { get; set; }

        public SocketStateHandler()
        {
            BufferSize = 4096;
            Buffer = new byte[BufferSize];
            Content = new StringBuilder();
        }
    }
}
