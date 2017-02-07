//-----------------------------------------------------------------------
// <copyright file="BiteMe.cs" company="Jonas Aklin">
//     Copyright (c) Jonas Aklin. All rights reserved.
// </copyright>
//-----------------------------------------------------------------------

namespace Geometry.Transformation
{
    using Base.RuntimeChecks;
    using Geometry.Elements;
    using Geometry.Extensions;

    /// <summary>
    /// Represents a 2D transformation. Includes a rotation, scale and translation component.
    /// </summary>
    public class Transformation
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Transformation"/> class.
        /// </summary>
        public Transformation(
            double rotation,
            double scale,
            Vector2 translation)
        {
            this.Rotation = rotation;
            this.Scale = scale;
            this.Translation = translation;
        }

        /// <summary>
        /// Gets the rotation component in radians.
        /// </summary>
        public double Rotation { get; }

        /// <summary>
        /// Gets the scale component.
        /// </summary>
        public double Scale { get; }

        /// <summary>
        /// Gets the translation component.
        /// </summary>
        public Vector2 Translation { get; }

        /// <summary>
        /// Applies another transformation on top of this and creates a new combinded transformation.
        /// </summary>
        Transformation ApplyTransformationOnTop(Transformation transformationToApply)
        {
            Checks.AssertNotNull(transformationToApply, nameof(transformationToApply));

            var newRotation = this.Rotation + transformationToApply.Rotation;
            var newScale = this.Scale * transformationToApply.Scale;
            var newTranslation = this.Translation.Multiply(transformationToApply.Scale).AddVector(transformationToApply.Translation);

            return new Transformation(
                newRotation,
                newScale,
                newTranslation);
        }
    }
}
