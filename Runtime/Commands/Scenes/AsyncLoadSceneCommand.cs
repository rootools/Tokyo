using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace Tokyo.Command {
    public class AsyncLoadSceneCommand : BaseCommand {

        private readonly string _sceneName;

        public delegate void OnProgressChangeDelegate(float progress);
        public OnProgressChangeDelegate OnProgressChange;

        public AsyncLoadSceneCommand(string sceneName) {
            _sceneName = sceneName;
        }

        protected override void ExecInternal() {
            TokyoCommandMB.Instance().StartCoroutine(LoadScene());
        }

        private IEnumerator LoadScene() {
            AsyncOperation ao = SceneManager.LoadSceneAsync(_sceneName, LoadSceneMode.Additive);

            while(!ao.isDone) {
                OnProgressChange?.Invoke(ao.progress);
                yield return null;
            }

            NotifyComplete();
        }

    }
}