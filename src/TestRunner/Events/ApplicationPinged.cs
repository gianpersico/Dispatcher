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
        public Guid Identifier { get; private set; }
        public string ApplicationName { get; private set; }
        public DateTime OccurredAt { get; private set; }

        public ApplicationPinged(Guid identifier, string applicationName, DateTime occurredAt)
        {
            Identifier = identifier;
            ApplicationName = applicationName;
            OccurredAt = occurredAt;
        }
    }
}
