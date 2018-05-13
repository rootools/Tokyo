using UnityEngine;
using System;

namespace CoreToolkit.Command {
    public class BaseCommand : ICommand {

        public bool Complete { get; protected set; }

        protected event Action<ICommand> completeEvent;

        public void Execute() {
            ExecInternal();
        }

        protected virtual void ExecInternal() { }
        protected virtual void NotifyComplete() {
            Complete = true;
            if (completeEvent != null)
                completeEvent(this);
        }

        public void AddCompleteHandler(Action<ICommand> completeHandler) {
            completeEvent += completeHandler;
        }

    }
}
