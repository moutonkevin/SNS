using System.Net.Sockets;

namespace Shared.Models
{
    public class ConnectionStateHandler
    {
        public Socket Socket { get; set; }

        public bool IsConnectionSuccessful { get; set; }
    }
}
