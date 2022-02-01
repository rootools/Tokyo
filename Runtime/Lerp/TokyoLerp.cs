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

        private bool _isIgnoreTimeScale;

        private float CurrentTime => (_isIgnoreTimeScale) ? UnityEngine.Time.realtimeSinceStartup : UnityEngine.Time.time;

        public bool IsPaused { get; private set; }

        public delegate void OnLerpUpdateDelegate(TokyoLerp lerp);
        public event OnLerpUpdateDelegate OnLerpUpdate;
        public event OnLerpUpdateDelegate OnLerpEnd;

        public TokyoLerp(float from, float to, float time, EaseType easeType = EaseType.Linear, bool isIgnoreTimeScale = false) {
            From = from;
            To = to;
            Time = time;
            Easing = easeType;

            _isIgnoreTimeScale = isIgnoreTimeScale;

            _startTime = CurrentTime;
            TokyoLerpManager.Instance().AddLerpTask(this);
        }

        public void Iterate() {
            if (IsPaused)
                return;

            Progress = (Time == 0f) ? 1f: Mathf.Clamp01((CurrentTime - _pauseTimeOffset - _startTime) / Time);
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
            _startPauseTime = CurrentTime;
        }

        public void Resume() {
            IsPaused = false;
            _pauseTimeOffset = CurrentTime - _startPauseTime;
        }

    }
}