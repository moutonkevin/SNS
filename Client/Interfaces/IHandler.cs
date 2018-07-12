using System;

namespace Client.Interfaces
{
    public interface IHandler
    {
        IListener Using(Func<object, bool> predicate);
    }
}
