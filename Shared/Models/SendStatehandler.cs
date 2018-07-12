using System.Net.Sockets;

namespace Shared.Models
{
    public class SendStatehandler
    {
        public Socket Socket { get; set; }

        public bool IsSendSuccessful { get; set; }
    }
}
