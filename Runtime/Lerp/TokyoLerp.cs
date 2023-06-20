﻿using UnityEngine;
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
        public UpdateType UpdateType { get; private set; }
        public float OwnTimeTick { get; private set; }

        public float Progress { get; private set; }
        public float Value { get; private set; }
        public bool IsPaused { get; private set; }
        public bool IsEnded { get; private set; }

        private float CurrentTime => (_isIgnoreTimeScale) ? UnityEngine.Time.realtimeSinceStartup : UnityEngine.Time.time;

        private readonly float _startTime;
        private readonly bool _isIgnoreTimeScale;
        private float _passedTime;
        private float _prevTickTime;
        
        private float _manualSetProgress;
        private float _manualTimeScale = 1f;

        public TokyoLerp(float time, EaseType easeType = EaseType.Linear, UpdateType updateType = UpdateType.Update, bool isIgnoreTimeScale = false, float ownTimeTick = 1f) : this(0f, 1f, time, easeType, updateType, isIgnoreTimeScale, ownTimeTick) {}

        public TokyoLerp(float from, float to, float time, EaseType easeType = EaseType.Linear, UpdateType updateType = UpdateType.Update, bool isIgnoreTimeScale = false, float ownTimeTick = 1f) {
            From = from;
            To = to;
            Time = time;
            Easing = easeType;
            UpdateType = updateType;

            _isIgnoreTimeScale = isIgnoreTimeScale;

            OwnTimeTick = ownTimeTick;

            _startTime = CurrentTime;
            _prevTickTime = CurrentTime;
            _passedTime = 0f;
            TokyoLerpManager.Instance().AddLerpTask(this);
        }

        public void Iterate() {
            if (IsPaused)
                return;

            if (Time == 0f)
                Progress = 1f;
            else {
                float tickTime = (CurrentTime - _prevTickTime) * _manualTimeScale; 
                
                _passedTime += tickTime;

                _prevTickTime = CurrentTime;

                float passedTimeAndManualProgress = _passedTime + (Time * _manualSetProgress);

                Progress = Mathf.Clamp01(passedTimeAndManualProgress / Time);
            }

            Value = Mathf.Lerp(From, To, TokyoEasings.Ease(Progress, Easing));

            OnLerpUpdate?.Invoke(this);

            if (Mathf.Approximately(Progress,1f)) {
                OnLerpEnd?.Invoke(this);
                Stop();
            }
        }

        public void Stop() {
            OnLerpUpdate = null;
            OnLerpEnd = null;
            IsEnded = true;
            
            TokyoLerpManager.Instance().RemoveLerpTask(this);
        }

        public void Pause() {
            IsPaused = true;
        }

        public void Resume() {
            IsPaused = false;
        }

        public void SetProgress(float progress) {
            _manualSetProgress = progress;
        }

        public void SetManualTimeScale(float timeScale) {
            _manualTimeScale = timeScale;
        }

    }
}