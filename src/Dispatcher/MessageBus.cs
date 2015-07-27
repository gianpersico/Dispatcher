using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher
{
    public class MessageBus
    {
        private Dictionary<Type, Action<ICommand>> _commandHandlers = new Dictionary<Type, Action<ICommand>>();
        private Dictionary<Type, List<Action<IEvent>>> _eventSubscribers = new Dictionary<Type, List<Action<IEvent>>>();

        public void RegisterCommandHandler<TCommand>(IHandleCommands<TCommand> handler) where TCommand : ICommand
        {
            if (_commandHandlers.Count > 0 && _commandHandlers.ContainsKey(typeof(TCommand)))
            {
                throw new DuplicateCommandHandlerException();
            }

            _commandHandlers.Add(typeof(TCommand), c =>
            {
                var evts = handler.Handle((TCommand)c);
                if (evts != null)
                {
                    foreach (var e in evts)
                    {
                        Publish((IEvent)e);
                    }
                }
            });
        }

        public void RegisterEventSubscriber<TEvent>(ISubscribeToEvents<TEvent> subscriber) where TEvent : IEvent
        {
            if (!_eventSubscribers.ContainsKey(typeof(TEvent)))
            {
                _eventSubscribers.Add(typeof(TEvent), new List<Action<IEvent>>());
            }
            _eventSubscribers[typeof(TEvent)].Add(e => subscriber.Handle((TEvent)e));
        }

        public void Send<TCommand>(TCommand command) where TCommand : ICommand
        {
            if (_commandHandlers.ContainsKey(typeof(TCommand)))
                _commandHandlers[typeof(TCommand)](command);
            else
                throw new UnknownCommandException("No command handler registered for " + typeof(TCommand).Name);
        }

        public void Publish<TEvent>(TEvent evt) where TEvent : IEvent
        {
            var eventType = evt.GetType();
            if (_eventSubscribers.ContainsKey(eventType))
            {
                foreach (var subscriber in _eventSubscribers[eventType])
                {
                    subscriber(evt);
                }
            }
        }
    }
}
