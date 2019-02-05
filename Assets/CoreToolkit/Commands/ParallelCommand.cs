using System;
using System.Collections;
using System.Collections.Generic;

namespace CoreToolkit.Command {

    public class ParallelCommand : BaseCommand {
        
        public List<ICommand> pool = new List<ICommand>();
        protected event Action<ParallelCommand, ICommand> CommandCompleteEvent;
        
        public void Add(ICommand command) {
            pool.Add(command);
            Complete = false;
        }
        
        protected override void ExecInternal() {
            foreach(ICommand cmd in pool) {
                cmd.AddCompleteHandler(OnCommandComplete);
                cmd.Execute();
            }
        }

        protected virtual void OnCommandComplete(ICommand cmd) {
            if (CommandCompleteEvent != null)
                CommandCompleteEvent(this, cmd);

            pool.Remove(cmd);
            if (pool.Count == 0)
                NotifyComplete();
        }

        protected override void NotifyComplete() {
            base.NotifyComplete();
            CommandCompleteEvent = null;
        }
    }
}