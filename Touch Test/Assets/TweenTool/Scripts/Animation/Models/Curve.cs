using System;
using UnityEngine;

namespace TweenAnimation.Models
{

    /// <summary>
    /// Represents an animation curve.
    /// </summary>
    public abstract class Curve
    {
        /// <summary>
        /// Represents linear curve.
        /// </summary>
        public class LinearCurve : Curve
        {
            /// <summary>
            /// Interpolates the vector.
            /// </summary>
            /// <returns>The vector.</returns>
            /// <param name="v0">V0.</param>
            /// <param name="v1">V1.</param>
            /// <param name="t">T.</param>
            public override Vector3 Interpolate(Vector3 v0, Vector3 v1, float t)
            {
                return v0 + (v1 - v0) * t;
            }

            /// <summary>
            /// Interpolates the quaternion.
            /// </summary>
            /// <returns>The vector.</returns>
            /// <param name="v0">V0.</param>
            /// <param name="v1">V1.</param>
            /// <param name="t">T.</param>
            /// <param name="q0">Q0.</param>
            /// <param name="q1">Q1.</param>
            public override Quaternion Interpolate(Quaternion q0, Quaternion q1, float t)
            {
                var eulerAngles = q0.eulerAngles + (q1.eulerAngles - q0.eulerAngles) * t;
                return Quaternion.Euler(eulerAngles);
            }
        }

        /// <summary>
        /// Represents linear curve.
        /// </summary>
        public class CubicEaseInCurve : Curve
        {
            /// <summary>
            /// Interpolates the vector.
            /// </summary>
            /// <returns>The vector.</returns>
            /// <param name="v0">V0.</param>
            /// <param name="v1">V1.</param>
            /// <param name="t">T.</param>
            public override Vector3 Interpolate(Vector3 v0, Vector3 v1, float t)
            {
                return v0 + (v1 - v0) * t * t * t;
            }

            /// <summary>
            /// Interpolates the quaternion.
            /// </summary>
            /// <returns>The vector.</returns>
            /// <param name="v0">V0.</param>
            /// <param name="v1">V1.</param>
            /// <param name="t">T.</param>
            /// <param name="q0">Q0.</param>
            /// <param name="q1">Q1.</param>
            public override Quaternion Interpolate(Quaternion q0, Quaternion q1, float t)
            {
                var eulerAngles = q0.eulerAngles + (q1.eulerAngles - q0.eulerAngles) * t * t * t;
                return Quaternion.Euler(eulerAngles);
            }
        }

        /// <summary>
        /// Represents linear curve.
        /// </summary>
        public class CubicEaseInOutCurve : Curve
        {
            /// <summary>
            /// Interpolates the vector.
            /// </summary>
            /// <returns>The vector.</returns>
            /// <param name="v0">V0.</param>
            /// <param name="v1">V1.</param>
            /// <param name="t">T.</param>
            public override Vector3 Interpolate(Vector3 v0, Vector3 v1, float t)
            {
                t = t * 2.0f;

                if (t < 1)
                {
                    return v0 + (v1 - v0) * 0.5f * t * t * t;
                }

                t -= 2.0f;

                return v0 + (v1 - v0) * 0.5f * (t * t * t + 2);
            }

            /// <summary>
            /// Interpolates the quaternion.
            /// </summary>
            /// <returns>The vector.</returns>
            /// <param name="v0">V0.</param>
            /// <param name="v1">V1.</param>
            /// <param name="t">T.</param>
            /// <param name="q0">Q0.</param>
            /// <param name="q1">Q1.</param>
            public override Quaternion Interpolate(Quaternion q0, Quaternion q1, float t)
            {
                var eulerAngles = Vector3.zero;

                t = t * 2.0f;

                if (t < 1)
                {
                    eulerAngles = q0.eulerAngles + (q1.eulerAngles - q0.eulerAngles) * 0.5f * t * t * t;
                }
                else
                {
                    t -= 2.0f;
                    eulerAngles = q0.eulerAngles + (q1.eulerAngles - q0.eulerAngles) * 0.5f * (t * t * t + 2);
                }

                return Quaternion.Euler(eulerAngles);
            }
        }

        /// <summary>
        /// Represents linear curve.
        /// </summary>
        public class CubicEaseOutCurve : Curve
        {
            /// <summary>
            /// Interpolates the vector.
            /// </summary>
            /// <returns>The vector.</returns>
            /// <param name="v0">V0.</param>
            /// <param name="v1">V1.</param>
            /// <param name="t">T.</param>
            public override Vector3 Interpolate(Vector3 v0, Vector3 v1, float t)
            {
                return v0 + (v1 - v0) * (1 + (t - 1) * (t - 1) * (t - 1));
            }

