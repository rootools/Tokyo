using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

namespace CoreToolkit.Command {
    public class MoveObjectToSceneCommand : BaseCommand {

        private GameObject _obj;
        private string _name;

        public MoveObjectToSceneCommand(GameObject obj, string name) {
            _obj = obj;
            _name = name;
        }

        protected override void ExecInternal() {
            _obj.transform.parent = null;
            SceneManager.MoveGameObjectToScene(_obj, SceneManager.GetSceneByName(_name));

            NotifyComplete();
        }

    }
}
