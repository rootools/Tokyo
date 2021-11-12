using UnityEngine.UI;

namespace Tokyo.Command {
    public class ChangeUITextCommand : BaseCommand {

        private readonly Text _targetTextObject;
        private readonly string _targetText;

        public ChangeUITextCommand(Text targetTextObject, string targetText) {
            _targetTextObject = targetTextObject;
            _targetText = targetText;
        }

        protected override void ExecInternal() {
            _targetTextObject.text = _targetText;
            NotifyComplete();
        }
    }
}