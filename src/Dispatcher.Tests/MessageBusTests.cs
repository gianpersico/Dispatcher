using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using FluentAssertions;
using Bus = Dispatcher.MessageBus;
using System.Collections.Generic;

namespace Dispatcher.Tests
{
    [TestClass]
    public class MessageBus
    {
        [TestMethod]
        public void ProvidesTheAbilityToRegisterCommandHandlers()
        {
            var testCommand = new Mock<ICommand>().Object;
            var handler = new Mock<IHandleCommands<TestCommand>>().Object;
            var bus = new Bus();
            bus.RegisterCommandHandler<TestCommand>(handler.Handle);
            bus.CommandHandlers.Count.Should().Be(1);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateCommandHandlerException))]
        public void StopsTwoCommandHandlersForTheSameCommandBeingRegistered()
        {
            var testCommand = new Mock<ICommand>().Object;
            var handler = new Mock<IHandleCommands<TestCommand>>().Object;
            var bus = new Bus();
            bus.RegisterCommandHandler<TestCommand>(handler.Handle);
            bus.RegisterCommandHandler<TestCommand>(handler.Handle);
        }

        [TestMethod]
        public void RegisteredCommandHandlesACommandSentToTheBus()
        {
            const string testString = "Command handled successfully;";
            var outputter = new Mock<IOutputTestCommandData>();
            var handler = new TestCommandHandler(outputter.Object);
            var bus = new Bus();
            bus.RegisterCommandHandler<TestCommand>(handler.Handle);
            bus.Send(new TestCommand { Output = testString });
            outputter.Verify(o => o.Output(It.IsAny<string>()), Times.Once);
        }

        [TestMethod]
        [ExpectedException(typeof(UnknownCommandException))]
        public void UnregisteredCommandThrowsAnExceptionWhenSentToTheBus()
        {
            var unknownCommand = new Mock<ICommand>().Object;
            const string testString = "Command handled successfully;";
            var outputter = new Mock<IOutputTestCommandData>();
            var handler = new TestCommandHandler(outputter.Object);
            var bus = new Bus();
            bus.RegisterCommandHandler<TestCommand>(handler.Handle);
            bus.Send(unknownCommand);
        }
    }

    
}
