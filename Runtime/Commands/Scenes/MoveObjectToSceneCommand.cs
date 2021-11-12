using UnityEngine;
using UnityEngine.SceneManagement;

namespace Tokyo.Command {
    public class MoveObjectToSceneCommand : BaseCommand {

        private readonly GameObject _obj;
        private readonly string _sceneName;

        public MoveObjectToSceneCommand(GameObject obj, string sceneName) {
            _obj = obj;
            _sceneName = sceneName;
        }

        protected override void ExecInternal() {
            _obj.transform.parent = null;
            SceneManager.MoveGameObjectToScene(_obj, SceneManager.GetSceneByName(_sceneName));

            NotifyComplete();
        }

    }
}