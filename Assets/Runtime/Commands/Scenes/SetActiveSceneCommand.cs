using UnityEngine.SceneManagement;

namespace CoreToolkit.Command {
    public class SetActiveSceneCommand : BaseCommand {

        private string _scene;

        public SetActiveSceneCommand(string scene) {
            _scene = scene;
        }

        protected override void ExecInternal() {
            SceneManager.SetActiveScene(SceneManager.GetSceneByName(_scene));
            NotifyComplete();
        }

    }
}
