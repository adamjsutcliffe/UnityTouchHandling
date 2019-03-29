using System;
using UnityEngine;

namespace TweenAnimation.Models.Animations {

	/// <summary>
	/// Represents an animation curve.
	/// </summary>
	public class Shake2D : TweenAnimation {

		/// <summary>
		/// The v0.
		/// </summary>
		protected Vector3 magnitude = Vector3.zero;

		/// <summary>
		/// The cycles.
		/// </summary>
		protected int times = 0;

		#region DSL methods

		/// <summary>
		/// For the specified duration.
		/// </summary>
		/// <param name="duration">Duration.</param>
		/// <param name="gameObject">Game object.</param>
		public static Shake2D On(GameObject gameObject) {
			return new Shake2D( gameObject.transform );
		}

		/// <summary>
		/// For the specified duration.
		/// </summary>
		/// <param name="duration">Duration.</param>
		/// <param name="gameObject">Game object.</param>
		public static Shake2D On(Transform transform) {
			return new Shake2D( transform );
		}

		/// <summary>
		/// For the specified duration.
		/// </summary>
		/// <param name="duration">Duration.</param>
		/// <param name="script">Script.</param>
		public static Shake2D On(MonoBehaviour script) {
			return new Shake2D( script.transform );
		}

		/// <summary>
		/// From the specified v0.
		/// </summary>
		/// <param name="v0">V0.</param>
		public Shake2D WithMagnitude(Vector3 magnitude) {
			this.magnitude = magnitude;
			return this;
		}

		/// <summary>
		/// From the specified v0.
		/// </summary>
		/// <param name="v0">V0.</param>
		public Shake2D Times(int times) {
			this.times = times;
			return this;
		}

		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="TweenAnimation.Models.Animation"/> class.
		/// </summary>
		/// <param name="curve">Curve.</param>
		/// <param name="duration">Duration.</param>
		public Shake2D(Transform transform) : base(transform) {
			this.magnitude = Vector3.zero;
		}

		/// <summary>
		/// Primary point of customization for animations.
		/// </summary>
		protected override void Tick(float t) {
			var phase = Mathf.Sin( t * Mathf.PI * 2 * times );
			var vt = curve.Interpolate( Vector3.zero, magnitude, phase );
			transform.localPosition = new Vector3( vt.x, vt.y, transform.localPosition.z );
		}

	}
}

