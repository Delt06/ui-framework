﻿using System;
using static UnityEngine.Mathf;

namespace DELTation.UI.Easing
{
    public static class EaseExtensions
    {
        /// <summary>
        ///     Returns the value of the easing function at point t.
        /// </summary>
        /// <param name="ease">Easing function</param>
        /// <param name="t">Point where to get value at (clamped between 0 and 1).</param>
        /// <param name="overshoot">How much to overshoot the target value (applicable only for some easing functions)</param>
        /// <returns>Value at t.</returns>
        /// <exception cref="ArgumentOutOfRangeException">Invalid easing function.</exception>
        public static float GetValue(this EaseMode ease, float t, float overshoot = 1.70158f)
        {
            t = Clamp01(t);

            // Beware of magic numbers

            switch (ease)
            {
                case EaseMode.Linear: return t;
                case EaseMode.InSine: return 1 - Cos(t * PI / 2);
                case EaseMode.OutSine: return Sin(t * PI / 2);
                case EaseMode.InOutSine: return -(Cos(PI * t) - 1) / 2;
                case EaseMode.InQuad: return t * t;
                case EaseMode.OutQuad: return 1 - (1 - t) * (1 - t);
                case EaseMode.InOutQuad: return t < 0.5f ? 2 * t * t : 1 - (-2 * t + 2) * (-2 * t + 2) / 2;
                case EaseMode.InCubic: return t * t * t;
                case EaseMode.OutCubic: return 1 - (1 - t) * (1 - t) * (1 - t);
                case EaseMode.InOutCubic:
                    return t < 0.5f ? 4 * t * t * t : 1 - (-2 * t + 2) * (-2 * t + 2) * (-2 * t + 2) / 2;
                case EaseMode.InQuart: return t * t * t * t;
                case EaseMode.OutQuart: return 1 - (1 - t) * (1 - t) * (1 - t) * (1 - t);
                case EaseMode.InOutQuart:
                    return t < 0.5f
                        ? 8 * t * t * t * t
                        : 1 - (-2 * t + 2) * (-2 * t + 2) * (-2 * t + 2) * (-2 * t + 2) / 2;
                case EaseMode.InQuint: return t * t * t * t * t;
                case EaseMode.OutQuint: return 1 - (1 - t) * (1 - t) * (1 - t) * (1 - t) * (1 - t);
                case EaseMode.InOutQuint:
                    return t < 0.5f
                        ? 16 * t * t * t * t * t
                        : 1 - (-2 * t + 2) * (-2 * t + 2) * (-2 * t + 2) * (-2 * t + 2) * (-2 * t + 2) / 2;
                case EaseMode.InExpo: return t == 0 ? 0 : Pow(2, 10 * t - 10);
                case EaseMode.OutExpo: return t == 1 ? 1 : 1 - Pow(2, -10 * t);
                case EaseMode.InOutExpo:
                    return t == 0
                        ? 0
                        : t == 1
                            ? 1
                            : t < 0.5
                                ? Pow(2, 20 * t - 10) / 2
                                : (2 - Pow(2, -20 * t + 10)) / 2;
                case EaseMode.InCirc: return 1 - Sqrt(1 - t * t);
                case EaseMode.OutCirc: return Sqrt(1 - Pow(t - 1, 2));
                case EaseMode.InOutCirc:
                    return t < 0.5
                        ? (1 - Sqrt(1 - 4 * t * t)) / 2
                        : (Sqrt(1 - (-2 * t + 2) * (-2 * t + 2)) + 1) / 2;
                case EaseMode.InBack: return (overshoot + 1) * t * t * t - overshoot * t * t;
                case EaseMode.OutBack:
                    return 1 + (overshoot + 1) * (t - 1) * (t - 1) * (t - 1) + overshoot * (t - 1) * (t - 1);
                case EaseMode.InOutBack:
                {
                    var c2 = overshoot * 1.525f;
                    return t < 0.5
                        ? 4 * t * t * ((c2 + 1) * 2 * t - c2) / 2
                        : ((2 * t - 2) * (2 * t - 2) * ((c2 + 1) * (t * 2 - 2) + c2) + 2) / 2;
                }
                case EaseMode.InElastic:
                    return t == 0
                        ? 0
                        : t == 1
                            ? 1
                            : -Pow(2, 10 * t - 10) * Sin((t * 10 - 10.75f) * (2 * PI / 3));
                case EaseMode.OutElastic:
                    return t == 0
                        ? 0
                        : t == 1
                            ? 1
                            : Pow(2, -10 * t) * Sin((t * 10 - 0.75f) * (2 * PI / 3)) + 1;
                case EaseMode.InOutElastic:
                {
                    const float c5 = 2 * PI / 4.5f;

                    return t == 0
                        ? 0
                        : t == 1
                            ? 1
                            : t < 0.5
                                ? -(Pow(2, 20 * t - 10) * Sin((20 * t - 11.125f) * c5)) / 2
                                : Pow(2, -20 * t + 10) * Sin((20 * t - 11.125f) * c5) / 2 + 1;
                }
                case EaseMode.InBounce: return 1 - EaseMode.OutBounce.GetValue(1 - t);
                case EaseMode.OutBounce:
                {
                    const float n1 = 7.5625f;
                    const float d1 = 2.75f;

                    if (t < 1 / d1) return n1 * t * t;
                    if (t < 2 / d1) return n1 * (t -= 1.5f / d1) * t + 0.75f;
                    if (t < 2.5 / d1) return n1 * (t -= 2.25f / d1) * t + 0.9375f;

                    return n1 * (t -= 2.625f / d1) * t + 0.984375f;
                }
                case EaseMode.InOutBounce:
                    return t < 0.5
                        ? (1 - EaseMode.OutBounce.GetValue(1 - 2 * t)) / 2
                        : (1 + EaseMode.OutBounce.GetValue(2 * t - 1)) / 2;
                default:
                    throw new ArgumentOutOfRangeException(nameof(ease), ease, "Unknown ease function.");
            }
        }
    }
}