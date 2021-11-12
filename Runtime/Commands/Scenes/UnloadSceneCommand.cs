using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tokyo.Command {
    public class UnloadSceneCommand : BaseCommand {

        private readonly string _sceneName;

        public UnloadSceneCommand(string sceneName) {
            _sceneName = sceneName;
        }

        protected override void ExecInternal() {
            Scene sc = SceneManager.GetSceneByName(_sceneName);
            if(sc != null && sc.isLoaded) {
                AsyncOperation ao = SceneManager.UnloadSceneAsync(sc);
                TokyoCommandMB.Instance().StartCoroutine(WaitUntilSceneUnloaded(ao, NotifyComplete));
            } else {
                NotifyComplete();
            }
        }

        private IEnumerator WaitUntilSceneUnloaded(AsyncOperation ao, Action callback = null) {
            yield return new WaitUntil(() => ao.isDone);
            callback?.Invoke();
        }

    }
}