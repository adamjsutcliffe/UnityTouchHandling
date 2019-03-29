using System;
using UnityEngine;

namespace TweenAnimation.Models.Animations {

	/// <summary>
	/// Represents an animation curve.
	/// </summary>
	public class Clip : TweenAnimation {

		/// <summary>
		/// The v0.
		/// </summary>
		protected AnimationClip animationClip = null;

		#region DSL methods

		/// <summary>
		/// For the specified duration.
		/// </summary>
		/// <param name="duration">Duration.</param>
		/// <param name="gameObject">Game object.</param>
		public static Clip On(GameObject gameObject) {
			return new Clip( gameObject.transform );
		}

		/// <summary>
		/// For the specified duration.
		/// </summary>
		/// <param name="duration">Duration.</param>
		/// <param name="gameObject">Game object.</param>
		public static Clip On(Transform transform) {
			return new Clip( transform );
		}

		/// <summary>
		/// For the specified duration.
		/// </summary>
		/// <param name="duration">Duration.</param>
		/// <param name="script">Script.</param>
		public static Clip On(MonoBehaviour script) {
			return new Clip( script.transform );
		}

		/// <summary>
		/// From the specified v0.
		/// </summary>
		/// <param name="v0">V0.</param>
		public Clip Using(AnimationClip animationClip) {
			this.animationClip = animationClip;
			return this;
		}

		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="TweenAnimation.Models.Animation"/> class.
		/// </summary>
		/// <param name="curve">Curve.</param>
		/// <param name="duration">Duration.</param>
		public Clip(Transform transform) : base(transform) {
		}

		/// <summary>
		/// Primary point of customization for animations.
		/// </summary>
		protected override void Tick(float t) {
			animationClip.SampleAnimation( transform.gameObject, t );
		}

	}
}

