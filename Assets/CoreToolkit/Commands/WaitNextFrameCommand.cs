using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreToolkit.Command {

    public class WaitNextFrameCommand : BaseCommand {
        private Coroutine _delayCoroutine;

        protected override void ExecInternal() {
            _delayCoroutine = CoreToolkitCommandMB.Instance().StartCoroutine(Delay());
        }

        private IEnumerator Delay() {
            yield return new WaitForEndOfFrame();
            NotifyComplete();
        }
    }
}