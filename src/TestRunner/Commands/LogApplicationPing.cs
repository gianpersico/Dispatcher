using Dispatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRunner.Commands
{
    public class LogApplicationPing : ICommand
    {
        public Guid Identifier { get; set; }
        public string ApplicationName { get; set; }
        public DateTime OccurredAt { get; set; }
    }
}
