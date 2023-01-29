using System;

namespace Tokyo.Command {
    public class BaseCommand : ICommand {

        public bool Completed { get; protected set; }
        public bool Terminated { get; protected set; }

        protected event Action<ICommand> completeEvent;

        public void Execute() {
            ExecInternal();
        }

        public void TerminateCommand() {
            TerminateInternal();
        }

        protected virtual void ExecInternal() { }
        protected virtual void NotifyComplete() {
            Completed = true;
            completeEvent?.Invoke(this);
        }
        
        protected virtual void TerminateInternal() {
            Terminated = true;
            completeEvent?.Invoke(this);
            completeEvent = null;
        }

        public void AddCompleteHandler(Action<ICommand> completeHandler) {
            completeEvent += completeHandler;
        }
        
        public void RemoveCompleteHandler(Action<ICommand> completeHandler) {
            completeEvent -= completeHandler;
        }
        
        public void AddCompleteHandler<T>(Action<ICommand, T> completeHandler, T userArgs) {
            completeEvent += cmd => completeHandler?.Invoke(cmd, userArgs);
        }

    }
}