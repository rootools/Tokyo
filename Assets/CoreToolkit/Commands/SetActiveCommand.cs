using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreToolkit.Command {
    public class SetActiveCommand : BaseCommand {

        private GameObject _targetObject;
        private bool _targetStatus;

        public SetActiveCommand(GameObject targetObject, bool targetStatus) {
            _targetObject = targetObject;
            _targetStatus = targetStatus;
        }

        protected override void ExecInternal() {
            _targetObject.SetActive(_targetStatus);
            NotifyComplete();
        }

    }
}
