namespace Client.Services
{
    public interface IHandling
    {
        bool Handle(string message);
    }

    public class Queue1Handler : IHandling
    {
        public bool Handle(string message)
        {
            return true;
        }
    }
}
