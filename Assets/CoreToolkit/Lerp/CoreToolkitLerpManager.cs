using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreToolkit {

    public class CoreToolkitLerpManager : MBSingletonDD<CoreToolkitLerpManager> {

        private List<CoreToolkitLerp> _tasksList = new List<CoreToolkitLerp>();

        public void AddLerpTask(CoreToolkitLerp lerpParams) {
            _tasksList.Add(lerpParams);
        }

        public void RemoveLerpTask(CoreToolkitLerp lerpParams) {
            _tasksList.Remove(lerpParams);
        }

        void Update() {
            foreach(CoreToolkitLerp task in _tasksList.ToArray()) {
                task.Iterate();
            }
        }
    }
}
