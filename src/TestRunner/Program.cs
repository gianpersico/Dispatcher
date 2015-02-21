using Dispatcher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestRunner.Commands;
using TestRunner.Events;

namespace TestRunner
{
    class Program
    {
        private static MessageBus _bus;

        static void Main(string[] args)
        {
            Bootstrap();

            _bus.Send(new LogApplicationPing
            {
                Identifier = Guid.NewGuid(),
                ApplicationName = "Test Application 1",
                OccurredAt = DateTime.UtcNow
            });

            _bus.Publish(new UnknownEvent());

            Console.Read();
        }

        static void Bootstrap()
        {
            _bus = new MessageBus();
            var agg = new ApplicationPingAggregate();
            _bus.RegisterCommandHandler<LogApplicationPing>(agg);
            _bus.RegisterEventSubscriber<ApplicationPinged>(agg);
        }
    }

    public class UnknownEvent : IEvent
    {

        public Guid Identifier
        {
            get;
            set;
        }
    }

}
