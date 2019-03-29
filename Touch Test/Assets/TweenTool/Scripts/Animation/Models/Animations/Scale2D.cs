using System;
using UnityEngine;

namespace TweenAnimation.Models.Animations {

	/// <summary>
	/// Represents an animation curve.
	/// </summary>
	public class Scale2D : TweenAnimation {

		/// <summary>
		/// The v0.
		/// </summary>
		protected Vector3 v0 = Vector3.zero;

		/// <summary>
		/// The v1.
		/// </summary>
		protected Vector3 v1 = Vector3.zero;

		#region DSL methods

		/// <summary>
		/// For the specified duration.
		/// </summary>
		/// <param name="duration">Duration.</param>
		/// <param name="gameObject">Game object.</param>
		public static Scale2D On(GameObject gameObject) {
			return new Scale2D( gameObject.transform );
		}

		/// <summary>
		/// For the specified duration.
		/// </summary>
		/// <param name="duration">Duration.</param>
		/// <param name="script">Script.</param>
		public static Scale2D On(MonoBehaviour script) {
			return new Scale2D( script.transform );
		}

		/// <summary>
		/// From the specified v0.
		/// </summary>
		/// <param name="v0">V0.</param>
		public Scale2D From(Vector3 v0) {
			this.v0 = v0;
			return this;
		}

		/// <summary>
		/// To the specified v1.
		/// </summary>
		/// <param name="v1">V1.</param>
		public Scale2D To(Vector3 v1) {
			this.v1 = v1;
			return this;
		}

		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="TweenAnimation.Models.Animation"/> class.
		/// </summary>
		/// <param name="curve">Curve.</param>
		/// <param name="duration">Duration.</param>
		public Scale2D(Transform transform) : base(transform) {
			this.v0 = transform.localScale;
		}

		/// <summary>
		/// Primary point of customization for animations.
		/// </summary>
		protected override void Tick(float t) {
			var vt = curve.Interpolate( v0, v1, t );
			transform.localScale = new Vector3( vt.x, vt.y, transform.localScale.z );
		}

	}
}

