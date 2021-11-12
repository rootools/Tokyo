using System.Collections;
using UnityEngine;

namespace Tokyo.Command {

    public class WaitNextFrameCommand : BaseCommand {
        private Coroutine _delayCoroutine;

        protected override void ExecInternal() {
            _delayCoroutine = TokyoCommandMB.Instance().StartCoroutine(Delay());
        }

        private IEnumerator Delay() {
            yield return new WaitForEndOfFrame();
            NotifyComplete();
        }
    }
}