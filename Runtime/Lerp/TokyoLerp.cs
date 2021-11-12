using UnityEngine;
using Tokyo.Math;

namespace Tokyo {

    public class TokyoLerp {
        public float From;
        public float To;
        public float Time;
        public EaseType Easing = EaseType.Linear;

        public float Progress { get; private set; }
        public float Value { get; private set; }

        private float _startTime;

        private float _startPauseTime;
        private float _pauseTimeOffset;

        public bool IsPaused { get; private set; }

        public delegate void OnLerpUpdateDelegate(TokyoLerp lerp);
        public event OnLerpUpdateDelegate OnLerpUpdate;
        public event OnLerpUpdateDelegate OnLerpEnd;

        public TokyoLerp(float from, float to, float time, EaseType easeType = EaseType.Linear) {
            From = from;
            To = to;
            Time = time;
            Easing = easeType;

            _startTime = UnityEngine.Time.time;
            TokyoLerpManager.Instance().AddLerpTask(this);
        }

        public void Iterate() {
            if (IsPaused)
                return;

            Progress = Mathf.Clamp01((UnityEngine.Time.time - _pauseTimeOffset - _startTime) / Time);
            Value = Mathf.Lerp(From, To, TokyoEasings.Ease(Progress, Easing));

            OnLerpUpdate?.Invoke(this);

            if (Progress == 1f) {
                OnLerpEnd?.Invoke(this);
                TokyoLerpManager.Instance().RemoveLerpTask(this);
            }
        }

        public void Stop() {
            OnLerpUpdate = null;
            OnLerpEnd = null;
            TokyoLerpManager.Instance().RemoveLerpTask(this);
        }

        public void Pause() {
            IsPaused = true;
            _startPauseTime = UnityEngine.Time.time;
        }

        public void Resume() {
            IsPaused = false;
            _pauseTimeOffset = UnityEngine.Time.time - _startPauseTime;
        }

    }
}