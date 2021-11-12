using UnityEngine;

namespace Tokyo.Command {
    public class SetActiveCommand : BaseCommand {

        private readonly GameObject _targetObject;
        private readonly bool _targetStatus;

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