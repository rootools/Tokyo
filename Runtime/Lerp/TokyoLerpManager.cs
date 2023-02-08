﻿using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Tokyo {

    public class TokyoLerpManager : MBSingletonDD<TokyoLerpManager> {

        private readonly List<TokyoLerp> _tasksList = new List<TokyoLerp>();

        private readonly Dictionary<float, Coroutine> _ownUnscaledFixedTimeTickUpdates = new Dictionary<float, Coroutine>();

        public void AddLerpTask(TokyoLerp lerpParams) {
            _tasksList.Add(lerpParams);

            if (lerpParams.UpdateType == UpdateType.OwnUnscaledFixedTimeTickUpdate)
                AddOwnUnscaledFixedTimeTickUpdate(lerpParams);
        }

        public void RemoveLerpTask(TokyoLerp lerpParams) {
            _tasksList.Remove(lerpParams);
            
            if (lerpParams.UpdateType == UpdateType.OwnUnscaledFixedTimeTickUpdate)
                RemoveOwnUnscaledFixedTimeTickUpdate(lerpParams);
        }

        private void AddOwnUnscaledFixedTimeTickUpdate(TokyoLerp lerpParams) {
            if (!_ownUnscaledFixedTimeTickUpdates.ContainsKey(lerpParams.OwnTimeTick))
                _ownUnscaledFixedTimeTickUpdates.Add(lerpParams.OwnTimeTick, StartCoroutine(StartOwnUnscaledFixedTimeTickUpdate(lerpParams.OwnTimeTick)));
        }
        
        private void RemoveOwnUnscaledFixedTimeTickUpdate(TokyoLerp lerpParams) {
            if (this == null)
                return;
            
            if (!_tasksList.Any(i => i.UpdateType == UpdateType.OwnUnscaledFixedTimeTickUpdate && Mathf.Approximately(i.OwnTimeTick, lerpParams.OwnTimeTick))) {
                
                if (_ownUnscaledFixedTimeTickUpdates[lerpParams.OwnTimeTick] == null)
                    return;
                
                StopCoroutine(_ownUnscaledFixedTimeTickUpdates[lerpParams.OwnTimeTick]);
                _ownUnscaledFixedTimeTickUpdates[lerpParams.OwnTimeTick] = null;
            }
        }

        private void Update() {
            foreach(TokyoLerp task in _tasksList.Where(i => i.UpdateType == UpdateType.Update).ToList()) {
                task.Iterate();
            }
        }

        private IEnumerator StartOwnUnscaledFixedTimeTickUpdate(float timeTick) {
            while (true) {
                foreach(TokyoLerp task in _tasksList.Where(i => i.UpdateType == UpdateType.OwnUnscaledFixedTimeTickUpdate && Mathf.Approximately(i.OwnTimeTick, timeTick)).ToList()) {
                    task.Iterate();
                }
                yield return new WaitForSecondsRealtime(timeTick);
            }
        }
    }
}