            /// <summary>
            /// Interpolates the quaternion.
            /// </summary>
            /// <returns>The vector.</returns>
            /// <param name="v0">V0.</param>
            /// <param name="v1">V1.</param>
            /// <param name="t">T.</param>
            /// <param name="q0">Q0.</param>
            /// <param name="q1">Q1.</param>
            public override Quaternion Interpolate(Quaternion q0, Quaternion q1, float t)
            {
                var eulerAngles = q0.eulerAngles + (q1.eulerAngles - q0.eulerAngles) * (1 + (t - 1) * (t - 1) * (t - 1));
                return Quaternion.Euler(eulerAngles);
            }
        }

        /// <summary>
        /// Represents linear curve.
        /// </summary>
        public class QuarticEaseInCurve : Curve
        {
            /// <summary>
            /// Interpolates the vector.
            /// </summary>
            /// <returns>The vector.</returns>
            /// <param name="v0">V0.</param>
            /// <param name="v1">V1.</param>
            /// <param name="t">T.</param>
            public override Vector3 Interpolate(Vector3 v0, Vector3 v1, float t)
            {
                return v0 + (v1 - v0) * t * t * t * t;
            }

            /// <summary>
            /// Interpolates the quaternion.
            /// </summary>
            /// <returns>The vector.</returns>
            /// <param name="v0">V0.</param>
            /// <param name="v1">V1.</param>
            /// <param name="t">T.</param>
            /// <param name="q0">Q0.</param>
            /// <param name="q1">Q1.</param>
            public override Quaternion Interpolate(Quaternion q0, Quaternion q1, float t)
            {
                var eulerAngles = q0.eulerAngles + (q1.eulerAngles - q0.eulerAngles) * t * t * t * t;
                return Quaternion.Euler(eulerAngles);
            }
        }

        /// <summary>
        /// Represents linear curve.
        /// </summary>
        public class QuarticEaseInOutCurve : Curve
        {
            /// <summary>
            /// Interpolates the vector.
            /// </summary>
            /// <returns>The vector.</returns>
            /// <param name="v0">V0.</param>
            /// <param name="v1">V1.</param>
            /// <param name="t">T.</param>
            public override Vector3 Interpolate(Vector3 v0, Vector3 v1, float t)
            {
                t = t * 2.0f;

                if (t < 1)
                {
                    return v0 + (v1 - v0) * 0.5f * t * t * t * t;
                }

                t -= 2.0f;

                return v0 + (v1 - v0) * 0.5f * (-t * t * t * t + 2);
            }

            /// <summary>
            /// Interpolates the quaternion.
            /// </summary>
            /// <returns>The vector.</returns>
            /// <param name="v0">V0.</param>
            /// <param name="v1">V1.</param>
            /// <param name="t">T.</param>
            /// <param name="q0">Q0.</param>
            /// <param name="q1">Q1.</param>
            public override Quaternion Interpolate(Quaternion q0, Quaternion q1, float t)
            {
                var eulerAngles = Vector3.zero;

                t = t * 2.0f;

                if (t < 1)
                {
                    eulerAngles = q0.eulerAngles + (q1.eulerAngles - q0.eulerAngles) * 0.5f * t * t * t * t;
                }
                else
                {
                    t -= 2.0f;
                    eulerAngles = q0.eulerAngles + (q1.eulerAngles - q0.eulerAngles) * 0.5f * (-t * t * t * t + 2);
                }

                return Quaternion.Euler(eulerAngles);
            }
        }

        /// <summary>
        /// Represents linear curve.
        /// </summary>
        public class QuarticEaseOutCurve : Curve
        {

            /// <summary>
            /// Interpolates the vector.
            /// </summary>
            /// <returns>The vector.</returns>
            /// <param name="v0">V0.</param>
            /// <param name="v1">V1.</param>
            /// <param name="t">T.</param>
            public override Vector3 Interpolate(Vector3 v0, Vector3 v1, float t)
            {
                return v0 + (v1 - v0) * (1 + (t - 1) * (t - 1) * (t - 1) * (t - 1));
            }

