using System;

namespace Tokyo.Command {
    public interface ICommand {
        bool Completed { get; }
        bool Terminated { get; }
        void AddCompleteHandler(Action<ICommand> completeHandler);
        void RemoveCompleteHandler(Action<ICommand> completeHandler);
        void Execute();
        void TerminateCommand();
    }
}