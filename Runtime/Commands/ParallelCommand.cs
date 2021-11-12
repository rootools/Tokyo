using System;
using System.Collections.Generic;

namespace Tokyo.Command {

    public class ParallelCommand : BaseCommand {

        private List<ICommand> _pool = new List<ICommand>();
        protected event Action<ParallelCommand, ICommand> CommandCompleteEvent;

        public void Add(ICommand command) {
            _pool.Add(command);
            Complete = false;
        }

        protected override void ExecInternal() {
            foreach(ICommand cmd in _pool) {
                cmd.AddCompleteHandler(OnCommandComplete);
                cmd.Execute();
            }
        }

        protected virtual void OnCommandComplete(ICommand cmd) {
            CommandCompleteEvent?.Invoke(this, cmd);

            _pool.Remove(cmd);
            if (_pool.Count == 0)
                NotifyComplete();
        }

        protected override void NotifyComplete() {
            base.NotifyComplete();
            CommandCompleteEvent = null;
        }
    }
}