            /// <summary>
            /// Interpolates the quaternion.
            /// </summary>
            /// <returns>The vector.</returns>
            /// <param name="v0">V0.</param>
            /// <param name="v1">V1.</param>
            /// <param name="t">T.</param>
            /// <param name="q0">Q0.</param>
            /// <param name="q1">Q1.</param>
            public override Quaternion Interpolate(Quaternion q0, Quaternion q1, float t)
            {
                var eulerAngles = q0.eulerAngles + (q1.eulerAngles - q0.eulerAngles) * (1 + (t - 1) * (t - 1) * (t - 1) * (t - 1));
                return Quaternion.Euler(eulerAngles);
            }
        }

        /// <summary>
        /// Represents linear curve.
        /// </summary>
        public class QuadricEaseInCurve : Curve
        {

            /// <summary>
            /// Interpolates the vector.
            /// </summary>
            /// <returns>The vector.</returns>
            /// <param name="v0">V0.</param>
            /// <param name="v1">V1.</param>
            /// <param name="t">T.</param>
            public override Vector3 Interpolate(Vector3 v0, Vector3 v1, float t)
            {
                return v0 + (v1 - v0) * t * t;
            }

            /// <summary>
            /// Interpolates the quaternion.
            /// </summary>
            /// <returns>The vector.</returns>
            /// <param name="v0">V0.</param>
            /// <param name="v1">V1.</param>
            /// <param name="t">T.</param>
            /// <param name="q0">Q0.</param>
            /// <param name="q1">Q1.</param>
            public override Quaternion Interpolate(Quaternion q0, Quaternion q1, float t)
            {
                var eulerAngles = q0.eulerAngles + (q1.eulerAngles - q0.eulerAngles) * t * t;
                return Quaternion.Euler(eulerAngles);
            }
        }

        /// <summary>
        /// Represents linear curve.
        /// </summary>
        public class QuadricEaseInOutCurve : Curve
        {
            /// <summary>
            /// Interpolates the vector.
            /// </summary>
            /// <returns>The vector.</returns>
            /// <param name="v0">V0.</param>
            /// <param name="v1">V1.</param>
            /// <param name="t">T.</param>
            public override Vector3 Interpolate(Vector3 v0, Vector3 v1, float t)
            {
                t = t * 2.0f;

                if (t < 1)
                {
                    return v0 + (v1 - v0) * 0.5f * t * t;
                }

                t -= 1.0f;

                return v0 - (v1 - v0) * 0.5f * (t * (t - 2) - 1);
            }

            /// <summary>
            /// Interpolates the quaternion.
            /// </summary>
            /// <returns>The vector.</returns>
            /// <param name="v0">V0.</param>
            /// <param name="v1">V1.</param>
            /// <param name="t">T.</param>
            /// <param name="q0">Q0.</param>
            /// <param name="q1">Q1.</param>
            public override Quaternion Interpolate(Quaternion q0, Quaternion q1, float t)
            {
                var eulerAngles = Vector3.zero;

                t = t * 2.0f;

                if (t < 1)
                {
                    eulerAngles = q0.eulerAngles + (q1.eulerAngles - q0.eulerAngles) * 0.5f * t * t;
                }
                else
                {
                    t -= 1.0f;
                    eulerAngles = q0.eulerAngles - (q1.eulerAngles - q0.eulerAngles) * 0.5f * (t * (t - 2) - 1);
                }

                return Quaternion.Euler(eulerAngles);
            }
        }

        /// <summary>
        /// Represents linear curve.
        /// </summary>
        public class QuadricEaseOutCurve : Curve
        {
            /// <summary>
            /// Interpolates the vector.
            /// </summary>
            /// <returns>The vector.</returns>
            /// <param name="v0">V0.</param>
            /// <param name="v1">V1.</param>
            /// <param name="t">T.</param>
            public override Vector3 Interpolate(Vector3 v0, Vector3 v1, float t)
            {
                return v0 + (v1 - v0) * (-t * (t - 2));
            }

            /// <summary>
            /// Interpolates the quaternion.
            /// </summary>
            /// <returns>The vector.</returns>
            /// <param name="v0">V0.</param>
            /// <param name="v1">V1.</param>
            /// <param name="t">T.</param>
            /// <param name="q0">Q0.</param>
            /// <param name="q1">Q1.</param>
            public override Quaternion Interpolate(Quaternion q0, Quaternion q1, float t)
            {
                var eulerAngles = q0.eulerAngles + (q1.eulerAngles - q0.eulerAngles) * (-t * (t - 2));
                return Quaternion.Euler(eulerAngles);
            }
        }

