using System.Collections.Generic;
using System.Linq;

namespace Tokyo {

    public class TokyoLerpManager : MBSingletonDD<TokyoLerpManager> {

        private readonly List<TokyoLerp> _tasksList = new List<TokyoLerp>();

        public void AddLerpTask(TokyoLerp lerpParams) {
            _tasksList.Add(lerpParams);
        }

        public void RemoveLerpTask(TokyoLerp lerpParams) {
            _tasksList.Remove(lerpParams);
        }

        private void Update() {
            foreach(TokyoLerp task in _tasksList.ToList().Where(i => i.UpdateType == UpdateType.Update)) {
                task.Iterate();
            }
        }

        private void FixedUpdate() {
            foreach(TokyoLerp task in _tasksList.ToList().Where(i => i.UpdateType == UpdateType.FixedUpdate)) {
                task.Iterate();
            }
        }

        private void LateUpdate() {
            foreach(TokyoLerp task in _tasksList.ToList().Where(i => i.UpdateType == UpdateType.LateUpdate)) {
                task.Iterate();
            }
        }
    }
}