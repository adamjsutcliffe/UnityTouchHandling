using System;
using UnityEngine;

namespace TweenAnimation.Models {

	/// <summary>
	/// Represents an animation curve.
	/// </summary>
	public abstract class TweenAnimation {

		/// <summary>
		/// The frame count.
		/// </summary>
		protected int frameCount = 0;

		/// <summary>
		/// The curve.
		/// </summary>
		protected Curve curve = Curve.Linear;

		/// <summary>
		/// The duration.
		/// </summary>
		protected float duration = 1.0f;

		/// <summary>
		/// The delay.
		/// </summary>
		protected float delay = 0.0f;

		/// <summary>
		/// The start time.
		/// </summary>
		protected float startTime = 0.0f;

		/// <summary>
		/// The transform.
		/// </summary>
		protected Transform transform = null;

		/// <summary>
		/// The finished.
		/// </summary>
		protected bool isFinished = false;

		/// <summary>
		/// The callback.
		/// </summary>
		protected Action finishCallback = null;

		/// <summary>
		/// The start callback.
		/// </summary>
		protected Action startCallback = null;

		/// <summary>
		/// Gets a value indicating whether this <see cref="TweenAnimation.Models.Animation"/> is finished.
		/// </summary>
		/// <value><c>true</c> if finished; otherwise, <c>false</c>.</value>
		public bool IsFinished {
			get {
				return isFinished;
			}
		}

		#region DSL methods

		/// <summary>
		/// For the specified duration.
		/// </summary>
		/// <param name="duration">Duration.</param>
		public TweenAnimation For(float duration) {
			this.duration = duration;
			return this;
		}

		/// <summary>
		/// Delay the specified duration.
		/// </summary>
		/// <param name="duration">Duration.</param>
		public TweenAnimation Delay(float delay) {
			this.delay = delay;
			return this;
		}

		/// <summary>
		/// Then the specified callback.
		/// </summary>
		/// <param name="callback">Callback.</param>
		public TweenAnimation AndThen(Action callback) {
			this.finishCallback = callback;
			return this;
		}

		/// <summary>
		/// Then the specified callback.
		/// </summary>
		/// <param name="callback">Callback.</param>
		public TweenAnimation ButBefore(Action callback) {
			this.startCallback = callback;
			return this;
		}

		/// <summary>
		/// As the specified curve.
		/// </summary>
		/// <param name="curve">Curve.</param>
		public TweenAnimation Over(Curve curve) {
			this.curve = curve;
			return this;
		}

		#endregion

		/// <summary>
		/// Initializes a new instance of the <see cref="TweenAnimation.Models.Animation"/> class.
		/// </summary>
		/// <param name="curve">Curve.</param>
		/// <param name="duration">Duration.</param>
		public TweenAnimation(Transform transform) {
			this.transform = transform;
		}

		/// <summary>
		/// Invoked once per animation tick.
		/// </summary>
		public void Update() {
			if ( transform == null || transform.gameObject == null ) {
				isFinished = true;
				return;
			}

			frameCount++;

			var timeOffset = (Time.time - startTime) * TweenScript.Instance.timeScale;

			if ( timeOffset < delay ) {
				return;
			}

			if ( timeOffset >= ( delay + duration ) ) {
				Tick( 1.0f );
				isFinished = true;

				return;
			}

			var t = ( timeOffset - delay ) / duration;
			Tick( t );
		}

		/// <summary>
		/// Starts the animation – the place to do all the calculations etc.
		/// </summary>
		public virtual TweenAnimation Start() {
			this.startTime = Time.time;
			Tick( 0.0f );

			//Debug.Log( "Starting " + GetType().Name + " at " + this.startTime );

			TweenScript.Instance.Enqueue( this );

			return this;
		}

		/// <summary>
		/// Started this instance.
		/// </summary>
		public virtual void Started() {
			if ( startCallback != null ) {
				startCallback();
			}
		}

		/// <summary>
		/// Finished this instance.
		/// </summary>
		public virtual void Finished() {
			if ( transform == null || transform.gameObject == null ) {
				return;
			}

			//Debug.Log( "Completed " + GetType().Name + " which lasted for " + (Time.time - startTime) + " (" + duration + " + " + delay + " = " + (duration + delay) + " requested) and executed over " + frameCount + " frames on " + transform );

			if ( finishCallback != null ) {
				finishCallback();
			}
		}

		/// <summary>
		/// Primary point of customization for animations.
		/// </summary>
		protected abstract void Tick(float t);

	}
}

