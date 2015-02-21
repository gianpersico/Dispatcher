using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FluentAssertions;
using System.Collections.Generic;

namespace Dispatcher.Tests
{
    [TestClass]
    public class MessageBusShould
    {
        [TestMethod]
        [ExpectedException(typeof(DuplicateCommandHandlerException))]
        public void StopTwoCommandHandlersForTheSameCommandBeingRegistered()
        {
            var handler = new Mock<IHandleCommands<TestCommand1>>().Object;
            var bus = new MessageBus();
            bus.RegisterCommandHandler<TestCommand1>(handler);
            bus.RegisterCommandHandler<TestCommand1>(handler);
        }

        [TestMethod]
        public void CorrectlyHandleARegisteredCommandSentToTheBus()
        {
            var identifier = Guid.NewGuid();
            var cmd = new TestCommand1 { Identifier = identifier };
            var handler = new Mock<IHandleCommands<TestCommand1>>();
            handler.Setup(h => h.Handle(cmd)).Callback<TestCommand1>(c =>
            {
                c.Identifier.Should().Be(identifier);
            });

            var bus = new MessageBus();
            bus.RegisterCommandHandler<TestCommand1>(handler.Object);
            bus.Send(cmd);
        }

        [TestMethod]
        [ExpectedException(typeof(UnknownCommandException))]
        public void ThrowAnExceptionWhenAnUnregisteredCommandIsSentToTheBus()
        {
            var unknownCmd = new UnknownCommand { Identifier = Guid.NewGuid() };
            var handler = new Mock<IHandleCommands<TestCommand1>>();
            var bus = new MessageBus();
            bus.RegisterCommandHandler<TestCommand1>(handler.Object);
            bus.Send(unknownCmd);
        }

        [TestMethod]
        public void CorrectlyPublishAnEventToAllRegisteredSubscribers()
        {
            var identifier1 = Guid.NewGuid();
            var evt1 = new TestEvent1 { Identifier = identifier1 };
            var identifier2 = Guid.NewGuid();
            var evt2 = new TestEvent2 { Identifier = identifier2 };

            var subscriber1 = new Mock<ISubscribeToEvents<TestEvent1>>();
            subscriber1.Setup(s => s.Handle(evt1)).Callback<TestEvent1>(e =>
            {
                e.Identifier.Should().Be(identifier1);
            });
            var subscriber2 = new Mock<ISubscribeToEvents<TestEvent1>>();
            subscriber2.Setup(s => s.Handle(evt1)).Callback<TestEvent1>(e =>
            {
                e.Identifier.Should().Be(identifier1);
            });
            var subscriber3 = new Mock<ISubscribeToEvents<TestEvent2>>();
            subscriber3.Setup(s => s.Handle(evt2)).Callback<TestEvent2>(e =>
            {
                e.Identifier.Should().Be(identifier1);
            });

            var bus = new MessageBus();
            bus.RegisterEventSubscriber(subscriber1.Object);
            bus.RegisterEventSubscriber(subscriber2.Object);
            bus.Publish(evt1);
            bus.Publish(evt2);
        }
    }
}