        /// <summary>
        /// Bounce ease out curve.
        /// </summary>
        public class BounceEaseOutCurve : Curve
        {
            public static float BounceOut(float p)
            {
                if (p < 4f / 11.0f)
                {
                    return (121f * p * p) / 16.0f;
                }
                else if (p < 8 / 11.0)
                {
                    return (363f / 40.0f * p * p) - (99f / 10.0f * p) + 17f / 5.0f;
                }
                else if (p < 9 / 10.0)
                {
                    return (4356f / 361.0f * p * p) - (35442f / 1805.0f * p) + 16061f / 1805.0f;
                }
                else
                {
                    return (54f / 5.0f * p * p) - (513f / 25.0f * p) + 268f / 25.0f;
                }
            }

            public override Vector3 Interpolate(Vector3 v0, Vector3 v1, float t)
            {
                return v0 + (v1 - v0) * BounceOut(t);
            }

            public override Quaternion Interpolate(Quaternion q0, Quaternion q1, float t)
            {
                var eulerAngles = q0.eulerAngles + (q1.eulerAngles - q0.eulerAngles) * BounceOut(t);
                return Quaternion.Euler(eulerAngles);
            }
        }

        /// <summary>
        /// Bounce ease in curve.
        /// </summary>
        public class BounceEaseInCurve : Curve
        {
            public static float BounceIn(float p)
            {
                return 1 - BounceEaseOutCurve.BounceOut(1 - p);
            }

            public override Vector3 Interpolate(Vector3 v0, Vector3 v1, float t)
            {
                return v0 + (v1 - v0) * BounceIn(t);
            }

            public override Quaternion Interpolate(Quaternion q0, Quaternion q1, float t)
            {
                var eulerAngles = q0.eulerAngles + (q1.eulerAngles - q0.eulerAngles) * BounceIn(t);
                return Quaternion.Euler(eulerAngles);
            }
        }

        /// <summary>
        /// Bounce ease in out curve.
        /// </summary>
        public class BounceEaseInOutCurve : Curve
        {
            public static float BounceInOut(float p)
            {
                if (p < 0.5f)
                {
                    return 0.5f * BounceEaseInCurve.BounceIn(p * 2);
                }
                else
                {
                    return 0.5f * BounceEaseOutCurve.BounceOut(p * 2 - 1) + 0.5f;
                }
            }

            public override Vector3 Interpolate(Vector3 v0, Vector3 v1, float t)
            {
                return v0 + (v1 - v0) * BounceInOut(t);
            }

            public override Quaternion Interpolate(Quaternion q0, Quaternion q1, float t)
            {
                var eulerAngles = q0.eulerAngles + (q1.eulerAngles - q0.eulerAngles) * BounceInOut(t);
                return Quaternion.Euler(eulerAngles);
            }
        }

        /// <summary>
        /// Back ease in curve.
        /// </summary>
        public class BackEaseInCurve : Curve
        {
            /// <summary>
            /// Modeled after the overshooting cubic y = x^3-x*sin(x*pi)
            /// </summary>
            private float BackIn(float t)
            {
                return t * t * t - t * Mathf.Sin(t * Mathf.PI);
            }

            public override Vector3 Interpolate(Vector3 v0, Vector3 v1, float t)
            {
                return v0 + (v1 - v0) * BackIn(t);
            }

            public override Quaternion Interpolate(Quaternion q0, Quaternion q1, float t)
            {
                var eulerAngles = q0.eulerAngles + (q1.eulerAngles - q0.eulerAngles) * BackIn(t);
                return Quaternion.Euler(eulerAngles);
            }
        }

        /// <summary>
        /// Back ease out curve.
        /// </summary>
        public class BackEaseOutCurve : Curve
        {
            /// <summary>
            /// Modeled after overshooting cubic y = 1-((1-x)^3-(1-x)*sin((1-x)*pi))
            /// </summary>
            private float BackOut(float t)
            {
                float f = 1f - t;
                return 1f - (f * f * f - f * Mathf.Sin(f * Mathf.PI));
            }

            public override Vector3 Interpolate(Vector3 v0, Vector3 v1, float t)
            {
                return v0 + (v1 - v0) * BackOut(t);
            }

            public override Quaternion Interpolate(Quaternion q0, Quaternion q1, float t)
            {
                var eulerAngles = q0.eulerAngles + (q1.eulerAngles - q0.eulerAngles) * BackOut(t);
                return Quaternion.Euler(eulerAngles);
            }
        }

