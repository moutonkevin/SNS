using System.Net;

namespace Server.Interfaces
{
    public interface IServer
    {
        void Start(IPEndPoint endpoint);
    }
}