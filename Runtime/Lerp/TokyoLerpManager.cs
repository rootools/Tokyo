using System.Collections.Generic;

namespace Tokyo {

    public class TokyoLerpManager : MBSingletonDD<TokyoLerpManager> {

        private List<TokyoLerp> _tasksList = new List<TokyoLerp>();

        public void AddLerpTask(TokyoLerp lerpParams) {
            _tasksList.Add(lerpParams);
        }

        public void RemoveLerpTask(TokyoLerp lerpParams) {
            _tasksList.Remove(lerpParams);
        }

        private void Update() {
            foreach(TokyoLerp task in _tasksList.ToArray()) {
                task.Iterate();
            }
        }
    }
}