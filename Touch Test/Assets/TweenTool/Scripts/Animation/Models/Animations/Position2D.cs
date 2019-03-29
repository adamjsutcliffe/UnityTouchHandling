using System;
using UnityEngine;

namespace TweenAnimation.Models.Animations
{
    /// <summary>
    /// Represents an animation curve.
    /// </summary>
    public class Position2D : TweenAnimation
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
        /// Should use position instead of localPosition?
        /// </summary>
        protected bool useGlobalValues;

        #region DSL methods

        /// <summary>
        /// For the specified duration.
        /// </summary>
        public static Position2D On(GameObject gameObject)
        {
            return new Position2D(gameObject.transform);
        }

        /// <summary>
        /// For the specified duration.
        /// </summary>
        public static Position2D On(Transform transform)
        {
            return new Position2D(transform);
        }

        public static Position2D GlobalOn(Transform transform)
        {
            return new Position2D(transform) {useGlobalValues = true};
        }


        /// <summary>
        /// For the specified duration.
        /// </summary>
        public static Position2D On(MonoBehaviour script)
        {
            return new Position2D(script.transform);
        }

        /// <summary>
        /// From the specified v0.
        /// </summary>
        public Position2D From(Vector3 v0)
        {
            this.v0 = v0;
            return this;
        }

        /// <summary>
        /// To the specified v1.
        /// </summary>
        public Position2D To(Vector3 v1)
        {
            this.v1 = v1;
            return this;
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="TweenAnimation.Models.Animation"/> class.
        /// </summary>
        public Position2D(Transform transform) : base(transform)
        {
            this.v0 = useGlobalValues ? transform.position : transform.localPosition;
        }

        /// <summary>
        /// Primary point of customization for animations.
        /// </summary>
        protected override void Tick(float t)
        {
            if (v0 == v1)
            {
                return;
            }

            Vector3 vt = curve.Interpolate(v0, v1, t);

            if (useGlobalValues)
            {
                vt.z = transform.localPosition.z;
                transform.position = vt;
            }
            else
            {
                transform.localPosition = vt;
            }
        }
    }
}