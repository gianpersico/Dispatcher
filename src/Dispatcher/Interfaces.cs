using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher
{
    public interface IMessage { }
    public interface ICommand : IMessage
    {
        
    }

    public interface IHandleCommands<T>
    {
        IEnumerable<IEvent> Handle(T command);
    }

    public interface IEvent : IMessage
    {
        Guid Identifier { get; }
    }

    public interface ISubscribeToEvents<T>
    {
        void Handle(T @event);
    }
}
