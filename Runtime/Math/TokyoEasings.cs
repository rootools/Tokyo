using UnityEngine;

namespace Tokyo.Math {

    // Source from: https://easings.net/

    public class TokyoEasings {

        private const float BACK_C1 = 1.70158f;
        private const float BACK_C2 = BACK_C1 * 1.525f;
        private const float BACK_C3 = BACK_C1 + 1f;
        private const float ELASTIC_C4 = (2f * Mathf.PI) / 3f;
        private const float ELASTIC_C5 = (2f * Mathf.PI) / 4.5f;
        private const float BOUNCE_N1 = 7.5625f;
        private const float BOUNCE_D1 = 2.75f;

        public static float Ease(float param, EaseType easeType = EaseType.Linear) {
            param = Mathf.Clamp01(param);

            switch (easeType) {
                case EaseType.Linear:
                    return Linear(param);

                case EaseType.InSine:
                    return InSine(param);

                case EaseType.OutSine:
                    return OutSine(param);

                case EaseType.InOutSine:
                    return InOutSine(param);

                case EaseType.InQuad:
                    return InQuad(param);

                case EaseType.OutQuad:
                    return OutQuad(param);

                case EaseType.InOutQuad:
                    return InOutQuad(param);

                case EaseType.InCubic:
                    return InCubic(param);

                case EaseType.OutCubic:
                    return OutCubic(param);

                case EaseType.InOutCubic:
                    return InOutCubic(param);

                case EaseType.InQuart:
                    return InQuart(param);

                case EaseType.OutQuart:
                    return OutQuart(param);

                case EaseType.InOutQuart:
                    return InOutQuart(param);

                case EaseType.InQuint:
                    return InQuint(param);

                case EaseType.OutQuint:
                    return OutQuint(param);

                case EaseType.InOutQuint:
                    return InOutQuint(param);

                case EaseType.InExpo:
                    return InExpo(param);

                case EaseType.OutExpo:
                    return OutExpo(param);

                case EaseType.InOutExpo:
                    return InOutExpo(param);

                case EaseType.InCirc:
                    return InCirc(param);

                case EaseType.OutCirc:
                    return OutCirc(param);

                case EaseType.InOutCirc:
                    return InOutCirc(param);

                case EaseType.InBack:
                    return InBack(param);

                case EaseType.OutBack:
                    return OutBack(param);

                case EaseType.InOutBack:
                    return InOutBack(param);

                case EaseType.InElastic:
                    return InElastic(param);

                case EaseType.OutElastic:
                    return OutElastic(param);

                case EaseType.InOutElastic:
                    return InOutElastic(param);

                case EaseType.InBounce:
                    return InBounce(param);

                case EaseType.OutBounce:
                    return OutBounce(param);

                case EaseType.InOutBounce:
                    return InOutBounce(param);
            }

            return Linear(param);
        }

        private static float Linear(float param) {
            return param;
        }

        private static float InSine(float param) {
            return 1f - Mathf.Cos((param * Mathf.PI) / 2f);
        }

        private static float OutSine(float param) {
            return Mathf.Sin((param * Mathf.PI) / 2f);
        }

        private static float InOutSine(float param) {
            return -(Mathf.Cos(Mathf.PI * param) - 1f) / 2f;
        }

        private static float InQuad(float param) {
            return param * param;
        }

        private static float OutQuad(float param) {
            return 1f - (1f - param) * (1f - param);
        }

        private static float InOutQuad(float param) {
            return (param < 0.5f) ? 2f * param * param : 1f - Mathf.Pow(-2f * param + 2f, 2f) / 2f;
        }

        private static float InCubic(float param) {
            return param * param * param;
        }

        private static float OutCubic(float param) {
            return 1f - Mathf.Pow(1f - param, 3f);
        }

        private static float InOutCubic(float param) {
            return (param < 0.5f) ? 4f * param * param * param : 1f - Mathf.Pow(-2f * param + 2f, 3f) / 2f;
        }

        private static float InQuart(float param) {
            return param * param * param * param;
        }

        private static float OutQuart(float param) {
            return 1f - Mathf.Pow(1f - param, 4f);
        }

        private static float InOutQuart(float param) {
            return (param < 0.5f) ? 8f * param * param * param * param : 1f - Mathf.Pow(-2f * param + 2f, 4f) / 2f;
        }

