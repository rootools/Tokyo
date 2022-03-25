using UnityEngine;
using Tokyo.Math;

namespace Tokyo {

    public class TokyoLerp {

        public delegate void OnLerpUpdateDelegate(TokyoLerp lerp);
        public event OnLerpUpdateDelegate OnLerpUpdate;
        public event OnLerpUpdateDelegate OnLerpEnd;

        public float From { get; private set; }
        public float To { get; private set; }
        public float Time { get; private set; }
        public EaseType Easing  { get; private set; }

        public float Progress { get; private set; }
        public float Value { get; private set; }
        public bool IsPaused { get; private set; }
        public bool IsStopped { get; private set; }

        private float CurrentTime => (_isIgnoreTimeScale) ? UnityEngine.Time.realtimeSinceStartup : UnityEngine.Time.time;

        private readonly float _startTime;
        private readonly bool _isIgnoreTimeScale;

        private float _startPauseTime;
        private float _pauseTimeOffset;
        private float _manualSetProgress;

        public TokyoLerp(float time, EaseType easeType = EaseType.Linear, bool isIgnoreTimeScale = false) : this(0f, 1f, time, easeType, isIgnoreTimeScale) {}

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

            if (Time == 0f)
                Progress = 1f;
            else {
                float passedTime = CurrentTime - _pauseTimeOffset - _startTime;
                float requiredTime = Time - (Time * _manualSetProgress);

                Progress = (requiredTime == 0f) ? 1f : Mathf.Clamp01(passedTime / requiredTime);
            }

            Value = Mathf.Lerp(From, To, TokyoEasings.Ease(Progress, Easing));

            OnLerpUpdate?.Invoke(this);

            if (Progress == 1f) {
                OnLerpEnd?.Invoke(this);
                Stop();
            }
        }

        public void Stop() {
            OnLerpUpdate = null;
            OnLerpEnd = null;
            TokyoLerpManager.Instance().RemoveLerpTask(this);
            IsStopped = true;
        }

        public void Pause() {
            IsPaused = true;
            _startPauseTime = CurrentTime;
        }

        public void Resume() {
            IsPaused = false;
            _pauseTimeOffset = CurrentTime - _startPauseTime;
        }

        public void SetProgress(float progress) {
            _manualSetProgress = progress;
        }

    }
}