        /// <summary>
        /// Back ease in out curve.
        /// </summary>
        public class BackEaseInOutCurve : Curve
        {
            /// <summary>
            /// Modeled after the piecewise overshooting cubic function:
            /// y = (1/2)*((2x)^3-(2x)*sin(2*x*pi))           ; [0, 0.5)
            /// y = (1/2)*(1-((1-x)^3-(1-x)*sin((1-x)*pi))+1) ; [0.5, 1]
            /// </summary>
            private float BackInOut(float t)
            {
                if (t < 0.5f)
                {
                    float f = 2 * t;
                    return 0.5f * (f * f * f - f * Mathf.Sin(f * Mathf.PI));
                }
                else
                {
                    float f = (1 - (2 * t - 1));
                    return 0.5f * (1 - (f * f * f - f * Mathf.Sin(f * Mathf.PI))) + 0.5f;
                }
            }

            public override Vector3 Interpolate(Vector3 v0, Vector3 v1, float t)
            {
                return v0 + (v1 - v0) * BackInOut(t);
            }

            public override Quaternion Interpolate(Quaternion q0, Quaternion q1, float t)
            {
                var eulerAngles = q0.eulerAngles + (q1.eulerAngles - q0.eulerAngles) * BackInOut(t);
                return Quaternion.Euler(eulerAngles);
            }
        }

        /// <summary>
        /// Elastic ease in curve.
        /// </summary>
        public class ElasticEaseInCurve : Curve
        {
            /// <summary>
            /// Modeled after the damped sine wave y = sin(13pi/2*x)*pow(2, 10 * (x - 1))
            /// </summary>
            private float ElasticIn(float t)
            {
                return Mathf.Sin(13f * Mathf.PI / 2f * t) * Mathf.Pow(2f, 10f * (t - 1));
            }

            public override Vector3 Interpolate(Vector3 v0, Vector3 v1, float t)
            {
                return v0 + (v1 - v0) * ElasticIn(t);
            }

            public override Quaternion Interpolate(Quaternion q0, Quaternion q1, float t)
            {
                var eulerAngles = q0.eulerAngles + (q1.eulerAngles - q0.eulerAngles) * ElasticIn(t);
                return Quaternion.Euler(eulerAngles);
            }
        }

        /// <summary>
        /// Elastic ease out curve.
        /// </summary>
        public class ElasticEaseOutCurve : Curve
        {
            /// <summary>
            /// Modeled after the damped sine wave y = sin(-13pi/2*(x + 1))*pow(2, -10x) + 1
            /// </summary>
            private float ElasticOut(float t)
            {
                return Mathf.Sin(-13f * Mathf.PI / 2f * (t + 1)) * Mathf.Pow(2f, -10f * t) + 1f;
            }

            public override Vector3 Interpolate(Vector3 v0, Vector3 v1, float t)
            {
                return v0 + (v1 - v0) * ElasticOut(t);
            }

            public override Quaternion Interpolate(Quaternion q0, Quaternion q1, float t)
            {
                var eulerAngles = q0.eulerAngles + (q1.eulerAngles - q0.eulerAngles) * ElasticOut(t);
                return Quaternion.Euler(eulerAngles);
            }
        }

        /// <summary>
        /// Elastic ease in out curve.
        /// </summary>
        public class ElasticEaseInOutCurve : Curve
        {
            /// <summary>
            /// y = (1/2)*sin(13pi/2*(2*x))*pow(2, 10 * ((2*x) - 1))      ; [0,0.5)
            /// y = (1/2)*(sin(-13pi/2*((2x-1)+1))*pow(2,-10(2*x-1)) + 2) ; [0.5, 1]
            /// </summary>
            private float ElasticInOut(float t)
            {
                if (t < 0.5f)
                {
                    return 0.5f * Mathf.Sin(13f * Mathf.PI / 2f * (2f * t)) * Mathf.Pow(2, 10 * ((2 * t) - 1));
                }
                else
                {
                    return 0.5f * (Mathf.Sin(-13f * Mathf.PI / 2f * ((2f * t - 1) + 1)) * Mathf.Pow(2, -10 * (2 * t - 1)) + 2);
                }
            }

            public override Vector3 Interpolate(Vector3 v0, Vector3 v1, float t)
            {
                return v0 + (v1 - v0) * ElasticInOut(t);
            }

