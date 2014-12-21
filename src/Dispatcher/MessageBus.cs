using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher
{
    public class MessageBus
    {
        private Dictionary<Type, object> commandHandlers = new Dictionary<Type, object>();
        public Dictionary<Type, object> CommandHandlers { get { return commandHandlers; } }

        public void RegisterCommandHandler<TCommand>(Action<TCommand> cmd) where TCommand : ICommand
        {
            if (commandHandlers.Count > 0 && commandHandlers.ContainsKey(typeof(TCommand)))
            {
                throw new DuplicateCommandHandlerException();
            }
            commandHandlers.Add(typeof(TCommand), cmd);
        }

        public void Send<TCommand>(TCommand command)
        {
            if (commandHandlers.ContainsKey(typeof(TCommand)))
                (commandHandlers[typeof(TCommand)] as Action<TCommand>).Invoke(command);
            else
                throw new UnknownCommandException("No command handler registered for " + typeof(TCommand).Name);
        }
    }
}
