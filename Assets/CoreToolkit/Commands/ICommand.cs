using System;

namespace CoreToolkit.Command {
    public interface ICommand {
        bool Complete { get; }
        void AddCompleteHandler(Action<ICommand> completeHandler);
        void Execute();
    }
}