            public override Quaternion Interpolate(Quaternion q0, Quaternion q1, float t)
            {
                var eulerAngles = q0.eulerAngles + (q1.eulerAngles - q0.eulerAngles) * ElasticInOut(t);
                return Quaternion.Euler(eulerAngles);
            }
        }

        /// <summary>
        /// Sine ease in curve.
        /// </summary>
        public class SineEaseInCurve : Curve
        {
            /// <summary>
            /// Modeled after quarter-cycle of sine wave
            /// </summary>
            private float SineIn(float t)
            {
                return Mathf.Sin((t - 1f) * Mathf.PI / 2f) + 1f;
            }

            public override Vector3 Interpolate(Vector3 v0, Vector3 v1, float t)
            {
                return v0 + (v1 - v0) * SineIn(t);
            }

            public override Quaternion Interpolate(Quaternion q0, Quaternion q1, float t)
            {
                var eulerAngles = q0.eulerAngles + (q1.eulerAngles - q0.eulerAngles) * SineIn(t);
                return Quaternion.Euler(eulerAngles);
            }
        }

        /// <summary>
        /// Sine ease out curve.
        /// </summary>
        public class SineEaseOutCurve : Curve
        {
            /// <summary>
            /// Modeled after quarter-cycle of sine wave (different phase)
            /// </summary>
            private float SineOut(float t)
            {
                return Mathf.Sin(t * Mathf.PI / 2f);
            }

            public override Vector3 Interpolate(Vector3 v0, Vector3 v1, float t)
            {
                return v0 + (v1 - v0) * SineOut(t);
            }

            public override Quaternion Interpolate(Quaternion q0, Quaternion q1, float t)
            {
                var eulerAngles = q0.eulerAngles + (q1.eulerAngles - q0.eulerAngles) * SineOut(t);
                return Quaternion.Euler(eulerAngles);
            }
        }

        /// <summary>
        /// Sine ease in out curve.
        /// </summary>
        public class SineEaseInOutCurve : Curve
        {
            /// <summary>
            /// Modeled after half sine wave
            /// </summary>
            private float SineInOut(float t)
            {
                return 0.5f * (1f - Mathf.Cos(t * Mathf.PI));
            }

            public override Vector3 Interpolate(Vector3 v0, Vector3 v1, float t)
            {
                return v0 + (v1 - v0) * SineInOut(t);
            }

            public override Quaternion Interpolate(Quaternion q0, Quaternion q1, float t)
            {
                var eulerAngles = q0.eulerAngles + (q1.eulerAngles - q0.eulerAngles) * SineInOut(t);
                return Quaternion.Euler(eulerAngles);
            }
        }

        /// <summary>
        /// Circular ease in curve.
        /// </summary>
        public class CircularEaseInCurve : Curve
        {
            /// <summary>
            /// Modeled after shifted quadrant IV of unit circle
            /// </summary>
            private float CircularIn(float t)
            {
                return 1f - Mathf.Sqrt(1f - (t * t));
            }

            public override Vector3 Interpolate(Vector3 v0, Vector3 v1, float t)
            {
                return v0 + (v1 - v0) * CircularIn(t);
            }

            public override Quaternion Interpolate(Quaternion q0, Quaternion q1, float t)
            {
                var eulerAngles = q0.eulerAngles + (q1.eulerAngles - q0.eulerAngles) * CircularIn(t);
                return Quaternion.Euler(eulerAngles);
            }
        }

        /// <summary>
        /// Circular ease out curve.
        /// </summary>
        public class CircularEaseOutCurve : Curve
        {
            /// <summary>
            /// Modeled after shifted quadrant II of unit circle
            /// </summary>
            private float CircularOut(float t)
            {
                return Mathf.Sqrt((2f - t) * t);
            }

            public override Vector3 Interpolate(Vector3 v0, Vector3 v1, float t)
            {
                return v0 + (v1 - v0) * CircularOut(t);
            }

            public override Quaternion Interpolate(Quaternion q0, Quaternion q1, float t)
            {
                var eulerAngles = q0.eulerAngles + (q1.eulerAngles - q0.eulerAngles) * CircularOut(t);
                return Quaternion.Euler(eulerAngles);
            }
        }

        /// <summary>
        /// Circular ease in out curve.
        /// </summary>
        public class CircularEaseInOutCurve : Curve
        {
            /// <summary>
            /// Modeled after the piecewise circular function
            /// y = (1/2)(1 - sqrt(1 - 4x^2))           ; [0, 0.5)
            /// y = (1/2)(sqrt(-(2x - 3)*(2x - 1)) + 1) ; [0.5, 1]
            /// </summary>
            private float CircularInOut(float t)
            {
                if (t < 0.5f)
                {
                    return 0.5f * (1 - Mathf.Sqrt(1 - 4 * (t * t)));
                }
                else
                {
                    return 0.5f * (Mathf.Sqrt(-((2 * t) - 3) * ((2 * t) - 1)) + 1);
                }
            }

