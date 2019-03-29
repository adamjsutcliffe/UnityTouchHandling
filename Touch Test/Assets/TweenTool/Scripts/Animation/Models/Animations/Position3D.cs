using System;
using UnityEngine;

namespace TweenAnimation.Models.Animations
{
    /// <summary>
    /// Represents an animation curve.
    /// </summary>
    public class Position3D : TweenAnimation
    {
        /// <summary>
        /// The v0.
        /// </summary>
        protected Vector3 v0 = Vector3.zero;

        /// <summary>
        /// The v1.
        /// </summary>
        protected Vector3 v1 = Vector3.zero;

        /// <summary>
        /// The v1 func.
        /// </summary>
        protected Func<Vector3> v1Func;

        /// <summary>
        /// Should use position instead of localPosition?
        /// </summary>
        protected bool useGlobalValues;

        #region DSL methods

        /// <summary>
        /// For the specified duration.
        /// </summary>
        public static Position3D On(GameObject gameObject)
        {
            return new Position3D(gameObject.transform);
        }

        /// <summary>
        /// For the specified duration.
        /// </summary>
        public static Position3D On(Transform transform)
        {
            return new Position3D(transform);
        }

        public static Position3D GlobalOn(Transform transform)
        {
            return new Position3D(transform) {useGlobalValues = true};
        }

        /// <summary>
        /// For the specified duration.
        /// </summary>
        public static Position3D On(MonoBehaviour script)
        {
            return new Position3D(script.transform);
        }

        /// <summary>
        /// From the specified v0.
        /// </summary>
        /// <param name="v0">V0.</param>
        public Position3D From(Vector3 v0)
        {
            this.v0 = v0;
            return this;
        }

        /// <summary>
        /// To the specified v1.
        /// </summary>
        /// <param name="v1">V1.</param>
        public Position3D To(Vector3 v1)
        {
            this.v1 = v1;
            return this;
        }
        /// <summary>
        /// To the specified v1.
        /// </summary>
        /// <param name="v1">V1.</param>
        public Position3D To(Func<Vector3> v1)
        {
            this.v1Func = v1;
            return this;
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="TweenAnimation.Models.Animation"/> class.
        /// </summary>
        public Position3D(Transform transform) : base(transform)
        {
            this.v0 = useGlobalValues ? transform.position : transform.localPosition;
        }

        /// <summary>
        /// Primary point of customization for animations.
        /// </summary>
        protected override void Tick(float t)
        {
            if (v1Func != null)
            {
                v1 = v1Func();
            }

            if (v0 == v1)
            {
                return;
            }

            Vector3 vt = curve.Interpolate(v0, v1, t);

            if (useGlobalValues)
            {
                transform.position = vt;
            }
            else
            {
                transform.localPosition = vt;
            }
        }
    }
}