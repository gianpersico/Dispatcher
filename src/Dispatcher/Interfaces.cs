using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher
{
    public interface ICommand
    {
        Guid Identifier { get; }
    }

    public interface IHandleCommands<in T> where T : ICommand
    {
        void Handle(T command);
    }
}
