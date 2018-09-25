﻿using UnityEngine;
using System;

namespace CoreToolkit.Command {
    public class BaseCommand : ICommand {

        public bool Complete { get; protected set; }
        public bool Terminate { get; protected set; }

        protected event Action<ICommand> completeEvent;

        public void Execute() {
            ExecInternal();
        }

        public void TerminateCommand() {
            TerminateInternal();
        }

        protected virtual void ExecInternal() { }
        protected virtual void NotifyComplete() {
            Complete = true;
            if (completeEvent != null)
                completeEvent(this);
        }
        protected virtual void TerminateInternal() {
            Terminate = true;
            completeEvent = null;
        }

        public void AddCompleteHandler(Action<ICommand> completeHandler) {
            completeEvent += completeHandler;
        }

    }
}
