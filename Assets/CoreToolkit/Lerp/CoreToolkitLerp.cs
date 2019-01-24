using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace CoreToolkit {

    public class CoreToolkitLerp {
        public float From;
        public float To;
        public float Time;

        private float _progress;
        public float Progress { get { return _progress; } }

        private float _value;
        public float Value { get { return _value; } }

        private float _startTime;

        public delegate void OnLerpUpdateDelegate(CoreToolkitLerp lerp);
        public event OnLerpUpdateDelegate OnLerpUpdate;
        public event OnLerpUpdateDelegate OnLerpEnd;


        public CoreToolkitLerp(float from, float to, float time) {
            From = from;
            To = to;
            Time = time;

            _startTime = UnityEngine.Time.time;
            CoreToolkitLerpManager.Instance().AddLerpTask(this);

        }

        public void Iterate() {
            _progress = Mathf.Clamp01((UnityEngine.Time.time - _startTime) / Time);
            _value = Mathf.Lerp(From, To, _progress);

            if (OnLerpUpdate != null)
                OnLerpUpdate(this);

            if (_progress == 1f)
                Terminate();
        }
        
        public void Terminate() {
            if(OnLerpEnd != null)
                OnLerpEnd(this);
            
            CoreToolkitLerpManager.Instance().RemoveLerpTask(this);
        }

    }
}
