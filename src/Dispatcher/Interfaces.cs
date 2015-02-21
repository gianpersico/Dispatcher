using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher
{
    public interface IMessage { }
    public interface ICommand : IMessage
    {
        Guid Identifier { get; set; }
    }

    public interface IHandleCommands<T>
    {
        IEnumerable Handle(T command);
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
