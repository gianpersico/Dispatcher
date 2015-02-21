using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher.Tests
{
    public class TestEvent1 : IEvent
    {
        public Guid Identifier { get; set; }
    }

    public class TestEvent2 : IEvent
    {
        public Guid Identifier { get; set; }
    }

    public class UnknownCommand : ICommand
    {
        public Guid Identifier { get; set; }
    }

    public class TestCommand1 : ICommand
    {
        public Guid Identifier { get; set; }
    }
}
