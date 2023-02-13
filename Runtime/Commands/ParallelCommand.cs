using System;
using System.Collections.Generic;
using System.Linq;

namespace Tokyo.Command {

    public class ParallelCommand : BaseCommand {

        private List<ICommand> _pool = new List<ICommand>();
        protected event Action<ParallelCommand, ICommand> CommandCompleteEvent;

        public void Add(ICommand command) {
            _pool.Add(command);
            Completed = false;
        }

        protected override void ExecInternal() {
            foreach(ICommand cmd in _pool) {
                cmd.AddCompleteHandler(OnCommandComplete);
                cmd.Execute();
            }
        }
        
        protected override void TerminateInternal() {
            foreach (ICommand cmd in _pool.ToList()) {
                cmd.RemoveCompleteHandler(OnCommandComplete);
                CommandCompleteEvent?.Invoke(this, cmd);
                
                cmd.TerminateCommand();
                _pool.Remove(cmd);
            }
            
            base.TerminateInternal();
            CommandCompleteEvent = null;
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