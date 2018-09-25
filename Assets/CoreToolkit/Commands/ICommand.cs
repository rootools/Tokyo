using System;

namespace CoreToolkit.Command {
    public interface ICommand {
        bool Complete { get; }
        bool Terminate { get; }
        void AddCompleteHandler(Action<ICommand> completeHandler);
        void Execute();
        void TerminateCommand();
    }
}
