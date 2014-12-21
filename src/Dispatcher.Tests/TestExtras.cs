using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dispatcher.Tests
{
    public class TestCommand : ICommand
    {
        private string _output;
        public Guid Identifier
        {
            get { throw new NotImplementedException(); }
        }

        public string Output
        {
            get { return _output; }
            set { _output = value; }
        }
    }

    public class TestCommandHandler : IHandleCommands<TestCommand>
    {
        private IOutputTestCommandData _outputter;
        public TestCommandHandler(IOutputTestCommandData outpuuter)
        {
            _outputter = outpuuter;
        }

        public void Handle(TestCommand command)
        {
            _outputter.Output(command.Output);
        }
    }

    public interface IOutputTestCommandData
    {
        string Output(string output);
    }
}
