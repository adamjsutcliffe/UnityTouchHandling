using UnityEngine;
using UnityEngine.UI;

namespace TweenAnimation.Models.Animations
{
    /// <summary>
    /// Represents an animation curve.
    /// </summary>
    public class ImageColor : TweenAnimation
    {
        /// <summary>
        /// The v0.
        /// </summary>
        protected Color from;

        /// <summary>
        /// The v1.
        /// </summary>
        protected Color to;

        /// <summary>
        /// The v1.
        /// </summary>
        protected Image image;
        
        #region DSL methods
        
        /// <summary>
        /// For the specified duration.
        /// </summary>
        public static ImageColor On(Image image)
        {
            return new ImageColor(image);
        }

        /// <summary>
        /// From the specified v0.
        /// </summary>
        /// <param name="v0">V0.</param>
        public ImageColor From(Color v0)
        {
            this.@from = v0;
            return this;
        }

        /// <summary>
        /// To the specified v1.
        /// </summary>
        /// <param name="v1">V1.</param>
        public ImageColor To(Color v1)
        {
            this.to = v1;
            return this;
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="TweenAnimation.Models.Animation"/> class.
        /// </summary>
        public ImageColor(Image image) : base(image.transform)
        {
            this.image = image;
            from = image.color;
        }

        /// <summary>
        /// Primary point of customization for animations.
        /// </summary>
        protected override void Tick(float t)
        {
            if (from == to)
            {
                return;
            }
            
            image.color = Color.Lerp(from, to, t);
        }
    }
}