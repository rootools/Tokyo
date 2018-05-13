using System;
using System.Collections.Generic;

namespace CoreToolkit.Command {
    public class QueueCommand : BaseCommand {

        public List<ICommand> queue = new List<ICommand>();
        protected event Action<QueueCommand, ICommand> CommandCompleteEvent;

        public void Add(ICommand command) {
            queue.Add(command);
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

            if(queue.Count > 0) {
                ICommand cmd = queue[0];
                queue.RemoveAt(0);
                cmd.AddCompleteHandler(OnCommandComplete);
                cmd.Execute();
            } else {
                NotifyComplete();
            }
        }

        protected virtual void OnCommandComplete(ICommand cmd) {
            if (CommandCompleteEvent != null)
                CommandCompleteEvent(this, cmd);

            Run();
        }

        protected override void NotifyComplete() {
            base.NotifyComplete();
            CommandCompleteEvent = null;
        }

    }
}
