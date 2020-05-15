using System;
using UnityEngine;
using UnityEngine.UI;

namespace CoreToolkit.Fader {
    public class CoreToolkitFader : MBSingleton<CoreToolkitFader> {

        public Action OnFadeComplete;

        private Image _faderImage;
        private CoreToolkitLerp _fadeLerp;

        private void Awake() {
            if (_faderImage == null)
                LoadFader();
        }

        private void LoadFader() {
            GameObject faderObjectPrefab = Resources.Load<GameObject>("CoreToolkitFaderCanvas");
            if (faderObjectPrefab == null)
                return;

            GameObject _faderObject = Instantiate(faderObjectPrefab, transform);
            _faderObject.name = "CoreToolkitFaderCanvas";

            _faderImage = _faderObject.GetComponentInChildren<Image>();
            _faderImage.color = new Color(1f, 1f, 1f, 0f);
            _faderImage.raycastTarget = false;
        }

        public void Fade(FadeConfig fadeConfig) {
            _fadeLerp?.Stop();

            _faderImage.raycastTarget = fadeConfig.IsLockedRaycasts;
            _faderImage.color = new Color(fadeConfig.FaderColor.r,
                                          fadeConfig.FaderColor.g,
                                          fadeConfig.FaderColor.b,
                                          fadeConfig.FromFaderAlpha);

            _fadeLerp = new CoreToolkitLerp(fadeConfig.FromFaderAlpha,
                                                       fadeConfig.ToFaderAlpha,
                                                       fadeConfig.Time);

            _fadeLerp.OnLerpUpdate += (lrp) => {
                Color currentColor = _faderImage.color;
                currentColor.a = fadeConfig.FadeFunction(lrp.Value);
                _faderImage.color = currentColor;
            };

            _fadeLerp.OnLerpEnd += (lrp) => {
                _faderImage.raycastTarget = false;
                OnFadeComplete?.Invoke();
            };
        }

    }

    public class FadeConfig {
        public float FromFaderAlpha;
        public float ToFaderAlpha;
        public float Time;
        public Color FaderColor = Color.black;
        public bool IsLockedRaycasts = true;

        public delegate float FadeFuncDelegate(float input);
        public FadeFuncDelegate FadeFunction = (float input) => { return input; };

        public FadeConfig(float from, float to, float time) {
            FromFaderAlpha = from;
            ToFaderAlpha = to;
            Time = time;
        }
    }
}
