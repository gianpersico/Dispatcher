using Dispatcher;
using System;
using System.Collections;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRunner.Commands;
using TestRunner.Events;

namespace TestRunner
{
    public class ApplicationPingAggregate : IHandleCommands<LogApplicationPing>, ISubscribeToEvents<ApplicationPinged>
    {
        public ApplicationPingAggregate()
        {
            
        }

        public IEnumerable Handle(LogApplicationPing command)
        {
            Console.WriteLine(">>CMD: Application '{0}' pinged successfully at '{1}'", command.ApplicationName, command.OccurredAt);

            yield return (new ApplicationPinged
            {
                Identifier = Guid.NewGuid(),
                ApplicationName = "Test Application 1",
                OccurredAt = DateTime.UtcNow
            });
        }

        public void Handle(ApplicationPinged @event)
        {
            Console.WriteLine(">>EVT: Application '{0}' pinged successfully at '{1}'", @event.ApplicationName, @event.OccurredAt);
        }
    }

}
