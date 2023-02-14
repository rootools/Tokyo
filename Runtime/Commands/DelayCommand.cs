using UnityEngine;
using System.Collections;

namespace Tokyo.Command {
    public class DelayCommand : BaseCommand {

        private readonly float _delayTime;
        private bool _isIgnoreTimeScale;
        
        private Coroutine _delayCoroutine;

        public DelayCommand(float delayTime, bool isIgnoreTimeScale = false) {
            _delayTime = delayTime;
            _isIgnoreTimeScale = isIgnoreTimeScale;
        }

        protected override void ExecInternal() {
            _delayCoroutine = TokyoCommandMB.Instance().StartCoroutine(Delay(_delayTime));
        }

        private IEnumerator Delay(float delayTime) {
            if (!_isIgnoreTimeScale)
                yield return new WaitForSeconds(delayTime);
            else
                yield return new WaitForSecondsRealtime(delayTime);
            
            NotifyComplete();
        }

        protected override void TerminateInternal() {
            if(_delayCoroutine != null && TokyoCommandMB.Instance() != null)
                TokyoCommandMB.Instance().StopCoroutine(_delayCoroutine);
            base.TerminateInternal();
        }

    }
}