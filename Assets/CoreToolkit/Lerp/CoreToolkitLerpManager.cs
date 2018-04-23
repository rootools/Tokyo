using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreToolkit {

    public class CoreToolkitLerpManager : MBSingleton<CoreToolkitLerpManager> {

        private List<CoreToolkitLerp> _tasksList = new List<CoreToolkitLerp>();

        private List<CoreToolkitLerp> _toRemoveTaskList = new List<CoreToolkitLerp>();

        public void AddLerpTask(CoreToolkitLerp lerpParams) {
            _tasksList.Add(lerpParams);
        }

        public void RemoveLerpTask(CoreToolkitLerp lerpParams) {
            _toRemoveTaskList.Add(lerpParams);
        }

        void Update() {
            foreach(CoreToolkitLerp task in _toRemoveTaskList) {
                _tasksList.Remove(task);
            }
            _toRemoveTaskList.Clear();

            foreach(CoreToolkitLerp task in _tasksList) {
                task.Iterate();
            }
        }
    }
}
