using Dispatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestRunner.Events
{
    public class ApplicationPinged : IEvent
    {
        public Guid Identifier { get; set; }
        public string ApplicationName { get; set; }
        public DateTime OccurredAt { get; set; }
    }
}
