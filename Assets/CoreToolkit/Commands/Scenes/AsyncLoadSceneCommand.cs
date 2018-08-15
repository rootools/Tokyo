using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

namespace CoreToolkit.Command {
    public class AsyncLoadSceneCommand : BaseCommand {

        private readonly string _sceneName;

        public AsyncLoadSceneCommand(string sceneName) {
            _sceneName = sceneName;
        }

        protected override void ExecInternal() {
            AsyncOperation ao = SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Additive);
            CoreToolkitCommandMB.Instance().StartCoroutine(WaitUntilSceneLoaded(ao, () => {
                NotifyComplete();
            }));
        }

        private IEnumerator WaitUntilSceneLoaded(AsyncOperation ao, Action callback = null) {
            yield return new WaitUntil(() => ao.isDone);
            if (callback != null)
                callback();
        }

    }
}