            public override Vector3 Interpolate(Vector3 v0, Vector3 v1, float t)
            {
                return v0 + (v1 - v0) * CircularInOut(t);
            }

            public override Quaternion Interpolate(Quaternion q0, Quaternion q1, float t)
            {
                var eulerAngles = q0.eulerAngles + (q1.eulerAngles - q0.eulerAngles) * CircularInOut(t);
                return Quaternion.Euler(eulerAngles);
            }
        }

        /// <summary>
        /// Exponential ease in.
        /// </summary>
        public class ExponentialEaseInCurve : Curve
        {
            /// <summary>
            /// Modeled after the exponential function y = 2^(10(x - 1))
            /// </summary>
            private float ExponentialIn(float t)
            {
                return t == 0.0f ? t : Mathf.Pow(2, 10 * (t - 1f));
            }

            public override Vector3 Interpolate(Vector3 v0, Vector3 v1, float t)
            {
                return v0 + (v1 - v0) * ExponentialIn(t);
            }

            public override Quaternion Interpolate(Quaternion q0, Quaternion q1, float t)
            {
                var eulerAngles = q0.eulerAngles + (q1.eulerAngles - q0.eulerAngles) * ExponentialIn(t);
                return Quaternion.Euler(eulerAngles);
            }
        }

        /// <summary>
        /// Exponential ease out.
        /// </summary>
        public class ExponentialEaseOutCurve : Curve
        {
            /// <summary>
            /// Modeled after the exponential function y = -2^(-10x) + 1
            /// </summary>
            private float ExponentialOut(float t)
            {
                return t == 0.0f ? t : 1f - Mathf.Pow(2f, -10f * t);
            }

            public override Vector3 Interpolate(Vector3 v0, Vector3 v1, float t)
            {
                return v0 + (v1 - v0) * ExponentialOut(t);
            }

            public override Quaternion Interpolate(Quaternion q0, Quaternion q1, float t)
            {
                var eulerAngles = q0.eulerAngles + (q1.eulerAngles - q0.eulerAngles) * ExponentialOut(t);
                return Quaternion.Euler(eulerAngles);
            }
        }

        /// <summary>
        /// Exponential ease in out.
        /// </summary>
        public class ExponentialEaseInOutCurve : Curve
        {
            /// <summary>
            /// Modeled after the exponential function y = -2^(-10x) + 1
            /// </summary>
            private float ExponentialInOut(float t)
            {
                if (t == 0.0f || t == 1.0f) return t;

                if (t < 0.5f)
                {
                    return 0.5f * Mathf.Pow(2f, (20f * t) - 10f);
                }
                else
                {
                    return 0.5f * Mathf.Pow(2f, (-20f * t) + 10f) + 1f;
                }
            }

            public override Vector3 Interpolate(Vector3 v0, Vector3 v1, float t)
            {
                return v0 + (v1 - v0) * ExponentialInOut(t);
            }

            public override Quaternion Interpolate(Quaternion q0, Quaternion q1, float t)
            {
                var eulerAngles = q0.eulerAngles + (q1.eulerAngles - q0.eulerAngles) * ExponentialInOut(t);
                return Quaternion.Euler(eulerAngles);
            }
        }

        public class TileBounceCurve : Curve
        {
            private float CurveTime(float t)
            {
                return TweenScript.Instance.TileBounceCurve.Evaluate(t);
            }

            public override Vector3 Interpolate(Vector3 v0, Vector3 v1, float t)
            {
                return v0 + (v1 - v0) * CurveTime(t);
            }

            public override Quaternion Interpolate(Quaternion q0, Quaternion q1, float t)
            {
                var eulerAngles = q0.eulerAngles + (q1.eulerAngles - q0.eulerAngles) * CurveTime(t);
                return Quaternion.Euler(eulerAngles);
            }
        }

        /// <summary>
        /// The linear.
        /// </summary>
        public static Curve Linear = new LinearCurve();

