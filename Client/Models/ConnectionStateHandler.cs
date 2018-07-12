using System.Net.Sockets;

namespace Client.Models
{
    public class ConnectionStateHandler
    {
        public Socket Socket { get; set; }

        public bool IsConnectionSuccessful { get; set; }
    }
}
