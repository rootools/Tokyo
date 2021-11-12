using System;
using System.Collections.Generic;

namespace Tokyo.Command {
    public class QueueCommand : BaseCommand {

        private List<ICommand> _queue = new List<ICommand>();
        protected event Action<QueueCommand, ICommand> CommandCompleteEvent;

        public void Add(ICommand command) {
            _queue.Add(command);
            Complete = false;
        }

        protected override void ExecInternal() {
            Run();
        }

        public void AddCommandCompleteHandler(Action<QueueCommand, ICommand> completeHandler) {
            CommandCompleteEvent += completeHandler;
        }

        protected virtual void Run() {
            if (Complete)
                return;

            if(Terminate) {
                foreach (ICommand cmd in _queue)
                    cmd.TerminateCommand();
                NotifyComplete();
            }

            if(_queue.Count > 0) {
                ICommand cmd = _queue[0];
                _queue.RemoveAt(0);
                cmd.AddCompleteHandler(OnCommandComplete);
                cmd.Execute();
            } else {
                NotifyComplete();
            }
        }

        protected virtual void OnCommandComplete(ICommand cmd) {
            CommandCompleteEvent?.Invoke(this, cmd);

            Run();
        }

        protected override void NotifyComplete() {
            base.NotifyComplete();
            CommandCompleteEvent = null;
        }

    }
}