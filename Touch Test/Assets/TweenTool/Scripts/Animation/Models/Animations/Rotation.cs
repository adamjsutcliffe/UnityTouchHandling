using System;
using UnityEngine;

namespace TweenAnimation.Models.Animations {

	/// <summary>
	/// Represents an animation curve.
	/// </summary>
	public class Rotation : TweenAnimation {

		/// <summary>
		/// The v0.
		/// </summary>
		protected Quaternion q0 = Quaternion.identity;

		/// <summary>
		/// The v1.
		/// </summary>
		protected Quaternion q1 = Quaternion.identity;

		#region DSL methods

		/// <summary>
		/// For the specified duration.
		/// </summary>
		/// <param name="duration">Duration.</param>
		/// <param name="gameObject">Game object.</param>
		public static Rotation On(GameObject gameObject) {
			return new Rotation( gameObject.transform );
		}

		/// <summary>
		/// For the specified duration.
		/// </summary>
		/// <param name="duration">Duration.</param>
		/// <param name="script">Script.</param>
		public static Rotation On(MonoBehaviour script) {
			return new Rotation( script.transform );
		}

		/// <summary>
		/// From the specified v0.
		/// </summary>
		/// <param name="v0">V0.</param>
		public Rotation From(Quaternion q0) {
			this.q0 = q0;
			return this;
		}

		/// <summary>
		/// To the specified v1.
		/// </summary>
		/// <param name="v1">V1.</param>
		public Rotation To(Quaternion q1) {
			this.q1 = q1;
			return this;
		}

		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="TweenAnimation.Models.Animation"/> class.
		/// </summary>
		/// <param name="curve">Curve.</param>
		/// <param name="duration">Duration.</param>
		public Rotation(Transform transform) : base(transform) {
			this.q0 = transform.localRotation;
		}

		/// <summary>
		/// Primary point of customization for animations.
		/// </summary>
		protected override void Tick(float t) {
			var qt = curve.Interpolate( q0, q1, t );
			transform.localRotation = qt;
		}

	}
}

