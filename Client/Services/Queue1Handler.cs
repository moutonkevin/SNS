using Client.Interfaces;

namespace Client.Services
{
    public class Queue1Handler : IHandling
    {
        public bool Handle(string message)
        {
            return true;
        }
    }
}