        /// <summary>
        /// The cubic ease in.
        /// </summary>
        public static Curve CubicEaseIn = new CubicEaseInCurve();
        /// <summary>
        /// The cubic ease out.
        /// </summary>
        public static Curve CubicEaseOut = new CubicEaseOutCurve();
        /// <summary>
        /// The cubic ease in out.
        /// </summary>
        public static Curve CubicEaseInOut = new CubicEaseInOutCurve();

        /// <summary>
        /// The quartic ease in.
        /// </summary>
        public static Curve QuarticEaseIn = new QuarticEaseInCurve();
        /// <summary>
        /// The quartic ease out.
        /// </summary>
        public static Curve QuarticEaseOut = new QuarticEaseOutCurve();
        /// <summary>
        /// The quartic ease in out.
        /// </summary>
        public static Curve QuarticEaseInOut = new QuarticEaseInOutCurve();

        /// <summary>
        /// The quadric ease in.
        /// </summary>
        public static Curve QuadricEaseIn = new QuadricEaseInCurve();
        /// <summary>
        /// The quadric ease out.
        /// </summary>
        public static Curve QuadricEaseOut = new QuadricEaseOutCurve();
        /// <summary>
        /// The quadric ease in out.
        /// </summary>
        public static Curve QuadricEaseInOut = new QuadricEaseInOutCurve();

        /// <summary>
        /// Bounce ease out.
        /// </summary>
        public static Curve BounceEaseOut = new BounceEaseOutCurve();
        /// <summary>
        /// The bounce ease in.
        /// </summary>
        public static Curve BounceEaseIn = new BounceEaseInCurve();
        /// <summary>
        /// The bounce ease in out.
        /// </summary>
        public static Curve BounceEaseInOut = new BounceEaseInOutCurve();

        /// <summary>
        /// The back ease in.
        /// </summary>
        public static Curve BackEaseIn = new BackEaseInCurve();
        /// <summary>
        /// The back ease out.
        /// </summary>
        public static Curve BackEaseOut = new BackEaseOutCurve();
        /// <summary>
        /// The back ease in out.
        /// </summary>
        public static Curve BackEaseInOut = new BackEaseInOutCurve();

        /// <summary>
        /// The sine ease in.
        /// </summary>
        public static Curve SineEaseIn = new SineEaseInCurve();
        /// <summary>
        /// The sine ease out.
        /// </summary>
        public static Curve SineEaseOut = new SineEaseOutCurve();
        /// <summary>
        /// The sine ease in out.
        /// </summary>
        public static Curve SineEaseInOut = new SineEaseInOutCurve();

        /// <summary>
        /// The elastic ease in.
        /// </summary>
        public static Curve ElasticEaseIn = new ElasticEaseInCurve();
        /// <summary>
        /// The elastic ease out.
        /// </summary>
        public static Curve ElasticEaseOut = new ElasticEaseOutCurve();
        /// <summary>
        /// The elastic ease in out.
        /// </summary>
        public static Curve ElasticEaseInOut = new ElasticEaseInOutCurve();

        /// <summary>
        /// The circular ease in.
        /// </summary>
        public static Curve CircularEaseIn = new CircularEaseInCurve();
        /// <summary>
        /// The circular ease out.
        /// </summary>
        public static Curve CircularEaseOut = new CircularEaseOutCurve();
        /// <summary>
        /// The circular ease in out.
        /// </summary>
        public static Curve CircularEaseInOut = new CircularEaseInOutCurve();

        /// <summary>
        /// The exponential ease in.
        /// </summary>
        public static Curve ExponentialEaseIn = new ExponentialEaseInCurve();
        /// <summary>
        /// The exponential ease out.
        /// </summary>
        public static Curve ExponentialEaseOut = new ExponentialEaseOutCurve();
        /// <summary>
        /// The exponential ease in out.
        /// </summary>
        public static Curve ExponentialEaseInOut = new ExponentialEaseInOutCurve();

        public static Curve TileBounce = new TileBounceCurve();

        /// <summary>
        /// Interpolates the vector.
        /// </summary>
        /// <returns>The vector.</returns>
        /// <param name="v0">V0.</param>
        /// <param name="v1">V1.</param>
        /// <param name="t">T.</param>
        public abstract Vector3 Interpolate(Vector3 v0, Vector3 v1, float t);

        /// <summary>
        /// Interpolate the specified q0, q1 and t.
        /// </summary>
        /// <param name="q0">Q0.</param>
        /// <param name="q1">Q1.</param>
        /// <param name="t">T.</param>
        public abstract Quaternion Interpolate(Quaternion q0, Quaternion q1, float t);

    }
}

