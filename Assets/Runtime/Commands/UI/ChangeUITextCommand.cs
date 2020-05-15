using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace CoreToolkit.Command {
    public class ChangeUITextCommand : BaseCommand {

        private Text _targetTextObject;
        private string _targetText;

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