using System;
using System.Net.Sockets;
using System.Text;

namespace Shared.Models
{
    public class ReadStateHandler
    {
        public ReadStateHandler()
        {
            BufferSize = 4096;
            Buffer = new byte[BufferSize];
            Content = new StringBuilder();
        }

        public StringBuilder Content { get; set; }
        public byte[] Buffer { get; set; }
        public TcpClient TcpClient { get; set; }
        public Socket Socket { get; set; }
        public int BufferSize { get; set; }
    }
}