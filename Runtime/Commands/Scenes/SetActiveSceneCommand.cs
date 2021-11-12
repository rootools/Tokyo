using UnityEngine.SceneManagement;

namespace Tokyo.Command {
    public class SetActiveSceneCommand : BaseCommand {

        private readonly string _sceneName;

        public SetActiveSceneCommand(string sceneName) {
            _sceneName = sceneName;
        }

        protected override void ExecInternal() {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(_sceneName));
            NotifyComplete();
        }

    }
}