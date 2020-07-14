﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CoreToolkit.Math;

namespace CoreToolkit {

    public class CoreToolkitLerp {
        public float From;
        public float To;
        public float Time;
        public EaseType Easing = EaseType.Linear;

        private float _progress;
        public float Progress { get { return _progress; } }

        private float _value;
        public float Value { get { return _value; } }

        private float _startTime;

        public delegate void OnLerpUpdateDelegate(CoreToolkitLerp lerp);
        public event OnLerpUpdateDelegate OnLerpUpdate;
        public event OnLerpUpdateDelegate OnLerpEnd;


        public CoreToolkitLerp(float from, float to, float time, EaseType easeType = EaseType.Linear) {
            From = from;
            To = to;
            Time = time;
            Easing = easeType;

            _startTime = UnityEngine.Time.time;
            CoreToolkitLerpManager.Instance().AddLerpTask(this);
        }

        public void Iterate() {
            _progress = Mathf.Clamp01((UnityEngine.Time.time - _startTime) / Time);
            _value = Mathf.Lerp(From, To, CoreToolkitEasings.Ease(_progress, Easing));

            if (OnLerpUpdate != null)
                OnLerpUpdate(this);

            if (_progress == 1f) {
                if(OnLerpEnd != null)
                    OnLerpEnd(this);
                CoreToolkitLerpManager.Instance().RemoveLerpTask(this);
            }
        }

        public void Stop() {
            OnLerpEnd = null;
            CoreToolkitLerpManager.Instance().RemoveLerpTask(this);
        }

    }
}