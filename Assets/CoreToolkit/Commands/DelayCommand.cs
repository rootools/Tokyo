using UnityEngine;
using System.Collections;

namespace CoreToolkit.Command {
    public class DelayCommand : BaseCommand {

        private float _delayTime;

        public DelayCommand(float delayTime) {
            _delayTime = delayTime;
        }

        protected override void ExecInternal() {
			CoreToolkitCommandMB.Instance().StartCoroutine(Delay(_delayTime));
        }

        private IEnumerator Delay(float delayTime) {
            yield return new WaitForSeconds(delayTime);
            NotifyComplete();
        }

    }
}
