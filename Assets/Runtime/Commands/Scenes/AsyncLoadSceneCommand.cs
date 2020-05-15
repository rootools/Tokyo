using UnityEngine;
using UnityEngine.SceneManagement;
using System;
using System.Collections;

namespace CoreToolkit.Command {
    public class AsyncLoadSceneCommand : BaseCommand {

        private readonly string _sceneName;

        public delegate void OnProgressChangeDelegate(float progress);
        public OnProgressChangeDelegate OnProgressChange;

        public AsyncLoadSceneCommand(string sceneName) {
            _sceneName = sceneName;
        }

        protected override void ExecInternal() {
            CoreToolkitCommandMB.Instance().StartCoroutine(LoadScene());
        }

        private IEnumerator LoadScene() {
            AsyncOperation ao = SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Additive);

            while(!ao.isDone) {
                if (OnProgressChange != null)
                    OnProgressChange(ao.progress);
                yield return null;
            }

            NotifyComplete();
        }

    }
}
