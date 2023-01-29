﻿using System;
using System.Collections.Generic;
using System.Linq;

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
        
        public void AddCommandCompleteHandler<T>(Action<QueueCommand, ICommand, T> completeHandler, T userArgs) {
            CommandCompleteEvent += (queue, cmd) => completeHandler?.Invoke(queue, cmd, userArgs);
        }

        public List<ICommand> GetCommandsList() {
            return _queue.ToList();
        }

        protected virtual void Run() {
            if (Complete)
                return;

            if (Terminate) {
                foreach (ICommand cmd in _queue)
                    cmd.TerminateCommand();
                NotifyComplete();
            }

            if (_queue.Count > 0) {
                ICommand cmd = _queue[0];
                cmd.AddCompleteHandler(OnCommandComplete);
                cmd.Execute();
            } else {
                NotifyComplete();
            }
        }

        protected virtual void OnCommandComplete(ICommand cmd) {
            CommandCompleteEvent?.Invoke(this, cmd);
            _queue.Remove(cmd);
            Run();
        }

        protected override void NotifyComplete() {
            base.NotifyComplete();
            CommandCompleteEvent = null;
        }

    }
}