using UnityEngine;
using System.Collections;

namespace CoreToolkit.Command {
    public class DelayCommand : BaseCommand {

        private float _delayTime;
        private Coroutine _delayCoroutine;

        public DelayCommand(float delayTime) {
            _delayTime = delayTime;
        }

        protected override void ExecInternal() {
            _delayCoroutine = CoreToolkitCommandMB.Instance().StartCoroutine(Delay(_delayTime));
        }

        private IEnumerator Delay(float delayTime) {
            yield return new WaitForSeconds(delayTime);
            NotifyComplete();
        }

        protected override void TerminateInternal() {
            if(_delayCoroutine != null)
                CoreToolkitCommandMB.Instance().StopCoroutine(_delayCoroutine);
            base.TerminateInternal();
        }

    }
}