        private static float InQuint(float param) {
            return param * param * param * param * param;
        }

        private static float OutQuint(float param) {
            return 1f - Mathf.Pow(1f - param, 5f);
        }

        private static float InOutQuint(float param) {
            return (param < 0.5f) ? 16f * param * param * param * param * param : 1f - Mathf.Pow(-2f * param + 2f, 5f) / 2f;
        }

        private static float InExpo(float param) {
            return (param == 0f) ? 0f : Mathf.Pow(2f, 10f * param - 10f);
        }

        private static float OutExpo(float param) {
            return (param == 1f) ? 1f : 1f - Mathf.Pow(2f, -10f * param);
        }

        private static float InOutExpo(float param) {
            if (param == 0f)
                return 0f;
            if (param == 1f)
                return 1f;

            return (param < 0.5f) ? Mathf.Pow(2f, 20f * param - 10f) / 2f
                                  : (2f - Mathf.Pow(2f, -20f * param + 10f)) / 2f;
        }

        private static float InCirc(float param) {
            return 1f - Mathf.Sqrt(1f - Mathf.Pow(param, 2f));
        }

        private static float OutCirc(float param) {
            return Mathf.Sqrt(1f - Mathf.Pow(param - 1f, 2f));
        }

        private static float InOutCirc(float param) {
            return (param < 0.5f) ? (1f - Mathf.Sqrt(1f - Mathf.Pow(2f * param, 2f))) / 2f
                                  : (Mathf.Sqrt(1f - Mathf.Pow(-2f * param + 2f, 2f)) + 1f) / 2f;
        }

        private static float InBack(float pararm) {
            return BACK_C3 * pararm * pararm * pararm - BACK_C1 * pararm * pararm;
        }

        private static float OutBack(float param) {
            return 1f + BACK_C3 * Mathf.Pow(param - 1f, 3f) + BACK_C1 * Mathf.Pow(param - 1f, 2f);
        }

        private static float InOutBack(float param) {
            return (param < 0.5f) ? (Mathf.Pow(2f * param, 2f) * ((BACK_C2 + 1f) * 2f * param - BACK_C2)) / 2f
                                  : (Mathf.Pow(2f * param - 2f, 2f) * ((BACK_C2 + 1f) * (param * 2f - 2f) + BACK_C2) + 2f) / 2f;
        }

        private static float InElastic(float param) {
            if (param == 0f)
                return 0f;
            if (param == 1f)
                return 1f;

            return -Mathf.Pow(2f, 10f * param - 10f) * Mathf.Sin((param * 10f - 10.75f) * ELASTIC_C4);
        }

        private static float OutElastic(float param) {
            if (param == 0f)
                return 0f;
            if (param == 1f)
                return 1f;

            return Mathf.Pow(2f, -10f * param) * Mathf.Sin((param * 10f - 0.75f) * ELASTIC_C4) + 1f;
        }

        private static float InOutElastic(float param) {
            if (param == 0f)
                return 0f;
            if (param == 1f)
                return 1f;

            return (param < 0.5f) ? -(Mathf.Pow(2f, 20f * param - 10f) * Mathf.Sin((20f * param - 11.125f) * ELASTIC_C5)) / 2f
                                  : (Mathf.Pow(2f, -20f * param + 10f) * Mathf.Sin((20f * param - 11.125f) * ELASTIC_C5)) / 2f + 1f;
        }

        private static float InBounce(float param) {
            return 1f - OutBounce(1f - param);
        }

        private static float OutBounce(float param) {
            if (param < 1f / BOUNCE_D1) {
                return BOUNCE_N1 * param * param;
            } else if (param < 2f / BOUNCE_D1) {
                return BOUNCE_N1 * (param -= 1.5f / BOUNCE_D1) * param + 0.75f;
            } else if (param < 2.5f / BOUNCE_D1) {
                return BOUNCE_N1 * (param -= 2.25f / BOUNCE_D1) * param + 0.9375f;
            } else {
                return BOUNCE_N1 * (param -= 2.625f / BOUNCE_D1) * param + 0.984375f;
            }
        }

        private static float InOutBounce(float param) {
            return (param < 0.5f) ? (1f - OutBounce(1f - 2f * param)) / 2f
                                  : (1f + OutBounce(2f * param - 1f)) / 2f;
        }
    }
}